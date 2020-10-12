# Array Buffer

`ArrayBuffer` is used to represent a generic, fixed-length raw binary data buffer.

```fsharp
type ArrayBuffer (byteLength: int) =
    new (ab: JS.ArrayBuffer)
    new (b: System.Buffer)

    /// The read-only size, in bytes, of the ArrayBuffer.
    member ByteLength: int
        
    /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
    /// inclusive, up to end, exclusive.
    member Slice (?begin': int, ?end': int) : ArrayBuffer
            
    /// Determines whether the passed value is one of the ArrayBuffer views, 
    /// such as typed array objects or a DataView.
    static member isView (arg: obj) : bool
    
    interface JS.ArrayBuffer
```

The functions outlined below are located in the `ArrayBuffer` module.

## byteLength

The read-only size, in bytes, of the `ArrayBuffer`. 

Signature:
```fsharp
(ab: ArrayBuffer) -> int
```

## isView

Determines whether the passed value is one of the `ArrayBuffer` views, 
such as [typed array objects](/extras/typed-array) or a [DataView](/extras/data-view).

Signature:
```fsharp
(maybeView: obj) -> bool
```

## slice

Returns a new `ArrayBuffer` whose contents are a copy of this `ArrayBuffer`'s bytes from beginning index, 
inclusive, up to end index, exclusive.

Signature:
```fsharp
(begin': int) -> (end': int) -> (ab: ArrayBuffer) -> ArrayBuffer
```

## sliceBegin

Returns a new `ArrayBuffer` whose contents are a copy of this `ArrayBuffer`'s bytes from the 
given index, inclusive to the end of the `ArrayBuffer`.

Signature:
```fsharp
(begin': int) -> (ab: ArrayBuffer) -> ArrayBuffer
```

## sliceEnd

Returns a new `ArrayBuffer` whose contents are a copy of this `ArrayBuffer`'s bytes from the beginning of 
the `ArrayBuffer` to the given index, exclusive.

Signature:
```fsharp
(end': int) -> (ab: ArrayBuffer) -> ArrayBuffer
```
