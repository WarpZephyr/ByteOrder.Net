using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a 32-bit signed integer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
/// <param name="value">The value to set.</param>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Int32<TEndian> :
    IEndianConverter<int>,
    IEquatable<Int32<TEndian>>,
    IComparable<Int32<TEndian>>,
    IEquatable<int>,
    IComparable<int>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly int m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="Int32{TEndian}"/>.
    /// </summary>
    public static readonly Int32<TEndian> MaxValue =
        new(int.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="Int32{TEndian}"/>.
    /// </summary>
    public static readonly Int32<TEndian> MinValue =
        new(int.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly int Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="Int32{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32(int value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Swap(int source)
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
    public static void Swap(Span<int> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<int> source, Span<int> destination)
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

    #region Operators Int32<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int32<TEndian> left, Int32<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int32<TEndian> left, Int32<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Int32<TEndian> left, Int32<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Int32<TEndian> left, Int32<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Int32<TEndian> left, Int32<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Int32<TEndian> left, Int32<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators Int32<TEndian> - Int32

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Int32<TEndian>(int value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int32<TEndian> left, int right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int32<TEndian> left, int right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Int32<TEndian> left, int right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Int32<TEndian> left, int right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Int32<TEndian> left, int right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Int32<TEndian> left, int right)
        => left.Get() >= right;

    #endregion

    #region Operators Int32 - Int32<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(Int32<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int left, Int32<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int left, Int32<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int left, Int32<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int left, Int32<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int left, Int32<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int left, Int32<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ToInt32()
        => Get();

    /// <inheritdoc cref="Int32{TEndian}(int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32<TEndian> FromInt32(int value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="int.Equals(int)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int32<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="int.Equals(int)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="int.CompareTo(int)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Int32<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="int.CompareTo(int)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(int other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="int.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Int32<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="int.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="int.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="int.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="int.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="int.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Int32<TEndian> other && Equals(other);

    /// <inheritdoc cref="int.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="int.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        // The value of the lower 32 bits XORed with the uppper 32 bits.
        => unchecked((int)((int)m_value)) ^ (int)(m_value >> 32);

    #endregion
}
