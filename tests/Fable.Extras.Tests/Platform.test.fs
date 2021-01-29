module PlatformTests

open Fable.Extras
open Fable.Extras.Platform
open Fable.Jester
open Fable.FastCheck
open Fable.FastCheck.Jest

Jest.describe("Platform", fun () ->
    Jest.test("Detects node.js", fun () ->
        Jest.expect(JSe.Platform.is.node).toBe(true)
    )

    Jest.test("Does not detect a browser", fun () ->
        Jest.expect(JSe.Platform.is.browser).toBe(false)
    )
)
