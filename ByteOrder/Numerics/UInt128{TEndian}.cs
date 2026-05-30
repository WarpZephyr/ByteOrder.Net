using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a 128-bit unsigned integer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
/// <param name="value">The value to set.</param>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct UInt128<TEndian> :
    IEndianConverter<UInt128>,
    IEquatable<UInt128<TEndian>>,
    IComparable<UInt128<TEndian>>,
    IEquatable<UInt128>,
    IComparable<UInt128>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly UInt128 m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="UInt128{TEndian}"/>.
    /// </summary>
    public static readonly UInt128<TEndian> MaxValue =
        new(UInt128.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="UInt128{TEndian}"/>.
    /// </summary>
    public static readonly UInt128<TEndian> MinValue =
        new(UInt128.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly UInt128 Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="UInt128{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt128(UInt128 value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Swap(UInt128 source)
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
    public static void Swap(Span<UInt128> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<UInt128> source, Span<UInt128> destination)
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

    #region Operators UInt128<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt128<TEndian> left, UInt128<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators UInt128<TEndian> - UInt128

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt128<TEndian>(UInt128 value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt128<TEndian> left, UInt128 right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt128<TEndian> left, UInt128 right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt128<TEndian> left, UInt128 right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt128<TEndian> left, UInt128 right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt128<TEndian> left, UInt128 right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt128<TEndian> left, UInt128 right)
        => left.Get() >= right;

    #endregion

    #region Operators UInt128 - UInt128<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt128(UInt128<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt128 left, UInt128<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt128 left, UInt128<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt128 left, UInt128<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt128 left, UInt128<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt128 left, UInt128<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt128 left, UInt128<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly UInt128 Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly UInt128 ToUInt128()
        => Get();

    /// <inheritdoc cref="UInt128{TEndian}(UInt128)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128<TEndian> FromUInt128(UInt128 value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="UInt128.Equals(UInt128)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt128<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="UInt128.Equals(UInt128)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt128 other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="UInt128.CompareTo(UInt128)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UInt128<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="UInt128.CompareTo(UInt128)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UInt128 other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="UInt128.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is UInt128<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="UInt128.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="UInt128.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="UInt128.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="UInt128.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="UInt128.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is UInt128<TEndian> other && Equals(other);

    /// <inheritdoc cref="UInt128.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="UInt128.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        // The value of the lower 32 bits XORed with the uppper 32 bits.
        => unchecked((int)((UInt128)m_value)) ^ (int)(m_value >> 32);

    #endregion
}
