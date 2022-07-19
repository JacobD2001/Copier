using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; } 
        public int ScanCounter { get; set; } 
        public new int Counter { get; set; }

        public void Print(in IDocument document)
        {

            if (GetState() == IDevice.State.on)
            {
                DateTime printInfo = DateTime.Now;
                Console.WriteLine($"{printInfo} Print: {document.GetFileName()}");

                PrintCounter++;
            }

        }

        public new void PowerOn()
        {
            if (GetState() == IDevice.State.off) 
            {
                base.PowerOn(); 
                Counter++;    
            }
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.TXT) //printing scan info
        {
            document = null;
            DateTime printScanInfo = DateTime.Now;

            if (GetState() == IDevice.State.on)
            {

                if (formatType == IDocument.FormatType.JPG)
                    document = new ImageDocument($"{printScanInfo} Scan: ImageScan{ScanCounter}.jpg");
                else if (formatType == IDocument.FormatType.PDF)
                    document = new PDFDocument($"{printScanInfo} Scan: PDFScan{ScanCounter}.pdf");
                else if (formatType == IDocument.FormatType.TXT)
                    document = new TextDocument($"{printScanInfo} Scan: TextScan{ScanCounter}.txt");

                ScanCounter++; //increments scan counter

                Console.WriteLine($"{printScanInfo} Scan: {document.GetFileName()}");
            }
        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);
                Print(in document);
            }
        }
    }
}



