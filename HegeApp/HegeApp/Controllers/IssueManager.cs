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

/*
 * Controls the issue objects. Handles indexing and loading issues on app startup, and allows access to the list of issues.
 */

namespace HegeApp.Controllers
{
    public class IssueManager
    {
        public List<Issue> issueList { get; set; } //List of issues to be displayed
        private IDownloadManager downloadManager;
        public List<Issue> finalList { get; set; }
        public string filename { get; set; } //File name for the saved metadata

        public IssueManager()
        {
            downloadManager = CrossDownloadManager.Current;
            filename = FindPath();

            if (!File.Exists(filename))
            {
                InitializeTextFile(filename);
            }

            //Get the right issue list
            issueList = InitializeToRAM();
        }

        /*
         * Returns the correctly constructed list, based on the host and the locally saved data.
         */
        public List<Issue> InitializeToRAM()
        {
            List<Issue> finalList = new List<Issue>();
            List<Issue> indexedList = HostToArray();
            List<Issue> localList = ReadFromLocal(filename);

            //First, copy every issue from localList into finalList
            foreach (Issue issue in localList)
            {
                finalList.Add(issue);
                Console.WriteLine("I just added this issue to the final list from local: " + issue);
            }

            //Then, delete all of the issues which have since been deleted, and are not in indexedList
            List<Issue> toDelete = new List<Issue>();
            foreach (Issue issue in NewIssues(localList, indexedList))
            {
                toDelete.Add(issue);
                Console.WriteLine("I just deleted this issue to the final list for being missing in the index: " + issue);
            }

            foreach (Issue issue in toDelete)
            {
                finalList.Remove(issue);
            }

            //Finally, add in all of the new issues in indexedList but not in localList
            foreach (Issue issue in NewIssues(indexedList, localList))
            {
                finalList.Add(issue);
                Console.WriteLine("I just added this issue to the final list from index: " + issue);
            }

            finalList.Sort(); //Make sure to sort the list before returning so it shows in chronological order

            return finalList;
        }

        /*
         * Indexes all issues from the filehost, a post on wordpress, and returns them as an array.
         */
        public List<Issue> HostToArray()
        {

            List<Issue> issues = new List<Issue>();

            WebClient wc = new WebClient();
            using (Stream st = wc.OpenRead("https://macalesterhegemonocle.wordpress.com/2018/11/13/test/"))
            {
                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                {
                    string html = sr.ReadToEnd();

                    Regex hrefRegex = new Regex(@"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)</a>"); //This matches all of the pdf links
                    Regex srcRegex = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>"); //This matches all of the image links

                    foreach (Match match in hrefRegex.Matches(html))
                    {
                        string pdfurl = match.Groups["href"].Value;
                        if (pdfurl.Contains("pdf"))
                        {
                            string issueName = match.Groups["value"].Value;
                            string[] elements = pdfurl.Split(new[] { '/' });

                            foreach (String thingy in elements)
                            {
                                Console.WriteLine("Element: " + thingy); 
                            }

                            string pdfFileName = elements[elements.Length - 1];
                            Console.WriteLine("look here");
                            Console.WriteLine(pdfFileName);
                            string genericName = pdfFileName.Split(new[] { '.' })[0].Substring(5);

                            bool exists = false;
                            foreach (Issue issue in issues)
                            {
                                if (issue.GenericFileName.Equals(genericName)) //This condition checks if there is already an issue object with the same filename but for the cover, rather than the pdf
                                {
                                    issue.IssueName = issueName;
                                    issue.PdfURL = pdfurl;
                                    issue.PdfURI = pdfFileName;
                                    exists = true;
                                }
                            }

                            if (!exists)
                            {
                                issues.Add(new Issue(issueName, genericName, "", pdfurl, pdfFileName, "", false));
                            }
                        }
                    }

                    Console.WriteLine("I am groot");
                    foreach (Match match in srcRegex.Matches(html))
                    {
                        string pngurl = match.Groups[1].Value;
                        if (pngurl.Contains("cover"))
                        {
                            Console.WriteLine(pngurl);
                            string[] elements = pngurl.Split(new[] { '/', '?' });
                            string pngFileName = elements[elements.Length - 2]; //No longer needed but kept for the next line's readability
                            string genericName = pngFileName.Split(new[] { '.' })[0].Substring(5);
                            Console.WriteLine("This is the png file name and generic name " + pngFileName + " " + genericName);

                            bool exists = false;
                            foreach (Issue issue in issues)
                            {
                                if (issue.GenericFileName.Equals(genericName)) //This condition checks if there is already an issue object with the same filename but for the cover, rather than the pdf
                                {
                                    issue.CoverURL = pngurl;
                                    exists = true;
                                }
                            }

                            if (!exists)
                            {
                                issues.Add(new Issue("", genericName, pngurl, "", "", "", false));
                            }
                        }
                    }
                }
            }

            //A quick sanity check to make sure we are returning good issues
            List<Issue> throwAway = new List<Issue>();
            foreach (Issue issue in issues)
            {
                if (issue.CoverURL.Equals("") | issue.PdfURL.Equals(""))
                {
                    throwAway.Add(issue);
                    Console.WriteLine("I am about to throw this issue away for a missing url: " + issue);
                }
                else
                {
                    Console.WriteLine("I kept this issue: " + issue);
                }
            }

            foreach (Issue issue in throwAway)
            {
                issues.Remove(issue);
            }

            return issues;
        }

