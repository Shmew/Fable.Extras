# Errors

The different errors that can be thrown during operations with Web Assembly.

## CompileError

```fsharp
type CompileError (?message: string, ?fileName: string, ?lineNumber: int) =
    inherit exn

    member columnNumber : int
    member fileName : string
    member lineNumber : int
    member message : string
    member name : string
    member stack : string
```

## LinkError

```fsharp
type LinkError (?message: string, ?fileName: string, ?lineNumber: int) =
    inherit exn

    member columnNumber : int
    member fileName : string
    member lineNumber : int
    member message : string
    member name : string
    member stack : string
```

## RuntimeError

```fsharp
type RuntimeError (?message: string, ?fileName: string, ?lineNumber: int) =
    inherit exn

    member columnNumber : int
    member fileName : string
    member lineNumber : int
    member message : string
    member name : string
    member stack : string
```
