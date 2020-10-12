# Table

## TableElement

The types that can be stored in a WebAssembly table.

```fsharp
type TableElement =
    | Anyfunc
```

## TableDescriptor

A configuration object for WebAssembly Table instantiation.

```fsharp
 type TableDescriptor private () =
    new (element: TableElement, initial: int)
    new (element: TableElement, initial: int, maximum: int)

    /// A string representing the type of value to be stored in the table. 
    ///
    /// At the moment this can only have a value of "anyfunc" (functions).
    member _.Element : TableElement

    /// The initial number of elements of the WebAssembly Table.
    member _.Initial : int

    /// The maximum number of elements the WebAssembly Table is allowed to grow to.
    member _.Maximum  : int option
```

The functions outlined below are located in the `TableDescriptor` module.

### element

A string representing the type of value to be stored in the table. 

At the moment this can only have a value of `Anyfunc` (functions).

Signature:
```fsharp
(td: TableDescriptor) -> TableElement
```

### initial

The initial number of elements of the WebAssembly Table.

Signature:
```fsharp
(td: TableDescriptor) -> int
```

### maximum

The maximum number of elements the WebAssembly `Table` is allowed to grow to.

Signature:
```fsharp
(td: TableDescriptor) -> int option
```

## Table

A Javascript wrapper object — an array-like structure representing a WebAssembly 
Table, which stores function references. 

A table created by JavaScript or in WebAssembly code will be accessible and mutable 
from both JavaScript and WebAssembly.

```fsharp
type Table (descriptor: TableDescriptor) =
    /// The length of the table, i.e. the number of elements.
    member _.Length : int
        
    /// Get or set a Table element.
    member _.Item
        with get (index: int) : obj
        and set (index: int) (value: obj) : unit

    /// Increases the size of the Table instance by a specified number of elements.
    member _.Grow (delta: int) : int
```

The functions outlined below are located in the `Table` module.

### get

Gets the element stored at a given index.

Signature:
```fsharp
(index: int) -> (table: Table) -> obj
```

### getAs

Gets the element stored at a given index.

Signature:
```fsharp
<'T> (index: int) -> (table: Table) -> 'T
```

### set

Sets an element stored at a given index to a given value.

Signature:
```fsharp
(index: int) -> (value: obj) -> (table: Table) -> Table
```
