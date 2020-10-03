module AtomicsTests

open Fable.Extras
open Fable.Extras.Atomics
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

Jest.describe("Atomics", fun () ->
    Jest.test("Can add", fun () ->
        let buffer = JSe.SharedArrayBuffer(10 * JSe.Int32Array.bytesPerElement)
        let ta = JSe.Int32Array(buffer)

        Jest.expect(JSe.Atomics.add 0 30 ta |> JSe.TypedArray.values).toEqual(expect.arrayContaining [30; yield! List.init 9 (fun _ -> 0)])
    )

    Jest.test("Can load", fun () ->
        let buffer = JSe.SharedArrayBuffer(10 * JSe.Int32Array.bytesPerElement)
        let ta = JSe.Int32Array(buffer)
        
        Jest.expect(JSe.Atomics.add 0 1 ta |> JSe.Atomics.add 0 2 |> JSe.Atomics.load 0).toBe(3)
    )
    
    Jest.test("Can store", fun () ->
        let buffer = JSe.SharedArrayBuffer(10 * JSe.Int32Array.bytesPerElement)
        let ta = JSe.Int32Array(buffer)
        
        Jest.expect(JSe.Atomics.store 0 1 ta |> JSe.Atomics.load 0).toBe(1)
    )

    Jest.test("Can use bitwise operations", fun () ->
        let buffer = JSe.SharedArrayBuffer(10 * JSe.Int32Array.bytesPerElement)
        let ta = JSe.Int32Array(buffer)
        
        Jest.expect(JSe.Atomics.store 0 1 ta |> JSe.Atomics.and' 0 2 |> JSe.Atomics.load 0).toBe(0)
    )
)
