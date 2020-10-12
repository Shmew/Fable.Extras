# Web Assembly

All functions outlined below are accessible via the `WA` module.

## compile

Compiles WebAssembly binary code into a [Module](/web-assembly/module) object. 

This function is useful if it is necessary to a compile a module before it can be instantiated 
(otherwise, the [instantiate](#instantiate) function should be used).

Signature:
```fsharp
<'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<WA.Module<'Exports>>
    (bufferSource: JS.ArrayBuffer) -> Promise<WA.Module<'Exports>>
<'Imports,'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<WA.Module<'Imports,'Exports>
    (bufferSource: JS.ArrayBuffer) -> Promise<WA.Module<'Imports,'Exports>>
```

## tryCompile

Compiles WebAssembly binary code into a [Module](/web-assembly/module) object. 

This function is useful if it is necessary to a compile a module before it can be instantiated 
(otherwise, the [instantiate](#instantiate) function should be used).

Signature:
```fsharp
<'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<Result<WA.Module<'Exports>,exn>>
    (bufferSource: JS.ArrayBuffer) -> Promise<Result<WA.Module<'Exports>,exn>>
<'Imports,'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<Result<WA.Module<'Imports,'Exports>,exn>>
    (bufferSource: JS.ArrayBuffer) -> Promise<Result<WA.Module<'Imports,'Exports>,exn>>
```

## compileStreaming

Compiles WebAssembly binary code into a [Module](/web-assembly/module) object. 

This function is useful if it is necessary to a compile a module before it can be instantiated 
(otherwise, the [instantiate](#instantiate) function should be used).

Signature:
```fsharp
<'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<WA.Module<'Exports>>
    (source: Fetch.Types.Response) -> Promise<WA.Module<'Exports>>
<'Imports,'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<WA.Module<'Imports,'Exports>
    (source: Fetch.Types.Response) -> Promise<WA.Module<'Imports,'Exports>>
```

## tryCompileStreaming

Compiles WebAssembly binary code into a [Module](/web-assembly/module) object. 

This function is useful if it is necessary to a compile a module before it can be instantiated 
(otherwise, the [instantiate](#instantiate) function should be used).

Signature:
```fsharp
<'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<Result<WA.Module<'Exports>,exn>>
    (source: Fetch.Types.Response) -> Promise<Result<WA.Module<'Exports>,exn>>
<'Imports,'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<Result<WA.Module<'Imports,'Exports>,exn>>
    (source: Fetch.Types.Response) -> Promise<Result<WA.Module<'Imports,'Exports>,exn>>
```

## instantiate

Allows you to compile and instantiate WebAssembly code.

This method is not the most efficient way of fetching and instantiating wasm modules. 

If at all possible, you should use the newer [instantiateStreaming](#instantiatestreaming) method instead, 
which fetches, compiles, and instantiates a module all in one step, directly from the raw 
bytecode, so doesn't require conversion to an [ArrayBuffer](/extras/array-buffer).

Signature:
```fsharp
<'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<WA.InstantiateResult<'Exports>>
    (bufferSource: JS.ArrayBuffer) -> Promise<WA.InstantiateResult<'Exports>>
    (module': WA.Module<'Exports>) -> Promise<WA.InstantiateResult<'Exports>>
<'Imports,'Exports> 
    (bufferSource: JS.TypedArray, importObject: WA.Import<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (bufferSource: JS.TypedArray, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (bufferSource: JS.ArrayBuffer, importObject: WA.Import<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (bufferSource: JS.ArrayBuffer, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (module': WA.Module<'Exports>, importObject: WA.Import<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (module': WA.Module<'Exports>, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
```

## tryInstantiate

Allows you to compile and instantiate WebAssembly code.

This method is not the most efficient way of fetching and instantiating wasm modules. 

If at all possible, you should use the newer [instantiateStreaming](#instantiatestreaming) method instead, 
which fetches, compiles, and instantiates a module all in one step, directly from the raw 
bytecode, so doesn't require conversion to an [ArrayBuffer](/extras/array-buffer).

Signature:
```fsharp
<'Exports> 
    (bufferSource: JS.TypedArray) -> Promise<Result<WA.InstantiateResult<'Exports>,exn>>
    (bufferSource: JS.ArrayBuffer) -> Promise<Result<WA.InstantiateResult<'Exports>,exn>>
    (module': WA.Module<'Exports>) -> Promise<Result<WA.InstantiateResult<'Exports>,exn>>
<'Imports,'Exports> 
    (bufferSource: JS.TypedArray, importObject: WA.Import<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (bufferSource: JS.TypedArray, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (bufferSource: JS.ArrayBuffer, importObject: WA.Import<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (bufferSource: JS.ArrayBuffer, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (module': WA.Module<'Exports>, importObject: WA.Import<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (module': WA.Module<'Exports>, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
```

## instantiateStreaming

Compiles and instantiates a WebAssembly module directly from a streamed 
underlying source, returning both a Module and its first Instance.

Signature:
```fsharp
<'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<WA.InstantiateResult<'Exports>>
    (source: Fetch.Types.Response) -> Promise<WA.InstantiateResult<'Exports>>
<'Imports,'Exports> 
    (source: Fable.SimpleHttp.HttpResponse, importObject: WA.Import<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (source: Fable.SimpleHttp.HttpResponsey, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (source: Fetch.Types.Response, importObject: WA.Import<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
    (source: Fetch.Types.Response, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<WA.InstantiateResult<'Imports,'Exports>>
```

## tryInstantiateStreaming

Compiles and instantiates a WebAssembly module directly from a streamed 
underlying source, returning both a Module and its first Instance.

Signature:
```fsharp
<'Exports> 
    (source: Fable.SimpleHttp.HttpResponse) -> Promise<Result<WA.InstantiateResult<'Exports>,exn>>
    (source: Fetch.Types.Response) -> Promise<Result<WA.InstantiateResult<'Exports>,exn>>
<'Imports,'Exports> 
    (source: Fable.SimpleHttp.HttpResponse, importObject: WA.Import<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (source: Fable.SimpleHttp.HttpResponse, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (source: Fetch.Types.Response, importObject: WA.Import<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
    (source: Fetch.Types.Response, importObject: WA.ImportEnv<'Imports>) 
        -> Promise<Result<WA.InstantiateResult<'Imports,'Exports>,exn>>
```
