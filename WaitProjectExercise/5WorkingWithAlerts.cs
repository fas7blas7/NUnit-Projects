using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WaitProjectExercise
{
    public class WorkingWithAlertsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(5));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void WorkingWithAlertsJSAlert()
        {
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsAlert()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text not as expected.");

            Thread.Sleep(2000);
            alert.Accept();

            var successfullClickMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(successfullClickMessage.Text, Is.EqualTo("You successfully clicked an alert"));
            Console.WriteLine(successfullClickMessage.Text + " *** TEST PASS ***", "Text result not as expected");
        }

        [Test, Order(2)]
        public void WorkingWithAlertsJSConfirmAndCancel()
        {
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsConfirm()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text not as expected.");

            Thread.Sleep(2000);
            alert.Accept();

            IWebElement resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You clicked: Ok"));
            Console.WriteLine(resultMessage.Text + " *** SCENARIO PASS ***", "Text result not as expected");

            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsConfirm()']")).Click();

            alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text not as expected.");

            Thread.Sleep(2000);
            alert.Dismiss();

            resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You clicked: Cancel"));
            Console.WriteLine(resultMessage.Text + " *** TEST PASS ***", "Text result not as expected");
        }

        [Test, Order(3)] 
        public void WorkingWithAlertsJSPrompt()
        {
            //Enter 123 in prompt
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsPrompt()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text not as expected.");

            Thread.Sleep(2000);
            alert.SendKeys("123");
            Thread.Sleep(2000);
            alert.Accept();

            IWebElement resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You entered: 123"));
            Console.WriteLine(resultMessage.Text + " *** SCENARIO PASS ***", "Text result not as expected");

            //Enter "Hello World!"
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsPrompt()']")).Click();

            alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text not as expected.");

            Thread.Sleep(2000);
            alert.SendKeys("Hello World!");
            Thread.Sleep(2000);
            alert.Accept();

            resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You entered: Hello World!"));
            Console.WriteLine(resultMessage.Text + " *** SCENARIO PASS ***", "Text result not as expected");
            
            //Cancel the prompt
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsPrompt()']")).Click();

            alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text not as expected.");

            
            Thread.Sleep(2000);
            alert.Dismiss();

            resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You entered: null"));
            Console.WriteLine(resultMessage.Text + " *** SCENARIO PASS ***", "Text result not as expected");

            //Dont enter anything in prompt, but click OK
            driver.FindElement(By.XPath("//div[@id='content']//button[@onclick='jsPrompt()']")).Click();

            alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text not as expected.");
                        
            Thread.Sleep(2000);
            alert.Accept();

            resultMessage = driver.FindElement(By.XPath("//p[@id='result']"));
            Assert.That(resultMessage.Text, Is.EqualTo("You entered:"));
            Console.WriteLine(resultMessage.Text + " *** SCENARIO PASS ***", "Text result not as expected");
        }
    }
}
