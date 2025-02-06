using StudentsRegistryPOM.Pages;

namespace StudentsRegistryPOM.PagesTests
{
    internal class ViewStudentsPageTests : BaseTests
    {
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            ViewStudentsPage page = new ViewStudentsPage(driver);
            page.OpenPage();
            Assert.That(page.GetPageTitle(), Is.EqualTo("Students"));
            Assert.That(page.GetPageHeading(), Is.EqualTo("Registered Students"));

            var students = page.GetRegisteredStudents();

            foreach (var st in students)
            {
                Assert.That(st.Contains("("), Is.True, $"Student entry '{st}' does not contain '(' at the correct position");
                Assert.That(st.LastIndexOf(")") == st.Length-1, Is.True);
            }
        }
        [Test]
        public void Test_ViewStudentsPage_Links()
        {
            ViewStudentsPage page = new ViewStudentsPage(driver);
            page.OpenPage();

            Assert.Multiple(() =>
            {
                page.HomePageLink.Click();
                Assert.IsTrue(new HomePage(driver).IsOpen());

                page.ViewStudentsLink.Click();                
                Assert.That(new ViewStudentsPage(driver).IsOpen());

                page.OpenPage();
                page.AddStudentLink.Click();
                Assert.That(new AddStudentPage(driver).IsOpen());
            });
        }
    }
}
