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
        public string GenericFileName { get; set; } //The backend name for the issue. This will always have the structure vX_iX
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
         * String of an issue object
         */
        public override string ToString(){
            string SIssue = BooleanStringer(PdfLocal);

            string final =  IssueName + "," + GenericFileName + "," + CoverURL + "," + 
                PdfURL + "," + PdfURI + "," + PdfPath + "," + SIssue;
            return final;
        }

       


        /*
        * based on the volume and issue numbers, compares to Issue objects
        * Highest Value (most recent) Issues come first       
        */
        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            if (!(obj is Issue objAsIssue)) return 0;
            Issue other = obj as Issue;
            int otherVolumeNum = other.GetVolumeNum();
            int otherIssueNum = other.GetIssueNum();
            int volumeNum = GetVolumeNum();
            int issueNum = GetIssueNum();

            if (other.Equals(this))
            {
                return 0;
            }

            if (volumeNum > otherVolumeNum)
            {
                return -1;
            } 
            else if (volumeNum == otherVolumeNum)
            {
                if (issueNum > otherIssueNum)
                {
                    return -1;
                }
            }
            return 1;
        }

        /*
         * checks to see if the object being compared is an issue, passed to subsequent Equals method 
         */
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Issue objAsIssue)) return false;
            Issue thing = obj as Issue;
            return Equals(thing);
        }

        /* 
         * Determines if two issues are equal, based on their volume and issue number
         */
        public bool Equals (Issue Iss)
        {
            return GenericFileName.Equals(Iss.GenericFileName);
        }

        /*
         * Parses GenericFileName for the volume number, and returns it converted to Int
         */
        public int GetVolumeNum()
        {
            string volumeString = GenericFileName.Split(new[] { 'v', '_' })[1];
            return Convert.ToInt32(volumeString);
        }

        /*
        * Parses GenericFileName for the issue number, returns it converted to Int 
        */
        public int GetIssueNum()
        {
            string issueString = GenericFileName.Split(new[] { 'i' })[1];
            return Convert.ToInt32(issueString);
        }

    
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
