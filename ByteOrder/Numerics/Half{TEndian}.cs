using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a half-precision floating-point number of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Half<TEndian> :
    IEndianConverter<Half>,
    IEquatable<Half<TEndian>>,
    IComparable<Half<TEndian>>,
    IEquatable<Half>,
    IComparable<Half>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly Half m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="Half{TEndian}"/>.
    /// </summary>
    public static readonly Half<TEndian> MaxValue =
        new(Half.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="Half{TEndian}"/>.
    /// </summary>
    public static readonly Half<TEndian> MinValue =
        new(Half.MinValue);

    /// <summary>
    /// Creates a new <see cref="Half{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Half(Half value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Swap(Half source)
    {
        if (!TEndian.IsNativeEndian)
        {
            var value = BinaryPrimitives.ReverseEndianness(Unsafe.As<Half, short>(ref source));
            return Unsafe.As<short, Half>(ref value);
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<Half> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var cast = MemoryMarshal.Cast<Half, short>(destination);
            BinaryPrimitives.ReverseEndianness(cast, cast);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<Half> source, Span<Half> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var sourceCast = MemoryMarshal.Cast<Half, short>(source);
            var destinationCast = MemoryMarshal.Cast<Half, short>(destination);
            BinaryPrimitives.ReverseEndianness(sourceCast, destinationCast);
        }
        else
        {
            source.CopyTo(destination);
        }
    }

    #endregion

    #region Operators Half<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Half<TEndian> left, Half<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Half<TEndian> left, Half<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Half<TEndian> left, Half<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Half<TEndian> left, Half<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Half<TEndian> left, Half<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Half<TEndian> left, Half<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators Half<TEndian> - Half

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Half<TEndian>(Half value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Half<TEndian> left, Half right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Half<TEndian> left, Half right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Half<TEndian> left, Half right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Half<TEndian> left, Half right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Half<TEndian> left, Half right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Half<TEndian> left, Half right)
        => left.Get() >= right;

    #endregion

    #region Operators Half - Half<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Half(Half<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Half left, Half<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Half left, Half<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Half left, Half<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Half left, Half<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Half left, Half<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Half left, Half<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Half Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Half ToHalf()
        => Get();

    /// <inheritdoc cref="Half{TEndian}(Half)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half<TEndian> FromHalf(Half value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="Half.Equals(Half)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Half<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="Half.Equals(Half)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Half other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="Half.CompareTo(Half)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Half<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="Half.CompareTo(Half)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Half other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="Half.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Half<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="Half.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="Half.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="Half.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="Half.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="Half.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Half<TEndian> other && Equals(other);

    /// <inheritdoc cref="Half.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="Half.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
