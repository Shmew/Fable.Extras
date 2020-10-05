# Fable.Extras [![Nuget](https://img.shields.io/nuget/v/Fable.Extras.svg?maxAge=0&colorB=brightgreen&label=Fable.Extras)](https://www.nuget.org/packages/Fable.Extras)

A more functional construct on-top of Fable.Core.

Zero bundle size, the entire library is erased at compile time.

A quick look:

```fsharp
JSe.Map.empty<int,int>
|> JSe.Map.set 1 1
|> JSe.Map.set 2 4
|> JSe.Map.values
|> List.sum // 5
```

```fsharp
let pattern = JSe.RegExp("^[0-9]")

"20 foxes jumped over the bridge".Replace(pattern, "numbers") // "numbers foxes jumped over the bridge"
```

```fsharp
open Fable.Extras.Operators

let maybeInt = None ?| Some 1 ?| None ?| Some 2 // Some 1
```
