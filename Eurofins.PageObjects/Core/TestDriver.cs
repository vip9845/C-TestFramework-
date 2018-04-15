using System.Collections.Generic;
using Eurofins.Selenium.Extension.Other;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eurofins.Genomics.MIDI.PageObjects.Core
{
    public class TestDriver : DriverTestFixtureBase
    {
        private string _userName;
        private string _userPassword;

        protected TestContext context;

        public TestContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        public string UserName
        {
            get { return this._userName; }
            set { this._userName = value; }
        }

        public string UserPassword
        {
            get { return this._userPassword; }
            set { this._userPassword = value; }
        }

        public string OperatorCode
        {
            get { return this._userName; }
        }

        public string StartPage
        {
            get
            {
                return "";
            }
        }

        //public string DBConnectionString
        //{
        //    get
        //    {
        //        return ConnectionString.TrainingOfferCNDB;
        //    }
        //}

        //public void AcceptChanges()
        //{
        //    DataPackage.Save();
        //}

        //public TT GetStartGolims<TT, T>()
        //   where TT : WindowFlowBase<T>
        //   where T : WindowBase
        //{

        //    var window = (T)Activator.CreateInstance(typeof(T));
            
        //    return (TT)Activator.CreateInstance(typeof(TT), window);
        //}

        //public void StartApplication()
        //{
        //    Process.Start(ConfigurationManager.AppSettings["AppPath"]);
        //    UIAWait.WaitFor(6);//Thread.Sleep(6000);
        //}

        public void ProcessKill()
        {
            foreach (Process p in System.Diagnostics.Process.GetProcessesByName(ConfigurationManager.AppSettings["Process"]))
            {
                try
                {
                    p.Kill();
                }catch (Win32Exception winException)
                {
                    //// process was terminating or can't be terminated - deal with it             
                    Assert.Fail(winException.Message);
                }
                catch (InvalidOperationException invalidException)
                {
                    //// process has already exited - might be able to let this one go              
                    Assert.Fail(invalidException.Message);
                }
            }

            foreach (Process p in System.Diagnostics.Process.GetProcessesByName("cmd.exe"))
            {
                try
                {
                    p.Kill();
                }catch (Win32Exception winException)
                {
                    //// process was terminating or can't be terminated - deal with it             
                    Assert.Fail(winException.Message);
                }
                catch (InvalidOperationException invalidException)
                {
                    //// process has already exited - might be able to let this one go              
                    Assert.Fail(invalidException.Message);
                }
            }
            //GetStartGolims<CommandAction, CommandObjects>().ClickOnCloseButton();

            foreach (Process p in System.Diagnostics.Process.GetProcessesByName("EclipseSimulator.exe"))
            {
                try
                {
                    p.Kill();
                }catch (Win32Exception winException)
                {
                    //// process was terminating or can't be terminated - deal with it             
                    Assert.Fail(winException.Message);
                }
                catch (InvalidOperationException invalidException)
                {
                    //// process has already exited - might be able to let this one go              
                    Assert.Fail(invalidException.Message);
                }
            }
        }

    }
}
