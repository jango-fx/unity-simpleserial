# SimpleSerial for Unity

basic serial communication for Unity.

developed in combination with ***[SimpleSerialVS](https://github.com/jango-fx/unity-simpleserial-vs)*** to enable serial communication with Unity's VisualScripting.

## ⚠️ wip
work in progress!  
only tested under macOS 13.2 (22D49, M1 Chip) and Unity 2021.3.
 
## 🙏 thx
makes heavy use of [Antanas Veivery's *EnhancedSerialPort*](https://antanas.veiverys.com/mono-serialport-datareceived-event-workaround-using-a-derived-class/) as replacement for .Net's `Serialport.DataReceived`-callback, which is not implemented in Mono.
