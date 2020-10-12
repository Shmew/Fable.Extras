# Shared Array Buffer

Used to represent a generic, fixed-length raw binary data buffer, 
similar to the [ArrayBuffer](/extras/array-buffer) object, but in a way that they can be 
used to create views on shared memory. 

Unlike an [ArrayBuffer](/extras/array-buffer), a `SharedArrayBuffer` cannot become detached.

The session must be in a secure context to use this construct.
You can validate this by calling [isSecureContext](/extras/globals#issecurecontext).

```fsharp
type SharedArrayBuffer (byteLength: int) =
    /// The read-only size, in bytes, of the ArrayBuffer.
    member ByteLength: int
        
    /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
    /// inclusive, up to end, exclusive.
    member Slice (?begin': int, ?end': int) : ArrayBuffer

    interface JS.ArrayBuffer
```
