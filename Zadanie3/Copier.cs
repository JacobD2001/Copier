using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; } //zwraca aktualną liczbę wydrukowanych dokumentów,
        public int ScanCounter { get; set; } //zwraca liczbę zeskanowanych dokumentów,
        public new int Counter { get; set; }
        public Printer Printer { get; }
        public Scanner Scanner { get; }

        public void TurnScannerOn()
        {
            if (GetState() == IDevice.State.on)
                Scanner.PowerOn();
        }
        
        public void TurnScannerOff()
        {
            Scanner.PowerOff();
        }

        public void TurnPrinterOn()
        {
            if (GetState() == IDevice.State.on)
                Printer.PowerOn();
        }

        public void TurnPrinterOff()
        {
            Printer.PowerOff();
        }

        public void Print(in IDocument document)
        {
            throw new NotImplementedException();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            throw new NotImplementedException();
        }

    }




    
}
