using UnityEngine;
using System.IO.Ports;

public class SerialRead_JoyStick : MonoBehaviour
{
    private SerialPort serialPort;
    public enum PortName
    {
        COM6,
        TTYACM0
    }
    public PortName portName = PortName.COM6;

    public enum BaudRate
    {
        Baud9600 = 9600,
        Baud115200 = 115200
    }
    public BaudRate baudRate = BaudRate.Baud9600;
    

    void Start()
    {
        // Configure the serial port with the selected baud rate
        serialPort = new SerialPort(GetPortName(), (int)baudRate);
        serialPort.Open();
    }

    void Update()
    {
        // Example: Reading data from the serial port
        if (serialPort.IsOpen)
        {
            string data = serialPort.ReadLine();
            Debug.Log("Received data: " + data);
        }
    }
    
    string GetPortName()
    {
        switch (portName)
        {
            case PortName.COM6:
                return "COM6";
            case PortName.TTYACM0:
                return "/dev/ttyACM0";
            default:
                Debug.LogError("Invalid port name");
                return null;
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application is closed
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
