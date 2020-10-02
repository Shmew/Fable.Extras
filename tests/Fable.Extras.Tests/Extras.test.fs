module ExtrasTests

open Fable.Core.JsInterop
open Fable.Extras
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

type WeakType = { Key: int }

Jest.describe("Map", fun () ->
    Jest.test("Can create an empty Map", fun () ->
        Jest.expect(JSe.Map<int,int>()).toBeDefined()
    )

    Jest.test("Can get the size", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(m.Size).toBe(2)
    )

    Jest.test("Can clear", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(m.Size).toBe(2)

        m.Clear()

        Jest.expect(m.Size).toBe(0)
    )

    Jest.test("Can delete a key", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(m.Size).toBe(2)

        Jest.expect(m.Delete(1)).toBe(true)
        Jest.expect(m.Delete(40)).toBe(false)

        Jest.expect(m.Size).toBe(1)
    )

    Jest.test("Can get the entries", fun () ->
        let entries = JSe.Map<int,int>([1,2;3,4]).Entries()
        
        Jest.expect(entries).toHaveLength(2)
        Jest.expect(entries).toEqual(expect.arrayContaining [(1,2);(3,4)])
    )

    Jest.test("Can forEach", fun () ->
        let mutable count = 0

        JSe.Map<int,int>([1,2;3,4])
        |> JSe.Map.forEach (fun v _ _ -> count <- count + v)
        
        Jest.expect(count).toBe(6)
    )

    Jest.test("Can get a value", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(JSe.Map.get 1 m).toBe(Some 2)
        Jest.expect(JSe.Map.get 40 m).toBe(None)
    )

    Jest.test("Can set a value", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(JSe.Map.get 5 m).toBe(None)

        Jest.expect(m.Set(5, 6).Get(5)).toBe(Some 6)
    )
    
    Jest.test("Can check for a key", fun () ->
        let m = JSe.Map<int,int>([1,2;3,4])
        
        Jest.expect(JSe.Map.has 1 m).toBe(true)
        Jest.expect(JSe.Map.has 40 m).toBe(false)
    )

    Jest.test("Can get the keys", fun () ->
        let keys = JSe.Map<int,int>([1,2;3,4]).Keys()
        
        Jest.expect(keys).toHaveLength(2)
        Jest.expect(keys).toContain(1)
        Jest.expect(keys).toContain(3)
    )

    Jest.test("Can get the values", fun () ->
        let values = JSe.Map<int,int>([1,2;3,4]).Values()
        
        Jest.expect(values).toHaveLength(2)
        Jest.expect(values).toContain(2)
        Jest.expect(values).toContain(4)
    )
)

Jest.describe("WeakMap", fun () ->
    Jest.test("Can create an empty WeakMap", fun () ->
        Jest.expect(JSe.WeakMap<WeakType,int>()).toBeDefined()
    )

    Jest.test("Can get a value", fun () ->
        let key1 = { Key = 1 }
        let key2 = { Key = 3 }

        let m = JSe.WeakMap<WeakType,int>([key1,2;key2,4])
        
        Jest.expect(JSe.WeakMap.get key1 m).toBe(Some 2)
        Jest.expect(JSe.WeakMap.get { Key = 40 } m).toBe(None)
    )
    
    Jest.test("Can set a value", fun () ->
        let key1 = { Key = 1 }
        let key2 = { Key = 3 }
        let key5 = { Key = 5 }

        let m = JSe.WeakMap<WeakType,int>([key1,2;key2,4])
        
        Jest.expect(JSe.WeakMap.get key5 m).toBe(None)
        Jest.expect(m.Set(key5, 6).Get(key5)).toBe(Some 6)
    )
    
    Jest.test("Can check for a key", fun () ->
        let key1 = { Key = 1 }
        let key2 = { Key = 3 }

        let m = JSe.WeakMap<WeakType,int>([key1,2;key2,4])
        
        Jest.expect(JSe.WeakMap.has key1 m).toBe(true)
        Jest.expect(JSe.WeakMap.has { Key = 1 } m).toBe(false)
    )

    Jest.test("Can delete a key", fun () ->
        let key1 = { Key = 1 }
        let key2 = { Key = 3 }

        let m = JSe.WeakMap<WeakType,int>([key1,2;key2,4])
        
        Jest.expect(m.Get(key1)).toBe(Some 2)

        Jest.expect(m.Delete(key1)).toBe(true)
        Jest.expect(m.Delete({ Key = 3 })).toBe(false)
        
        Jest.expect(m.Get(key1)).toBe(None)
    )
)

