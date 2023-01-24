using AplicationLayer.Pages;
using AventStack.ExtentReports;
using CommonLibs.Implementation;
using CommonLibs.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Tests
{
    public class BaseTests
    {
        public CommonDriver commonDriver;
        public LoginPage loginPage;

        private IConfigurationRoot config;

        public ExtentReportUtils extentReportUtils;

        SreenUtils sreenUtils;

        string baseUrl;

        string currentProjectDirectory;
        string currentSolutionDirectory;

        string reportFileName;

        [OneTimeSetUp]
        public void preSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            currentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;

            config = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "//Config//appSettings.json").Build();

            reportFileName = currentSolutionDirectory + $"/reports/Test{DateTime.Now}.html";
            extentReportUtils = new ExtentReportUtils(reportFileName);

        }

        [SetUp]
        public void SetUp()
        {
            extentReportUtils.CreateTestCase("SetUp");
            string browserType = config["browserType"];
            commonDriver = new CommonDriver(browserType);

            extentReportUtils.AddTestLog(Status.Info, "browser type" + browserType);
            baseUrl = config["baseUrl"];
            extentReportUtils.AddTestLog(Status.Info, "base url" + baseUrl);
            commonDriver.NavigateToFirstURL(baseUrl);

            loginPage = new LoginPage(commonDriver.Driver);
            sreenUtils = new SreenUtils(commonDriver.Driver);
        }

        [TearDown]
        public void TearDown()
        {
            string screenFileName = $"{currentSolutionDirectory}/screen/test{DateTime.Now}.jpeg";

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                extentReportUtils.AddTestLog(Status.Fail, "Test Failed.");
                sreenUtils.GetScreen(screenFileName);

                extentReportUtils.AddScreen(screenFileName);
            }

            commonDriver.CloseAllBrowsers();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extentReportUtils.FlushReport();
        }
    }
}
