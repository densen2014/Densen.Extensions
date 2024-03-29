﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Linq;
using System.Text;

namespace AME;

/// <summary>
/// Byte[] 扩展方法
/// </summary>
public static partial class BytesExtensions
{
    public static string BitConv(this byte[] data)
    {
        return BitConverter.ToString(data).Replace("-", string.Empty);
    }
    public static string StringBuilderTest(this byte[] data)
    {
        StringBuilder sb = new StringBuilder(data.Length * 2);
        foreach (byte b in data)
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
    public static string LinqConcat(this byte[] data)
    {
        return string.Concat(data.Select(b => b.ToString("X2")).ToArray());
    }
    public static string LinqJoin(this byte[] data)
    {
        return string.Join("",
            data.Select(
                bin => bin.ToString("X2")
                ).ToArray());
    }
    public static string LinqAgg(this byte[] data)
    {
        return data.Aggregate(new StringBuilder(),
                                   (sb, v) => sb.Append(v.ToString("X2"))
                                  ).ToString();
    }
    public static string ToHex(this byte[] bytes)
    {
        char[] c = new char[bytes.Length * 2];

        byte b;

        for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
        {
            b = ((byte)(bytes[bx] >> 4));
            c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

            b = ((byte)(bytes[bx] & 0x0F));
            c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
        }

        return new string(c);
    }
    public static string ByteArrayToHexString(this byte[] Bytes)
    {
        StringBuilder Result = new StringBuilder(Bytes.Length * 2);
        string HexAlphabet = "0123456789ABCDEF";

        foreach (byte B in Bytes)
        {
            Result.Append(HexAlphabet[B >> 4]);
            Result.Append(HexAlphabet[B & 0xF]);
        }

        return Result.ToString();
    }
}
