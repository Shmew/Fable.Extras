# Weak Set

Stores weakly held objects in a collection.

```fsharp
type WeakSet<'T> (?iterable: seq<'T>) =
    new (ws: JS.WeakSet<'T>)

    //// Appends a new element.
    member _.Add (value: 'T) : WeakSet<'T>
        
    /// Removes the specified element.
    member _.Delete (value: 'T) : bool
        
    /// Returns a boolean indicating whether an element with the specified value exists.
    member _.Has (value: 'T) : bool

    interface JS.WeakSet<'T>
```

The functions outlined below are located in the `WeakSet` module.

## add

Appends a new element.

Signature:
```fsharp
(value: 'T) -> (s: Set<'T>) -> WeakSet<'T>
```

## delete

Removes the specified element.

Signature:
```fsharp
(value: 'T) -> (m: WeakSet<'T>) -> WeakSet<'T>
```

## empty

Creates an empty WeakSet.

Signature:
```fsharp
<'T> -> WeakSet<'T>
```

## has

Returns a boolean indicating whether an element with the specified value exists.

Signature:
```fsharp
(value: 'T) -> (m: WeakSet<'T>) -> bool
```
