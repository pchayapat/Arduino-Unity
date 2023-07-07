using UnityEngine;
using System.IO.Ports;

public class JoystickArduino : MonoBehaviour
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

    public Rigidbody rb;
    public float sensitivity = 0.01f;

    public GameObject targetObject;

    void Start()
    {
        serialPort = new SerialPort(GetPortName(), (int)baudRate);
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            string receivedString = serialPort.ReadLine();
            Debug.Log("Received data: " + receivedString);
            string[] data = receivedString.Split(',');

            if (data.Length == 3)
            {
                float axisX = float.Parse(data[0]);
                float axisY = float.Parse(data[1]);
                float rotation = float.Parse(data[2]);

                Vector3 movement = new Vector3(axisX, 0f, axisY) * sensitivity;
                rb.AddForce(movement, ForceMode.VelocityChange);

                transform.Rotate(0f, rotation, 0f);
            }
        }
        SetHighLED();
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

    public void SetHighLED()
    {
        if (targetObject != null && targetObject.GetComponent<Collider>() != null)
        {
            // Check for collisions
            if (targetObject.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                // Collision occurred
                if (serialPort.IsOpen)
                {
                    // Send data to Arduino to set LED pin 13 high
                    serialPort.WriteLine("HIGH");
                }
            }
            else
            {
                if (serialPort.IsOpen)
                {
                    // Send data to Arduino to set LED pin 13 high
                    serialPort.WriteLine("LOW");
                }
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