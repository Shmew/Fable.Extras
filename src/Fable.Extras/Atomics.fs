namespace Fable.Extras.Atomics

open Fable.Core
open FSharp.Core
open System.ComponentModel

[<Erase;RequireQualifiedAccess>]
module JSe =
    [<EditorBrowsable(EditorBrowsableState.Never);Erase>]
    module Internal =
        [<EditorBrowsable(EditorBrowsableState.Never);Erase>]
        type ToInt =
            static member ToInt (x: JS.TypedArray<int8>) = unbox<int8> x
            static member ToInt (x: JS.TypedArray<uint8>) = unbox<uint8> x
            static member ToInt (x: JS.TypedArray<int16>) = unbox<int16> x
            static member ToInt (x: JS.TypedArray<uint16>) = unbox<uint16> x
            static member ToInt (x: JS.TypedArray<int>) = unbox<int> x
            static member ToInt (x: JS.TypedArray<uint32>) = unbox<uint32> x
            static member ToInt (x: JS.TypedArray<bigint>) = unbox<bigint> x

            static member inline Invoke (x: 'TypedArray) : 'IntType =
                let inline call2 (_: ^a, b: ^b) =
                    ((^a or ^b) : (static member ToInt : _ -> _) b)
                call2 (Unchecked.defaultof<ToInt>, x)
        
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        let inline whenInt x y = let _ = if false then ToInt.Invoke x else y in ()

    open Internal

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

        interface JS.ArrayBuffer with
            member this.byteLength = this.ByteLength
            member this.slice (begin', end') = this.Slice(begin', ?end' = end')

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
        static member inline Add (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType =
            whenInt typedArray value
            jsNative

        /// Computes a bitwise AND with a given value at a given position in the 
        /// array, and returns the old value at that position. This atomic operation 
        /// guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.and($0, $1, $2)")>]
        static member inline And (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative
        
        /// Exchanges a given replacement value at a given position in the array, if a given expected 
        /// value equals the old value. It returns the old value at that position whether it was equal 
        /// to the expected value or not. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.compareExchange($0, $1, $2, $3)")>]
        static member inline CompareExchange (typedArray: 'TypedArray, index: int, expectedValue: 'IntType, replacementValue: 'IntType) : 'IntType = 
            whenInt typedArray expectedValue
            jsNative

        /// Stores a given value at a given position in the array and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens between the read of the old value 
        /// and the write of the new value.
        [<Emit("Atomics.exchange($0, $1, $2)")>]
        static member inline Exchange (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative

        /// Used to determine whether to use locks or atomic operations. 
        ///
        /// It returns true, if the given size is one of the BYTES_PER_ELEMENT property of integer 
        /// TypedArray types.
        [<Emit("Atomics.isLockFree($0, $1, $2)")>]
        static member IsLockFree (size: int) : bool = jsNative

        /// Returns a value at a given position in the array.
        [<Emit("Atomics.load($0, $1)")>]
        static member inline Load (typedArray: 'TypedArray, index: int) : 'IntType = 
            whenInt typedArray (unbox<'IntType>())
            jsNative

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
        static member inline Or (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative
        
        /// Stores a given value at the given position in the array and returns that value.
        ///
        /// Returns the value that was stored
        [<Emit("Atomics.store($0, $1, $2)")>]
        static member inline Store (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative

        /// Substracts a given value at a given position in the array and returns the old value at that 
        /// position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.sub($0, $1, $2)")>]
        static member inline Sub (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative
        
        /// Verifies that a given position in an Int32Array still contains a given value and if so sleeps, awaiting a wakeup or a timeout.
        [<Emit("Atomics.wait($0...)")>]
        static member Wait (typedArray: JS.TypedArray<int>, index: int, value: int, ?timeout: int) : AtomicWait = jsNative
        
        /// Computes a bitwise XOR with a given value at a given position in the array, and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        [<Emit("Atomics.xor($0, $1, $2)")>]
        static member inline Xor (typedArray: 'TypedArray, index: int, value: 'IntType) : 'IntType = 
            whenInt typedArray value
            jsNative
        
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
        let inline add (index: int) (value: 'IntType) (typedArray: 'TypedArray) = 
            Atomics.Add(typedArray, index, value) |> ignore 
            typedArray

        /// Computes a bitwise AND with a given value at a given position in the 
        /// array, and returns the old value at that position. This atomic operation 
        /// guarantees that no other write happens until the modified value is written back.
        let inline and' (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
            Atomics.And(typedArray, index, value) |> ignore
            typedArray
        
        /// Exchanges a given replacement value at a given position in the array, if a given expected 
        /// value equals the old value. It returns the old value at that position whether it was equal 
        /// to the expected value or not. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline compareExchange (index: int) (expectedValue: 'IntType) (replacementValue: 'IntType) (typedArray: 'TypedArray) =
            Atomics.CompareExchange(typedArray, index, expectedValue, replacementValue)

        /// Stores a given value at a given position in the array and returns the old value at that position. 
        ///
        /// This atomic operation guarantees that no other write happens between the read of the old value 
        /// and the write of the new value.
        let inline exchange (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
            Atomics.Exchange(typedArray, index, value)

        /// Returns a value at a given position in the array.
        let inline load (index: int) (typedArray: 'TypedArray) = 
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
        let inline or' (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
            Atomics.Or(typedArray, index, value) |> ignore
            typedArray
        
        /// Stores a given value at the given position in the array and returns that value.
        let inline store (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
            Atomics.Store(typedArray, index, value) |> ignore
            typedArray

        /// Substracts a given value at a given position in the array and returns the old value at that 
        /// position. 
        ///
        /// This atomic operation guarantees that no other write happens until the modified value is written back.
        let inline sub (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
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
        let inline xor (index: int) (value: 'IntType) (typedArray: 'TypedArray) =
            Atomics.Xor(typedArray, index, value) |> ignore
            typedArray
