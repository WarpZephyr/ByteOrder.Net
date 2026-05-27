namespace ByteOrder.Binary;

/// <summary>
/// An interface representing endianness.
/// </summary>
public interface IEndian
{
    /// <summary>
    /// Whether or not the byte order is little-endian.
    /// </summary>
    public static abstract bool IsLittleEndian { get; }

    /// <summary>
    /// Whether or not the byte order is big-endian.
    /// </summary>
    public static abstract bool IsBigEndian { get; }

    /// <summary>
    /// Whether or not the byte order is the native endianness.
    /// </summary>
    public static abstract bool IsNativeEndian { get; }
}
