using NUnit.Framework;
using CommonLibs.Implementation;
using AventStack.ExtentReports;

namespace TestFramework.Tests
{
    public class LoginPageTests : BaseTests
    {
        [Test]
        public void VerifyLoginTest() 
        {
            ExtentReportUtils.CreateTestCase("Test: Verify Login Test");

            ExtentReportUtils.AddTestLog(Status.Info, "Sing In");
            LoginPage.Login("id750643415", "111222000");

            string expectedTittle = "Смотреть Аниме онлайн бесплатно в хорошем качестве";
            string actualTittle = CommonDriver.GetPageTittle();

            Assert.AreEqual(expectedTittle, actualTittle);
        }
    }
}