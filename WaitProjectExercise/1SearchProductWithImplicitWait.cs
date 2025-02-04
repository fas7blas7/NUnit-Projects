using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WaitProjectExercise
{
        public class ImplicitWaitTests
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
        public void SearchProductWithImplicitWait()
        {
            driver.FindElements(By.Name("keywords"))[0].SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();
            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The item was not found in the cart.");
                Console.WriteLine("***Scenario complete****");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }

        [Test]
        public void SearchProductWithImplicitWaitFAILNEGATIVETEST()
        {
            driver.FindElements(By.Name("keywords"))[0].SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            IWebElement notFoundItem = driver.FindElement(By.XPath("//div[@class='contentText']//p"));
            string actualMessage = notFoundItem.Text;
            string expectedText = "There is no product that matches the search criteria.";
            string customMSG = "***PRODUCT NOT FOUND SUCCESSFULLY***";
            Assert.That(actualMessage, Is.EqualTo(expectedText));
            Console.WriteLine($"Actual -> {actualMessage}, Expected -> {expectedText}, {customMSG}");

            try
            {
                Assert.IsTrue(driver.PageSource.Contains("junk"), "The item was not found in the cart.");
                Console.WriteLine("***Scenario complete****");
            }
            catch (Exception ex)
            {
                Assert.Fail("No Such Element Exception: " + ex.Message);
            }

        }

    }
}