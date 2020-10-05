namespace Fable.Extras.Reflection

open Fable.Core
open Fable.Extras
open FSharp.Core
open System.ComponentModel

[<EditorBrowsable(EditorBrowsableState.Never)>]
module Internal =
    open Fable.Core.JsInterop
    
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    let dynamicProxy<'T,'V when 'T : not struct> (target: 'T) (ph: obj) (targetFullName: string) (executor: obj -> obj -> obj) : 'V =
        match JSe.typeof target with
        | JSe.Types.Function ->
            match targetFullName.Split([|','|]).Length - 1 with
            | 1 -> executor target ph
            | 2 ->
                let execFunc : System.Func<obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj> (unbox target)) ph
                unbox (fun a b -> execFunc.Invoke(a, b))
            | 3 ->
                let execFunc : System.Func<obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c -> execFunc.Invoke(a, b, c))
            | 4 ->
                let execFunc : System.Func<obj,obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c d -> execFunc.Invoke(a, b, c, d))
            | 5 ->
                let execFunc : System.Func<obj,obj,obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c d e -> execFunc.Invoke(a, b, c, d, e))
            | 6 ->
                let execFunc : System.Func<obj,obj,obj,obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c d e f -> execFunc.Invoke(a, b, c, d, e, f))
            | 7 ->
                let execFunc : System.Func<obj,obj,obj,obj,obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c d e f g -> execFunc.Invoke(a, b, c, d, e, f, g))
            | 8 ->
                let execFunc : System.Func<obj,obj,obj,obj,obj,obj,obj,obj,obj> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                unbox (fun a b c d e f g h -> execFunc.Invoke(a, b, c, d, e, f, g, h))
            | _ -> failwith "Currying to more than 8-arity is not supported for proxies."
        | _ -> executor target ph
        |> unbox<'V>
        
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    [<Global>]
    type RevocableProxy<'T> =
        member _.proxy
            with get () : 'T = jsNative
            and set (x: 'T) : unit = jsNative

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    let dynamicRevocableProxy<'T,'V when 'T : not struct> (target: 'T) (ph: obj) (targetFullName: string) (executor: obj -> obj -> obj) : 'V =
        match JSe.typeof target with
        | JSe.Types.Function ->
            match targetFullName.Split([|','|]).Length - 1 with
            | 1 -> executor target ph
            | 2 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b -> originalProxy.Invoke(a, b))
                unbox revocable
            | 3 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c -> originalProxy.Invoke(a, b, c))
                unbox revocable
            | 4 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c d -> originalProxy.Invoke(a, b, c, d))
                unbox revocable
            | 5 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c d e -> originalProxy.Invoke(a, b, c, d, e))
                unbox revocable
            | 6 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c d e f -> originalProxy.Invoke(a, b, c, d, e, f))
                unbox revocable
            | 7 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj,obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c d e f g -> originalProxy.Invoke(a, b, c, d, e, f, g))
                unbox revocable
            | 8 ->
                let revocable : RevocableProxy<System.Func<obj,obj,obj,obj,obj,obj,obj,obj,obj>> = unbox <| executor (System.Func<obj,obj,obj,obj,obj,obj,obj,obj,obj> (unbox target)) ph
                
                let originalProxy = revocable.proxy

                revocable.proxy <- unbox (fun a b c d e f g h -> originalProxy.Invoke(a, b, c, d, e, f, g, h))
                unbox revocable
            | _ -> failwith "Currying to more than 8-arity is not supported for proxies."
        | _ -> executor target ph
        |> unbox<'V>

