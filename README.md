# ByteOrder.Net
High-performance endianness views and converters for .NET.

## Features
* **Zero-Cost Abstractions**: Leveraging C# 13 `static abstract` and generic specialization.
* **AOT Ready**: Fully compatible with NativeAOT and trimming.
* **SIMD Optimized**: Hardware-accelerated batch swaps for common numeric types.

## Usage

### Views
Use wrappers to ensure a value is always interpreted with a specific endianness, or have a generic endianness.

```csharp
// Construct a big endian Int32 from an int in native endianness
// The endianness will be swapped on construction if the target endianness does not match
var bigInt = new Int32<BigEndian>(42);

// Access the native value
// Zero-cost on BigEndian systems
// One swap on LittleEndian systems
// The swap may even get compiled out to instructions such as `movbe` depending on the code.
int native = bigInt.Get(); 

// Or use it like a regular primitive
Console.WriteLine($"Value: {bigInt}");
```

```csharp
static void PrintValue<TEndian>(Int32<TEndian> value) where TEndian : struct, IEndian
{
    // Access the native value
    int native = value.Get(); 

    // Or use it like a regular primitive
    Console.WriteLine($"Value: {value}");
}
```

```csharp
// Create structs supporting generic endianness
// At only the cost of possibly swapping a field when accessing it
public struct FileHeader<TEndian> where TEndian : struct, IEndian
{
    public Int32<TEndian> Version;
    public Int32<TEndian> DataOffset;
    public Int32<TEndian> DataLength;
}
```

### Batch Conversion
Uses the `IEndianConverter<T>` interface for SIMD-accelerated batch processing of raw data.  
Each primitive implements it for their native type so they may be accepted as converters generically.

```csharp
// Pack an array of native floats into BigEndian wrappers
ReadOnlySpan<float> nativeFloats = [1.0f, 2.0f, 3.0f, 4.0f];
Span<Single<BigEndian>> bigFloats = stackalloc Single<BigEndian>[4];

// Uses SIMD shuffles under the hood where possible
Single<BigEndian>.Swap(nativeFloats, MemoryMarshal.Cast<Single<BigEndian>, float>(bigFloats));
```

```csharp
void Write<T, TEndianConverter>(ReadOnlySpan<T> values)
    where T : unmanaged
    where TEndianConverter : struct, IEndianConverter<T>
{
    // Swap into a buffer with generics
    // The explicit type is not required
    // Uses SIMD shuffles under the hood where possible
    TEndianConverter.Swap(values, MemoryMarshal.Cast<byte, T>(_buffer));
}
```

## License
See the [LICENSE](LICENSE) file for details.
