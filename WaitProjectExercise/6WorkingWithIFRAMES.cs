using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitProjectExercise
{
    public class WorkingWithIFRAMES
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void TestFrameByIndex()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@id='result']")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();
            var dropDownButton = (ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));
            

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
                Console.WriteLine("******TEST PASS******");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(2)]
        public void TestFrameById()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void TestFrameByElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));
            driver.SwitchTo().Frame(frameElement);
                       
            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }
    }
}
