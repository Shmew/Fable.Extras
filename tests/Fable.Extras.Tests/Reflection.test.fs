module ReflectionTests

open Fable.Core.Experimental
open Fable.Extras
open Fable.Extras.Reflection
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

type WeakType = { Key: int }

Jest.describe("Reflect", fun () ->
    Jest.test("Can apply", fun () ->
        let add x y = x + y
        let op : int = JSe.Reflect.apply2 add JSe.this' [|1;2|]

        Jest.expect(op).toBe(3)
    )

    Jest.test("Can set an Object", fun () ->
        let o = { Key = 2 }
        
        Jest.expect(JSe.Reflect.has o (nameof o.Key)).toBe(true)
        Jest.expect(JSe.Reflect.set o (nameof o.Key) 1).toBe(true)
        Jest.expect(JSe.Reflect.get<WeakType,int> o (nameof o.Key)).toBe(1)
    )
)

Jest.describe("Proxy", fun () ->
    Jest.test("Can intercept a single artity function", fun () ->
        let addOne (x: int) = x + 1

        let proxy =
            JSe.ProxyHandler<int -> int>()
            |> JSe.ProxyHandler.setApply (fun f _ args -> JSe.apply f args |> (+) 1)
            |> JSe.Proxy.create addOne
            
        Jest.expect(addOne 1).toBe(2)
        Jest.expect(proxy 2).toBe(4)
    )

    Jest.test("Can intercept a function", fun () ->
        let add (x: int) (y: int) = x + y

        let proxy =
            JSe.ProxyHandler<int -> int -> int>()
            |> JSe.ProxyHandler.setApply (fun f _ args -> JSe.apply f args |> (+) 1)
            |> JSe.Proxy.create add
            
        Jest.expect(add 1 2).toBe(3)
        Jest.expect(proxy 1 2).toBe(4)
    )
)