namespace Fable.Extras.Media

open Fable.Core
open Fable.Extras.Events
open FSharp.Core

[<Erase;RequireQualifiedAccess>]
module JSe =
    /// The state of a MediaSource.
    [<RequireQualifiedAccess;StringEnum(CaseRules.LowerFirst)>]
    type MediaSourceState =
        /// The source is not currently attached to a media element.
        | Closed
        /// The source is attached to a media element but the stream 
        /// has been ended via a call to MediaSource.endOfStream.
        | Ended
        /// The source is attached to a media element and ready to 
        /// receive SourceBuffer objects.
        | Open

    /// An error to throw when the end of the stream is reached.
    [<RequireQualifiedAccess;StringEnum(CaseRules.LowerFirst)>]
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

    /// Represents a source of media data for an HTMLMediaElement object. 
    ///
    /// A MediaSource object can be attached to a HTMLMediaElement to be played 
    /// in the user agent.
    [<Global>]
    type MediaSource () =
        inherit JSe.EventTarget()

        /// A SourceBufferList object containing the list of SourceBuffer objects 
        /// associated with this MediaSource.
        [<Emit("$0.sourceBuffers")>]
        member _.SourceBuffers : Browser.Types.SourceBufferList = jsNative

        /// A SourceBufferList object containing a subset of the SourceBuffer objects 
        /// contained within MediaSource.sourceBuffers - the list of objects providing 
        /// the selected video track, enabled audio tracks, and shown/hidden text tracks.
        [<Emit("$0.activeSourceBuffers")>]
        member _.ActiveSourceBuffers : Browser.Types.SourceBufferList = jsNative

        /// The state of the current MediaSource.
        [<Emit("$0.readyState")>]
        member _.ReadyState : MediaSourceState = jsNative

        /// <summary>
        /// Gets and sets the duration of the current media being presented in seconds.
        /// </summary>
        /// <exception cref="System.Exception">An attempt was made to set a duration value 
        /// that was negative, or NaN.</exception>
        /// <exception cref="System.Exception">MediaSource.ReadyState is not equal to Open, 
        /// or one or more of the SourceBuffer objects in MediaSource.SourceBuffers are being 
        /// updated (i.e. their SourceBuffer.updating property is true.)</exception>
        member _.Duration
            with [<Emit("$0.duration")>] get () : float = jsNative
            and [<Emit("$0.duration = $1")>] set (x: float) = jsNative

        /// The event handler for the sourceclose event.
        member _.OnSourceClose
            with [<Emit("$0.onsourceclose")>] get () : (Browser.Types.Event -> unit) option = jsNative
            and [<Emit("$0.onsourceclose = $1")>] set (f: (Browser.Types.Event -> unit) option) = jsNative

        /// The event handler for the sourceended event.
        member _.OnSourceEnded
            with [<Emit("$0.onsourceended")>] get () : (Browser.Types.Event -> unit) option = jsNative
            and [<Emit("$0.onsourceended = $1")>] set (f: (Browser.Types.Event -> unit) option) = jsNative

        /// The event handler for the sourceopen event.
        member _.OnSourceOpen
            with [<Emit("$0.onsourceopen")>] get () : (Browser.Types.Event -> unit) option = jsNative
            and [<Emit("$0.onsourceopen = $1")>] set (f: (Browser.Types.Event -> unit) option) = jsNative
        
        /// <summary>
        /// Creates a new SourceBuffer of the given MIME type and adds it to the MediaSource's 
        /// sourceBuffers list.
        /// </summary>
        /// <exception cref="System.Exception">
        /// The value specified for mimeType is an empty string rather than a valid MIME type.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The MediaSource is not in the "open" readyState.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The specified mimeType isn't supported by the user agent, or is not compatible with
        /// the MIME types of other SourceBuffer objects that are already included in the media 
        /// source's sourceBuffers list.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The user agent can't handle any more SourceBuffer objects, or creating a new 
        /// SourceBuffer using the given mimeType would result in an unsupported configuration 
        /// of SourceBuffers.
        /// </exception>
        [<Emit("$0.addSourceBuffer($1)")>]
        member _.AddSourceBuffer (mimeType: string) : Browser.Types.SourceBuffer = jsNative

        /// <summary>
        /// Signals the end of the stream.
        /// </summary>
        /// <exception cref="System.Exception">
        /// MediaSource.readyState is not equal to open, or one or more of the SourceBuffer 
        /// objects in MediaSource.SourceBuffers are being updated (i.e. their 
        /// SourceBuffer.updating property is true.)
        /// </exception>
        [<Emit("$0.endOfStream($1...)")>]
        member _.EndOfStream (?errMsg: MediaSourceStreamError) : unit = jsNative

        /// <summary>
        /// Removes the given SourceBuffer from the SourceBuffers list associated with this 
        /// MediaSource object.
        /// </summary>
        /// <exception cref="System.Exception">
        /// The supplied sourceBuffer doesn't exist in MediaSource.SourceBuffers.
        /// </exception>
        [<Emit("$0.removeSourceBuffer($1)")>]
        member _.RemoveSourceBuffer (sourceBuffer: Browser.Types.SourceBuffer) : unit = jsNative

    [<Erase;RequireQualifiedAccess>]
    module MediaSource =
        /// A SourceBufferList object containing the list of SourceBuffer objects 
        /// associated with this MediaSource.
        let inline sourceBuffers (ms: MediaSource) = ms.SourceBuffers

        /// A SourceBufferList object containing a subset of the SourceBuffer objects 
        /// contained within MediaSource.sourceBuffers - the list of objects providing 
        /// the selected video track, enabled audio tracks, and shown/hidden text tracks.
        let inline activeSourceBuffers (ms: MediaSource) = ms.ActiveSourceBuffers

        /// The state of the current MediaSource.
        let inline readyState (ms: MediaSource) = ms.ReadyState

        /// Gets the duration of the current media being presented in seconds.
        let inline duration (ms: MediaSource) = ms.Duration

        /// <summary>
        /// Sets the duration of the current media being presented in seconds.
        /// </summary>
        /// <exception cref="System.Exception">An attempt was made to set a duration value 
        /// that was negative, or NaN.</exception>
        /// <exception cref="System.Exception">MediaSource.ReadyState is not equal to Open, 
        /// or one or more of the SourceBuffer objects in MediaSource.SourceBuffers are being 
        /// updated (i.e. their SourceBuffer.updating property is true.)</exception>
        let inline setDuration (duration: float) (ms: MediaSource) = 
            ms.Duration <- duration
            ms

        /// Attempts to set the duration of the current media being presented in seconds.
        let inline trySetDuration (duration: float) (ms: MediaSource) =
            try 
                ms.Duration <- duration 
                Ok ms
            with e -> Error e

        /// Get the event handler for the sourceclose event.
        let inline onSourceClose (ms: MediaSource) = ms.OnSourceClose

        /// Set the event handler for the sourceclose event.
        let inline setOnSourceClose (f: Browser.Types.Event -> unit) (ms: MediaSource) = 
            ms.OnSourceClose <- Some f
            ms
        
        /// Get the event handler for the sourceended event.
        let inline onSourceEnded (ms: MediaSource) = ms.OnSourceEnded
        
        /// Set the event handler for the sourceended event.
        let inline setOnSourceEnded (f: Browser.Types.Event -> unit) (ms: MediaSource) = 
            ms.OnSourceEnded <- Some f
            ms

        /// Get the event handler for the sourceopen event.
        let inline onSourceOpen (ms: MediaSource) = ms.OnSourceOpen
        
        /// Set the event handler for the sourceopen event.
        let inline setOnSourceOpen (f: Browser.Types.Event -> unit) (ms: MediaSource) = 
            ms.OnSourceOpen <- Some f
            ms

        /// <summary>
        /// Creates a new SourceBuffer of the given MIME type and adds it to the MediaSource's 
        /// sourceBuffers list.
        /// </summary>
        /// <exception cref="System.Exception">
        /// The value specified for mimeType is an empty string rather than a valid MIME type.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The MediaSource is not in the "open" readyState.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The specified mimeType isn't supported by the user agent, or is not compatible with
        /// the MIME types of other SourceBuffer objects that are already included in the media 
        /// source's sourceBuffers list.
        /// </exception>
        /// <exception cref="System.Exception">
        /// The user agent can't handle any more SourceBuffer objects, or creating a new 
        /// SourceBuffer using the given mimeType would result in an unsupported configuration 
        /// of SourceBuffers.
        /// </exception>
        let inline addSourceBuffer (mimeType: string) (ms: MediaSource) = ms.AddSourceBuffer(mimeType)

        /// Attempts to create a new SourceBuffer of the given MIME type and adds it to the MediaSource's 
        /// sourceBuffers list.
        let inline tryAddSourceBuffer (mimeType: string) (ms: MediaSource) = 
            try ms.AddSourceBuffer(mimeType) |> Ok
            with e -> Error e

        /// <summary>
        /// Signals the end of the stream.
        /// </summary>
        /// <exception cref="System.Exception">
        /// MediaSource.readyState is not equal to open, or one or more of the SourceBuffer 
        /// objects in MediaSource.SourceBuffers are being updated (i.e. their 
        /// SourceBuffer.updating property is true.)
        /// </exception>
        let inline endOfStream (ms: MediaSource) = 
            ms.EndOfStream()
            ms

        /// Attempts to signal the end of the stream.
        let inline tryEndOfStream (ms: MediaSource) =
            try 
                ms.EndOfStream()
                Ok ms
            with e -> Error e

        /// <summary>
        /// Signals the end of the stream.
        /// </summary>
        /// <exception cref="System.Exception">
        /// MediaSource.readyState is not equal to open, or one or more of the SourceBuffer 
        /// objects in MediaSource.SourceBuffers are being updated (i.e. their 
        /// SourceBuffer.updating property is true.)
        /// </exception>
        let inline endOfStreamWithMsg (errMsg: MediaSourceStreamError) (ms: MediaSource) = 
            ms.EndOfStream(errMsg)
            ms

        /// Attempts to signal the end of the stream.
        let inline tryEndOfStreamWithMsg (errMsg: MediaSourceStreamError) (ms: MediaSource) =
            try 
                ms.EndOfStream(errMsg)
                Ok ms
            with e -> Error e

        /// <summary>
        /// Removes the given SourceBuffer from the SourceBuffers list associated with this 
        /// MediaSource object.
        /// </summary>
        /// <exception cref="System.Exception">
        /// The supplied sourceBuffer doesn't exist in MediaSource.SourceBuffers.
        /// </exception>
        let inline removeSourceBuffer (sourceBuffer: Browser.Types.SourceBuffer) (ms: MediaSource) = 
            ms.RemoveSourceBuffer(sourceBuffer)
            ms
        
        /// Attempts to remove the given SourceBuffer from the SourceBuffers list associated with this 
        /// MediaSource object.
        let inline tryRemoveSourceBuffer (sourceBuffer: Browser.Types.SourceBuffer) (ms: MediaSource) = 
            try
                ms.RemoveSourceBuffer(sourceBuffer)
                Ok ms
            with e -> Error e

        /// Indicates if the given MIME type is supported by the current user agent — this is, 
        /// if it can successfully create SourceBuffer objects for that MIME type.
        [<Emit("MediaSource.isTypeSupported()")>]
        let inline isTypeSupported (mimeType: string) : bool = jsNative

        /// Checks if the browser supports MediaSource.
        [<Emit("'MediaSource' in window")>]
        let inline isMediaSourceSupported () : bool = jsNative
