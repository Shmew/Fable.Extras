# Typed Array

Describes an array-like view of an underlying binary data buffer.

```fsharp
type TypedArray<'T> =
    member Item
        with get (index: int) : 'T
        and set (index: int) (value: int) : unit

    /// Returns a sequence that contains the key/value pairs for each index in the array.
    member Entries () : seq<int * 'T>

    /// The ArrayBuffer referenced by a TypedArray at construction time.
    member Buffer: ArrayBuffer

    /// The length (in bytes) of a typed array.
    member ByteLength: int

    /// The offset (in bytes) of a typed array from the start of its ArrayBuffer.
    member ByteOffset: int

    /// The length (in elements) of a typed array.
    member Length: int

    /// Copies the sequence of array elements within the array to the position 
    /// starting at target. 
    ///
    /// The copy is taken from the index positions of the second and third arguments 
    /// start and end. The end argument is optional and defaults to the length of the array.
    member CopyWithin (targetStartIndex: int, start: int, ?end': int) : unit

    /// The keys for each index in the array.
    member Keys () : seq<int>

    /// Joins all elements of an array into a string.
    member Join (separator: string) : string

    /// Fills all the elements of a typed array from a start index to an end index with 
    /// a static value.
    member Fill (value:'T, ?start': int, ?end': int) : TypedArray<'T>
        
    /// Creates a new typed array with all elements that pass the test 
    /// implemented by the provided function.
    member Filter (f: 'T -> bool) : TypedArray<'T>
    /// Creates a new typed array with all elements that pass the test 
    /// implemented by the provided function.
    ///
    /// Provides the element and index.
    member Filter (f: 'T -> int -> bool) : TypedArray<'T>
    /// Creates a new typed array with all elements that pass the test 
    /// implemented by the provided function.
    ///
    /// Provides the element, index, and array.
    member Filter (f: 'T -> int -> TypedArray<'T> -> bool) : TypedArray<'T>
        
    /// Returns a value in the typed array, if an element satisfies the provided testing function.
    member Find (f: 'T -> bool) : 'T option
    /// Returns a value in the typed array, if an element satisfies the provided testing function.
    ///
    /// Provides the element and index.
    member Find (f: 'T -> int -> bool) : 'T option
    /// Returns a value in the typed array, if an element satisfies the provided testing function.
    ///
    /// Provides the element, index, and array.
    member Find (f: 'T -> int -> TypedArray<'T> -> bool) : 'T option
        
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function. Otherwise -1 is returned.
    member FindIndex (f: 'T -> bool) : int
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function. Otherwise -1 is returned.
    ///
    /// Provides the element and index.
    member FindIndex (f: 'T -> int -> bool) : int
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function. Otherwise -1 is returned.
    ///
    /// Provides the element, index, and array.
    member FindIndex (f: 'T -> int -> TypedArray<'T> -> bool) : int
        
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function.
    member inline this.TryFindIndex (f: 'T -> bool) = 
        this.FindIndex(f) |> fun i -> if i < 0 then None else Some i
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function.
    ///
    /// Provides the element and index.
    member inline this.TryFindIndex (f: 'T -> int -> bool) = 
        this.FindIndex(f) |> fun i -> if i < 0 then None else Some i
    /// Returns an index in the typed array, if an element in the typed array satisfies the 
    /// provided testing function.
    ///
    /// Provides the element, index, and array.
    member inline this.TryFindIndex (f: 'T -> int -> TypedArray<'T> -> bool) = 
        this.FindIndex(f) |> fun i -> if i < 0 then None else Some i

    /// Executes a provided function once per array element.
    member ForEach (f: 'T -> bool) : unit
    /// Executes a provided function once per array element.
    ///
    /// Provides the element and index.
    member ForEach (f: 'T -> int -> bool) : unit
    /// Executes a provided function once per array element.
    ///
    /// Provides the element, index, and array.
    member ForEach (f: 'T -> int -> TypedArray<'T> -> bool) : unit

    /// Determines whether a typed array includes a certain element, 
    /// returning true or false as appropriate.
    member Includes (searchElement:'T, ?fromIndex: int) : bool

    /// Returns the first index at which a given element can be found 
    /// in the typed array, or -1 if it is not present.
    member IndexOf (searchElement:'T, ?fromIndex: int) : int

    /// Returns the last index at which a given element can be found in 
    /// the typed array, or -1 if it is not present.
    member LastIndexOf (searchElement:'T, ?fromIndex: int) : int
        
    /// Creates a new typed array with the results of calling a provided 
    /// function on every element in this typed array.
    member Map (f: 'T -> 'U) : TypedArray<'U>
    /// Creates a new typed array with the results of calling a provided 
    /// function on every element in this typed array.
    ///
    /// Provides the element and index.
    member MapWithIndex (f: 'T -> int -> 'U) : TypedArray<'U>
    /// Creates a new typed array with the results of calling a provided 
    /// function on every element in this typed array.
    ///
    /// Provides the element, index, and array.
    member MapWithIndexArray (f: 'T -> int -> TypedArray<'T> -> 'U) : TypedArray<'U>
        
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from left-to-right) has to reduce it to a single value.
    member Reduce (f: 'State -> 'T -> 'State, state: 'State) : 'State
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from left-to-right) has to reduce it to a single value.
    ///
    /// Provides the element and index.
    member Reduce (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from left-to-right) has to reduce it to a single value.
    ///
    /// Provides the element, index, and array.
    member Reduce (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State
        
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from right-to-left) has to reduce it to a single value.
    member ReduceRight (f: 'State -> 'T -> 'State, state:'State) : 'State
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from right-to-left) has to reduce it to a single value.
    ///
    /// Provides the element and index.
    member ReduceRight (f: 'State -> 'T -> int -> 'State, state: 'State) : 'State
    /// Applies a function against an accumulator and each value of the typed 
    /// array (from right-to-left) has to reduce it to a single value.
    ///
    /// Provides the element, index, and array.
    member ReduceRight (f: 'State -> 'T -> int -> TypedArray<'T> -> 'State, state: 'State) : 'State

    /// Reverses a typed array in place.
    member Reverse () : TypedArray<'T>

    /// Stores multiple values in the typed array, reading input values from a specified array.
    member Set (source: System.Array, ?offset: int) : unit
    /// Stores multiple values in the typed array, reading input values from a specified array.
    member Set (source: #JS.TypedArray, ?offset: int) : unit
        
    /// Returns a shallow copy of a portion of a typed array into a new typed array object. 
    member Slice (?begin': int, ?end': int) : TypedArray<'T>
        
    /// Tests whether some element in the typed array passes the test implemented by 
    /// the provided function.
    member Some (f: 'T -> bool) : bool
    /// Tests whether some element in the typed array passes the test implemented by 
    /// the provided function.
    ///
    /// Provides the element and index.
    member Some (f: 'T -> int -> bool) : bool
    /// Tests whether some element in the typed array passes the test implemented by 
    /// the provided function.
    ///
    /// Provides the element, index, and array.
    member Some (f: 'T -> int -> TypedArray<'T> -> bool) : bool
        
    /// Sorts the elements of a typed array numerically in place and returns the typed array. 
    member Sort (?sortFunction: 'T -> 'T -> int) : TypedArray<'T>
        
    /// Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
    /// as for this TypedArray object. 
    /// 
    /// The begin offset is inclusive and the end offset is exclusive.
    member Subarray (?begin': int, ?end': int) : TypedArray<'T>
        
    /// Returns a sequence that contains the values for each index in the array.
    member Values () : seq<'T>

    interface JS.ArrayBufferView
    interface JS.TypedArray
    interface JS.TypedArray<'T>
    interface Collections.Generic.IEnumerable<'T>
    interface Collections.IEnumerable
```

