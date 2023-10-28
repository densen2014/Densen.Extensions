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
#pragma warning disable CA1416 // #warning 指令

public class SerialPortUtils
{

    public static List<string> GetPortNames()
    {
        return SerialPort.GetPortNames().ToList();
    }


    public static bool Debug = false;
    public static SerialPort? SerialPort = null;
    public static List<byte> Buffer = new List<byte>();
    //public static string[]? RecvAry => BitConverter.ToString(Buffer.ToArray()).Split('-');
    public static SerialPort OpenClosePort(string comName, int baud, bool debug = false)
    {
        Debug=debug;

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
        do
        {
            var _bytesToRead = _SerialPort.BytesToRead;
            var recvData = new byte[_bytesToRead];

            _SerialPort.Read(recvData, 0, _bytesToRead);

            //1.缓存数据
            Buffer.AddRange(recvData);
        } while (_SerialPort.BytesToRead > 0);

        //向控制台打印数据
        //Console.WriteLine($"{Environment.NewLine}收到数据：{RecvData.ByteArrayToHexString()}");
        var Recv = BitConverter.ToString(Buffer.ToArray());   // 82-C8-EA-17
        if (Debug) Console.WriteLine($"{Environment.NewLine}收到数据：{Recv}");
    }

    public static void ClearRecvData()
    {
        Buffer = new List<byte>();
    }


    public static bool SendData(byte[] data)
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            SerialPort.Write(data, 0, data.Length);
            //Console.WriteLine($"发送数据：{data.ByteArrayToHexString()}");
            if (Debug) Console.WriteLine($"发送数据：{BitConverter.ToString(data)}{Environment.NewLine}");
            return true;
        }
        else
        {
            return false;
        }
    }

}
