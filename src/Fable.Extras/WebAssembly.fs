namespace Fable.Extras.WebAssembly

open Fable.Core
open Fable.Extras
open FSharp.Core
open System.ComponentModel

[<Global>]
type WA =
    /// The size of a WebAssembly page in bytes.
    static member inline bytesPerPage = 65536

[<RequireQualifiedAccess>]
module WA =
    /// Indicates an error during WebAssembly decoding or validation.
    [<Global>]
    type CompileError [<Emit("new WebAssembly.CompileError($0...)")>] (?message: string, ?fileName: string, ?lineNumber: int) =
        inherit exn()

        member _.columnNumber : int = jsNative
        member _.fileName : string = jsNative
        member _.lineNumber : int = jsNative
        member _.message : string = jsNative
        member _.name : string = jsNative
        member _.stack : string = jsNative
    
    /// Indicates an error during module instantiation (besides traps from the start function).
    [<Global>]
    type LinkError [<Emit("new WebAssembly.LinkError($0...)")>] (?message: string, ?fileName: string, ?lineNumber: int) =
        inherit exn()

        member _.columnNumber : int = jsNative
        member _.fileName : string = jsNative
        member _.lineNumber : int = jsNative
        member _.message : string = jsNative
        member _.name : string = jsNative
        member _.stack : string = jsNative

    /// The type that is thrown whenever WebAssembly specifies a trap.
    [<Global>]
    type RuntimeError [<Emit("new WebAssembly.RuntimeError($0...)")>] (?message: string, ?fileName: string, ?lineNumber: int) =
        inherit exn()

        member _.columnNumber : int = jsNative
        member _.fileName : string = jsNative
        member _.lineNumber : int = jsNative
        member _.message : string = jsNative
        member _.name : string = jsNative
        member _.stack : string = jsNative

    [<StringEnum;RequireQualifiedAccess>]
    type DescriptorValue =
        | I32
        | I64
        | F32
        | F64

    /// A configuration object for WebAssembly Global instantiation.
    [<Global>]
    type GlobalDescriptor private () =
        [<Emit("Object.defineProperty(Object.create(null), 'value', {value: $0})")>]
        new (value: DescriptorValue) = GlobalDescriptor()

        [<Emit("Object.defineProperty(Object.defineProperty(Object.create(null), 'value', {value: $0}), 'mutable', {value: $1})")>]
        new (value: DescriptorValue, mutable': bool) = GlobalDescriptor()

        /// The data type of the global.
        [<Emit("$0.value")>]
        member _.Value : DescriptorValue = jsNative

        /// A boolean value that determines whether the global is mutable or not. 
        /// By default, this is false.
        [<Emit("$0.mutable")>]
        member _.Mutable : bool option = jsNative
            
    /// A configuration object for WebAssembly Global instantiation.
    [<Erase;RequireQualifiedAccess>]
    module GlobalDescriptor =
        /// The data type of the global.
        let inline value (gd: GlobalDescriptor) = gd.Value

        /// A boolean value that determines whether the global is mutable or not. 
        /// By default, this is false.
        let inline mutable' (gd: GlobalDescriptor) = gd.Mutable
        
    /// Creates a new Global object representing a global variable instance, accessible from both JavaScript and 
    /// importable/exportable across one or more WebAssembly.Module instances. 
    ///
    /// This allows dynamic linking of multiple modules.
    [<Global>]
    type Global<'T> (descriptor: GlobalDescriptor, value: 'T) =
        member _.value
            with [<Emit("$0.value")>] get () : 'T = jsNative
            and [<Emit("$0.value = $1")>] set (x: 'T) = jsNative

        member _.valueOf () : 'T = jsNative

    [<Erase;RequireQualifiedAccess>]
    module Global =
        let inline value (global': Global<'T>) = global'.value

        let inline setValue (value: 'T) (global': Global<'T>) = 
            global'.value <- value
            value

        let inline valueOf (global': Global<'T>) = global'.valueOf()

    /// Units of WebAssembly pages when configurating WebAssembly Memory objects.
    [<Measure>]
    type WasmPages
    
    [<Erase;RequireQualifiedAccess>]
    module WasmPages =
        let inline asBytes (value: int<WasmPages>) = value / 1<WasmPages> |> (*) WA.bytesPerPage

    /// A configuration object for WebAssembly Memory instantiation.
    [<Global>]
    type MemoryDescriptor private () =
        [<Emit("Object.defineProperty(Object.create(null), 'initial', {value: $0})")>]
        new (initial: int<WasmPages>) = MemoryDescriptor()

        [<Emit("Object.defineProperty(Object.defineProperty(Object.create(null), 'initial', {value: $0}), 'maximum', {value: $1})")>]
        new (initial: int<WasmPages>, maximum: int<WasmPages>) = MemoryDescriptor()
        
        [<Emit("Object.defineProperty(Object.defineProperty(Object.defineProperty(Object.create(null), 'initial', {value: $0}), 'maximum', {value: $1}), 'shared', {value: $2})")>]
        new (initial: int<WasmPages>, maximum: int<WasmPages>, shared: bool) = MemoryDescriptor()

        [<Emit("Object.defineProperty(Object.defineProperty(Object.create(null), 'initial', {value: $0}), 'shared', {value: $1})")>]
        new (initial: int<WasmPages>, shared: bool) = MemoryDescriptor()

        /// The initial size of the WebAssembly Memory, in units of WebAssembly pages.
        [<Emit("$0.initial")>]
        member _.Initial : int<WasmPages> = jsNative

        /// The maximum size the WebAssembly Memory is allowed to grow to, in units of WebAssembly pages.
        ///
        /// When present, the maximum parameter acts as a hint to the engine to reserve memory up front. 
        /// However, the engine may ignore or clamp this reservation request. Unshared WebAssembly 
        /// memories don't need to set a maximum, but shared memories do.
        [<Emit("$0.maximum")>]
        member _.Maximum : int<WasmPages> option = jsNative

        /// A boolean value that defines whether the memory is a shared memory or not. 
        ///
        /// If set to true, it is a shared memory. 
        ///
        /// The default is false.
        [<Emit("$0.shared")>]
        member _.Shared : bool option = jsNative
    
    /// A configuration object for WebAssembly Memory instantiation.
    [<Erase;RequireQualifiedAccess>]
    module MemoryDescriptor =
        /// The initial size of the WebAssembly Memory, in units of WebAssembly pages.
        let inline initial (md: MemoryDescriptor) = md.Initial

        /// The maximum size the WebAssembly Memory is allowed to grow to, in units of WebAssembly pages.
        ///
        /// When present, the maximum parameter acts as a hint to the engine to reserve memory up front. 
        /// However, the engine may ignore or clamp this reservation request. Unshared WebAssembly 
        /// memories don't need to set a maximum, but shared memories do.
        let inline maximum (md: MemoryDescriptor) = md.Maximum
        
        /// A boolean value that defines whether the memory is a shared memory or not. 
        ///
        /// If set to true, it is a shared memory. 
        ///
        /// The default is false.
        let inline shared (md: MemoryDescriptor) = md.Shared

    /// <summary>
    /// Creates a new Memory object whose buffer property is a resizable ArrayBuffer or SharedArrayBuffer 
    /// that holds the raw bytes of memory accessed by a WebAssembly Instance.
    ///
    /// A Memory created by JavaScript or in WebAssembly code will be accessible and mutable from both 
    /// Javascript and WebAssembly.
    /// </summmary>
    /// <exception cref="System.Exception">If the memory descriptor property maximum is defined and is 
    /// lower than the initial property value.</exception>
    [<Global>]
    type Memory (descriptor: MemoryDescriptor) =
        /// The buffer contained in the memory.
        [<Emit("$0.buffer")>]
        member _.Buffer : JS.ArrayBuffer = jsNative

        /// Increases the size of the memory instance by a specified number of WebAssembly pages.
        ///
        /// Returns the previous size of the memory in units of WebAssembly pages.
        [<Emit("$0.grow($1)")>]
        member _.Grow (delta: int<WasmPages>) : int<WasmPages> = jsNative

    /// The types that can be stored in a WebAssembly table.
    [<RequireQualifiedAccess;StringEnum(CaseRules.LowerFirst)>]
    type TableElement =
        | Anyfunc

    /// A configuration object for WebAssembly Table instantiation.
    [<Global>]
    type TableDescriptor private () =
        [<Emit("Object.defineProperty(Object.defineProperty(Object.create(null), 'element', {value: $0}), 'initial', {value: $1})")>]
        new (element: TableElement, initial: int) = TableDescriptor()
        
        [<Emit("Object.defineProperty(Object.defineProperty(Object.defineProperty(Object.create(null), 'element', {value: $0}), 'initial', {value: $1}), 'maximum', {value: $2})")>]
        new (element: TableElement, initial: int, maximum: int) = TableDescriptor()

        /// A string representing the type of value to be stored in the table. 
        ///
        /// At the moment this can only have a value of "anyfunc" (functions).
        [<Emit("$0.element")>]
        member _.Element : TableElement = jsNative

        /// The initial number of elements of the WebAssembly Table.
        [<Emit("$0.initial")>]
        member _.Initial : int = jsNative

        /// The maximum number of elements the WebAssembly Table is allowed to grow to.
        [<Emit("$0.maximum ")>]
        member _.Maximum  : int option = jsNative
    
    /// A configuration object for WebAssembly Table instantiation.
    [<Erase;RequireQualifiedAccess>]
    module TableDescriptor =
        /// A string representing the type of value to be stored in the table. 
        ///
        /// At the moment this can only have a value of "anyfunc" (functions).
        let inline element (td: TableDescriptor) = td.Element
        
        /// The initial number of elements of the WebAssembly Table.
        let inline initial (td: TableDescriptor) = td.Initial
        
        /// The maximum number of elements the WebAssembly Table is allowed to grow to.
        let inline maximum (td: TableDescriptor) = td.Maximum

    [<Erase>]
    type Function =
        /// Casts the function to a type (no runtime detriment).
        member inline this.As<'T> () = unbox<'T> this

        /// The number of declared arguments in the wasm function signature.
        [<Emit("$0.length")>]
        member _.Length = jsNative

    [<Erase;RequireQualifiedAccess>]
    module Function =
        /// Casts the function to a type (no runtime detriment).
        let inline as'<'T> (f: Function) = unbox<'T> f
        
        /// The number of declared arguments in the wasm function signature.
        let inline length (f: Function) = f.Length

    /// <summary>
    /// A Javascript wrapper object — an array-like structure representing a WebAssembly 
    /// Table, which stores function references. 
    ///
    /// A table created by JavaScript or in WebAssembly code will be accessible and mutable 
    /// from both JavaScript and WebAssembly.
    /// </summmary>
    /// <exception cref="System.Exception">If the table descriptor property maximum is defined and is 
    /// lower than the initial property value.</exception>
    [<Global>]
    type Table (descriptor: TableDescriptor) =
        /// The buffer contained in the memory.
        [<Emit("$0.length")>]
        member _.Length : int = jsNative
        
        /// Get or set a Table element.
        member _.Item
            with [<Emit("$0.get($1)")>] get (index: int) : Function = jsNative
            and [<Emit("$0.set($1, $2)")>] set (index: int) (value: Function) = jsNative

        /// Increases the size of the Table instance by a specified number of elements.
        [<Emit("$0.grow($1)")>]
        member _.Grow (delta: int) : int = jsNative

    /// A Javascript wrapper object — an array-like structure representing a WebAssembly 
    /// Table, which stores function references. 
    ///
    /// A table created by JavaScript or in WebAssembly code will be accessible and mutable 
    /// from both JavaScript and WebAssembly.
    [<Erase;RequireQualifiedAccess>]
    module Table =
        /// Gets the element stored at a given index.
        let inline get (index: int) (table: Table) = table.[index]
        
        /// Sets an element stored at a given index to a given value.
        let inline set (index: int) (value: Function) (table: Table) = table.[index] <- value

    [<RequireQualifiedAccess;StringEnum(CaseRules.LowerFirst)>]
    type ImportExportKind =
        | Function
        | Global
        | Memory
        | Table

    type ModuleExportDescriptor =
        [<Emit("$0.kind")>]
        member _.Kind : ImportExportKind = jsNative

        [<Emit("$0.name")>]
        member _.Name : string = jsNative
        
    [<Erase;RequireQualifiedAccess>]
    module ModuleExportDescriptor =
        let inline kind (descriptor: #ModuleExportDescriptor) = descriptor.Kind
        let inline name (descriptor: #ModuleExportDescriptor) = descriptor.Name
    
    type ModuleImportDescriptor =
        [<Emit("$0.kind")>]
        member _.Kind : ImportExportKind = jsNative
        
        [<Emit("$0.module")>]
        member _.Module : string = jsNative

        [<Emit("$0.name")>]
        member _.Name : string = jsNative

    [<Erase;RequireQualifiedAccess>]
    module ModuleImportDescriptor =
        let inline kind (descriptor: #ModuleImportDescriptor) = descriptor.Kind
        let inline module' (descriptor: #ModuleImportDescriptor) = descriptor.Module
        let inline name (descriptor: #ModuleImportDescriptor) = descriptor.Name

    /// Creates a new Module object containing stateless WebAssembly code that has already 
    /// been compiled by the browser and can be efficiently shared with Workers, and 
    /// instantiated multiple times.
    ///
    /// You should avoid using the constructor when possible as it will compile the module synchronously.
    [<Global>]
    type Module<'Exports when 'Exports : not struct> private () =
        [<Emit("new WebAssembly.Module($0)")>]
        new (buffer: Fable.Core.JS.ArrayBuffer) = new Module<'Exports>()

        [<Emit("new WebAssembly.Module($0)")>]
        new (buffer: Fable.Core.JS.ArrayBufferView) = new Module<'Exports>()
        
        /// Returns a copy of the contents of all custom sections in the given module with the given string name.
        [<Emit("WebAssembly.Module.customSections($0, $1)")>]
        static member customSections (module': Module<'Exports>) (sectionName: string) : JS.ArrayBuffer [] = jsNative
        
        /// Returns an array containing descriptions of all the declared exports of the given Module.
        [<Emit("WebAssembly.Module.exports($0)")>]
        static member exports (module': Module<'Exports>) : ModuleExportDescriptor [] = jsNative

    /// Creates a new Module object containing stateless WebAssembly code that has already 
    /// been compiled by the browser and can be efficiently shared with Workers, and 
    /// instantiated multiple times.
    ///
    /// You should avoid using the constructor when possible as it will compile the module synchronously.
    [<Global>]
    type Module<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> private () =
        [<Emit("new WebAssembly.Module($0)")>]
        new (buffer: Fable.Core.JS.ArrayBuffer) = new Module<'Imports,'Exports>()

        [<Emit("new WebAssembly.Module($0)")>]
        new (buffer: Fable.Core.JS.ArrayBufferView) = new Module<'Imports,'Exports>()
        
        /// Returns a copy of the contents of all custom sections in the given module with the given string name.
        [<Emit("WebAssembly.Module.customSections($0, $1)")>]
        static member customSections (module': Module<'Imports,'Exports>) (sectionName: string) : JS.ArrayBuffer [] = jsNative
        
        /// Returns an array containing descriptions of all the declared exports of the given Module.
        [<Emit("WebAssembly.Module.exports($0)")>]
        static member exports (module': Module<'Imports,'Exports>) : ModuleExportDescriptor [] = jsNative
        
        /// Returns an array containing descriptions of all the declared imports of the given Module.
        [<Emit("WebAssembly.Module.imports($0)")>]
        static member imports (module': Module<'Imports,'Exports>) : ModuleImportDescriptor [] = jsNative

    /// An object containing the values to be imported into the newly-created Instance, 
    /// such as functions or WebAssembly.Memory objects.
    [<Global>]
    type Import<'Imports when 'Imports : not struct> private () =
        [<Emit("Object.defineProperty(Object.create(null), 'imports', {value: $0})")>]
        new (imports: 'Imports) = Import(imports)

        [<Emit("$0.imports")>]
        member _.Imports: 'Imports = jsNative
    
    /// An object containing the values to be imported into the newly-created Instance, 
    /// such as functions or WebAssembly.Memory objects.
    ///
    /// For use with Rust WebAssembly imports.
    ///
    /// Rust wasm looks for a {env: ...} object rather than {imports: ...}
    [<Global>]
    type ImportEnv<'Imports when 'Imports : not struct> private () =
        [<Emit("Object.defineProperty(Object.create(null), 'env', {value: $0})")>]
        new (imports: 'Imports) = ImportEnv(imports)

        [<Emit("$0.env")>]
        member _.Env: 'Imports = jsNative

    /// <summary>
    /// A stateful, executable instance of a WebAssembly.Module.
    ///
    /// Since instantiation for large modules can be expensive, developers should only use the Instance constructor
    /// when synchronous instantiation is absolutely required; the asynchronous WA.instantiateStreaming
    /// method should be used at all other times.
    /// </summary>
    /// <exception cref="System.Exception">There must be one matching property for each declared import for the module
    /// when an importObject is defined or else a WebAssembly.LinkError is thrown.</exception>
    [<Global>]
    type Instance<'Exports when 'Exports : not struct> private () =
        new (module': Module<'Exports>) = new Instance<'Exports>()

        [<Emit("$0.customSections")>]
        member _.CustomSections : JS.ArrayBuffer = jsNative

        [<Emit("$0.exports")>]
        member _.Exports : 'Exports = jsNative
        
        static member inline customSections (i: Instance<'Exports>) = i.CustomSections
        static member inline exports (i: Instance<'Exports>) = i.Exports
        
    /// <summary>
    /// A stateful, executable instance of a WebAssembly.Module.
    ///
    /// Since instantiation for large modules can be expensive, developers should only use the Instance constructor
    /// when synchronous instantiation is absolutely required; the asynchronous WA.instantiateStreaming
    /// method should be used at all other times.
    /// </summary>
    /// <exception cref="System.Exception">There must be one matching property for each declared import for the module
    /// when an importObject is defined or else a WebAssembly.LinkError is thrown.</exception>
    [<Global>]
    type Instance<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> private () =
        new (module': Module<'Imports,'Exports>, ?importObject: Import<'Imports>) = new Instance<'Imports,'Exports>()

        [<Emit("$0.customSections")>]
        member _.CustomSections : JS.ArrayBuffer = jsNative

        [<Emit("$0.exports")>]
        member _.Exports : 'Exports = jsNative
        
        [<Emit("$0.imports")>]
        member _.Imports : 'Imports = jsNative
        
        static member inline customSections (i: Instance<'Imports,'Exports>) = i.CustomSections
        static member inline exports (i: Instance<'Imports,'Exports>) = i.Exports
        static member inline imports (i: Instance<'Imports,'Exports>) = i.Imports

    [<Erase>]
    type InstantiateResult<'Exports when 'Exports : not struct> =
        /// The compiled WebAssembly Module. 
        ///
        /// This Module can be instantiated again, shared via postMessage() or 
        /// cached in IndexedDB.
        [<Emit("$0.module")>]
        member _.Module : Module<'Exports> = jsNative
        
        /// A WebAssembly Instance that contains all the exported WebAssembly functions.
        [<Emit("$0.instance")>]
        member _.Instance : Instance<'Exports> = jsNative

    [<Erase>]
    type InstantiateResult<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> =
        /// The compiled WebAssembly Module. 
        ///
        /// This Module can be instantiated again, shared via postMessage() or 
        /// cached in IndexedDB.
        [<Emit("$0.module")>]
        member _.Module : Module<'Imports,'Exports> = jsNative
        
        /// A WebAssembly Instance that contains all the exported WebAssembly functions.
        [<Emit("$0.instance")>]
        member _.Instance : Instance<'Imports,'Exports> = jsNative

[<AutoOpen;Erase;EditorBrowsable(EditorBrowsableState.Never)>]
module WAExtensions =
    type WA with
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compile($0)")>]
        static member compile<'Exports when 'Exports : not struct>
            (bufferSource: JS.TypedArray) : JS.Promise<WA.Module<'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compile($0)")>]
        static member compile<'Exports when 'Exports : not struct>
            (bufferSource: JS.ArrayBuffer) : JS.Promise<WA.Module<'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compile($0)")>]
        static member compile<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray) : JS.Promise<WA.Module<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compile($0)")>]
        static member compile<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer) : JS.Promise<WA.Module<'Imports,'Exports>> = jsNative

        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompile<'Exports when 'Exports : not struct> 
            (bufferSource: JS.TypedArray) =
            
            try WA.compile<'Exports>(bufferSource) |> Ok
            with e -> Error e
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompile<'Exports when 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer) =
            
            try WA.compile<'Exports>(bufferSource) |> Ok
            with e -> Error e
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompile<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray) =
            
            try WA.compile<'Imports,'Exports>(bufferSource) |> Ok
            with e -> Error e
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompile<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer) =
            
            try WA.compile<'Imports,'Exports>(bufferSource) |> Ok
            with e -> Error e

        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compileStreaming($0)")>]
        static member compileStreaming<'Exports when 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse) : JS.Promise<WA.Module<'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compileStreaming($0)")>]
        static member compileStreaming<'Exports when 'Exports : not struct> 
            (source: Fetch.Types.Response) : JS.Promise<WA.Module<'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compileStreaming($0)")>]
        static member compileStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse) : JS.Promise<WA.Module<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        /// </summary>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.compileStreaming($0)")>]
        static member compileStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fetch.Types.Response) : JS.Promise<WA.Module<'Imports,'Exports>> = jsNative

        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompileStreaming<'Exports when 'Exports : not struct> (source: Fable.SimpleHttp.HttpResponse) =
            promise {
                try
                    return! WA.compileStreaming(source) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompileStreaming<'Exports> (source: Fetch.Types.Response) =
            promise {
                try
                    return! WA.compileStreaming(source) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompileStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> (source: Fable.SimpleHttp.HttpResponse) =
            promise {
                try
                    return! WA.compileStreaming(source) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Compiles WebAssembly binary code into a WebAssembly.Module object. 
        ///
        /// This function is useful if it is necessary to a compile a module before it can be instantiated 
        /// (otherwise, the WA.instantiate function should be used).
        static member inline tryCompileStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> (source: Fetch.Types.Response) =
            promise {
                try
                    return! WA.compileStreaming(source) |> Promise.map Ok
                with e ->
                    return Error e
            }

        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Exports when 'Exports : not struct> 
            (bufferSource: JS.TypedArray) : JS.Promise<WA.InstantiateResult<'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Exports when 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer) : JS.Promise<WA.InstantiateResult<'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Exports when 'Exports : not struct> 
            (module': WA.Module<'Exports>) : JS.Promise<WA.InstantiateResult<'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray, importObject: WA.Import<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray, importObject: WA.ImportEnv<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer, importObject: WA.Import<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer, importObject: WA.ImportEnv<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (module': WA.Module<'Imports,'Exports>, importObject: WA.Import<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiate($0...)")>]
        static member instantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (module': WA.Module<'Imports,'Exports>, importObject: WA.ImportEnv<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative

        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Exports when 'Exports : not struct> 
            (bufferSource: JS.TypedArray) =
            
            try WA.instantiate<'Exports>(bufferSource) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Exports when 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer) = 

            try WA.instantiate<'Exports>(bufferSource) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Exports when 'Exports : not struct> 
            (module': WA.Module<'Exports>) =
            
            try WA.instantiate<'Exports>(module') |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray, importObject: WA.Import<'Imports>) =
            
            try WA.instantiate<'Imports,'Exports>(bufferSource, importObject = importObject) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.TypedArray, importObject: WA.ImportEnv<'Imports>) =
            
            try WA.instantiate<'Imports,'Exports>(bufferSource, importObject = importObject) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer, importObject: WA.Import<'Imports>) = 

            try WA.instantiate<'Imports,'Exports>(bufferSource, importObject = importObject) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (bufferSource: JS.ArrayBuffer, importObject: WA.ImportEnv<'Imports>) = 

            try WA.instantiate<'Imports,'Exports>(bufferSource, importObject = importObject) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (module': WA.Module<'Imports,'Exports>, importObject: WA.Import<'Imports>) =
            
            try WA.instantiate<'Imports,'Exports>(module', importObject = importObject) |> Ok
            with e -> Error e
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiate<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (module': WA.Module<'Imports,'Exports>, importObject: WA.ImportEnv<'Imports>) =
            
            try WA.instantiate<'Imports,'Exports>(module', importObject = importObject) |> Ok
            with e -> Error e

        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Exports when 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse) : JS.Promise<WA.InstantiateResult<'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Exports when 'Exports : not struct> 
            (source: Fetch.Types.Response) : JS.Promise<WA.InstantiateResult<'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse, importObject: WA.Import<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse, importObject: WA.ImportEnv<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fetch.Types.Response, importObject: WA.Import<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative
        /// <summary>
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        /// </summary>
        /// <exception cref="System.Exception">The module loaded does not match the provided type structure.</exception>
        /// <exception cref="System.Exception">The operation failed and the promise rejects.</exception>
        [<Emit("WebAssembly.instantiateStreaming($0...)")>]
        static member instantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fetch.Types.Response, importObject: WA.ImportEnv<'Imports>) : JS.Promise<WA.InstantiateResult<'Imports,'Exports>> = jsNative

        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Exports when 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse) =
                
            promise {
                try
                    return! WA.instantiateStreaming<'Exports>(source) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Exports when 'Exports : not struct> 
            (source: Fetch.Types.Response) =

            promise {
                try
                    return! WA.instantiateStreaming<'Exports>(source) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse, importObject: WA.Import<'Imports>) =
                
            promise {
                try
                    return! WA.instantiateStreaming<'Imports,'Exports>(source, importObject = importObject) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fable.SimpleHttp.HttpResponse, importObject: WA.ImportEnv<'Imports>) =
                
            promise {
                try
                    return! WA.instantiateStreaming<'Imports,'Exports>(source, importObject = importObject) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fetch.Types.Response, importObject: WA.Import<'Imports>) =

            promise {
                try
                    return! WA.instantiateStreaming<'Imports,'Exports>(source, importObject = importObject) |> Promise.map Ok
                with e ->
                    return Error e
            }
        /// Allows you to compile and instantiate WebAssembly code.
        ///
        /// This method is not the most efficient way of fetching and instantiating wasm modules. 
        ///
        /// If at all possible, you should use the newer WebAssembly.instantiateStreaming() method instead, 
        /// which fetches, compiles, and instantiates a module all in one step, directly from the raw 
        /// bytecode, so doesn't require conversion to an ArrayBuffer.
        static member inline tryInstantiateStreaming<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> 
            (source: Fetch.Types.Response, importObject: WA.ImportEnv<'Imports>) =

            promise {
                try
                    return! WA.instantiateStreaming<'Imports,'Exports>(source, importObject = importObject) |> Promise.map Ok
                with e ->
                    return Error e
            }

        /// Validates a given typed array of WebAssembly binary code, returning whether the bytes form 
        /// a valid wasm module (true) or not (false).
        [<Emit("WebAssembly.validate($0)")>]
        static member validate (bufferSource: JS.TypedArray) : bool = jsNative
        /// Validates a given typed array of WebAssembly binary code, returning whether the bytes form 
        /// a valid wasm module (true) or not (false).
        [<Emit("WebAssembly.validate($0)")>]
        static member validate (bufferSource: JS.ArrayBuffer) : bool = jsNative
