namespace Fable.Extras.WebAssembly

open Fable.Core
open Fable.Extras
open FSharp.Core

[<Erase;RequireQualifiedAccess>]
module WA =
    [<Global>]
    type CompileError =
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

    [<Erase>]
    type GlobalDescriptor private (o: obj) =
        [<Emit("{ value: $0; mutable: false }")>]
        new (value: DescriptorValue) = GlobalDescriptor(value)

        [<Emit("{ value: $0; mutable: $1 }")>]
        new (value: DescriptorValue, mutable': bool) = GlobalDescriptor(value)

        [<Emit("$0.value")>]
        member _.value : DescriptorValue = jsNative
        [<Emit("$0.mutable")>]
        member _.mutable' : bool = jsNative

    [<Global>]
    type Global<'T> =
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

[<Global>]
type WA =
    /// Creates a new WebAssembly CompileError object, which indicates an error during WebAssembly decoding or validation.
    [<Emit("new WebAssembly.CompileError($0...)")>] 
    static member CompileError (?message: string, ?fileName: string, ?lineNumber: int) = jsNative
    
    /// Creates a new Global object representing a global variable instance, accessible from both JavaScript and 
    /// importable/exportable across one or more WebAssembly.Module instances. 
    ///
    /// This allows dynamic linking of multiple modules.
    [<Emit("new WebAssembly.Global($0, $1)")>]
    static member Global<'T> (descriptor: WA.GlobalDescriptor, value: 'T) = jsNative

    // TODO: Finish this