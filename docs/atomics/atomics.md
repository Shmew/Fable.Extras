# Atomics

Provides atomic operations as static functions.

Only works on (u)int types.

```fsharp
type Atomics =
    /// Adds a given value at a given position in the array and returns the 
    /// old value at that position. 
    ///
    /// This atomic operation guarantees that no other write happens until the 
    /// modified value is written back.
    static member Add (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType

    /// Computes a bitwise AND with a given value at a given position in the 
    /// array, and returns the old value at that position. This atomic operation 
    /// guarantees that no other write happens until the modified value is written back.
    static member And (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType
        
    /// Exchanges a given replacement value at a given position in the array, if a given expected 
    /// value equals the old value. It returns the old value at that position whether it was equal 
    /// to the expected value or not. 
    ///
    /// This atomic operation guarantees that no other write happens until the modified value is 
    /// written back.
    static member CompareExchange (typedArray: 'TypedArray, index: int, expectedValue: 'IntType, 
                                   replacementValue: 'IntType) : 'IntType

    /// Stores a given value at a given position in the array and returns the old value at that 
    /// position. 
    ///
    /// This atomic operation guarantees that no other write happens between the read of the 
    /// old value and the write of the new value.
    static member Exchange (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType

    /// Used to determine whether to use locks or atomic operations. 
    ///
    /// It returns true, if the given size is one of the BYTES_PER_ELEMENT property of integer 
    /// TypedArray types.
    static member IsLockFree (size: int) : bool

    /// Returns a value at a given position in the array.
    static member Load (typedArray: 'TypedArray, index: int) : 'IntType

    /// Notifies up some agents that are sleeping in the wait queue.
    ///
    /// Returns the number of woken up agents.
    static member Notify (typedArray: JS.TypedArray<int>, index: int, count: int) : int
        
    /// Computes a bitwise OR with a given value at a given position in the array, and returns 
    /// the old value at that position. 
    ///
    /// This atomic operation guarantees that no other write happens until the modified value is 
    /// written back.
    static member Or (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType
        
    /// Stores a given value at the given position in the array and returns that value.
    ///
    /// Returns the value that was stored
    static member Store (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType

    /// Substracts a given value at a given position in the array and returns the old value at that 
    /// position. 
    ///
    /// This atomic operation guarantees that no other write happens until the modified value is 
    /// written back.
    static member Sub (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType
        
    /// Verifies that a given position in an Int32Array still contains a given value and if so 
    /// sleeps, awaiting a wakeup or a timeout.
    static member Wait (typedArray: JS.TypedArray<int>, index: int, value: int, 
                        ?timeout: int) : AtomicWait
        
    /// Computes a bitwise XOR with a given value at a given position in the array, and returns 
    /// the old value at that position. 
    ///
    /// This atomic operation guarantees that no other write happens until the modified value is 
    /// written back.
    static member Xor (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType
```

You will notice that the method signatures look a bit odd where it specifies `'TypedArray` and `'IntType`.
This is a necessity in order to ensure that the [TypedArray](/extras/typed-array) that is passed in
is valid at compile time if the numeric type matches one of the following:
* int8
* unit8
* int16
* uint16
* int32
* uint32
* int64
* uint64

## AtomicWait

```fsharp
type AtomicWait =
    | Ok
    | NotEqual
    | TimedOut
```

The functions outlined below are located in the `Atomics` module.

## add

Adds a given value at a given position in the array and returns the 
old value at that position. 

This atomic operation guarantees that no other write happens until the 
modified value is written back.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```

## and'

Computes a bitwise AND with a given value at a given position in the 
array, and returns the old value at that position. This atomic operation 
guarantees that no other write happens until the modified value is written back.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```

## compareExchange

Exchanges a given replacement value at a given position in the array, if a given expected 
value equals the old value. It returns the old value at that position whether it was equal 
to the expected value or not. 

This atomic operation guarantees that no other write happens until the modified value is written back.

Signature:
```fsharp
(index: int) -> (expectedValue: 'IntType) -> (replacementValue: 'IntType) 
    -> (typedArray: 'TypedArray) -> 'IntType
```

## exchange

Stores a given value at a given position in the array and returns the old value at that position. 

This atomic operation guarantees that no other write happens between the read of the old value 
and the write of the new value.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'IntType
```

## load

Returns a value at a given position in the array.

Signature:
```fsharp
(index: int) -> (typedArray: 'TypedArray) -> 'IntType
```

## notify

Notifies up some agents that are sleeping in the wait queue.

Returns the number of woken up agents.

Signature:
```fsharp
(index: int) -> (count: int) -> (typedArray: #JS.TypedArray<int>) -> int
```

## or'

Computes a bitwise OR with a given value at a given position in the array, and returns the old 
value at that position. 

This atomic operation guarantees that no other write happens until the modified value is written back.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```

## store

Stores a given value at the given position in the array and returns that value.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```

## sub

Substracts a given value at a given position in the array and returns the old value at that 
position. 

This atomic operation guarantees that no other write happens until the modified value is written back.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```

## wait

Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup.

Signature:
```fsharp
(index: int) -> (value: int) -> (typedArray: #JS.TypedArray<int>) -> AtomicWait
```

## waitTimeout

Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup.

Signature:
```fsharp
(index: int) -> (value: int) -> (timeout: int) -> (typedArray: #JS.TypedArray<int>) -> AtomicWait
```

## xor

Computes a bitwise XOR with a given value at a given position in the array, and returns the old value at that position. 

This atomic operation guarantees that no other write happens until the modified value is written back.

Signature:
```fsharp
(index: int) -> (value: 'IntType) -> (typedArray: 'TypedArray) -> 'TypedArray
```
