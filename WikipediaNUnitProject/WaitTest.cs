using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WikipediaNUnitProject
{
    public class WaitTest
    {

        private ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver
            {
                Url = "http://www.uitestingplayground.com/ajax"
            };
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
            }
        [TearDown]
        public void Teardown()
        {
            driver.Dispose();
            driver.Quit();
        }

        [Test]
        public void WaitExample()
        {
            
            driver.FindElement(By.Id("ajaxButton")).Click();
            IWebElement fieldText = driver.FindElement(By.XPath("/html/body/section/div/div/p"));
            Assert.That(fieldText.Text, Is.EqualTo("Data loaded with AJAX get request."));
            Console.WriteLine(fieldText);
        }
    }
}
