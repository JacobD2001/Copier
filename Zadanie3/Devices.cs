using System;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State { on, off };
        void PowerOn(); 
        void PowerOff(); 
        State GetState(); 
        int Counter { get; }  
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
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
    public class Printer : BaseDevice, IPrinter 
    {
        public int PrintCount { get; set; }

        public void Print(in IDocument document)
        { 
            

            if (GetState() == IDevice.State.on)
            {
               
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}"); 
                PrintCount++;

            }
        }
    }

    public class Scanner : BaseDevice, IScanner 
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

                ScanCount++; 

                Console.WriteLine($"{printScanInfo} Scan: {document.GetFileName()}");
            }
        }
    }
}
