# Set

Lets you store unique values of any type, whether primitive values or object references.

```fsharp
type Set<'T> (?iterable: seq<'T>) =
    new (set: JS.Set<'T>)

    /// The number of elements.
    member Size: int

    //// Appends a new element.
    member Add (value: 'T) : Set<'T>
        
    /// Removes all elements.
    member Clear () : unit
        
    /// Removes the specified element.
    member Delete (value: 'T) : bool

    /// Returns a list of values in tupled form, use the values 
    /// method to get a list of just the value.
    member Entries () : ('T * 'T) list

    /// Applies the given function once per each value.
    member ForEach (action: 'T -> 'T -> Set<'T> -> unit, ?thisArg: obj) : unit

    /// Returns a boolean indicating whether an element with the specified value exists.
    member Has (value: 'T) : bool
        
    /// Returns a list of values.
    ///
    /// This is an alias for the values method.
    member Keys () : 'T list

    /// Returns a list of values.
    member Values () : 'T list
    
    interface JS.Set<'T>
```

The functions outlined below are located in the `Set` module.

## add

Appends a new element.

Signature:
```fsharp
(value: 'T) -> (s: Set<'T>) -> Set<'T>
```

## clear

Removes all elements.

Signature:
```fsharp
(m: Set<'T>) -> Set<'T>
```

## delete

Removes the specified element.

Signature:
```fsharp
(value: 'T) -> (m: Set<'T>) -> Set<'T>
```

## empty

Creates an empty Set.

Signature:
```fsharp
<'T> -> Set<'T>
```

## entries

Returns a list of values in tupled form, use the values 
method to get a list of just the value.

Signature:
```fsharp
(m: Set<'T>) -> ('T * 'T) list
```

## forEach

Applies the given function once per each value.

Signature:
```fsharp
(action: 'T -> 'T -> Set<'T> -> unit) -> (s: Set<'T>) -> unit
```

## forEachThis

Applies the given function once per each value.

Signature:
```fsharp
(action: 'T -> 'T -> Set<'T> -> unit) -> (thisArg: obj) -> (s: Set<'T>) -> unit
```

## has

Returns a boolean indicating whether an element with the specified value exists.

Signature:
```fsharp
(value: 'T) -> (m: Set<'T>) -> bool
```

## keys

Returns a list of values.

This is an alias for the values method.

Signature:
```fsharp
(m: Set<'T>) -> 'T list
```

## size

The number of elements.

Signature:
```fsharp
(m: Set<'T>) -> int
```

## values

Returns a list of values in insertion order.

Signature:
```fsharp
(m: Set<'T>) -> 'T list
```
