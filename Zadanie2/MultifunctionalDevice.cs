using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax
    {
        public int SentFaxes { get; set; }
        public int ReceivedFaxes { get; set; }

        public void SendFax(in IDocument document, string recpient)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Fax: {document.GetFileName()} sent to : {recpient}");
                SentFaxes++;
            }
        }

        public void ReceiveFax(in IDocument document, string sender)
        {
            if (GetState() == IDevice.State.on)
            {              
                Console.WriteLine($"Received document: { document.GetFileName() } from: { sender }");
          
                Print(document);
                ReceivedFaxes++;
            }
            
        }

        public void ScanSendFax(String recipent) 
        {
            IDocument scannedDocument;
            Scan(out scannedDocument); //scans document
            SendFax(scannedDocument, recipent); //sends fax
           
        }
    }
}
