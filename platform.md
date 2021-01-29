# Platform

Exposes a series of boolean properties to help with platform/runtime/environment detection.

Accessible via the `Fable.Extras.Platform` namespace.

<br/>

Used like so:

```fsharp
if Jse.Platform.is.chrome then
    JSe.console.log("Hi chrome!")
else JSe.console.log("Something else :(")
```

## has
* gui

## is
* android
* blink
* browser
* chrome
* chromeIos
* chromeOS
* cordova
* edge
* edgeAndroid
* edgeChromium
* edgeHtml
* electron
* firefox
* firefoxIos
* gecko
* ie
* ios
* linux
* linuxBased
* macOS
* node
* nwjs
* opera
* packaged
* pwa
* safari
* samsungBrowser
* serviceWorker
* tizen
* trident
* uwp
* webkit
* website
* windows
* worker
