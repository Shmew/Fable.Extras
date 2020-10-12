# Property Descriptor

Describes the configuration of a specific property on a given object.

```fsharp
type PropertyDescriptor<'T> () =
    new (pd: JS.PropertyDescriptor)

    /// True if the type of this property descriptor may be changed and 
    /// if the property may be deleted from the corresponding object.
    member _.Configurable
        with get () : bool option
        and set (x: bool option) : unit

    /// True if and only if this property shows up during enumeration 
    /// of the properties on the corresponding object.
    member _.Enumerable
        with get () : bool option
        and set (x: bool option) : unit

    /// A function which serves as a getter for the property, or undefined if 
    /// there is no getter. 
    ///
    /// When the property is accessed, this function is called without arguments and 
    /// with this set to the object through which the property is accessed (this may not 
    /// be the object on which the property is defined due to inheritance). 
    ///
    /// The return value will be used as the value of the property.
    member _.Get
        with get () : (unit -> 'T) option
        and set (x: (unit -> 'T) option) : unit

    /// A function which serves as a setter for the property, or undefined if there is no 
    /// setter. 
    ///
    /// When the property is assigned, this function is called with one argument (the value 
    /// being assigned to the property) and with this set to the object through which the 
    /// property is assigned.
    member _.Set
        with get () : ('T -> unit) option
        and set (x: ('T -> unit) option) : unit

    /// The value associated with the property. Can be any valid JavaScript 
    /// value (number, object, function, etc).
    member _.Value
        with get () : 'T option 
        and set (x: 'T option) : unit

    /// True if the value associated with the property may be changed with 
    /// an assignment operator.
    member _.Writable
        with get () : bool option
        and set (x: bool option) : unit

    interface JS.PropertyDescriptor
```

The functions outlined below are located in the `PropertyDescriptor` module.

## configurable

True if the type of this property descriptor may be changed and 
if the property may be deleted from the corresponding object.

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> bool option
```

## enumerable

True if and only if this property shows up during enumeration 
of the properties on the corresponding object.

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> bool option
```

## get

A function which serves as a getter for the property, or undefined if 
there is no getter. 

When the property is accessed, this function is called without arguments and 
with this set to the object through which the property is accessed (this may not 
be the object on which the property is defined due to inheritance). 

The return value will be used as the value of the property.

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> (unit -> 'T) option
```

## set

A function which serves as a setter for the property, or undefined if there is no 
setter. 

When the property is assigned, this function is called with one argument (the value 
being assigned to the property) and with this set to the object through which the 
property is assigned.

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> ('T -> unit) option
```

## value

The value associated with the property. Can be any valid JavaScript 
value (number, object, function, etc).

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> 'T option
```

## writable

True if the value associated with the property may be changed with 
an assignment operator.

Signature:
```fsharp
(pd: PropertyDescriptor<'T>) -> bool option
```

## setConfigurable

True if the type of this property descriptor may be changed and 
if the property may be deleted from the corresponding object.

Signature:
```fsharp
(b: bool) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```

## setEnumerable

True if and only if this property shows up during enumeration 
of the properties on the corresponding object.

Signature:
```fsharp
(b: bool) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```

## setGet

A function which serves as a getter for the property, or undefined if 
there is no getter. 

When the property is accessed, this function is called without arguments and 
with this set to the object through which the property is accessed (this may not 
be the object on which the property is defined due to inheritance). 

The return value will be used as the value of the property.

Signature:
```fsharp
(f: unit -> 'T) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```

## setSet

A function which serves as a setter for the property, or undefined if there is no 
setter. 

When the property is assigned, this function is called with one argument (the value 
being assigned to the property) and with this set to the object through which the 
property is assigned.

Signature:
```fsharp
(f: 'T -> unit) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```

## setValue

The value associated with the property. Can be any valid JavaScript 
value (number, object, function, etc).

Signature:
```fsharp
(v: 'T) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```

## setWritable

True if the value associated with the property may be changed with 
an assignment operator.

Signature:
```fsharp
(b: bool) -> (pd: PropertyDescriptor<'T>) -> PropertyDescriptor<'T>
```
