namespace Fable.Extras.Events

open Fable.Core
open FSharp.Core
open System

[<Erase;RequireQualifiedAccess>]
module JSe =
    /// A DOM interface implemented by objects that can receive 
    /// events and may have listeners for them.
    [<AbstractClass;Global>]
    type EventTarget () =
        /// Registers an event handler of a specific event type on the EventTarget.
        member _.addEventListener (type': string, listener: #Browser.Types.Event -> unit, ?options: Browser.Types.AddEventListenerOptions) : unit = jsNative
        
        /// Removes an event listener from the EventTarget.
        member _.removeEventListener (type': string, listener: #Browser.Types.Event -> unit, ?options: Browser.Types.AddEventListenerOptions) : unit = jsNative

        /// Dispatches an event to this EventTarget.
        member _.dispatchEvent (event: #Browser.Types.Event) : bool = jsNative
