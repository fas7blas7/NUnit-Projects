Simple tests with NUnit. Different approaches used. Webdriver learning and playing with. DropDown manipulations. Working with Web Tables. Handles of form inputs. Web app testing. 

# GitHub Repository - POM Testing Projects

## Overview
This repository contains two projects implementing the Page Object Model (POM) approach for automated UI testing with NUnit.

## Projects
### 1. **StudentsRegistryPOM**
A test automation project for a student registry application using the POM approach with NUnit.

#### **Project Structure:**
- **Pages/** _(Contains page object classes)_
  - `AddStudentPage`
  - `BasePage`
  - `HomePage`
  - `ViewStudentsPage`
- **PageTests/** _(Contains test classes)_
  - `AddStudentsPageTests`
  - `BaseTests`
  - `HomePageTests`
  - `ViewStudentsPageTests`

### 2. **POM Shopping App**
A test automation project for an e-commerce shopping application using the POM approach with NUnit.

#### **Project Structure:**
- **Pages/** _(Contains page object classes)_
  - `BasePage`
  - `CartPage`
  - `CheckoutPage`
  - `HiddenMenuPage`
  - `InventoryPage`
  - `LoginPage`
- **Tests/** _(Contains test classes)_
  - `BaseTest`
  - `CartTests`
  - `CheckoutTests`
  - `HiddenMenuTests`
  - `InventoryTests`
  - `LoginTests`

## Technologies Used
- C#
- NUnit
- Selenium WebDriver
- Page Object Model (POM)

## Getting Started
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/your-repo-name.git
   ```
2. Open the project in Visual Studio.
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Run tests:
   ```sh
   dotnet test
   ```
