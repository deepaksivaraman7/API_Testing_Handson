using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentsNUnit.Utilities
{
    internal class CoreCodes
    {
        protected RestClient client;
        protected ExtentReports extent;
        protected ExtentTest test;
        ExtentSparkReporter sparkReporter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            string logFilePath = currDir + "/Logs/Log_" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
        }

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://jsonplaceholder.typicode.com/");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}
