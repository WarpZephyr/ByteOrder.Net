using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a signed integer where the bit-width is the same as a pointer of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]

public readonly struct IntPtr<TEndian> :
    IEndianConverter<nint>,
    IEquatable<IntPtr<TEndian>>,
    IComparable<IntPtr<TEndian>>,
    IEquatable<nint>,
    IComparable<nint>,
    IComparable,
    IFormattable,
    ISpanFormattable,
    IUtf8SpanFormattable
    where TEndian : unmanaged, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly nint m_value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="IntPtr{TEndian}"/>.
    /// </summary>
    public static readonly IntPtr<TEndian> MaxValue =
        new(nint.MaxValue);

    /// <summary>
    /// Represents the smallest possible value of an <see cref="IntPtr{TEndian}"/>.
    /// </summary>
    public static readonly IntPtr<TEndian> MinValue =
        new(nint.MinValue);

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly nint Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="IntPtr{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntPtr(nint value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Swap(nint source)
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
    public static void Swap(Span<nint> destination)
    {
        if (!TEndian.IsNativeEndian)
        {
            BinaryPrimitives.ReverseEndianness(destination, destination);
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<nint> source, Span<nint> destination)
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

    #region Operators IntPtr<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.m_value != right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.Get() < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.Get() > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.Get() <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IntPtr<TEndian> left, IntPtr<TEndian> right)
        => left.Get() >= right.Get();

    #endregion

    #region Operators IntPtr<TEndian> - IntPtr

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IntPtr<TEndian>(nint value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IntPtr<TEndian> left, nint right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IntPtr<TEndian> left, nint right)
        => left.Get() != right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IntPtr<TEndian> left, nint right)
        => left.Get() < right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IntPtr<TEndian> left, nint right)
        => left.Get() > right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IntPtr<TEndian> left, nint right)
        => left.Get() <= right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IntPtr<TEndian> left, nint right)
        => left.Get() >= right;

    #endregion

    #region Operators IntPtr - IntPtr<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nint(IntPtr<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(nint left, IntPtr<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(nint left, IntPtr<TEndian> right)
        => left != right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(nint left, IntPtr<TEndian> right)
        => left < right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(nint left, IntPtr<TEndian> right)
        => left > right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(nint left, IntPtr<TEndian> right)
        => left <= right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(nint left, IntPtr<TEndian> right)
        => left >= right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly nint Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly nint ToIntPtr()
        => Get();

    /// <inheritdoc cref="IntPtr{TEndian}(nint)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr<TEndian> FromIntPtr(nint value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="nint.Equals(nint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IntPtr<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="nint.Equals(nint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(nint other)
        => Get() == other;

    #endregion

    #region IComparable

    /// <inheritdoc cref="nint.CompareTo(nint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(IntPtr<TEndian> other)
        => Get().CompareTo(other.Get());

    /// <inheritdoc cref="nint.CompareTo(nint)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(nint other)
        => Get().CompareTo(other);

    /// <inheritdoc cref="nint.CompareTo(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is IntPtr<TEndian> other)
            return CompareTo(other);

        throw new ArgumentException($"{nameof(Object)} must be of type {GetType().Name}");
    }

    #endregion

    #region IFormattable

    /// <inheritdoc cref="nint.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="nint.ToString(IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? formatProvider)
        => Get().ToString(formatProvider);

    /// <inheritdoc cref="nint.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    /// <inheritdoc cref="nint.TryFormat" />
    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        => Get().TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Object

    /// <inheritdoc cref="nint.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is IntPtr<TEndian> other && Equals(other);

    /// <inheritdoc cref="nint.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="nint.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        // The value of the lower 32 bits XORed with the uppper 32 bits.
        => ((int)m_value) ^ (int)(m_value >> 32);

    #endregion
}
