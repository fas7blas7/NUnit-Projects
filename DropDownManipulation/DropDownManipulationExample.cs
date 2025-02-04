using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace DropDownManipulation
{
    public class DropDownManipulationExample
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable - search - engine - choice - screen");
            driver = new ChromeDriver(options);
            driver.Url = "https://practice.bpbonline.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void ExtractInformationBasedOnDropdownOptions()
        {
            string path = Directory.GetCurrentDirectory() + "/Manufacturers.txt";
            SelectElement dropwdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select[@name='manufacturers_id']")));
            IList<IWebElement> options = dropwdown.Options;
            List<string> optionsAsString = new List<string>();
            foreach (var manuname in options)
            {
                optionsAsString.Add(manuname.Text);
            }

            optionsAsString.RemoveAt(0);

            foreach (var manuname in optionsAsString)
            {
                dropwdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select[@name='manufacturers_id']")));
                dropwdown.SelectByText(manuname);
                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {manuname} has no products.");
                }
                else
                {
                    IWebElement productsTable = driver.FindElement(By.ClassName("productListingData"));
                    File.AppendAllText(path, $"\n\n The Manufacturer {manuname} products are listed below.");

                    ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));
                    foreach (var row in tableRows) 
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }

            }
        }
    }
}