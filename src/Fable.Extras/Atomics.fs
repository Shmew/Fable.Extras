namespace Fable.Extras.Atomics

open Fable.Core
open Fable.Extras
open FSharp.Core

[<Erase;RequireQualifiedAccess>]
module JSe =
    /// Used to represent a generic, fixed-length raw binary data buffer, 
    /// similar to the ArrayBuffer object, but in a way that they can be 
    /// used to create views on shared memory. 
    ///
    /// Unlike an ArrayBuffer, a SharedArrayBuffer cannot become detached.
    ///
    /// The session must be in a secure context to use this construct.
    /// You can validate this by calling isSecureContext.
    [<Global>]
    type SharedArrayBuffer (byteLength: int) =
        /// The read-only size, in bytes, of the ArrayBuffer.
        [<Emit("$0.byteLength")>]
        member _.ByteLength: int = jsNative
        
        /// Returns a new ArrayBuffer whose contents are a copy of this ArrayBuffer's bytes from begin, 
        /// inclusive, up to end, exclusive.
        [<Emit("$0.slice($1...)")>]
        member _.Slice (?begin': int, ?end': int) : JS.ArrayBuffer = jsNative

    [<RequireQualifiedAccess;StringEnum(CaseRules.KebabCase)>]
    type AtomicWait =
        | Ok
        | NotEqual
        | TimedOut

    /// Provides atomic operations as static functions.
    ///
    /// Only works on (u)int types.
    [<Erase>]
    type Atomics =
        /// Adds a given value at a given position in the array and returns the 
        /// old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the 
        /// modified value is written back.
        [<Emit("Atomics.add($0, $1, $2)")>]
        static member Add (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative

        /// Computes a bitwise AND with a given value at a given position in the 
        /// array, and returns the old value at that position. This atomic operation 
        /// guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.and($0, $1, $2)")>]
        static member And (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative
        
        /// Exchanges a given replacement value at a given position in the array, if a given expected 
        /// value equals the old value. It returns the old value at that position whether it was equal 
        /// to the expected value or not. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.compareExchange($0, $1, $2, $3)")>]
        static member CompareExchange (typedArray: JS.TypedArray<'T>, index: int, expectedValue: 'T, replacementValue: 'T) : 'T = jsNative        

        /// Stores a given value at a given position in the array and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens between the read of the old value 
        /// and the write of the new value.
        [<Emit("Atomics.exchange($0, $1, $2)")>]
        static member Exchange (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative

        /// Used to determine whether to use locks or atomic operations. 
        ///
        /// It returns true, if the given size is one of the BYTES_PER_ELEMENT property of integer 
        /// TypedArray types.
        [<Emit("Atomics.isLockFree($0, $1, $2)")>]
        static member IsLockFree (size: int) : bool = jsNative

        /// Returns a value at a given position in the array.
        [<Emit("Atomics.load($0, $1)")>]
        static member Load (typedArray: JS.TypedArray<'T>, index: int) : 'T = jsNative

        /// Notifies up some agents that are sleeping in the wait queue.
        ///
        /// Returns the number of woken up agents.
        [<Emit("Atomics.notify($0, $1, $2)")>]
        static member Notify (typedArray: JS.TypedArray<int>, index: int, count: int) : int = jsNative
        
        /// Computes a bitwise OR with a given value at a given position in the array, and returns the old 
        /// value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.or($0, $1, $2)")>]
        static member Or (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative
        
        /// Stores a given value at the given position in the array and returns that value.
        ///
        /// Returns the value that was stored
        [<Emit("Atomics.store($0, $1, $2)")>]
        static member Store (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative

        /// Substracts a given value at a given position in the array and returns the old value at that 
        /// position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.sub($0, $1, $2)")>]
        static member Sub (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative
        
        /// Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup or a timeout.
        [<Emit("Atomics.wait($0...)")>]
        static member Wait (typedArray: JS.TypedArray<int>, index: int, value: int, ?timeout: int) : AtomicWait = jsNative
        
        /// Computes a bitwise XOR with a given value at a given position in the array, and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.xor($0, $1, $2)")>]
        static member Xor (typedArray: JS.TypedArray<'T>, index: int, value: 'T) : 'T = jsNative
        
    /// Provides atomic operations as static functions.
    ///
    /// Only works on (u)int types.
    [<Erase;RequireQualifiedAccess>]
    module Atomics =
        /// Adds a given value at a given position in the array and returns the 
        /// old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the 
        /// modified value is written back.
        let inline add (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) = 
            Atomics.Add(typedArray, index, value) |> ignore 
            typedArray

        /// Computes a bitwise AND with a given value at a given position in the 
        /// array, and returns the old value at that position. This atomic operation 
        /// guarantees that no other write happens until the modified value is written back.
        let inline and' (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.And(typedArray, index, value) |> ignore
            typedArray
        
        /// Exchanges a given replacement value at a given position in the array, if a given expected 
        /// value equals the old value. It returns the old value at that position whether it was equal 
        /// to the expected value or not. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline compareExchange (index: int) (expectedValue: 'T) (replacementValue: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.CompareExchange(typedArray, index, expectedValue, replacementValue) |> ignore
            typedArray

        /// Stores a given value at a given position in the array and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens between the read of the old value 
        /// and the write of the new value.
        let inline exchange (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.Exchange(typedArray, index, value) |> ignore
            typedArray

        /// Returns a value at a given position in the array.
        let inline load (index: int) (typedArray: JS.TypedArray<'T>) = 
            Atomics.Load(typedArray, index)

        /// Notifies up some agents that are sleeping in the wait queue.
        ///
        /// Returns the number of woken up agents.
        let inline notify (index: int) (count: int) (typedArray: JS.TypedArray<int>) = 
            Atomics.Notify(typedArray, index, count)
        
        /// Computes a bitwise OR with a given value at a given position in the array, and returns the old 
        /// value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline or' (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.Or(typedArray, index, value) |> ignore
            typedArray
        
        /// Stores a given value at the given position in the array and returns that value.
        let inline store (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.Store(typedArray, index, value) |> ignore
            typedArray

        /// Substracts a given value at a given position in the array and returns the old value at that 
        /// position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline sub (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.Sub(typedArray, index, value) |> ignore
            typedArray
        
        /// Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup.
        let inline wait (index: int) (value: int) (typedArray: JS.TypedArray<int>) =
            Atomics.Wait(typedArray, index, value)
        
        /// Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup or a timeout.
        let inline waitTimeout (index: int) (value: int) (timeout: int) (typedArray: JS.TypedArray<int>) =
            Atomics.Wait(typedArray, index, value, timeout = timeout)
        
        /// Computes a bitwise XOR with a given value at a given position in the array, and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline xor (index: int) (value: 'T) (typedArray: JS.TypedArray<'T>) =
            Atomics.Xor(typedArray, index, value) |> ignore
            typedArray
