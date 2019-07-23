using Iqvia.E360.CodeListManager.AutomatedTests.Models;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Interactions;

namespace Iqvia.E360.CodeListManager.AutomatedTests.Utils.Workers
{
    public class CodelistWorker
    {
        private RemoteWebDriver _driver;
        private ElementHelper _elements;
        private Vocabulary _vocabulary;

        private CodeList _codelist;

        private WebDriverWait _waiter; 

        public CodelistWorker(RemoteWebDriver driver)
        {
            _driver = driver;

            _elements = new ElementHelper(driver);
            _codelist = new CodeList();
            _waiter = new WebDriverWait(_driver,TimeSpan.FromSeconds(15));
        }

        public string getCodelistNameFromBreadcrumb()
        {
            return _elements.FindElement("breadcrumb").Text;
        }

        public void makeGlobalSearch(string criteria)
        {
            var searchInput = _elements.GetVisibleElement("globalSearchInput");
            searchInput.Click();
            searchInput.SendKeys(criteria);
            _elements.FindElement("globalSearchButton").Click();
        }
    
        //wishlist: pass column name to params so that no need to hardcode column name in the locator
        public void makeColumnSearch(string criteria)
        {
            var searchInput = _elements.GetVisibleElement("sourceNameColumnSearchInput");
            searchInput.Click();
            searchInput.SendKeys(criteria);
            searchInput.SendKeys(Keys.Return);
        }

        //to do - get number of selected codes and store it in a variable; after adding, return it
        public int selectAndAddCode() 
        {
            _elements.FindElement("rowInTable").Click();
            _elements.FindElement("addSelectedButton").Click();
            return 1;
        }

        // add all results and return code count displayed in the codelist header after add
        public int addAllResults()
        {
            _elements.FindElement("look-up-table-action-button-add-all").Click();
            
            //after this change need to run the test again to make sure it is working 
            _waiter.Until(ExpectedConditions.ElementIsVisible(By.ClassName("notification-success")));

            return Convert.ToInt32(_elements.FindElement("codesCount").Text);
        }

        public int getCodeCountFromHeader()
        {
            //the following line should be moved to ctor
            _waiter.Until(ExpectedConditions.ElementIsVisible(By.ClassName("notification-success")));

            return Convert.ToInt32(_elements.FindElement("codesCount").Text);
        }

        public int getTotalSearchResultsCount()
        {
            var resultsCount = _elements.FindElement("searchResultsCount");
            var resultsCountStr = resultsCount.Text;
            return Convert.ToInt32(resultsCountStr.Substring(15));
        }

        //todo !!!!!!!!!!!!! 
        public bool globalSearchVerifyResults (string criteria)
        {
            // var searchInput = _elements.GetVisibleElement("globalSearch");
            // searchInput.Click();
            // searchInput.SendKeys(criteria);
            return true;
        }
        public void selectCodes(int number) 
        {

        }
    }
}