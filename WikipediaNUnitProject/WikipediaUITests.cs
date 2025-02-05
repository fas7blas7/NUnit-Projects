using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WikipediaNUnitProject
{
    public class WikipediaUITests
    {
        private ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Url = "https://wikipedia.com/";
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown() 
        {           
            driver.Dispose();
            driver.Quit();
        }
            
        [Test]
        public void CheckPageTitle()
        {           
            Assert.That(driver.Title, Is.EqualTo("Wikipedia"));
        }

        [Test]
        public void CheckQualityAssurancePageTitle()
        {
            var searchInput = driver.FindElement(By.Id("searchInput"));
            Console.WriteLine("Element attribute is size - " + searchInput.GetDomProperty("size"));
            searchInput.SendKeys("Quality Assurance" + Keys.Enter);
            Assert.That(driver.Title, Is.EqualTo("Quality assurance - Wikipedia"));

            var currentPageTitle = driver.Title;
            Console.WriteLine("Current page title is " + currentPageTitle);
        }

        [Test]
        public void GetAllCookies()
        {
            // Get all cookies
            var cookies = driver.Manage().Cookies.AllCookies;

            // Output each cookie's details
            foreach (var cookie in cookies)
            {
                Console.WriteLine($"Name: {cookie.Name}, Value: {cookie.Value}, Domain: {cookie.Domain}, Path: {cookie.Path}, Expiry: {cookie.Expiry}");
            }
        }
    }
}