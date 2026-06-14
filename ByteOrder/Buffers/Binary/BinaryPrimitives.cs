using ByteOrder.Binary;
using ByteOrder.Numerics;
using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Buffers.Binary;

/// <summary>
/// Reads bytes as primitives with generic endianness.
/// </summary>
/// <typeparam name="TEndian">The endianness to use.</typeparam>
public static class BinaryPrimitives<TEndian>
    where TEndian : unmanaged, IEndian, allows ref struct
{
    #region Int16

    /// <summary>
    /// Reads a <see cref="short" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="short" />.
    /// </exception>
    public static short ReadInt16(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadInt16BigEndian(source)
        : BinaryPrimitives.ReadInt16LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="short" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="short" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadInt16(scoped ReadOnlySpan<byte> source, out short value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadInt16LittleEndian(source, out value)
        : BinaryPrimitives.TryReadInt16BigEndian(source, out value);

    #endregion

    #region UInt16

    /// <summary>
    /// Reads a <see cref="ushort" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="ushort" />.
    /// </exception>
    public static ushort ReadUInt16(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadUInt16BigEndian(source)
        : BinaryPrimitives.ReadUInt16LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="ushort" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="ushort" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadUInt16(scoped ReadOnlySpan<byte> source, out ushort value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadUInt16LittleEndian(source, out value)
        : BinaryPrimitives.TryReadUInt16BigEndian(source, out value);

    #endregion

    #region Int32

    /// <summary>
    /// Reads a <see cref="int" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="int" />.
    /// </exception>
    public static int ReadInt32(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadInt32BigEndian(source)
        : BinaryPrimitives.ReadInt32LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="int" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="int" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadInt32(scoped ReadOnlySpan<byte> source, out int value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadInt32LittleEndian(source, out value)
        : BinaryPrimitives.TryReadInt32BigEndian(source, out value);

    #endregion

    #region UInt32

    /// <summary>
    /// Reads a <see cref="uint" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="uint" />.
    /// </exception>
    public static uint ReadUInt32(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadUInt32BigEndian(source)
        : BinaryPrimitives.ReadUInt32LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="uint" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="uint" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadUInt32(scoped ReadOnlySpan<byte> source, out uint value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadUInt32LittleEndian(source, out value)
        : BinaryPrimitives.TryReadUInt32BigEndian(source, out value);

    #endregion

    #region Int64

    /// <summary>
    /// Reads a <see cref="long" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="long" />.
    /// </exception>
    public static long ReadInt64(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadInt64BigEndian(source)
        : BinaryPrimitives.ReadInt64LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="long" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="long" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadInt64(scoped ReadOnlySpan<byte> source, out long value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadInt64LittleEndian(source, out value)
        : BinaryPrimitives.TryReadInt64BigEndian(source, out value);

    #endregion

    #region UInt64

    /// <summary>
    /// Reads a <see cref="ulong" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="ulong" />.
    /// </exception>
    public static ulong ReadUInt64(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadUInt64BigEndian(source)
        : BinaryPrimitives.ReadUInt64LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="ulong" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="ulong" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadUInt64(scoped ReadOnlySpan<byte> source, out ulong value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadUInt64LittleEndian(source, out value)
        : BinaryPrimitives.TryReadUInt64BigEndian(source, out value);

    #endregion

    #region Int128

    /// <summary>
    /// Reads a <see cref="Int128" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Int128" />.
    /// </exception>
    public static Int128 ReadInt128(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadInt128BigEndian(source)
        : BinaryPrimitives.ReadInt128LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="Int128" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Int128" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadInt128(scoped ReadOnlySpan<byte> source, out Int128 value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadInt128LittleEndian(source, out value)
        : BinaryPrimitives.TryReadInt128BigEndian(source, out value);

    #endregion

    #region UInt128

    /// <summary>
    /// Reads a <see cref="UInt128" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="UInt128" />.
    /// </exception>
    public static UInt128 ReadUInt128(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadUInt128BigEndian(source)
        : BinaryPrimitives.ReadUInt128LittleEndian(source);

    /// <summary>
    /// Reads a <see cref="UInt128" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="UInt128" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadUInt128(scoped ReadOnlySpan<byte> source, out UInt128 value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadUInt128LittleEndian(source, out value)
        : BinaryPrimitives.TryReadUInt128BigEndian(source, out value);

    #endregion

    #region IntPtr

    /// <summary>
    /// Reads a <see cref="nint" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 4 bytes on 32-bit platforms -or- 8 bytes on 64-bit platforms from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="nint" />.
    /// </exception>
    public static nint ReadIntPtr(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadIntPtrBigEndian(source)
        : BinaryPrimitives.ReadIntPtrLittleEndian(source);

    /// <summary>
    /// Reads a <see cref="nint" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="nint" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 4 bytes on 32-bit platforms -or- 8 bytes on 64-bit platforms from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadIntPtr(scoped ReadOnlySpan<byte> source, out nint value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadIntPtrLittleEndian(source, out value)
        : BinaryPrimitives.TryReadIntPtrBigEndian(source, out value);

    #endregion

    #region UIntPtr

    /// <summary>
    /// Reads a <see cref="nuint" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 4 bytes on 32-bit platforms -or- 8 bytes on 64-bit platforms from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="nuint" />.
    /// </exception>
    public static nuint ReadUIntPtr(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadUIntPtrBigEndian(source)
        : BinaryPrimitives.ReadUIntPtrLittleEndian(source);

    /// <summary>
    /// Reads a <see cref="nuint" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="nuint" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 4 bytes on 32-bit platforms -or- 8 bytes on 64-bit platforms from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadUIntPtr(scoped ReadOnlySpan<byte> source, out nuint value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadUIntPtrLittleEndian(source, out value)
        : BinaryPrimitives.TryReadUIntPtrBigEndian(source, out value);

    #endregion

    #region Half

    /// <summary>
    /// Reads a <see cref="Half" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Half" />.
    /// </exception>
    public static Half ReadHalf(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadHalfBigEndian(source)
        : BinaryPrimitives.ReadHalfLittleEndian(source);

    /// <summary>
    /// Reads a <see cref="Half" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Half" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 2 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadHalf(scoped ReadOnlySpan<byte> source, out Half value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadHalfLittleEndian(source, out value)
        : BinaryPrimitives.TryReadHalfBigEndian(source, out value);

    #endregion

    #region Single

    /// <summary>
    /// Reads a <see cref="float" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="float" />.
    /// </exception>
    public static float ReadSingle(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadSingleBigEndian(source)
        : BinaryPrimitives.ReadSingleLittleEndian(source);

    /// <summary>
    /// Reads a <see cref="float" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="float" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadSingle(scoped ReadOnlySpan<byte> source, out float value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadSingleLittleEndian(source, out value)
        : BinaryPrimitives.TryReadSingleBigEndian(source, out value);

    #endregion

    #region Double

    /// <summary>
    /// Reads a <see cref="double" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="double" />.
    /// </exception>
    public static double ReadDouble(scoped ReadOnlySpan<byte> source)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.ReadDoubleBigEndian(source)
        : BinaryPrimitives.ReadDoubleLittleEndian(source);

    /// <summary>
    /// Reads a <see cref="double" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="double" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadDouble(scoped ReadOnlySpan<byte> source, out double value)
        => TEndian.IsLittleEndian
        ? BinaryPrimitives.TryReadDoubleLittleEndian(source, out value)
        : BinaryPrimitives.TryReadDoubleBigEndian(source, out value);

    #endregion

    #region Vector2

    /// <summary>
    /// Reads a <see cref="Vector2" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Vector2" />.
    /// </exception>
    public static Vector2 ReadVector2(scoped ReadOnlySpan<byte> source)
        => MemoryMarshal.Read<Vector2<TEndian>>(source).Get();

    /// <summary>
    /// Reads a <see cref="Vector2" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Vector2" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadVector2(scoped ReadOnlySpan<byte> source, out Vector2 value)
    {
        bool result = MemoryMarshal.TryRead(source, out Vector2<TEndian> raw);
        value = raw.Get();
        return result;
    }

    #endregion

    #region Vector3

    /// <summary>
    /// Reads a <see cref="Vector3" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 12 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Vector3" />.
    /// </exception>
    public static Vector3 ReadVector3(scoped ReadOnlySpan<byte> source)
        => MemoryMarshal.Read<Vector3<TEndian>>(source).Get();

    /// <summary>
    /// Reads a <see cref="Vector3" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Vector3" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 12 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadVector3(scoped ReadOnlySpan<byte> source, out Vector3 value)
    {
        bool result = MemoryMarshal.TryRead(source, out Vector3<TEndian> raw);
        value = raw.Get();
        return result;
    }

    #endregion

    #region Vector4

    /// <summary>
    /// Reads a <see cref="Vector4" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Vector4" />.
    /// </exception>
    public static Vector4 ReadVector4(scoped ReadOnlySpan<byte> source)
        => MemoryMarshal.Read<Vector4<TEndian>>(source).Get();

    /// <summary>
    /// Reads a <see cref="Vector4" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Vector4" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadVector4(scoped ReadOnlySpan<byte> source, out Vector4 value)
    {
        bool result = MemoryMarshal.TryRead(source, out Vector4<TEndian> raw);
        value = raw.Get();
        return result;
    }

    #endregion

    #region Quaternion

    /// <summary>
    /// Reads a <see cref="Quaternion" /> from the beginning of a read-only span of bytes, <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span to read.</param>
    /// <returns>The <typeparamref name="TEndian"/> value.</returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="source"/> is too small to contain a <see cref="Quaternion" />.
    /// </exception>
    public static Quaternion ReadQuaternion(scoped ReadOnlySpan<byte> source)
        => MemoryMarshal.Read<Quaternion<TEndian>>(source).Get();

    /// <summary>
    /// Reads a <see cref="Quaternion" /> from the beginning of a read-only span of bytes, as <typeparamref name="TEndian"/>.
    /// </summary>
    /// <param name="source">The read-only span of bytes to read.</param>
    /// <param name="value">When this method returns, contains the value read out of the read-only span of bytes, as <typeparamref name="TEndian"/>.</param>
    /// <returns>
    /// <see langword="true" /> if the span is large enough to contain a <see cref="Quaternion" />; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>Reads exactly 16 bytes from the beginning of the span.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryReadQuaternion(scoped ReadOnlySpan<byte> source, out Quaternion value)
    {
        bool result = MemoryMarshal.TryRead(source, out Quaternion<TEndian> raw);
        value = raw.Get();
        return result;
    }

    #endregion
}
