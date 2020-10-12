# Fable.Extras [![Nuget](https://img.shields.io/nuget/v/Fable.Extras.svg?maxAge=0&colorB=brightgreen&label=Fable.Extras)](https://www.nuget.org/packages/Fable.Extras)

A more functional construct on-top of Fable.Core.

Near-zero bundle size, as almost the entire library is erased at compile time.

### A quick look:


#### JS Maps

```fsharp
JSe.Map.empty<int,int>
|> JSe.Map.set 1 1
|> JSe.Map.set 2 4
|> JSe.Map.values
|> List.sum // 5
```

#### JS Regular Expressions and String extensions

```fsharp
let pattern = JSe.RegExp("^[0-9]")

"20 foxes jumped over the bridge".Replace(pattern, "numbers") // "numbers foxes jumped over the bridge"
```

#### Or assignment chaining

```fsharp
open Fable.Extras.Operators

let maybeInt = None ?| Some 1 ?| None ?| Some 2 // Some 1
```

#### Web Assembly

```fsharp
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
