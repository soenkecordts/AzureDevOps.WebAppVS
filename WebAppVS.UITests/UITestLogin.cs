using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace WebAppVS.UITests
{
    [TestClass]
    public class UITestLogin
    {
        static TestContext _context;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _context = context;
        }

        RemoteWebDriver _driver;
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(500);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void TitleShouldBeLebensmittelretter()
        {
            Console.WriteLine(_context.Properties["webAppUrl"]);

            var webAppUrl = _context.Properties["webAppUrl"].ToString();

            _driver.Navigate().GoToUrl(webAppUrl);

            Assert.AreEqual("Lebensmittelretter", _driver.Title, "Expected title of homepage: Lebensmittelretter");
        }

        [TestMethod]
        public void PasswordAndEmailShouldExistsAsType()
        {
            var webAppUrl = _context.Properties["webAppUrl"].ToString() + "/Login";

            _driver.Navigate().GoToUrl(webAppUrl);

            var elemEmail = _driver.FindElementById("Email");
            var elemPassword = _driver.FindElementById("Password");
            var typeEmail = elemEmail.GetAttribute("type");
            var typePassword = elemPassword.GetAttribute("type");

            Assert.AreEqual("email", typeEmail, "Expected type of email input element: email");
            Assert.AreEqual("password", typePassword, "Expected type of password input element: password");
        }

    }
}