Typed Arrays in Javascript must be constructed using special constructors 
and only contain specific numeric types. 

The types that are allowed are as follows:
* int8
* unit8
* int16
* uint16
* int32
* uint32
* int64
* uint64
* float32
* float

Each of these has a corresponding type that can be used to construct a `TypedArray<'T>` where
`'T` is their corresponding numeric type:

```fsharp
type Int8Array = 
    inherit TypedArray<int8>

    new (size: int)
    new (typedArray: JS.TypedArray)
    new (typedArray: TypedArray<int8>)
    new (typedArray: JS.TypedArray<int8>)
    new (buffer: JS.ArrayBuffer, ?offset: int, ?length: int)
    new (buffer: ArrayBuffer, ?offset: int, ?length: int)
    new (seq: seq<int8>)
        
    /// The size in bytes of each element in the typed array.
    static member bytesPerElement : int

    /// A string value of the typed array constructor name.
    static member name : string

    /// Helper function to convert (no runtime cost) a Fable.Core.JS interface 
    /// typed array to this representation.
    static member cast (a: JS.Int8Array) : Int8Array

    interface JS.ArrayBufferView
    interface JS.TypedArray
    interface JS.TypedArray<int8>
    interface Collections.Generic.IEnumerable<int8>
    interface Collections.IEnumerable
```

