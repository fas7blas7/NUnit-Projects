using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTables
{
    public class WebTablesTests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void WorkingWithTableElements()
        {
            driver.Url = "https://practice.bpbonline.com/";
            IWebElement productsTable = driver.FindElement(By.XPath("//*[@id='bodyContent']/div/div[2]/table"));
            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));
            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";
            if (File.Exists(path)) {
                File.Delete(path);
            }

            foreach (IWebElement row in tableRows)
            {

                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath("//td"));
                foreach (var tData in tableData)
                {
                    //extract product name and cost
                    string data = tData.Text;
                    string[] productInfo = data.Split('\n');
                    //Write product information extracted to file
                    File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");
                 }
            }

            Assert.That(File.Exists(path), "CSV File Not Created.");
            Assert.That(new FileInfo(path).Length > 0);
        }
    }
}