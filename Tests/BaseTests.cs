using AplicationLayer.Pages;
using AventStack.ExtentReports;
using CommonLibs.Implementation;
using CommonLibs.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;

namespace TestFramework.Tests
{
    public class BaseTests
    {
        public CommonDriver CommonDriver { get; set; }
        public LoginPage LoginPage { get; set; }
        public ExtentReportUtils ExtentReportUtils { get; set; }
        public SreenUtils SreenUtils { get; set; }
        
        private IConfigurationRoot config;
        private string baseUrl;
        private string currentSolutionDirectory;
        private string reportFileName;

        [OneTimeSetUp]
        public void PreSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            currentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;

            config = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "//Config//appSettings.json").Build();

            reportFileName = currentSolutionDirectory + $"/reports/Test{DateTime.Now}.html";
            ExtentReportUtils = new ExtentReportUtils(reportFileName);
        }

        [SetUp]
        public void SetUp()
        {
            ExtentReportUtils.CreateTestCase("SetUp");
            string browserType = config["browserType"];
            CommonDriver = new CommonDriver(browserType);

            ExtentReportUtils.AddTestLog(Status.Info, "browser type" + browserType);
            baseUrl = config["baseUrl"];
            ExtentReportUtils.AddTestLog(Status.Info, "base url" + baseUrl);
            CommonDriver.NavigateToFirstURL(baseUrl);

            LoginPage = new LoginPage(CommonDriver.Driver);
            SreenUtils = new SreenUtils(CommonDriver.Driver);
        }

        [TearDown]
        public void TearDown()
        {
            string screenFileName = $"{currentSolutionDirectory}/screen/test{DateTime.Now}.jpeg";

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ExtentReportUtils.AddTestLog(Status.Fail, "Test Failed.");
                SreenUtils.GetScreen(screenFileName);

                ExtentReportUtils.AddScreen(screenFileName);
            }

            CommonDriver.CloseAllBrowsers();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportUtils.FlushReport();
        }
    }
}
