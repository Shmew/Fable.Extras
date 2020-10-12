# Module

## Import Export Kind

The type being imported/exported.

```fsharp
type ImportExportKind =
    | Function
    | Global
    | Memory
    | Table
```

## Module Export Descriptor

Module exports metadata.

```fsharp
type ModuleExportDescriptor =
    /// The type of export.
    member _.Kind : ImportExportKind

    /// The name of the export.
    member _.Name : string
```

The functions outlined below are located in the `ModuleExportDescriptor` module.

### kind

The type of export.

Signature:
```fsharp
(descriptor: ModuleExportDescriptor) -> ImportExportKind
```

### name

The name of the export.

Signature:
```fsharp
(descriptor: ModuleExportDescriptor) -> string
```

## Module Import Descriptor

Module exports metadata.

```fsharp
type ModuleImportDescriptor =
    /// The type of import.
    member _.Kind : ImportExportKind
        
    /// The name of the module.
    member _.Module : string
        
    /// The name of the import.
    member _.Name : string
```

The functions outlined below are located in the `ModuleImportDescriptor` module.

### kind

The type of import.

Signature:
```fsharp
(descriptor: ModuleImportDescriptor) -> ImportExportKind
```

### module'

The name of the module.

Signature:
```fsharp
(descriptor: ModuleImportDescriptor) -> string
```

### name

The name of the import.

Signature:
```fsharp
(descriptor: ModuleImportDescriptor) -> string
```

## Module

Creates a new `Module` object containing stateless WebAssembly code that has already 
been compiled by the browser and can be efficiently shared with Workers, and 
instantiated multiple times.

You should avoid using the constructor when possible as it will compile the module synchronously.

There are two variants of `Module` based on if an export is also provided.

```fsharp
type Module<'Exports when 'Exports : not struct> =
    new (buffer: JS.ArrayBuffer)
    new (buffer: JS.ArrayBufferView)

    /// Returns a copy of the contents of all custom sections in the given module with the 
    /// given string name.
    static member customSections (module': Module<'Exports>) (sectionName: string) : ArrayBuffer []

    /// Returns an array containing descriptions of all the declared exports of the 
    /// given Module.
    static member exports (module': Module<'Exports>) : ModuleExportDescriptor []
```

```fsharp
type Module<'Imports,'Exports when 'Imports : not struct and 'Exports : not struct> =
    new (buffer: JS.ArrayBuffer)
    new (buffer: JS.ArrayBufferView)
        
    /// Returns a copy of the contents of all custom sections in the given module with the 
    /// given string name.
    static member customSections (module': Module<'Imports,'Exports>) (sectionName: string) 
        : ArrayBuffer []
        
    /// Returns an array containing descriptions of all the declared exports of the given Module.
    static member exports (module': Module<'Imports,'Exports>) : ModuleExportDescriptor []
        
    /// Returns an array containing descriptions of all the declared imports of the given Module.
    static member imports (module': Module<'Imports,'Exports>) : ModuleImportDescriptor []
```
