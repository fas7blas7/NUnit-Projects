using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryPOM.Pages
{
    public class AddStudentPage :BasePage
    {
        public AddStudentPage(IWebDriver driver) :base(driver)
        {

        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/add-student";

        public IWebElement ErrorMsgElement => driver.FindElement(By.XPath("//div[text()='Cannot add student. Name and email fields are required!']"));

        public IWebElement StudentNameField => driver.FindElement(By.XPath("//input[@id='name']"));

        public IWebElement StudentEmailField => driver.FindElement(By.XPath("//input[@id='email']"));

        public IWebElement AddButton => driver.FindElement(By.XPath("//form//button[@type='submit']"));

        public string GetErrorMessage()
        {
            return ErrorMsgElement.Text;
        }

        public void AddStudentFunction(string name, string email)
        {
            this.StudentNameField.SendKeys(name);
            this.StudentEmailField.SendKeys(email);
            AddButton.Click();
        }

    }
}
