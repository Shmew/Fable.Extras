# Media

Represents a source of media data for an `HTMLMediaElement` object. 

A MediaSource object can be attached to a `HTMLMediaElement` to be played 
in the user agent.

```fsharp
type MediaSource () =
    /// A SourceBufferList object containing the list of SourceBuffer objects 
    /// associated with this MediaSource.
    member SourceBuffers : Browser.Types.SourceBufferList

    /// A SourceBufferList object containing a subset of the SourceBuffer objects 
    /// contained within MediaSource.sourceBuffers - the list of objects providing 
    /// the selected video track, enabled audio tracks, and shown/hidden text tracks.
    member ActiveSourceBuffers : Browser.Types.SourceBufferList

    /// The state of the current MediaSource.
    member ReadyState : MediaSourceState

    /// Gets and sets the duration of the current media being presented in seconds.
    member Duration
        with get () : float
        and set (x: float) : unit

    /// The event handler for the sourceclose event.
    member OnSourceClose
        with get () : (Browser.Types.Event -> unit) option
        and set (f: (Browser.Types.Event -> unit) option) : unit

    /// The event handler for the sourceended event.
    member OnSourceEnded
        with get () : (Browser.Types.Event -> unit) option
        and set (f: (Browser.Types.Event -> unit) option) : unit

    /// The event handler for the sourceopen event.
    member OnSourceOpen
        with get () : (Browser.Types.Event -> unit) option
        and set (f: (Browser.Types.Event -> unit) option) : unit
        
    /// Creates a new SourceBuffer of the given MIME type and adds it to the MediaSource's 
    /// sourceBuffers list.
    member AddSourceBuffer (mimeType: string) : Browser.Types.SourceBuffer

    /// Signals the end of the stream.
    member EndOfStream (?errMsg: MediaSourceStreamError) : unit

    /// Removes the given SourceBuffer from the SourceBuffers list associated with this 
    /// MediaSource object.
    member RemoveSourceBuffer (sourceBuffer: Browser.Types.SourceBuffer) : unit
```

### MediaSourceState

The state of a MediaSource.

```fsharp
type MediaSourceState =
    /// The source is not currently attached to a media element.
    | Closed
    /// The source is attached to a media element but the stream 
    /// has been ended via a call to MediaSource.endOfStream.
    | Ended
    /// The source is attached to a media element and ready to 
    /// receive SourceBuffer objects.
    | Open
```

### MediaSourceStreamError

An error to throw when the end of the stream is reached.

```fsharp
type MediaSourceStreamError =
    /// Terminates playback and signals that a decoding error has occured. 
    ///
    /// This can be used to indicate that a parsing error has occured while 
    /// fetching media data; maybe the data is corrupt, or is encoded using 
    /// a codec that the browser doesn't know how to decode.
    | Decode
    /// Terminates playback and signals that a network error has occured. 
    ///
    /// This can be used create a custom error handler related to media streams. 
    ///
    /// For example, you might have a function that handles media chunk requests, 
    /// separate from other network requests.
    | Network
```

The functions outlined below are located in the `MediaSource` module.

## sourceBuffers

A `SourceBufferList` object containing the list of `SourceBuffer` objects 
associated with this `MediaSource`.

Signature:
```fsharp
(ms: MediaSource) -> Browser.Types.SourceBufferList
```

## activeSourceBuffers

A `SourceBufferList` object containing a subset of the `SourceBuffer` objects 
contained within [sourceBuffers](#sourcebuffers) - the list of objects providing 
the selected video track, enabled audio tracks, and shown/hidden text tracks.

Signature:
```fsharp
(ms: MediaSource) -> Browser.Types.SourceBufferList
```

## readyState

The state of the current `MediaSource`.

Signature:
```fsharp
(ms: MediaSource) -> MediaSourceState
```

## duration

Gets the duration of the current media being presented in seconds.

Signature:
```fsharp
(ms: MediaSource) -> float
```

## setDuration

Sets the duration of the current media being presented in seconds.

Signature:
```fsharp
(duration: float) -> (ms: MediaSource) -> MediaSource
```

## trySetDuration

Attempts to set the duration of the current media being presented in seconds.

Signature:
```fsharp
(duration: float) -> (ms: MediaSource) -> Result<MediaSource,exn>
```

## onSourceClose

Get the event handler for the `sourceclose` event.

Signature:
```fsharp
(ms: MediaSource) -> (Browser.Types.Event -> unit) option
```

## setOnSourceClose

Set the event handler for the sourceclose event.

Signature:
```fsharp
(Browser.Types.Event -> unit) -> (ms: MediaSource) -> MediaSource
```

## onSourceEnded

Get the event handler for the `sourceended` event.

Signature:
```fsharp
(ms: MediaSource) -> (Browser.Types.Event -> unit) option
```

## setOnSourceEnded

Set the event handler for the sourceended event.

Signature:
```fsharp
(Browser.Types.Event -> unit) -> (ms: MediaSource) -> MediaSource
```

## onSourceOpen

Get the event handler for the sourceopen event.

Signature:
```fsharp
(ms: MediaSource) -> (Browser.Types.Event -> unit) option
```

## setOnSourceOpen

Set the event handler for the sourceopen event.

Signature:
```fsharp
(Browser.Types.Event -> unit) -> (ms: MediaSource) -> MediaSource
```

## addSourceBuffer

Creates a new SourceBuffer of the given MIME type and adds it to the MediaSource's 

Signature:
```fsharp
(mimeType: string) -> (ms: MediaSource) -> Browser.Types.SourceBuffer
```

## tryAddSourceBuffer

Attempts to create a new SourceBuffer of the given MIME type and adds it to the MediaSource's 
sourceBuffers list.

Signature:
```fsharp
(mimeType: string) -> (ms: MediaSource) -> Result<Browser.Types.SourceBuffer,exn>
```

## endOfStream

Signals the end of the stream.

Signature:
```fsharp
(ms: MediaSource) -> Browser.Types.SourceBuffer
```

## tryEndOfStream

Attempts to signal the end of the stream.

Signature:
```fsharp
(ms: MediaSource) -> Result<Browser.Types.SourceBuffer,exn>
```

## endOfStreamWithMsg

Signals the end of the stream.

Signature:
```fsharp
(errMsg: MediaSourceStreamError) -> (ms: MediaSource) -> Browser.Types.SourceBuffer
```

## tryEndOfStreamWithMsg

Attempts to signal the end of the stream.

Signature:
```fsharp
(errMsg: MediaSourceStreamError) -> (ms: MediaSource) -> Result<Browser.Types.SourceBuffer,exn>
```

## removeSourceBuffer

Removes the given `SourceBuffer` from the SourceBuffers list associated with this 
`MediaSource` object.

Signature:
```fsharp
(sourceBuffer: Browser.Types.SourceBuffer) -> (ms: MediaSource) -> MediaSource
```

## tryRemoveSourceBuffer

Attempts to remove the given `SourceBuffer` from the SourceBuffers list associated with this 
`MediaSource` object.

Signature:
```fsharp
(sourceBuffer: Browser.Types.SourceBuffer) -> (ms: MediaSource) -> Result<MediaSource,exn>
```

## isTypeSupported

Indicates if the given MIME type is supported by the current user agent — this is, 
if it can successfully create `SourceBuffer` objects for that MIME type.

Signature:
```fsharp
(mimeType: string) -> bool
```

## isMediaSourceSupported

Checks if the browser supports `MediaSource`.

Signature:
```fsharp
unit -> bool
```
