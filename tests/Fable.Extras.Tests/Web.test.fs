module WebTests

open Fable.Extras
open Fable.Extras.Web
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

Jest.describe("Web globals", fun () ->
    Jest.test("Can encode URI", fun () ->
        Jest.expect(JSe.encodeURI "hello world").toBe("hello%20world")
    )

    Jest.test("Can decode URI", fun () ->
        Jest.expect(JSe.decodeURI "hello%20world").toBe("hello world")
    )

    Jest.test("Can roundtrip URI", fun () ->
        Jest.expect(JSe.encodeURI "hello world" |> JSe.decodeURI).toBe("hello world")
    )

    Jest.test("Can encode URI component", fun () ->
        Jest.expect(JSe.encodeURIComponent "hello world?").toBe("hello%20world%3F")
    )

    Jest.test("Can decode URI component", fun () ->
        Jest.expect(JSe.decodeURIComponent "hello%20world%3F").toBe("hello world?")
    )

    Jest.test("Can roundtrip URI component", fun () ->
        Jest.expect(JSe.encodeURIComponent "hello world?" |> JSe.decodeURIComponent).toBe("hello world?")
    )
)

let [<Literal>] paramsString = "?someField=someValue"

Jest.describe("URLSearchParams", fun () ->
    Jest.test("Can create", fun () ->
        Jest.expect(JSe.URLSearchParams paramsString).toBeDefined()
    )

    Jest.test("Has works", fun () ->
        Jest.expect(JSe.URLSearchParams paramsString |> JSe.URLSearchParams.has "someField").toBe(true)
    )

    Jest.test("Can get", fun () ->
        Jest.expect(JSe.URLSearchParams paramsString |> JSe.URLSearchParams.get "someField").toBe("someValue")
        Jest.expect(JSe.URLSearchParams paramsString |> JSe.URLSearchParams.get "wrongField" |> Option.isNone).toBe(true)
    )

    Jest.test("Can getAll", fun () ->
        let searchParams = JSe.URLSearchParams paramsString 
        let validRes = searchParams |> JSe.URLSearchParams.getAll "someField"
        let invalidRes = searchParams |> JSe.URLSearchParams.getAll "wrongField"

        Jest.expect(validRes).toHaveLength(1)
        Jest.expect(validRes).toEqual(expect.arrayContaining(["someValue"]))
        Jest.expect(invalidRes).toHaveLength(0)
    )

    Jest.test("Can append", fun () ->
        let res = 
            JSe.URLSearchParams paramsString 
            |> JSe.URLSearchParams.append "someField" "newValue" 
            |> JSe.URLSearchParams.getAll "someField"

        Jest.expect(res).toHaveLength(2)
        Jest.expect(res).toEqual(expect.arrayContaining(["someValue"; "newValue"]))
    )

    Jest.test("Can convert toString", fun () ->
        let res = 
            JSe.URLSearchParams paramsString 
            |> JSe.URLSearchParams.append "someField" "newValue" 
            |> JSe.URLSearchParams.toString

        Jest.expect(res).toBe("someField=someValue&someField=newValue")
    )

    Jest.test("Can set", fun () ->
        let res =
            JSe.URLSearchParams paramsString 
            |> JSe.URLSearchParams.append "someField" "newValue"
            |> JSe.URLSearchParams.set "someField" "overrided"
            |> JSe.URLSearchParams.toString

        Jest.expect(res).toBe("someField=overrided")
    )

    Jest.test("Can delete", fun () ->
        let res =
            JSe.URLSearchParams paramsString 
            |> JSe.URLSearchParams.append "someField" "newValue"
            |> JSe.URLSearchParams.delete "someField"
            |> JSe.URLSearchParams.toString

        Jest.expect(res).toBe("")
    )
)

Jest.describe("URL", fun () ->
    Jest.test("Can create", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org")).toBeDefined()
    )

    Jest.test("Can get hash", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org/#test") |> JSe.URL.hash).toBe("#test")
        Jest.expect(JSe.URL("https://www.fsharp.org/test") |> JSe.URL.hash).toBe("")
    )

    Jest.test("Can get host", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org") |> JSe.URL.host).toBe("www.fsharp.org")
    )
    
    Jest.test("Can get hostname", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org") |> JSe.URL.hostName).toBe("www.fsharp.org")
    )

    Jest.test("Can get href", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org") |> JSe.URL.href).toBe("https://www.fsharp.org/")
    )

    Jest.test("Can get origin", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org") |> JSe.URL.origin).toBe("https://www.fsharp.org")
    )

    Jest.test("Can modify properties", fun () ->
        Jest.expect(JSe.URL("https://www.fsharp.org") |> JSe.URL.setHash "test" |> JSe.URL.toString).toBe("https://www.fsharp.org/#test")
    )
)