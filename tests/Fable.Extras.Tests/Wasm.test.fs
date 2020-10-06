module WasmTests

open Fable.Extras
open Fable.Extras.WebAssembly
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest
open Node.Api
    
type WasmFableExports =
    abstract add: int * int -> int
    
Jest.describe("Wasm", fun () ->
    let mutable wasmInstance : WasmFableExports option = None
    
    Jest.beforeAll(promise {
        let! wasmBin =
            JSe.Promise(fun resolve reject ->
                let wasmPath = path.resolve(__dirname, "..", "..", "..", "tests", "Fable.Extras.Tests", "wasm", "fable_wasm_bg.wasm")
                
                fs.readFile(wasmPath, (fun maybeError buffer -> 
                    match maybeError with
                    | None -> resolve (JSe.ArrayBuffer buffer)
                    | Some err -> reject (System.Exception err.message)
                ))
            )
        
        let! waModule = WA.instantiate<WasmFableExports>(wasmBin)
            
        wasmInstance <- Some waModule.Instance.Exports
    })

    Jest.test("Can add from wasm", fun () ->
        let add = wasmInstance |> Option.map (fun m -> m.add) |> Option.get

        Jest.expect(add(1,2)).toBe(3)
    )
)

type WasmFableIE =
    abstract add: int -> int

type WasmFableII =
    { getAddValue: unit -> int }

Jest.describe("Wasm with Import", fun () ->
    let mutable wasmInstance : WasmFableIE option = None
    
    Jest.beforeAll(promise {
        let! wasmBin =
            JSe.Promise(fun resolve reject ->
                let wasmPath = path.resolve(__dirname, "..", "..", "..", "tests", "Fable.Extras.Tests", "wasm", "fable_wasm_import_bg.wasm")
                
                fs.readFile(wasmPath, (fun maybeError buffer -> 
                    match maybeError with
                    | None -> resolve (JSe.ArrayBuffer buffer)
                    | Some err -> reject (System.Exception err.message)
                ))
            )
        
        let! waModule = WA.instantiate<WasmFableII,WasmFableIE>(wasmBin, WA.ImportEnv { getAddValue = fun () -> 5 })
            
        wasmInstance <- Some waModule.Instance.Exports
    })

    Jest.test("Can add from wasm", fun () ->
        let add = wasmInstance |> Option.map (fun m -> m.add) |> Option.get

        Jest.expect(add 1).toBe(6)
    )
)
