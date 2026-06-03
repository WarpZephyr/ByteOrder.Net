using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a 64-bit signed integer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
/// <param name="value">The value to set.</param>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Int64<TEndian> :
    IEndianConverter<long>,
    IEquatable<Int64<TEndian>>,
    IComparable<Int64<TEndian>>,
    IEquatable<long>,
    IComparable<long>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly long m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="Int64{TEndian}"/>.
    /// </summary>
    public static readonly Int64<TEndian> MaxValue =
        new(long.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="Int64{TEndian}"/>.
    /// </summary>
    public static readonly Int64<TEndian> MinValue =
        new(long.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly long Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="Int64{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64(long value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Swap(long source)
    {
        if (!TEndian.IsNativeEndian)
        {
            return BinaryPrimitives.ReverseEndianness(source);
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<long> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<long> source, Span<long> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(source, destination);
        }
        else
        {
            source.CopyTo(destination);
        }
    }

    #endregion

    #region Operators Int64<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int64<TEndian> left, Int64<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int64<TEndian> left, Int64<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Int64<TEndian> left, Int64<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Int64<TEndian> left, Int64<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Int64<TEndian> left, Int64<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Int64<TEndian> left, Int64<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators Int64<TEndian> - Int64

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Int64<TEndian>(long value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int64<TEndian> left, long right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int64<TEndian> left, long right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Int64<TEndian> left, long right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Int64<TEndian> left, long right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Int64<TEndian> left, long right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Int64<TEndian> left, long right)
        => left.Get() >= right;

    #endregion

    #region Operators Int64 - Int64<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator long(Int64<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(long left, Int64<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(long left, Int64<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(long left, Int64<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(long left, Int64<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(long left, Int64<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(long left, Int64<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long ToInt64()
        => Get();

    /// <inheritdoc cref="Int64{TEndian}(long)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64<TEndian> FromInt64(long value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="long.Equals(long)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int64<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="long.Equals(long)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(long other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="long.CompareTo(long)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Int64<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="long.CompareTo(long)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(long other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="long.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Int64<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="long.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="long.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="long.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="long.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="long.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Int64<TEndian> other && Equals(other);

    /// <inheritdoc cref="long.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="long.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        // The value of the lower 32 bits XORed with the uppper 32 bits.
        => unchecked((int)((long)m_value)) ^ (int)(m_value >> 32);

    #endregion
}
