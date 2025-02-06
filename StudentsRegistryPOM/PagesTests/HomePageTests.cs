using StudentsRegistryPOM.Pages;

namespace StudentsRegistryPOM.PagesTests
{
    public class HomePageTests : BaseTests
    {
        [Test]
        public void Test_Homepage_Content()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();

            Assert.Multiple(() =>
            {
                Assert.That(homePage.GetPageTitle(), Is.EqualTo("MVC Example"), "Incorrect Expected Title");
                Assert.That(homePage.GetPageHeading(), Is.EqualTo("Students Registry"), "Incorrect Expected Heading");
            });

            Assert.True(homePage.StudentCount() > 0);
            Console.Write("The current student count is: " + homePage.StudentCount());
        }
        [Test]
        public void Test_Homepage_Links()
        {
            HomePage homePage = new HomePage(driver);
            
            homePage.OpenPage();
            homePage.HomePageLink.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            homePage.OpenPage();
            homePage.ViewStudentsLink.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());

            homePage.OpenPage();
            homePage.AddStudentLink.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());


        }
    }
}
