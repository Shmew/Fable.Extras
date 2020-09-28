namespace Fable.Extras

open Fable.Core
open FSharp.Core
open System
open System.ComponentModel
open System.Text.RegularExpressions

[<Erase;RequireQualifiedAccess>]
module JS =
    /// Holds key-value pairs and remembers the original insertion order of the keys.
    ///
    /// Any value (both objects and primitive values) may be used as either a key or a value.
    [<Global>]
    type Map<'K,'V> (?iterable: seq<'K * 'V>) =
        /// The number of elements.
        member _.size: int = jsNative

        /// Removes all elements.
        member _.clear () : unit = jsNative

        /// Removes the specified element by key.
        member _.delete (key: 'K) : bool = jsNative

        /// Returns a sequence of key value pairs in insertion order.
        member _.entries () : seq<'K * 'V> = jsNative
        
        /// Applies the given function once per each key value pair in insertion order.
        member _.forEach (callbackfn: 'V->'K->Map<'K, 'V>->unit, ?thisArg: obj) : unit = jsNative

        /// Returns the value for the specified key.
        member _.get (key: 'K) : 'V option = jsNative

        /// Returns a boolean indicating whether an element with the specified key exists or not.
        member _.has (key: 'K) : bool = jsNative

        /// Returns a sequence of keys in insertion order.
        member _.keys () : seq<'K> = jsNative

        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        member _.set (key: 'K, ?value: 'V) : Map<'K, 'V> = jsNative

        /// Returns a sequence of values in insertion order.
        member _.values () : seq<'V> = jsNative

        interface JS.Map<'K,'V> with
            member this.size = this.size
            member this.clear () = this.clear()
            member this.delete k = this.delete k
            member this.entries () = this.entries()
            member this.forEach (callback, ?thisArg) = this.forEach(callback, ?thisArg = thisArg)
            member this.get k = this.get k |> Option.get
            member this.has k = this.has k
            member this.keys () = this.keys()
            member this.set (key, ?value) = upcast this.set(key, ?value = value)
            member this.values () = this.values()
    
    [<Erase;RequireQualifiedAccess>]
    module Map =
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (m: Map<'K,'V>) = m.set(key,value)
        
        /// Removes all elements.
        let inline clear (m: Map<'K,'V>) = m.clear()
        
        /// Removes the specified element by key.
        let inline delete (key: 'K) (m: Map<'K,'V>) = m.delete(key)
        
        /// Returns a sequence of key value pairs in insertion order.
        let inline entries (m: Map<'K,'V>) = m.entries()

        /// Returns the value for the specified key.
        let inline get (key: 'K) (m: Map<'K,'V>) = m.get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (m: Map<'K,'V>) = m.has(key)
        
        /// Returns a sequence of keys in insertion order.
        let inline keys (m: Map<'K,'V>) = m.keys()
        
        /// The number of elements.
        let inline size (m: Map<'K,'V>) = m.size
        
        /// Returns a sequence of values in insertion order.
        let inline values (m: Map<'K,'V>) = m.values()

    /// Lets you store weakly held objects in a collection.
    [<Global>]
    type WeakMap<'K,'V> (?iterable: seq<'K * 'V>) =
        /// Removes all elements.
        member _.clear () : unit = jsNative
        
        /// Removes the specified element by key.
        member _.delete (key: 'K) : bool = jsNative
        
        /// Returns the value for the specified key.
        member _.get (key: 'K) : 'V option = jsNative
        
        /// Returns a boolean indicating whether an element with the specified key exists or not.
        member _.has (key: 'K) : bool = jsNative
        
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        member _.set (key: 'K, ?value: 'V) : WeakMap<'K, 'V> = jsNative
    
        interface JS.WeakMap<'K,'V> with
            member this.clear () = this.clear()
            member this.delete k = this.delete k
            member this.get k = this.get k |> Option.get
            member this.has k = this.has k
            member this.set (key, ?value) = upcast this.set(key, ?value = value)

    [<Erase;RequireQualifiedAccess>]
    module WeakMap =
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (wm: WeakMap<'K,'V>) = wm.set(key,value)
        
        /// Removes all elements.
        let inline clear (wm: WeakMap<'K,'V>) = wm.clear()
        
        /// Removes the specified element by key.
        let inline delete (key: 'K) (wm: WeakMap<'K,'V>) = wm.delete(key)
        
        /// Returns the value for the specified key.
        let inline get (key: 'K) (wm: WeakMap<'K,'V>) = wm.get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (wm: WeakMap<'K,'V>) = wm.has(key)

    /// Lets you store unique values of any type, whether primitive values or object references.
    [<Global>]
    type Set<'T> (?iterable: seq<'T>) =
        /// The number of elements.
        member _.size: int = jsNative

        //// Appends a new element.
        member _.add (value: 'T) : Set<'T> = jsNative
        
        /// Removes all elements.
        member _.clear () : unit = jsNative
        
        /// Removes the specified element.
        member _.delete (value: 'T) : bool = jsNative

        /// Returns a sequence of values in tupled form, use the values 
        /// method to get a sequence of just the value.
        member _.entries () : seq<'T * 'T> = jsNative
        
        /// Applies the given function once per each value.
        member _.forEach (callbackfn: 'T -> 'T -> Set<'T> -> unit, ?thisArg: obj) : unit = jsNative

        /// Returns a boolean indicating whether an element with the specified value exists.
        member _.has (value: 'T) : bool = jsNative

        /// Returns a sequence of values.
        ///
        /// This is an alias for the values method.
        member _.keys () : seq<'T> = jsNative
        
        /// Returns a sequence of values.
        member _.values () : seq<'T> = jsNative
    
        interface JS.Set<'T> with
            member this.size = this.size
            member this.add value = upcast this.add(value)
            member this.clear () = this.clear()
            member this.delete k = this.delete k
            member this.entries () = this.entries()
            member this.forEach (callback, ?thisArg) = this.forEach(callback, ?thisArg = thisArg)
            member this.has k = this.has k
            member this.keys () = this.keys()
            member this.values () = this.values()

    [<Erase;RequireQualifiedAccess>]
    module Set =
        /// Appends a new element.
        let inline add (value: 'T) (s: Set<'T>) = s.add(value)
        
        /// Removes all elements.
        let inline clear (s: Set<'T>) = s.clear()
        
        /// Removes the specified element.
        let inline delete (value: 'T) (s: Set<'T>) = s.delete(value)
        
        /// Returns a sequence of values in tupled form, use the values 
        /// method to get a sequence of just the value.
        let inline entries (s: Set<'T>) = s.entries()
        
        /// Applies the given function once per each value.
        let inline forEach (callbackFn: 'T -> 'T -> Set<'T> -> unit) (s: Set<'T>) = s.forEach(callbackFn)
        
        /// Applies the given function once per each value.
        let inline forEachWithThis (callbackFn: 'T -> 'T -> Set<'T> -> unit) (thisArg: obj) (s: Set<'T>) = s.forEach(callbackFn, thisArg = thisArg)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (s: Set<'T>) = s.has(value)
        
        /// Returns a sequence of values.
        ///
        /// This is an alias for the values method.
        let inline keys (s: Set<'T>) = s.keys()
        
        /// The number of elements.
        let inline size (s: Set<'T>) = s.size
        
        /// Returns a sequence of values.
        let inline values (s: Set<'T>) = s.values()
        
    /// Stores weakly held objects in a collection.
    [<Global>]
    type WeakSet<'T> (?iterable: seq<'T>) =
        [<Emit("$0")>]
        new (ws: JS.WeakSet<'T>) = WeakSet<'T>()

        //// Appends a new element.
        member _.add (value: 'T) : WeakSet<'T> = jsNative
        
        /// Removes all elements.
        member _.clear () : unit = jsNative
        
        /// Removes the specified element.
        member _.delete (value: 'T) : bool = jsNative
        
        /// Returns a boolean indicating whether an element with the specified value exists.
        member _.has (value: 'T) : bool = jsNative

        interface JS.WeakSet<'T> with
            member this.add value = upcast this.add(value)
            member this.clear () = this.clear()
            member this.delete k = this.delete k
            member this.has k = this.has k
    
    [<Erase;RequireQualifiedAccess>]
    module WeakSet =
        /// Appends a new element.
        let inline add (value: 'T) (ws: WeakSet<'T>) = ws.add(value)
        
        /// Removes all elements.
        let inline clear (ws: WeakSet<'T>) = ws.clear()
        
        /// Removes the specified element.
        let inline delete (value: 'T) (ws: WeakSet<'T>) = ws.delete(value)
        
        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (ws: WeakSet<'T>) = ws.has(value)


    /// Describes the configuration of a specific property on a given object.
    [<Global>]
    type PropertyDescriptor<'T> [<Emit("{}")>] () =
        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        ///
        /// Defaults to false.
        member _.configurable
            with get () : bool option = jsNative
            and set (x: bool option) = jsNative

        /// True if and only if this property shows up during enumeration 
        /// of the properties on the corresponding object.
        ///
        /// Defaults to false.
        member _.enumerable
            with get () : bool option = jsNative
            and set (x: bool option) = jsNative

        /// A function which serves as a getter for the property, or undefined if 
        /// there is no getter. 
        ///
        /// When the property is accessed, this function is called without arguments and 
        /// with this set to the object through which the property is accessed (this may not 
        /// be the object on which the property is defined due to inheritance). 
        ///
        /// The return value will be used as the value of the property.
        ///
        /// Defaults to undefined.
        member _.get
            with get () : (unit -> 'T) option = jsNative
            and set (x: (unit -> 'T) option) = jsNative

        /// A function which serves as a setter for the property, or undefined if there is no 
        /// setter. 
        ///
        /// When the property is assigned, this function is called with one argument (the value 
        /// being assigned to the property) and with this set to the object through which the 
        /// property is assigned.
        ///
        /// Defaults to undefined.
        member _.set
            with get () : ('T -> unit) option = jsNative
            and set (x: ('T -> unit) option) = jsNative

        /// The value associated with the property. Can be any valid JavaScript 
        /// value (number, object, function, etc).
        ///
        /// Defaults to None.
        member _.value
            with get () : 'T option  = jsNative
            and set (x: 'T option) = jsNative

        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        ///
        /// Defaults to false.
        member _.writable
            with get () : bool option = jsNative
            and set (x: bool option) = jsNative

        interface JS.PropertyDescriptor with
            member this.configurable
                with get () = this.configurable
                and set x = this.configurable <- x
            member this.enumerable
                with get () = this.enumerable
                and set x = this.enumerable <- x
            member this.get () = box (this.get)
            member this.set x = this.set <- (unbox<('T -> unit) option> x)
            member this.value
                with get () = unbox this.value
                and set x = this.value <- unbox x
            member this.writable
                with get () = this.writable
                and set x = this.writable <- x
    
    [<Erase;RequireQualifiedAccess>]
    module PropertyDescriptor =
        [<Erase;RequireQualifiedAccess>]
        module Get =
            /// True if the type of this property descriptor may be changed and 
            /// if the property may be deleted from the corresponding object.
            ///
            /// Defaults to false.
            let inline configurable (pd: PropertyDescriptor<'T>) = pd.configurable

            /// Indicates whether the specified property is enumerable 
            /// and is the object's own property.
            let inline enumerable (pd: PropertyDescriptor<'T>) = pd.enumerable

            /// Indicates whether the object has the specified property 
            /// as its own property (as opposed to inheriting it).
            let inline get (pd: PropertyDescriptor<'T>) = pd.get
        
            /// Checks if an object exists in another object's prototype chain.
            let inline set (pd: PropertyDescriptor<'T>) = pd.set

            /// Indicates whether the specified property is enumerable 
            /// and is the object's own property.
            ///
            /// An integer would be used when testing against an array.
            let inline value (pd: PropertyDescriptor<'T>) = pd.value
        
            /// True if the value associated with the property may be changed with 
            /// an assignment operator.
            ///
            /// Defaults to false.
            let inline writable (pd: PropertyDescriptor<'T>) = pd.writable

        [<Erase;RequireQualifiedAccess>]
        module Set =
            /// True if the type of this property descriptor may be changed and 
            /// if the property may be deleted from the corresponding object.
            ///
            /// Defaults to false.
            let inline configurable (b: bool) (pd: PropertyDescriptor<'T>) = 
                pd.configurable <- Some b
                pd

            /// Indicates whether the specified property is enumerable 
            /// and is the object's own property.
            let inline enumerable (b: bool) (pd: PropertyDescriptor<'T>) = 
                pd.enumerable <- Some b
                pd

            /// Indicates whether the object has the specified property 
            /// as its own property (as opposed to inheriting it).
            let inline get (f: unit -> 'T) (pd: PropertyDescriptor<'T>) = 
                pd.get <- Some f
                pd

            /// Checks if an object exists in another object's prototype chain.
            let inline set (f: 'T -> unit) (pd: PropertyDescriptor<'T>) = 
                pd.set <- Some f
                pd

            /// Indicates whether the specified property is enumerable 
            /// and is the object's own property.
            ///
            /// An integer would be used when testing against an array.
            let inline value (v: 'T) (pd: PropertyDescriptor<'T>) = 
                pd.value <- Some v
                pd

            /// True if the value associated with the property may be changed with 
            /// an assignment operator.
            ///
            /// Defaults to false.
            let inline writable (b: bool) (pd: PropertyDescriptor<'T>) = 
                pd.writable <- Some b
                pd

    [<Global>]
    type Array =
        static member isArray (arg: obj) : bool = jsNative

        interface JS.ArrayConstructor with
            member _.isArray x = Array.isArray x
    
    [<Global>]
    type Number =
        static member isNaN (value: float) : bool = jsNative
    
        interface JS.NumberConstructor with
            member _.isNaN x = Number.isNaN x

    [<Global>]
    type Object =
        /// Copies all enumerable own properties from one or more source 
        /// objects to a target object. It returns the target object.
        static member assign (target: 'T) (source: 'U) : obj = jsNative

        /// Copies all enumerable own properties from one or more source 
        /// objects to a target object. It returns the target object.
        [<Emit("Object.assign($0,$1,$2)")>]
        static member assign2 (target: 'T) (source1: 'U) (source2: 'V) : obj = jsNative

        /// Copies all enumerable own properties from one or more source 
        /// objects to a target object. It returns the target object.
        [<Emit("Object.assign($0,$1,$2,$3)")>]
        static member assign3 (target: 'T) (source1: 'U) (source2: 'V) (source3: 'W) : obj = jsNative

        /// Copies all enumerable own properties from one or more source 
        /// objects to a target object. It returns the target object.
        [<Emit("Object.assign(...[$0,...$1])")>]
        static member assignMany (target: 'T) (sources: seq<'U>) : obj = jsNative

        /// Creates a new object, using an existing object as the prototype of the newly created object.
        static member create<'T when 'T : not struct> (o: 'T) : 'T = jsNative
        
        /// Creates a new object, using an existing object as the prototype of the newly created object.
        [<Emit("Object.create($1,Object.fromEntries($0))")>]
        static member createWithDescriptors<'T when 'T : not struct> (descriptors: seq<string * JS.PropertyDescriptor>) (o: 'T) : 'T = jsNative

        /// Defines new or modifies existing properties directly on an object, returning the object.
        [<Emit("Object.defineProperties($1,Object.fromEntries($0))")>]
        static member defineProperties<'T when 'T : not struct> (descriptors: seq<string * JS.PropertyDescriptor>) (o: 'T) : obj = jsNative

        /// Defines new or modifies an existing property directly on an object, returning the object.
        [<Emit("Object.defineProperties($2,$0,$1)")>]
        static member defineProperty<'T when 'T : not struct> (propertyKey: string) (descriptor: JS.PropertyDescriptor) (o: 'T) : obj = jsNative

        /// Freezes an object. 
        ///
        /// A frozen object can no longer be changed; freezing an object prevents new properties from 
        /// being added to it, existing properties from being removed, prevents changing the enumerability, 
        /// configurability, or writability of existing properties, and prevents the values of existing 
        /// properties from being changed
        static member freeze<'T when 'T : not struct> (o: 'T) : 'T = jsNative

        /// Returns an object describing the configuration of a specific property on a given object 
        /// (that is, one directly present on an object and not in the object's prototype chain).
        static member getOwnPropertyDescriptor<'T> (o: obj) (p: string) : PropertyDescriptor<'T> option = jsNative

        /// Returns all own property descriptors of a given object..
        [<Emit("new Map(Object.entries(Object.getOwnPropertyDescriptors($0)))")>]
        static member getOwnPropertyDescriptors (o: 'T) : Map<string,PropertyDescriptor<obj>> = jsNative

        /// Returns the prototype (i.e. the value of the internal [[Prototype]] property) of the specified object.
        static member getPrototypeOf (o: 'T) : Object = jsNative
        
        /// Indicates whether the object has the specified property 
        /// as its own property (as opposed to inheriting it).
        [<Emit("Object.prototype.hasOwnProperty($0,$1)")>]
        static member hasOwnProperty (property: string) (o: 'T) : bool = jsNative

        /// Determines whether two values are the same value.
        static member is (value1: 'T) (value2: 'U) : bool = jsNative

        /// Determines if an object is extensible (whether it can have new properties added to it).
        static member isExtensible (o: 'T) : bool = jsNative

        /// Determines if an object is frozen.
        static member isFrozen (o: 'T) : bool = jsNative
        
        /// Checks if an object exists in another object's prototype chain.
        [<Emit("Object.prototype.isPrototypeOf($0,$1)")>]
        static member isPrototypeOf (proto: 'T) (o: 'U) : bool = jsNative
        
        /// Determines if an object is sealed.
        static member isSealed (o: 'T) : bool = jsNative

        /// Prevents new properties from ever being added to an object (i.e. prevents future extensions to the object).
        static member preventExtensions (o: 'T) : 'T = jsNative
        
        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        [<Emit("x => Object.prototype.propertyIsEnumerable($0,x)")>]
        static member propertyIsEnumerable (property: int) : 'T -> bool = jsNative
        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        [<Emit("x => Object.prototype.propertyIsEnumerable($0,x)")>]
        static member propertyIsEnumerable (property: string) : 'T -> bool = jsNative

        /// Seals an object, preventing new properties from being added to it and marking all existing properties as 
        /// non-configurable. Values of present properties can still be changed as long as they are writable.
        static member seal (o: 'T) : 'T = jsNative

        /// Sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to another object.
        static member setPrototypeOf (o: 'T) (proto: 'U) : obj = jsNative
        
        /// Sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to null.
        [<Emit("Object.setPrototypeOf($0, null)")>]
        static member setPrototypeOfNull (o: 'T) : obj = jsNative
        
        [<Emit("Object.prototype.toLocaleString($0)")>]
        static member toLocaleString (o: 'T) : string = jsNative
        
        [<Emit("Object.prototype.toString($0)")>]
        static member toString (o: 'T) : string = jsNative

        /// Returns the primitive value of the specified object.
        [<Emit("Object.prototype.valueOf($0)")>]
        static member valueOf (o: 'T) : obj = jsNative

    [<Global>]
    type Math =
        /// Represents the base of natural logarithms, e, approximately 2.718.
        static member E: float = jsNative

        /// Represents the natural logarithm of 10, approximately 2.302.
        static member LN10: float = jsNative

        /// Represents the natural logarithm of 2, approximately 0.693.
        static member LN2: float = jsNative

        /// Represents the base 2 logarithm of e, approximately 1.442.
        static member LOG2E: float = jsNative
        
        /// Represents the base 10 logarithm of e, approximately 0.434.
        static member LOG10E: float = jsNative

        /// Represents the ratio of the circumference of a circle to its diameter, approximately 3.14159.
        static member PI: float = jsNative

        /// Represents the square root of 1/2 which is approximately 0.707.
        static member SQRT1_2: float = jsNative

        /// Represents the square root of 2, approximately 1.414.
        static member SQRT2: float = jsNative

        /// Returns the absolute value of a number.
        static member abs (x: decimal) : decimal = jsNative
        /// Returns the absolute value of a number.
        static member abs (x: float) : float = jsNative
        /// Returns the absolute value of a number.
        static member abs (x: int16) : int16 = jsNative
        /// Returns the absolute value of a number.
        static member abs (x: int) : int = jsNative
        /// Returns the absolute value of a number.
        static member abs (x: int64) : int64 = jsNative
        /// Returns the absolute value of a number.
        static member abs (x: sbyte) : sbyte = jsNative
        
        /// Returns the arccosine (in radians) of a number.
        static member acos (x: float) : float = jsNative

        /// Returns the hyperbolic arc-cosine of a number.
        static member acosh (x: float) : float = jsNative

        /// Returns the arcsine (in radians) of a number.
        static member asin (x: float) : float = jsNative

        /// Returns the hyperbolic sine of a number.
        static member asinh (x: float) : float = jsNative

        /// Returns the arctangent (in radians) of a number.
        static member atan (x: float) : float = jsNative

        /// Returns the hyperbolic arctangent of a number.
        static member atanh (x: float) : float = jsNative
        
        /// Returns the angle in the plane (in radians) between the 
        /// positive x-axis and the ray from (0,0) to the point (x,y).
        static member atan2 (y: float, x: float) : float = jsNative
        
        /// Returns the cube root of a number.
        static member cbrt (x: float) : float = jsNative
        
        /// Always rounds a number up to the next largest integer.
        static member ceil (x: decimal) : int = jsNative
        /// Always rounds a number up to the next largest integer.
        static member ceil (x: float) : int = jsNative
        
        /// Returns the number of leading zero bits in the 32-bit binary representation of a number.
        static member clz32 (x: float) : float = jsNative
        
        /// Returns the cosine of the specified angle, which must be specified in radians.
        static member cos (x: float) : float = jsNative
        
        /// Returns the hyperbolic cosine of a number.
        static member cosh (x: float) : float = jsNative
        
        /// Returns e^x, where x is the argument, and e is Euler's number (also known as Napier's constant), 
        /// the base of the natural logarithms.
        static member exp (x: float) : float = jsNative
        
        /// Returns ex - 1, where x is the argument, and e the base of the natural logarithms.
        static member expm1 (x: float) : float = jsNative
        
        /// Returns the largest integer less than or equal to a given number.
        static member floor (x: decimal) : int = jsNative
        /// Returns the largest integer less than or equal to a given number.
        static member floor (x: float) : int = jsNative
        
        /// Returns the nearest 32-bit single precision float representation of a Number.
        static member fround (x: float) : float = jsNative
        
        /// Returns the square root of the sum of squares of its arguments.
        static member hypot ([<ParamArray>] values: float []) : float = jsNative
        
        /// Returns the result of the C-like 32-bit multiplication of the two parameters.
        static member imul (x: float, y: float) : float = jsNative
        
        /// Returns the natural logarithm (base e) of a number.
        static member log (x: float) : float = jsNative
        
        /// Returns the base 10 logarithm of a number.
        static member log10 (x: float) : float = jsNative
        
        /// Returns the natural logarithm (base e) of 1 + a number.
        static member log1p (x: float) : float = jsNative
        
        /// Returns the base 2 logarithm of a number.
        static member log2 (x: float) : float = jsNative
        
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: byte []) : byte = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: decimal []) : decimal = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: float []) : float = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: int16 []) : int16 = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: int []) : int = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: int64 []) : int64 = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: sbyte []) : sbyte = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: uint16 []) : uint16 = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: uint32 []) : uint32 = jsNative
        /// Returns the largest of the zero or more numbers given as input parameters.
        static member max ([<ParamArray>] values: uint64 []) : uint64 = jsNative
        
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: byte []) : byte = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: decimal []) : decimal = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: float []) : float = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: int16 []) : int16 = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: int []) : int = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: int64 []) : int64 = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: sbyte []) : sbyte = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: uint16 []) : uint16 = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: uint32 []) : uint32 = jsNative
        /// Returns the lowest of the zero or more numbers given as input parameters.
        static member min ([<ParamArray>] values: uint64 []) : uint64 = jsNative
        
        /// Returns the base to the exponent power.
        static member pow (x: float, y: float) : float = jsNative
        
        /// Returns a floating-point, pseudo-random number in the range 0 to less than 1 
        /// (inclusive of 0, but not 1) with approximately uniform distribution over that range.
        static member random () : float = jsNative
        
        /// Returns the value of a number rounded to the nearest integer.
        static member round (x: decimal) : int = jsNative
        /// Returns the value of a number rounded to the nearest integer.
        static member round (x: float) : int = jsNative
        
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: decimal) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: float) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: int16) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: int) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: int64) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        ///
        /// Note that if the number is positive, an explicit (+) will not be returned.
        static member sign (x: sbyte) : int = jsNative

        /// Returns the sine of a number.
        static member sin (x: float) : float = jsNative
        
        /// Returns the hyperbolic sine of a number.
        static member sinh (x: float) : float = jsNative
        
        /// Returns the square root of a number.
        static member sqrt (x: float) : float = jsNative
        
        /// Returns the tangent of a number.
        static member tan (x: float) : float = jsNative
        
        /// Returns the hyperbolic tangent of a number.
        static member tanh (x: float) : float = jsNative
        
        /// Returns the integer part of a number by removing any fractional digits.
        static member trunc (x: decimal) : int = jsNative
        /// Returns the integer part of a number by removing any fractional digits.
        static member trunc (x: float) : int = jsNative
    
    /// Represents a single moment in time in a platform-independent format.
    [<Global>]
    type Date private (?inp: obj) =
        new () = Date()
        new (ticks: int) = Date(unbox<int> ticks)
        new (ticks: int64) = Date(unbox<int> ticks)
        new (value: string) = Date(value)
        new (year: float, month: float, ?date: float, ?hours: float, ?minutes: float, ?seconds: float, ?ms: float) = 
            Date(box (year, month, date, hours, minutes, seconds, ms))
        new (year: int, month: int, ?date: int, ?hours: int, ?minutes: int, ?seconds: int, ?ms: int ) = 
            Date(box (year, month, date, hours, minutes, seconds, ms))
            
        member _.getDate () : int = jsNative
        
        member _.getDay () : DayOfWeek = jsNative

        member _.getFullYear () : int = jsNative
        
        member _.getHours () : int = jsNative
        
        member _.getMilliseconds () : int = jsNative

        member _.getMinutes () : int = jsNative

        member _.getMonth () : int = jsNative
        
        member _.getSeconds () : int = jsNative

        member _.getTime () : int64 = jsNative
        
        member _.getTimezoneOffset () : int = jsNative

        member _.getUTCDate () : int = jsNative

        member _.getUTCDay () : DayOfWeek = jsNative

        member _.getUTCFullYear () : int = jsNative

        member _.getUTCHours () : int = jsNative
        
        member _.getUTCMilliseconds () : int = jsNative

        member _.getUTCMinutes () : int = jsNative

        member _.getUTCMonth () : int = jsNative
        
        member _.getUTCSeconds () : int = jsNative
        
        [<Emit("new Date($0.setDate($1))")>]
        member _.setDate (date: int) : Date = jsNative
        
        [<Emit("new Date($0.setFullYear($1...))")>]
        member _.setFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setHours($1...))")>]
        member _.setHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMilliseconds($1))")>]
        member _.setMilliseconds (ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMinutes($1...))")>]
        member _.setMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMonth($1...))")>]
        member _.setMonth (month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setSeconds($1...))")>]
        member _.setSeconds (sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setTime($1))")>]
        member _.setTime (time: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCDate($1))")>]
        member _.setUTCDate (date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCFullYear($1...))")>]
        member _.setUTCFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCHours($1...))")>]
        member _.setUTCHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMilliseconds($1))")>]
        member _.setUTCMilliseconds (ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMinutes($1...))")>]
        member _.setUTCMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMonth($1...))")>]
        member _.setUTCMonth (month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCSeconds($1...))")>]
        member _.setUTCSeconds (sec: int, ?ms: int) : Date = jsNative
        
        member _.toDateString () : string = jsNative

        member _.toJSON () : string = jsNative

        member _.toLocaleDateString () : string = jsNative

        member _.toLocaleString () : string = jsNative

        member _.toLocaleTimeString () : string = jsNative

        member _.toISOString () : string = jsNative

        member _.toString () : string = jsNative

        member _.toTimeString () : string = jsNative

        member _.toUTCString () : string = jsNative

        member _.valueOf () : int64 = jsNative
    
        /// Like Date.now(), but returns a string value of the date.
        [<Emit("Date()")>]
        static member Invoke () : string = jsNative
    
        /// Parses a string representation of a date and returns the ticks.
        static member parse (s: string) : int64 = jsNative
    
        /// Accepts parameters similar to the Date constructor, but treats them as UTC in ticks.
        static member UTC (year: int, month: int, ?date: int, ?hours: int, ?minutes: int, ?seconds: int, ?ms: int) : int64 = jsNative
    
        /// Returns the current time in ticks.
        static member now () : int64 = jsNative

    [<Global>]
    type JSON =
        static member parse (text: string, ?reviver: obj -> obj -> obj) : obj = jsNative
        [<Emit("JSON.parse($0...)")>]
        static member parseAs<'T> (text: string, ?reviver: obj -> obj -> obj) : 'T = jsNative
        static member stringify (value: obj, ?replacer: string -> obj -> obj, ?space: obj) : string = jsNative
    
    /// Represents the eventual completion (or failure) of an asynchronous operation and its resulting value.
    [<Global>]
    type Promise<'T> (executor: ('T -> unit) -> (Exception -> unit) -> unit) =
        [<Emit("$0")>]
        new (p: JS.Promise<'T>) = Promise<'T>(unbox<('T -> unit) -> (Exception -> unit) -> unit> p)

        /// Handles thrown or rejected promises.
        [<Emit("$0.catch($1...)")>]
        member _.catch<'Reject> (?onrejected: 'Reject -> 'T) : Promise<'T> = jsNative
        
        /// Callback functions for the success case of the Promise.
        [<Emit("$0.then($1...)")>]
        member _.then'<'Reject,'TResult> (onfulfilled: 'T -> 'TResult) : Promise<'TResult> = jsNative
        /// Callback functions for the success and failure cases of the Promise.
        [<Emit("$0.then($1...)")>]
        member _.then'<'Reject,'TResult> (onfulfilled: 'T -> 'TResult, onrejected: 'Reject -> 'TResult) : Promise<'TResult> = jsNative
        
        /// When the promise is settled, i.e either fulfilled or rejected, the specified callback function is executed.
        [<Emit("$0.finally($1)")>]
        member _.finally' (handler: unit -> unit) : Promise<'T> = jsNative
        
        /// Takes a sequence of Promises, and returns a single Promise that resolves to an array of the results 
        /// of the input promises. This returned promise will resolve when all of the input's promises have 
        /// resolved, or if the input iterable contains no promises. It rejects immediately upon any of the input 
        /// promises rejecting or non-promises throwing an error, and will reject with this first rejection message / error.
        static member all (promises: seq<#JS.Promise<'T>>) : Promise<'T []> = jsNative

        /// Returns a promise that fulfills or rejects as soon as one of the promises in an iterable fulfills or 
        /// rejects, with the value or reason from that promise.
        static member race (values: seq<#JS.Promise<'T>>) : Promise<'T> = jsNative

        /// Returns a Promise object that is rejected with a given reason.
        static member reject () : Promise<unit> = jsNative
        /// Returns a Promise object that is rejected with a given reason.
        static member reject (reason: 'Reason) : Promise<'Reason> = jsNative
        
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a thenable (i.e. has a "then" 
        /// method), the returned promise will "follow" that thenable, adopting its eventual state; otherwise the 
        /// returned promise will be fulfilled with the value. This function flattens nested layers of promise-like 
        /// objects (e.g. a promise that resolves to a promise that resolves to something) into a single layer.
        static member resolve () : Promise<unit> = jsNative
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a thenable (i.e. has a "then" 
        /// method), the returned promise will "follow" that thenable, adopting its eventual state; otherwise the 
        /// returned promise will be fulfilled with the value. This function flattens nested layers of promise-like 
        /// objects (e.g. a promise that resolves to a promise that resolves to something) into a single layer.
        static member resolve (promise: #JS.Promise<'T>) : Promise<'T> = jsNative

        interface JS.Promise<'T> with
            member this.catch f = upcast this.catch (unbox f)
            member this.``then`` (fulfilled, rejected) = upcast this.then'(unbox fulfilled, unbox rejected)
    
    /// Used to represent a generic, fixed-length raw binary data buffer.
    [<Global>]
    type ArrayBuffer (byteLength: int) =
        [<Emit("$0")>]
        new (ab: JS.ArrayBuffer) = ArrayBuffer(0)

        /// The read-only size, in bytes, of the ArrayBuffer. 
        member _.byteLength: int = jsNative
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        member _.slice (?begin': int, ?end': int) : ArrayBuffer = jsNative
            
        /// Determines whether the passed value is one of the ArrayBuffer views, 
        /// such as typed array objects or a DataView.
        static member isView (arg: obj) : bool = jsNative
    
        interface JS.ArrayBuffer with
            member this.byteLength = this.byteLength
            member this.slice (begin', end') = this.slice(begin', ?end' = end') :> JS.ArrayBuffer

    [<Erase;RequireQualifiedAccess>]
    module ArrayBuffer =
        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (ab: ArrayBuffer) = ab.byteLength

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline slice (begin': int) (end': int) (ab: ArrayBuffer) = ab.slice(begin', end')

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline sliceBegin (begin': int) (ab: ArrayBuffer) = ab.slice(begin' = begin')
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline sliceEnd (end': int) (ab: ArrayBuffer) = ab.slice(end' = end')

    /// Provides a low-level interface for reading and writing multiple number types in a binary 
    /// ArrayBuffer, without having to care about the platform's endianness.
    [<Global>]
    type DataView (buffer: ArrayBuffer, ?byteOffset: int, ?byteLength: float) =
        [<Emit("$0")>]
        new (dv: JS.DataView) = DataView(unbox<ArrayBuffer> dv)

        /// The ArrayBuffer referenced by a DataView at construction time.
        member _.buffer: ArrayBuffer = jsNative

        /// The read-only size, in bytes, of the ArrayBuffer. 
        member _.byteLength: int = jsNative

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        member _.byteOffset: int = jsNative

        member _.getFloat32 (byteOffset: int, ?littleEndian: bool) : float32 = jsNative
        member _.getFloat64 (byteOffset: int, ?littleEndian: bool) : float = jsNative
        member _.getInt8 (byteOffset: int) : sbyte = jsNative
        member _.getInt16 (byteOffset: int, ?littleEndian: bool) : int16 = jsNative
        member _.getInt32 (byteOffset: int, ?littleEndian: bool) : int32 = jsNative
        member _.getUint8 (byteOffset: int) : byte = jsNative
        member _.getUint16 (byteOffset: int, ?littleEndian: bool) : uint16 = jsNative
        member _.getUint32 (byteOffset: int, ?littleEndian: bool) : uint32 = jsNative
        member _.setFloat32 (byteOffset: int, value: float32, ?littleEndian: bool) : unit = jsNative
        member _.setFloat64 (byteOffset: int, value: float, ?littleEndian: bool) : unit = jsNative
        member _.setInt8 (byteOffset: int, value: sbyte) : unit = jsNative
        member _.setInt16 (byteOffset: int, value: int16, ?littleEndian: bool) : unit = jsNative
        member _.setInt32 (byteOffset: int, value: int32, ?littleEndian: bool) : unit = jsNative
        member _.setUint8 (byteOffset: int, value: byte) : unit = jsNative
        member _.setUint16 (byteOffset: int, value: uint16, ?littleEndian: bool) : unit = jsNative
        member _.setUint32 (byteOffset: int, value: uint32, ?littleEndian: bool) : unit = jsNative

        interface JS.ArrayBufferView with
            member this.buffer = upcast this.buffer
            member this.byteLength = this.byteLength
            member this.byteOffset = this.byteOffset

    [<Erase;RequireQualifiedAccess>]
    module DataView =
        /// The ArrayBuffer referenced by a DataView at construction time.
        let inline buffer (dv: DataView) = dv.buffer

        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (dv: DataView) = jsNative

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        let inline byteOffset (dv: DataView) = jsNative

        [<Erase>]
        module LittleEndian =
            let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.getFloat32(byteOffset, true)
            
            let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.getFloat64(byteOffset, true)
            
            let inline getInt16 (byteOffset: int) (dv: DataView) = dv.getInt16(byteOffset, true)
            
            let inline getInt32 (byteOffset: int) (dv: DataView) = dv.getInt32(byteOffset, true)
            
            let inline getUint16 (byteOffset: int) (dv: DataView) = dv.getUint16(byteOffset, true)
            
            let inline getUint32 (byteOffset: int) (dv: DataView) = dv.getUint32(byteOffset, true)
            
            let inline setFloat32 (byteOffset: int, value: float32) (dv: DataView) = dv.setFloat32(byteOffset, value, true)
            
            let inline setFloat64 (byteOffset: int, value: float) (dv: DataView) = dv.setFloat64(byteOffset, value, true)
            
            let inline setInt16 (byteOffset: int, value: int16) (dv: DataView) = dv.setInt16(byteOffset, value, true)
            
            let inline setInt32 (byteOffset: int, value: int32) (dv: DataView) = dv.setInt32(byteOffset, value, true)
            
            let inline setUint16 (byteOffset: int, value: uint16) (dv: DataView) = dv.setUint16(byteOffset, value, true)
            
            let inline setUint32 (byteOffset: int, value: uint32) (dv: DataView) = dv.setUint32(byteOffset, value, true)

        let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.getFloat32(byteOffset)

        let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.getFloat64(byteOffset)
        
        let inline getInt8 (byteOffset: int) (dv: DataView) = dv.getInt8(byteOffset)

        let inline getInt16 (byteOffset: int) (dv: DataView) = dv.getInt16(byteOffset)

        let inline getInt32 (byteOffset: int) (dv: DataView) = dv.getInt32(byteOffset)

        let inline getUint16 (byteOffset: int) (dv: DataView) = dv.getUint16(byteOffset)

        let inline getUint32 (byteOffset: int) (dv: DataView) = dv.getUint32(byteOffset)

        let inline setFloat32 (byteOffset: int, value: float32) (dv: DataView) = dv.setFloat32(byteOffset, value)

        let inline setFloat64 (byteOffset: int, value: float) (dv: DataView) = dv.setFloat64(byteOffset, value)
        
        let inline setInt8 (byteOffset: int, value: sbyte) (dv: DataView) = dv.setInt8(byteOffset, value)

        let inline setInt16 (byteOffset: int, value: int16) (dv: DataView) = dv.setInt16(byteOffset, value)

        let inline setInt32 (byteOffset: int, value: int32) (dv: DataView) = dv.setInt32(byteOffset, value)
        
        let inline setUint8 (byteOffset: int, value: byte) (dv: DataView) = dv.setUint8(byteOffset, value)

        let inline setUint16 (byteOffset: int, value: uint16) (dv: DataView) = dv.setUint16(byteOffset, value)

        let inline setUint32 (byteOffset: int, value: uint32) (dv: DataView) = dv.setUint32(byteOffset, value)
        
    /// Describes an array-like view of an underlying binary data buffer.
    [<Erase>]
    type TypedArray<'T> internal () =
        member _.Item
            with [<Emit("$0[$1]")>] get (index: int) : 'T = jsNative
            and [<Emit("$0[$1] = $2")>] set (index: int) (value: int) = jsNative

        /// Returns a sequence that contains the key/value pairs for each index in the array.
        [<Emit("$0.entries()")>]
        member _.entries () : seq<int * 'T> = jsNative

        /// The ArrayBuffer referenced by a TypedArray at construction time.
        [<Emit("$0.buffer")>]
        member _.buffer: ArrayBuffer = jsNative

        /// The length (in bytes) of a typed array.
        [<Emit("$0.byteLength")>]
        member _.byteLength: int = jsNative

        /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
        [<Emit("$0.byteOffset")>]
        member _.byteOffset: int = jsNative

        /// The length (in elements) of a typed array.
        [<Emit("$0.length")>]
        member _.length: int = jsNative

        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second and third arguments 
        /// start and end. The end argument is optional and defaults to the length of the array.
        [<Emit("$0.copyWithin($1...)")>]
        member _.copyWithin (targetStartIndex: int, start: int, ?end': int) : unit = jsNative

        /// The keys for each index in the array.
        [<Emit("$0.keys()")>]
        member _.keys () : seq<int> = jsNative

        /// Joins all elements of an array into a string.
        [<Emit("$0.join($1)")>]
        member _.join (separator: string) : string = jsNative

        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        [<Emit("$0.fill($1...)")>]
        member _.fill (value:'T, ?start': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.filter (f: 'T -> bool) : TypedArray<'T> = jsNative
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.filter (f: 'T -> int -> bool) : TypedArray<'T> = jsNative
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.filter (f: 'T -> int -> TypedArray<'T> -> bool) : TypedArray<'T> = jsNative
        
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        [<Emit("$0.find($1)")>]
        member _.find (f: 'T -> bool) : 'T option = jsNative
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element and index.
        [<Emit("$0.find($1)")>]
        member _.find (f: 'T -> int -> bool) : 'T option = jsNative
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.find($1)")>]
        member _.find (f: 'T -> int -> TypedArray<'T> -> bool) : 'T option = jsNative
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        [<Emit("$0.findIndex($1)")>]
        member _.findIndex (f: 'T -> bool) : int = jsNative
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element and index.
        [<Emit("$0.findIndex($1)")>]
        member _.findIndex (f: 'T -> int -> bool) : int = jsNative
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.findIndex($1)")>]
        member _.findIndex (f: 'T -> int -> TypedArray<'T> -> bool) : int = jsNative
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        member inline this.tryFindIndex (f: 'T -> bool) = 
            this.findIndex(f) |> fun i -> if i < 0 then None else Some i
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element and index.
        member inline this.tryFindIndex (f: 'T -> int -> bool) = 
            this.findIndex(f) |> fun i -> if i < 0 then None else Some i
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element, index, and array.
        member inline this.tryFindIndex (f: 'T -> int -> TypedArray<'T> -> bool) = 
            this.findIndex(f) |> fun i -> if i < 0 then None else Some i

        /// Executes a provided function once per array element.
        [<Emit("$0.forEach($1)")>]
        member _.forEach (f: 'T -> bool) : unit = jsNative
        /// Executes a provided function once per array element.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.forEach($1)")>]
        member _.forEach (f: 'T -> int -> bool) : unit = jsNative
        /// Executes a provided function once per array element.
        ///
        /// Provides the element and index.
        [<Emit("$0.forEach($1)")>]
        member _.forEach (f: 'T -> int -> TypedArray<'T> -> bool) : unit = jsNative

        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        [<Emit("$0.includes($1...)")>]
        member _.includes (searchElement:'T, ?fromIndex: int) : bool = jsNative

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        [<Emit("$0.indexOf($1...)")>]
        member _.indexOf (searchElement:'T, ?fromIndex: int) : int = jsNative

        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        [<Emit("$0.lastIndexOf($1...)")>]
        member _.lastIndexOf (searchElement:'T, ?fromIndex: int) : int = jsNative
        
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.map (f: 'T -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.mapWithIndex (f: 'T -> int -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.mapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) : TypedArray<'U> = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.reduce (f: 'State -> 'T -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.reduce (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.reduce (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.reduceRight (f: 'State -> 'T -> 'State, state:'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.reduceRight (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.reduceRight (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State = jsNative

        /// Reverses a typed array in place.
        [<Emit("$0.reverse()")>]
        member _.reverse () : TypedArray<'T> = jsNative

        /// Stores multiple values in the typed array, reading input values from a specified array.
        [<Emit("$0.set($1...)")>]
        member _.set (source: System.Array, ?offset: int) : unit = jsNative
        /// Stores multiple values in the typed array, reading input values from a specified array.
        [<Emit("$0.set($1...)")>]
        member _.set (source: #JS.TypedArray, ?offset: int) : unit = jsNative
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        [<Emit("$0.slice($1...)")>]
        member _.slice (?begin': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.some (f: 'T -> bool) : bool = jsNative
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.some (f: 'T -> int -> bool) : bool = jsNative
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.some (f: 'T -> int -> TypedArray<'T> -> bool) : bool = jsNative
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        [<Emit("$0.sort($1...)")>]
        member _.sort (?sortFunction: 'T -> 'T -> int) : TypedArray<'T> = jsNative
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        [<Emit("$0.subarray($1...)")>]
        member _.subarray (?begin': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Returns a sequence that contains the values for each index in the array.
        [<Emit("$0.values()")>]
        member _.values () : seq<'T> = jsNative

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<'T> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<'T>>()
            member _.filter (f: 'T -> bool) = unbox<JS.TypedArray<'T>>()
            member _.filter (f: 'T -> int -> bool) = unbox<JS.TypedArray<'T>>()
            member _.filter (f: 'T -> int -> JS.TypedArray<'T> -> bool) = unbox<JS.TypedArray<'T>>()
            member _.find (f: 'T -> bool) = unbox<'T option>()
            member _.find (f: 'T -> int -> bool) = unbox<'T option>()
            member _.find (f: 'T -> int -> JS.TypedArray<'T> -> bool) = unbox<'T option>()
            member _.findIndex (f: 'T -> bool) = 1
            member _.findIndex (f: 'T -> int -> bool) = 1
            member _.findIndex (f: 'T -> int -> JS.TypedArray<'T> -> bool) = 1
            member _.forEach (f: 'T -> bool) = ()
            member _.forEach (f: 'T -> int -> bool) = ()
            member _.forEach (f: 'T -> int -> JS.TypedArray<'T> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: 'T -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: 'T -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: 'T -> int -> JS.TypedArray<'T> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> 'T -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> 'T -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> 'T -> int -> JS.TypedArray<'T> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> 'T -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> 'T -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> 'T -> int -> JS.TypedArray<'T> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<'T>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<'T>>()
            member _.some (f: 'T -> bool) = true
            member _.some (f: 'T -> int -> bool) = true
            member _.some (f: 'T -> int -> JS.TypedArray<'T> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<'T>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<'T> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<'T>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase;RequireQualifiedAccess>]
    module TypedArray =
        /// Returns a sequence that contains the key/value pairs for each index in the array.
        let inline entries (ta: TypedArray<'T>) = ta.entries()
        
        /// The ArrayBuffer referenced by a TypedArray at construction time.
        let inline buffer (ta: TypedArray<'T>) = ta.buffer
        
        /// The length (in bytes) of a typed array.
        let inline byteLength (ta: TypedArray<'T>) = ta.byteLength
        
        /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
        let inline byteOffset (ta: TypedArray<'T>) = ta.byteOffset
        
        /// The length (in elements) of a typed array.
        let inline length (ta: TypedArray<'T>) = ta.length
        
        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second argument start.
        let inline copyWithin (targetStartIndex: int) (start: int) (ta: TypedArray<'T>) = ta.copyWithin(targetStartIndex, start)
        
        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second and third arguments 
        /// start and end.
        let inline copyWithinEnd (targetStartIndex: int) (start: int) (end': int) (ta: TypedArray<'T>) = ta.copyWithin(targetStartIndex, start, end')

        /// The keys for each index in the array.
        let inline keys (ta: TypedArray<'T>) = ta.keys
        
        /// Joins all elements of an array into a string.
        let inline join (sep: string) (ta: TypedArray<'T>) = ta.join(sep)
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fill (value: 'T) (ta: TypedArray<'T>) = ta.fill(value)
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillStart (value: 'T) (start': int) (ta: TypedArray<'T>) = ta.fill(value, start' = start')
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillEnd (value: 'T) (end': int) (ta: TypedArray<'T>) = ta.fill(value, end' = end')
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillStartEnd (value: 'T) (start': int) (end': int) (ta: TypedArray<'T>) = ta.fill(value, start', end')

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filter (f: 'T -> bool) (ta: TypedArray<'T>) = ta.filter(f)

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filterWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.filter(f)

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filterWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.filter(f)
        
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        let inline find (f: 'T -> bool) (ta: TypedArray<'T>) = ta.find(f)

        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element and index.
        let inline findWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.find(f)
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element, index, and array.
        let inline findWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.find(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        let inline findIndex (f: 'T -> bool) (ta: TypedArray<'T>) = ta.findIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element and index.
        let inline findIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.findIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element, index, and array.
        let inline findIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.findIndex(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        let inline tryFindIndex (f: 'T -> bool) (ta: TypedArray<'T>) = 
            ta.findIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element and index.
        let inline tryFindIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = 
            ta.findIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element, index, and array.
        let inline tryFindIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = 
            ta.findIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Executes a provided function once per array element.
        let inline forEach (f: 'T -> bool) (ta: TypedArray<'T>) = ta.forEach(f)
        
        /// Executes a provided function once per array element.
        ///
        /// Provides the element, index, and array.
        let inline forEachWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.forEach(f)
        
        /// Executes a provided function once per array element.
        ///
        /// Provides the element and index.
        let inline forEachWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.forEach(f)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includes (searchElement:'T) (ta: TypedArray<'T>) = ta.includes(searchElement)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includesFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.includes(searchElement)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOf (searchElement:'T) (ta: TypedArray<'T>) = ta.indexOf(searchElement)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOfFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.indexOf(searchElement, fromIndex)
        
        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOf (searchElement:'T) (ta: TypedArray<'T>) = ta.lastIndexOf(searchElement)
        
        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOfFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.lastIndexOf(searchElement, fromIndex)
        
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline map (f: 'T -> 'U) (ta: TypedArray<'T>) = ta.map(f)

        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline mapWithIndex (f: 'T -> int -> 'U) (ta: TypedArray<'T>) = ta.mapWithIndex(f)

        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline mapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) (ta: TypedArray<'T>) = ta.mapWithIndexArray(f)
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduce (f: 'State -> 'T -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.reduce(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduceWithIndex (f: 'State -> 'T -> int -> 'State, state: 'State) (ta: TypedArray<'T>) = ta.reduce(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduceWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) (ta: TypedArray<'T>) = ta.reduce(f, state)
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRight (f: 'State -> 'T -> 'State) (state:'State) (ta: TypedArray<'T>) = ta.reduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndex (f: 'State -> 'T -> int -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.reduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.reduceRight(f, state)
        
        /// Reverses a typed array in place.
        let inline reverse (ta: TypedArray<'T>) = ta.reverse()
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArray (source: System.Array) (ta: TypedArray<'T>) = ta.set(source)

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArrayOffset (source: System.Array) (offset: int) (ta: TypedArray<'T>) = ta.set(source, offset)

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArray (source: #JS.TypedArray) (ta: TypedArray<'T>) = ta.set(source)
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArrayOffset (source: #JS.TypedArray) (offset: int) (ta: TypedArray<'T>)= ta.set(source, offset)

        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeq (source: seq<'T>) (ta: TypedArray<'T>) = ta.set(unbox<System.Array> (ResizeArray source))
        
        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeqOffset (source: seq<'T>) (offset: int) (ta: TypedArray<'T>) = ta.set(unbox<System.Array> (ResizeArray source), offset = offset)
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline slice (begin': int) (end': int) (ta: TypedArray<'T>) = ta.slice(begin', end')

        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline sliceBegin (begin': int) (ta: TypedArray<'T>) = ta.slice(begin' = begin')
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline sliceEnd (end': int) (ta: TypedArray<'T>) = ta.slice(end' = end')
        
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline some (f: 'T -> bool) (ta: TypedArray<'T>) = ta.some(f)

        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline someWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.some(f)

        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline someWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.some(f)
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        let inline sort (ta: TypedArray<'T>) = ta.sort()
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        let inline sortBy (sortFunction: 'T -> 'T -> int) (ta: TypedArray<'T>) = ta.sort(sortFunction)

        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarray (begin': int) (end': int) (ta: TypedArray<'T>) = ta.subarray(begin', end')
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarrayBegin (begin': int) (ta: TypedArray<'T>) = ta.subarray(begin' = begin')
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarrayEnd (end': int) (ta: TypedArray<'T>) = ta.subarray(end' = end')
        
        /// Returns a sequence that contains the values for each index in the array.
        let inline values (ta: TypedArray<'T>) = ta.values()

    [<Erase>]
    type Int8Array private () = 
        inherit TypedArray<int8>()

        [<Emit("new Int8Array($0)")>]
        new (size: int) = Int8Array()
        [<Emit("new Int8Array($0)")>]
        new (typedArray: JS.TypedArray) = Int8Array()
        [<Emit("new Int8Array($0)")>]
        new (typedArray: TypedArray<int8>) = Int8Array()
        [<Emit("new Int8Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Int8Array()
        [<Emit("new Int8Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Int8Array()
        [<Emit("new Int8Array($0)")>]
        new (seq: seq<int8>) = Int8Array()
        
        /// The size in bytes of each element in the typed array.
        [<Emit("Int8Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative

        /// A string value of the typed array constructor name.
        [<Emit("Int8Array.name")>]
        static member name : string = jsNative

        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Int8Array) = unbox<Int8Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<int8> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<int8>>()
            member _.filter (f: int8 -> bool) = unbox<JS.TypedArray<int8>>()
            member _.filter (f: int8 -> int -> bool) = unbox<JS.TypedArray<int8>>()
            member _.filter (f: int8 -> int -> JS.TypedArray<int8> -> bool) = unbox<JS.TypedArray<int8>>()
            member _.find (f: int8 -> bool) = unbox<int8 option>()
            member _.find (f: int8 -> int -> bool) = unbox<int8 option>()
            member _.find (f: int8 -> int -> JS.TypedArray<int8> -> bool) = unbox<int8 option>()
            member _.findIndex (f: int8 -> bool) = 1
            member _.findIndex (f: int8 -> int -> bool) = 1
            member _.findIndex (f: int8 -> int -> JS.TypedArray<int8> -> bool) = 1
            member _.forEach (f: int8 -> bool) = ()
            member _.forEach (f: int8 -> int -> bool) = ()
            member _.forEach (f: int8 -> int -> JS.TypedArray<int8> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: int8 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int8 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int8 -> int -> JS.TypedArray<int8> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> int8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int8 -> int -> JS.TypedArray<int8> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int8 -> int -> JS.TypedArray<int8> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<int8>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<int8>>()
            member _.some (f: int8 -> bool) = true
            member _.some (f: int8 -> int -> bool) = true
            member _.some (f: int8 -> int -> JS.TypedArray<int8> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<int8>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<int8> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<int8>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Uint8Array private () = 
        inherit TypedArray<uint8>()
            
        [<Emit "new Uint8Array($0)">]
        new (size: int) = Uint8Array()
        [<Emit("new Uint8Array($0)")>]
        new (typedArray: JS.TypedArray) = Uint8Array()
        [<Emit("new Uint8Array($0)")>]
        new (typedArray: TypedArray<uint8>) = Uint8Array()
        [<Emit("new Uint8Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Uint8Array()
        [<Emit("new Uint8Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Uint8Array()
        [<Emit "new Uint8Array($0)">]
        new (seq: seq<uint8>) = Uint8Array()
    
        /// The size in bytes of each element in the typed array.
        [<Emit("Uint8Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Uint8Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Uint8Array) = unbox<Uint8Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<uint8> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> int -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.find (f: uint8 -> bool) = unbox<uint8 option>()
            member _.find (f: uint8 -> int -> bool) = unbox<uint8 option>()
            member _.find (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = unbox<uint8 option>()
            member _.findIndex (f: uint8 -> bool) = 1
            member _.findIndex (f: uint8 -> int -> bool) = 1
            member _.findIndex (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = 1
            member _.forEach (f: uint8 -> bool) = ()
            member _.forEach (f: uint8 -> int -> bool) = ()
            member _.forEach (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: uint8 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint8 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint8 -> int -> JS.TypedArray<uint8> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> uint8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint8 -> int -> JS.TypedArray<uint8> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> int -> JS.TypedArray<uint8> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<uint8>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<uint8>>()
            member _.some (f: uint8 -> bool) = true
            member _.some (f: uint8 -> int -> bool) = true
            member _.some (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<uint8>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<uint8> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<uint8>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Uint8ClampedArray private () = 
        inherit TypedArray<uint8>()
            
        [<Emit "new Uint8ClampedArray($0)">]
        new (size: int) = Uint8ClampedArray()
        [<Emit("new Uint8ClampedArray($0)")>]
        new (typedArray: JS.TypedArray) = Uint8ClampedArray()
        [<Emit("new Uint8ClampedArray($0)")>]
        new (typedArray: TypedArray<uint8>) = Uint8ClampedArray()
        [<Emit("new Uint8ClampedArray($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Uint8ClampedArray()
        [<Emit("new Uint8ClampedArray($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Uint8ClampedArray()
        [<Emit "new Uint8ClampedArray($0)">]
        new (seq: seq<uint8>) = Uint8ClampedArray()
        
        /// The size in bytes of each element in the typed array.
        [<Emit("Uint8ClampedArray.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Uint8ClampedArray.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Uint8ClampedArray) = unbox<Uint8ClampedArray> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<uint8> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> int -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.filter (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = unbox<JS.TypedArray<uint8>>()
            member _.find (f: uint8 -> bool) = unbox<uint8 option>()
            member _.find (f: uint8 -> int -> bool) = unbox<uint8 option>()
            member _.find (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = unbox<uint8 option>()
            member _.findIndex (f: uint8 -> bool) = 1
            member _.findIndex (f: uint8 -> int -> bool) = 1
            member _.findIndex (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = 1
            member _.forEach (f: uint8 -> bool) = ()
            member _.forEach (f: uint8 -> int -> bool) = ()
            member _.forEach (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: uint8 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint8 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint8 -> int -> JS.TypedArray<uint8> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> uint8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint8 -> int -> JS.TypedArray<uint8> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint8 -> int -> JS.TypedArray<uint8> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<uint8>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<uint8>>()
            member _.some (f: uint8 -> bool) = true
            member _.some (f: uint8 -> int -> bool) = true
            member _.some (f: uint8 -> int -> JS.TypedArray<uint8> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<uint8>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<uint8> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<uint8>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Int16Array private () = 
        inherit TypedArray<int16>()
            
        [<Emit "new Int16Array($0)">]
        new (size: int) = Int16Array()
        [<Emit("new Int16Array($0)")>]
        new (typedArray: JS.TypedArray) = Int16Array()
        [<Emit("new Int16Array($0)")>]
        new (typedArray: TypedArray<int16>) = Int16Array()
        [<Emit("new Int16Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Int16Array()
        [<Emit("new Int16Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Int16Array()
        [<Emit "new Int16Array($0)">]
        new (seq: seq<int16>) = Int16Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Int16Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Int16Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Int16Array) = unbox<Int16Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<int16> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<int16>>()
            member _.filter (f: int16 -> bool) = unbox<JS.TypedArray<int16>>()
            member _.filter (f: int16 -> int -> bool) = unbox<JS.TypedArray<int16>>()
            member _.filter (f: int16 -> int -> JS.TypedArray<int16> -> bool) = unbox<JS.TypedArray<int16>>()
            member _.find (f: int16 -> bool) = unbox<int16 option>()
            member _.find (f: int16 -> int -> bool) = unbox<int16 option>()
            member _.find (f: int16 -> int -> JS.TypedArray<int16> -> bool) = unbox<int16 option>()
            member _.findIndex (f: int16 -> bool) = 1
            member _.findIndex (f: int16 -> int -> bool) = 1
            member _.findIndex (f: int16 -> int -> JS.TypedArray<int16> -> bool) = 1
            member _.forEach (f: int16 -> bool) = ()
            member _.forEach (f: int16 -> int -> bool) = ()
            member _.forEach (f: int16 -> int -> JS.TypedArray<int16> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: int16 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int16 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int16 -> int -> JS.TypedArray<int16> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> int16 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int16 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int16 -> int -> JS.TypedArray<int16> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int16 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int16 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int16 -> int -> JS.TypedArray<int16> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<int16>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<int16>>()
            member _.some (f: int16 -> bool) = true
            member _.some (f: int16 -> int -> bool) = true
            member _.some (f: int16 -> int -> JS.TypedArray<int16> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<int16>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<int16> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<int16>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Uint16Array private () = 
        inherit TypedArray<uint16>()
            
        [<Emit "new Uint16Array($0)">]
        new (size: int) = Uint16Array()
        [<Emit("new Uint16Array($0)")>]
        new (typedArray: JS.TypedArray) = Uint16Array()
        [<Emit("new Uint16Array($0)")>]
        new (typedArray: TypedArray<uint16>) = Uint16Array()
        [<Emit("new Uint16Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Uint16Array()
        [<Emit("new Uint16Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Uint16Array()
        [<Emit "new Uint16Array($0)">]
        new (seq: seq<uint16>) = Uint16Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Uint16Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Uint16Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Uint16Array) = unbox<Uint16Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<uint16> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<uint16>>()
            member _.filter (f: uint16 -> bool) = unbox<JS.TypedArray<uint16>>()
            member _.filter (f: uint16 -> int -> bool) = unbox<JS.TypedArray<uint16>>()
            member _.filter (f: uint16 -> int -> JS.TypedArray<uint16> -> bool) = unbox<JS.TypedArray<uint16>>()
            member _.find (f: uint16 -> bool) = unbox<uint16 option>()
            member _.find (f: uint16 -> int -> bool) = unbox<uint16 option>()
            member _.find (f: uint16 -> int -> JS.TypedArray<uint16> -> bool) = unbox<uint16 option>()
            member _.findIndex (f: uint16 -> bool) = 1
            member _.findIndex (f: uint16 -> int -> bool) = 1
            member _.findIndex (f: uint16 -> int -> JS.TypedArray<uint16> -> bool) = 1
            member _.forEach (f: uint16 -> bool) = ()
            member _.forEach (f: uint16 -> int -> bool) = ()
            member _.forEach (f: uint16 -> int -> JS.TypedArray<uint16> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: uint16 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint16 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint16 -> int -> JS.TypedArray<uint16> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> uint16 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint16 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint16 -> int -> JS.TypedArray<uint16> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint16 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint16 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint16 -> int -> JS.TypedArray<uint16> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<uint16>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<uint16>>()
            member _.some (f: uint16 -> bool) = true
            member _.some (f: uint16 -> int -> bool) = true
            member _.some (f: uint16 -> int -> JS.TypedArray<uint16> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<uint16>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<uint16> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<uint16>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Int32Array private () = 
        inherit TypedArray<int>()
            
        [<Emit "new Int32Array($0)">]
        new (size: int) = Int32Array()
        [<Emit("new Int32Array($0)")>]
        new (typedArray: JS.TypedArray) = Int32Array()
        [<Emit("new Int32Array($0)")>]
        new (typedArray: TypedArray<int>) = Int32Array()
        [<Emit("new Int32Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Int32Array()
        [<Emit("new Int32Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Int32Array()
        [<Emit "new Int32Array($0)">]
        new (seq: seq<int>) = Int32Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Int32Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Int32Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Int32Array) = unbox<Int32Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<int> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<int>>()
            member _.filter (f: int -> bool) = unbox<JS.TypedArray<int>>()
            member _.filter (f: int -> int -> bool) = unbox<JS.TypedArray<int>>()
            member _.filter (f: int -> int -> JS.TypedArray<int> -> bool) = unbox<JS.TypedArray<int>>()
            member _.find (f: int -> bool) = unbox<int option>()
            member _.find (f: int -> int -> bool) = unbox<int option>()
            member _.find (f: int -> int -> JS.TypedArray<int> -> bool) = unbox<int option>()
            member _.findIndex (f: int -> bool) = 1
            member _.findIndex (f: int -> int -> bool) = 1
            member _.findIndex (f: int -> int -> JS.TypedArray<int> -> bool) = 1
            member _.forEach (f: int -> bool) = ()
            member _.forEach (f: int -> int -> bool) = ()
            member _.forEach (f: int -> int -> JS.TypedArray<int> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int -> int -> JS.TypedArray<int> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int -> int -> JS.TypedArray<int> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int -> int -> JS.TypedArray<int> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<int>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<int>>()
            member _.some (f: int -> bool) = true
            member _.some (f: int -> int -> bool) = true
            member _.some (f: int -> int -> JS.TypedArray<int> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<int>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<int> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<int>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Uint32Array private () = 
        inherit TypedArray<uint32>()
            
        [<Emit "new Uint32Array($0)">]
        new (size: int) = Uint32Array()
        [<Emit("new Uint32Array($0)")>]
        new (typedArray: JS.TypedArray) = Uint32Array()
        [<Emit("new Uint32Array($0)")>]
        new (typedArray: TypedArray<uint32>) = Uint32Array()
        [<Emit("new Uint32Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Uint32Array()
        [<Emit("new Uint32Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Uint32Array()
        [<Emit "new Uint32Array($0)">]
        new (seq: seq<uint32>) = Uint32Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Uint32Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Uint32Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Uint32Array) = unbox<Uint32Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<uint32> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<uint32>>()
            member _.filter (f: uint32 -> bool) = unbox<JS.TypedArray<uint32>>()
            member _.filter (f: uint32 -> int -> bool) = unbox<JS.TypedArray<uint32>>()
            member _.filter (f: uint32 -> int -> JS.TypedArray<uint32> -> bool) = unbox<JS.TypedArray<uint32>>()
            member _.find (f: uint32 -> bool) = unbox<uint32 option>()
            member _.find (f: uint32 -> int -> bool) = unbox<uint32 option>()
            member _.find (f: uint32 -> int -> JS.TypedArray<uint32> -> bool) = unbox<uint32 option>()
            member _.findIndex (f: uint32 -> bool) = 1
            member _.findIndex (f: uint32 -> int -> bool) = 1
            member _.findIndex (f: uint32 -> int -> JS.TypedArray<uint32> -> bool) = 1
            member _.forEach (f: uint32 -> bool) = ()
            member _.forEach (f: uint32 -> int -> bool) = ()
            member _.forEach (f: uint32 -> int -> JS.TypedArray<uint32> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: uint32 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint32 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint32 -> int -> JS.TypedArray<uint32> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> uint32 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint32 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint32 -> int -> JS.TypedArray<uint32> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint32 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint32 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint32 -> int -> JS.TypedArray<uint32> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<uint32>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<uint32>>()
            member _.some (f: uint32 -> bool) = true
            member _.some (f: uint32 -> int -> bool) = true
            member _.some (f: uint32 -> int -> JS.TypedArray<uint32> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<uint32>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<uint32> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<uint32>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Float32Array private () = 
        inherit TypedArray<float32>()
            
        [<Emit "new Float32Array($0)">]
        new (size: int) = Float32Array()
        [<Emit("new Float32Array($0)")>]
        new (typedArray: JS.TypedArray) = Float32Array()
        [<Emit("new Float32Array($0)")>]
        new (typedArray: TypedArray<float32>) = Float32Array()
        [<Emit("new Float32Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Float32Array()
        [<Emit("new Float32Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Float32Array()
        [<Emit "new Float32Array($0)">]
        new (seq: seq<float32>) = Float32Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Float32Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Float32Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Float32Array) = unbox<Float32Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<float32> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<float32>>()
            member _.filter (f: float32 -> bool) = unbox<JS.TypedArray<float32>>()
            member _.filter (f: float32 -> int -> bool) = unbox<JS.TypedArray<float32>>()
            member _.filter (f: float32 -> int -> JS.TypedArray<float32> -> bool) = unbox<JS.TypedArray<float32>>()
            member _.find (f: float32 -> bool) = unbox<float32 option>()
            member _.find (f: float32 -> int -> bool) = unbox<float32 option>()
            member _.find (f: float32 -> int -> JS.TypedArray<float32> -> bool) = unbox<float32 option>()
            member _.findIndex (f: float32 -> bool) = 1
            member _.findIndex (f: float32 -> int -> bool) = 1
            member _.findIndex (f: float32 -> int -> JS.TypedArray<float32> -> bool) = 1
            member _.forEach (f: float32 -> bool) = ()
            member _.forEach (f: float32 -> int -> bool) = ()
            member _.forEach (f: float32 -> int -> JS.TypedArray<float32> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: float32 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: float32 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: float32 -> int -> JS.TypedArray<float32> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> float32 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> float32 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> float32 -> int -> JS.TypedArray<float32> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float32 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float32 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float32 -> int -> JS.TypedArray<float32> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<float32>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<float32>>()
            member _.some (f: float32 -> bool) = true
            member _.some (f: float32 -> int -> bool) = true
            member _.some (f: float32 -> int -> JS.TypedArray<float32> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<float32>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<float32> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<float32>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Float64Array private () = 
        inherit TypedArray<float>()
            
        [<Emit "new Float64Array($0)">]
        new (size: int) = Float64Array()
        [<Emit("new Float64Array($0)")>]
        new (typedArray: JS.TypedArray) = Float64Array()
        [<Emit("new Float64Array($0)")>]
        new (typedArray: TypedArray<float>) = Float64Array()
        [<Emit("new Float64Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Float64Array()
        [<Emit("new Float64Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Float64Array()
        [<Emit "new Float64Array($0)">]
        new (seq: seq<float>) = Float64Array()
            
        /// The size in bytes of each element in the typed array.
        [<Emit("Float64Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("Float64Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.Float64Array) = unbox<Float64Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<float> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<float>>()
            member _.filter (f: float -> bool) = unbox<JS.TypedArray<float>>()
            member _.filter (f: float -> int -> bool) = unbox<JS.TypedArray<float>>()
            member _.filter (f: float -> int -> JS.TypedArray<float> -> bool) = unbox<JS.TypedArray<float>>()
            member _.find (f: float -> bool) = unbox<float option>()
            member _.find (f: float -> int -> bool) = unbox<float option>()
            member _.find (f: float -> int -> JS.TypedArray<float> -> bool) = unbox<float option>()
            member _.findIndex (f: float -> bool) = 1
            member _.findIndex (f: float -> int -> bool) = 1
            member _.findIndex (f: float -> int -> JS.TypedArray<float> -> bool) = 1
            member _.forEach (f: float -> bool) = ()
            member _.forEach (f: float -> int -> bool) = ()
            member _.forEach (f: float -> int -> JS.TypedArray<float> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: float -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: float -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: float -> int -> JS.TypedArray<float> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> float -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> float -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> float -> int -> JS.TypedArray<float> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> float -> int -> JS.TypedArray<float> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<float>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<float>>()
            member _.some (f: float -> bool) = true
            member _.some (f: float -> int -> bool) = true
            member _.some (f: float -> int -> JS.TypedArray<float> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<float>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<float> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<float>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type BigInt64Array private () = 
        inherit TypedArray<bigint>()
            
        [<Emit "new BigInt64Array($0)">]
        new (size: int) = BigInt64Array()
        [<Emit("new BigInt64Array($0)")>]
        new (typedArray: JS.TypedArray) = BigInt64Array()
        [<Emit("new BigInt64Array($0)")>]
        new (typedArray: TypedArray<bigint>) = BigInt64Array()
        [<Emit("new BigInt64Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = BigInt64Array()
        [<Emit("new BigInt64Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = BigInt64Array()
        [<Emit "new BigInt64Array($0)">]
        new (seq: seq<bigint>) = BigInt64Array()
    
        /// The size in bytes of each element in the typed array.
        [<Emit("BigInt64Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("BigInt64Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.BigInt64Array) = unbox<BigInt64Array> a

        // All of the interfaces are erased, so implementation doesn't actually matter

        interface JS.ArrayBufferView with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1

        interface JS.TypedArray with
            member _.buffer = unbox<JS.ArrayBuffer>()
            member _.byteLength = 1
            member _.byteOffset = 1
            member _.length = 1
            member _.copyWithin (targetStartIndex, start, ?end') = ()
            member _.entries () = box ()
            member _.keys () = box ()
            member _.join sep = ""

        interface JS.TypedArray<bigint> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<bigint>>()
            member _.filter (f: bigint -> bool) = unbox<JS.TypedArray<bigint>>()
            member _.filter (f: bigint -> int -> bool) = unbox<JS.TypedArray<bigint>>()
            member _.filter (f: bigint -> int -> JS.TypedArray<bigint> -> bool) = unbox<JS.TypedArray<bigint>>()
            member _.find (f: bigint -> bool) = unbox<bigint option>()
            member _.find (f: bigint -> int -> bool) = unbox<bigint option>()
            member _.find (f: bigint -> int -> JS.TypedArray<bigint> -> bool) = unbox<bigint option>()
            member _.findIndex (f: bigint -> bool) = 1
            member _.findIndex (f: bigint -> int -> bool) = 1
            member _.findIndex (f: bigint -> int -> JS.TypedArray<bigint> -> bool) = 1
            member _.forEach (f: bigint -> bool) = ()
            member _.forEach (f: bigint -> int -> bool) = ()
            member _.forEach (f: bigint -> int -> JS.TypedArray<bigint> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: bigint -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: bigint -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: bigint -> int -> JS.TypedArray<bigint> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> bigint -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> bigint -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> bigint -> int -> JS.TypedArray<bigint> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> bigint -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> bigint -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> bigint -> int -> JS.TypedArray<bigint> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<bigint>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<bigint>>()
            member _.some (f: bigint -> bool) = true
            member _.some (f: bigint -> int -> bool) = true
            member _.some (f: bigint -> int -> JS.TypedArray<bigint> -> bool) = true
            member _.sort (?sortFunction) = true
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<bigint>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<bigint> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<bigint>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Global>]
    type console =
        [<Emit("$0.assert($1...)")>]
        member _.assert' (test: bool, [<ParamArray>] optionalParams: obj []) : unit = jsNative
        [<Emit("$0.assert($1...)")>]
        member _.assert' (test: bool, message: string, [<ParamArray>] optionalParams: obj []) : unit = jsNative
        member _.clear () : unit = jsNative
        member _.count (?countTitle: string) : unit = jsNative
        member _.debug ([<ParamArray>] items: obj []) : unit = jsNative
        member _.dir (value: obj, [<ParamArray>] optionalParams: obj []) : unit = jsNative
        member _.dirxml (value: obj) : unit = jsNative
        member _.error ([<ParamArray>] items: obj []) : unit = jsNative
        member _.group (?groupTitle: string) : unit = jsNative
        member _.groupCollapsed (?groupTitle: string) : unit = jsNative
        member _.groupEnd () : unit = jsNative
        member _.info ([<ParamArray>] items: obj []) : unit = jsNative
        member _.log ([<ParamArray>] items: obj []) : unit = jsNative
        member _.profile (?reportName: string) : unit = jsNative
        member _.profileEnd () : unit = jsNative
        member _.time (?timerName: string) : unit = jsNative
        member _.timeEnd (?timerName: string) : unit = jsNative
        member _.trace ([<ParamArray>] items: obj []) : unit = jsNative
        member _.warn ([<ParamArray>] items: obj []) : unit = jsNative
        member _.table (?data: obj) : unit = jsNative

[<Erase>]
type JS =
    [<Emit("new RegExp($0...)")>] 
    static member RegExp (pattern: string, ?flags: string) : Regex = jsNative

[<AutoOpen;Erase;EditorBrowsable(EditorBrowsableState.Never)>]
module JSExtensions =
    type JS.Object with
        /// Casts an object to a type.
        member inline this.as'<'T> () = unbox<'T> this

        /// Returns a list of all properties (including non-enumerable properties except for 
        /// those which use Symbol) found directly in a given object.
        static member inline getOwnPropertyNames (o: obj) = JS.Object.getOwnPropertyNames(o) |> List.ofSeq

        /// Returns a list of a given object's own enumerable property names, 
        /// iterated in the same order that a normal loop would.
        static member inline keys (o: obj) = JS.Object.keys(o) |> List.ofSeq

    type JS.Promise<'T> with
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a thenable (i.e. has a "then" 
        /// method), the returned promise will "follow" that thenable, adopting its eventual state; otherwise the 
        /// returned promise will be fulfilled with the value. This function flattens nested layers of promise-like 
        /// objects (e.g. a promise that resolves to a promise that resolves to something) into a single layer.
        [<Emit("Promise.resolve($0)")>]
        static member resolve (value: 'T) : JS.Promise<'T> = jsNative
    
    type JS.TypedArray<'T> with
        /// Stores multiple values in the typed array, reading input values from a given sequence.
        member inline this.set (source: seq<'T>, ?offset: int) : unit = this.set(unbox<System.Array> (ResizeArray source), ?offset = offset)

    type Promise.PromiseBuilder with
        member inline this.Bind(a: Async<'T>, f: 'T -> Fable.Core.JS.Promise<'R>) = this.Bind(Async.StartAsPromise a, f)
        member inline this.Bind(a: Async<'T>, f: 'T -> Async<'R>) = this.Bind(Async.StartAsPromise a, f >> Async.StartAsPromise)
        member inline this.Bind(p: Fable.Core.JS.Promise<'T>, f: 'T -> Async<'R>) = this.Bind(p, f >> Async.StartAsPromise)

        member inline this.ReturnFrom(a: Async<'T>) = this.ReturnFrom(Async.StartAsPromise a)

    type AsyncBuilder with
        member inline this.Bind (a: Fable.Core.JS.Promise<'T>, f: 'T -> Async<'T>) = this.Bind(Async.AwaitPromise a, f)
        member inline this.Bind (a: Async<'T>, f: 'T -> Fable.Core.JS.Promise<'T>) = this.Bind(a, f >> Async.AwaitPromise)
        member inline this.Bind (p: Fable.Core.JS.Promise<'T>, f: 'T -> Fable.Core.JS.Promise<'T>) = this.Bind(Async.AwaitPromise p, f >> Async.AwaitPromise)
        
        member inline this.ReturnFrom (p: JS.Promise<unit>) = this.ReturnFrom(Async.AwaitPromise p)
