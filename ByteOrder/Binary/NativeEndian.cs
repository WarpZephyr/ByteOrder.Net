using System;
using System.Runtime.CompilerServices;

namespace ByteOrder.Binary;

/// <summary>
/// A definition of the current native byte order.
/// </summary>
public readonly struct NativeEndian : IEndian
{
    /// <inheritdoc/>
    public static bool IsLittleEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => BitConverter.IsLittleEndian;
    }

    /// <inheritdoc/>
    public static bool IsBigEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => !BitConverter.IsLittleEndian;
    }

    /// <inheritdoc/>
    public static bool IsNativeEndian
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => true;
    }
}
