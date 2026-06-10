using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a 16-bit unsigned integer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
/// <param name="value">The value to set.</param>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct UInt16<TEndian> :
    IEndianConverter<ushort>,
    IEquatable<UInt16<TEndian>>,
    IComparable<UInt16<TEndian>>,
    IEquatable<ushort>,
    IComparable<ushort>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly ushort m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="UInt16{TEndian}"/>.
    /// </summary>
    public static readonly UInt16<TEndian> MaxValue =
        new(ushort.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="UInt16{TEndian}"/>.
    /// </summary>
    public static readonly UInt16<TEndian> MinValue =
        new(ushort.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly ushort Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="UInt16{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt16(ushort value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Swap(ushort source)
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
    public static void Swap(Span<ushort> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<ushort> source, Span<ushort> destination)
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

    #region Operators UInt16<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt16<TEndian> left, UInt16<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators UInt16<TEndian> - UInt16

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt16<TEndian>(ushort value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt16<TEndian> left, ushort right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt16<TEndian> left, ushort right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt16<TEndian> left, ushort right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt16<TEndian> left, ushort right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt16<TEndian> left, ushort right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt16<TEndian> left, ushort right)
        => left.Get() >= right;

    #endregion

    #region Operators UInt16 - UInt16<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ushort(UInt16<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ushort left, UInt16<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ushort left, UInt16<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(ushort left, UInt16<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(ushort left, UInt16<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(ushort left, UInt16<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(ushort left, UInt16<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort ToUInt16()
        => Get();

    /// <inheritdoc cref="UInt16{TEndian}(ushort)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt16<TEndian> FromUInt16(ushort value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="ushort.Equals(ushort)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt16<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="ushort.Equals(ushort)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ushort other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="ushort.CompareTo(ushort)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UInt16<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="ushort.CompareTo(ushort)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(ushort other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="ushort.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is UInt16<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="ushort.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="ushort.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="ushort.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="ushort.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="ushort.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is UInt16<TEndian> other && Equals(other);

    /// <inheritdoc cref="ushort.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="ushort.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value;

    #endregion
}
