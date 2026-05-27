using ByteOrder.Binary;
using System;
using System.Runtime.CompilerServices;

namespace ByteOrder.Numerics;

/// <summary>
/// An endian converter that acts as a "do nothing" pass-through when swapping is not needed in generic scenarios.
/// </summary>
/// <typeparam name="T">The type being converted.</typeparam>
public readonly struct NativeEndianConverter<T> : IEndianConverter<T>
    where T : unmanaged
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Swap(T source)
        => source;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<T> destination) { }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<T> source, Span<T> destination)
        => source.CopyTo(destination);
}
