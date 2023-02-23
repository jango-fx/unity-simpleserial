using UnityEngine;
using System.IO.Ports;

namespace ƒx.SimpleSerial
{
    [CreateAssetMenu(fileName = "SerialConnection", menuName = "ScriptableObjects/Serial/Connection")]
    public sealed class SerialConnection : ScriptableObject
    {
        // [ReadOnly]
        public bool verbose = false;

        public EnhancedSerialPort mySerialPort;
        public string port = "COM1";
        public int baudRate = 115200;
        public bool rts = true;
        public bool dtr = true;

        public delegate void CallbackHandler(string data);
        public CallbackHandler callbackHandler;


        public SerialConnection()
        {
            this.Init();
        }

        [ContextMenu("Toggle Verbosity")]
        public void ToggleVerbosity()
        {
            this.verbose = !this.verbose;
            if (mySerialPort != null) mySerialPort.verbose = this.verbose;
        }

        [ContextMenu("Print Serial Port Names")]
        public void PrintPortNames()
        {
            Debug.Log("[SERIAL]: Ports\n" + string.Join("\n", System.IO.Ports.SerialPort.GetPortNames()));
        }

        [ContextMenu("▶️ Open Serial Connection")]
        public void Open()
        {
            if (verbose) Debug.Log("[SERIAL]: Opening " + port + " with " + baudRate + " baud");
            this.Init();
            mySerialPort.Open();
        }

        [ContextMenu("⏹️ Close Serial Connection")]
        public void Close()
        {
            if (verbose) Debug.Log("[SERIAL]: Closing " + port);
            mySerialPort.Close();
        }

        // [ContextMenu("Reset Serial Connection")]
        public void Init()
        {
            mySerialPort = new EnhancedSerialPort(this.port);
            mySerialPort.BaudRate = this.baudRate;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = this.rts;
            mySerialPort.DtrEnable = this.dtr;
            mySerialPort.verbose = this.verbose;


            mySerialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            EnhancedSerialPort sp = (EnhancedSerialPort)sender;
            string incoming = sp.ReadExisting();
            if (sp.verbose) Debug.Log("[SERIAL]: " + incoming);

            callbackHandler.Invoke(incoming);
        }

        public void Send(string message)
        {
            mySerialPort.Write(message);
        }

        public void SendLine(string message)
        {
            mySerialPort.WriteLine(message);
        }

    }
}