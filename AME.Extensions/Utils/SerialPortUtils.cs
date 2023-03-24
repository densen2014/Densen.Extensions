// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace AME.Util;

public class SerialPortUtils
{

    public static List<string> GetPortNames()
    {
        return SerialPort.GetPortNames().ToList();
    }


    public static SerialPort SerialPort = null;
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

        //向控制台打印数据
        Debug.WriteLine("收到数据：" + recvData);
    }

    public static bool SendData(byte[] data)
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            SerialPort.Write(data, 0, data.Length);
            Debug.WriteLine("发送数据：" + data);
            return true;
        }
        else
        {
            return false;
        }
    }

}
