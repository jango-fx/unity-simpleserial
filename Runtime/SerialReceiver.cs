using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ƒx.SimpleSerial;

public class SerialReceiver : MonoBehaviour
{
    public SerialConnection serialConnection;

    void Start()
    {
        serialConnection.useReceiver = true;
        serialConnection.Open();
        // serialConnection.callbackHandler -= OnReceive;
        // serialConnection.callbackHandler += OnReceive;
    }

    void Update()
    {
        if(serialConnection.callbackHandler != null) {
            if(serialConnection.serialPort.IsOpen){
                if(serialConnection.serialPort.BytesToRead > 0){
                    string msg = serialConnection.serialPort.ReadTo("\n");
                        serialConnection.callbackHandler.Invoke(msg);
                    serialConnection.serialPort.DiscardInBuffer();
                }
            }
        }
    }

    void OnDestroy()
    {
        serialConnection.Close(); 
        serialConnection.useReceiver = false;
    }

    // public void OnReceive(string data){
    //         Debug.Log(data);
    // }
}
