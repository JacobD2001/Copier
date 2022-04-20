using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie2;

namespace UnitTests2
{
    [TestClass]
    public class UnitTest2
    {

        public class ConsoleRedirectionToStringWriter : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleRedirectionToStringWriter()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxMethod_StateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("test.pdf");
                string to = "recipent";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxMethod_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("test.pdf");
                string to = "recipent";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanSendFax_StateOff()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                string to = "lala";
                copier.ScanSendFax(to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanSendFax_StateOn()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                string to = "recipent";
                copier.ScanSendFax(to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ReceivedFaxes()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            string from = "sender";

            IDocument doc1 = new PDFDocument("test.pdf");
            IDocument doc2 = new TextDocument("test.txt");
            IDocument doc3 = new ImageDocument("test.jpg");
            IDocument doc4 = new ImageDocument("test.jpg");

            copier.ReceiveFax(in doc1, from);
            copier.ReceiveFax(in doc2, from);
            copier.ReceiveFax(in doc3, from);

            copier.PowerOff();
            copier.ReceiveFax(in doc4, from);
            copier.PowerOn();

            Assert.AreEqual(3, copier.ReceivedFaxes);

        }
    }
}
