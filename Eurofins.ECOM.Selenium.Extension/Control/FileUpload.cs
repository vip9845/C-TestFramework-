using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input")]
    public class FileUpload : ContainerControl
    {
        public FileUpload() { }

        public FileUpload(By by)
            : base(by)
        {
            
        }

        public string FilePath
        {
            get
            {
                return FilePath;
            }
            set
            {
                WrappedElement.SendKeys(value);
            }
        }
    }
}
