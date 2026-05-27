using ByteOrder.Binary;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a vector with three single-precision floating-point values of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Vector2<TEndian> :
    IEndianConverter<Vector2>,
    IEquatable<Vector2<TEndian>>,
    IEquatable<Vector2>,
    IFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly Vector2 m_value;

    /// <summary>
    /// Creates a new <see cref="Vector2{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2(Vector2 value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Swap(Vector2 source)
    {
        if (!TEndian.IsNativeEndian)
        {
            ref int x = ref Unsafe.As<Vector2, int>(ref source);
            ref int y = ref Unsafe.Add(ref x, sizeof(int));
            x = BinaryPrimitives.ReverseEndianness(x);
            y = BinaryPrimitives.ReverseEndianness(y);
            return source;
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<Vector2> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector2, float>(destination));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<Vector2> source, Span<Vector2> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector2, float>(source), MemoryMarshal.Cast<Vector2, float>(destination));

    #endregion

    #region Operators Vector2<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector2<TEndian> left, Vector2<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector2<TEndian> left, Vector2<TEndian> right)
        => left.m_value != right.m_value;

    #endregion

    #region Operators Vector2<TEndian> - Vector2

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector2<TEndian>(Vector2 value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector2<TEndian> left, Vector2 right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector2<TEndian> left, Vector2 right)
        => left.Get() != right;

    #endregion

    #region Operators Vector2 - Vector2<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector2(Vector2<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector2 left, Vector2<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector2 left, Vector2<TEndian> right)
        => left != right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector2 Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector2 ToVector2()
        => Get();

    /// <inheritdoc cref="Vector2{TEndian}(Vector2)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TEndian> FromVector2(Vector2 value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="Vector2.Equals(Vector2)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector2<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="Vector2.Equals(Vector2)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector2 other)
        => Get() == other;

    #endregion

    #region IFormattable

    /// <inheritdoc cref="Vector2.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="Vector2.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    #endregion

    #region Object

    /// <inheritdoc cref="Vector2.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Vector2<TEndian> other && Equals(other);

    /// <inheritdoc cref="Vector2.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="Vector2.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
