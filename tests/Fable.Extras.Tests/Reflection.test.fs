module ReflectionTests

open Fable.Core.Experimental
open Fable.Extras
open Fable.Extras.Reflection
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

type WeakType = { Key: int }

type MutableObject () =
    let mutable key = 0

    member _.Key
        with get () = key
        and set x = key <- x

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

    // Not currently being compiled correctly see https://github.com/fable-compiler/Fable/issues/2193
    Jest.test.skip("Can intercept a function", fun () ->
        let add (x: int) (y: int) = x + y

        let proxy =
            JSe.ProxyHandler<int -> int -> int>()
            |> JSe.ProxyHandler.setApply (fun f _ args -> JSe.apply f args |> (+) 1)
            |> JSe.Proxy.create add
            
        Jest.expect(add 1 2).toBe(3)
        Jest.expect(proxy 1 2).toBe(4)
    )

    Jest.test("Can intercept an object get", fun () ->
        let o = MutableObject()

        let proxy =
            JSe.ProxyHandler<MutableObject>()
            |> JSe.ProxyHandler.setGet (fun f propName _ -> if propName = "key" then 2 else f.Key)
            |> JSe.Proxy.create (MutableObject())

        Jest.expect(o.Key).toBe(0)
        Jest.expect(proxy.Key).toBe(2)
    )

    Jest.test("Can intercept an object set", fun () ->
        let o = MutableObject()
            
        let proxy =
            JSe.ProxyHandler<MutableObject>()
            |> JSe.ProxyHandler.setSet (fun f propName _ _ -> 
                if propName = "key" then
                    f.Key <- 10 
                    true
                else false)
            |> JSe.Proxy.create (MutableObject())

        Jest.expect(o.Key).toBe(0)
        Jest.expect(proxy.Key).toBe(0)
        
        o.Key <- 5
        proxy.Key <- 5
        
        Jest.expect(o.Key).toBe(5)
        Jest.expect(proxy.Key).toBe(10)
    )
)

Jest.describe("RevocableProxy", fun () ->
    Jest.test("Can intercept a single artity function", fun () ->
        let addOne (x: int) = x + 1

        let revocable =
            JSe.ProxyHandler<int -> int>()
            |> JSe.ProxyHandler.setApply (fun f _ args -> JSe.apply f args |> (+) 1)
            |> JSe.Proxy.createRevocable addOne
            
        Jest.expect(addOne 1).toBe(2)
        Jest.expect(revocable.proxy 2).toBe(4)

        revocable.revoke()

        Jest.expect(fun () -> revocable.proxy 2).toThrow()
    )

    // Not currently being compiled correctly see https://github.com/fable-compiler/Fable/issues/2193
    Jest.test.skip("Can intercept a function", fun () ->
        let add (x: int) (y: int) = x + y

        let revocable =
            JSe.ProxyHandler<int -> int -> int>()
            |> JSe.ProxyHandler.setApply (fun f _ args -> JSe.apply f args |> (+) 1)
            |> JSe.Proxy.createRevocable add
            
        Jest.expect(add 1 2).toBe(3)
        Jest.expect(revocable.proxy 1 2).toBe(4)

        revocable.revoke()

        Jest.expect(fun () -> revocable.proxy 1 2).toThrow()
    )

    Jest.test("Can intercept an object get", fun () ->
        let o = MutableObject()

        let revocable =
            JSe.ProxyHandler<MutableObject>()
            |> JSe.ProxyHandler.setGet (fun f propName _ -> if propName = "key" then 2 else f.Key)
            |> JSe.Proxy.createRevocable (MutableObject())

        Jest.expect(o.Key).toBe(0)
        Jest.expect(revocable.proxy.Key).toBe(2)

        revocable.revoke()

        Jest.expect(fun () -> revocable.proxy.Key).toThrow()
    )

    Jest.test("Can intercept an object set", fun () ->
        let o = MutableObject()
            
        let revocable =
            JSe.ProxyHandler<MutableObject>()
            |> JSe.ProxyHandler.setSet (fun f propName _ _ -> 
                if propName = "key" then
                    f.Key <- 10 
                    true
                else false)
            |> JSe.Proxy.createRevocable (MutableObject())

        Jest.expect(o.Key).toBe(0)
        Jest.expect(revocable.proxy.Key).toBe(0)
        
        o.Key <- 5
        revocable.proxy.Key <- 5
        
        Jest.expect(o.Key).toBe(5)
        Jest.expect(revocable.proxy.Key).toBe(10)

        revocable.revoke()

        Jest.expect(fun () -> revocable.proxy.Key <- 5).toThrow()
    )
)
