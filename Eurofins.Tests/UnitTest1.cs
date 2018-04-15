using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eurofins.Genomics.MIDI.PageObjects.Core;
using Eurofins.ECOM.Selenium.Extension.Other;
using Eurofins.PageObjects.WorkFlow;
using Eurofins.PageObjects.Pages;

namespace Eurofins.Tests
{
    [TestClass]
    public class Sanity_Test:TestDriver
    {
        private static string pathStringfolder = System.IO.Path.Combine(System.IO.Path.Combine("C:\\MIDILog\\",DateTime.Today.ToShortDateString().Replace('/','_')),"SanityTests"+DateTime.Now.ToFileTimeUtc());

        [ClassInitialize]
        public static void MyClassInitializer(TestContext context)
        {
            System.IO.Directory.CreateDirectory(pathStringfolder);
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                MIDILogger.Info("Execution Started");

                var start = GetStart<StartFlow>(StartPage);

                var cartPage = start.GoToPage<HomePageFlow, HomePage>()
                    .EnterText("Python")
                    .ClickOnSerachButton()
                    ;

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
