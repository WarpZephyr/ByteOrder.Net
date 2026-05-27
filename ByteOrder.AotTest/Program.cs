using ByteOrder.Binary;
using ByteOrder.Numerics;
using System;

namespace ByteOrder.AotTest;

internal class Program
{
    static void Main(string[] args)
    {
        var val = new Int32<BigEndian>(42);
        Console.WriteLine($"Value: {val.Get()}");
    }
}
