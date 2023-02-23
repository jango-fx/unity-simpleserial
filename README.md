# SimpleSerial for Unity

basic serial communication for Unity.

developed in combination with ***SimpleSerialVS*** to enable serial communication with Unity's VisualScripting.
 
makes heavy use of [Antanas Veiverys EnhancedSerialPort](https://antanas.veiverys.com/mono-serialport-datareceived-event-workaround-using-a-derived-class/) as replacement for .Net's `Serialport.DataReceived`-callback, which is not implemented in Mono.
## 🙏