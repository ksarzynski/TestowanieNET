using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CinemaSeleniumTests
{
    [TestClass]
    public class CustomerTests
    {
            InternetExplorerDriver _driver;
            string _url;
            string _email;
            string _password;
            string _customerFirstName;
            string _customerLastName;
            string _customerEmail;

        [TestInitialize]
            public void Initialize()
            {
                _url = "https://localhost:44378";
                _driver = new InternetExplorerDriver(@"C:\Users\kamil\Desktop\TestowanieNet\Projekt 3\CinemaSeleniumTests\bin\Debug\netcoreapp2.0");
                _email = "admin@admin.com";
                _password = "p@sw1ooorD";
                _customerFirstName = "Kamil";
                _customerLastName = "Sarzyñski";
                _customerEmail = "ksarzynski@gmail.com";
             }

        [TestMethod]
        public void LogInViewTest()
        {
            var _urlaccount = "https://localhost:44378/account/login";

            try
            {
                NavigateToURL(_urlaccount);

                var emailField = _driver.FindElement(By.Id("Email"));
                emailField.Click();
                emailField.SendKeys(_email);

                var passwordField = _driver.FindElement(By.Id("Password"));
                passwordField.Click();
                passwordField.SendKeys(_password);

                _driver.FindElement(By.XPath("//button[@type='submit'][text()='Log in']")).Click();

                var checkLogin = _driver.FindElement(By.Id("UserLogin")).Text;
                StringAssert.Contains(checkLogin, _email);
            }
            catch (Exception e)
            {
             Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
          }

        [TestMethod]
        public void CustomerCrudOperations()
        {
            CreateCustomerViewTest();
            DetailsCustomerViewTest();
            EditCustomerViewTest();
            DeleteCustomerViewTest();
        }
                
        private void CreateCustomerViewTest()
        {
            var firstName = _customerFirstName;
            var lastName = _customerLastName;
            var email = _customerEmail;
             
            try
            {
                NavigateToURL(_url);
                LogIn(_email, _password);
                _driver.FindElement(By.CssSelector("[href *= 'Customers']")).Click();
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                var expected = elements.Count + 1;

                _driver.FindElement(By.CssSelector("[href *= 'Create']")).Click();
                _driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                _driver.FindElement(By.Id("LastName")).SendKeys(lastName);
                _driver.FindElement(By.Id("Email")).SendKeys(email);
                _driver.FindElement(By.ClassName("btn-default")).Click();
                _driver.FindElement(By.CssSelector("[href *= 'Customers']")).Click();

                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                Assert.AreEqual(expected, elements.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }
        private void DetailsCustomerViewTest()
        {
            var name = _customerFirstName;
           
            try
            {
                NavigateToURL(_url);
                LogIn(_email, _password);
                _driver.FindElement(By.CssSelector("[href *= 'Customers']")).Click();
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                      "[normalize-space(text())='" + name + "']]//" +
                      "a[@id='customer_details']"))
                      .Click();

                StringAssert.Contains(_driver.Url, "/Customers/Details/");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }
       public void EditCustomerViewTest()
        {
            var lastName = _customerLastName;
            var newLastName = "Kowalski";
            
            try
            {
                NavigateToURL(_url);
                LogIn(_email, _password);
                _driver.FindElement(By.CssSelector("[href *= 'Customers']")).Click();
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                      "[normalize-space(text())='" + lastName + "']]//" +
                      "a[@id='customer_edit']"))
                      .Click();
                var teamName = _driver.FindElement(By.Id("LastName"));
                teamName.Clear();
                teamName.SendKeys(newLastName);
                _driver.FindElement(By.Id("customer_edit")).Click();
                StringAssert.Contains(_url + "/Customers", _driver.Url);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }
                
        private void DeleteCustomerViewTest()
        {
            var name = _customerFirstName;
           
            try
            {
                NavigateToURL(_url);
                LogIn(_email, _password);
                _driver.FindElement(By.CssSelector("[href *= 'Customers']")).Click();
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                var expected = elements - 1;

                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                       "[normalize-space(text())='" + name + "']]//" +
                       "a[@id='customer_delete']"))
                       .Click();
                _driver.FindElement(By.Id("customer_delete")).Click();
                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
            _driver = null;
            _url = null;
            _email = null;
            _password = null;
            _customerFirstName = null;
            _customerLastName = null;
            _customerEmail = null;
        }

        private void NavigateToURL(string url)
        {
            var nav = _driver.Navigate();
            nav.GoToUrl(url);
          }

        private void LogIn(string email, string password)
        {
            _driver.FindElement(By.CssSelector("[href*='Login']")).Click();
            _driver.FindElement(By.Id("Email")).SendKeys(email);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.XPath("//button[@type='submit'][text()='Log in']")).Click();
        }

        private void LogOut()
        {
            _driver.FindElement(By.Id("logout_button")).Click();
        }

    }
}