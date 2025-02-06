using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Exercise.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By userNameField = By.Id("user-name");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.XPath("//div[@class='error-message-container error']//h3");
    }

    public LoginPage(IWebDriver driver) : base(driver)
    {

    }

    public void FillUserName(string username)
    {
        Type(userNameField, username);
    }
}
