using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents an unsigned integer where the bit-width is the same as a pointer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]

public readonly struct UIntPtr<TEndian> :
    IEndianConverter<nuint>,
    IEquatable<UIntPtr<TEndian>>,
    IComparable<UIntPtr<TEndian>>,
    IEquatable<nuint>,
    IComparable<nuint>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly nuint m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="UIntPtr{TEndian}"/>.
    /// </summary>
    public static readonly UIntPtr<TEndian> MaxValue =
        new(nuint.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="UIntPtr{TEndian}"/>.
    /// </summary>
    public static readonly UIntPtr<TEndian> MinValue =
        new(nuint.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly nuint Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="UIntPtr{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UIntPtr(nuint value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint Swap(nuint source)
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
    public static void Swap(Span<nuint> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<nuint> source, Span<nuint> destination)
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

    #region Operators UIntPtr<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UIntPtr<TEndian> left, UIntPtr<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators UIntPtr<TEndian> - UIntPtr

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UIntPtr<TEndian>(nuint value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UIntPtr<TEndian> left, nuint right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UIntPtr<TEndian> left, nuint right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UIntPtr<TEndian> left, nuint right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UIntPtr<TEndian> left, nuint right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UIntPtr<TEndian> left, nuint right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UIntPtr<TEndian> left, nuint right)
        => left.Get() >= right;

    #endregion

    #region Operators UIntPtr - UIntPtr<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nuint(UIntPtr<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(nuint left, UIntPtr<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(nuint left, UIntPtr<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(nuint left, UIntPtr<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(nuint left, UIntPtr<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(nuint left, UIntPtr<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(nuint left, UIntPtr<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly nuint Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly nuint ToUIntPtr()
        => Get();

    /// <inheritdoc cref="UIntPtr{TEndian}(nuint)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UIntPtr<TEndian> FromUIntPtr(nuint value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="nuint.Equals(nuint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UIntPtr<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="nuint.Equals(nuint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(nuint other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="nuint.CompareTo(nuint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UIntPtr<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="nuint.CompareTo(nuint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(nuint other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="nuint.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is UIntPtr<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="nuint.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="nuint.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="nuint.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="nuint.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="nuint.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is UIntPtr<TEndian> other && Equals(other);

    /// <inheritdoc cref="nuint.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="nuint.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        // The value of the lower 32 bits XORed with the uppper 32 bits.
        => ((int)m_value) ^ (int)(m_value >> 32);

    #endregion
}
