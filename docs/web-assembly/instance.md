# Instance

A stateful, executable instance of a [Module](/web-assembly/module).

Since instantiation for large modules can be expensive, developers should only use the `Instance` constructor
when synchronous instantiation is absolutely required; the asynchronous [instantiateStreaming](/web-assembly/web-assembly#instantiatestreaming)
method should be used at all other times.

There are two variants of `Instance` based on if an export is also provided.

```fsharp
type Instance<'Exports when 'Exports : not struct> (module': Module<'Exports>) =
    member _.CustomSections : ArrayBuffer
    member _.Exports : 'Exports

    static member customSections (i: Instance<'Exports>) : ArrayBuffer
    static member exports (i: Instance<'Exports>) : 'Exports
```

```fsharp
type Instance<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> =
    new (module': Module<'Imports,'Exports>, ?importObject: Import<'Imports>)    

    member _.CustomSections : ArrayBuffer

    member _.Exports : 'Exports
        
    member _.Imports : 'Imports
    
    static member customSections (i: Instance<'Imports,'Exports>) : ArrayBuffer
    static member exports (i: Instance<'Imports,'Exports>) : 'Exports
    static member imports (i: Instance<'Imports,'Exports>) : 'Imports
```
