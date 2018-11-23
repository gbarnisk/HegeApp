/*
 * An issue data type. Contains a name, a pdf uri, and a cover art uri.
 */

namespace HegeApp.Models
{
    class Issue
    {
        public string IssueName { get; set; }
        public string CoverURL { get; set; }
        public string CoverURI { get; set; }
        public bool CoverLocal { get; set; } //Whether there is a local copy of the cover
        public string PdfURL { get; set; }
        public string PdfURI { get; set; }
        public bool PdfLocal { get; set; } // Whether there is a local copy of the pdf

        public Issue(string IssueName, string CoverURL, string CoverURI, bool CoverLocal, string PdfURL, string PdfURI, bool PdfLocal)
        {
            this.IssueName = IssueName;
            this.CoverURL = CoverURL;
            this.CoverURI = CoverURI;
            this.CoverLocal = CoverLocal;
            this.PdfURL = PdfURL;
            this.PdfURI = PdfURI;
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
            string SCover = BooleanStringer(CoverLocal);
            string SIssue = BooleanStringer(PdfLocal);

            string final =  IssueName + "," + CoverURL + "," + CoverURI + ","  + SCover + "," + 
                PdfURL + "," + PdfURI + "," + SIssue;
            return final;
        }

    }
}
