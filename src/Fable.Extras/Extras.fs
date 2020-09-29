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
        [<Emit("$0.size")>]
        member _.Size: int = jsNative

        /// Removes all elements.
        [<Emit("$0.clear()")>]
        member _.Clear () : unit = jsNative

        /// Removes the specified element by key.
        [<Emit("$0.delete($1)")>]
        member _.Delete (key: 'K) : bool = jsNative

        /// Returns a sequence of key value pairs in insertion order.
        [<Emit("$0.entries()")>]
        member _.Entries () : seq<'K * 'V> = jsNative
        
        /// Applies the given function once per each key value pair in insertion order.
        [<Emit("$0.forEach($1...)")>]
        member _.ForEach (callbackfn: 'V->'K->Map<'K, 'V>->unit, ?thisArg: obj) : unit = jsNative

        /// Returns the value for the specified key.
        [<Emit("$0.get($1)")>]
        member _.Get (key: 'K) : 'V option = jsNative

        /// Returns a boolean indicating whether an element with the specified key exists or not.
        [<Emit("$0.has($1)")>]
        member _.Has (key: 'K) : bool = jsNative

        /// Returns a sequence of keys in insertion order.
        [<Emit("$0.keys()")>]
        member _.Keys () : seq<'K> = jsNative

        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        [<Emit("$0.set($1...)")>]
        member _.Set (key: 'K, ?value: 'V) : Map<'K, 'V> = jsNative

        /// Returns a sequence of values in insertion order.
        [<Emit("$0.values()")>]
        member _.Values () : seq<'V> = jsNative

        interface JS.Map<'K,'V> with
            member this.size = this.Size
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.entries () = this.Entries()
            member this.forEach (callback, ?thisArg) = this.ForEach(callback, ?thisArg = thisArg)
            member this.get k = this.Get k |> Option.get
            member this.has k = this.Has k
            member this.keys () = this.Keys()
            member this.set (key, ?value) = upcast this.Set(key, ?value = value)
            member this.values () = this.Values()
    
    [<Erase;RequireQualifiedAccess>]
    module Map =
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (m: Map<'K,'V>) = m.Set(key,value)
        
        /// Removes all elements.
        let inline clear (m: Map<'K,'V>) = m.Clear()
        
        /// Removes the specified element by key.
        let inline delete (key: 'K) (m: Map<'K,'V>) = m.Delete(key)
        
        /// Returns a sequence of key value pairs in insertion order.
        let inline entries (m: Map<'K,'V>) = m.Entries()

        /// Returns the value for the specified key.
        let inline get (key: 'K) (m: Map<'K,'V>) = m.Get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (m: Map<'K,'V>) = m.Has(key)
        
        /// Returns a sequence of keys in insertion order.
        let inline keys (m: Map<'K,'V>) = m.Keys()
        
        /// The number of elements.
        let inline size (m: Map<'K,'V>) = m.Size
        
        /// Returns a sequence of values in insertion order.
        let inline values (m: Map<'K,'V>) = m.Values()

    /// Lets you store weakly held objects in a collection.
    [<Global>]
    type WeakMap<'K,'V> (?iterable: seq<'K * 'V>) =
        /// Removes all elements.
        [<Emit("$0.clear()")>]
        member _.Clear () : unit = jsNative
        
        /// Removes the specified element by key.
        [<Emit("$0.delete($1)")>]
        member _.Delete (key: 'K) : bool = jsNative
        
        /// Returns the value for the specified key.
        [<Emit("$0.get($1)")>]
        member _.Get (key: 'K) : 'V option = jsNative
        
        /// Returns a boolean indicating whether an element with the specified key exists or not.
        [<Emit("$0.has($1)")>]
        member _.Has (key: 'K) : bool = jsNative
        
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        [<Emit("$0.set($1...)")>]
        member _.Set (key: 'K, ?value: 'V) : WeakMap<'K, 'V> = jsNative
    
        interface JS.WeakMap<'K,'V> with
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.get k = this.Get k |> Option.get
            member this.has k = this.Has k
            member this.set (key, ?value) = upcast this.Set(key, ?value = value)

    [<Erase;RequireQualifiedAccess>]
    module WeakMap =
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (wm: WeakMap<'K,'V>) = wm.Set(key,value)
        
        /// Removes all elements.
        let inline clear (wm: WeakMap<'K,'V>) = wm.Clear()
        
        /// Removes the specified element by key.
        let inline delete (key: 'K) (wm: WeakMap<'K,'V>) = wm.Delete(key)
        
        /// Returns the value for the specified key.
        let inline get (key: 'K) (wm: WeakMap<'K,'V>) = wm.Get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (wm: WeakMap<'K,'V>) = wm.Has(key)

    /// Lets you store unique values of any type, whether primitive values or object references.
    [<Global>]
    type Set<'T> (?iterable: seq<'T>) =
        /// The number of elements.
        [<Emit("$0.size")>]
        member _.Size: int = jsNative

        //// Appends a new element.
        [<Emit("$0.add($1)")>]
        member _.Add (value: 'T) : Set<'T> = jsNative
        
        /// Removes all elements.
        [<Emit("$0.clear()")>]
        member _.Clear () : unit = jsNative
        
        /// Removes the specified element.
        [<Emit("$0.delete($1)")>]
        member _.Delete (value: 'T) : bool = jsNative

        /// Returns a sequence of values in tupled form, use the values 
        /// method to get a sequence of just the value.
        [<Emit("$0.entries()")>]
        member _.Entries () : seq<'T * 'T> = jsNative
        
        /// Applies the given function once per each value.
        [<Emit("$0.forEach($1...)")>]
        member _.ForEach (callbackfn: 'T -> 'T -> Set<'T> -> unit, ?thisArg: obj) : unit = jsNative

        /// Returns a boolean indicating whether an element with the specified value exists.
        [<Emit("$0.has($1)")>]
        member _.Has (value: 'T) : bool = jsNative

        /// Returns a sequence of values.
        ///
        /// This is an alias for the values method.
        [<Emit("$0.keys()")>]
        member _.Keys () : seq<'T> = jsNative
        
        /// Returns a sequence of values.
        [<Emit("$0.values()")>]
        member _.Values () : seq<'T> = jsNative
    
        interface JS.Set<'T> with
            member this.size = this.Size
            member this.add value = upcast this.Add(value)
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.entries () = this.Entries()
            member this.forEach (callback, ?thisArg) = this.ForEach(callback, ?thisArg = thisArg)
            member this.has k = this.Has k
            member this.keys () = this.Keys()
            member this.values () = this.Values()

    [<Erase;RequireQualifiedAccess>]
    module Set =
        /// Appends a new element.
        let inline add (value: 'T) (s: Set<'T>) = s.Add(value)
        
        /// Removes all elements.
        let inline clear (s: Set<'T>) = s.Clear()
        
        /// Removes the specified element.
        let inline delete (value: 'T) (s: Set<'T>) = s.Delete(value)
        
        /// Returns a sequence of values in tupled form, use the values 
        /// method to get a sequence of just the value.
        let inline entries (s: Set<'T>) = s.Entries()
        
        /// Applies the given function once per each value.
        let inline forEach (callbackFn: 'T -> 'T -> Set<'T> -> unit) (s: Set<'T>) = s.ForEach(callbackFn)
        
        /// Applies the given function once per each value.
        let inline forEachWithThis (callbackFn: 'T -> 'T -> Set<'T> -> unit) (thisArg: obj) (s: Set<'T>) = s.ForEach(callbackFn, thisArg = thisArg)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (s: Set<'T>) = s.Has(value)
        
        /// Returns a sequence of values.
        ///
        /// This is an alias for the values method.
        let inline keys (s: Set<'T>) = s.Keys()
        
        /// The number of elements.
        let inline size (s: Set<'T>) = s.Size
        
        /// Returns a sequence of values.
        let inline values (s: Set<'T>) = s.Values()
        
    /// Stores weakly held objects in a collection.
    [<Global>]
    type WeakSet<'T> (?iterable: seq<'T>) =
        [<Emit("$0")>]
        new (ws: JS.WeakSet<'T>) = WeakSet<'T>()

        //// Appends a new element.
        [<Emit("$0.add($1)")>]
        member _.Add (value: 'T) : WeakSet<'T> = jsNative
        
        /// Removes all elements.
        [<Emit("$0.clear()")>]
        member _.Clear () : unit = jsNative
        
        /// Removes the specified element.
        [<Emit("$0.delete($1)")>]
        member _.Delete (value: 'T) : bool = jsNative
        
        /// Returns a boolean indicating whether an element with the specified value exists.
        [<Emit("$0.has($1)")>]
        member _.Has (value: 'T) : bool = jsNative

        interface JS.WeakSet<'T> with
            member this.add value = upcast this.Add(value)
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.has k = this.Has k
    
    [<Erase;RequireQualifiedAccess>]
    module WeakSet =
        /// Appends a new element.
        let inline add (value: 'T) (ws: WeakSet<'T>) = ws.Add(value)
        
        /// Removes all elements.
        let inline clear (ws: WeakSet<'T>) = ws.Clear()
        
        /// Removes the specified element.
        let inline delete (value: 'T) (ws: WeakSet<'T>) = ws.Delete(value)
        
        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (ws: WeakSet<'T>) = ws.Has(value)


    /// Describes the configuration of a specific property on a given object.
    [<Global>]
    type PropertyDescriptor<'T> [<Emit("{}")>] () =
        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        ///
        /// Defaults to false.
        member _.Configurable
            with [<Emit("$0.configurable")>] get () : bool option = jsNative
            and [<Emit("$0.configurable = $2")>] set (x: bool option) = jsNative

        /// True if and only if this property shows up during enumeration 
        /// of the properties on the corresponding object.
        ///
        /// Defaults to false.
        member _.Enumerable
            with [<Emit("$0.enumerable")>] get () : bool option = jsNative
            and [<Emit("$0.enumerable = $2")>] set (x: bool option) = jsNative

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
        member _.Get
            with [<Emit("$0.get")>] get () : (unit -> 'T) option = jsNative
            and [<Emit("$0.get = $2")>] set (x: (unit -> 'T) option) = jsNative

        /// A function which serves as a setter for the property, or undefined if there is no 
        /// setter. 
        ///
        /// When the property is assigned, this function is called with one argument (the value 
        /// being assigned to the property) and with this set to the object through which the 
        /// property is assigned.
        ///
        /// Defaults to undefined.
        member _.Set
            with [<Emit("$0.set")>] get () : ('T -> unit) option = jsNative
            and [<Emit("$0.set = $2")>] set (x: ('T -> unit) option) = jsNative

        /// The value associated with the property. Can be any valid JavaScript 
        /// value (number, object, function, etc).
        ///
        /// Defaults to None.
        member _.Value
            with [<Emit("$0.value")>] get () : 'T option  = jsNative
            and [<Emit("$0.value = $2")>] set (x: 'T option) = jsNative

        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        ///
        /// Defaults to false.
        member _.Writable
            with [<Emit("$0.writable")>] get () : bool option = jsNative
            and [<Emit("$0.writable = $2")>] set (x: bool option) = jsNative

        interface JS.PropertyDescriptor with
            member this.configurable
                with get () = this.Configurable
                and set x = this.Configurable <- x
            member this.enumerable
                with get () = this.Enumerable
                and set x = this.Enumerable <- x
            member this.get () = box (this.Get)
            member this.set x = this.Set <- (unbox<('T -> unit) option> x)
            member this.value
                with get () = unbox this.Value
                and set x = this.Value <- unbox x
            member this.writable
                with get () = this.Writable
                and set x = this.Writable <- x
    
    [<Erase;RequireQualifiedAccess>]
    module PropertyDescriptor =
        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        ///
        /// Defaults to false.
        let inline configurable (pd: PropertyDescriptor<'T>) = pd.Configurable

        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        let inline enumerable (pd: PropertyDescriptor<'T>) = pd.Enumerable

        /// Indicates whether the object has the specified property 
        /// as its own property (as opposed to inheriting it).
        let inline get (pd: PropertyDescriptor<'T>) = pd.Get
        
        /// Checks if an object exists in another object's prototype chain.
        let inline set (pd: PropertyDescriptor<'T>) = pd.Set

        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        let inline value (pd: PropertyDescriptor<'T>) = pd.Value
        
        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        ///
        /// Defaults to false.
        let inline writable (pd: PropertyDescriptor<'T>) = pd.Writable

        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        ///
        /// Defaults to false.
        let inline setConfigurable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Configurable <- Some b
            pd

        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        let inline setEnumerable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Enumerable <- Some b
            pd

        /// Indicates whether the object has the specified property 
        /// as its own property (as opposed to inheriting it).
        let inline setGet (f: unit -> 'T) (pd: PropertyDescriptor<'T>) = 
            pd.Get <- Some f
            pd

        /// Checks if an object exists in another object's prototype chain.
        let inline setSet (f: 'T -> unit) (pd: PropertyDescriptor<'T>) = 
            pd.Set <- Some f
            pd

        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        let inline setValue (v: 'T) (pd: PropertyDescriptor<'T>) = 
            pd.Value <- Some v
            pd

        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        ///
        /// Defaults to false.
        let inline setWritable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Writable <- Some b
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
        /// properties from being changed.
        static member freeze<'T when 'T : not struct> (o: 'T) : 'T = jsNative

        /// Returns an object describing the configuration of a specific property on a given object 
        /// (that is, one directly present on an object and not in the object's prototype chain).
        static member getOwnPropertyDescriptor<'T> (o: obj) (p: string) : PropertyDescriptor<'T> option = jsNative

        /// Returns all own property descriptors of a given object.
        [<Emit("new Map(Object.entries(Object.getOwnPropertyDescriptors($0)))")>]
        static member getOwnPropertyDescriptors (o: 'T) : Map<string,PropertyDescriptor<obj>> = jsNative

        /// Returns the prototype (i.e. the value of the internal [[Prototype]] property) of the specified object.
        static member getPrototypeOf (o: 'T) : obj = jsNative
        
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
            
        [<Emit("$0.getDate()")>]
        member _.GetDate () : int = jsNative
        
        [<Emit("$0.getDay()")>]
        member _.GetDay () : DayOfWeek = jsNative

        [<Emit("$0.getFullYear()")>]
        member _.GetFullYear () : int = jsNative
        
        [<Emit("$0.getHours()")>]
        member _.GetHours () : int = jsNative
        
        [<Emit("$0.getMilliseconds()")>]
        member _.GetMilliseconds () : int = jsNative

        [<Emit("$0.getMinutes()")>]
        member _.GetMinutes () : int = jsNative

        [<Emit("$0.getMonth()")>]
        member _.GetMonth () : int = jsNative
        
        [<Emit("$0.getSeconds()")>]
        member _.GetSeconds () : int = jsNative

        [<Emit("$0.getTime()")>]
        member _.GetTime () : int64 = jsNative
        
        [<Emit("$0.getTimezoneOffset()")>]
        member _.GetTimezoneOffset () : int = jsNative

        [<Emit("$0.getUTCDate()")>]
        member _.GetUTCDate () : int = jsNative

        [<Emit("$0.getUTCDay()")>]
        member _.GetUTCDay () : DayOfWeek = jsNative

        [<Emit("$0.getUTCFullYear()")>]
        member _.GetUTCFullYear () : int = jsNative

        [<Emit("$0.getUTCHours()")>]
        member _.GetUTCHours () : int = jsNative
        
        [<Emit("$0.getUTCMilliseconds()")>]
        member _.GetUTCMilliseconds () : int = jsNative

        [<Emit("$0.getUTCMinutes()")>]
        member _.GetUTCMinutes () : int = jsNative

        [<Emit("$0.getUTCMonth()")>]
        member _.GetUTCMonth () : int = jsNative
        
        [<Emit("$0.getUTCSeconds()")>]
        member _.GetUTCSeconds () : int = jsNative
        
        [<Emit("new Date($0.setDate($1))")>]
        member _.SetDate (date: int) : Date = jsNative
        
        [<Emit("new Date($0.setFullYear($1...))")>]
        member _.SetFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setHours($1...))")>]
        member _.SetHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMilliseconds($1))")>]
        member _.SetMilliseconds (ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMinutes($1...))")>]
        member _.SetMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setMonth($1...))")>]
        member _.SetMonth (month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setSeconds($1...))")>]
        member _.SetSeconds (sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setTime($1))")>]
        member _.SetTime (time: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCDate($1))")>]
        member _.SetUTCDate (date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCFullYear($1...))")>]
        member _.SetUTCFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCHours($1...))")>]
        member _.SetUTCHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMilliseconds($1))")>]
        member _.SetUTCMilliseconds (ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMinutes($1...))")>]
        member _.SetUTCMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCMonth($1...))")>]
        member _.SetUTCMonth (month: int, ?date: int) : Date = jsNative
        
        [<Emit("new Date($0.setUTCSeconds($1...))")>]
        member _.SetUTCSeconds (sec: int, ?ms: int) : Date = jsNative
        
        [<Emit("$0.toDateString()")>]
        member _.ToDateString () : string = jsNative

        [<Emit("$0.toJSON()")>]
        member _.ToJSON () : string = jsNative

        [<Emit("$0.toLocaleDateString()")>]
        member _.ToLocaleDateString () : string = jsNative

        [<Emit("$0.toLocaleString()")>]
        member _.ToLocaleString () : string = jsNative

        [<Emit("$0.toLocaleTimeString()")>]
        member _.ToLocaleTimeString () : string = jsNative

        [<Emit("$0.toISOString()")>]
        member _.ToISOString () : string = jsNative

        [<Emit("$0.toString()")>]
        override _.ToString () : string = jsNative

        [<Emit("$0.toTimeString()")>]
        member _.ToTimeString () : string = jsNative

        [<Emit("$0.toUTCString()")>]
        member _.ToUTCString () : string = jsNative

        [<Emit("$0.valueOf()")>]
        member _.ValueOf () : int64 = jsNative
    
        /// Like Date.now(), but returns a string value of the date.
        [<Emit("Date()")>]
        static member invoke () : string = jsNative
    
        /// Parses a string representation of a date and returns the ticks.
        static member parse (s: string) : int64 = jsNative
    
        /// Accepts parameters similar to the Date constructor, but treats them as UTC in ticks.
        static member UTC (year: int, month: int, ?date: int, ?hours: int, ?minutes: int, ?seconds: int, ?ms: int) : int64 = jsNative
    
        /// Returns the current time in ticks.
        static member now () : int64 = jsNative

    [<Erase;RequireQualifiedAccess>]
    module Date =
        let inline getDate (d: Date) = d.GetDate()
        
        let inline getDay (d: Date) = d.GetDay()
        
        let inline getFullYear (d: Date) = d.GetFullYear()
        
        let inline getHours (d: Date) = d.GetHours()
        
        let inline getMilliseconds (d: Date) = d.GetMilliseconds()
        
        let inline getMinutes (d: Date) = d.GetMinutes()
        
        let inline getMonth (d: Date) = d.GetMonth()
        
        let inline getSeconds (d: Date) = d.GetSeconds()
        
        let inline getTime (d: Date) = d.GetTime()
        
        let inline getTimezoneOffset (d: Date) = d.GetTimezoneOffset()
        
        let inline getUTCDate (d: Date) = d.GetUTCDate()
        
        let inline getUTCDay (d: Date) = d.GetUTCDay()
        
        let inline getUTCFullYear (d: Date) = d.GetUTCFullYear()
        
        let inline getUTCHours (d: Date) = d.GetUTCHours()
        
        let inline getUTCMilliseconds (d: Date) = d.GetUTCMilliseconds()
        
        let inline getUTCMinutes (d: Date) = d.GetUTCMinutes()
        
        let inline getUTCMonth (d: Date) = d.GetUTCMonth()
        
        let inline getUTCSeconds (d: Date) = d.GetUTCSeconds()
        
        let inline setDate (date: int) (d: Date) = d.SetDate(date)
        
        let inline setFullYear (year: int) (d: Date) = d.SetFullYear(year)
        let inline setFullYearM (year: int) (month: int) (d: Date) = d.SetFullYear(year, month = month)
        let inline setFullYearD (year: int) (date: int) (d: Date) = d.SetFullYear(year, date = date)
        let inline setFullYearMD (year: int) (month: int) (date: int) (d: Date) = d.SetFullYear(year, month, date)
        
        let inline setHours (hours: int) (d: Date) = d.SetHours(hours)
        let inline setHoursM (hours: int) (min: int) (d: Date) = d.SetHours(hours, min = min)
        let inline setHoursMS (hours: int) (min: int) (sec: int) (d: Date) = d.SetHours(hours, min = min, sec = sec)
        let inline setHoursMSM (hours: int) (min: int) (sec: int) (ms: int) (d: Date) = d.SetHours(hours, min, sec, ms)
        
        let inline setMilliseconds (ms: int) (d: Date) = d.SetMilliseconds(ms)
        
        let inline setMinutes (min: int) (d: Date) = d.SetMinutes(min)
        let inline setMinutesS (min: int) (sec: int) (d: Date) = d.SetMinutes(min, sec = sec)
        let inline setMinutesSM (min: int) (sec: int) (ms: int) (d: Date) = d.SetMinutes(min, sec, ms)
        
        let inline setMonth (month: int) (d: Date) = d.SetMonth(month)
        let inline setMonthD (month: int) (date: int) (d: Date) = d.SetMonth(month, date)
        
        let inline setSeconds (sec: int) (d: Date) = d.SetSeconds(sec)
        let inline setSecondsM (sec: int) (ms: int) (d: Date) = d.SetSeconds(sec, ms)
        
        let inline setTime (time: int) (d: Date) = d.SetTime(time)
        
        let inline setUTCDate (date: int) (d: Date) = d.SetUTCDate(date)
        
        let inline setUTCFullYear (year: int) (d: Date) = d.SetUTCFullYear(year)
        let inline setUTCFullYearM (year: int) (month: int) (d: Date) = d.SetUTCFullYear(year, month = month)
        let inline setUTCFullYearD (year: int) (date: int) (d: Date) = d.SetUTCFullYear(year, date = date)
        let inline setUTCFullYearMD (year: int) (month: int) (date: int) (d: Date) = d.SetUTCFullYear(year, month, date)
        
        let inline setUTCHours (hours: int) (d: Date) = d.SetUTCHours(hours)
        let inline setUTCHoursM (hours: int) (min: int) (d: Date) = d.SetUTCHours(hours, min = min)
        let inline setUTCHoursMS (hours: int) (min: int) (sec: int) (d: Date) = d.SetUTCHours(hours, min = min, sec = sec)
        let inline setUTCHoursMSM (hours: int) (min: int) (sec: int) (ms: int) (d: Date) = d.SetUTCHours(hours, min, sec, ms)
        
        let inline setUTCMilliseconds (ms: int) (d: Date) = d.SetUTCMilliseconds(ms)
        
        let inline setUTCMinutes (min: int) (d: Date) = d.SetUTCMinutes(min)
        let inline setUTCMinutesS (min: int) (sec: int) (d: Date) = d.SetUTCMinutes(min, sec = sec)
        let inline setUTCMinutesMS (min: int) (sec: int) (ms: int) (d: Date) = d.SetUTCMinutes(min, sec, ms)
        
        let inline setUTCMonth (month: int) (d: Date) = d.SetUTCMonth(month)
        let inline setUTCMonthD (month: int) (date: int) (d: Date) = d.SetUTCMonth(month, date)
        
        let inline setUTCSeconds (sec: int) (d: Date) = d.SetUTCSeconds(sec)
        let inline setUTCSecondsM (sec: int) (ms: int) (d: Date) = d.SetUTCSeconds(sec, ms)
        
        let inline toDateString (d: Date) = d.ToDateString()
        
        let inline toJSON (d: Date) = d.ToJSON()
        
        let inline toLocaleDateString (d: Date) = d.ToLocaleDateString()
        
        let inline toLocaleString (d: Date) = d.ToLocaleString()
        
        let inline toLocaleTimeString (d: Date) = d.ToLocaleTimeString()
        
        let inline toISOString (d: Date) = d.ToISOString()
        
        let inline toString (d: Date) = d.ToString()
        
        let inline toTimeString (d: Date) = d.ToTimeString()
        
        let inline toUTCString (d: Date) = d.ToUTCString()
        
        let inline valueOf (d: Date) = d.ValueOf()

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
        [<Emit("$0.byteLength")>]
        member _.ByteLength: int = jsNative
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        [<Emit("$0.slice($1...)")>]
        member _.Slice (?begin': int, ?end': int) : ArrayBuffer = jsNative
            
        /// Determines whether the passed value is one of the ArrayBuffer views, 
        /// such as typed array objects or a DataView.
        static member isView (arg: obj) : bool = jsNative
    
        interface JS.ArrayBuffer with
            member this.byteLength = this.ByteLength
            member this.slice (begin', end') = this.Slice(begin', ?end' = end') :> JS.ArrayBuffer

    [<Erase;RequireQualifiedAccess>]
    module ArrayBuffer =
        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (ab: ArrayBuffer) = ab.ByteLength

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline slice (begin': int) (end': int) (ab: ArrayBuffer) = ab.Slice(begin', end')

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline sliceBegin (begin': int) (ab: ArrayBuffer) = ab.Slice(begin' = begin')
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        let inline sliceEnd (end': int) (ab: ArrayBuffer) = ab.Slice(end' = end')

    /// Provides a low-level interface for reading and writing multiple number types in a binary 
    /// ArrayBuffer, without having to care about the platform's endianness.
    [<Global>]
    type DataView (buffer: ArrayBuffer, ?byteOffset: int, ?byteLength: float) =
        [<Emit("$0")>]
        new (dv: JS.DataView) = DataView(unbox<ArrayBuffer> dv)

        /// The ArrayBuffer referenced by a DataView at construction time.
        [<Emit("$0.buffer")>]
        member _.Buffer: ArrayBuffer = jsNative

        /// The read-only size, in bytes, of the ArrayBuffer. 
        [<Emit("$0.byteLength")>]
        member _.ByteLength: int = jsNative

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        [<Emit("$0.byteOffset")>]
        member _.ByteOffset: int = jsNative
        
        [<Emit("$0.getFloat32($1...)")>]
        member _.GetFloat32 (byteOffset: int, ?littleEndian: bool) : float32 = jsNative

        [<Emit("$0.getFloat64($1...)")>]
        member _.GetFloat64 (byteOffset: int, ?littleEndian: bool) : float = jsNative

        [<Emit("$0.getInt8($1...)")>]
        member _.GetInt8 (byteOffset: int) : sbyte = jsNative

        [<Emit("$0.getInt16($1...)")>]
        member _.GetInt16 (byteOffset: int, ?littleEndian: bool) : int16 = jsNative

        [<Emit("$0.getInt32($1...)")>]
        member _.GetInt32 (byteOffset: int, ?littleEndian: bool) : int32 = jsNative

        [<Emit("$0.getUint8($1...)")>]
        member _.GetUint8 (byteOffset: int) : byte = jsNative

        [<Emit("$0.getUint16($1...)")>]
        member _.GetUint16 (byteOffset: int, ?littleEndian: bool) : uint16 = jsNative

        [<Emit("$0.getUint32($1...)")>]
        member _.GetUint32 (byteOffset: int, ?littleEndian: bool) : uint32 = jsNative

        [<Emit("$0.setFloat32($1...)")>]
        member _.SetFloat32 (byteOffset: int, value: float32, ?littleEndian: bool) : unit = jsNative

        [<Emit("$0.setFloat64($1...)")>]
        member _.SetFloat64 (byteOffset: int, value: float, ?littleEndian: bool) : unit = jsNative

        [<Emit("$0.setInt8($1...)")>]
        member _.SetInt8 (byteOffset: int, value: sbyte) : unit = jsNative

        [<Emit("$0.setInt16($1...)")>]
        member _.SetInt16 (byteOffset: int, value: int16, ?littleEndian: bool) : unit = jsNative

        [<Emit("$0.setInt32($1...)")>]
        member _.SetInt32 (byteOffset: int, value: int32, ?littleEndian: bool) : unit = jsNative

        [<Emit("$0.setUint8($1...)")>]
        member _.SetUint8 (byteOffset: int, value: byte) : unit = jsNative

        [<Emit("$0.setUint16($1...)")>]
        member _.SetUint16 (byteOffset: int, value: uint16, ?littleEndian: bool) : unit = jsNative

        [<Emit("$0.setUint32($1...)")>]
        member _.SetUint32 (byteOffset: int, value: uint32, ?littleEndian: bool) : unit = jsNative

        interface JS.ArrayBufferView with
            member this.buffer = upcast this.Buffer
            member this.byteLength = this.ByteLength
            member this.byteOffset = this.ByteOffset

    [<Erase;RequireQualifiedAccess>]
    module DataView =
        /// The ArrayBuffer referenced by a DataView at construction time.
        let inline buffer (dv: DataView) = dv.Buffer

        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (dv: DataView) = dv.ByteLength

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        let inline byteOffset (dv: DataView) = dv.ByteOffset

        [<Erase>]
        module LittleEndian =
            let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.GetFloat32(byteOffset, true)
            
            let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.GetFloat64(byteOffset, true)
            
            let inline getInt16 (byteOffset: int) (dv: DataView) = dv.GetInt16(byteOffset, true)
            
            let inline getInt32 (byteOffset: int) (dv: DataView) = dv.GetInt32(byteOffset, true)
            
            let inline getUint16 (byteOffset: int) (dv: DataView) = dv.GetUint16(byteOffset, true)
            
            let inline getUint32 (byteOffset: int) (dv: DataView) = dv.GetUint32(byteOffset, true)
            
            let inline setFloat32 (byteOffset: int, value: float32) (dv: DataView) = dv.SetFloat32(byteOffset, value, true)
            
            let inline setFloat64 (byteOffset: int, value: float) (dv: DataView) = dv.SetFloat64(byteOffset, value, true)
            
            let inline setInt16 (byteOffset: int, value: int16) (dv: DataView) = dv.SetInt16(byteOffset, value, true)
            
            let inline setInt32 (byteOffset: int, value: int32) (dv: DataView) = dv.SetInt32(byteOffset, value, true)
            
            let inline setUint16 (byteOffset: int, value: uint16) (dv: DataView) = dv.SetUint16(byteOffset, value, true)
            
            let inline setUint32 (byteOffset: int, value: uint32) (dv: DataView) = dv.SetUint32(byteOffset, value, true)

        let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.GetFloat32(byteOffset)

        let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.GetFloat64(byteOffset)
        
        let inline getInt8 (byteOffset: int) (dv: DataView) = dv.GetInt8(byteOffset)

        let inline getInt16 (byteOffset: int) (dv: DataView) = dv.GetInt16(byteOffset)

        let inline getInt32 (byteOffset: int) (dv: DataView) = dv.GetInt32(byteOffset)

        let inline getUint16 (byteOffset: int) (dv: DataView) = dv.GetUint16(byteOffset)

        let inline getUint32 (byteOffset: int) (dv: DataView) = dv.GetUint32(byteOffset)

        let inline setFloat32 (byteOffset: int, value: float32) (dv: DataView) = dv.SetFloat32(byteOffset, value)

        let inline setFloat64 (byteOffset: int, value: float) (dv: DataView) = dv.SetFloat64(byteOffset, value)
        
        let inline setInt8 (byteOffset: int, value: sbyte) (dv: DataView) = dv.SetInt8(byteOffset, value)

        let inline setInt16 (byteOffset: int, value: int16) (dv: DataView) = dv.SetInt16(byteOffset, value)

        let inline setInt32 (byteOffset: int, value: int32) (dv: DataView) = dv.SetInt32(byteOffset, value)
        
        let inline setUint8 (byteOffset: int, value: byte) (dv: DataView) = dv.SetUint8(byteOffset, value)

        let inline setUint16 (byteOffset: int, value: uint16) (dv: DataView) = dv.SetUint16(byteOffset, value)

        let inline setUint32 (byteOffset: int, value: uint32) (dv: DataView) = dv.SetUint32(byteOffset, value)
        
    /// Describes an array-like view of an underlying binary data buffer.
    [<Erase>]
    type TypedArray<'T> internal () =
        member _.Item
            with [<Emit("$0[$1]")>] get (index: int) : 'T = jsNative
            and [<Emit("$0[$1] = $2")>] set (index: int) (value: int) = jsNative

        /// Returns a sequence that contains the key/value pairs for each index in the array.
        [<Emit("$0.entries()")>]
        member _.Entries () : seq<int * 'T> = jsNative

        /// The ArrayBuffer referenced by a TypedArray at construction time.
        [<Emit("$0.buffer")>]
        member _.Buffer: ArrayBuffer = jsNative

        /// The length (in bytes) of a typed array.
        [<Emit("$0.byteLength")>]
        member _.ByteLength: int = jsNative

        /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
        [<Emit("$0.byteOffset")>]
        member _.ByteOffset: int = jsNative

        /// The length (in elements) of a typed array.
        [<Emit("$0.length")>]
        member _.Length: int = jsNative

        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second and third arguments 
        /// start and end. The end argument is optional and defaults to the length of the array.
        [<Emit("$0.copyWithin($1...)")>]
        member _.CopyWithin (targetStartIndex: int, start: int, ?end': int) : unit = jsNative

        /// The keys for each index in the array.
        [<Emit("$0.keys()")>]
        member _.Keys () : seq<int> = jsNative

        /// Joins all elements of an array into a string.
        [<Emit("$0.join($1)")>]
        member _.Join (separator: string) : string = jsNative

        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        [<Emit("$0.fill($1...)")>]
        member _.Fill (value:'T, ?start': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.Filter (f: 'T -> bool) : TypedArray<'T> = jsNative
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.Filter (f: 'T -> int -> bool) : TypedArray<'T> = jsNative
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        [<Emit("$0.filter($1)")>]
        member _.Filter (f: 'T -> int -> TypedArray<'T> -> bool) : TypedArray<'T> = jsNative
        
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        [<Emit("$0.find($1)")>]
        member _.Find (f: 'T -> bool) : 'T option = jsNative
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element and index.
        [<Emit("$0.find($1)")>]
        member _.Find (f: 'T -> int -> bool) : 'T option = jsNative
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.find($1)")>]
        member _.Find (f: 'T -> int -> TypedArray<'T> -> bool) : 'T option = jsNative
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        [<Emit("$0.findIndex($1)")>]
        member _.FindIndex (f: 'T -> bool) : int = jsNative
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element and index.
        [<Emit("$0.findIndex($1)")>]
        member _.FindIndex (f: 'T -> int -> bool) : int = jsNative
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.findIndex($1)")>]
        member _.FindIndex (f: 'T -> int -> TypedArray<'T> -> bool) : int = jsNative
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        member inline this.TryFindIndex (f: 'T -> bool) = 
            this.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element and index.
        member inline this.TryFindIndex (f: 'T -> int -> bool) = 
            this.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element, index, and array.
        member inline this.TryFindIndex (f: 'T -> int -> TypedArray<'T> -> bool) = 
            this.FindIndex(f) |> fun i -> if i < 0 then None else Some i

        /// Executes a provided function once per array element.
        [<Emit("$0.forEach($1)")>]
        member _.ForEach (f: 'T -> bool) : unit = jsNative
        /// Executes a provided function once per array element.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.forEach($1)")>]
        member _.ForEach (f: 'T -> int -> bool) : unit = jsNative
        /// Executes a provided function once per array element.
        ///
        /// Provides the element and index.
        [<Emit("$0.forEach($1)")>]
        member _.ForEach (f: 'T -> int -> TypedArray<'T> -> bool) : unit = jsNative

        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        [<Emit("$0.includes($1...)")>]
        member _.Includes (searchElement:'T, ?fromIndex: int) : bool = jsNative

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        [<Emit("$0.indexOf($1...)")>]
        member _.IndexOf (searchElement:'T, ?fromIndex: int) : int = jsNative

        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        [<Emit("$0.lastIndexOf($1...)")>]
        member _.LastIndexOf (searchElement:'T, ?fromIndex: int) : int = jsNative
        
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.Map (f: 'T -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.MapWithIndex (f: 'T -> int -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.MapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) : TypedArray<'U> = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.ReduceRight (f: 'State -> 'T -> 'State, state:'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.ReduceRight (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.ReduceRight (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State = jsNative

        /// Reverses a typed array in place.
        [<Emit("$0.reverse()")>]
        member _.Reverse () : TypedArray<'T> = jsNative

        /// Stores multiple values in the typed array, reading input values from a specified array.
        [<Emit("$0.set($1...)")>]
        member _.Set (source: System.Array, ?offset: int) : unit = jsNative
        /// Stores multiple values in the typed array, reading input values from a specified array.
        [<Emit("$0.set($1...)")>]
        member _.Set (source: #JS.TypedArray, ?offset: int) : unit = jsNative
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        [<Emit("$0.slice($1...)")>]
        member _.Slice (?begin': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.Some (f: 'T -> bool) : bool = jsNative
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.Some (f: 'T -> int -> bool) : bool = jsNative
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        [<Emit("$0.some($1)")>]
        member _.Some (f: 'T -> int -> TypedArray<'T> -> bool) : bool = jsNative
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        [<Emit("$0.sort($1...)")>]
        member _.Sort (?sortFunction: 'T -> 'T -> int) : TypedArray<'T> = jsNative
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        [<Emit("$0.subarray($1...)")>]
        member _.Subarray (?begin': int, ?end': int) : TypedArray<'T> = jsNative
        
        /// Returns a sequence that contains the values for each index in the array.
        [<Emit("$0.values()")>]
        member _.Values () : seq<'T> = jsNative

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
        let inline entries (ta: TypedArray<'T>) = ta.Entries()
        
        /// The ArrayBuffer referenced by a TypedArray at construction time.
        let inline buffer (ta: TypedArray<'T>) = ta.Buffer
        
        /// The length (in bytes) of a typed array.
        let inline byteLength (ta: TypedArray<'T>) = ta.ByteLength
        
        /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
        let inline byteOffset (ta: TypedArray<'T>) = ta.ByteOffset
        
        /// The length (in elements) of a typed array.
        let inline length (ta: TypedArray<'T>) = ta.Length
        
        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second argument start.
        let inline copyWithin (targetStartIndex: int) (start: int) (ta: TypedArray<'T>) = ta.CopyWithin(targetStartIndex, start)
        
        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second and third arguments 
        /// start and end.
        let inline copyWithinEnd (targetStartIndex: int) (start: int) (end': int) (ta: TypedArray<'T>) = ta.CopyWithin(targetStartIndex, start, end')

        /// The keys for each index in the array.
        let inline keys (ta: TypedArray<'T>) = ta.Keys
        
        /// Joins all elements of an array into a string.
        let inline join (sep: string) (ta: TypedArray<'T>) = ta.Join(sep)
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fill (value: 'T) (ta: TypedArray<'T>) = ta.Fill(value)
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillStart (value: 'T) (start': int) (ta: TypedArray<'T>) = ta.Fill(value, start' = start')
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillEnd (value: 'T) (end': int) (ta: TypedArray<'T>) = ta.Fill(value, end' = end')
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillStartEnd (value: 'T) (start': int) (end': int) (ta: TypedArray<'T>) = ta.Fill(value, start', end')

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filter (f: 'T -> bool) (ta: TypedArray<'T>) = ta.Filter(f)

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filterWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.Filter(f)

        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        let inline filterWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.Filter(f)
        
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        let inline find (f: 'T -> bool) (ta: TypedArray<'T>) = ta.Find(f)

        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element and index.
        let inline findWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.Find(f)
        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        ///
        /// Provides the element, index, and array.
        let inline findWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.Find(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        let inline findIndex (f: 'T -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element and index.
        let inline findIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        ///
        /// Provides the element, index, and array.
        let inline findIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        let inline tryFindIndex (f: 'T -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element and index.
        let inline tryFindIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        ///
        /// Provides the element, index, and array.
        let inline tryFindIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Executes a provided function once per array element.
        let inline forEach (f: 'T -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Executes a provided function once per array element.
        ///
        /// Provides the element, index, and array.
        let inline forEachWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Executes a provided function once per array element.
        ///
        /// Provides the element and index.
        let inline forEachWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includes (searchElement:'T) (ta: TypedArray<'T>) = ta.Includes(searchElement)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includesFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.Includes(searchElement)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOf (searchElement:'T) (ta: TypedArray<'T>) = ta.IndexOf(searchElement)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOfFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.IndexOf(searchElement, fromIndex)
        
        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOf (searchElement:'T) (ta: TypedArray<'T>) = ta.LastIndexOf(searchElement)
        
        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOfFromIndex (searchElement:'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.LastIndexOf(searchElement, fromIndex)
        
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline map (f: 'T -> 'U) (ta: TypedArray<'T>) = ta.Map(f)

        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline mapWithIndex (f: 'T -> int -> 'U) (ta: TypedArray<'T>) = ta.MapWithIndex(f)

        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        let inline mapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) (ta: TypedArray<'T>) = ta.MapWithIndexArray(f)
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduce (f: 'State -> 'T -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.Reduce(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduceWithIndex (f: 'State -> 'T -> int -> 'State, state: 'State) (ta: TypedArray<'T>) = ta.Reduce(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduceWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) (ta: TypedArray<'T>) = ta.Reduce(f, state)
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRight (f: 'State -> 'T -> 'State) (state:'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndex (f: 'State -> 'T -> int -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)
        
        /// Reverses a typed array in place.
        let inline reverse (ta: TypedArray<'T>) = ta.Reverse()
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArray (source: System.Array) (ta: TypedArray<'T>) = ta.Set(source)

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArrayOffset (source: System.Array) (offset: int) (ta: TypedArray<'T>) = ta.Set(source, offset)

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArray (source: #JS.TypedArray) (ta: TypedArray<'T>) = ta.Set(source)
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArrayOffset (source: #JS.TypedArray) (offset: int) (ta: TypedArray<'T>)= ta.Set(source, offset)

        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeq (source: seq<'T>) (ta: TypedArray<'T>) = ta.Set(unbox<System.Array> (ResizeArray source))
        
        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeqOffset (source: seq<'T>) (offset: int) (ta: TypedArray<'T>) = ta.Set(unbox<System.Array> (ResizeArray source), offset = offset)
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline slice (begin': int) (end': int) (ta: TypedArray<'T>) = ta.Slice(begin', end')

        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline sliceBegin (begin': int) (ta: TypedArray<'T>) = ta.Slice(begin' = begin')
        
        /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
        let inline sliceEnd (end': int) (ta: TypedArray<'T>) = ta.Slice(end' = end')
        
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline some (f: 'T -> bool) (ta: TypedArray<'T>) = ta.Some(f)

        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline someWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.Some(f)

        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        let inline someWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.Some(f)
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        let inline sort (ta: TypedArray<'T>) = ta.Sort()
        
        /// Sorts the elements of a typed array numerically in place and returns the typed array. 
        let inline sortBy (sortFunction: 'T -> 'T -> int) (ta: TypedArray<'T>) = ta.Sort(sortFunction)

        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarray (begin': int) (end': int) (ta: TypedArray<'T>) = ta.Subarray(begin', end')
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarrayBegin (begin': int) (ta: TypedArray<'T>) = ta.Subarray(begin' = begin')
        
        /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
        /// as for this TypedArray object. 
        /// 
        /// The begin offset is inclusive and the end offset is exclusive.
        let inline subarrayEnd (end': int) (ta: TypedArray<'T>) = ta.Subarray(end' = end')
        
        /// Returns a sequence that contains the values for each index in the array.
        let inline values (ta: TypedArray<'T>) = ta.Values()

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
        member inline this.Set (source: seq<'T>, ?offset: int) : unit = this.Set(unbox<System.Array> (ResizeArray source), ?offset = offset)

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
