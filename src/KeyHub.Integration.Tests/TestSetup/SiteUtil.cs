﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyHub.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace KeyHub.Integration.Tests.TestSetup
{
    public class SiteUtil
    {
        public static void CreateLocalAccount(KeyHubWebDriver site, string email, string password, Action<RemoteWebDriver> onFinish = null)
        {
            onFinish = onFinish ?? delegate(RemoteWebDriver browser)
            {
                WaitUntilUserIsLoggedIn(browser);
            };

            using (var browser = BrowserUtil.GetBrowser())
            {
                browser.Navigate().GoToUrl(site.UrlFor("Account/Register"));

                SubmitRegistrationForm(browser, email, password);

                onFinish(browser);
            }
        }

        public static IWebElement WaitUntilUserIsLoggedIn(RemoteWebDriver browser)
        {
            return browser.FindElementByCssSelector("a[href='/Account/LogOff']");
        }

        public static void SubmitLoginForm(RemoteWebDriver browser, string email, string password)
        {
            var formSelector = "form[action^='/Account/Login'] ";
            browser.FindElementByCssSelector(formSelector + "input#Email").SendKeys(email);
            browser.FindElementByCssSelector(formSelector + "input#Password").SendKeys(password);
            browser.FindElementByCssSelector(formSelector + "input[value='Log in']").Click();
            WaitUntilUserIsLoggedIn(browser);
        }

        public static void SubmitRegistrationForm(RemoteWebDriver browser, string email, string password)
        {
            browser.FindElementByCssSelector("input[name=Email]").SendKeys(email);
            browser.FindElementByCssSelector("input[name=Password]").SendKeys(password);
            browser.FindElementByCssSelector("input[name=ConfirmPassword]").SendKeys(password);
            browser.FindElementByCssSelector("input[value=Register]").Click();
        }

        public static void SetValueForChosenJQueryControl(RemoteWebDriver browser, string cssSelector, string value)
        {
            // We're using the jQuery Chosen library for some front-end widgets.  These require special 
            // handling to get the timing correct.

            var selector = browser.FindElementByCssSelector(cssSelector);

            selector.Click();
            
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector + " input[type=text]")));

            browser.FindElementByCssSelector(cssSelector + " input[type=text]").SendKeys(value + Keys.Enter);
        }

        public static void SetDateValueForJQueryDatepicker(RemoteWebDriver browser, string elementSelector, DateTime value)
        {
            var formattedDate = value.ToString("d MMMM yyyy");
            browser.FindElementByCssSelector(elementSelector);
            browser.ExecuteScript("$(arguments[0]).datepicker('setDate', arguments[1]);", elementSelector, formattedDate);
        }

        public static string CreateCustomer(RemoteWebDriver browser)
        {
            var customerName = "customer.name";

            browser.FindElementByCssSelector("a[href='/Customer']").Click();
            browser.FindElementByCssSelector("a[href='/Customer/Create']").Click();

            browser.FindElementByCssSelector("#Customer_Name").SendKeys(customerName);
            browser.FindElementByCssSelector("#Customer_Department").SendKeys("customer.department");
            browser.FindElementByCssSelector("#Customer_Street").SendKeys("customer.street");
            browser.FindElementByCssSelector("#Customer_PostalCode").SendKeys("customer.postalcode");
            browser.FindElementByCssSelector("#Customer_City").SendKeys("customer.city");
            browser.FindElementByCssSelector("#Customer_Region").SendKeys("customer.region");
            browser.FindElementByCssSelector("input[value='Create Customer']").Click();

            browser.FindElementByCssSelector(".success");

            return customerName;
        }

        public static string CreateVendor(RemoteWebDriver browser)
        {
            string vendorName = "vendor name";

            browser.FindElementByCssSelector("a[href='/Vendor']").Click();
            browser.FindElementByCssSelector("a[href='/Vendor/Create']").Click();

            browser.FindElementByCssSelector("input#Vendor_Name").SendKeys(vendorName);
            browser.FindElementByCssSelector("input#Vendor_Street").SendKeys("vendor street");
            browser.FindElementByCssSelector("input#Vendor_PostalCode").SendKeys("vendor street");
            browser.FindElementByCssSelector("input#Vendor_City").SendKeys("vendor city");
            browser.FindElementByCssSelector("input#Vendor_Region").SendKeys("vendor region");

            browser.FindElementByCssSelector("form[action='/Vendor/Create'] input[type=submit]").Click();
            browser.FindElementByCssSelector(".success");

            return vendorName;
        }

        public static void CreateAccountRightsFor(RemoteWebDriver browser, string userEmail, ObjectTypes objectType, string objectName)
        {
            browser.FindElementByCssSelector("a[href='/Account']").Click();
            var vendorUserRow =
                browser.FindElementByLinkText(userEmail).FindElement(By.XPath("./ancestor::tr"));

            vendorUserRow.FindElement(By.CssSelector("a[href^='/Account/Edit']")).Click();

            browser.FindElementByCssSelector("a[href^='/AccountRights/Create'][href$='" + Enum.GetName(typeof(ObjectTypes), objectType) + "']").Click();

            SetValueForChosenJQueryControl(browser, "#ObjectId_chzn", objectName);
            browser.FindElementByCssSelector("form[action^='/AccountRights/Create'] input[type='submit']").Click();
            browser.FindElementByCssSelector(".success");
        }

        public static void CreateSku(RemoteWebDriver browser, string skuCode, string vendorName, string featureName)
        {
            browser.FindElementByCssSelector("a[href='/SKU']").Click();
            browser.FindElementByCssSelector("a[href='/SKU/Create']").Click();
            SetValueForChosenJQueryControl(browser, "#SKU_VendorId_chzn", vendorName);
            browser.FindElementByCssSelector("input#SKU_SkuCode").SendKeys(skuCode);
            SetValueForChosenJQueryControl(browser, "#SKU_SelectedFeatureGUIDs_chzn", featureName);
            browser.FindElementByCssSelector("input#SKU_LicenseDuration").SendKeys("100");
            browser.FindElementByCssSelector("input#SKU_AutoDomainDuration").SendKeys("100");
            browser.FindElementByCssSelector("form[action='/SKU/Create'] input[type='submit']").Click();
            browser.FindElementByCssSelector(".success");
        }

        public static void CreateFeature(RemoteWebDriver browser, string featureName, string vendorName)
        {
            browser.FindElementByCssSelector("a[href='/Feature']").Click();
            browser.FindElementByCssSelector("a[href='/Feature/Create']").Click();
            browser.FindElementByCssSelector("input#Feature_FeatureName").SendKeys(featureName);
            SetValueForChosenJQueryControl(browser, "#Feature_VendorId_chzn", vendorName);
            browser.FindElementByCssSelector("form[action='/Feature/Create'] input[type='submit']").Click();
            browser.FindElementByCssSelector(".success");
        }
    }
}