Jest.describe("Set", fun () ->
    Jest.test("Can create an empty Map", fun () ->
        Jest.expect(JSe.Set<int>()).toBeDefined()
    )

    Jest.test("Can get the size", fun () ->
        let s = JSe.Set<int>([1;2;3;4])
        
        Jest.expect(s.Size).toBe(4)
    )

    Jest.test("Can clear", fun () ->
        let s = JSe.Set<int>([1;2;3;4])
        
        Jest.expect(s.Size).toBe(4)

        s.Clear()

        Jest.expect(s.Size).toBe(0)
    )

    Jest.test("Can delete an item", fun () ->
        let s = JSe.Set<int>([1;2;3;4])
        
        Jest.expect(s.Size).toBe(4)

        Jest.expect(s.Delete(1)).toBe(true)
        Jest.expect(s.Delete(40)).toBe(false)

        Jest.expect(s.Size).toBe(3)
    )

    Jest.test("Can get the entries", fun () ->
        let entries = JSe.Set<int>([1;2;3;4]).Entries()
        
        Jest.expect(entries).toHaveLength(4)
        Jest.expect(entries).toEqual(expect.arrayContaining [(1,1);(2,2);(3,3);(4,4)])
    )

    Jest.test("Can forEach", fun () ->
        let mutable count = 0

        JSe.Set<int>([1;2;3;4])
        |> JSe.Set.forEach (fun v _ _ -> count <- count + v)
        
        Jest.expect(count).toBe(10)
    )

    Jest.test("Can add an item", fun () ->
        let s = JSe.Set<int>([1;2;3;4])
        
        Jest.expect(JSe.Set.has 5 s).toBe(false)

        Jest.expect(s.Add(5).Has(5)).toBe(true)
    )
    
    Jest.test("Can check for an item", fun () ->
        let s = JSe.Set<int>([1;2;3;4])
        
        Jest.expect(JSe.Set.has 1 s).toBe(true)
        Jest.expect(JSe.Set.has 40 s).toBe(false)
    )

    Jest.test("Can get the keys", fun () ->
        let keys = JSe.Set<int>([1;2;3;4]).Keys()
        
        Jest.expect(keys).toHaveLength(4)
        Jest.expect(keys).toContain(1)
        Jest.expect(keys).toContain(2)
        Jest.expect(keys).toContain(3)
        Jest.expect(keys).toContain(4)
    )

    Jest.test("Can get the values", fun () ->
        let values = JSe.Set<int>([1;2;3;4]).Values()
        
        Jest.expect(values).toHaveLength(4)
        Jest.expect(values).toContain(1)
        Jest.expect(values).toContain(2)
        Jest.expect(values).toContain(3)
        Jest.expect(values).toContain(4)
    )
)

Jest.describe("WeakSet", fun () ->
    Jest.test("Can create an empty WeakMap", fun () ->
        Jest.expect(JSe.WeakSet<WeakType>()).toBeDefined()
    )

    Jest.test("Can get an item", fun () ->
        let item1 = { Key = 1 }
        let item2 = { Key = 2 }

        let s = JSe.WeakSet<WeakType>([item1;item2])
        
        Jest.expect(JSe.WeakSet.has item1 s).toBe(true)
        Jest.expect(JSe.WeakSet.has { Key = 40 } s).toBe(false)
    )
    
    Jest.test("Can add an item", fun () ->
        let item1 = { Key = 1 }
        let item2 = { Key = 2 }
        let item5 = { Key = 5 }
        
        let s = JSe.WeakSet<WeakType>([item1;item2])
        
        Jest.expect(JSe.WeakSet.has item5 s).toBe(false)
        Jest.expect(s.Add(item5).Has(item5)).toBe(true)
    )
    
    Jest.test("Can check for an item", fun () ->
        let item1 = { Key = 1 }
        let item2 = { Key = 2 }
        
        let s = JSe.WeakSet<WeakType>([item1;item2])
        
        Jest.expect(JSe.WeakSet.has item1 s).toBe(true)
        Jest.expect(JSe.WeakSet.has { Key = 1 } s).toBe(false)
    )

    Jest.test("Can delete an item", fun () ->
        let item1 = { Key = 1 }
        let item2 = { Key = 2 }
        
        let s = JSe.WeakSet<WeakType>([item1;item2])
        
        Jest.expect(s.Has(item1)).toBe(true)

        Jest.expect(s.Delete(item1)).toBe(true)
        Jest.expect(s.Delete({ Key = 3 })).toBe(false)
        
        Jest.expect(s.Has(item1)).toBe(false)
    )
)

