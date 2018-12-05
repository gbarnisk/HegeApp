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

            IndexToDrive();
            InitializeToRAM();
            InitializeTextFile(issueList);
            finalList = ReadFromLocal(filePath);
            Console.WriteLine("the final list size is:" +finalList.Count);

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
            List<string> ret = new List<string>();
            List<string> names = new List<string>();
            var link = new List<string>();

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


                        if (url.Contains("pdf"))
                        {
                            ret.Add(url);
                            names.Add(text);
                 
                        }
                        issueList = new List<Issue>();

                        for (int i = 0; i < ret.Count; i++)
                            {
                            issueList.Add(new Issue(names[i], "", "", false, ret[i], "", false));
                            }
                        }
                    }
                }
            }

        /*
        * Saves a list of Issues into a textfile, used to save Issue objects when app closes
         */
        public void SaveToLocal(List<Issue> issues, string filename)
        {
            List<Issue> cleanIssues = new List<Issue>();
            string currentText = ReadForText(filename);
            foreach(Issue iss in issues)
            {
                if (!cleanIssues.Contains(iss)){
                    cleanIssues.Add(iss);
                }
            }
            using (var streamWriter = new StreamWriter(filename, true))
            {
                foreach (Issue iss in cleanIssues)
                {
                    Console.WriteLine("this is going to suck: " + currentText);
                    string stringOfIssue = iss.ToString();
                    //TODO: hey Trevor!!!
                    if (!currentText.Contains(stringOfIssue))
                    {
                        streamWriter.WriteLine(iss);
                        Console.WriteLine("There is a new Issue that is: " + iss.ToString());
                    }
                }
                streamWriter.Close();
            }
        }

        /*
         * returns (in string form) the content of a file at the given path
         */
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
         * Arching function that takes the path of where the text file is, 
         * converts it back into the list of issues using IssueListFromString
         * and returns that list
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
                if(!thing.Equals("")){
                    object[] elements = thing.Split(new[] { ',' });
                    Issue CreatedIssue = new Issue(elements[0].ToString(), 
                                                   elements[1].ToString(), 
                                                   elements[2].ToString(), 
                                                   ToBool(elements[3].ToString()),
                                                   elements[4].ToString(),
                                                   elements[5].ToString(),
                                                   ToBool(elements[6].ToString()));
                    newList.Add(CreatedIssue);
                }
            }
            return newList;
        }

        /*
         * Converts strings to booleans
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
         * Wrapper for saving a list of issues to a text file located in 
         * the resources folder of the Device        
         */
        public void InitializeTextFile(List<Issue> issues)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "IssueListStorage.txt");
            File.Create(filename).Dispose();
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

    
