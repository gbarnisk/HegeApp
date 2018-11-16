using HegeApp.Models;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

/*
 * Controls the issue objects. Handles indexing and loading issues on app startup.
 */

namespace HegeApp.Controllers
{
    class IssueManager
    {
        public List<Issue> issueList { get; set; }
        private IDownloadManager downloadManager;

        public IssueManager()
        {
            //Following from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager
            /*CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file =>
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    return Path.Combine();
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    return Path.Combine();
                }
                else
                {
                    return Path.Combine();
                }
            });*/
            downloadManager = CrossDownloadManager.Current;

            IndexToDrive();
            InitializeToRAM();
        }

        /*
         * Loads issues from the hard drive metadata into a list stored in RAM.
         */
        public void InitializeToRAM()
        {
            //Temporary hardcoding:
            issueList = new List<Issue>();
            issueList.Add(new Issue("Life on the Hege", "", "Life_on_the_hege.png", true, "", "Hege1.pdf", true));
            issueList.Add(new Issue("The Hege Gets a Job", "", "The_Hege_gets_a_job.png", true, "", "Hege2.pdf", true));
            issueList.Add(new Issue("The Last Minute Issue", "", "The_Last_Minute_Issue.png", true, "", "Hege3.pdf", true));

            //Implement proper system
        }

        /*
         * Indexes all issues from the file host and saves the relevant metadata to the hard drive.
         */
        public void IndexToDrive()
        {
            //Implement
        }

        /*
         * Downloads the issue from index into local storage. Using code from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager and https://stackoverflow.com/questions/43008813/xamarin-crossdownloadmanager-waiting-for-download-file
         */
        public async Task DownloadIssueAsync(int index)
        {
            await Task.Run(async() =>
            {
                IDownloadFile pdf = downloadManager.CreateDownloadFile(issueList[index].PdfURL);
                downloadManager.Start(pdf);
                bool isDownloading = true;
                while (isDownloading)
                {
                    await Task.Delay(2 * 1000);
                    isDownloading = IsDownloading(pdf);
                }

                if (pdf.Status == DownloadFileStatus.COMPLETED)
                {
                    issueList[index].PdfLocal = true;
                }
            });

            await Task.Run(async () =>
            {
                IDownloadFile cover = downloadManager.CreateDownloadFile(issueList[index].CoverURL);
                downloadManager.Start(cover);
                bool isDownloading = true;
                while (isDownloading)
                {
                    await Task.Delay(2 * 1000);
                    isDownloading = IsDownloading(cover);
                }

                if (cover.Status == DownloadFileStatus.COMPLETED)
                {
                    issueList[index].PdfLocal = true;
                }
            });

        }

        /*
         * Checks if a file is still downloading. Uses code from https://stackoverflow.com/questions/43008813/xamarin-crossdownloadmanager-waiting-for-download-file
         */
         bool IsDownloading(IDownloadFile file)
        {
            if (file == null)
            {
                return false;
            }

            switch (file.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                    return true;
                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
