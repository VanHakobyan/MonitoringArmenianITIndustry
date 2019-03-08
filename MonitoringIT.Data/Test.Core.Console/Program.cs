using System;
using Lib.MonitoringIT.Data.Staff.am.Scrapper;

namespace Test.Core.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            StaffScrapper scrapper = new StaffScrapper();
            scrapper.StartSScrapping();
        }
    }
}
