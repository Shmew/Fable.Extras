# Global

## Descriptor Value

The data type of a global value.

```fsharp
type DescriptorValue =
    | I32
    | I64
    | F32
    | F64
```

## Global Descriptor

A configuration object for WebAssembly Global instantiation.

```fsharp
type GlobalDescriptor =
    new (value: DescriptorValue)
    new (value: DescriptorValue, mutable': bool)

    /// The data type of the global.
    member _.Value : DescriptorValue

    /// A boolean value that determines whether the global is mutable or not. 
    /// By default, this is false.
    member _.Mutable : bool option
```

The functions outlined below are located in the `GlobalDescriptor` module.

### value

The data type of the global.

Signature:
```fsharp
(gd: GlobalDescriptor) -> DescriptorValue
```

### mutable'

A boolean value that determines whether the global is mutable or not. 

Signature:
```fsharp
(gd: GlobalDescriptor) -> bool option
```

## Global

Creates a new Global object representing a global variable instance, accessible from both JavaScript and 
importable/exportable across one or more WebAssembly.Module instances. 

This allows dynamic linking of multiple modules.

```fsharp
type Global<'T> (descriptor: GlobalDescriptor, value: 'T) =
    /// The value of the global variable instance.
    member value
        with get () : 'T
        and set (x: 'T) : unit
```

The functions outlined below are located in the `Global` module.

### create

Creates a new Global object representing a global variable instance, accessible from both JavaScript and 
importable/exportable across one or more WebAssembly.Module instances. 

Signature:
```fsharp
(value: 'T) -> (descriptor: GlobalDescriptor) -> Global<'T>
```

### value

Get the value of the global variable instance.

Signature:
```fsharp
(global': Global<'T>) -> 'T
```

### setValue

Set the value of the global variable instance.

Signature:
```fsharp
(value: 'T) -> (global': Global<'T>) -> Global<'T>
```
