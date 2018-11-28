using HegeApp.Models;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            //issueList.Add(new Issue("Life on the Hege", "https://macalesterhegemonocle.files.wordpress.com/2018/11/v2_i1.pdf", "Life_on_the_hege.png", true, "https://macalesterhegemonocle.files.wordpress.com/2018/11/v9_i2.pdf", "Hege1.pdf", true));
            //issueList.Add(new Issue("The Hege Gets a Job", "", "The_Hege_gets_a_job.png", true, "", "Hege2.pdf", true));
            //issueList.Add(new Issue("The Last Minute Issue", "", "The_Last_Minute_Issue.png", true, "", "Hege3.pdf", true));
            issueList.Add(new Issue("v13 i1", "", "", false, "https://macalesterhegemonocle.files.wordpress.com/2018/11/issuev13_i1.pdf", "issuev13_i1.pdf", false));

            //Implement proper system
        }

        /*
         * Indexes all issues from the file host and saves the relevant metadata to the hard drive.
         */


       
        public void IndexToDrive()
        {

            WebClient wc = new WebClient();
            using (Stream st = wc.OpenRead("https://macalesterhegemonocle.wordpress.com/2018/11/13/test/"))
            {
                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                {
                    string html = sr.ReadToEnd();

                    Regex r = new Regex(@"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)</a>");

                    foreach (Match match in r.Matches(html))
                    {
                        string url = match.Groups["href"].Value;
                        string text = match.Groups["value"].Value;

                        Console.WriteLine(url + text);
                    }



                }
            }
        }

        /*
         * Saves an object into a textfile at a specified path
         */
        public void SaveToLocal(object issues, string filename)
        {
            Console.WriteLine("Saved to local started");
            using (var streamWriter = new StreamWriter(filename, true))
            {
                Console.WriteLine(issues);
                streamWriter.Write(issues);
            }


        }
        /*
         * reads a text file at a specified path and returns the content
         * in the form of a list of issues
         * Once IssueListFromString is completed, this should work
         */
        //public List<Issue> ReadFromLocal(string filename){
        //    using (var streamReader = new StreamReader(filename))
        //    {
        //        var content = streamReader.ReadToEnd();
        //        List<Issue> issues = IssueListFromString(content);
        //        return issues;
        //    }
        //}
        /*
         * 
         */
        //public List<Issue> IssueListFromString (String issuetext){

        //}
    
            
        /*
         * locates the correct file path, and uses SaveToLocal to save a given list of issues to a text file
         */
        public void InitializeTextFile(List<Issue> issues)
        {
            Console.WriteLine(issues);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "StoredIssues.txt");
            SaveToLocal(issues, filename);

        }
        /*
         * Downloads the issue from index into local storage. Using code from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager and https://stackoverflow.com/questions/43008813/xamarin-crossdownloadmanager-waiting-for-download-file
         */
        public async Task DownloadIssueAsync(int index)
        {
            await Task.Run(async() =>
            {
                System.Console.WriteLine("GRIFFIN'S DEBUG Download method reached");
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
                    System.Console.WriteLine("GRIFFIN'S DEBUG Download started");
                    return true;
                case DownloadFileStatus.COMPLETED:
                    System.Console.WriteLine("GRIFFIN'S DEBUG Download completed");
                    return false;
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    System.Console.WriteLine("GRIFFIN'S DEBUG Download failed");
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
