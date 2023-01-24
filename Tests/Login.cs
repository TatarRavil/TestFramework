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
            extentReportUtils.CreateTestCase("Test: Verify Login Test");

            extentReportUtils.AddTestLog(Status.Info, "Sing In");
            loginPage.Login("id750643415", "111222000");

            string expectedTittle = "Смотреть Аниме онлайн бесплатно в хорошем качестве";
            string actualTittle = commonDriver.GetPageTittle();

            Assert.AreEqual(expectedTittle, actualTittle);
        }
    }
}