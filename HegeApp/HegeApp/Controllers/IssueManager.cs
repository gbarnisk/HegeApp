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
    public class IssueManager
    {
        public List<Issue> issueList { get; set; }
        private IDownloadManager downloadManager;
        public string filePath = "";
        public List<Issue> finalList { get; set; }

        public IssueManager()
        {
            downloadManager = CrossDownloadManager.Current;

            issueList = new List<Issue>();

            IndexToDrive();
            InitializeToRAM();
            foreach (Issue thingamabob in issueList)
            {
                Console.WriteLine("I hate app" + thingamabob.ToString());
            }
            InitializeTextFile(issueList);
            List<Issue> thing = ReadFromLocal(filePath);
            foreach (Issue thing3 in thing)
            {
                Console.WriteLine("medium codebase contributor my ass. " + thing3.ToString());
            }
        }

        /*
         * Loads issues from the hard drive metadata into a list stored in RAM.
         */
        public void InitializeToRAM()
        {

        }
        //  }
        /*
         * Indexes all issues from the file host and saves the relevant metadata to the hard drive.
         */



        public void IndexToDrive()
        {

            List<Issue> issues = new List<Issue>();

            WebClient wc = new WebClient();
            using (Stream st = wc.OpenRead("https://macalesterhegemonocle.wordpress.com/2018/11/13/test/"))
            {
                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                {
                    string html = sr.ReadToEnd();

                    Regex r = new Regex(@"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)</a>");
                    //Regex t = new Regex(@"<img.*?src=(""|')(?<src>.*?)(""|').*?>(?<value>.*?)>");
                    //Regex t = new Regex(@"<img.*?src=(""|')(?<src>.*?)(""|').*?>>");
                    Regex t = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>");

                    foreach (Match match in r.Matches(html))
                    {
                        string pdfurl = match.Groups["href"].Value;
                        if (pdfurl.Contains("issue"))
                        {
                            string issueName = match.Groups["value"].Value;
                            string[] elements = pdfurl.Split(new[] { '/' });
                            string pdfFileName = elements[elements.Length - 1];
                            Console.WriteLine("look here");
                            Console.WriteLine(pdfFileName);

                            bool exists = false;
                            foreach (Issue issue in issues)
                            {
                                if (issue.CoverURI.Split(new[] { '.' })[0].Equals(pdfFileName.Split(new[] { '.' })[0])) //This condition checks if there is already an issue object with the same filename but for the cover, rather than the pdf
                                {
                                    issue.PdfURL = pdfurl;
                                    issue.PdfURI = pdfFileName;
                                    exists = true;
                                }
                            }

                            if (!exists)
                            {
                                issues.Add(new Issue(issueName, "", "", false, pdfurl, pdfFileName, false));
                            }
                        }
                    }

                    Console.WriteLine("I am groot");
                    foreach (Match match in t.Matches(html))
                    {
                        string pngurl = match.Groups[1].Value;
                        if (pngurl.Contains("cover"))
                        {
                            Console.WriteLine(pngurl);
                            string[] elements = pngurl.Split(new[] { '/', '?' });
                            string pngFileName = elements[elements.Length - 2];
                            System.Console.WriteLine(pngFileName);
                        }
                    }







                    //if (url.Contains("pdf"))
                    //{
                    //    ret.Add(url);
                    //    names.Add(name);
                    //    issueList = new List<Issue>();

                    //}
                    //for (int i = 0; i < ret.Count; i++)
                    //    {

                    //    //for (int j = 0; j < issueList.Count; j++){
                    //    //    if(issueList[j].IssueName.Equals(text)){


                    //    //    }
                    //    //}



                    //        issueList.Add(new Issue(names[i], "", "", false, ret[i] , "", false));

                    //    }






                    //



                }

            }
        }




        /*
 * Saves an object into a textfile at a specified path
 */
        public void SaveToLocal(List<Issue> issues, string filename)
        {
            //Console.WriteLine("Saved to local started");
            string currentText = ReadForText(filename);
            Console.Write("Current = " + currentText);

            using (var streamWriter = new StreamWriter(filename, true))
            {

                foreach (Issue iss in issues)
                {
                    string stringOfIssue = iss.ToString();
                    //TODO: hey Trevor!!!
                    if (!currentText.Contains(stringOfIssue))
                    {
                        streamWriter.WriteLine(iss);
                        Console.WriteLine("There is a new Issue that is: " + iss.ToString());
                    }
                }

                //Console.WriteLine(issues);
                //streamWriter.Write(issues);
                streamWriter.Close();
            }



        }

        public string ReadForText(string filename)
        {
            string content;
            using (var streamReader = new StreamReader(filename))
            {

                content = streamReader.ReadToEnd();
            }
            return content;
        }
        /*
         * reads a text file at a specified path and returns the content
         * in the form of a list of issues
         * Once IssueListFromString is completed, this should work
         */
        public List<Issue> ReadFromLocal(string filename)
        {
            using (var streamReader = new StreamReader(filename))
            {

                string content = streamReader.ReadToEnd();
                List<Issue> helloTrello = IssueListFromString(content);
                return helloTrello;
            }
        }
        /*
         * Given an output string saved in the text file, parses the code and returns it in a list of issues
         */
        public List<Issue> IssueListFromString(String issuelis1)
        {
            //Console.Write(issuelist);
            List<Issue> newList = new List<Issue>();
            string[] result = issuelis1.Split(new[] { '\r', '\n' });
            Console.WriteLine();
            foreach (String thing in result)
            {
                if (!thing.Equals(""))
                {
                    object[] elements = thing.Split(new[] { ',' });
                    int hack = 0;
                    foreach (String part in elements)
                    {
                        //Console.WriteLine("WILL'S DEBUGGER" + part);
                        hack++;
                    }
                    //Console.WriteLine(partIssue[2] + "WOO! It's happening now");
                    Issue CreatedIssue = new Issue(elements[0].ToString(), elements[1].ToString(), elements[2].ToString(),
                                                   ToBool(elements[3].ToString()), elements[4].ToString(), elements[5].ToString(),
                                                   ToBool(elements[6].ToString()));
                    Console.WriteLine("It's happening!!! ToString is: " + CreatedIssue.ToString());
                    newList.Add(CreatedIssue);
                }

            }
            return newList;
        }




        /*
         * converts a string back to a boolean
         */
        public Boolean ToBool(string value)
        {
            if (value.Equals("true") | value.Equals("True"))
            {
                return true;
            }
            if (value.Equals("false") | value.Equals("False"))
            {
                return false;
            }
            throw new ArgumentException("neither true nor false");

        }
        /*
         * locates the correct file path, and uses SaveToLocal to save a given list of issues to a text file
         */
        public void InitializeTextFile(List<Issue> issues)
        {
            Console.WriteLine(issues);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //string path;
            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    path = "/android_asset/Content/";
            //} else if (Device.RuntimePlatform == Device.iOS)
            //{
            //    path = "/Content/";
            //} else
            //{
            //    path = "";
            //}
            string filename = Path.Combine(path, "IssueListStorage.txt");
            File.Create(filename).Dispose();
            Console.WriteLine("Hey you idiot; this is the filename" + filename);
            Console.WriteLine("im loving this" + File.Exists(filename));
            filePath = filename;
            SaveToLocal(issues, filename);

        }
        /*
         * Downloads the issue from index into local storage. Using code from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager and https://stackoverflow.com/questions/43008813/xamarin-crossdownloadmanager-waiting-for-download-file
         */
        public async Task DownloadIssueAsync(int index)
        {
            await Task.Run(async () =>
            {
                Console.WriteLine("GRIFFIN'S DEBUG Download method reached");
                Console.WriteLine("GRIFFIN'S DEBUG Trying to download from " + issueList[index].PdfURL);
                Console.WriteLine("GRIFFIN'S DEBUG DownloadIssueAsync is trying to download the issue " + issueList[index]);
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
                    issueList[index].PdfPath = pdf.DestinationPathName;
                }
            });
        }

        /*
         * Downloads the cover of the issue from the index to a local location.
         */
        public async Task DownloadCoverAsync(int index)
        {
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
                    issueList[index].CoverLocal = true;
                    issueList[index].CoverPath = cover.DestinationPathName;
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
                    System.Console.WriteLine("GRIFFIN'S DEBUG Download started/in progress");
                    return true;
                case DownloadFileStatus.COMPLETED:
                    System.Console.WriteLine("GRIFFIN'S DEBUG Download completed");
                    System.Console.WriteLine("GRIFFIN'S DEBUG The completed path is: " + file.DestinationPathName);
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
