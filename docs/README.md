# Fable.Extras

A more functional construct on-top of Fable.Core.

Near-zero bundle size, as almost the entire library is erased at compile time.

### A quick look:


#### JS Maps

```fsharp
open Fable.Extras

JSe.Map.empty<int,int>
|> JSe.Map.set 1 1
|> JSe.Map.set 2 4
|> JSe.Map.values
|> List.sum // 5
```

#### JS Regular Expressions and String extensions

```fsharp
open Fable.Extras

let pattern = JSe.RegExp("^[0-9]")

"20 foxes jumped over the bridge".Replace(pattern, "numbers") 
// "numbers foxes jumped over the bridge"
```

#### Or assignment chaining

```fsharp
open Fable.Extras
open Fable.Extras.Operators

let maybeInt = None ?| Some 1 ?| None ?| Some 2 // Some 1
```

#### Web Assembly

```fsharp
open Fable.Extras
open Fable.Extras.WebAssembly

// Wasm binding
type WasmFableExports =
    abstract add: int * int -> int

async {
    let! wasmBinary = ... // HttpResponse

    return! WA.compileStreaming<WasmFableExports>(wasmBinary)
}

...

wasmInstance.add(1,2) // 3
```
