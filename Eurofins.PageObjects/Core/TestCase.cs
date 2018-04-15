using Eurofins.Selenium.Extension.Other;

namespace Eurofins.Genomics.MIDI.PageObjects.Core
{
    public class TestCase : DriverTestFixtureBase
    {
        public string StartPage
        {
            get { return ""; }
        }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string QuotationCode { get; set; }

        public bool ImpersonateUserCode
        {
            get { return false; }
        }

        //public void AcceptChanges()
        //{
        //    DataPackage.Save();
        //}
    }
}