        /*
        * Saves a list of issues into a textfile at a specified path. Saves in a csv format, with issues separated by lines.
        */
        public void SaveToLocal(List<Issue> issues, string filename)
        {
            InitializeTextFile(filename);
            string currentText = ReadForText(filename);
            using (var streamWriter = new StreamWriter(filename, true))
            {

                foreach (Issue iss in issues)
                {
                    string stringOfIssue = iss.ToString();
                    //TODO: hey Trever!!!
                    if (!currentText.Contains(stringOfIssue))
                    {
                        streamWriter.WriteLine(iss);
                    }
                }
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
         * Reads a text file at a specified path and returns the content in the form of a list of issues
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
            List<Issue> newList = new List<Issue>();
            string[] result = issuelis1.Split(new[] { '\r', '\n' });
            foreach (String thing in result)
            {
                if (!thing.Equals(""))
                {
                    object[] elements = thing.Split(new[] { ',' });
                    int hack = 0;
                    foreach (String part in elements)
                    {
                        hack++;
                    }
                    Issue CreatedIssue = new Issue(elements[0].ToString(),
                                                    elements[1].ToString(),
                                                    elements[2].ToString(),
                                                    elements[3].ToString(),
                                                    elements[4].ToString(),
                                                    elements[5].ToString(),
                                                    ToBool(elements[6].ToString()));
            
                    newList.Add(CreatedIssue);
                }

            }
            return newList;
        }

        /*
         * Converts a string back to a boolean
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
         * Creates an empty file at filename.
         */
        public void InitializeTextFile(string filename)
        {
            File.Create(filename).Dispose();
            Console.WriteLine("I just created a file with name " + filename);
        }

        /*
         * Locates and returns the correct file path for the local save state.
         */
        public string FindPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "IssueListStorage.txt");
            return filename;
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

        /*
         * Returns every issue in newList, paramater 1, which is not in oldList, parameter 2. It determines issue overlap by checking the generic names.
         */
        public List<Issue> NewIssues(List<Issue> newList, List<Issue> oldList)
        {
            List<Issue> finalList = new List<Issue>();

            foreach (Issue newIssue in newList)
            {
                bool found = false;
                foreach (Issue oldIssue in oldList)
                {
                    if (newIssue.GenericFileName.Equals(oldIssue.GenericFileName))
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    finalList.Add(newIssue);
                }
            }

            return finalList;
        }
    }
}
