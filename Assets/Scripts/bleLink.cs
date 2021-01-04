using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEditorInternal;

public class bleLink : MonoBehaviour
{
    public string port="COM5";
    private SerialPort sp;
    private Thread recvThread;
    public string serial;
    bool isend = false;
    // Start is called before the first frame update
    void Start()
    {
        sp = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);

        if (!sp.IsOpen)
        {
            sp.Open();
        }
        recvThread = new Thread(ReceiveData);
        recvThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                string s = "";
                while ((s = sp.ReadLine()) != null)
                {
                    //print(s);
                    //print(sp.ReadLine());
                    serial = s;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
            if (isend)
            {
                break;
            }
        }
    }
    void OnDestroy()
    {
        isend = true;
        recvThread.Abort();
    }
}
