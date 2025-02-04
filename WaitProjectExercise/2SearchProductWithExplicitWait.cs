using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace WaitProjectExercise
{
    public class ExplicitWaitTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://practice.bpbonline.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchProductWithExplicitWait()
        {
            driver.FindElements(By.Name("keywords"))[0].SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                buyNowLink.Click();

                string productName = driver.FindElement(By.XPath("//*[@id=\"bodyContent\"]/form/div/div[1]/table/tbody/tr/td[1]/table/tbody/tr/td[2]/a[1]/strong")).Text;

                Assert.IsTrue(productName == "Microsoft Internet Keyboard PS/2");
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page.");
                Console.WriteLine("***SCENARIO COMPLETE***)");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }

    }
}
