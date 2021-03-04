namespace Fable.Extras

open Fable.Core
open FSharp.Core
open System
open System.ComponentModel
open System.Text.RegularExpressions

[<Erase;RequireQualifiedAccess>]
module JSe =
    /// Decodes a string of data which has been encoded using Base64 encoding.
    [<Emit("atob($0)")>]
    let atob (encodedData: string) : string = jsNative

    /// Creates a Base64-encoded ASCII string from a binary string (i.e., a 
    /// String object in which each character in the string is treated as a 
    /// byte of binary data).
    [<Emit("btoa($0)")>]
    let btoa (stringToEncode: string) : string = jsNative

    /// Calls the object's apply prototype.
    [<Emit("$0.apply(this, $1)")>]
    let apply<'T,'U> (f: 'T) (arguments: obj []) : 'U = jsNative

    /// Applies an argument array to a curried function.
    [<Emit("$1.reduce((p, c) => p(c), $0)")>]
    let applyCurried<'T,'U> (f: 'T) (arguments: obj [])  : 'U = jsNative

    /// An Array-like object accessible inside functions that contains 
    /// the values of the arguments passed to that function.
    [<Emit("arguments")>]
    let arguments : obj [] = jsNative

    /// Javascript globalThis keyword.
    [<Emit("globalThis")>]
    let globalThis : obj = jsNative
    
    /// Tests to see if the prototype property of a constructor appears 
    /// anywhere in the prototype chain of an object.
    ///
    /// This should only be used when working with external code (like bindings).
    [<Emit("$1 instanceof $0")>]
    let instanceOf (ctor: obj) (value: obj) : bool = jsNative

    /// Returns if the session is considered secure.
    [<Emit("isSecureContext")>]
    let isSecureContext : bool = jsNative

    /// The Javascript || operator to collect the first non-None option
    [<Emit("$0 || $1")>]
    let or' (lh: 'T option) (rh: 'T option) : 'T option = jsNative

    /// Queues a microtask to be executed at a safe time prior to control returning 
    /// to the browser's event loop. The microtask is a short function which will 
    /// run after the current task has completed its work and when there is no other 
    /// code waiting to be run before control of the execution context is returned 
    /// to the browser's event loop.
    ///
    /// This lets your code run without interfering with any other, potentially higher 
    /// priority, code that is pending, but before the browser regains control over 
    /// the execution context, potentially depending on work you need to complete.
    [<Emit("queueMicrotask($0)")>]
    let queueMicrotask (f: unit -> unit) : unit = jsNative

    /// Javascript this keyword.
    [<Emit("this")>]
    let this' : obj = jsNative

    /// Javascript types.
    [<RequireQualifiedAccess;StringEnum(CaseRules.LowerFirst)>]
    type Types =
        | Bigint
        | Boolean
        | Function
        | Null
        | Number
        | Object
        | String
        | Symbol
        | Undefined
    
    /// Returns a Types union case indicating the type of the unevaluated operand.
    [<Emit("typeof $0")>]
    let typeof (o: obj) : Types = jsNative

    /// Type checking and equality helpers.
    [<Erase>]
    type is =
        [<Emit("typeof $0 === 'bigint'")>]
        static member bigint (x: obj) : bool = jsNative

        [<Emit("typeof $0 === 'boolean'")>]
        static member boolean (x: obj) : bool = jsNative

        [<Emit("typeof $0 === 'function'")>]
        static member function' (value: obj) : bool = jsNative
        
        [<Emit("typeof $0 === null")>]
        static member null' (value: obj) : bool = jsNative
        
        /// Checks if the input is both an object and has a Symbol.iterator.
        [<Emit("typeof $0 === 'object' && !$0[Symbol.iterator]")>]
        static member nonEnumerableObject (value: obj) : bool = jsNative

        [<Emit("typeof $0 === 'number'")>]
        static member number (x: obj) : bool = jsNative
        
        [<Emit("typeof $0 === 'object'")>]
        static member object (x: obj) : bool = jsNative
        
        [<Emit("!!$0 && typeof $0.then === 'function'")>]
        static member promise (value: obj) : bool = jsNative

        [<Emit("typeof $0 === 'string'")>]
        static member string (x: obj) : bool = jsNative

        [<Emit("typeof $0 === 'symbol'")>]
        static member symbol (x: obj) : bool = jsNative

        [<Emit("typeof $0 === 'undefined'")>]
        static member undefined (x: obj) : bool = jsNative

    [<Measure>]
    type IntervalId

    /// Cancels the repeated execution set using setInterval.
    [<Emit("clearTimeout($0)")>]
    let clearInterval (id: int<IntervalId>) : unit = jsNative
    
    /// Schedules a function to execute every time a given number 
    /// of milliseconds elapses.
    [<Emit("setInterval($0, $1)")>]
    let setInterval (f: unit -> unit) (delay: int) : int<IntervalId> = jsNative
    
    [<Measure>]
    type TimeoutId

    /// Cancels the delayed execution set using setTimeout.
    [<Emit("clearTimeout($0)")>]
    let clearTimeout (id: int<TimeoutId>) : unit = jsNative

    /// Schedules a function to execute in a given amount of time (in miliseconds).
    [<Emit("setTimeout($0, $1)")>]
    let setTimeout (f: unit -> unit) (timeout: int) : int<TimeoutId> = jsNative

    /// Holds key-value pairs and remembers the original insertion order of the keys.
    ///
    /// Any value (both objects and primitive values) may be used as either a key or a value.
    [<Erase>]
    type Map<'K,'V> [<Emit("new Map($0...)")>] (?iterable: seq<'K * 'V>) =
        [<Emit("$0")>]
        new (m: JS.Map<'K,'V>) = Map()

        /// The number of elements.
        [<Emit("$0.size")>]
        member _.Size: int = jsNative

        /// Removes all elements.
        [<Emit("$0.clear()")>]
        member _.Clear () : unit = jsNative

        /// Removes the specified element by key.
        [<Emit("$0.delete($1)")>]
        member _.Delete (key: 'K) : bool = jsNative

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.entries()")>]
        member _.Entries' () : seq<'K * 'V> = jsNative
        
        /// Returns a list of key value pairs in insertion order.
        member inline this.Entries () = this.Entries'() |> List.ofSeq

        /// Applies the given function once per each key value pair in insertion order.
        [<Emit("$0.forEach($1...)")>]
        member _.ForEach (action: 'V -> 'K -> Map<'K, 'V> -> unit, ?thisArg: obj) : unit = jsNative

        /// Returns the value for the specified key.
        [<Emit("$0.get($1)")>]
        member _.Get (key: 'K) : 'V option = jsNative

        /// Returns a boolean indicating whether an element with the specified key exists or not.
        [<Emit("$0.has($1)")>]
        member _.Has (key: 'K) : bool = jsNative

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.keys()")>]
        member _.Keys' () : seq<'K> = jsNative
        
        /// Returns a list of keys in insertion order.
        member inline this.Keys () = this.Keys'() |> List.ofSeq

        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        [<Emit("$0.set($1...)")>]
        member _.Set (key: 'K, ?value: 'V) : Map<'K, 'V> = jsNative

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.values()")>]
        member _.Values' () : seq<'V> = jsNative
        
        /// Returns a list of values in insertion order.
        member inline this.Values () = this.Values'() |> List.ofSeq

        interface JS.Map<'K,'V> with
            member this.size = this.Size
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.entries () = unbox (this.Entries())
            member this.forEach (callback, ?thisArg) = this.ForEach(callback, ?thisArg = thisArg)
            member this.get k = this.Get k |> Option.get
            member this.has k = this.Has k
            member this.keys () = unbox (this.Keys())
            member this.set (key, value) = upcast this.Set(key, value)
            member this.values () = unbox (this.Values())
    
    /// Holds key-value pairs and remembers the original insertion order of the keys.
    ///
    /// Any value (both objects and primitive values) may be used as either a key or a value.
    [<Erase;RequireQualifiedAccess>]
    module Map =
        /// Removes all elements.
        let inline clear (m: Map<'K,'V>) = 
            m.Clear()
            m
        
        /// Removes the specified element by key.
        let inline delete (key: 'K) (m: Map<'K,'V>) = 
            m.Delete(key) |> ignore
            m
        
        /// Returns a list of key value pairs in insertion order.
        let inline entries (m: Map<'K,'V>) = m.Entries()

        /// Creates an empty Map.
        let inline empty<'K,'V> = Map<'K,'V>()
        
        /// Applies the given function once per each key value pair in insertion order.
        let inline forEach (action: 'V -> 'K -> Map<'K, 'V> -> unit) (m: Map<'K,'V>) = m.ForEach(action)

        /// Applies the given function once per each key value pair in insertion order.
        let inline forEachThis (action: 'V -> 'K -> Map<'K, 'V> -> unit) (thisArg: obj) (m: Map<'K,'V>) = m.ForEach(action, thisArg)
        
        /// Returns the value for the specified key.
        let inline get (key: 'K) (m: Map<'K,'V>) = m.Get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (m: Map<'K,'V>) = m.Has(key)
        
        /// Returns a list of keys in insertion order.
        let inline keys (m: Map<'K,'V>) = m.Keys()
        
        /// Converts a FSharp Map into a JS Map.
        let inline ofMap (m: Microsoft.FSharp.Collections.Map<'K,'V>) = 
            (Map<'K,'V>(), m) ||> Map.fold (fun jsMap k v -> jsMap.Set(k, v))

        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (m: Map<'K,'V>) = m.Set(key,value)

        /// The number of elements.
        let inline size (m: Map<'K,'V>) = m.Size
        
        /// Returns a list of values in insertion order.
        let inline values (m: Map<'K,'V>) = m.Values()

    /// Lets you store weakly held objects in a collection.
    [<Global>]
    type WeakMap<'K,'V when 'K : not struct> (?iterable: seq<'K * 'V>) =
        [<Emit("$0")>]
        new (wm: JS.WeakMap<'K,'V>) = WeakMap()

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
            member _.clear () = ()
            member this.delete k = this.Delete k
            member this.get k = this.Get k |> Option.get
            member this.has k = this.Has k
            member this.set (key, value) = upcast this.Set(key, value)
    
    /// Lets you store weakly held objects in a collection.
    [<Erase;RequireQualifiedAccess>]
    module WeakMap =
        /// Removes the specified element by key.
        let inline delete (key: 'K) (wm: WeakMap<'K,'V>) = wm.Delete(key)
        
        /// Creates an empty WeakMap.
        let inline empty<'K,'V when 'K : not struct> = WeakMap<'K,'V>()

        /// Returns the value for the specified key.
        let inline get (key: 'K) (wm: WeakMap<'K,'V>) = wm.Get(key)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (key: 'K) (wm: WeakMap<'K,'V>) = wm.Has(key)
        
        /// Adds or updates an element with the specified key.
        ///
        /// If a value is not provided the value will be removed, but the key will still exist.
        let inline set (key: 'K) (value: 'V) (wm: WeakMap<'K,'V>) = wm.Set(key,value)

    /// Lets you store unique values of any type, whether primitive values or object references.
    [<Erase>]
    type Set<'T> [<Emit("new Set($0...)")>] (?iterable: seq<'T>) =
        [<Emit("$0")>]
        new (set: JS.Set<'T>) = Set()

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

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.entries()")>]
        member _.Entries' () : seq<'T * 'T> = jsNative
        
        /// Returns a list of values in tupled form, use the values 
        /// method to get a list of just the value.
        member inline this.Entries () = this.Entries'() |> List.ofSeq

        /// Applies the given function once per each value.
        [<Emit("$0.forEach($1...)")>]
        member _.ForEach (action: 'T -> 'T -> Set<'T> -> unit, ?thisArg: obj) : unit = jsNative

        /// Returns a boolean indicating whether an element with the specified value exists.
        [<Emit("$0.has($1)")>]
        member _.Has (value: 'T) : bool = jsNative

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.keys()")>]
        member _.Keys' () : seq<'T> = jsNative
        
        /// Returns a list of values.
        ///
        /// This is an alias for the values method.
        member inline this.Keys () = this.Keys'() |> List.ofSeq

        [<EditorBrowsable(EditorBrowsableState.Never);Emit("$0.values()")>]
        member _.Values' () : seq<'T> = jsNative

        /// Returns a list of values.
        member inline this.Values () = this.Values'() |> List.ofSeq
    
        interface JS.Set<'T> with
            member this.size = this.Size
            member this.add value = upcast this.Add(value)
            member this.clear () = this.Clear()
            member this.delete k = this.Delete k
            member this.entries () = unbox (this.Entries())
            member this.forEach (callback, ?thisArg) = this.ForEach(callback, ?thisArg = thisArg)
            member this.has k = this.Has k
            member this.keys () = unbox (this.Keys())
            member this.values () = unbox (this.Values())
    
    /// Lets you store unique values of any type, whether primitive values or object references.
    [<Erase;RequireQualifiedAccess>]
    module Set =
        /// Appends a new element.
        let inline add (value: 'T) (s: Set<'T>) = s.Add(value)
        
        /// Removes all elements.
        let inline clear (s: Set<'T>) = 
            s.Clear()
            s

        /// Removes the specified element.
        let inline delete (value: 'T) (s: Set<'T>) = 
            s.Delete(value) |> ignore
            s
        
        /// Creates an empty Set.
        let inline empty<'T> = Set<'T>()

        /// Returns a list of values in tupled form, use the values 
        /// method to get a list of just the value.
        let inline entries (s: Set<'T>) = s.Entries()
        
        /// Applies the given function once per each value.
        let inline forEach (action: 'T -> 'T -> Set<'T> -> unit) (s: Set<'T>) = s.ForEach(action)
        
        /// Applies the given function once per each value.
        let inline forEachThis (action: 'T -> 'T -> Set<'T> -> unit) (thisArg: obj) (s: Set<'T>) = s.ForEach(action, thisArg = thisArg)

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (s: Set<'T>) = s.Has(value)
        
        /// Returns a list of values.
        ///
        /// This is an alias for the values method.
        let inline keys (s: Set<'T>) = s.Keys()
        
        /// Converts a FSharp Set into a JS Set.
        let inline ofSet (s: Microsoft.FSharp.Collections.Set<'T>) =
            (Set<'T>(), s) ||> Set.fold (fun jsSet v -> jsSet.Add v)

        /// The number of elements.
        let inline size (s: Set<'T>) = s.Size
        
        /// Returns a list of values.
        let inline values (s: Set<'T>) = s.Values()
        
    /// Stores weakly held objects in a collection.
    [<Global>]
    type WeakSet<'T> (?iterable: seq<'T>) =
        [<Emit("$0")>]
        new (ws: JS.WeakSet<'T>) = WeakSet<'T>()

        //// Appends a new element.
        [<Emit("$0.add($1)")>]
        member _.Add (value: 'T) : WeakSet<'T> = jsNative
        
        /// Removes the specified element.
        [<Emit("$0.delete($1)")>]
        member _.Delete (value: 'T) : bool = jsNative
        
        /// Returns a boolean indicating whether an element with the specified value exists.
        [<Emit("$0.has($1)")>]
        member _.Has (value: 'T) : bool = jsNative

        interface JS.WeakSet<'T> with
            member this.add value = upcast this.Add(value)
            member _.clear () = ()
            member this.delete k = this.Delete k
            member this.has k = this.Has k
    
    /// Stores weakly held objects in a collection.
    [<Erase;RequireQualifiedAccess>]
    module WeakSet =
        /// Appends a new element.
        let inline add (value: 'T) (ws: WeakSet<'T>) = ws.Add(value)
                
        /// Removes the specified element.
        let inline delete (value: 'T) (ws: WeakSet<'T>) = ws.Delete(value)
        
        /// Creates an empty WeakSet.
        let inline empty<'T> = WeakSet<'T>()

        /// Returns a boolean indicating whether an element with the specified value exists.
        let inline has (value: 'T) (ws: WeakSet<'T>) = ws.Has(value)

    /// Describes the configuration of a specific property on a given object.
    [<Global>]
    type PropertyDescriptor<'T> [<Emit("Object.create(null)")>] () =
        [<Emit("$0")>]
        new (pd: JS.PropertyDescriptor) = PropertyDescriptor<'T>()

        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        member _.Configurable
            with [<Emit("$0.configurable")>] get () : bool option = jsNative
            and [<Emit("$0.configurable = $1")>] set (x: bool option) = jsNative

        /// True if and only if this property shows up during enumeration 
        /// of the properties on the corresponding object.
        member _.Enumerable
            with [<Emit("$0.enumerable")>] get () : bool option = jsNative
            and [<Emit("$0.enumerable = $1")>] set (x: bool option) = jsNative

        /// A function which serves as a getter for the property, or undefined if 
        /// there is no getter. 
        ///
        /// When the property is accessed, this function is called without arguments and 
        /// with this set to the object through which the property is accessed (this may not 
        /// be the object on which the property is defined due to inheritance). 
        ///
        /// The return value will be used as the value of the property.
        member _.Get
            with [<Emit("$0.get")>] get () : (unit -> 'T) option = jsNative
            and [<Emit("$0.get = $1")>] set (x: (unit -> 'T) option) = jsNative

        /// A function which serves as a setter for the property, or undefined if there is no 
        /// setter. 
        ///
        /// When the property is assigned, this function is called with one argument (the value 
        /// being assigned to the property) and with this set to the object through which the 
        /// property is assigned.
        member _.Set
            with [<Emit("$0.set")>] get () : ('T -> unit) option = jsNative
            and [<Emit("$0.set = $1")>] set (x: ('T -> unit) option) = jsNative

        /// The value associated with the property. Can be any valid JavaScript 
        /// value (number, object, function, etc).
        member _.Value
            with [<Emit("$0.value")>] get () : 'T option  = jsNative
            and [<Emit("$0.value = $1")>] set (x: 'T option) = jsNative

        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        member _.Writable
            with [<Emit("$0.writable")>] get () : bool option = jsNative
            and [<Emit("$0.writable = $1")>] set (x: bool option) = jsNative

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
    
    /// Describes the configuration of a specific property on a given object.
    [<Erase;RequireQualifiedAccess>]
    module PropertyDescriptor =
        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        let inline configurable (pd: PropertyDescriptor<'T>) = pd.Configurable

        /// True if and only if this property shows up during enumeration 
        /// of the properties on the corresponding object.
        let inline enumerable (pd: PropertyDescriptor<'T>) = pd.Enumerable

        /// A function which serves as a getter for the property, or undefined if 
        /// there is no getter. 
        ///
        /// When the property is accessed, this function is called without arguments and 
        /// with this set to the object through which the property is accessed (this may not 
        /// be the object on which the property is defined due to inheritance). 
        ///
        /// The return value will be used as the value of the property.
        let inline get (pd: PropertyDescriptor<'T>) = pd.Get
        
        /// A function which serves as a setter for the property, or undefined if there is no 
        /// setter. 
        ///
        /// When the property is assigned, this function is called with one argument (the value 
        /// being assigned to the property) and with this set to the object through which the 
        /// property is assigned.
        let inline set (pd: PropertyDescriptor<'T>) = pd.Set

        /// The value associated with the property. Can be any valid JavaScript 
        /// value (number, object, function, etc).
        let inline value (pd: PropertyDescriptor<'T>) = pd.Value
        
        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        let inline writable (pd: PropertyDescriptor<'T>) = pd.Writable

        /// True if the type of this property descriptor may be changed and 
        /// if the property may be deleted from the corresponding object.
        let inline setConfigurable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Configurable <- Some b
            pd

        /// True if and only if this property shows up during enumeration 
        /// of the properties on the corresponding object.
        let inline setEnumerable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Enumerable <- Some b
            pd

        /// A function which serves as a getter for the property, or undefined if 
        /// there is no getter. 
        ///
        /// When the property is accessed, this function is called without arguments and 
        /// with this set to the object through which the property is accessed (this may not 
        /// be the object on which the property is defined due to inheritance). 
        ///
        /// The return value will be used as the value of the property.
        let inline setGet (f: unit -> 'T) (pd: PropertyDescriptor<'T>) = 
            pd.Get <- Some f
            pd

        /// A function which serves as a setter for the property, or undefined if there is no 
        /// setter. 
        ///
        /// When the property is assigned, this function is called with one argument (the value 
        /// being assigned to the property) and with this set to the object through which the 
        /// property is assigned.
        let inline setSet (f: 'T -> unit) (pd: PropertyDescriptor<'T>) = 
            pd.Set <- Some f
            pd

        /// The value associated with the property. Can be any valid JavaScript 
        /// value (number, object, function, etc).
        let inline setValue (v: 'T) (pd: PropertyDescriptor<'T>) = 
            pd.Value <- Some v
            pd

        /// True if the value associated with the property may be changed with 
        /// an assignment operator.
        let inline setWritable (b: bool) (pd: PropertyDescriptor<'T>) = 
            pd.Writable <- Some b
            pd

    [<Global>]
    type Array =
        /// Determines whether the passed value is an Array.
        static member isArray (maybeArray: obj) : bool = jsNative
    
    [<Global>]
    type Number =
        /// Determines whether the passed value is NaN and its type is Number.
        static member isNaN (maybeNumber: obj) : bool = jsNative

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

        /// Creates a new object using an existing object as the prototype of the newly created object.
        ///
        /// Using an option type will throw errors at runtime if the passed value is ever None.
        static member create<'T when 'T : not struct> (o: 'T) : 'T = jsNative

        /// Creates a new empty object.
        [<Emit("Object.create(null)")>]
        static member createEmpty<'T when 'T : not struct> () : 'T = jsNative
        
        /// Creates a new object, using an existing object as the prototype of the newly created object.
        [<Emit("Object.create($1,Object.fromEntries($0))")>]
        static member createWithDescriptors<'T, 'U when 'T : not struct and 'U :> JS.PropertyDescriptor> (descriptors: seq<string * 'U>) (o: 'T) : 'T = jsNative

        /// Defines new or modifies existing properties directly on an object, returning the object.
        [<Emit("Object.defineProperties($1,Object.fromEntries($0))")>]
        static member defineProperties<'T, 'U when 'T : not struct and 'U :> JS.PropertyDescriptor> (descriptors: seq<string * 'U>) (o: 'T) : obj = jsNative

        /// Defines new or modifies an existing property directly on an object, returning the object.
        [<Emit("Object.defineProperties($2,$0,$1)")>]
        static member defineProperty<'T, 'U when 'T : not struct and 'U :> JS.PropertyDescriptor> (propertyKey: string) (descriptor: 'U) (o: 'T) : obj = jsNative

        /// Returns a sequence of key value pairs representing the object's own enumerable properties.
        ///
        /// The order is not guaranteed, and should be sorted first if that matters.
        static member entries (o: 'T) : (string * obj) [] = jsNative

        /// Freezes an object. 
        ///
        /// A frozen object can no longer be changed; freezing an object prevents new properties from 
        /// being added to it, existing properties from being removed, prevents changing the enumerability, 
        /// configurability, or writability of existing properties, and prevents the values of existing 
        /// properties from being changed.
        static member freeze<'T when 'T : not struct> (o: 'T) : 'T = jsNative

        /// Transforms a sequence of key-value pairs into an object.
        static member fromEntries<'T when 'T : not struct> (entries: seq<string * obj>) : 'T = jsNative

        /// Returns an object describing the configuration of a specific property on a given object 
        /// (that is, one directly present on an object and not in the object's prototype chain).
        static member getOwnPropertyDescriptor<'T> (o: obj) (p: string) : PropertyDescriptor<'T> option = jsNative

        /// Returns all own property descriptors of a given object.
        [<Emit("new Map(Object.entries(Object.getOwnPropertyDescriptors($0)))")>]
        static member getOwnPropertyDescriptors (o: 'T) : Map<string,PropertyDescriptor<obj>> = jsNative

        /// Returns the prototype (i.e. the value of the internal [[Prototype]] property) of the specified object.
        static member getPrototypeOf<'T,'U when 'T : not struct and 'U : not struct> (o: 'T) : 'U = jsNative
        
        /// Indicates whether the object has the specified property 
        /// as its own property (as opposed to inheriting it).
        [<Emit("$1.hasOwnProperty($0)")>]
        static member hasOwnProperty (property: string) (o: 'T) : bool = jsNative

        /// Determines whether two values are the same value.
        static member is (value1: 'T) (value2: 'U) : bool = jsNative

        /// Determines if an object is extensible (whether it can have new properties added to it).
        static member isExtensible (o: 'T) : bool = jsNative

        /// Determines if an object is frozen.
        static member isFrozen (o: 'T) : bool = jsNative
        
        /// Checks if an object exists in another object's prototype chain.
        [<Emit("$1.isPrototypeOf($0)")>]
        static member isPrototypeOf (proto: 'T) (o: 'U) : bool = jsNative
        
        /// Determines if an object is sealed.
        static member isSealed (o: 'T) : bool = jsNative

        /// Prevents new properties from ever being added to an object (i.e. prevents future extensions to the object).
        static member preventExtensions (o: 'T) : 'T = jsNative
        
        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        [<Emit("x => x.propertyIsEnumerable($0)")>]
        static member propertyIsEnumerable (property: int) : 'T -> bool = jsNative
        /// Indicates whether the specified property is enumerable 
        /// and is the object's own property.
        ///
        /// An integer would be used when testing against an array.
        [<Emit("x => x.propertyIsEnumerable($0)")>]
        static member propertyIsEnumerable (property: string) : 'T -> bool = jsNative

        /// Seals an object, preventing new properties from being added to it and marking all existing properties as 
        /// non-configurable. Values of present properties can still be changed as long as they are writable.
        static member seal (o: 'T) : 'T = jsNative

        /// Sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to another object.
        static member setPrototypeOf (o: 'T) (proto: 'U) : obj = jsNative
        
        /// Sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to null.
        [<Emit("Object.setPrototypeOf($0, null)")>]
        static member setPrototypeOfNull (o: 'T) : obj = jsNative
        
        [<Emit("$0.toLocaleString()")>]
        static member toLocaleString (o: 'T) : string = jsNative
        
        [<Emit("$0.toString()")>]
        static member toString (o: 'T) : string = jsNative

        /// Returns the primitive value of the specified object.
        [<Emit("$0.valueOf()")>]
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
        static member fround (x: float) : float32 = jsNative
        /// Returns the nearest 32-bit single precision float representation of a Number.
        static member fround (x: float32) : float32 = jsNative
        
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
        static member sign (x: decimal) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        static member sign (x: float32) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        static member sign (x: float) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        static member sign (x: int16) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        static member sign (x: int) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
        static member sign (x: int64) : int = jsNative
        /// Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
        /// the argument. 
        ///
        /// If the number passed into Math.sign() is 0, it will return a +/- 0. 
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
        [<Emit("$0")>]
        new (d: JS.Date) = Date()
            
        /// Converts the Date to System.DateTime.
        ///
        /// No runetime cost.
        [<Emit("$0")>]
        member _.AsDateTime () : DateTime = jsNative

        /// Returns the day of the month (1-31) for the specified date according to local time.
        [<Emit("$0.getDate()")>]
        member _.GetDate () : int = jsNative
        
        /// Returns the day of the week (0-6) for the specified date according to local time.
        [<Emit("$0.getDay()")>]
        member _.GetDay () : DayOfWeek = jsNative
        
        /// Returns the year (4 digits for 4-digit years) of the specified date according to local time.
        [<Emit("$0.getFullYear()")>]
        member _.GetFullYear () : int = jsNative
        
        /// Returns the hour (0-23) in the specified date according to local time.
        [<Emit("$0.getHours()")>]
        member _.GetHours () : int = jsNative
        
        /// Returns the milliseconds (0-999) in the specified date according to local time.
        [<Emit("$0.getMilliseconds()")>]
        member _.GetMilliseconds () : int = jsNative
        
        /// Returns the minutes (0-59) in the specified date according to local time.
        [<Emit("$0.getMinutes()")>]
        member _.GetMinutes () : int = jsNative
        
        /// Returns the month (0-11) in the specified date according to local time.
        [<Emit("$0.getMonth()")>]
        member _.GetMonth () : int = jsNative
        
        /// Returns the seconds (0-59) in the specified date according to local time.
        [<Emit("$0.getSeconds()")>]
        member _.GetSeconds () : int = jsNative
        
        /// Returns the numeric value of the specified date as the number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
        [<Emit("$0.getTime()")>]
        member _.GetTime () : int64 = jsNative
        
        /// Returns the numeric value of the specified date as the number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
        [<Emit("$0.getTimezoneOffset()")>]
        member _.GetTimezoneOffset () : int64 = jsNative
        
        /// Returns the day (date) of the month (1-31) in the specified date according to 
        /// universal time.
        [<Emit("$0.getUTCDate()")>]
        member _.GetUTCDate () : int = jsNative
        
        /// Returns the day of the week (0-6) in the specified date according to universal time.
        [<Emit("$0.getUTCDay()")>]
        member _.GetUTCDay () : DayOfWeek = jsNative
        
        /// Returns the year (4 digits for 4-digit years) in the specified date according to 
        /// universal time.
        [<Emit("$0.getUTCFullYear()")>]
        member _.GetUTCFullYear () : int = jsNative
        
        /// Returns the hours (0-23) in the specified date according to universal time.
        [<Emit("$0.getUTCHours()")>]
        member _.GetUTCHours () : int = jsNative
        
        /// Returns the milliseconds (0-999) in the specified date according to universal time.
        [<Emit("$0.getUTCMilliseconds()")>]
        member _.GetUTCMilliseconds () : int = jsNative
        
        /// Returns the minutes (0-59) in the specified date according to universal time.
        [<Emit("$0.getUTCMinutes()")>]
        member _.GetUTCMinutes () : int = jsNative
        
        /// Returns the month (0-11) in the specified date according to universal time.
        [<Emit("$0.getUTCMonth()")>]
        member _.GetUTCMonth () : int = jsNative
        
        /// Returns the seconds (0-59) in the specified date according to universal time.
        [<Emit("$0.getUTCSeconds()")>]
        member _.GetUTCSeconds () : int = jsNative
        
        /// Sets the day of the month for a specified date according to local time.
        [<Emit("new Date($0.setDate($1))")>]
        member _.SetDate (date: int) : Date = jsNative
        
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// local time.
        [<Emit("new Date($0.setFullYear($1...))")>]
        member _.SetFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        /// Sets the hours for a specified date according to local time.
        [<Emit("new Date($0.setHours($1...))")>]
        member _.SetHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        /// Sets the milliseconds for a specified date according to local time.
        [<Emit("new Date($0.setMilliseconds($1))")>]
        member _.SetMilliseconds (ms: int) : Date = jsNative
        
        /// Sets the minutes for a specified date according to local time.
        [<Emit("new Date($0.setMinutes($1...))")>]
        member _.SetMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        /// Sets the month for a specified date according to local time.
        [<Emit("new Date($0.setMonth($1...))")>]
        member _.SetMonth (month: int, ?date: int) : Date = jsNative
        
        /// Sets the seconds for a specified date according to local time.
        [<Emit("new Date($0.setSeconds($1...))")>]
        member _.SetSeconds (sec: int, ?ms: int) : Date = jsNative
        
        /// Sets the Date object to the time represented by a number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC. 
        ///
        /// Use negative numbers for times prior.
        [<Emit("new Date($0.setTime($1))")>]
        member _.SetTime (time: int64) : Date = jsNative
        
        /// Sets the day of the month for a specified date according to universal time.
        [<Emit("new Date($0.setUTCDate($1))")>]
        member _.SetUTCDate (date: int) : Date = jsNative
        
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// universal time.
        [<Emit("new Date($0.setUTCFullYear($1...))")>]
        member _.SetUTCFullYear (year: int, ?month: int, ?date: int) : Date = jsNative
        
        /// Sets the hour for a specified date according to universal time.
        [<Emit("new Date($0.setUTCHours($1...))")>]
        member _.SetUTCHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        /// Sets the milliseconds for a specified date according to universal time.
        [<Emit("new Date($0.setUTCMilliseconds($1))")>]
        member _.SetUTCMilliseconds (ms: int) : Date = jsNative
        
        /// Sets the minutes for a specified date according to universal time.
        [<Emit("new Date($0.setUTCMinutes($1...))")>]
        member _.SetUTCMinutes (min: int, ?sec: int, ?ms: int) : Date = jsNative
        
        /// Sets the month for a specified date according to universal time.
        [<Emit("new Date($0.setUTCMonth($1...))")>]
        member _.SetUTCMonth (month: int, ?date: int) : Date = jsNative
        
        /// Sets the seconds for a specified date according to universal time.
        [<Emit("new Date($0.setUTCSeconds($1...))")>]
        member _.SetUTCSeconds (sec: int, ?ms: int) : Date = jsNative
        
        /// Returns the "date" portion of the Date as a human-readable string like 'Thu Apr 12 2018'.
        [<Emit("$0.toDateString()")>]
        member _.ToDateString () : string = jsNative
        
        /// Returns a string representing the Date using toISOString(). 
        /// 
        /// Intended for use by JSON.stringify().
        [<Emit("$0.toJSON()")>]
        member _.ToJSON () : string = jsNative
        
        /// Returns a string with a locality sensitive representation of the date portion of this 
        /// date based on system settings.
        [<Emit("$0.toLocaleDateString()")>]
        member _.ToLocaleDateString () : string = jsNative
        
        /// Returns a string with a locality-sensitive representation of this date.
        [<Emit("$0.toLocaleString()")>]
        member _.ToLocaleString () : string = jsNative
        
        /// Returns a string with a locality-sensitive representation of the time portion of this
        /// date, based on system settings.
        [<Emit("$0.toLocaleTimeString()")>]
        member _.ToLocaleTimeString () : string = jsNative
        
        /// Converts a date to a string following the ISO 8601 Extended Format.
        [<Emit("$0.toISOString()")>]
        member _.ToISOString () : string = jsNative
        
        /// Returns a string representing the specified Date object.
        [<Emit("$0.toString()")>]
        override _.ToString () : string = jsNative
        
        /// Returns the "time" portion of the Date as a human-readable string.
        [<Emit("$0.toTimeString()")>]
        member _.ToTimeString () : string = jsNative
        
        /// Converts a date to a string using the UTC timezone.
        [<Emit("$0.toUTCString()")>]
        member _.ToUTCString () : string = jsNative
        
        /// Returns the primitive value of a Date object.
        [<Emit("$0.valueOf()")>]
        member _.ValueOf () : int64 = jsNative
    
        /// Like Date.now(), but returns a string value of the date.
        [<Emit("Date()")>]
        static member invoke () : string = jsNative
    
        /// Returns the current time in ticks.
        static member now () : int64 = jsNative

        /// Parses a string representation of a date and returns the ticks.
        static member parse (s: string) : int64 = jsNative
    
        /// Accepts parameters similar to the Date constructor, but treats them as UTC 
        /// in ticks.
        static member UTC (year: int, month: int, ?date: int, ?hours: int, ?minutes: int, ?seconds: int, ?ms: int) : int64 = jsNative
        
    /// Represents a single moment in time in a platform-independent format.
    [<Erase;RequireQualifiedAccess>]
    module Date =
        /// Converts the Date to System.DateTime.
        ///
        /// No runetime cost.
        let inline asDateTime (d: Date) = d.AsDateTime()
        
        /// Returns the day of the month (1-31) for the specified date according to local time.
        let inline getDate (d: Date) = d.GetDate()
        
        /// Returns the day of the week (0-6) for the specified date according to local time.
        let inline getDay (d: Date) = d.GetDay()
        
        /// Returns the year (4 digits for 4-digit years) of the specified date according to local time.
        let inline getFullYear (d: Date) = d.GetFullYear()
        
        /// Returns the hour (0-23) in the specified date according to local time.
        let inline getHours (d: Date) = d.GetHours()
        
        /// Returns the milliseconds (0-999) in the specified date according to local time.
        let inline getMilliseconds (d: Date) = d.GetMilliseconds()
        
        /// Returns the minutes (0-59) in the specified date according to local time.
        let inline getMinutes (d: Date) = d.GetMinutes()
        
        /// Returns the month (0-11) in the specified date according to local time.
        let inline getMonth (d: Date) = d.GetMonth()
        
        /// Returns the seconds (0-59) in the specified date according to local time.
        let inline getSeconds (d: Date) = d.GetSeconds()
        
        /// Returns the numeric value of the specified date as the number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
        let inline getTime (d: Date) = d.GetTime()
        
        /// Returns the numeric value of the specified date as the number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
        let inline getTimezoneOffset (d: Date) = d.GetTimezoneOffset()
        
        /// Returns the day (date) of the month (1-31) in the specified date according to 
        /// universal time.
        let inline getUTCDate (d: Date) = d.GetUTCDate()
        
        /// Returns the day of the week (0-6) in the specified date according to universal time.
        let inline getUTCDay (d: Date) = d.GetUTCDay()
        
        /// Returns the year (4 digits for 4-digit years) in the specified date according to 
        /// universal time.
        let inline getUTCFullYear (d: Date) = d.GetUTCFullYear()
        
        /// Returns the hours (0-23) in the specified date according to universal time.
        let inline getUTCHours (d: Date) = d.GetUTCHours()
        
        /// Returns the milliseconds (0-999) in the specified date according to universal time.
        let inline getUTCMilliseconds (d: Date) = d.GetUTCMilliseconds()
        
        /// Returns the minutes (0-59) in the specified date according to universal time.
        let inline getUTCMinutes (d: Date) = d.GetUTCMinutes()
        
        /// Returns the month (0-11) in the specified date according to universal time.
        let inline getUTCMonth (d: Date) = d.GetUTCMonth()
        
        /// Returns the seconds (0-59) in the specified date according to universal time.
        let inline getUTCSeconds (d: Date) = d.GetUTCSeconds()
        
        /// Sets the day of the month for a specified date according to local time.
        let inline setDate (date: int) (d: Date) = d.SetDate(date)
        
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// local time.
        let inline setFullYear (year: int) (d: Date) = d.SetFullYear(year)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// local time.
        let inline setFullYearM (year: int) (month: int) (d: Date) = d.SetFullYear(year, month = month)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// local time.
        let inline setFullYearD (year: int) (date: int) (d: Date) = d.SetFullYear(year, date = date)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// local time.
        let inline setFullYearMD (year: int) (month: int) (date: int) (d: Date) = d.SetFullYear(year, month, date)
        
        /// Sets the hours for a specified date according to local time.
        let inline setHours (hours: int) (d: Date) = d.SetHours(hours)
        /// Sets the hours for a specified date according to local time.
        let inline setHoursM (hours: int) (min: int) (d: Date) = d.SetHours(hours, min = min)
        /// Sets the hours for a specified date according to local time.
        let inline setHoursMS (hours: int) (min: int) (sec: int) (d: Date) = d.SetHours(hours, min = min, sec = sec)
        /// Sets the hours for a specified date according to local time.
        let inline setHoursMSM (hours: int) (min: int) (sec: int) (ms: int) (d: Date) = d.SetHours(hours, min, sec, ms)
        
        /// Sets the milliseconds for a specified date according to local time.
        let inline setMilliseconds (ms: int) (d: Date) = d.SetMilliseconds(ms)
        
        /// Sets the minutes for a specified date according to local time.
        let inline setMinutes (min: int) (d: Date) = d.SetMinutes(min)
        /// Sets the minutes for a specified date according to local time.
        let inline setMinutesS (min: int) (sec: int) (d: Date) = d.SetMinutes(min, sec = sec)
        /// Sets the minutes for a specified date according to local time.
        let inline setMinutesSM (min: int) (sec: int) (ms: int) (d: Date) = d.SetMinutes(min, sec, ms)
        
        /// Sets the month for a specified date according to local time.
        let inline setMonth (month: int) (d: Date) = d.SetMonth(month)
        /// Sets the month for a specified date according to local time.
        let inline setMonthD (month: int) (date: int) (d: Date) = d.SetMonth(month, date)
        
        /// Sets the seconds for a specified date according to local time.
        let inline setSeconds (sec: int) (d: Date) = d.SetSeconds(sec)
        /// Sets the seconds for a specified date according to local time.
        let inline setSecondsM (sec: int) (ms: int) (d: Date) = d.SetSeconds(sec, ms)
        
        /// Sets the Date object to the time represented by a number of milliseconds since 
        /// January 1, 1970, 00:00:00 UTC.
        ///
        /// Use negative numbers for times prior.
        let inline setTime (time: int64) (d: Date) = d.SetTime(time)
        
        /// Sets the day of the month for a specified date according to universal time.
        let inline setUTCDate (date: int) (d: Date) = d.SetUTCDate(date)
        
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// universal time.
        let inline setUTCFullYear (year: int) (d: Date) = d.SetUTCFullYear(year)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// universal time.
        let inline setUTCFullYearM (year: int) (month: int) (d: Date) = d.SetUTCFullYear(year, month = month)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// universal time.
        let inline setUTCFullYearD (year: int) (date: int) (d: Date) = d.SetUTCFullYear(year, date = date)
        /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
        /// universal time.
        let inline setUTCFullYearMD (year: int) (month: int) (date: int) (d: Date) = d.SetUTCFullYear(year, month, date)
        
        /// Sets the hour for a specified date according to universal time.
        let inline setUTCHours (hours: int) (d: Date) = d.SetUTCHours(hours)
        /// Sets the hour for a specified date according to universal time.
        let inline setUTCHoursM (hours: int) (min: int) (d: Date) = d.SetUTCHours(hours, min = min)
        /// Sets the hour for a specified date according to universal time.
        let inline setUTCHoursMS (hours: int) (min: int) (sec: int) (d: Date) = d.SetUTCHours(hours, min = min, sec = sec)
        /// Sets the hour for a specified date according to universal time.
        let inline setUTCHoursMSM (hours: int) (min: int) (sec: int) (ms: int) (d: Date) = d.SetUTCHours(hours, min, sec, ms)
        
        /// Sets the milliseconds for a specified date according to universal time.
        let inline setUTCMilliseconds (ms: int) (d: Date) = d.SetUTCMilliseconds(ms)
        
        /// Sets the minutes for a specified date according to universal time.
        let inline setUTCMinutes (min: int) (d: Date) = d.SetUTCMinutes(min)
        /// Sets the minutes for a specified date according to universal time.
        let inline setUTCMinutesS (min: int) (sec: int) (d: Date) = d.SetUTCMinutes(min, sec = sec)
        /// Sets the minutes for a specified date according to universal time.
        let inline setUTCMinutesMS (min: int) (sec: int) (ms: int) (d: Date) = d.SetUTCMinutes(min, sec, ms)
        
        /// Sets the month for a specified date according to universal time.
        let inline setUTCMonth (month: int) (d: Date) = d.SetUTCMonth(month)
        /// Sets the month for a specified date according to universal time.
        let inline setUTCMonthD (month: int) (date: int) (d: Date) = d.SetUTCMonth(month, date)
        
        /// Sets the seconds for a specified date according to universal time.
        let inline setUTCSeconds (sec: int) (d: Date) = d.SetUTCSeconds(sec)
        /// Sets the seconds for a specified date according to universal time.
        let inline setUTCSecondsM (sec: int) (ms: int) (d: Date) = d.SetUTCSeconds(sec, ms)
        
        /// Returns the "date" portion of the Date as a human-readable string like 'Thu Apr 12 2018'.
        let inline toDateString (d: Date) = d.ToDateString()
        
        /// Returns a string representing the Date using toISOString(). 
        /// 
        /// Intended for use by JSON.stringify().
        let inline toJSON (d: Date) = d.ToJSON()
        
        /// Returns a string with a locality sensitive representation of the date portion of this 
        /// date based on system settings.
        let inline toLocaleDateString (d: Date) = d.ToLocaleDateString()
        
        /// Returns a string with a locality-sensitive representation of this date.
        let inline toLocaleString (d: Date) = d.ToLocaleString()
        
        /// Returns a string with a locality-sensitive representation of the time portion of this
        /// date, based on system settings.
        let inline toLocaleTimeString (d: Date) = d.ToLocaleTimeString()
        
        /// Converts a date to a string following the ISO 8601 Extended Format.
        let inline toISOString (d: Date) = d.ToISOString()
        
        /// Returns a string representing the specified Date object.
        let inline toString (d: Date) = d.ToString()
        
        /// Returns the "time" portion of the Date as a human-readable string.
        let inline toTimeString (d: Date) = d.ToTimeString()
        
        /// Converts a date to a string using the UTC timezone.
        let inline toUTCString (d: Date) = d.ToUTCString()
        
        /// Returns the primitive value of a Date object.
        let inline valueOf (d: Date) = d.ValueOf()

    /// Functions for parsing JavaScript Object Notation (JSON) and converting values to JSON.
    [<Erase>]
    type JSON =
        /// <summary>
        /// Parse the string text as JSON, optionally transform the produced value and its properties, 
        /// and return the value. 
        ///
        /// The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
        /// A function to map each key (first parameter) and value (second parameter) pair before the end result.
        /// </summary>
        /// <exception cref="System.Exception">Any violations of the JSON syntax, including those pertaining to the differences between 
        /// JavaScript and JSON, cause a SyntaxError to be thrown. 
        /// </exception>
        [<Emit("JSON.parse($0...)")>]
        static member parse (text: string, ?reviver: obj -> obj -> obj) : obj = jsNative

        /// Parse the string text as JSON, optionally transform the produced value and its properties, 
        /// and return the value. 
        ///
        /// The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
        /// A function to map each key (first parameter) and value (second parameter) pair before the end result.
        static member inline tryParse (text: string, ?reviver: obj -> obj -> obj) =
            try JSON.parse(text, ?reviver = reviver) |> Ok
            with e -> Error e

        /// <summary>
        /// Parse the string text as JSON, optionally transform the produced value and its properties, 
        /// and return the value. 
        ///
        /// The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
        /// A function to map each key (first parameter) and value (second parameter) pair before the end result.
        ///
        /// This does not validate the returned type.
        /// </summary>
        /// <exception cref="System.Exception">Any violations of the JSON syntax, including those pertaining to the differences between 
        /// JavaScript and JSON, cause a SyntaxError to be thrown. 
        /// </exception>
        [<Emit("JSON.parse($0...)")>]
        static member parseAs<'T> (text: string, ?reviver: obj -> obj -> obj) : 'T = jsNative

        /// Parse the string text as JSON, optionally transform the produced value and its properties, 
        /// and return the value. 
        ///
        /// The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
        /// A function to map each key (first parameter) and value (second parameter) pair before the end result.
        ///
        /// This does not validate the returned type.
        static member inline tryParseAs<'T> (text: string, ?reviver: obj -> obj -> obj) =
            try JSON.parseAs<'T>(text, ?reviver = reviver) |> Ok
            with e -> Error e
        
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, space: int) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, space: string) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: string -> obj -> obj) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: seq<obj>) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: string -> obj -> obj, space: int) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: seq<obj>, space: int) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: string -> obj -> obj, space: string) : string = jsNative
        /// Return a JSON string corresponding to the specified value, optionally including only certain properties 
        /// or replacing property values in a user-defined manner. 
        ///
        /// By default, all instances of undefined are replaced with null, and other unsupported native data types 
        /// are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
        /// and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).
        ///
        /// The space option indicates how to insert white space into the output of the JSON for readability.
        ///
        /// If an integer it indicates the number of space characters to use as white space; this number is capped at 
        /// 10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.
        ///
        /// If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 
        /// 
        /// If no space is provided, no white space is used.
        [<Emit("JSON.stringify($0...)")>]
        static member stringify (value: obj, replacer: seq<obj>, space: string) : string = jsNative

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
        
        /// When the promise is settled, i.e either fulfilled or rejected, the specified 
        /// callback function is executed.
        [<Emit("$0.finally($1)")>]
        member _.finally' (handler: unit -> unit) : Promise<'T> = jsNative
        
        /// Takes a sequence of Promises, and returns a single Promise that resolves to 
        /// an array of the results of the input promises. This returned promise will 
        /// resolve when all of the input's promises have resolved, or if the input 
        /// iterable contains no promises. It rejects immediately upon any of the input 
        /// promises rejecting or non-promises throwing an error, and will reject with 
        /// this first rejection message / error.
        static member all (promises: seq<#JS.Promise<'T>>) : Promise<'T []> = jsNative

        /// Returns a promise that fulfills or rejects as soon as one of the promises in 
        /// an iterable fulfills or rejects, with the value or reason from that promise.
        static member race (values: seq<#JS.Promise<'T>>) : Promise<'T> = jsNative

        /// Returns a Promise object that is rejected with a given reason.
        static member reject () : Promise<unit> = jsNative
        /// Returns a Promise object that is rejected with a given reason.
        static member reject (reason: 'Reason) : Promise<'Reason> = jsNative
        
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a 
        /// thenable (i.e. has a "then" method), the returned promise will "follow" 
        /// that thenable, adopting its eventual state; otherwise the returned promise 
        /// will be fulfilled with the value. This function flattens nested layers of 
        /// promise-like objects (e.g. a promise that resolves to a promise that 
        /// resolves to something) into a single layer.
        static member resolve () : Promise<unit> = jsNative
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a 
        /// thenable (i.e. has a "then" method), the returned promise will "follow" 
        /// that thenable, adopting its eventual state; otherwise the returned promise 
        /// will be fulfilled with the value. This function flattens nested layers of 
        /// promise-like objects (e.g. a promise that resolves to a promise that 
        /// resolves to something) into a single layer.
        static member resolve (promise: #JS.Promise<'T>) : Promise<'T> = jsNative

        interface JS.Promise<'T> with
            member this.catch f = upcast this.catch (unbox f)
            member this.``then`` (fulfilled, rejected) = upcast this.then'(unbox fulfilled, unbox rejected)
    
    /// Used to represent a generic, fixed-length raw binary data buffer.
    [<Global>]
    type ArrayBuffer (byteLength: int) =
        [<Emit("$0")>]
        new (ab: JS.ArrayBuffer) = ArrayBuffer(0)

        [<Emit("$0")>]
        new (b: System.Buffer) = ArrayBuffer(0)

        /// The read-only size, in bytes, of the ArrayBuffer.
        [<Emit("$0.byteLength")>]
        member _.ByteLength: int = jsNative
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        [<Emit("$0.slice($1...)")>]
        member _.Slice (?begin': int, ?end': int) : ArrayBuffer = jsNative
            
        /// Determines whether the passed value is one of the ArrayBuffer views, 
        /// such as typed array objects or a DataView.
        static member isView (maybeView: obj) : bool = jsNative
    
        interface JS.ArrayBuffer with
            member this.byteLength = this.ByteLength
            member this.slice (begin', end') = this.Slice(begin', ?end' = end') :> JS.ArrayBuffer

    [<Erase;RequireQualifiedAccess>]
    module ArrayBuffer =
        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (ab: ArrayBuffer) = ab.ByteLength

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from beginning index, 
        /// inclusive, up to end index, exclusive.
        let inline slice (begin': int) (end': int) (ab: ArrayBuffer) = ab.Slice(begin', end')

        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from the 
        /// given index, inclusive to the end of the ArrayBuffer.
        let inline sliceBegin (begin': int) (ab: ArrayBuffer) = ab.Slice(begin' = begin')
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from the beginning of 
        /// the ArrayBuffer to the given index, exclusive.
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
        
        /// Gets a signed 32-bit float (float) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getFloat32($1...)")>]
        member _.GetFloat32 (byteOffset: int, ?littleEndian: bool) : float32 = jsNative
        
        /// Gets a signed 64-bit float (double) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getFloat64($1...)")>]
        member _.GetFloat64 (byteOffset: int, ?littleEndian: bool) : float = jsNative
        
        /// Gets a signed 8-bit integer (byte) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getInt8($1...)")>]
        member _.GetInt8 (byteOffset: int) : sbyte = jsNative
        
        /// Gets a signed 16-bit integer (short) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getInt16($1...)")>]
        member _.GetInt16 (byteOffset: int, ?littleEndian: bool) : int16 = jsNative
        
        /// Gets a signed 32-bit integer (long) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getInt32($1...)")>]
        member _.GetInt32 (byteOffset: int, ?littleEndian: bool) : int32 = jsNative
        
        /// Gets a signed 64-bit integer (long long) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getBigInt64($1...)")>]
        member _.GetInt64 (byteOffset: int, ?littleEndian: bool) : int32 = jsNative

        /// Gets an unsigned 8-bit integer (unsigned byte) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getUint8($1...)")>]
        member _.GetUint8 (byteOffset: int) : byte = jsNative
        
        /// Gets an unsigned 16-bit integer (unsigned short) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getUint16($1...)")>]
        member _.GetUint16 (byteOffset: int, ?littleEndian: bool) : uint16 = jsNative
        
        /// Gets an unsigned 32-bit integer (unsigned long) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getUint32($1...)")>]
        member _.GetUint32 (byteOffset: int, ?littleEndian: bool) : uint32 = jsNative

        /// Gets an unsigned 64-bit integer (unsigned long long) at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.getBigUint64($1...)")>]
        member _.GetUint64 (byteOffset: int, ?littleEndian: bool) : uint64 = jsNative
        
        /// Stores a signed 32-bit float (float) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setFloat32($1...)")>]
        member _.SetFloat32 (byteOffset: int, value: float32, ?littleEndian: bool) : unit = jsNative
        
        /// Stores a signed 64-bit float (double) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setFloat64($1...)")>]
        member _.SetFloat64 (byteOffset: int, value: float, ?littleEndian: bool) : unit = jsNative
        
        /// Stores a signed 8-bit integer (byte) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setInt8($1...)")>]
        member _.SetInt8 (byteOffset: int, value: sbyte) : unit = jsNative
        
        /// Stores a signed 16-bit integer (short) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setInt16($1...)")>]
        member _.SetInt16 (byteOffset: int, value: int16, ?littleEndian: bool) : unit = jsNative
        
        /// Stores a signed 32-bit integer (long) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setInt32($1...)")>]
        member _.SetInt32 (byteOffset: int, value: int32, ?littleEndian: bool) : unit = jsNative
        
        /// Stores a signed 64-bit integer (long long) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setBigInt64($1...)")>]
        member _.SetInt64 (byteOffset: int, value: int64, ?littleEndian: bool) : unit = jsNative

        /// Stores an unsigned 8-bit integer (unsigned byte) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setUint8($1...)")>]
        member _.SetUint8 (byteOffset: int, value: byte) : unit = jsNative
        
        /// Stores an unsigned 16-bit integer (unsigned short) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setUint16($1...)")>]
        member _.SetUint16 (byteOffset: int, value: uint16, ?littleEndian: bool) : unit = jsNative
        
        /// Stores an unsigned 32-bit integer (unsigned long) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setUint32($1...)")>]
        member _.SetUint32 (byteOffset: int, value: uint32, ?littleEndian: bool) : unit = jsNative
        
        /// Stores an unsigned 64-bit integer (unsigned long long) value at the specified byte 
        /// offset from the start of the view.
        [<Emit("$0.setBigUint64($1...)")>]
        member _.SetUint64 (byteOffset: int, value: uint64, ?littleEndian: bool) : unit = jsNative

        interface JS.ArrayBufferView with
            member this.buffer = upcast this.Buffer
            member this.byteLength = this.ByteLength
            member this.byteOffset = this.ByteOffset
            
    /// Provides a low-level interface for reading and writing multiple number types in a binary 
    /// ArrayBuffer, without having to care about the platform's endianness.
    [<Erase;RequireQualifiedAccess>]
    module DataView =
        /// The ArrayBuffer referenced by a DataView at construction time.
        let inline buffer (dv: DataView) = dv.Buffer

        /// The read-only size, in bytes, of the ArrayBuffer. 
        let inline byteLength (dv: DataView) = dv.ByteLength

        /// The offset (in bytes) of this view from the start of its ArrayBuffer.
        let inline byteOffset (dv: DataView) = dv.ByteOffset

        /// Gets a signed 32-bit float (float) at the specified byte 
        /// offset from the start of the view.
        let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.GetFloat32(byteOffset)
        
        /// Gets a signed 64-bit float (double) at the specified byte 
        /// offset from the start of the view.
        let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.GetFloat64(byteOffset)
        
        /// Gets a signed 8-bit integer (byte) at the specified byte 
        /// offset from the start of the view.
        let inline getInt8 (byteOffset: int) (dv: DataView) = dv.GetInt8(byteOffset)
        
        /// Gets a signed 16-bit integer (short) at the specified byte 
        /// offset from the start of the view.
        let inline getInt16 (byteOffset: int) (dv: DataView) = dv.GetInt16(byteOffset)
        
        /// Gets a signed 32-bit integer (long) at the specified byte 
        /// offset from the start of the view.
        let inline getInt32 (byteOffset: int) (dv: DataView) = dv.GetInt32(byteOffset)
        
        /// Gets a signed 64-bit integer (long long) at the specified byte 
        /// offset from the start of the view.
        let inline getInt64 (byteOffset: int) (dv: DataView) = dv.GetInt64(byteOffset)
        
        /// Gets an unsigned 8-bit integer (unsigned byte) at the specified byte 
        /// offset from the start of the view.
        let inline getUint8 (byteOffset: int) (dv: DataView) = dv.GetUint8(byteOffset)
        
        /// Gets an unsigned 16-bit integer (unsigned short) at the specified byte 
        /// offset from the start of the view.
        let inline getUint16 (byteOffset: int) (dv: DataView) = dv.GetUint16(byteOffset)
        
        /// Gets an unsigned 32-bit integer (unsigned long) at the specified byte 
        /// offset from the start of the view.
        let inline getUint32 (byteOffset: int) (dv: DataView) = dv.GetUint32(byteOffset)
        
        /// Gets an unsigned 64-bit integer (unsigned long long) at the specified byte 
        /// offset from the start of the view.
        let inline getUint64 (byteOffset: int) (dv: DataView) = dv.GetUint64(byteOffset)
        
        /// Stores a signed 32-bit float (float) value at the specified byte 
        /// offset from the start of the view.
        let inline setFloat32 (byteOffset: int) (value: float32) (dv: DataView) = 
            dv.SetFloat32(byteOffset, value)
            dv

        /// Stores a signed 64-bit float (double) value at the specified byte 
        /// offset from the start of the view.
        let inline setFloat64 (byteOffset: int) ( value: float) (dv: DataView) = 
            dv.SetFloat64(byteOffset, value)
            dv
        
        /// Stores a signed 8-bit integer (byte) value at the specified byte 
        /// offset from the start of the view.
        let inline setInt8 (byteOffset: int) ( value: sbyte) (dv: DataView) = 
            dv.SetInt8(byteOffset, value)
            dv
        
        /// Stores a signed 16-bit integer (short) value at the specified byte 
        /// offset from the start of the view.
        let inline setInt16 (byteOffset: int) ( value: int16) (dv: DataView) = 
            dv.SetInt16(byteOffset, value)
            dv
        
        /// Stores a signed 32-bit integer (long) value at the specified byte 
        /// offset from the start of the view.
        let inline setInt32 (byteOffset: int) ( value: int32) (dv: DataView) = 
            dv.SetInt32(byteOffset, value)
            dv
        
        /// Stores a signed 64-bit integer (long long) value at the specified byte 
        /// offset from the start of the view.
        let inline setInt64 (byteOffset: int) ( value: int64) (dv: DataView) = 
            dv.SetInt64(byteOffset, value)
            dv
        
        /// Stores an unsigned 8-bit integer (unsigned byte) value at the specified byte 
        /// offset from the start of the view.
        let inline setUint8 (byteOffset: int) ( value: byte) (dv: DataView) = 
            dv.SetUint8(byteOffset, value)
            dv
        
        /// Stores an unsigned 16-bit integer (unsigned short) value at the specified byte 
        /// offset from the start of the view.
        let inline setUint16 (byteOffset: int) ( value: uint16) (dv: DataView) = 
            dv.SetUint16(byteOffset, value)
            dv
        
        /// Stores an unsigned 32-bit integer (unsigned long) value at the specified byte 
        /// offset from the start of the view.
        let inline setUint32 (byteOffset: int) ( value: uint32) (dv: DataView) = 
            dv.SetUint32(byteOffset, value)
            dv
        
        /// Stores an unsigned 64-bit integer (unsigned long long) value at the specified byte 
        /// offset from the start of the view.
        let inline setUint64 (byteOffset: int) ( value: uint64) (dv: DataView) = 
            dv.SetUint64(byteOffset, value)
            dv

        /// DataView getters and setters with littleEndian set to true.
        [<Erase>]
        module LittleEndian =
            /// Gets a signed 32-bit float (float) at the specified byte 
            /// offset from the start of the view.
            let inline getFloat32 (byteOffset: int) (dv: DataView) = dv.GetFloat32(byteOffset, true)
            
            /// Gets a signed 64-bit float (double) at the specified byte 
            /// offset from the start of the view.
            let inline getFloat64 (byteOffset: int) (dv: DataView) = dv.GetFloat64(byteOffset, true)
            
            /// Gets a signed 16-bit integer (short) at the specified byte 
            /// offset from the start of the view.
            let inline getInt16 (byteOffset: int) (dv: DataView) = dv.GetInt16(byteOffset, true)
            
            /// Gets a signed 32-bit integer (long) at the specified byte 
            /// offset from the start of the view.
            let inline getInt32 (byteOffset: int) (dv: DataView) = dv.GetInt32(byteOffset, true)
            
            /// Gets a signed 64-bit integer (long long) at the specified byte 
            /// offset from the start of the view.
            let inline getInt64 (byteOffset: int) (dv: DataView) = dv.GetInt64(byteOffset, true)

            /// Gets an unsigned 16-bit integer (unsigned short) at the specified byte 
            /// offset from the start of the view.
            let inline getUint16 (byteOffset: int) (dv: DataView) = dv.GetUint16(byteOffset, true)
            
            /// Gets an unsigned 32-bit integer (unsigned long) at the specified byte 
            /// offset from the start of the view.
            let inline getUint32 (byteOffset: int) (dv: DataView) = dv.GetUint32(byteOffset, true)
            
            /// Gets an unsigned 64-bit integer (unsigned long long) at the specified byte 
            /// offset from the start of the view.
            let inline getUint64 (byteOffset: int) (dv: DataView) = dv.GetUint64(byteOffset, true)

            /// Stores a signed 32-bit float (float) value at the specified byte 
            /// offset from the start of the view.
            let inline setFloat32 (byteOffset: int) ( value: float32) (dv: DataView) = 
                dv.SetFloat32(byteOffset, value, true)
            
            /// Stores a signed 64-bit float (double) value at the specified byte 
            /// offset from the start of the view.
            let inline setFloat64 (byteOffset: int) ( value: float) (dv: DataView) = 
                dv.SetFloat64(byteOffset, value, true)
                dv

            /// Stores a signed 16-bit integer (short) value at the specified byte 
            /// offset from the start of the view.
            let inline setInt16 (byteOffset: int) ( value: int16) (dv: DataView) = 
                dv.SetInt16(byteOffset, value, true)
                dv
            
            /// Stores a signed 32-bit integer (long) value at the specified byte 
            /// offset from the start of the view.
            let inline setInt32 (byteOffset: int) ( value: int32) (dv: DataView) = 
                dv.SetInt32(byteOffset, value, true)
                dv
            
            /// Stores a signed 64-bit integer (long long) value at the specified byte 
            /// offset from the start of the view.
            let inline setInt64 (byteOffset: int) ( value: int64) (dv: DataView) = 
                dv.SetInt64(byteOffset, value, true)
                dv
            
            /// Stores an unsigned 16-bit integer (unsigned short) value at the specified byte 
            /// offset from the start of the view.
            let inline setUint16 (byteOffset: int) ( value: uint16) (dv: DataView) = 
                dv.SetUint16(byteOffset, value, true)
                dv
            
            /// Stores an unsigned 32-bit integer (unsigned long) value at the specified byte 
            /// offset from the start of the view.
            let inline setUint32 (byteOffset: int) ( value: uint32) (dv: DataView) = 
                dv.SetUint32(byteOffset, value, true)
                dv
            
            /// Stores an unsigned 64-bit integer (unsigned long long) value at the specified byte 
            /// offset from the start of the view.
            let inline setUint64 (byteOffset: int) ( value: uint64) (dv: DataView) = 
                dv.SetUint64(byteOffset, value, true)
                dv
        
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
        ///
        /// Provides the element and index.
        [<Emit("$0.filter($1)")>]
        member _.Filter (f: 'T -> int -> bool) : TypedArray<'T> = jsNative
        /// Creates a new typed array with all elements that pass the test 
        /// implemented by the provided function.
        ///
        /// Provides the element, index, and array.
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
        /// Provides the element and index.
        [<Emit("$0.forEach($1)")>]
        member _.ForEach (f: 'T -> int -> bool) : unit = jsNative
        /// Executes a provided function once per array element.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.forEach($1)")>]
        member _.ForEach (f: 'T -> int -> TypedArray<'T> -> bool) : unit = jsNative

        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        [<Emit("$0.includes($1...)")>]
        member _.Includes (searchElement: 'T, ?fromIndex: int) : bool = jsNative

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        [<Emit("$0.indexOf($1...)")>]
        member _.IndexOf (searchElement: 'T, ?fromIndex: int) : int = jsNative

        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        [<Emit("$0.LastIndexOf($1...)")>]
        member _.LastIndexOf (searchElement: 'T, ?fromIndex: int) : int = jsNative
        
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        [<Emit("$0.map($1)")>]
        member _.Map (f: 'T -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        ///
        /// Provides the element and index.
        [<Emit("$0.map($1)")>]
        member _.MapWithIndex (f: 'T -> int -> 'U) : TypedArray<'U> = jsNative
        /// Creates a new typed array with the results of calling a provided 
        /// function on every element in this typed array.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.map($1)")>]
        member _.MapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) : TypedArray<'U> = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        ///
        /// Provides the element and index.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        ///
        /// Provides the element, index, and array.
        [<Emit("$0.reduce($1,$2)")>]
        member _.Reduce (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State = jsNative
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.ReduceRight (f: 'State -> 'T -> 'State, state:'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        ///
        /// Provides the element and index.
        [<Emit("$0.reduceRight($1,$2)")>]
        member _.ReduceRight (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State = jsNative
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        ///
        /// Provides the element, index, and array.
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
        ///
        /// Provides the element and index.
        [<Emit("$0.some($1)")>]
        member _.Some (f: 'T -> int -> bool) : bool = jsNative
        /// Tests whether some element in the typed array passes the test implemented by 
        /// the provided function.
        ///
        /// Provides the element, index, and array.
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
            member this.sort (?sortFunction) = upcast this
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<'T>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<'T> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<'T>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    /// Describes an array-like view of an underlying binary data buffer.
    [<Erase;RequireQualifiedAccess>]
    module TypedArray =        
        /// The ArrayBuffer referenced by a TypedArray at construction time.
        let inline buffer (ta: TypedArray<'T>) = ta.Buffer
        
        /// The length (in bytes) of a typed array.
        let inline byteLength (ta: TypedArray<'T>) = ta.ByteLength
        
        /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
        let inline byteOffset (ta: TypedArray<'T>) = ta.ByteOffset
        
        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second argument start.
        let inline copyWithin (targetStartIndex: int) (start: int) (ta: TypedArray<'T>) = 
            ta.CopyWithin(targetStartIndex, start)
            ta

        /// Copies the sequence of array elements within the array to the position 
        /// starting at target. 
        ///
        /// The copy is taken from the index positions of the second and third arguments 
        /// start and end.
        let inline copyWithinEnd (targetStartIndex: int) (start: int) (end': int) (ta: TypedArray<'T>) = 
            ta.CopyWithin(targetStartIndex, start, end')
            ta

        /// Returns a sequence that contains the key/value pairs for each index in the array.
        let inline entries (ta: TypedArray<'T>) = ta.Entries()

        /// The length (in elements) of a typed array.
        let inline length (ta: TypedArray<'T>) = ta.Length

        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fill (value: 'T) (ta: TypedArray<'T>) = ta.Fill(value)
        
        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillEnd (value: 'T) (end': int) (ta: TypedArray<'T>) = ta.Fill(value, end' = end')

        /// Fills all the elements of a typed array from a start index to an end index with 
        /// a static value.
        let inline fillStart (value: 'T) (start': int) (ta: TypedArray<'T>) = ta.Fill(value, start' = start')
        
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
        let inline findWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.Find(f)

        /// Returns a value in the typed array, if an element satisfies the provided testing function.
        let inline findWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.Find(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        let inline findIndex (f: 'T -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        let inline findIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)

        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function. Otherwise -1 is returned.
        let inline findIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.FindIndex(f)
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        let inline tryFindIndex (f: 'T -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        let inline tryFindIndexWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Returns an index in the typed array, if an element in the typed array satisfies the 
        /// provided testing function.
        let inline tryFindIndexWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = 
            ta.FindIndex(f) |> fun i -> if i < 0 then None else Some i
        
        /// Executes a provided function once per array element.
        let inline forEach (f: 'T -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Executes a provided function once per array element.
        let inline forEachWithIndex (f: 'T -> int -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Executes a provided function once per array element.
        let inline forEachWithIndexArray (f: 'T -> int -> TypedArray<'T> -> bool) (ta: TypedArray<'T>) = ta.ForEach(f)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includes (searchElement: 'T) (ta: TypedArray<'T>) = ta.Includes(searchElement)
        
        /// Determines whether a typed array includes a certain element, 
        /// returning true or false as appropriate.
        let inline includesFromIndex (searchElement: 'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.Includes(searchElement, fromIndex)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOf (searchElement: 'T) (ta: TypedArray<'T>) = ta.IndexOf(searchElement)

        /// Returns the first index at which a given element can be found 
        /// in the typed array, or -1 if it is not present.
        let inline indexOfFromIndex (searchElement: 'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.IndexOf(searchElement, fromIndex)

        /// Tries to return the first index at which a given element can be found 
        /// in the typed array.
        let inline tryIndexOf (searchElement: 'T) (ta: TypedArray<'T>) =
            ta.IndexOf(searchElement) |> fun i -> if i < 0 then None else Some i
            
        /// Tries to return the first index at which a given element can be found 
        /// in the typed array.
        let inline tryIndexOfFromIndex (searchElement: 'T) (fromIndex: int) (ta: TypedArray<'T>) =
            ta.IndexOf(searchElement, fromIndex) |> fun i -> if i < 0 then None else Some i

        /// Joins all elements of an array into a string.
        let inline join (sep: string) (ta: TypedArray<'T>) = ta.Join(sep)

        /// The keys for each index in the array.
        let inline keys (ta: TypedArray<'T>) = ta.Keys()

        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOf (searchElement: 'T) (ta: TypedArray<'T>) = ta.LastIndexOf(searchElement)
        
        /// Returns the last index at which a given element can be found in 
        /// the typed array, or -1 if it is not present.
        let inline lastIndexOfFromIndex (searchElement: 'T) (fromIndex: int) (ta: TypedArray<'T>) = ta.LastIndexOf(searchElement, fromIndex)
        
        /// Tries to return the last index at which a given element can be found 
        /// in the typed array.
        let inline tryLastIndexOf (searchElement: 'T) (ta: TypedArray<'T>) =
            ta.LastIndexOf(searchElement) |> fun i -> if i < 0 then None else Some i
            
        /// Tries to return the last index at which a given element can be found 
        /// in the typed array.
        let inline tryLastIndexOfFromIndex (searchElement: 'T) (fromIndex: int) (ta: TypedArray<'T>) =
            ta.LastIndexOf(searchElement, fromIndex) |> fun i -> if i < 0 then None else Some i

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
        let inline reduceWithIndex (f: 'State -> 'T -> int -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.Reduce(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from left-to-right) has to reduce it to a single value.
        let inline reduceWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.Reduce(f, state)
        
        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRight (f: 'State -> 'T -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndex (f: 'State -> 'T -> int -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)

        /// Applies a function against an accumulator and each value of the typed 
        /// array (from right-to-left) has to reduce it to a single value.
        let inline reduceRightWithIndexArray (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) (state: 'State) (ta: TypedArray<'T>) = ta.ReduceRight(f, state)
        
        /// Reverses a typed array in place.
        let inline reverse (ta: TypedArray<'T>) = ta.Reverse()
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArray (source: System.Array) (ta: TypedArray<'T>) = 
            ta.Set(source)
            ta

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setArrayOffset (source: System.Array) (offset: int) (ta: TypedArray<'T>) = 
            ta.Set(source, offset)
            ta

        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArray (source: #JS.TypedArray) (ta: TypedArray<'T>) = 
            ta.Set(source)
            ta
        
        /// Stores multiple values in the typed array, reading input values from a specified array.
        let inline setTypedArrayOffset (source: #JS.TypedArray) (offset: int) (ta: TypedArray<'T>)= 
            ta.Set(source, offset)
            ta

        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeq (source: seq<'T>) (ta: TypedArray<'T>) = 
            ta.Set(unbox<System.Array> (ResizeArray source))
            ta
        
        /// Stores multiple values in the typed array, reading input values from a given sequence.
        let inline setSeqOffset (source: seq<'T>) (offset: int) (ta: TypedArray<'T>) = 
            ta.Set(unbox<System.Array> (ResizeArray source), offset = offset)
            ta
        
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
        
        /// Sorts the elements of a typed array numerically in place using the provided function
        /// and returns the typed array. 
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<int8>) = Int8Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<uint8>) = Uint8Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<uint8>) = Uint8ClampedArray()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<int16>) = Int16Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<uint16>) = Uint16Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<int32>) = Int32Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<uint32>) = Uint32Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<float32>) = Float32Array()
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
            member this.sort (?sortFunction) = upcast this
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
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<float>) = Float64Array()
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
            member this.sort (?sortFunction) = upcast this
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<float>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<float> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<float>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Int64Array private () = 
        inherit TypedArray<int64>()
            
        [<Emit "new BigInt64Array($0)">]
        new (size: int) = Int64Array()
        [<Emit("new BigInt64Array($0)")>]
        new (typedArray: JS.TypedArray) = Int64Array()
        [<Emit("new BigInt64Array($0)")>]
        new (typedArray: TypedArray<bigint>) = Int64Array()
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<bigint>) = Int64Array()
        [<Emit("new BigInt64Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Int64Array()
        [<Emit("new BigInt64Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Int64Array()
        [<Emit "new BigInt64Array($0)">]
        new (seq: seq<bigint>) = Int64Array()
    
        /// The size in bytes of each element in the typed array.
        [<Emit("BigInt64Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
        
        /// A string value of the typed array constructor name.
        [<Emit("BigInt64Array.name")>]
        static member name : string = jsNative
        
        /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
        /// typed array to this representation.
        static member inline cast (a: JS.BigInt64Array) = unbox<Int64Array> a

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

        interface JS.TypedArray<int64> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<int64>>()
            member _.filter (f: int64 -> bool) = unbox<JS.TypedArray<int64>>()
            member _.filter (f: int64 -> int -> bool) = unbox<JS.TypedArray<int64>>()
            member _.filter (f: int64 -> int -> JS.TypedArray<int64> -> bool) = unbox<JS.TypedArray<int64>>()
            member _.find (f: int64 -> bool) = unbox<int64 option>()
            member _.find (f: int64 -> int -> bool) = unbox<int64 option>()
            member _.find (f: int64 -> int -> JS.TypedArray<int64> -> bool) = unbox<int64 option>()
            member _.findIndex (f: int64 -> bool) = 1
            member _.findIndex (f: int64 -> int -> bool) = 1
            member _.findIndex (f: int64 -> int -> JS.TypedArray<int64> -> bool) = 1
            member _.forEach (f: int64 -> bool) = ()
            member _.forEach (f: int64 -> int -> bool) = ()
            member _.forEach (f: int64 -> int -> JS.TypedArray<int64> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: int64 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int64 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: int64 -> int -> JS.TypedArray<int64> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> int64 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int64 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> int64 -> int -> JS.TypedArray<int64> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int64 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int64 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> int64 -> int -> JS.TypedArray<int64> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<int64>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<int64>>()
            member _.some (f: int64 -> bool) = true
            member _.some (f: int64 -> int -> bool) = true
            member _.some (f: int64 -> int -> JS.TypedArray<int64> -> bool) = true
            member this.sort (?sortFunction) = upcast this
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<int64>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<int64> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<int64>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    [<Erase>]
    type Uint64Array private () = 
        inherit TypedArray<uint64>()
        
        [<Emit "new BigUint64Array($0)">]
        new (size: int) = Uint64Array()
        [<Emit("new BigUint64Array($0)")>]
        new (typedArray: JS.TypedArray) = Uint64Array()
        [<Emit("new BigUint64Array($0)")>]
        new (typedArray: TypedArray<bigint>) = Uint64Array()
        [<Emit("$0")>]
        new (typedArray: JS.TypedArray<bigint>) = Uint64Array()
        [<Emit("new BigUint64Array($0...)")>]
        new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int) = Uint64Array()
        [<Emit("new BigUint64Array($0...)")>]
        new (buffer: ArrayBuffer, ?offset: int, ?length: int) = Uint64Array()
        [<Emit "new BigUint64Array($0)">]
        new (seq: seq<bigint>) = Uint64Array()

        /// The size in bytes of each element in the typed array.
        [<Emit("BigUint64Array.BYTES_PER_ELEMENT")>]
        static member bytesPerElement : int = jsNative
    
        /// A string value of the typed array constructor name.
        [<Emit("BigUint64Array.name")>]
        static member name : string = jsNative
    
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

        interface JS.TypedArray<uint64> with
            member this.Item
                with get i = this.Item i
                and set i x = this.[i] <- unbox x

            member _.fill (value, begin', end') = unbox<JS.TypedArray<uint64>>()
            member _.filter (f: uint64 -> bool) = unbox<JS.TypedArray<uint64>>()
            member _.filter (f: uint64 -> int -> bool) = unbox<JS.TypedArray<uint64>>()
            member _.filter (f: uint64 -> int -> JS.TypedArray<uint64> -> bool) = unbox<JS.TypedArray<uint64>>()
            member _.find (f: uint64 -> bool) = unbox<uint64 option>()
            member _.find (f: uint64 -> int -> bool) = unbox<uint64 option>()
            member _.find (f: uint64 -> int -> JS.TypedArray<uint64> -> bool) = unbox<uint64 option>()
            member _.findIndex (f: uint64 -> bool) = 1
            member _.findIndex (f: uint64 -> int -> bool) = 1
            member _.findIndex (f: uint64 -> int -> JS.TypedArray<uint64> -> bool) = 1
            member _.forEach (f: uint64 -> bool) = ()
            member _.forEach (f: uint64 -> int -> bool) = ()
            member _.forEach (f: uint64 -> int -> JS.TypedArray<uint64> -> bool) = ()
            member _.includes (searchElement, ?fromIndex) = true
            member _.indexOf (searchElement, ?fromIndex) = 1
            member _.lastIndexOf (searchElement, ?fromIndex) = 1
            member _.map (f: uint64 -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint64 -> int -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.map (f: uint64 -> int -> JS.TypedArray<uint64> -> 'U) = unbox<JS.TypedArray<'U>>()
            member _.reduce (f: 'State -> uint64 -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint64 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduce (f: 'State -> uint64 -> int -> JS.TypedArray<uint64> -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint64 -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint64 -> int -> 'State, state: 'State) = unbox<'State>()
            member _.reduceRight (f: 'State -> uint64 -> int -> JS.TypedArray<uint64> -> 'State, state: 'State) = unbox<'State>()
            member _.reverse () = unbox<JS.TypedArray<uint64>>()
            member _.set (source: System.Array, ?offset: int) = ()
            member _.set (source: #JS.TypedArray, ?offset: int) = ()
            member _.slice (?begin', ?end') = unbox<JS.TypedArray<uint64>>()
            member _.some (f: uint64 -> bool) = true
            member _.some (f: uint64 -> int -> bool) = true
            member _.some (f: uint64 -> int -> JS.TypedArray<uint64> -> bool) = true
            member this.sort (?sortFunction) = upcast this
            member _.subarray (?begin', ?end') = unbox<JS.TypedArray<uint64>>()
            member _.values () = box ()

        interface Collections.Generic.IEnumerable<uint64> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<uint64>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    /// Provides access to the browser's debugging console. The specifics of how it works varies 
    /// from browser to browser, but there is a de facto set of features that are typically provided.
    [<Global>]
    type console =
        /// Writes an error message to the console if the assertion is false. 
        /// If the assertion is true, nothing happens.
        [<Emit("console.assert($0...)")>]
        static member assert' (test: bool, [<ParamArray>] optionalParams: obj []) : unit = jsNative
        /// Writes an error message to the console if the assertion is false. 
        /// If the assertion is true, nothing happens.
        [<Emit("console.assert($0...)")>]
        static member assert' (test: bool, message: string, [<ParamArray>] optionalParams: obj []) : unit = jsNative

        /// Clears the console if the environment allows it.
        static member clear () : unit = jsNative

        /// Logs the number of times that this particular call to count() has been called.
        static member count (?countLabel: string) : unit = jsNative

        /// Resets the counter used with console.count()
        ///
        /// If the count label is supplied, countReset() resets the count for that label to 0. 
        /// If omitted, countReset() resets the default counter to 0.
        static member countReset (?countLabel: string) : unit = jsNative
        
        /// Outputs a message to the web console at the "debug" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display debug output.
        static member debug ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console at the "debug" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display debug output.
        static member debug (msg: string, [<ParamArray>] items: obj []) : unit = jsNative

        /// Displays an interactive list of the properties of the specified JavaScript object. 
        ///
        /// The output is presented as a hierarchical listing with disclosure triangles that let you see 
        /// the contents of child objects.
        static member dir (value: obj) : unit = jsNative

        /// Displays an interactive tree of the descendant elements of the specified XML/HTML element. 
        ///
        /// If it is not possible to display as an element the JavaScript Object view is shown instead. 
        /// The output is presented as a hierarchical listing of expandable nodes that let you see the 
        /// contents of child nodes.
        static member dirxml (value: obj) : unit = jsNative
        
        /// Outputs a message to the web console at the "error" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display error output.
        static member error ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console at the "error" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display error output.
        static member error (msg: string, [<ParamArray>] items: obj []) : unit = jsNative

        /// Creates a new inline group in the web console log. This indents following console messages by an 
        /// additional level, until console.groupEnd() is called.
        static member group (?groupTitle: string) : unit = jsNative

        /// Creates a new inline group in the web console log. Unlike console.group(), however, the new group is 
        /// created collapsed. The user will need to use the disclosure button next to it to expand it, revealing 
        /// the entries created in the group.
        ///
        /// Call console.groupEnd() to back out to the parent group.
        static member groupCollapsed (?groupTitle: string) : unit = jsNative

        /// Exits the current inline group in the web console log.
        static member groupEnd () : unit = jsNative
        
        /// Outputs a message to the web console at the "info" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display info output.
        static member info ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console at the "info" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display info output.
        static member info (msg: string, [<ParamArray>] items: obj []) : unit = jsNative

        /// Outputs a message to the web console log.
        ///
        /// Specifically, console.log gives special treatment to DOM elements, whereas console.dir does not. 
        /// This is often useful when trying to see the full representation of the DOM JS object.
        static member log ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console log.
        ///
        /// Specifically, console.log gives special treatment to DOM elements, whereas console.dir does not. 
        /// This is often useful when trying to see the full representation of the DOM JS object.
        static member log (msg: string, [<ParamArray>] items: obj []) : unit = jsNative

        /// Starts recording a performance profile (for example, the Firefox performance tool).
        ///
        /// You can optionally supply an argument to name the profile and this then enables you to stop only 
        /// that profile if multiple profiles are being recorded.
        ///
        /// To stop recording call Console.profileEnd().
        /// 
        /// This is a *Non-standard* API, DO NOT use it in production!
        static member profile (?reportLabel: string) : unit = jsNative
        
        /// The profileEnd method stops recording a profile previously started with Console.profile().
        ///
        /// You can optionally supply an argument to name the profile. Doing so enables you to stop only 
        /// that profile if you have multiple profiles being recorded.
        ///
        /// If passed a profile name, and it matches the name of a profile being recorded, then that profile 
        /// is stopped.
        ///
        /// If passed a profile name and it does not match the name of a profile being recorded, no changes 
        /// will be made.
        ///
        /// If is not passed a profile name, the most recently started profile is stopped.
        /// 
        /// This is a *Non-standard* API, DO NOT use it in production!
        static member profileEnd (?reportLabel: string) : unit = jsNative
        
        /// Takes one mandatory argument data, which must be an array or an object, 
        /// and one additional optional parameter columns.
        ///
        /// It logs data as a table. Each element in the array (or enumerable property 
        /// if data is an object) will be a row in the table.
        ///        
        /// The first column in the table will be labeled (index). If data is an array, 
        /// then its values will be the array indices. If data is an object, then its 
        /// values will be the property names.
        static member table<'T when 'T : not struct> (data: 'T, ?columns: seq<string>) : unit = jsNative

        /// Starts a timer you can use to track how long an operation takes. 
        ///
        /// You give each timer a unique name, and may have up to 10,000 timers running on a given page. 
        /// When you call console.timeEnd() with the same name, the browser will output the time, in 
        /// milliseconds, that elapsed since the timer was started.
        static member time (?timerLabel: string) : unit = jsNative
        
        /// Stops a timer that was previously started by calling console.time().
        ///
        /// If given a name it will stop only that timer and the elapsed time is automatically displayed in 
        /// the web console along with an indicator that the time has ended.
        static member timeEnd (?timerLabel: string) : unit = jsNative

        /// Logs the current value of a timer that was previously started by calling console.time() to the 
        /// web console.
        static member timeLog (?timerLabel: string) : unit = jsNative
        
        /// Adds a single marker to the browser's Performance or Waterfall tool. This lets you correlate a 
        /// point in your code with the other events recorded in the timeline, such as layout and paint events.
        ///
        /// You can optionally supply an argument to label the timestamp, and this label will then be shown 
        /// alongside the marker.
        /// 
        /// This is a *Non-standard* API, DO NOT use it in production!
        static member timeStamp (?timerLabel: string) : unit = jsNative

        /// Outputs a message to the web console at the "trace" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display trace output.
        static member trace ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console at the "trace" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display trace output.
        static member trace (msg: string, [<ParamArray>] items: obj []) : unit = jsNative
        
        /// Outputs a message to the web console at the "warn" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display warn output.
        static member warn ([<ParamArray>] items: obj []) : unit = jsNative
        /// Outputs a message to the web console at the "warn" log level. 
        ///
        /// The message is only displayed to the user if the console is configured to display warn output.
        static member warn (msg: string, [<ParamArray>] items: obj []) : unit = jsNative

    /// Helper to safetly construct regex flags.
    [<Erase>]
    type RegExpFlag [<Emit("''")>] () =
        /// Global match
        ///
        /// Find all matches rather than stopping after the first match.
        member inline this.g = unbox<RegExpFlag>(unbox<string>(this) + "g")
        
        /// Ignore case
        ///
        /// If u flag is also enabled, usees Unicode case folding.
        member inline this.i = unbox<RegExpFlag>(unbox<string>(this) + "i")

        /// Multiline
        ///
        /// Treat beginning and end characters (^ and $) as working over multiple 
        /// lines. 
        ///
        /// In other words, match the beginning or end of each line (delimited by \n 
        /// or \r), not only the very beginning or end of the whole input string.
        member inline this.m = unbox<RegExpFlag>(unbox<string>(this) + "m")
        
        /// Dot All
        ///
        /// Allows . to match newlines.
        member inline this.s = unbox<RegExpFlag>(unbox<string>(this) + "s")
        
        /// Unicode
        ///
        /// Treat pattern as a sequence of Unicode code points.
        member inline this.u = unbox<RegExpFlag>(unbox<string>(this) + "u")
        
        /// Sticky
        ///
        /// Matches only from the index indicated by the LastIndex property of this 
        /// regular expression in the target string. 
        ///
        /// Does not attempt to match from any later indexes.
        member inline this.y = unbox<RegExpFlag>(unbox<string>(this) + "y")

    [<Global>]
    type RegExpReplacer =
        member _.match' : string = jsNative
        member _.captures : string [] = jsNative
        member _.offset : int = jsNative
        member _.string : string = jsNative
        member _.groups : (string * string) [] option = jsNative

    [<Erase>]
    type RegExp private (pattern: string, ?flags: obj) =
        [<Emit("new RegExp($0...)")>]
        new (pattern: string) = RegExp(pattern)
        [<Emit("new RegExp($0...)")>]
        new (pattern: string, flags: string) = RegExp(pattern, flags = flags)
        [<Emit("new RegExp($0...)")>]
        new (pattern: string, flags: RegExpFlag) = RegExp(pattern, flags = flags)

        /// Converts this object to the System.Text.RegularExpressions representation.
        ///
        /// No runtime cost.
        member inline this.AsRegex () = unbox<Regex> this

        /// Indicates whether or not the "s" flag is used with this regular expression.
        [<Emit("$0.dotAll")>]
        member _.DotAll : bool = jsNative

        /// Executes a search for a match in a specified string.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        [<Emit("$0.exec($1)")>]
        member _.Exec (value: string) : seq<string> option = jsNative

        /// Returns a string consisting of the flags used with this regular expression.
        [<Emit("$0.flags")>]
        member _.Flags : string = jsNative
        
        /// Indicates whether or not the "g" flag is used with this regular expression.
        [<Emit("$0.global")>]
        member _.Global : bool = jsNative

        /// Indicates whether or not the "i" flag is used with this regular expression.
        [<Emit("$0.ignoreCase")>]
        member _.IgnoreCase : bool = jsNative
        
        /// A mutable integer property that specifies the index at which to start the next match.
        ///
        /// This property is set only if the regular expression instance used the g flag to 
        /// indicate a global search, or the y flag to indicate a sticky search.
        ///
        /// If LastIndex is greater than the length of the string, test() and exec() fail, then 
        /// LastIndex is set to 0.
        ///
        /// If LastIndex is equal to or less than the length of the string and if the regular 
        /// expression matches the empty string, then the regular expression matches input 
        /// starting from LastIndex.
        ///
        /// If LastIndex is equal to the length of the string and if the regular expression 
        /// does not match the empty string, then the regular expression mismatches input, 
        /// and LastIndex is reset to 0.
        ///
        /// Otherwise, LastIndex is set to the next position following the most recent match.
        member _.LastIndex
            with [<Emit("$0.lastIndex")>] get () = 1
            and [<Emit("$0.lastIndex = $1")>] set (x: int) = ()

        /// Retrieves the matches when matching a string against the regular expression.
        [<Emit("$1.match($0)")>]
        member _.Match (value: string) : seq<string> = jsNative
        
        /// Retrieves all matches when matching a string against the regular expression.
        [<Emit("$1.matchAll($0)")>]
        member _.MatchAll (value: string) : seq<seq<string>> = jsNative

        /// Indicates whether or not the "m" flag is used with this regular expression.
        [<Emit("$0.multiline")>]
        member _.Multiline : bool = jsNative
        
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        [<Emit("$1.replace($0, $2)")>]
        member _.Replace (source: string, newSubString: string) : string = jsNative
        
        [<Emit("$1.replace($0, $2)"); EditorBrowsable(EditorBrowsableState.Never)>]
        member _.ReplaceFun (source: string, replacer: obj -> string) : string = jsNative

        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member inline this.Replace (source: string, replacer: RegExpReplacer -> string) =
            fun _ ->
                match arguments.[arguments.Length - 1] with
                | arg when typeof arg = Types.Object -> 
                    {| match' = arguments.[0]
                       captures = arguments.[arguments.Length - 3]
                       string = arguments.[arguments.Length - 2]
                       captures = arguments.[1 .. arguments.Length - 4]
                       groups = Some (unbox<(string * string) []> (Object.entries(arg))) |}
                | _ ->
                    {| match' = arguments.[0]
                       captures = arguments.[arguments.Length - 3]
                       string = arguments.[arguments.Length - 2]
                       captures = arguments.[1 .. arguments.Length - 4]
                       groups = None |}
                |> unbox<RegExpReplacer>
                |> replacer
            |> fun replacer -> this.ReplaceFun(source, replacer)
        
        /// Returns the number of matches found in a given string.
        [<Emit("$1.search($0)")>]
        member _.Search (source: string) : int = jsNative

        /// Pattern of the regular expression without any forward slashes or flags.
        [<Emit("$0.source")>]
        member _.Source : string = jsNative
        
        /// Indicates whether or not the "y" flag is used with this regular expression.
        [<Emit("$0.sticky")>]
        member _.Sticky : bool = jsNative
        
        /// Executes a search for a match in a specified string and returns if it was 
        /// successful or not.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        [<Emit("$0.test($1)")>]
        member _.Test (value: string) : bool = jsNative

        /// Returns a string representing the regular expression.
        [<Emit("$0.toString()")>]
        override _.ToString () : string = jsNative

        /// Indicates whether or not the "u" flag is used with this regular expression.
        [<Emit("$0.unicode")>]
        member _.Unicode : bool = jsNative

    [<Erase;RequireQualifiedAccess>]
    module RegExp =
        /// Creates an empty RegExpFlag
        [<Emit("''")>]
        let flag = unbox<RegExpFlag> ""

        /// Converts a JS regular expression to the System.Text.RegularExpressions representation.
        ///
        /// No runtime cost.
        let inline asRegex (re: RegExp) = re.AsRegex

        /// Indicates whether or not the "s" flag is used with a regular expression.
        let inline dotAll (re: RegExp) = re.DotAll

        /// Executes a search for a match in a specified string.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        let inline exec (value: string) (re: RegExp) = re.Exec(value)

        /// Returns a string consisting of the flags used with this regular expression.
        let inline flags (re: RegExp) = re.Flags
        
        /// Converts this object to the System.Text.RegularExpressions representation.
        ///
        /// No runtime cost.
        let inline fromRegex (regex: Regex) = unbox<RegExp>(regex)

        /// Indicates whether or not the "g" flag is used with a regular expression.
        let inline global' (re: RegExp) = re.Global

        /// Indicates whether or not the "i" flag is used with a regular expression.
        let inline ignoreCase (re: RegExp) = re.IgnoreCase

        /// A mutable integer property that specifies the index at which to start the next match.
        ///
        /// This property is set only if the regular expression instance used the g flag to 
        /// indicate a global search, or the y flag to indicate a sticky search.
        ///
        /// If LastIndex is greater than the length of the string, test() and exec() fail, then 
        /// LastIndex is set to 0.
        ///
        /// If LastIndex is equal to or less than the length of the string and if the regular expression 
        /// matches the empty string, then the regular expression matches input starting from LastIndex.
        ///
        /// If LastIndex is equal to the length of the string and if the regular expression does not match 
        /// the empty string, then the regular expression mismatches input, and LastIndex is reset to 0.
        ///
        /// Otherwise, LastIndex is set to the next position following the most recent match.
        let inline lastIndex (re: RegExp) = re.LastIndex

        /// A mutable integer property that specifies the index at which to start the next match.
        ///
        /// This property is set only if the regular expression instance used the g flag to 
        /// indicate a global search, or the y flag to indicate a sticky search.
        ///
        /// If LastIndex is greater than the length of the string, test() and exec() fail, then 
        /// LastIndex is set to 0.
        ///
        /// If LastIndex is equal to or less than the length of the string and if the regular expression 
        /// matches the empty string, then the regular expression matches input starting from LastIndex.
        ///
        /// If LastIndex is equal to the length of the string and if the regular expression does not match 
        /// the empty string, then the regular expression mismatches input, and LastIndex is reset to 0.
        ///
        /// Otherwise, LastIndex is set to the next position following the most recent match.
        let inline setLastIndex (value: int) (re: RegExp) = 
            re.LastIndex <- value
            re

        /// Retrieves the matches when matching a string against a regular expression.
        let inline match' (value: string) (re: RegExp) = re.Match(value)

        /// Retrieves all matches when matching a string against a regular expression.
        let inline matchAll (value: string) (re: RegExp) = re.MatchAll(value)

        /// Indicates whether or not the "m" flag is used with a regular expression.
        let inline multiline (re: RegExp) = re.Multiline

        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        let inline replace (source: string) (newSubString: string) (re: RegExp) = re.Replace(source, newSubString)

        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        let inline replaceFun (source: string) (replacer: RegExpReplacer -> string) (re: RegExp) = re.Replace(source, replacer)

        /// Returns the number of matches found in a given string.
        let inline search (source: string) (re: RegExp) = re.Search(source)

        /// Pattern of the regular expression without any forward slashes or flags.
        let inline source (re: RegExp) = re.Source

        /// Indicates whether or not the "y" flag is used with a regular expression.
        let inline sticky (re: RegExp) = re.Sticky

        /// Executes a search for a match in a specified string and returns if it was successful or not.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        let inline test (value: string) (re: RegExp) = re.Test(value)

        /// Returns a string representing the regular expression.
        let inline toString (re: RegExp) = re.ToString()

        /// Indicates whether or not the "u" flag is used with a regular expression.
        let inline unicode (re: RegExp) = re.Unicode

[<AutoOpen;Erase;EditorBrowsable(EditorBrowsableState.Never)>]
module JSExtensions =
    type JSe.Object with
        /// Casts an object to a type.
        static member inline as'<'T> (o: obj) = unbox<'T> o

        /// Returns a list of all properties (including non-enumerable properties except for 
        /// those which use Symbol) found directly in a given object.
        #if FABLE_COMPILER
        static member inline getOwnPropertyNames (o: obj) = 
        #else
        static member getOwnPropertyNames (o: obj) = 
        #endif
            JSe.Object.getOwnPropertyNames(o) |> List.ofSeq

        /// Returns a list of a given object's own enumerable property names, 
        /// iterated in the same order that a normal loop would.
        #if FABLE_COMPILER
        static member inline keys (o: obj) =
        #else
        static member keys (o: obj) =
        #endif
            JSe.Object.keys(o) |> List.ofSeq

    type JSe.Promise<'T> with
        /// Returns a Promise object that is resolved with a given value. 
        ///
        /// If the value is a promise, that promise is returned; if the value is a thenable (i.e. has a "then" 
        /// method), the returned promise will "follow" that thenable, adopting its eventual state; otherwise the 
        /// returned promise will be fulfilled with the value. This function flattens nested layers of promise-like 
        /// objects (e.g. a promise that resolves to a promise that resolves to something) into a single layer.
        [<Emit("Promise.resolve($0)")>]
        static member resolve (value: 'T) : JS.Promise<'T> = jsNative
    
    type JSe.TypedArray<'T> with
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

    type System.String with
        /// Retrieves the matches when matching a string against a regular expression.
        member inline this.Match (regExp: JSe.RegExp) = regExp.Match(this)
        /// Retrieves the matches when matching a string against a regular expression.
        member inline this.Match (regex: Regex) = JSe.RegExp.fromRegex(regex).Match(this)

        /// Retrieves all matches when matching a string against a regular expression.
        member inline this.MatchAll (regExp: JSe.RegExp) = regExp.MatchAll(this)
        /// Retrieves all matches when matching a string against a regular expression.
        member inline this.MatchAll (regex: Regex) = JSe.RegExp.fromRegex(regex).MatchAll(this)
        
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member inline this.Replace (regExp: JSe.RegExp, newSubString: string) = regExp.Replace(this, newSubString)
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member inline this.Replace (regex: Regex, newSubString: string) = JSe.RegExp.fromRegex(regex).Replace(this, newSubString)
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member inline this.Replace (regExp: JSe.RegExp, replacer: JSe.RegExpReplacer -> string) = regExp.Replace(this, replacer)
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member inline this.Replace (regex: Regex, replacer: JSe.RegExpReplacer -> string) = JSe.RegExp.fromRegex(regex).Replace(this, replacer)
        
        /// The index of the first match between the regular expression and the given string, 
        /// or -1 if no match was found.
        member inline this.Search (regExp: JSe.RegExp) = regExp.Search(this)
        /// The index of the first match between the regular expression and the given string, 
        /// or -1 if no match was found.
        member inline this.Search (regex: Regex) = JSe.RegExp.fromRegex(regex).Search(this)

        /// Splits a string into substrings based on regular expression matches.
        [<Emit("$0.split($1)")>]
        member _.Split (regExp: JSe.RegExp) : string [] = jsNative
        /// Splits a string into substrings based on regular expression matches.
        member inline this.Split (regex: Regex) = this.Split(JSe.RegExp.fromRegex regex)

[<Erase>]
module Operators =
    let inline (?|) l r = JSe.or' l r

[<AutoOpen>]
module NonErasedExtensions =
    open Fable.Core.JsInterop

    type JSe.is with
        /// Normal structural F# comparison, but ignores top-level functions (e.g. Elmish dispatch).
        static member equalsButFunctions (x: 'a) (y: 'a) =
            if obj.ReferenceEquals(x, y) then
                true
            elif JSe.is.nonEnumerableObject x && not(isNull(box y)) then
                let keys = JS.Constructors.Object.keys x
                let length = keys.Count
                let mutable i = 0
                let mutable result = true
                while i < length && result do
                    let key = keys.[i]
                    i <- i + 1
                    let xValue = x?(key)
                    result <- JSe.is.function' xValue || xValue = y?(key)
                result
            else
                (box x) = (box y)

        /// Performs a memberwise comparison where value types and strings are compared by value,
        /// and other types by reference.
        static member equalsWithReferences (x: 'a) (y: 'a) =
            if obj.ReferenceEquals(x, y) then
                true
            elif JSe.is.nonEnumerableObject x && not(isNull(box y)) then
                let keys = JS.Constructors.Object.keys x
                let length = keys.Count
                let mutable i = 0
                let mutable result = true
                while i < length && result do
                    let key = keys.[i]
                    i <- i + 1
                    let xValue = x?(key)
                    result <- JSe.is.function' xValue || obj.ReferenceEquals(xValue, y?(key))
                result
            else
                false
