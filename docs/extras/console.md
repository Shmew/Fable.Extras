# Console

Provides access to the browser's debugging console. The specifics of how it works varies 
from browser to browser, but there is a de facto set of features that are typically provided.

## assert'

Writes an error message to the console if the assertion is false. 

If the assertion is true, nothing happens.

Signature:
```fsharp
(test: bool, [<ParamArray>] optionalParams: obj []) -> unit
(test: bool, message: string, [<ParamArray>] optionalParams: obj []) -> unit
```

## clear

Clears the console if the environment allows it.

Signature:
```fsharp
unit -> unit
```

## count

Logs the number of times that this particular call to count() has been called.

Signature:
```fsharp
(?countLabel: string) -> unit
```

## countReset

Resets the counter used with [count](#count)

If the count label is supplied, countReset() resets the count for that label to 0. 

If omitted, countReset() resets the default counter to 0.

Signature:
```fsharp
(?countLabel: string) -> unit
```

## debug

Outputs a message to the web console at the "debug" log level. 

The message is only displayed to the user if the console is configured to display debug output.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```

## dir

Displays an interactive list of the properties of the specified JavaScript object. 

The output is presented as a hierarchical listing with disclosure triangles that let you see 
the contents of child objects.

Signature:
```fsharp
(value: obj) -> unit
```

## dirxml

Displays an interactive tree of the descendant elements of the specified XML/HTML element. 

If it is not possible to display as an element the JavaScript Object view is shown instead. 

The output is presented as a hierarchical listing of expandable nodes that let you see the 
contents of child nodes.

Signature:
```fsharp
(value: obj) -> unit
```

## error

Outputs a message to the web console at the "error" log level. 

The message is only displayed to the user if the console is configured to display error output.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```

## group

Creates a new inline group in the web console log. This indents following console messages by an 
additional level, until [groupEnd](#groupend) is called.

Signature:
```fsharp
(?groupTitle: string) -> unit
```

## groupCollapsed

Creates a new inline group in the web console log. Unlike [group](#group), however, the new group is 
created collapsed. The user will need to use the disclosure button next to it to expand it, revealing 
the entries created in the group.

Call [groupEnd](#groupend) to back out to the parent group.

Signature:
```fsharp
(?groupTitle: string) -> unit
```

## groupEnd

Exits the current inline group in the web console log.

Signature:
```fsharp
unit -> unit
```

## info

Outputs a message to the web console at the "info" log level. 

The message is only displayed to the user if the console is configured to display info output.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```

## log

Outputs a message to the web console log.

Specifically, log gives special treatment to DOM elements, whereas [dir](#dir) does not. 

This is often useful when trying to see the full representation of the DOM JS object.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```

## profile

Starts recording a performance profile (for example, the Firefox performance tool).

You can optionally supply an argument to name the profile and this then enables you to stop only 
that profile if multiple profiles are being recorded.

To stop recording call [profileEnd](#profileend).

<Note type="warning">This is a *Non-standard* API, DO NOT use it in production!</Note>

Signature:
```fsharp
(?reportLabel: string) -> unit
```

## profileEnd

The profileEnd method stops recording a profile previously started with [profile](#profile).

You can optionally supply an argument to name the profile. Doing so enables you to stop only 
that profile if you have multiple profiles being recorded.

If passed a profile name, and it matches the name of a profile being recorded, then that profile 
is stopped.

If passed a profile name and it does not match the name of a profile being recorded, no changes 
will be made.

If is not passed a profile name, the most recently started profile is stopped.

<Note type="warning">This is a *Non-standard* API, DO NOT use it in production!</Note>

Signature:
```fsharp
(?reportLabel: string) -> unit
```

## table

Takes one mandatory argument data, which must be an array or an object, 
and one additional optional parameter columns.

It logs data as a table. Each element in the array (or enumerable property 
if data is an object) will be a row in the table.

The first column in the table will be labeled (index). If data is an array, 
then its values will be the array indices. If data is an object, then its 
values will be the property names.

Signature:
```fsharp
(data: 'T, ?columns: seq<string>) -> unit
```

## time

Starts a timer you can use to track how long an operation takes. 

You give each timer a unique name, and may have up to 10,000 timers running on a given page. 
When you call [timeEnd](#timeend) with the same name, the browser will output the time, in 
milliseconds, that elapsed since the timer was started.

Signature:
```fsharp
(?timerLabel: string) -> unit
```

## timeEnd

Stops a timer that was previously started by calling [time](#time).

If given a name it will stop only that timer and the elapsed time is automatically displayed in 
the web console along with an indicator that the time has ended.

Signature:
```fsharp
(?timerLabel: string) -> unit
```

## timeLog

Logs the current value of a timer that was previously started by calling [time](#time) to the 
web console.

Signature:
```fsharp
(?timerLabel: string) -> unit
```

## timeStamp

Adds a single marker to the browser's Performance or Waterfall tool. This lets you correlate a 
point in your code with the other events recorded in the timeline, such as layout and paint events.

You can optionally supply an argument to label the timestamp, and this label will then be shown 
alongside the marker.

<Note type="warning">This is a *Non-standard* API, DO NOT use it in production!</Note>

Signature:
```fsharp
(?timerLabel: string) -> unit
```

## trace

Outputs a message to the web console at the "trace" log level. 

The message is only displayed to the user if the console is configured to display trace output.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```

## warn

Outputs a message to the web console at the "warn" log level. 

The message is only displayed to the user if the console is configured to display warn output.

Signature:
```fsharp
([<ParamArray>] items: obj []) -> unit
(msg: string, [<ParamArray>] items: obj []) -> unit
```
