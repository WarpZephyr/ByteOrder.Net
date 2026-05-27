using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a single-precision floating-point number of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Single<TEndian> :
    IEndianConverter<float>,
    IEquatable<Single<TEndian>>,
    IComparable<Single<TEndian>>,
    IEquatable<float>,
    IComparable<float>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly float m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="Single{TEndian}"/>.
    /// </summary>
    public static readonly Single<TEndian> MaxValue =
        new(float.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="Single{TEndian}"/>.
    /// </summary>
    public static readonly Single<TEndian> MinValue =
        new(float.MinValue);

    /// <summary>
    /// Creates a new <see cref="Single{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Single(float value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Swap(float source)
    {
        if (!TEndian.IsNativeEndian)
        {
            var value = BinaryPrimitives.ReverseEndianness(Unsafe.As<float, int>(ref source));
            return Unsafe.As<int, float>(ref value);
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<float> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var cast = MemoryMarshal.Cast<float, int>(destination);
            BinaryPrimitives.ReverseEndianness(cast, cast);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<float> source, Span<float> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            var sourceCast = MemoryMarshal.Cast<float, int>(source);
            var destinationCast = MemoryMarshal.Cast<float, int>(destination);
            BinaryPrimitives.ReverseEndianness(sourceCast, destinationCast);
        }
        else
        {
            source.CopyTo(destination);
        }
    }

    #endregion

    #region Operators Single<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Single<TEndian> left, Single<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Single<TEndian> left, Single<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Single<TEndian> left, Single<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Single<TEndian> left, Single<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Single<TEndian> left, Single<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Single<TEndian> left, Single<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators Single<TEndian> - Single

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Single<TEndian>(float value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Single<TEndian> left, float right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Single<TEndian> left, float right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Single<TEndian> left, float right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Single<TEndian> left, float right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Single<TEndian> left, float right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Single<TEndian> left, float right)
        => left.Get() >= right;

    #endregion

    #region Operators Single - Single<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(Single<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float left, Single<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float left, Single<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(float left, Single<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(float left, Single<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(float left, Single<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(float left, Single<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float ToSingle()
        => Get();

    /// <inheritdoc cref="Single{TEndian}(float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Single<TEndian> FromSingle(float value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="float.Equals(float)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Single<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="float.Equals(float)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(float other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="float.CompareTo(float)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Single<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="float.CompareTo(float)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(float other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="float.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Single<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="float.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="float.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="float.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="float.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="float.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Single<TEndian> other && Equals(other);

    /// <inheritdoc cref="float.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="float.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
