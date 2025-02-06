using StudentsRegistryPOM.Pages;

namespace StudentsRegistryPOM.PagesTests
{
    internal class AddStudentsPageTests : BaseTests
    {
        [Test]
        public void Test_TestAddStudentPage_Content()
        {
            AddStudentPage page = new AddStudentPage(driver);
            page.OpenPage();
            Assert.Multiple(() =>
            {
                Assert.That(page.GetPageTitle(), Is.EqualTo("Add Student"), "Incorrect Expected Title");
                Assert.That(page.GetPageHeading(), Is.EqualTo("Register New Student"), "Incorrect Expected Heading");
                Assert.That(page.StudentNameField.Text, Is.EqualTo(""));
                Assert.That(page.StudentEmailField.Text, Is.EqualTo(""));
                Assert.That(page.AddButton.Text, Is.EqualTo("Add"));
            });
        }
        [Test]
        public void Test_TestAddStudentPage_Links()
        {
            AddStudentPage studentPage = new AddStudentPage(driver);
            studentPage.OpenPage();

            Assert.Multiple(() =>
            {
                studentPage.HomePageLink.Click();
                Assert.IsTrue(new HomePage(driver).IsOpen());

                studentPage.OpenPage();
                studentPage.ViewStudentsLink.Click();
                Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());

                studentPage.OpenPage();
                studentPage.AddStudentLink.Click();
                Assert.IsTrue(new AddStudentPage(driver).IsOpen());
            });            
        }
        [Test]
        public void Test_TestAddStudentPage_AddValidStudent()
        {
            AddStudentPage pageStudents = new AddStudentPage(driver);
            pageStudents.OpenPage();

            string name = GenerateRandomName();
            string email = GenerateRandomEmail(name);

            pageStudents.AddStudentFunction(name, email);

            ViewStudentsPage studentsPage = new ViewStudentsPage(driver);
                        
            Assert.That(studentsPage.IsOpen(), Is.True);

            var students = studentsPage.GetRegisteredStudents();

            string newStudentFullString = name + " (" + email + ")";

            Assert.That(students, Does.Contain(newStudentFullString));
          
        }
        private string GenerateRandomName()
        {
            var random = new Random();
            string[] names = { "Ivan", "Petar", "Djeki", "Djoni" };
            return names[random.Next(names.Length)] + random.Next(999, 9999).ToString();
        }

        private string GenerateRandomEmail(string name)
        {
            var random = new Random();
            string domain = "@gmail.com";
            return name.ToLower() + random.Next(999, 9999).ToString() + domain;
        }



    }
}
