using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace StudentsRegistryPOM.Pages
{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver) 
        {
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/students";

        public ReadOnlyCollection<IWebElement> StudentListItems => driver.FindElements(By.XPath("//body//ul//li"));

        public string[] GetRegisteredStudents()
        {
            var elementStudents = this.StudentListItems.Select(s => s.Text).ToArray();
            return elementStudents;
        }

    }
}