Jest.describe("PropertyDescriptor", fun () ->
    Jest.test("Can create an empty PropertyDescriptor", fun () ->
        Jest.expect(JSe.PropertyDescriptor()).toBeDefined()
    )

    Jest.test("Can set properties in a PropertyDescriptor", fun () ->
        let pd = JSe.PropertyDescriptor() |> JSe.PropertyDescriptor.setValue 1
        
        Jest.expect(pd.Value.IsSome).toBe(true)
        Jest.expect(pd.Value).toBe(1)
    )
)

Jest.describe("Object", fun () ->
    Jest.test("Can create a JS Object", fun () ->
        Jest.expect(JSe.Object.createEmpty<WeakType>()).toBeDefined()
        Jest.expect(JSe.Object.create({ Key = 1 })).toBeDefined()
        
        let newObj = JSe.Object.createWithDescriptors ["Key", JSe.PropertyDescriptor() |> JSe.PropertyDescriptor.setValue 1] { Key = 2 }
        Jest.expect(newObj.Key).toBe(1)
    )

    Jest.test("Can set properties in a PropertyDescriptor", fun () ->
        let o = JSe.Object.createWithDescriptors ["Key", JSe.PropertyDescriptor() |> JSe.PropertyDescriptor.setValue 1] { Key = 2 }

        Jest.expect(JSe.Object.hasOwnProperty "Key" o).toBeTruthy()
        Jest.expect(o.Key).toBe(1)
    )

    Jest.test("Can assign", fun () ->
        let o = { Key = 2 }
        let newO = JSe.Object.assign o {| test = 1 |} |> JSe.Object.as'<{| test: int |}>

        Jest.expect(newO.test).toBe(1)
    )

    Jest.test("Can assign many", fun () ->
        let o = { Key = 2 }
        let newO = 
            [ box {| test = 1 |}
              box {| test2 = 2 |}
              box {| test3 = 3 |}
              box {| test4 = 4 |}
              box {| test5 = 5 |} ]
            |> JSe.Object.assignMany o
            |> JSe.Object.as'<{| test: int; test4: int |}>

        Jest.expect(newO.test).toBe(1)
        Jest.expect(newO.test4).toBe(4)
    )

    Jest.test("Can check enumerability", fun () ->
        Jest.expect(JSe.Object.propertyIsEnumerable "test" {| test = [1;2;3;4] |}).toBe(true)
        Jest.expect(JSe.Object.propertyIsEnumerable "test" { Key = 1 }).toBe(false)
    )
)

Jest.describe("Date", fun () ->
    Jest.beforeAll <| fun () ->
        Jest.useFakeTimers()
        Jest.setSystemTime(0)

    Jest.test("Can create an empty Date", fun () ->
        let d = JSe.Date()

        Jest.expect(d).toBeDefined()
        Jest.expect(d.ValueOf()).toBe(0)
    )

    Jest.test("Can create an explicit Date", fun () ->
        let d = JSe.Date(2020, 0, 1)

        Jest.expect(d).toBeDefined()
        Jest.expect(d.GetFullYear()).toBe(2020)
        Jest.expect(d.GetMonth()).toBe(0)
        Jest.expect(d.GetDay()).toBe(System.DayOfWeek.Wednesday)
    )

    Jest.test("Can modify a Date", fun () ->
        let d = JSe.Date() |> JSe.Date.setFullYear 2000

        Jest.expect(d).toBeDefined()
        Jest.expect(d.GetFullYear()).toBe(2000)
    )
)

Jest.describe("JSON", fun () ->
    Jest.test("Can round trip", fun () ->
        let o = { Key = 1 } |> JSe.JSON.stringify |> JSe.JSON.parseAs<WeakType>
        
        Jest.expect(o.Key).toBe(1)
    )
)

Jest.describe("Promise", fun () ->
    Jest.test("Can create", promise {
        let p = JSe.Promise(fun resolve _ -> resolve 1)

        do! Jest.expect(p).resolves.toBe(1)
    })
    
    Jest.test("Can create and reject", promise {
        let p = JSe.Promise<int>(fun _ reject -> System.Exception("Bad!") |> reject)

        do! Jest.expect(p).rejects.toThrow()
    })

    Jest.test("Can run parallel", promise {
        let promI i = 
            JSe.Promise<int>(fun resolve _ -> 
                resolve i
            )
        
        let promises = List.init 10 promI

        do! Jest.expect(JSe.Promise.all promises).resolves.toEqual(expect.arrayContaining [0 .. 9])
    })
)
