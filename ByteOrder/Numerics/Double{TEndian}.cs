using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a double-precision floating-point number of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Double<TEndian> :
    IEndianConverter<double>,
    IEquatable<Double<TEndian>>,
    IComparable<Double<TEndian>>,
    IEquatable<double>,
    IComparable<double>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly double m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="Double{TEndian}"/>.
    /// </summary>
    public static readonly Double<TEndian> MaxValue =
        new(double.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="Double{TEndian}"/>.
    /// </summary>
    public static readonly Double<TEndian> MinValue =
        new(double.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly double Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="Double{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Double(double value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Swap(double source)
    {
        if (!TEndian.IsNativeEndian)
        {
            var value = BinaryPrimitives.ReverseEndianness(Unsafe.As<double, long>(ref source));
            return Unsafe.As<long, double>(ref value);
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<double> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var cast = MemoryMarshal.Cast<double, long>(destination);
            BinaryPrimitives.ReverseEndianness(cast, cast);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<double> source, Span<double> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var sourceCast = MemoryMarshal.Cast<double, long>(source);
            var destinationCast = MemoryMarshal.Cast<double, long>(destination);
            BinaryPrimitives.ReverseEndianness(sourceCast, destinationCast);
        }
        else
        {
            source.CopyTo(destination);
        }
    }

    #endregion

    #region Operators Double<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double<TEndian> left, Double<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double<TEndian> left, Double<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Double<TEndian> left, Double<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Double<TEndian> left, Double<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Double<TEndian> left, Double<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Double<TEndian> left, Double<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators Double<TEndian> - Double

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Double<TEndian>(double value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double<TEndian> left, double right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double<TEndian> left, double right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Double<TEndian> left, double right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Double<TEndian> left, double right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Double<TEndian> left, double right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Double<TEndian> left, double right)
        => left.Get() >= right;

    #endregion

    #region Operators Double - Double<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(Double<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double left, Double<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double left, Double<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double left, Double<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double left, Double<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double left, Double<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double left, Double<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double ToDouble()
        => Get();

    /// <inheritdoc cref="Double{TEndian}(double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double<TEndian> FromDouble(double value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="double.Equals(double)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Double<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="double.Equals(double)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(double other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="double.CompareTo(double)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Double<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="double.CompareTo(double)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(double other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="double.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Double<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="double.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="double.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="double.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="double.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="double.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Double<TEndian> other && Equals(other);

    /// <inheritdoc cref="double.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="double.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