The names of those types are as follows:
* Int8Array
* Uint8Array
* Uint8ClampedArray
* Int16Array
* Uint16Array
* Int32Array
* Uint32Array
* Int64Array
* Uint64Array
* Float32Array
* Float64Array

You can also iterate over `TypedArray<'T>` using `for .. do` syntax:

```fsharp
let ta = JSe.Int32Array [1;2;3;4]

for i in ta do
    JSe.console.log i
```

The functions outlined below are located in the `TypedArray` module.

## buffer

The [ArrayBuffer](/extras/array-buffer) referenced by a TypedArray at construction time.

Signature:
```fsharp
(ta: TypedArray<'T>) -> ArrayBuffer
```

## byteLength

The length (in bytes) of a typed array.

Signature:
```fsharp
(ta: TypedArray<'T>) -> int
```

## byteOffset

The offset (in bytes) of a typed array from the start of its ArrayBuffer.

Signature:
```fsharp
(ta: TypedArray<'T>) -> int
```

## copyWithin

Copies the sequence of array elements within the array to the position 
starting at target. 

The copy is taken from the index positions of the second argument start.

Signature:
```fsharp
(targetStartIndex: int) -> (start: int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## copyWithinEnd

Copies the sequence of array elements within the array to the position 
starting at target. 

The copy is taken from the index positions of the second and third arguments 
start and end.

Signature:
```fsharp
(targetStartIndex: int) -> (start: int) -> (end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## entries

Returns a sequence that contains the key/value pairs for each index in the array.

Signature:
```fsharp
(ta: TypedArray<'T>) -> seq<int * 'T>
```

## length

The length (in elements) of a typed array.

Signature:
```fsharp
(ta: TypedArray<'T>) -> int
```

## fill

Fills all the elements of a typed array from a start index to an end index with 
a static value.

Signature:
```fsharp
(value: 'T) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## fillEnd

Fills all the elements of a typed array from a start index to an end index with 
a static value.

Signature:
```fsharp
(value: 'T) -> (end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## fillStart

Fills all the elements of a typed array from a start index to an end index with 
a static value.

Signature:
```fsharp
(value: 'T) -> (start': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## fillStartEnd

Fills all the elements of a typed array from a start index to an end index with 
a static value.

Signature:
```fsharp
(value: 'T) -> (start': int) -> (end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## filter

Creates a new typed array with all elements that pass the test 
implemented by the provided function.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## filterWithIndex

Creates a new typed array with all elements that pass the test 
implemented by the provided function.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## filterWithIndexArray

Creates a new typed array with all elements that pass the test 
implemented by the provided function.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## find

Returns a value in the typed array, if an element satisfies the provided testing function.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> 'T option
```

## findWithIndex

Returns a value in the typed array, if an element satisfies the provided testing function.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> 'T option
```

## findWithIndexArray

Returns a value in the typed array, if an element satisfies the provided testing function.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> 'T option
```

## findIndex

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function. Otherwise -1 is returned.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> int
```

## findIndexWithIndex

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function. Otherwise -1 is returned.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> int
```

## findIndexWithIndexArray

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function. Otherwise -1 is returned.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> int
```

## tryFindIndex

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> int option
```

## tryFindIndexWithIndex

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> int option
```

## tryFindIndexWithIndexArray

Returns an index in the typed array, if an element in the typed array satisfies the 
provided testing function.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> int option
```

## forEach

Executes a provided function once per array element.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> unit
```

## forEachWithIndex

Executes a provided function once per array element.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> unit
```

## forEachWithIndexArray

Executes a provided function once per array element.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> unit
```

## includes

Determines whether a typed array includes a certain element, 
returning true or false as appropriate.

Signature:
```fsharp
(searchElement: 'T) -> (ta: TypedArray<'T>) -> bool
```

## includesFromIndex

Creates a new typed array with all elements that pass the test 
implemented by the provided function.

Signature:
```fsharp
(searchElement: 'T) -> (fromIndex: int) -> (ta: TypedArray<'T>) -> bool
```

## indexOf

Returns the first index at which a given element can be found 
in the typed array, or -1 if it is not present.

Signature:
```fsharp
(searchElement: 'T) -> (ta: TypedArray<'T>) -> int
```

## indexOfFromIndex

Returns the first index at which a given element can be found 
in the typed array, or -1 if it is not present.

Signature:
```fsharp
(searchElement: 'T) -> (fromIndex: int) -> (ta: TypedArray<'T>) -> int
```

## tryIndexOf

Tries to return the first index at which a given element can be found 
in the typed array.

Signature:
```fsharp
(searchElement: 'T) -> (ta: TypedArray<'T>) -> int option
```

## tryIndexOfFromIndex

Tries to return the first index at which a given element can be found 
in the typed array.

Signature:
```fsharp
(searchElement: 'T) -> (fromIndex: int) -> (ta: TypedArray<'T>) -> int option
```

## join

Joins all elements of an array into a string.

Signature:
```fsharp
(sep: string) -> (ta: TypedArray<'T>) -> string
```

## keys

The keys for each index in the array.

Signature:
```fsharp
(ta: TypedArray<'T>) -> seq<int>
```

## lastIndexOf

Returns the last index at which a given element can be found 
in the typed array, or -1 if it is not present.

Signature:
```fsharp
(searchElement: 'T) -> (ta: TypedArray<'T>) -> int
```

## lastIndexOfFromIndex

Returns the last index at which a given element can be found 
in the typed array, or -1 if it is not present.

Signature:
```fsharp
(searchElement: 'T) -> (fromIndex: int) -> (ta: TypedArray<'T>) -> int
```

## tryLastIndexOf

Tries to return the last index at which a given element can be found 
in the typed array.

Signature:
```fsharp
(searchElement: 'T) -> (ta: TypedArray<'T>) -> int option
```

## tryLastIndexOfFromIndex

Tries to return the last index at which a given element can be found 
in the typed array.

Signature:
```fsharp
(searchElement: 'T) -> (fromIndex: int) -> (ta: TypedArray<'T>) -> int option
```

## map

Creates a new typed array with the results of calling a provided 
function on every element in this typed array.

Signature:
```fsharp
(f: 'T -> 'U) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## mapWithIndex

Creates a new typed array with the results of calling a provided 
function on every element in this typed array.

Signature:
```fsharp
(f: 'T -> int -> 'U) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## mapWithIndexArray

Creates a new typed array with the results of calling a provided 
function on every element in this typed array.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> 'U) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## reduce

Applies a function against an accumulator and each value of the typed 
array (from left-to-right) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## reduceWithIndex

Applies a function against an accumulator and each value of the typed 
array (from left-to-right) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> int -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## reduceWithIndexArray

Applies a function against an accumulator and each value of the typed 
array (from left-to-right) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) 
    -> TypedArray<'T>
```

## reduceRight

Applies a function against an accumulator and each value of the typed 
array (from right-to-left) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## reduceRightWithIndex

Creates a new typed array with the results of calling a provided 
array (from right-to-left) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> int -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## reduceRightWithIndexArray

Creates a new typed array with the results of calling a provided 
array (from right-to-left) has to reduce it to a single value.

Signature:
```fsharp
(f: 'State -> 'T -> int -> TypedArray<'T> -> 'State) -> (state: 'State) -> (ta: TypedArray<'T>) 
    -> TypedArray<'T>
```

## reverse

Reverses a typed array in place.

Signature:
```fsharp
(ta: TypedArray<'T>) -> TypedArray<'T>
```

## setArray

Stores multiple values in the typed array, reading input values from a specified array.

Signature:
```fsharp
(source: System.Array) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## setArrayOffset

Stores multiple values in the typed array, reading input values from a specified array.

Signature:
```fsharp
(source: System.Array) -> (offset: int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## setTypedArray

Stores multiple values in the typed array, reading input values from a specified array.

Signature:
```fsharp
(source: #JS.TypedArray) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## setTypedArrayOffset

Stores multiple values in the typed array, reading input values from a specified array.

Signature:
```fsharp
(source: #JS.TypedArray) -> (offset: int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## setSeq

Stores multiple values in the typed array, reading input values from a given sequence.

Signature:
```fsharp
(source: seq<'T>) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## setSeqOffset

Stores multiple values in the typed array, reading input values from a given sequence.

Signature:
```fsharp
(source: seq<'T>) -> (offset: int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## slice

Returns a shallow copy of a portion of a typed array into a new typed array object. 

Signature:
```fsharp
(begin': int) -> (end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## sliceBegin

Returns a shallow copy of a portion of a typed array into a new typed array object. 

Signature:
```fsharp
(begin': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## sliceEnd

Returns a shallow copy of a portion of a typed array into a new typed array object. 

Signature:
```fsharp
(end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## some

Tests whether some element in the typed array passes the test implemented by 
the provided function.

Signature:
```fsharp
(f: 'T -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## someWithIndex

Tests whether some element in the typed array passes the test implemented by 
the provided function.

Signature:
```fsharp
(f: 'T -> int -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## someWithIndexArray

Tests whether some element in the typed array passes the test implemented by 
the provided function.

Signature:
```fsharp
(f: 'T -> int -> TypedArray<'T> -> bool) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## sort

Sorts the elements of a typed array numerically in place and returns the typed array. 

Signature:
```fsharp
(ta: TypedArray<'T>) -> TypedArray<'T>
```

## sortBy

Sorts the elements of a typed array numerically in place using the provided function
and returns the typed array. 

Signature:
```fsharp
(sortFunction: 'T -> 'T -> int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## subarray

Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
as for this TypedArray object. 

The begin offset is inclusive and the end offset is exclusive.

Signature:
```fsharp
(begin': int) -> (end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## subarrayBegin

Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
as for this TypedArray object. 

The begin offset is inclusive and the end offset is exclusive.

Signature:
```fsharp
(begin': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## subarrayEnd

Returns a new TypedArray on the same ArrayBuffer store and with the same element types 
as for this TypedArray object. 

The begin offset is inclusive and the end offset is exclusive.

Signature:
```fsharp
(end': int) -> (ta: TypedArray<'T>) -> TypedArray<'T>
```

## values

Returns a sequence that contains the values for each index in the array.

Signature:
```fsharp
(ta: TypedArray<'T>) -> seq<'T>
```
