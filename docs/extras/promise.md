# Promise

Represents the eventual completion (or failure) of an asynchronous operation and its resulting value.

```fsharp
type Promise<'T> (executor: ('T -> unit) -> (Exception -> unit) -> unit) =
    new (p: JS.Promise<'T>)

    /// Handles thrown or rejected promises.
    member catch<'Reject> (?onrejected: 'Reject -> 'T) : Promise<'T>
        
    /// Callback functions for the success case of the Promise.
    member then'<'Reject,'TResult> (onfulfilled: 'T -> 'TResult) : Promise<'TResult>
    member then'<'Reject,'TResult> (onfulfilled: 'T -> 'TResult, 
                                    onrejected: 'Reject -> 'TResult) : Promise<'TResult>
        
    /// When the promise is settled, i.e either fulfilled or rejected, the specified 
    /// callback function is executed.
    member finally' (handler: unit -> unit) : Promise<'T>

    interface JS.Promise<'T>
```

The functions outlined below are static methods on the `Promise` type.

## all

Takes a sequence of Promises, and returns a single Promise that resolves to 
an array of the results of the input promises. This returned promise will 
resolve when all of the input's promises have resolved, or if the input 
iterable contains no promises. It rejects immediately upon any of the input 
promises rejecting or non-promises throwing an error, and will reject with 
this first rejection message / error.

Signature:
```fsharp
(promises: seq<#JS.Promise<'T>>) -> Promise<'T []>
```

## race

Returns a promise that fulfills or rejects as soon as one of the promises in 
an iterable fulfills or rejects, with the value or reason from that promise.

Signature:
```fsharp
(values: seq<#JS.Promise<'T>>) -> Promise<'T>
```

## reject

Returns a Promise object that is rejected with a given reason.

Signature:
```fsharp
unit -> Promise<unit>
(reason: 'Reason) -> Promise<'Reason>
```

## resolve

Returns a Promise object that is resolved with a given value. 

If the value is a promise, that promise is returned; if the value is a 
thenable (i.e. has a "then" method), the returned promise will "follow" 
that thenable, adopting its eventual state; otherwise the returned promise 
will be fulfilled with the value. This function flattens nested layers of 
promise-like objects (e.g. a promise that resolves to a promise that 
resolves to something) into a single layer.

Signature:
```fsharp
unit -> Promise<unit>
(promise: #JS.Promise<'T>) -> Promise<'T>
```
