using Listener.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TestIT.Listener
{
    public static class TestResultConverter
    {
        public static IEnumerable<TestResult> ConvertFromXml(Stream stream)
        {
            List<TestResult> testResults = new List<TestResult>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            XmlElement xRoot = xmlDocument.DocumentElement;

            foreach (XmlNode results in xRoot)
            {
                if (results.Name != "Results")
                    continue;

                foreach (XmlNode unitTestResult in results)
                {
                    if (unitTestResult.Name != "UnitTestResult")
                        continue;

                    TestResult testResult = new TestResult();
                    XmlNode outcomeNode = unitTestResult.Attributes.GetNamedItem("outcome");
                    if (outcomeNode != null)
                        testResult.Result = (TestResultOutcomes)Enum.Parse(typeof(TestResultOutcomes), outcomeNode.Value);

                    XmlNode testNameNode = unitTestResult.Attributes.GetNamedItem("testName");
                    if (testNameNode != null)
                        testResult.Name = testNameNode.Value;

                    if (unitTestResult.ChildNodes[0] != null && unitTestResult.ChildNodes[0].Name == "Output")
                    {
                        XmlNode outputNode = unitTestResult.ChildNodes[0];
                        if (outputNode.ChildNodes[0].Name == "ErrorInfo")
                        {
                            XmlNode errorInfoNode = outputNode.ChildNodes[0];

                            foreach (XmlNode info in errorInfoNode)
                            {
                                switch (info.Name)
                                {
                                    case "Message":
                                        testResult.Messenge = info.InnerText;
                                        break;
                                    case "StackTrace":
                                        testResult.StackTrace = info.InnerText;
                                        break;
                                }
                            }
                        }
                    }

                    testResults.Add(testResult);
                }
            }

            return testResults;
        }
    }
}
