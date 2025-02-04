using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace SeleniumWaits
{
    public class WebDriverWaitsDemoTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }
        [TearDown]
        public void TearDown() 
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void RedBoxInteraction()
        {
            driver.Url = "https://www.selenium.dev/selenium/web/dynamic.html";
            IWebElement addButton = driver.FindElement(By.XPath("//input[@id='adder']"));
            addButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var redBox = driver.FindElement(By.XPath("//div[@id='box0']"));
            Assert.True(redBox.Displayed);
        }

        [Test]
        public void InputFieldInteraction()
        {
            driver.Url = "https://www.selenium.dev/selenium/web/dynamic.html";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();
            IWebElement revealedBox = driver.FindElement(By.XPath("//input[@id='revealed']"));
            Assert.That(revealedBox.TagName, Is.EqualTo("input"));
         }

        [Test]
        public void ExplicitWaitElementCreatedButNotVisible()
        {
            driver.Url = "https://the-internet.herokuapp.com/dynamic_loading/1";
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/button")).Click();
            WebDriverWait waitTheStuff = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement finishDiv = waitTheStuff.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));
            Assert.True(finishDiv.Displayed);
        }

        [Test]
        public void ImplicitWaitElementCreatedButNotVisible()
        {
            driver.Url = "https://the-internet.herokuapp.com/dynamic_loading/2";
            driver.FindElement(By.XPath("//div[@id='start']//button")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var hfour = driver.FindElement(By.XPath("//div[@id=\"finish\"]//h4"));
            Assert.That(hfour.Displayed);
        }

        [Test]

        public void PageLoadTimeout()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
            var startButton = driver.FindElement(By.XPath("//div[@id='start']//button"));
            Assert.True(startButton.Displayed);

        }

        [Test]
        public void JavaScriptTimeoutTest()
        {
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);
            string scriptDateDelayed = @"const start = new Date().getTime();
                            const delay = 45000;
                            while (new Date().getTime() < start + delay)
                            {
                                // do something while waiting 45000ms   
                            }
                            console.log('45000 ms execution')";
            IJavaScriptExecutor jsExecutorName = (IJavaScriptExecutor)driver;
            jsExecutorName.ExecuteScript(scriptDateDelayed);
        }

        [Test]

        public void FluentWait_ElementCreatedButNotVisibleTests()
        {
            driver.Url = "https://the-internet.herokuapp.com/dynamic_loading/1";
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/button")).Click();

            DefaultWait<IWebDriver> fluentWaitName = new DefaultWait<IWebDriver>(driver);
            fluentWaitName.Timeout = TimeSpan.FromSeconds(10);
            fluentWaitName.PollingInterval = TimeSpan.FromMilliseconds(55);

            IWebElement finishDiv = fluentWaitName.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id=\"finish\"]")));
            Assert.True(finishDiv.Displayed);
        }

        [Test]

        public void IgnoreException_with_Fluent_Wait()
        {
            driver.Url = "https://the-internet.herokuapp.com/dynamic_loading/2";
            driver.FindElement(By.XPath("//div[@class='row']//div[@id='content']//div[@class='example']//div//button")).Click();

            DefaultWait<IWebDriver> fluentWaitName = new DefaultWait<IWebDriver> (driver);

            fluentWaitName.Timeout = TimeSpan.FromSeconds(10);
            fluentWaitName.PollingInterval= TimeSpan.FromMilliseconds(55);
            fluentWaitName.IgnoreExceptionTypes(typeof(NoSuchElementException));

            IWebElement finishDiv = fluentWaitName.Until(ExpectedConditions.ElementExists(By.XPath("//div[@id=\"finish\"]//h4")));

            Assert.True(finishDiv.Displayed);

        }

    }
}