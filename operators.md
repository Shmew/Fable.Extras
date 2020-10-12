# Operators

Operators are accessible by opening the `Fable.Extras.Operators` namespace.

## or'

An operator for [or'](/extras/globals#or).

Operator: `?|`

Usage:
```fsharp
let maybeInt = None ?| Some 1 ?| None ?| Some 2 // Some 1
```
