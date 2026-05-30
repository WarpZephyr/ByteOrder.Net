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
/// Represents a vector with four single-precision floating-point values of the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Vector4<TEndian> :
    IEndianConverter<Vector4>,
    IEquatable<Vector4<TEndian>>,
    IEquatable<Vector4>,
    IFormattable
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly Vector4 m_value;

    /// <summary>
    /// Gets the underlying value without any changes.
    /// </summary>
    public readonly Vector4 Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector4{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector4(Vector4 value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Swap(Vector4 source)
    {
        if (!TEndian.IsNativeEndian)
        {
            ref byte byteSrc = ref Unsafe.As<Vector4, byte>(ref source);
            Vector128<byte> vector = Unsafe.ReadUnaligned<Vector128<byte>>(ref byteSrc);
            Vector128<byte> mask = Vector128.Create((byte)
                 3, 2, 1, 0,
                 7, 6, 5, 4,
                11, 10, 9, 8,
                15, 14, 13, 12
            );

            Vector128<byte> shuffle = Vector128.Shuffle(vector, mask);
            return Vector128.AsVector4(shuffle.AsSingle());
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<Vector4> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector4, float>(destination));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<Vector4> source, Span<Vector4> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Vector4, float>(source), MemoryMarshal.Cast<Vector4, float>(destination));

    #endregion

    #region Operators Vector4<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector4<TEndian> left, Vector4<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector4<TEndian> left, Vector4<TEndian> right)
        => left.m_value != right.m_value;

    #endregion

    #region Operators Vector4<TEndian> - Vector4

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector4<TEndian>(Vector4 value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector4<TEndian> left, Vector4 right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector4<TEndian> left, Vector4 right)
        => left.Get() != right;

    #endregion

    #region Operators Vector4 - Vector4<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector4(Vector4<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector4 left, Vector4<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector4 left, Vector4<TEndian> right)
        => left != right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector4 Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vector4 ToVector4()
        => Get();

    /// <inheritdoc cref="Vector4{TEndian}(Vector4)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<TEndian> FromVector4(Vector4 value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="Vector4.Equals(Vector4)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector4<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="Vector4.Equals(Vector4)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector4 other)
        => Get() == other;

    #endregion

    #region IFormattable

    /// <inheritdoc cref="Vector4.ToString(string?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
        => Get().ToString(format);

    /// <inheritdoc cref="Vector4.ToString(string?, IFormatProvider?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
        => Get().ToString(format, formatProvider);

    #endregion

    #region Object

    /// <inheritdoc cref="Vector4.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Vector4<TEndian> other && Equals(other);

    /// <inheritdoc cref="Vector4.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="Vector4.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
