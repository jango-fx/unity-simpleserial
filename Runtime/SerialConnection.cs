using UnityEngine;
using System.IO.Ports;

namespace ƒx.SimpleSerial
{
    [CreateAssetMenu(fileName = "SerialConnection", menuName = "ScriptableObjects/Serial/Connection")]
    public sealed class SerialConnection : ScriptableObject
    {
        // [ReadOnly]
        public bool verbose = false;
        public bool useUpdate = false;

        public EnhancedSerialPort serialPort;
        public string port = "COM1";
        public int baudRate = 115200;
        public bool rts = true;
        public bool dtr = true;

        public delegate void CallbackHandler(string data);
        public CallbackHandler callbackHandler;


        public SerialConnection()
        {
            this.port = System.IO.Ports.SerialPort.GetPortNames()[0];
            this.Init();
        }

        [ContextMenu("Toggle Verbosity")]
        public void ToggleVerbosity()
        {
            this.verbose = !this.verbose;
            if (serialPort != null) serialPort.verbose = this.verbose;
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
            serialPort.Open();

            if(!useUpdate){
                serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
        }

        [ContextMenu("⏹️ Close Serial Connection")]
        public void Close()
        {
            if (verbose) Debug.Log("[SERIAL]: Closing " + port);
            serialPort.Close();
        }

        // [ContextMenu("Reset Serial Connection")]
        public void Init()
        {
            serialPort = new EnhancedSerialPort(this.port);
            serialPort.BaudRate = this.baudRate;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = this.rts;
            serialPort.DtrEnable = this.dtr;
            serialPort.verbose = this.verbose;
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
            serialPort.Write(message);
        }

        public void SendLine(string message)
        {
            serialPort.WriteLine(message);
        }

        public void Update(){
            if(callbackHandler != null) {
                if(serialPort.IsOpen){
                    if(serialPort.BytesToRead > 0){
                        string msg = serialPort.ReadTo("\n");
                            callbackHandler.Invoke(msg);
                        serialPort.DiscardInBuffer();
                    }
                }
            }
        }


    }
}