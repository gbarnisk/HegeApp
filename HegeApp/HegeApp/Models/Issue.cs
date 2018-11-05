/*
 * An issue data type. Contains a name, a pdf uri, and a cover art uri.
 */

namespace HegeApp.Models
{
    class Issue
    {
        public string issueName { get; set; }
        public string coverURI { get; set; }
        public string pdfURI { get; set; }
		public string pdfURL { get; set; }

        public Issue(string issueName, string coverURI, string pdfURI)
        {
            this.issueName = issueName;
            this.coverURI = coverURI;
            this.pdfURI = pdfURI;
        }

		public void downloadIssue (pdfURL filename) {
            //go to google drive and download a file, saving the uri to the object
			print filename;
    }
	}
}
