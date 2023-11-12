using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class FanControl : MonoBehaviour
{
    private SerialPort serialPort;
    public enum PortName
    {
        COM4,
        TTYACM0
    }
    public PortName portName = PortName.COM4;

    public enum BaudRate
    {
        Baud9600 = 9600,
        Baud115200 = 115200
    }
    public BaudRate baudRate = BaudRate.Baud9600;
    [Header("Fan Switch")]

    public bool Open = false;
    public bool Close = true;


    void Start()
    {
        serialPort = new SerialPort(GetPortName(), (int)baudRate);
        serialPort.Open();
    }

    void Update()
    {
        
        FanSwitch();
    }

    string GetPortName()
    {
        switch (portName)
        {
            case PortName.COM4:
                return "COM4";
            case PortName.TTYACM0:
                return "/dev/ttyACM0";
            default:
                Debug.LogError("Invalid port name");
                return null;
        }
    }

    public void FanSwitch()
    {
        if(Open == false && Close == true)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("CLOSE");
            }
        }
        else if(Open == true && Close == false)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("OPEN");
            }
        }
        else if(Open == false && Close == false)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("CLOS");
            }
        }
    }

    private void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
