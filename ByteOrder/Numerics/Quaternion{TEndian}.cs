using ByteOrder.Binary;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace ByteOrder.Numerics;

/// <summary>
/// Represents a vector that is used to encode three-dimensional physical rotations with the endianness <typeparamref name="TEndian"/>.
/// </summary>
/// <typeparam name="TEndian">The endianness of the type.</typeparam>
[DebuggerDisplay("{Get()}")]
[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Quaternion<TEndian> :
    IEndianConverter<Quaternion>,
    IEquatable<Quaternion<TEndian>>,
    IEquatable<Quaternion>
    where TEndian : struct, IEndian, allows ref struct
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    private readonly Quaternion m_value;

    /// <summary>
    /// Creates a new <see cref="Quaternion{TEndian}"/> from the specified value, swapping byte order if necessary.
    /// </summary>
    /// <param name="value">The value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quaternion(Quaternion value)
    {
        m_value = Swap(value);
    }

    #region Swap

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Swap(Quaternion source)
    {
        if (!TEndian.IsNativeEndian)
        {
            ref byte byteSrc = ref Unsafe.As<Quaternion, byte>(ref source);
            Vector128<byte> vector = Unsafe.ReadUnaligned<Vector128<byte>>(ref byteSrc);
            Vector128<byte> mask = Vector128.Create((byte)
                 3, 2, 1, 0,
                 7, 6, 5, 4,
                11, 10, 9, 8,
                15, 14, 13, 12
            );

            Vector128<byte> shuffle = Vector128.Shuffle(vector, mask);
            return Vector128.AsQuaternion(shuffle.AsSingle());
        }
        else
        {
            return source;
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(Span<Quaternion> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Quaternion, float>(destination));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ReadOnlySpan<Quaternion> source, Span<Quaternion> destination)
        => Single<TEndian>.Swap(MemoryMarshal.Cast<Quaternion, float>(source), MemoryMarshal.Cast<Quaternion, float>(destination));

    #endregion

    #region Operators Quaternion<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Quaternion<TEndian> left, Quaternion<TEndian> right)
        => left.m_value == right.m_value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Quaternion<TEndian> left, Quaternion<TEndian> right)
        => left.m_value != right.m_value;

    #endregion

    #region Operators Quaternion<TEndian> - Quaternion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Quaternion<TEndian>(Quaternion value)
        => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Quaternion<TEndian> left, Quaternion right)
        => left.Get() == right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Quaternion<TEndian> left, Quaternion right)
        => left.Get() != right;

    #endregion

    #region Operators Quaternion - Quaternion<TEndian>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Quaternion(Quaternion<TEndian> source)
        => source.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Quaternion left, Quaternion<TEndian> right)
        => left == right.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Quaternion left, Quaternion<TEndian> right)
        => left != right.Get();

    #endregion

    #region Explicit

    /// <summary>
    /// Gets the underlying value as the native endianness through a swap if necessary.
    /// </summary>
    /// <returns>The underlying value as the native endianness.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Quaternion Get()
        => Swap(m_value);

    /// <inheritdoc cref="Get()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Quaternion ToQuaternion()
        => Get();

    /// <inheritdoc cref="Quaternion{TEndian}(Quaternion)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<TEndian> FromQuaternion(Quaternion value)
        => new(value);

    #endregion

    #region IEquatable

    /// <inheritdoc cref="Quaternion.Equals(Quaternion)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Quaternion<TEndian> other)
        => m_value == other.m_value;

    /// <inheritdoc cref="Quaternion.Equals(Quaternion)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Quaternion other)
        => Get() == other;

    #endregion

    #region Object

    /// <inheritdoc cref="Quaternion.Equals(object?)" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj is Quaternion<TEndian> other && Equals(other);

    /// <inheritdoc cref="Quaternion.ToString()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Get().ToString();

    /// <inheritdoc cref="Quaternion.GetHashCode()" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => m_value.GetHashCode();

    #endregion
}
