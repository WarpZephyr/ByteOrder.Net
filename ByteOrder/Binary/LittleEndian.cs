using System;
using System.Runtime.CompilerServices;

namespace ByteOrder.Binary;

/// <summary>
/// A definition of the little-endian byte order.
/// </summary>
public readonly struct LittleEndian : IEndian
{
    /// <inheritdoc/>
    public static bool IsLittleEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => true;
    }

    /// <inheritdoc/>
    public static bool IsBigEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => false;
    }

    /// <inheritdoc/>
    public static bool IsNativeEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => BitConverter.IsLittleEndian;
    }
}