[<Erase;RequireQualifiedAccess>]
module JSe =
    open Internal

    type Proxy<'T> = 'T

    /// A proxy that can be disabled.
    [<Global>]
    type RevocableProxy<'T> =
        /// The proxied item.
        member _.proxy : Proxy<'T> = jsNative

        /// Invalidates the proxy.
        ///
        /// Any future calls to the proxy will throw an exception after this
        /// has been called.
        member _.revoke () : unit = jsNative

    /// Defines the custom behavior of the proxy.
    [<Global>]
    type ProxyHandler<'T> [<Emit("Object.create(null)")>] () =
        /// A trap for a function call.
        ///
        /// The arguments are the target, the `this` keyword scoped to the fuction call, and the function arguments.        
        member _.Apply
            with [<Emit("$0.apply")>] get () : ('T -> obj -> obj [] -> obj) option = jsNative
            and [<Emit("$0.apply = $1")>] set (f: ('T -> obj -> obj [] -> obj) option) = jsNative

        /// A trap for the new operator.
        ///
        /// The arguments are the target, and the constructor arguments.
        member _.Construct
            with [<Emit("$0.construct")>] get () : ('T -> obj [] -> obj) option = jsNative
            and [<Emit("$0.construct = $1")>] set (f: ('T -> obj [] -> obj) option) = jsNative

        /// A trap for Object.defineProperty.
        ///
        /// The arguments are the target, property name, and property descriptor.
        member _.DefineProperty
            with [<Emit("$0.defineProperty")>] get () : ('T -> string -> JS.PropertyDescriptor -> bool) option = jsNative
            and [<Emit("$0.defineProperty = $1")>] set (f: ('T -> string -> JS.PropertyDescriptor -> bool) option) = jsNative

        /// A trap for the delete operator.
        ///
        /// The arguments are the target and property name.
        member _.DeleteProperty
            with [<Emit("$0.deleteProperty")>] get () : ('T -> string -> bool) option = jsNative
            and [<Emit("$0.deleteProperty = $1")>] set (f: ('T -> string -> bool) option) = jsNative

        /// A trap for getting property values.
        ///
        /// The arguments are the target, property name, and proxy.
        member _.Get
            with [<Emit("$0.get")>] get () : ('T -> string -> Proxy<'T> -> obj) option  = jsNative
            and [<Emit("$0.get = $1")>] set (f: ('T -> string -> Proxy<'T> -> obj) option) = jsNative

        /// A trap for Object.getOwnPropertyDescriptor.
        ///
        /// The arguments are the target and property name.
        member _.GetOwnPropertyDescriptor
            with [<Emit("$0.getOwnPropertyDescriptor")>] get () : ('T -> string -> JS.PropertyDescriptor option) option = jsNative
            and [<Emit("$0.getOwnPropertyDescriptor = $1")>] set (f: ('T -> string -> JS.PropertyDescriptor option) option) = jsNative

        /// A trap for Object.getPrototypeOf.
        ///
        /// The argument is the target, the function must return either the prototype or null.
        member _.GetPrototypeOf
            with [<Emit("$0.getPrototypeOf")>] get () : ('T -> obj) option = jsNative
            and [<Emit("$0.getPrototypeOf = $1")>] set (f: ('T -> obj) option) = jsNative

        /// A trap for the in operator.
        ///
        /// The arguments are the target and property name.
        member _.Has
            with [<Emit("$0.has")>] get () : ('T -> string -> bool) option = jsNative
            and [<Emit("$0.has = $1")>] set (f: ('T -> string -> bool) option) = jsNative

        /// A trap for Object.isExtensible.
        ///
        /// The argument is the target.
        member _.IsExtensible
            with [<Emit("$0.isExtensible")>] get () : ('T -> bool) option = jsNative
            and [<Emit("$0.isExtensible = $1")>] set (f: ('T -> bool) option) = jsNative
    
        /// A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.
        ///
        /// The argument is the target.
        member _.OwnKeys
            with [<Emit("$0.ownKeys")>] get () : ('T -> seq<_>) option = jsNative
            and [<Emit("$0.ownKeys = $1")>] set (f: ('T -> seq<_>) option) = jsNative

        /// A trap for Object.preventExtensions.
        ///
        /// The argument is the target.
        member _.PreventExtensions
            with [<Emit("$0.preventExtensions")>] get () : ('T -> bool) option = jsNative
            and [<Emit("$0.preventExtensions = $1")>] set (f: ('T -> bool) option) = jsNative
        
        /// A trap for setting property values.
        ///
        /// The arguments are the target, property name, value of the property, and 
        /// receiver (the object the assignment was originally directed, this is usually 
        /// the proxy itself).
        member _.Set
            with [<Emit("$0.set")>] get () : ('T -> string -> obj -> obj -> bool) option = jsNative
            and [<Emit("$0.set = $1")>] set (f: ('T -> string -> obj -> obj -> bool) option) = jsNative

        /// A trap for Object.setPrototypeOf.
        ///
        /// The arguments are the target and the prototype.
        member _.SetPrototypeOf
            with [<Emit("$0.setPrototypeOf")>] get () : ('T -> obj option -> bool) option = jsNative
            and [<Emit("$0.setPrototypeOf = $1")>] set (f: ('T -> obj option -> bool) option) = jsNative
            
    /// Defines the custom behavior of the proxy.
    [<Erase;RequireQualifiedAccess>]
    module ProxyHandler =
        /// A trap for a function call.
        ///
        /// The arguments are the target, the `this` keyword scoped to the fuction call, and the function arguments.    
        let inline apply (ph: ProxyHandler<'T>) = ph.Apply
        
        /// A trap for the new operator.
        ///
        /// The arguments are the target, and the constructor arguments.
        let inline construct (ph: ProxyHandler<'T>) = ph.Construct

        /// A trap for Object.defineProperty.
        ///
        /// The arguments are the target, property name, and property descriptor.
        let inline defineProperty (ph: ProxyHandler<'T>) = ph.DefineProperty
        
        /// A trap for the delete operator.
        ///
        /// The arguments are the target and property name.
        let inline deleteProperty (ph: ProxyHandler<'T>) = ph.DeleteProperty

        /// A trap for getting property values.
        ///
        /// The arguments are the target, property name, and proxy.
        let inline get (ph: ProxyHandler<'T>) = ph.Get
        
        /// A trap for Object.getOwnPropertyDescriptor.
        ///
        /// The arguments are the target and property name.
        let inline getOwnPropertyDescriptor (ph: ProxyHandler<'T>) = ph.GetOwnPropertyDescriptor

        /// A trap for Object.getPrototypeOf.
        ///
        /// The argument is the target, the function must return either the prototype or null.
        let inline getPrototypeOf (ph: ProxyHandler<'T>) = ph.GetPrototypeOf

        /// A trap for the in operator.
        ///
        /// The arguments are the target and property name.
        let inline has (ph: ProxyHandler<'T>) = ph.Has

        /// A trap for Object.isExtensible.
        ///
        /// The argument is the target.
        let inline isExtensible (ph: ProxyHandler<'T>) = ph.IsExtensible

        /// A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.
        ///
        /// The argument is the target.
        let inline ownKeys (ph: ProxyHandler<'T>) = ph.OwnKeys

        /// A trap for Object.preventExtensions.
        ///
        /// The argument is the target.
        let inline preventExtensions (ph: ProxyHandler<'T>) = ph.PreventExtensions
            
        /// A trap for setting property values.
        ///
        /// The arguments are the target, property name, value of the property, and 
        /// receiver (the object the assignment was originally directed, this is usually 
        /// the proxy itself).
        let inline set (ph: ProxyHandler<'T>) = ph.Set

        /// A trap for Object.setPrototypeOf.
        ///
        /// The arguments are the target and the prototype.
        let inline setPrototypeOf (ph: ProxyHandler<'T>) = ph.SetPrototypeOf

        /// A trap for a function call.
        ///
        /// The arguments are the target, the `this` keyword scoped to the fuction call, and the function arguments.   
        let inline setApply f (ph: ProxyHandler<'T>) = 
            ph.Apply <- Some (fun pFun this' args -> f pFun this' args |> box)
            ph
        
        /// A trap for the new operator.
        ///
        /// The arguments are the target, and the constructor arguments.
        let inline setConstruct f (ph: ProxyHandler<'T>) = 
            ph.Construct <- Some (fun pFun args -> f pFun args |> box)
            ph

        /// A trap for Object.defineProperty.
        ///
        /// The arguments are the target, property name, and property descriptor.
        let inline setDefineProperty f (ph: ProxyHandler<'T>) = 
            ph.DefineProperty <- Some f
            ph
        
        /// A trap for the delete operator.
        ///
        /// The arguments are the target and property name.
        let inline setDeleteProperty f (ph: ProxyHandler<'T>) = 
            ph.DeleteProperty <- Some f
            ph

        /// A trap for getting property values.
        ///
        /// The arguments are the target, property name, and proxy.
        let inline setGet f (ph: ProxyHandler<'T>) = 
            ph.Get <- Some (fun pFun this' proxy -> f pFun this' proxy |> box)
            ph
        
        /// A trap for Object.getOwnPropertyDescriptor.
        ///
        /// The arguments are the target and property name.
        let inline setGetOwnPropertyDescriptor f (ph: ProxyHandler<'T>) = 
            ph.GetOwnPropertyDescriptor <- Some f
            ph

        /// A trap for Object.getPrototypeOf.
        ///
        /// The argument is the target, the function must return either the prototype or null.
        let inline setGetPrototypeOf f (ph: ProxyHandler<'T>) = 
            ph.GetPrototypeOf <- Some f
            ph

        /// A trap for the in operator.
        ///
        /// The arguments are the target and property name.
        let inline setHas f (ph: ProxyHandler<'T>) = 
            ph.Has <- Some f
            ph

        /// A trap for Object.isExtensible.
        ///
        /// The argument is the target.
        let inline setIsExtensible f (ph: ProxyHandler<'T>) = 
            ph.IsExtensible <- Some f
            ph

        /// A trap for Object.getOwnPropertyNames and Object.getOwnPropertySymbols.
        ///
        /// The argument is the target.
        let inline setOwnKeys f (ph: ProxyHandler<'T>) = 
            ph.OwnKeys <- Some f
            ph

        /// A trap for Object.preventExtensions.
        ///
        /// The argument is the target.
        let inline setPreventExtensions f (ph: ProxyHandler<'T>) = 
            ph.PreventExtensions <- Some f
            ph
            
        /// A trap for setting property values.
        ///
        /// The arguments are the target, property name, value of the property, and 
        /// receiver (the object the assignment was originally directed, this is usually 
        /// the proxy itself).
        let inline setSet f (ph: ProxyHandler<'T>) = 
            ph.Set <- Some f
            ph

        /// A trap for Object.setPrototypeOf.
        ///
        /// The arguments are the target and the prototype.
        let inline setSetPrototypeOf f (ph: ProxyHandler<'T>) = 
            ph.SetPrototypeOf <- Some f
            ph

    [<Erase>]
    type Proxy =
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        [<Emit("new Proxy($0, $1)")>]
        static member createInternal<'T when 'T : not struct> (target: 'T) (ph: ProxyHandler<'T>) : Proxy<'T> = target
        
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        [<Emit("Proxy.revocable($0, $1)")>]
        static member createRevocableInternal<'T when 'T : not struct> (target: 'T) (ph: ProxyHandler<'T>) : RevocableProxy<'T> = jsNative
        
        /// Creates a proxy from the specified handler.
        static member inline create<'T when 'T : not struct> (target: 'T) (ph: ProxyHandler<'T>) : Proxy<'T> =
            dynamicProxy<'T,Proxy<'T>> target ph (target.GetType().FullName) (unbox Proxy.createInternal)

        /// Creates a proxy from the specified handler that can be disabled.
        static member inline createRevocable<'T when 'T : not struct> (target: 'T) (ph: ProxyHandler<'T>) : RevocableProxy<'T> =
            dynamicRevocableProxy<'T,RevocableProxy<'T>> target ph (target.GetType().FullName) (unbox Proxy.createRevocableInternal)

    [<Global>]
    type Reflect =
        /// Calls a target function with arguments as specified by the argumentsList parameter.
        static member apply (target: 'a -> 'b) (thisArg: obj) (arguments: obj []) : 'b = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply2 (target: 'a -> 'b -> 'c) (thisArg: obj) (arguments: obj []) : 'c  = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply3 (target: 'a -> 'b -> 'c -> 'd) (thisArg: obj) (arguments: obj []) : 'd = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply4 (target: 'a -> 'b -> 'c -> 'd -> 'e) (thisArg: obj) (arguments: obj []) : 'e = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply5 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f) (thisArg: obj) (arguments: obj []) : 'f = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply6 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g) (thisArg: obj) (arguments: obj []) : 'g = jsNative

        /// Calls a target function with arguments as specified by the argumentsList parameter.
        [<Emit("Reflect.apply($0, $1, $2)")>]
        static member apply7 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g -> 'h) (thisArg: obj) (arguments: obj []) : 'h = jsNative

        /// Acts like the new operator, but as a function.
        static member construct (target: 'a -> 'b) (arguments: obj []) : 'b = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct2 (target: 'a -> 'b -> 'c) (arguments: obj []) : 'c  = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct3 (target: 'a -> 'b -> 'c -> 'd) (arguments: obj []) : 'd = jsNative

        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct4 (target: 'a -> 'b -> 'c -> 'd -> 'e) (arguments: obj []) : 'e = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct5 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f) (arguments: obj []) : 'f = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct6 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g) (arguments: obj []) : 'g = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1)")>]
        static member construct7 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g -> 'h) (arguments: obj []) : 'h = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0,$1,$2)")>]
        static member constructNewTarget (target: 'a -> 'b) (arguments: obj []) (newTarget: 'a -> 'b) : 'b = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget2 (target: 'a -> 'b -> 'c) (arguments: obj []) (newTarget: 'a -> 'b -> 'c) : 'c  = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget3 (target: 'a -> 'b -> 'c -> 'd) (arguments: obj []) (newTarget: 'a -> 'b -> 'c -> 'd) : 'd = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget4 (target: 'a -> 'b -> 'c -> 'd -> 'e) (arguments: obj []) (newTarget: 'a -> 'b -> 'c -> 'd -> 'e) : 'e = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget5 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f) (arguments: obj []) (newTarget: 'a -> 'b -> 'c -> 'd -> 'e -> 'f) : 'f = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget6 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g) (arguments: obj []) (newTarget: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g) : 'g = jsNative
        
        /// Acts like the new operator, but as a function.
        [<Emit("Reflect.construct($0, $1, $2)")>]
        static member constructNewTarget7 (target: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g -> 'h) (arguments: obj []) (newTarget: 'a -> 'b -> 'c -> 'd -> 'e -> 'f -> 'g -> 'h) : 'h = jsNative

        /// Defines a new property directly on an object, or modifies an existing 
        /// property on an object, and returns the object.
        static member defineProperty<'T, 'U when 'T : not struct and 'U :> JS.PropertyDescriptor> (target: 'T) (propertyName: string) (propertyDescriptor: 'U) : bool = jsNative

        /// The delete operator as a function. Equivalent to calling delete target[propertyKey].
        static member deleteProperty<'T, 'U when 'T : not struct and 'U :> JS.PropertyDescriptor> (target: 'T) (propertyName: string) (propertyDescriptor: 'U) : bool = jsNative

        /// Returns the value of the property. Works like getting a property from an object (target[propertyKey]) as a function.
        static member get<'T,'U when 'T : not struct> (target: 'T) (propertyName: string) : 'U = jsNative

        /// Returns the value of the property. Works like getting a property from an object (target[propertyKey]) as a function.
        [<Emit("Reflect.get($0, $1, $2)")>]
        static member getWithReceiver<'T,'U when 'T : not struct> (target: 'T) (propertyName: string) (receiver: obj) : 'U = jsNative

        /// Returns a property descriptor of the given property if it exists on the object or None.
        static member getOwnPropertyDescriptor<'T,'U when 'T : not struct> (target: 'T) (propertyName: string) : JSe.PropertyDescriptor<'U> option = jsNative

        /// Returns the prototype (i.e. the value of the internal [[Prototype]] property) of the specified object.
        static member getPrototypeOf<'T,'U when 'T : not struct and 'U : not struct> (target: 'T) : 'U = jsNative

        /// Returns a Boolean indicating whether the target has the property. Either as own or inherited. 
        ///
        /// Works like the in operator as a function.
        static member has<'T when 'T : not struct> (target: 'T) (propertyName: string) : bool = jsNative

        /// Returns a Boolean that is true if the target is extensible.
        static member isExtensible<'T when 'T : not struct> (target: 'T) : bool = jsNative
    
        /// Returns an array of the target object's own (not inherited) property keys.
        static member ownKeys<'T when 'T : not struct> (target: 'T) : string [] = jsNative

        /// Prevents new properties from ever being added to an object (i.e. prevents future extensions to the object).
        static member preventExtensions<'T when 'T : not struct> (target: 'T) : bool = jsNative
        
        /// Assigns values to properties. Returns a Boolean that is true if the update was successful.
        static member set<'T,'U when 'T : not struct> (target: 'T) (propertyName: string) (value: 'U) : bool = jsNative

        /// Assigns values to properties. Returns a Boolean that is true if the update was successful.
        [<Emit("Reflect.set($0, $1, $2, $3)")>]
        static member setWithReceiver<'T,'U when 'T : not struct> (target: 'T) (propertyName: string) (value: 'U) (receiver: obj) : bool = jsNative

        /// It sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to another object, 
        /// and returns true if the operation was successful, or false otherwise.
        static member setPrototypeOf<'T,'U when 'T : not struct and 'U : not struct> (target: 'T) (prototype: 'U) : bool = jsNative
        
        /// It sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to null, 
        /// and returns true if the operation was successful, or false otherwise.
        [<Emit("Reflect.setPrototypeOf($0, null)")>]
        static member setPrototypeOfNull<'T when 'T : not struct> (target: 'T) : bool = jsNative
