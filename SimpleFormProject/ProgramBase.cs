using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SimpleFormProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create (new chrome) Browser
           var driver = new ChromeDriver();

            driver.Url = "https://wikipedia.com/";

            Console.WriteLine("Page title:" + driver.Title);

            var searchInput = driver.FindElement(By.Id("searchInput"));

            searchInput.Click();
            searchInput.SendKeys("Quality Assurance" + Keys.Enter);
              //another method for searchinput sendkeys
              //driver.FindElement(By.Id("searchInput")).SendKeys("Quality Assurance" + Keys.Enter);


            var currentPageTitle = driver.Title;
            Console.WriteLine("Current Page title is " + currentPageTitle);

            if (currentPageTitle == "Quality assurance - Wikipedia")
            {
                Console.WriteLine(" **TEST PASS** ");
            }
            else
            {
                Console.WriteLine("**TEST FAILED**");
            }
        }
    }
}
