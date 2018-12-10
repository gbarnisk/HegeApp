/*
 * An issue data type. Contains a name, a pdf uri, and a cover art uri.
 */

using System;
using System.Collections.Generic;

namespace HegeApp.Models
{
    public class Issue : IComparable
    {
        public string IssueName { get; set; } //The display name of the issue, i.e. "The Hege Gets a Job"
        public string GenericFileName { get; set; } //The backend name for the issue. This will always have the structure vx_ix
        public string CoverURL { get; set; } //The url for the hosted cover
        public string PdfURL { get; set; } //The url for the hosted pdf
        public string PdfURI { get; set; } //The uri for the pdf. This will always have the strucute Issuevx_ix.pdf
        public string PdfPath { get; set; } //The full local path to the issue file. Leave empty if the file does not yet exist
        public bool PdfLocal { get; set; } //Whether there is a local copy of the pdf

        public Issue(string IssueName, string GenericFileName, string CoverURL, string PdfURL, string PdfURI, string PdfPath, bool PdfLocal)
        {
            this.IssueName = IssueName;
            this.GenericFileName = GenericFileName;
            this.CoverURL = CoverURL;
            this.PdfURL = PdfURL;
            this.PdfURI = PdfURI;
            this.PdfPath = PdfPath;
            this.PdfLocal = PdfLocal;
        }

        /*
         * returns a string of a boolean, for use in ToString
         */
        public string BooleanStringer(bool b)
        {
            return b.Equals(true) ? "True" : "False";
        }

        /*
         * String of an issue
         */
        public override string ToString(){
            string SIssue = BooleanStringer(PdfLocal);

            string final =  IssueName + "," + GenericFileName + "," + CoverURL + "," + 
                PdfURL + "," + PdfURI + "," + PdfPath + "," + SIssue;
            return final;
        }

        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            Issue other = obj as Issue;
            int otherVolumeNum = other.getVolumeNum();
            int otherIssueNum = other.getIssueNum();
            int volumeNum = getVolumeNum();
            int issueNum = getIssueNum();

            if (other.Equals(this))
            {
                Console.WriteLine("It's equal");
                return 0;
            }

            if (volumeNum > otherVolumeNum)
            {
                Console.WriteLine("It's more than other");
                return -1;
            } 
            else if (volumeNum == otherVolumeNum)
            {
                if (issueNum > otherIssueNum)
                {
                    Console.WriteLine("It's more than other");
                    return -1;
                }
            }

            Console.WriteLine("It's less than other");
            return 1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Issue objAsIssue)) return false;
            return Equals((Issue)obj);
        }

        /* 
         * Determines if two issues are equal, based on their volume
         */

        public bool Equals (Issue Iss)
        {
            int IssIssNum = Iss.getIssueNum();
            int IssVolNum = Iss.getVolumeNum();
            //Console.WriteLine("push notification" + IssIssNum);
            //Console.WriteLine("things should work" + IssVolNum);
            return getIssueNum() == IssIssNum && getVolumeNum() == IssVolNum;
        }

        public int getVolumeNum()
        {
            Console.WriteLine("Griffin sucks" + GenericFileName);
            //return (numbers[0]);
            foreach (string thing in GenericFileName.Split(new[] { 'v', '_' })){
                Console.WriteLine("woo!"+ thing);
            }
            string volumeString = GenericFileName.Split(new[] { 'v', '_' })[1];
            //int test = Convert.ToInt32(volumeString);
            //Console.WriteLine(test);
            Console.WriteLine("getVolume was called and is about to return :" + volumeString);

            return Convert.ToInt32(volumeString);
        }

        public int getIssueNum()
        {
            string issueString = GenericFileName.Split(new[] { 'i'})[2];
            Console.WriteLine("getIssue was called and is about to return :" + issueString);
            return Convert.ToInt32(issueString);
        }
    }

}
