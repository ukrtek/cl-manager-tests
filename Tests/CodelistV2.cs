using Iqvia.E360.CodeListManager.AutomatedTests.Models;
using Iqvia.E360.CodeListManager.AutomatedTests.Utils;
using Iqvia.E360.CodeListManager.AutomatedTests.Utils.WebDriverBuilder;
using Iqvia.E360.CodeListManager.AutomatedTests.Utils.Workers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Diagnostics;

namespace Iqvia.E360.CodeListManager.AutomatedTests
{
    [TestFixture]
    public class CodelistV2
    {
        private RemoteWebDriver _driver { get; set; }

        private string _codelistName { get; set; }

        private PlatformWorker _platformWorker;

        private CodelistWorker _codelistWorker;

        [OneTimeSetUp]
        public void init()
        {
            //kill all Google Chrome processes - windows only
            /*foreach (var processToKill in new[] { "chromedriver", "chrome" })
                foreach (var process in System.Diagnostics.Process.GetProcessesByName(processToKill))
                {
                    try
                    {
                        var processDescription = FileVersionInfo.GetVersionInfo(process.MainModule.FileName);
                        if (processDescription.ProductName.Equals("Google Chrome") || process.ProcessName.Equals("chromedriver"))
                        {
                            process.Kill();
                        }
                    }
                    // correct???
                    catch (System.Exception ex)
                    {
                        Trace.WriteLine("access denied");
                    }
                }*/

            var driverOption = ConfigProvider.GetFromSection<DriverOption>("driverOption");
            var codelist = ConfigProvider.GetFromSection<CodeList>("codelist");
            _codelistName = codelist.Name;

            if (codelist.UseExistingCodelist)
            {
                driverOption.StartUrl = codelist.ExistingCodelistURL;
            }

            _driver = WebDriverBuilder.BuildDriver(driverOption);
            var user = ConfigProvider.GetFromSection<TestUser>("testUser");

            _codelistWorker = new CodelistWorker(_driver);
            _platformWorker = new PlatformWorker(_driver);
            _platformWorker.LoginToPlatform(user);

            if (!codelist.UseExistingCodelist)
            {
                _platformWorker.NavigateTo("Codelist Manager", "View Grouped Codelists");
                _platformWorker.OpenTile("Marias Automated Tests");

                _platformWorker.OpenCreateCodelistModal();
                _codelistName = _platformWorker.FillAndSubmitNewCodelistForm(codelist);
            }
        }

        [Test]
        public void createCodelist()
        {
            var codelistNameFromBreadcrumb = _codelistWorker.getCodelistNameFromBreadcrumb();
            Assert.AreEqual(_codelistName, codelistNameFromBreadcrumb,
            $"codelist name from breadcrumb {codelistNameFromBreadcrumb} does not match name of codelist created {_codelistName}");
        }

        [Test]
        public void addCodeToFlatCodelistFromGlobalSearchSelect()
        //
        {
                _codelistWorker.makeGlobalSearch("sickle cell"); 
                var codesCount = _codelistWorker.selectAndAddCode();
                var countHeader = _codelistWorker.getCodeCountFromHeader();
                Assert.AreEqual(codesCount,countHeader);
        }

        [Test] 
        public void addAllCodesFromColumnSearch()
        {
            //make search in Source Name column
            _codelistWorker.makeColumnSearch("iron deficiency anemia");
            
            //add all codes and compare with the codes count in codelist header
            _codelistWorker.addAllResults();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            //_driver.Quit();
        }
    }
}