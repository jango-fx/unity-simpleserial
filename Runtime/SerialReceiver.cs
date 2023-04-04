using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ƒx.SimpleSerial;

public class SerialReceiver : MonoBehaviour
{
    public SerialConnection serialConnection;

    void Start()
    {
        serialConnection.useUpdate = true;
        serialConnection.Open();
    }

    void Update()
    {
        // if(serialConnection.callbackHandler != null) {
        //     if(serialConnection.serialPort.IsOpen){
        //         if(serialConnection.serialPort.BytesToRead > 0){
        //             string msg = serialConnection.serialPort.ReadTo("\n");
        //                 serialConnection.callbackHandler.Invoke(msg);
        //             serialConnection.serialPort.DiscardInBuffer();
        //         }
        //     }
        // }
        serialConnection.Update();
    }

    void OnDestroy()
    {
        serialConnection.Close(); 
        serialConnection.useUpdate = false;
    }
}
