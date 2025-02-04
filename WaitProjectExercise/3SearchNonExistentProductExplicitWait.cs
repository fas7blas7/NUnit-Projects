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
    public class SearchNonExistentProductExplicitWait
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
        public void SearchForNonExistentProductWithExplicitWait()
        {
            driver.FindElements(By.Name("keywords"))[0].SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                buyNowLink.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Expected WEBDRIVERTIMEOUTEXCEPTION was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }                
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }

        [Test]
        public void SearchForNonExistentProductWithExplicitWaitAnotherAssert()
        {
            driver.FindElements(By.Name("keywords"))[0].SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                           
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                
            IWebElement noSuchItemMessage = wait.Until(e => e.FindElement(By.XPath("//div[@class='contentText']//p")));
                
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                
            string noSuchItemMessageField = driver.FindElement(By.XPath("//div[@class='contentText']//p")).Text;
                
            Assert.IsTrue(driver.PageSource.Contains("junk"));
            Assert.IsTrue(noSuchItemMessageField == "There is no product that matches the search criteria.");
               
            Console.WriteLine($"This is a non-existent product - {noSuchItemMessageField}");
        }

    }
}
