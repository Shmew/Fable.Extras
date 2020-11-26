# Map

Holds key-value pairs and remembers the original insertion order of the keys.

Any value (both objects and primitive values) may be used as either a key or a value.

```fsharp
type Map<'K,'V> (?iterable: seq<'K * 'V>) =
    new (m: JS.Map<'K,'V>)

    /// The number of elements.
    member Size: int

    /// Removes all elements.
    member Clear () : unit

    /// Removes the specified element by key.
    member Delete (key: 'K) : bool
        
    /// Returns a sequence of key value pairs in insertion order.
    member Entries () : ('K * 'V) list

    /// Applies the given function once per each key value pair in insertion order.
    member ForEach (action: 'V -> 'K -> Map<'K, 'V> -> unit, ?thisArg: obj) : unit

    /// Returns the value for the specified key.
    member Get (key: 'K) : 'V option

    /// Returns a boolean indicating whether an element with the specified key exists or not.
    member Has (key: 'K) : bool
        
    /// Returns a sequence of keys in insertion order.
    member Keys () = 'K list

    /// Adds or updates an element with the specified key.
    ///
    /// If a value is not provided the value will be removed, but the key will still exist.
    member Set (key: 'K, ?value: 'V) : Map<'K, 'V>
        
    /// Returns a sequence of values in insertion order.
    member Values () : 'V list

    interface JS.Map<'K,'V>
```

The functions outlined below are located in the `Map` module.

## clear

Removes all elements.

Signature:
```fsharp
(m: Map<'K,'V>) -> Map<'K,'V>
```

## delete

Removes the specified element by key.

Signature:
```fsharp
(key: 'K) -> (m: Map<'K,'V>) -> Map<'K,'V>
```

## empty

Creates an empty Map.

Signature:
```fsharp
<'K,'V> -> Map<'K,'V>
```

## entries

Returns a list of key value pairs in insertion order.

Signature:
```fsharp
(m: Map<'K,'V>) -> ('K * 'V) list
```

## forEach

Applies the given function once per each key value pair in insertion order.

Signature:
```fsharp
(action: 'V -> 'K -> Map<'K, 'V> -> unit) -> (m: Map<'K,'V>) -> unit
```

## forEachThis

Applies the given function once per each key value pair in insertion order.

Signature:
```fsharp
(action: 'V -> 'K -> Map<'K, 'V> -> unit) -> (thisArg: obj) -> (m: Map<'K,'V>) -> unit
```

## get

Returns the value for the specified key.

Signature:
```fsharp
(key: 'K) -> (m: Map<'K,'V>) -> 'V option
```

## has

Returns a boolean indicating whether an element with the specified value exists.

Signature:
```fsharp
(key: 'K) -> (m: Map<'K,'V>) -> bool
```

## keys

Returns a list of keys in insertion order.

Signature:
```fsharp
(m: Map<'K,'V>) -> 'K list
```

## ofMap

Converts a FSharp Map into a JS Map.

Signature:
```fsharp
(m: Microsoft.FSharp.Collections.Map<'K,'V>) -> Map<'K,'V>
```

## set

Adds or updates an element with the specified key.

If a value is not provided the value will be removed, but the key will still exist.

Signature:
```fsharp
(key: 'K) -> (value: 'V) -> (m: Map<'K,'V>) -> Map<'K,'V>
```

## size

The number of elements.

Signature:
```fsharp
(m: Map<'K,'V>) -> int
```

## values

Returns a list of values in insertion order.

Signature:
```fsharp
(m: Map<'K,'V>) -> 'V list
```
