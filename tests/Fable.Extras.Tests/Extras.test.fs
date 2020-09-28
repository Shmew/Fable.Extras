module ExtrasTests

open Fable.Extras
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

Jest.describe("PropertyDescriptor", fun () ->
    Jest.test("Can create an empty PropertyDescriptor", fun () ->
        Jest.expect(JS.PropertyDescriptor()).toBeDefined()
    )

    Jest.test("Can set properties in a PropertyDescriptor", fun () ->
        let pd = JS.PropertyDescriptor() |> JS.PropertyDescriptor.Set.value 1
        
        Jest.expect(pd.value).toHaveProperty(["value"])
        Jest.expect(pd.value).toBe(1)
    )
)

Jest.describe("Object", fun () ->
    Jest.test("Can create an empty JS Object", fun () ->
        Jest.expect(JS.Object.create(None : int option)).toBeDefined()
    )

    Jest.test("Can set properties in a PropertyDescriptor", fun () ->
        let o = JS.Object.create({| value = 1 |})

        Jest.expect(JS.Object.hasOwnProperty "value" o).toBeTruthy()
        Jest.expect(JS.Object.valueOf(o)).toBe(1)
    )
)

Jest.describe("Map", fun () ->
    Jest.test("Can create an empty Map", fun () ->
        Jest.expect(JS.Map<int,int>()).toBeDefined()
    )

    Jest.test("Can set properties in a PropertyDescriptor", fun () ->
        let m = JS.Map<int,int>([1,2;3,4])
        
        Jest.expect(JS.Map.get 1 m).toBe(Some 2)
    )
)
