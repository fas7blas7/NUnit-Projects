using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitProjectExercise
{
    public class WorkingWithWindows
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://the-internet.herokuapp.com/windows";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void HandleMultipleWindows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> tabWindowHandles = driver.WindowHandles;
            Assert.That(tabWindowHandles.Count, Is.EqualTo(2), "There should be two windows open.");

            Thread.Sleep(2000);
            driver.SwitchTo().Window(tabWindowHandles[1]);

            string newWindowContent = driver.PageSource;
            Assert.IsTrue(newWindowContent.Contains("New Window"), "The content of the window is not as expected");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "tabs.txt");
            if (Directory.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "Window (tab) handle for new windwow (tab): " + driver.CurrentWindowHandle + "\n\n");

            driver.Close();

            driver.SwitchTo().Window(tabWindowHandles[0]);

            string originalWindowContent = driver.PageSource;
            Assert.IsTrue(originalWindowContent.Contains("Opening a new window"), "The content expected was not found");

            //log the content of the original window

            File.AppendAllText(path, "Window (tab) handle for original window(TAB): " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");
        }

        [Test, Order(2)]
        public void HandleNoSuchWindowException()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> tabWindowHandles = driver.WindowHandles;

            Thread.Sleep(2000);
            driver.SwitchTo().Window(tabWindowHandles[1]);

            driver.Close();

            try
            {
                driver.SwitchTo().Window(tabWindowHandles[1]);
            }
            catch (NoSuchWindowException exception)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
                File.AppendAllText(path, "NoSuchWindowException caught: " + exception.Message + "\n\n");
                Assert.Pass("NoSuchWindowException was correctly handled.");
            }
            catch (Exception exception)
            {
                Assert.Fail("An unexpected exception was thrown: " + exception.Message);
            }
            finally
            {
                driver.SwitchTo().Window(tabWindowHandles[0]);
            }
        }
    }
}