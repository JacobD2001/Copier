using System;

namespace Zadanie2
{
    public interface IDevice
    {
        enum State {on, off};

        void PowerOn();
        void PowerOff(); 
        State GetState(); 

        int Counter {get;}  
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

    public interface IFax : IDevice
    {
        void SendFax(in IDocument document, string recpient);
        void ReceiveFax(in IDocument document, string sender);
    }


}
