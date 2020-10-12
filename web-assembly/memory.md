# Memory

## Wasm Pages

Units of WebAssembly pages when configurating WebAssembly Memory objects.

```fsharp
[<Measure>]
type WasmPages
```

There is also a `WasmPages` module that exposes the below functions:

### asBytes

Converts wasm pages into the number of bytes used.

Signature:
```fsharp
(value: int<WasmPages>) -> int
```

## Memory Descriptor

A configuration object for WebAssembly Memory instantiation.

```fsharp
type MemoryDescriptor =
    new (initial: int<WasmPages>)
    new (initial: int<WasmPages>, maximum: int<WasmPages>)
    new (initial: int<WasmPages>, maximum: int<WasmPages>, shared: bool)
    new (initial: int<WasmPages>, shared: bool)

    /// The initial size of the WebAssembly Memory, in units of WebAssembly pages.
    member Initial : int<WasmPages>

    /// The maximum size the WebAssembly Memory is allowed to grow to, in units of 
    /// WebAssembly pages.
    ///
    /// When present, the maximum parameter acts as a hint to the engine to reserve 
    /// memory up front. However, the engine may ignore or clamp this reservation 
    /// request. Unshared WebAssembly memories don't need to set a maximum, but 
    /// shared memories do.
    member Maximum : int<WasmPages> option

    /// A boolean value that defines whether the memory is a shared memory or not. 
    ///
    /// If set to true, it is a shared memory. 
    ///
    /// The default is false.
    member Shared : bool option
```

The functions outlined below are located in the `MemoryDescriptor` module.

### initial

The initial size of the WebAssembly Memory, in units of WebAssembly pages.

Signature:
```fsharp
(md: MemoryDescriptor) -> int<WasmPages>
```

### maximum

The maximum size the WebAssembly Memory is allowed to grow to, in units of WebAssembly pages.

When present, the maximum parameter acts as a hint to the engine to reserve memory up front. 
However, the engine may ignore or clamp this reservation request. Unshared WebAssembly 
memories don't need to set a maximum, but shared memories do.

Signature:
```fsharp
(md: MemoryDescriptor) -> int<WasmPages> option
```

## shared

A boolean value that defines whether the memory is a shared memory or not. 

If set to true, it is a shared memory. 

The default is false.

Signature:
```fsharp
(md: MemoryDescriptor) -> bool option
```

## Memory

Creates a new `Memory` object whose buffer property is a resizable [ArrayBuffer](/extras/array-buffer) or [SharedArrayBuffer](/atomics/shared-array-buffer)
that holds the raw bytes of memory accessed by a WebAssembly Instance.

A Memory created by Javascript or in WebAssembly code will be accessible and mutable from both 
Javascript and WebAssembly.

```fsharp
type Memory (descriptor: MemoryDescriptor) =
    /// The buffer contained in the memory.
    member Buffer : JS.ArrayBuffer

    /// Increases the size of the memory instance by a specified number of WebAssembly pages.
    ///
    /// Returns the previous size of the memory in units of WebAssembly pages.
    member Grow (delta: int<WasmPages>) : int<WasmPages>
```

The functions outlined below are located in the `Memory` module.

### buffer

The buffer contained in the memory.

Signature:
```fsharp
(md: Memory) -> ArrayBuffer
```

### grow

Increases the size of the memory instance by a specified number of WebAssembly pages.

Returns the previous size of the memory in units of WebAssembly pages.

Signature:
```fsharp
(delta: int<WasmPages>) -> (md: Memory) -> Memory
```
