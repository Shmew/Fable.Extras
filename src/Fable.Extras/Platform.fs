namespace Fable.Extras.Platform

open Browser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Extras
open FSharp.Core

[<Erase;RequireQualifiedAccess>]
module internal PlatformImpl =
    [<Erase>]
    module NW =
        [<Erase>]
        type Window =
            [<Emit("$0.Get()")>]
            member _.Get () : obj = jsNative

    [<Erase>]
    type NW =
        [<Emit("$0.Window")>]
        member _.Window : NW.Window = jsNative

    [<Erase>]
    type ProcessVersions =        
        [<Emit("$0.electron")>]
        member _.electron : bool option = jsNative

        [<Emit("$0.node")>]
        member _.node : bool option = jsNative

        [<Emit("$0.nw")>]
        member _.nw : bool option = jsNative

    [<Erase>]
    type Process =
        [<Emit("$0.platform")>]
        member _.platform : string = jsNative

        [<Emit("$0.versions")>]
        member _.versions : ProcessVersions option = jsNative

    let [<Global>] nw : NW = jsNative
    let [<Global("process")>] process' : Process = jsNative
    
    let [<Global>] MSApp : obj = jsNative
    let [<Global>] Windows : obj = jsNative

[<RequireQualifiedAccess>]
module JSe =
    module Platform =
        type has =
            /// The app has a window to render DOM content.
            static member gui =
                match JSe.typeof navigator <> JSe.Types.Undefined && JSe.typeof window <> JSe.Types.Undefined with
                | true when JSe.typeof PlatformImpl.nw <> JSe.Types.Undefined ->
                    try 
                        PlatformImpl.nw.Window.Get() |> ignore
                        true
                    with _ -> false
                | b -> b

        type is =
            /// Fully functional Node & core modules.
            ///
            /// Is true for Node.js, Electron, NW.JS
            ///
            /// Is false for browsers with shims or bundles of some Node modules (shimmed process, EventEmitter, etc..)
            static member node = 
                JSe.typeof PlatformImpl.process' <> JSe.Types.Undefined 
                && PlatformImpl.process'.versions.IsSome 
                && PlatformImpl.process'.versions.Value.node.IsSome

            /// Progressive Web App.
            static member pwa =
                has.gui 
                && window.matchMedia("(display-mode: standalone)").matches
                && document.head.querySelector("[rel=\"manifest\"]") |> isNull |> not

            /// Windows 10 app - Universal Windows Platform.
            static member uwp = 
                JSe.typeof PlatformImpl.Windows <> JSe.Types.Undefined 
                && JSe.typeof PlatformImpl.MSApp <> JSe.Types.Undefined

            /// Node + Chromium
            static member nwjs = is.node && PlatformImpl.process'.versions.Value.nw.IsSome

            /// electron.js
            static member electron = is.node && PlatformImpl.process'.versions.Value.electron.IsSome

            /// Cordova mobile app
            static member cordova = has.gui && (unbox<obj option> window?cordova).IsSome

            /// The platform requires app to be compiled, bundled or packaged.
            static member packaged = is.uwp || is.nwjs || is.electron || is.cordova

            /// The app runs inside a browser and is served from a server or browser cache.
            static member browser = not (is.node || is.packaged)

            /// App is a webpage and not a PWA.
            static member website = is.browser && not is.pwa

            /// Script is executed inside Web Worker
            static member worker =
                not has.gui
                && JSe.typeof self <> JSe.Types.Undefined
                && JSe.typeof self?importScripts <> JSe.Types.Undefined
            
            /// Script is executed inside Service Worker
            static member serviceWorker =
                is.worker 
                && navigator.serviceWorker.IsSome 
                && navigator.serviceWorker.Value.controller.IsSome
            
            static member chromeIos = has.gui && navigator.userAgent.Contains("CriOS/")

            static member edgeAndroid = has.gui && navigator.userAgent.Contains("EdgA/")

            static member edgeChromium = has.gui && navigator.userAgent.Contains("Edg/")

            static member edgeHtml = has.gui && navigator.userAgent.Contains("Edge/")
            
            static member edgeIos = has.gui && navigator.userAgent.Contains("EdgiOS/")

            static member edge =  is.edgeChromium || is.edgeHtml || is.edgeAndroid || is.edgeIos
            
            static member firefoxIos = has.gui && navigator.userAgent.Contains("FxiOS/")
            
            static member firefox = has.gui && (navigator.userAgent.Contains("Firefox") || is.firefoxIos)

            static member opera = has.gui && (navigator.userAgent.Contains("Opera") || navigator.userAgent.Contains("OPR/"))
            
            static member samsungBrowser = has.gui && navigator.userAgent.Contains("SamsungBrowser/")

            static member chrome = 
                has.gui 
                && (navigator.userAgent.Contains("Chrome") || is.chromeIos) 
                && not is.edge 
                && not is.opera 
                && not is.samsungBrowser

            static member safari =
                (
                    has.gui
                    && navigator.userAgent.Contains("Safari")
                    && not (is.chrome || is.edge || is.opera || is.samsungBrowser)
                )
                || is.edgeIos
                || is.chromeIos
                || is.firefoxIos
                
            static member ie = has.gui && navigator.userAgent.Contains("Trident")

            static member trident = is.ie

            static member blink = (is.chrome && not is.chromeIos) || is.edgeChromium || is.edgeAndroid || is.samsungBrowser

            static member webkit = is.blink || is.safari

            static member gecko = is.firefox && not (is.firefoxIos || is.webkit || is.safari)

            static member android = has.gui && navigator.userAgent.Contains("Android")

            static member chromeOS = has.gui && navigator.userAgent.Contains("CrOS")
            
            static member tizen = has.gui && navigator.userAgent.Contains("Tizen")
            
            static member ios =
                has.gui 
                && JSe.RegExp("iPad|iPhone|iPod").Test(navigator.userAgent) 
                && (JSe.typeof window?MSStream <> JSe.Types.Undefined)

            static member linuxBased = is.android || is.tizen
            
            static member windows = 
                if is.node then PlatformImpl.process'.platform = "win32"
                else navigator.userAgent.Contains("Windows")
            
            static member macos = 
                if is.node then PlatformImpl.process'.platform = "darwin"
                else navigator.userAgent.Contains("Macintosh")
            
            static member linux = 
                if is.node then PlatformImpl.process'.platform = "linux"
                else 
                    navigator.userAgent.Contains("Linux")
                    && not (is.linuxBased || is.macos)
