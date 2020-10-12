# Data View

Provides a low-level interface for reading and writing multiple number types in a binary 
[ArrayBuffer], without having to care about the platform's endianness.

```fsharp
type DataView (buffer: ArrayBuffer, ?byteOffset: int, ?byteLength: float) =
        new (dv: JS.DataView)

        /// The ArrayBuffer referenced by a DataView at construction time.
        member Buffer: ArrayBuffer

        /// The read-only size, in bytes, of the ArrayBuffer. 
        member ByteLength: int

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        member ByteOffset: int
        
        /// Gets a signed 32-bit float (float) at the specified byte 
        /// offset from the start of the view.
        member GetFloat32 (byteOffset: int, ?littleEndian: bool) : float32
        
        /// Gets a signed 64-bit float (double) at the specified byte 
        /// offset from the start of the view.
        member GetFloat64 (byteOffset: int, ?littleEndian: bool) : float
        
        /// Gets a signed 8-bit integer (byte) at the specified byte 
        /// offset from the start of the view.
        member GetInt8 (byteOffset: int) : sbyte
        
        /// Gets a signed 16-bit integer (short) at the specified byte 
        /// offset from the start of the view.
        member GetInt16 (byteOffset: int, ?littleEndian: bool) : int16
        
        /// Gets a signed 32-bit integer (long) at the specified byte 
        /// offset from the start of the view.
        member GetInt32 (byteOffset: int, ?littleEndian: bool) : int32
        
        /// Gets a signed 64-bit integer (long long) at the specified byte 
        /// offset from the start of the view.
        member GetInt64 (byteOffset: int, ?littleEndian: bool) : int32

        /// Gets an unsigned 8-bit integer (unsigned byte) at the specified byte 
        /// offset from the start of the view.
        member GetUint8 (byteOffset: int) : byte
        
        /// Gets an unsigned 16-bit integer (unsigned short) at the specified byte 
        /// offset from the start of the view.
        member GetUint16 (byteOffset: int, ?littleEndian: bool) : uint16
        
        /// Gets an unsigned 32-bit integer (unsigned long) at the specified byte 
        /// offset from the start of the view.
        member GetUint32 (byteOffset: int, ?littleEndian: bool) : uint32

        /// Gets an unsigned 64-bit integer (unsigned long long) at the specified byte 
        /// offset from the start of the view.
        member GetUint64 (byteOffset: int, ?littleEndian: bool) : uint64
        
        /// Stores a signed 32-bit float (float) value at the specified byte 
        /// offset from the start of the view.
        member SetFloat32 (byteOffset: int, value: float32, ?littleEndian: bool) : unit
        
        /// Stores a signed 64-bit float (double) value at the specified byte 
        /// offset from the start of the view.
        member SetFloat64 (byteOffset: int, value: float, ?littleEndian: bool) : unit
        
        /// Stores a signed 8-bit integer (byte) value at the specified byte 
        /// offset from the start of the view.
        member SetInt8 (byteOffset: int, value: sbyte) : unit
        
        /// Stores a signed 16-bit integer (short) value at the specified byte 
        /// offset from the start of the view.
        member SetInt16 (byteOffset: int, value: int16, ?littleEndian: bool) : unit
        
        /// Stores a signed 32-bit integer (long) value at the specified byte 
        /// offset from the start of the view.
        member SetInt32 (byteOffset: int, value: int32, ?littleEndian: bool) : unit
        
        /// Stores a signed 64-bit integer (long long) value at the specified byte 
        /// offset from the start of the view.
        member SetInt64 (byteOffset: int, value: int64, ?littleEndian: bool) : unit

        /// Stores an unsigned 8-bit integer (unsigned byte) value at the specified byte 
        /// offset from the start of the view.
        member SetUint8 (byteOffset: int, value: byte) : unit
        
        /// Stores an unsigned 16-bit integer (unsigned short) value at the specified byte 
        /// offset from the start of the view.
        member SetUint16 (byteOffset: int, value: uint16, ?littleEndian: bool) : unit
        
        /// Stores an unsigned 32-bit integer (unsigned long) value at the specified byte 
        /// offset from the start of the view.
        member SetUint32 (byteOffset: int, value: uint32, ?littleEndian: bool) : unit
        
        /// Stores an unsigned 64-bit integer (unsigned long long) value at the specified byte 
        /// offset from the start of the view.
        member SetUint64 (byteOffset: int, value: uint64, ?littleEndian: bool) : unit

        interface JS.ArrayBufferView
```

The functions outlined below are located in the `DataView` module.

## buffer

The [ArrayBuffer] referenced by a `DataView` at construction time.

Signature:
```fsharp
(dv: DataView) -> ArrayBuffer
```

## byteLength

The read-only size, in bytes, of the [ArrayBuffer]. 

Signature:
```fsharp
(dv: DataView) -> int
```

## byteOffset

The offset (in bytes) of this view from the start of its [ArrayBuffer].

Signature:
```fsharp
(dv: DataView) -> int
```

## getFloat32

Gets a signed 32-bit float (float) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> float32
```

## getFloat64

Gets a signed 64-bit float (double) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> float64
```

## getInt8

Gets a signed 8-bit integer (byte) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> int8
```

## getInt16

Gets a signed 16-bit integer (short) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> int16
```

## getInt32

Gets a signed 32-bit integer (long) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> int32
```

## getInt64

Gets a signed 64-bit integer (long long) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> int64
```

## getUint8

Gets an unsigned 8-bit integer (unsigned byte) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> uint8
```

## getUint16

Gets an unsigned 16-bit integer (unsigned short) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> uint16
```

## getUint32

Gets an unsigned 32-bit integer (unsigned long) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> uint32
```

## getUint64

Gets an unsigned 64-bit integer (unsigned long long) at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) -> (dv: DataView) -> uint64
```

## setFloat32

Stores a signed 32-bit float (float) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: float32) (dv: DataView) -> DataView
```

## setFloat64

Stores a signed 64-bit float (double) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: float) (dv: DataView) -> DataView
```

## setInt8

Stores a signed 8-bit integer (byte) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: int8) (dv: DataView) -> DataView
```

## setInt16

Stores a signed 16-bit integer (short) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: int16) (dv: DataView) -> DataView
```

## setInt32

Stores a signed 32-bit integer (long) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: int32) (dv: DataView) -> DataView
```

## setInt64

Stores a signed 64-bit integer (long long) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: int64) (dv: DataView) -> DataView
```

## setUint8

Stores an unsigned 8-bit integer (unsigned byte) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: uint8) (dv: DataView) -> DataView
```

## setUint16

Stores an unsigned 16-bit integer (unsigned short) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: uint16) (dv: DataView) -> DataView
```

## setUint32

Stores an unsigned 32-bit integer (unsigned long) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: unint32) (dv: DataView) -> DataView
```

## setUint64

Stores an unsigned 64-bit integer (unsigned long long) value at the specified byte 
offset from the start of the view.

Signature:
```fsharp
(byteOffset: int) (value: uint64) (dv: DataView) -> DataView
```

## LittleEndian

`DataView` getters and setters with little endian set to true.

Has the same setters and getters as listed above with the except of the `int8` and `uint8` variants.

[ArrayBuffer]: /extras/array-buffer