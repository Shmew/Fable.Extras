# Import

The formats a Web Assembly module will be imported and accessible from.

## Import

An object containing the values to be imported into the newly-created Instance, 
such as functions or [Memory](/web-assembly/memory) objects.

```fsharp
type Import<'Imports when 'Imports : not struct> (imports: 'Imports) =
    member _.imports: 'Imports = jsNative
```

## ImportEnv

An object containing the values to be imported into the newly-created Instance, 
such as functions or [Memory](/web-assembly/memory) objects.

For use with Rust WebAssembly imports.

Rust wasm looks for a {env: ...} object rather than {imports: ...}

```fsharp
type ImportEnv<'Imports when 'Imports : not struct> (imports: 'Imports) =
    member _.env: 'Imports
```
