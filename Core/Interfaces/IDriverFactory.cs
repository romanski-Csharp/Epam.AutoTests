using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Core.Interfaces
{
    internal interface IDriverFactory
    {
        IWebDriver CreateDriver(string DownloadDirectory);
    }
}
