using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AutomacaoMantis.Helpers
{
    public class Browsers
    {
        private static string seleniumHub = BuilderJson.ReturnParameterAppSettings("SELENIUM_HUB");

        #region Chrome
        public static IWebDriver GetLocalChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-maximized");
            chromeOptions.AddArgument("enable-automation");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-browser-side-navigation");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;

            return new ChromeDriver(GeneralHelpers.GetProjectBinDebugPath(), chromeOptions);
        }

        public static IWebDriver GetRemoteChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--lang=pt-BR");

            return new RemoteWebDriver(new Uri(seleniumHub), chromeOptions.ToCapabilities());
        }

        public static IWebDriver GetLocalChromeHeadless()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");

            return new ChromeDriver(GeneralHelpers.GetProjectBinDebugPath(), chromeOptions);
        }

        public static IWebDriver GetRemoteChromeHeadless()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--lang=pt-BR");
            chromeOptions.AddArgument("--headless");

            return new RemoteWebDriver(new Uri(seleniumHub), chromeOptions.ToCapabilities());
        }
        #endregion

        #region Firefox
        public static IWebDriver GetLocalFirefox()
        {
            return new FirefoxDriver(GeneralHelpers.GetProjectBinDebugPath());
        }

        public static IWebDriver GetRemoteFirefox()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.SetPreference("intl.accept_languages", "pt-BR");

            return new RemoteWebDriver(new Uri(seleniumHub), firefoxOptions.ToCapabilities());
        }
        #endregion

        #region Internet Explorer
        public static IWebDriver GetLocalInternetExplorer()
        {
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();
            ieOptions.EnableNativeEvents = true;
            ieOptions.RequireWindowFocus = true;
            return new InternetExplorerDriver(GeneralHelpers.GetProjectBinDebugPath(), ieOptions);
        }

        public static IWebDriver GetRemoteInternetExplorer()
        {
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();

            return new RemoteWebDriver(new Uri(seleniumHub), ieOptions.ToCapabilities());
        }
        #endregion

        #region Edge
        public static IWebDriver GetLocalEdge()
        {
            return new EdgeDriver(GeneralHelpers.GetProjectBinDebugPath());
        }

        public static IWebDriver GetRemoteEdge()
        {
            EdgeOptions edgeOptions = new EdgeOptions();

            return new RemoteWebDriver(new Uri(seleniumHub), edgeOptions.ToCapabilities());
        }
        #endregion
    }
}