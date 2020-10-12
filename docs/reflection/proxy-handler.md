# Proxy Handler

Proxies are constructed using the `ProxyHandler<'T>` class which is used to
define the behavior:

```fsharp
type ProxyHandler<'T> [<Emit("Object.create(null)")>] () =
    /// A trap for a function call.
    ///
    /// The arguments are the target, the `this` keyword scoped to the fuction call, 
    /// and the function arguments.         
    member Apply
        with get () : ('T -> obj -> obj [] -> obj) option
        and set (f: ('T -> obj -> obj [] -> obj) option) : unit

    /// A trap for the new operator.
    ///
    /// The arguments are the target, and the constructor arguments.
    member Construct
        with get () : ('T -> obj [] -> obj) option
        and set (f: ('T -> obj [] -> obj) option)

    /// A trap for Object.defineProperty.
    ///
    /// The arguments are the target, property name, and property descriptor.
    member DefineProperty
        with get () : ('T -> string -> PropertyDescriptor<obj> -> bool) option
        and set (f: ('T -> string -> PropertyDescriptor<obj> -> bool) option) : unit

    /// A trap for the delete operator.
    ///
    /// The arguments are the target and property name.
    member DeleteProperty
        with get () : ('T -> string -> bool) option
        and set (f: ('T -> string -> bool) option) : unit

    /// A trap for getting property values.
    ///
    /// The arguments are the target, property name, and proxy.
    member Get
        with get () : ('T -> string -> Proxy<'T> -> obj) option 
        and set (f: ('T -> string -> Proxy<'T> -> obj) option) : unit

    /// A trap for Object.getOwnPropertyDescriptor.
    ///
    /// The arguments are the target and property name.
    member GetOwnPropertyDescriptor
        with get () : ('T -> string -> PropertyDescriptor<obj> option) option
        and set (f: ('T -> string -> PropertyDescriptor<obj> option) option) : unit

    /// A trap for Object.getPrototypeOf.
    ///
    /// The argument is the target, the function must return either the prototype or null.
    member GetPrototypeOf
        with get () : ('T -> obj) option
        and set (f: ('T -> obj) option) : unit

    /// A trap for the in operator.
    ///
    /// The arguments are the target and property name.
    member Has
        with get () : ('T -> string -> bool) option
        and set (f: ('T -> string -> bool) option) : unit

    /// A trap for Object.isExtensible.
    ///
    /// The argument is the target.
    member IsExtensible
        with get () : ('T -> bool) option
        and set (f: ('T -> bool) option) : unit
    
    /// A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.
    ///
    /// The argument is the target.
    member OwnKeys
        with get () : ('T -> seq<_>) option
        and set (f: ('T -> seq<_>) option) : unit

    /// A trap for Object.preventExtensions.
    ///
    /// The argument is the target.
    member PreventExtensions
        with get () : ('T -> bool) option
        and set (f: ('T -> bool) option) : unit
        
    /// A trap for setting property values.
    ///
    /// The arguments are the target, property name, value of the property, and 
    /// receiver (the object the assignment was originally directed, this is usually 
    /// the proxy itself).
    member Set
        with get () : ('T -> string -> obj -> obj -> bool) option
        and set (f: ('T -> string -> obj -> obj -> bool) option) : unit

    /// A trap for Object.setPrototypeOf.
    ///
    /// The arguments are the target and the prototype.
    member SetPrototypeOf
        with get () : ('T -> obj option -> bool) option
        and set (f: ('T -> obj option -> bool) option) : unit
```

The functions outlined below are located in the `ProxyHandler` module.

## apply

A trap for a function call.

The arguments are the target, the `this` keyword scoped to the fuction call, and the function arguments.   

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> obj -> obj [] -> obj) option
```

## construct

A trap for the `new` operator.

The arguments are the target, and the constructor arguments.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> obj [] -> obj) option
```

## defineProperty

A trap for Object.defineProperty.

The arguments are the target, property name, and property descriptor.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> PropertyDescriptor<obj> -> bool) option
```

## deleteProperty

A trap for the `delete` operator.

The arguments are the target and property name.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> bool) option
```

## get

A trap for getting property values.

The arguments are the target, property name, and proxy.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> Proxy<'T> -> obj) option 
```

## getOwnPropertyDescriptor

A trap for Object.getOwnPropertyDescriptor.

The argument is the target, the function must return either the prototype or null.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> PropertyDescriptor<obj> option) option
```

## getPrototypeOf

A trap for Object.getPrototypeOf.

The arguments are the target, property name, and property descriptor.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> obj) option
```

## has

A trap for the `in` operator.

The arguments are the target and property name.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> bool) option
```

## isExtensible

A trap for Object.isExtensible.

The argument is the target.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> bool) option
```

## ownKeys

A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.

The argument is the target.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> seq<obj>) option
```

## preventExtensions

A trap for Object.preventExtensions.

The argument is the target.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> bool) option
```

## set

A trap for setting property values.

The arguments are the target, property name, value of the property, and 
receiver (the object the assignment was originally directed, this is usually 
the proxy itself).

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> string -> obj -> obj -> bool) option
```

## setPrototypeOf

A trap for Object.setPrototypeOf.

The arguments are the target and the prototype.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> ('T -> obj option -> bool) option
```

## setApply

A trap for a function call.

The arguments are the target, the `this` keyword scoped to the fuction call, and the function arguments.   

Signature:
```fsharp
('T -> obj -> obj [] -> obj) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setConstruct

A trap for the `new` operator.

The arguments are the target, and the constructor arguments.

Signature:
```fsharp
 -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setDefineProperty

A trap for Object.defineProperty.

The arguments are the target, property name, and property descriptor.

Signature:
```fsharp
('T -> string -> PropertyDescriptor<obj> -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setDeleteProperty

A trap for the `delete` operator.

The arguments are the target and property name.

Signature:
```fsharp
('T -> string -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setGet

A trap for getting property values.

The arguments are the target, property name, and proxy.

Signature:
```fsharp
('T -> string -> Proxy<'T> -> obj) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setGetOwnPropertyDescriptor

A trap for Object.getOwnPropertyDescriptor.

The argument is the target, the function must return either the prototype or null.

Signature:
```fsharp
('T -> string -> PropertyDescriptor<obj> option) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setGetPrototypeOf

A trap for Object.getPrototypeOf.

The arguments are the target, property name, and property descriptor.

Signature:
```fsharp
('T -> obj) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setHas

A trap for the `in` operator.

The arguments are the target and property name.

Signature:
```fsharp
('T -> string -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setIsExtensible

A trap for Object.isExtensible.

The argument is the target.

Signature:
```fsharp
('T -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setOwnKeys

A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.

The argument is the target.

Signature:
```fsharp
('T -> seq<obj>) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setPreventExtensions

A trap for Object.preventExtensions.

The argument is the target.

Signature:
```fsharp
('T -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setSet

A trap for setting property values.

The arguments are the target, property name, value of the property, and 
receiver (the object the assignment was originally directed, this is usually 
the proxy itself).

Signature:
```fsharp
('T -> string -> obj -> obj -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```

## setSetPrototypeOf

A trap for Object.setPrototypeOf.

The arguments are the target and the prototype.

Signature:
```fsharp
('T -> obj option -> bool) -> (ph: ProxyHandler<'T>) -> ProxyHandler<'T>
```
