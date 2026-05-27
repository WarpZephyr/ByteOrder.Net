using ByteOrder.Binary;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a vector with three single-precision floating-point values of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
/// <param name="value">The value to set.</param>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Vector3<TEndian> :
    IEquatable<Vector3<TEndian>>,
    IEquatable<Vector3>,
    IFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly Vector3 m_value;

    /// <summary>
    /// Creates a new <see cref="Vector3{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3(Vector3 value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Swap(Vector3 source)
    {
        if (BitConverter.IsLittleEndian)
        {
            Vector128<byte> vector = Vector128.AsVector128Unsafe(source).AsByte();
            Vector128<byte> mask = Vector128.Create((byte)
                 3, 2, 1, 0,
                 7, 6, 5, 4,
                11, 10, 9, 8,
                 0, 0, 0, 0
            );

            Vector128<byte> shuffle = Vector128.Shuffle(vector, mask);
            return Vector128.AsVector3(shuffle.AsSingle());
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<Vector3> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector3, float>(destination));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<Vector3> source, Span<Vector3> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector3, float>(source), MemoryMarshal.Cast<Vector3, float>(destination));


    #endregion

    #region Operators Vector3<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector3<TEndian> left, Vector3<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector3<TEndian> left, Vector3<TEndian> right)
        => left.m_value != right.m_value;

    #endregion

    #region Operators Vector3<TEndian> - Vector3

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector3<TEndian>(Vector3 value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector3<TEndian> left, Vector3 right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector3<TEndian> left, Vector3 right)
        => left.Get() != right;

    #endregion

    #region Operators Vector3 - Vector3<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector3(Vector3<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector3 left, Vector3<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector3 left, Vector3<TEndian> right)
        => left != right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector3 Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector3 ToVector3()
        => Get();

    /// <inheritdoc cref="Vector3{TEndian}(Vector3)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<TEndian> FromVector3(Vector3 value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="Vector3.Equals(Vector3)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector3<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="Vector3.Equals(Vector3)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector3 other)
        => Get() == other;

    #endregion

    #region IFormattable

    /// <inheritdoc cref="Vector3.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="Vector3.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    #endregion

    #region Object

    /// <inheritdoc cref="Vector3.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Vector3<TEndian> other && Equals(other);

    /// <inheritdoc cref="Vector3.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="Vector3.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
