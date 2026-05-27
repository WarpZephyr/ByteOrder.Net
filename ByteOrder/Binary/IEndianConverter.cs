using System;

namespace ByteOrder.Binary;

/// <summary>
/// An interface defining methods to swap byte order to that of the converter.
/// </summary>
/// <typeparam name="T">The type being represented.</typeparam>
public interface IEndianConverter<T>
    where T : unmanaged
{
    /// <summary>
    /// Swap the source endianness to that of the converter.
    /// </summary>
    /// <remarks>
    /// If the endianness is not the same a swap is done.<br/>
    /// If the endianness is the same the value is returned as-is.
    /// </remarks>
    /// <param name="source">The value to swap.</param>
    /// <returns>The swapped result.</returns>
    public static abstract T Swap(T source);

    /// <summary>
    /// Swap the endianness of the specified <see cref="Span{T}"/> to that of the converter into itself.
    /// </summary>
    /// <remarks>
    /// If the endianness is not the same a swap is done.<br/>
    /// If the endianness is the same nothing is done.
    /// </remarks>
    /// <param name="destination">The <see cref="Span{T}"/> to swap into itself.</param>
    public static abstract void Swap(Span<T> destination);

    /// <summary>
    /// Swap the endianness of the specified <see cref="ReadOnlySpan{T}"/> source to that of the converter into the specified <see cref="Span{T}"/> destination.
    /// </summary>
    /// <remarks>
    /// If the endianness is not the same a swap is done.<br/>
    /// If the endianness is the same a copy is done.
    /// </remarks>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to swap.</param>
    /// <param name="destination">The <see cref="Span{T}"/> to store the result in.</param>
    public static abstract void Swap(ReadOnlySpan<T> source, Span<T> destination);
}
