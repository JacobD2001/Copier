using System;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new MultifunctionalDevice();
            IDocument doc1 = new PDFDocument("BirthDoc.pdf");
            xerox.PowerOn();
            xerox.SendFax(doc1,"Andrew");
            xerox.ReceiveFax(doc1, "Jacob");
        
            System.Console.WriteLine(xerox.ReceivedFaxes);
            System.Console.WriteLine(xerox.SentFaxes);


        }
    }
}
