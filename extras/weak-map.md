# Weak Map

Lets you store weakly held objects in a collection.

```fsharp
type WeakMap<'K,'V when 'K : not struct> (?iterable: seq<'K * 'V>) =
    new (wm: JS.WeakMap<'K,'V>)

    /// Removes the specified element by key.
    member Delete (key: 'K) : bool
        
    /// Returns the value for the specified key.
    member Get (key: 'K) : 'V option
        
    /// Returns a boolean indicating whether an element with the specified key exists or not.
    member Has (key: 'K) : bool
        
    /// Adds or updates an element with the specified key.
    ///
    /// If a value is not provided the value will be removed, but the key will still exist.
    member Set (key: 'K, ?value: 'V) : WeakMap<'K, 'V>
    
    interface JS.WeakMap<'K,'V>
```

The functions outlined below are located in the `WeakMap` module.

## delete

Removes the specified element by key.

Signature:
```fsharp
(key: 'K) -> (m: WeakMap<'K,'V>) -> WeakMap<'K,'V>
```

## empty

Creates an empty WeakMap.

Signature:
```fsharp
<'K,'V> -> WeakMap<'K,'V>
```

## get

Returns the value for the specified key.

Signature:
```fsharp
(key: 'K) -> (m: WeakMap<'K,'V>) -> 'V option
```

## has

Returns a boolean indicating whether an element with the specified value exists.

Signature:
```fsharp
(key: 'K) -> (m: WeakMap<'K,'V>) -> bool
```

## set

Adds or updates an element with the specified key.

If a value is not provided the value will be removed, but the key will still exist.

Signature:
```fsharp
(key: 'K) -> (value: 'V) -> (m: WeakMap<'K,'V>) -> WeakMap<'K,'V>
```
