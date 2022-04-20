using System;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State { on, off };

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

    //Zadanie 3

    public class Printer : BaseDevice, IPrinter //Printer class (1)
    {
        public int PrintCount { get; set; }

        public void Print(in IDocument document)
        { 
            

            if (GetState() == IDevice.State.on)
            {
               
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}"); //prints info
                PrintCount++;

            }
        }
    }

    public class Scanner : BaseDevice, IScanner //Scanmer class (2)
    {
        public int ScanCount { get; set; }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.TXT)
        {
            document = null;
            DateTime printScanInfo = DateTime.Now;

            if (GetState() == IDevice.State.on)
            {

                if (formatType == IDocument.FormatType.JPG)
                    document = new ImageDocument($"{printScanInfo} Scan: ImageScan{ScanCount}.jpg");
                else if (formatType == IDocument.FormatType.PDF)
                    document = new PDFDocument($"{printScanInfo} Scan: PDFScan{ScanCount}.pdf");
                else if (formatType == IDocument.FormatType.TXT)
                    document = new TextDocument($"{printScanInfo} Scan: TextScan{ScanCount}.txt");

                ScanCount++; //increments scan counter

                Console.WriteLine($"{printScanInfo} Scan: {document.GetFileName()}");


            }

        }
    }

}
