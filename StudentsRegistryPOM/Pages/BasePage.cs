using OpenQA.Selenium;

namespace StudentsRegistryPOM.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        public virtual string PageUrl { get; }

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public IWebElement HomePageLink => driver.FindElement(By.XPath("//a[@href='/']"));

        public IWebElement ViewStudentsLink => driver.FindElement(By.XPath("//a[@href='/students']"));

        public IWebElement AddStudentLink => driver.FindElement(By.XPath("//a[@href='/add-student']"));

        public IWebElement PageHeading => driver.FindElement(By.XPath("//body//h1"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(this.PageUrl);
        }

        public bool IsOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeading()
        {
            return PageHeading.Text;
        }

    }
}
