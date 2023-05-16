// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace AME.Util;
#nullable enable

public class SerialPortUtils
{

    public static List<string> GetPortNames()
    {
        return SerialPort.GetPortNames().ToList();
    }


    public static SerialPort? SerialPort = null;
    public static List<byte> RecvData =new List<byte>();
    public static string[]? RecvAry => BitConverter.ToString(RecvData.ToArray()).Split('-');
    public static SerialPort OpenClosePort(string comName, int baud)
    {
        //串口未打开
        if (SerialPort == null || !SerialPort.IsOpen)
        {
            SerialPort = new SerialPort();
            //串口名称
            SerialPort.PortName = comName;
            //波特率
            SerialPort.BaudRate = baud;
            //数据位
            SerialPort.DataBits = 8;
            //停止位
            SerialPort.StopBits = StopBits.One;
            //校验位
            SerialPort.Parity = Parity.None;
            //打开串口
            SerialPort.Open();
            //串口数据接收事件实现
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);

            return SerialPort;
        }
        //串口已经打开
        else
        {
            SerialPort.Close();
            return SerialPort;
        }
    }

    public static void ReceiveData(object sender, SerialDataReceivedEventArgs e)
    {
        var _SerialPort = (SerialPort)sender;

        var _bytesToRead = _SerialPort.BytesToRead;
        var recvData = new byte[_bytesToRead];

        _SerialPort.Read(recvData, 0, _bytesToRead);

        RecvData.AddRange(recvData);

        //向控制台打印数据
        //Console.WriteLine($"{Environment.NewLine}收到数据：{RecvData.ByteArrayToHexString()}");
       var  Recv = BitConverter.ToString(RecvData.ToArray ());   // 82-C8-EA-17
        Console.WriteLine($"{Environment.NewLine}收到数据：{Recv}"); 
    }

    public static void ClearRecvData()
    {
        RecvData = new List<byte>(); 
    } 


    public static bool SendData(byte[] data)
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            SerialPort.Write(data, 0, data.Length);
            //Console.WriteLine($"发送数据：{data.ByteArrayToHexString()}");
            Console.WriteLine($"发送数据：{BitConverter.ToString(data)}{Environment.NewLine}");
            return true;
        }
        else
        {
            return false;
        }
    }

}
