# Globals

Helpers for using Javascript keywords and global functions.

## apply

Calls the object's apply prototype.

<Note type="warning">This will have unexpected results if the function is 
curried, use [applyCurried](#applycurried) when that is the case.</Note>

Signature:
```fsharp
<'T,'U> (f: 'T) -> (arguments: obj []) -> 'U
```

## applyCurried

Applies an argument array to a curried function.

<Note type="warning">This will have unexpected results if the function is *not*
 curried, use [apply](#apply) when that is the case.</Note>

Signature:
```fsharp
<'T,'U> (f: 'T) -> (arguments: obj []) -> 'U
```

## arguments

An Array-like object accessible inside functions that contains 
the values of the arguments passed to that function.

<Note type="warning">If the function is curried this will always at most return a singleton.</Note>

Signature:
```fsharp
obj []
```

## atob

Decodes a string of data which has been encoded using Base64 encoding.

Signature:
```fsharp
(encodedData: string) -> string
```

## btoa

Creates a Base64-encoded ASCII string from a binary string (i.e., a 
String object in which each character in the string is treated as a 
byte of binary data).

Signature:
```fsharp
(stringToEncode: string) -> string
```

## globalThis

Javascript `globalThis` keyword.

Signature:
```fsharp
obj
```

## Interval

```fsharp
[<Measure>]
type IntervalId
```

### clearInterval

Cancels the repeated execution set using [setInterval](#setinterval).

Signature:
```fsharp
(id: int<IntervalId>) -> unit
```

### setInterval

Schedules a function to execute every time a given number 
of milliseconds elapses.

Signature:
```fsharp
(f: unit -> unit) -> (delay: int) -> int<IntervalId>
```

## instanceOf

Tests to see if the prototype property of a constructor appears 
anywhere in the prototype chain of an object.

This should only be used when working with external code (like bindings).

Signature:
```fsharp
(ctor: obj) -> (value: obj) -> bool 
```

## isSecureContext

Returns if the session is considered secure.

Signature:
```fsharp
bool
```

## or'

The Javascript || operator to collect the first non-None option

<Note>This is available as an operator, see [operators](/operators#or).</Note>

Signature:
```fsharp
(lh: 'T option) -> (rh: 'T option) -> 'T option
```

## queueMicrotask

Queues a microtask to be executed at a safe time prior to control returning 
to the browser's event loop. The microtask is a short function which will 
run after the current task has completed its work and when there is no other 
code waiting to be run before control of the execution context is returned 
to the browser's event loop.

This lets your code run without interfering with any other, potentially higher 
priority, code that is pending, but before the browser regains control over 
the execution context, potentially depending on work you need to complete.

Signature:
```fsharp
(f: unit -> unit) -> unit
```

## this'

Javascript `this` keyword.

Signature:
```fsharp
obj
```

## Timeout

```fsharp
[<Measure>]
type TimeoutId
```

### clearTimeout

Cancels the delayed execution set using [setTimeout](#settimeout).

Signature:
```fsharp
(id: int<TimeoutId>) -> unit
```

### setTimeout

Schedules a function to execute in a given amount of time (in miliseconds).

Signature:
```fsharp
(f: unit -> unit) -> (timeout: int) -> int<TimeoutId>
```

## typeof

Returns a Types union case indicating the type of the unevaluated operand.

#### Types

Javascript types.

```fsharp
type Types =
    | Bigint
    | Boolean
    | Function
    | Null
    | Number
    | Object
    | String
    | Symbol
    | Undefined
```

Signature:
```fsharp
(o: obj) -> Types
```

## is

Type checking and equality helpers.

Has functions to make checking aginst Javascript [types](#types) easier.

* bigint
* boolean
* equalsButFunctions - Normal structural F# comparison, but ignores 
    top-level functions (e.g. Elmish dispatch).
* equalsWithReferences - Performs a memberwise comparison where value 
    types and strings are compared by value, and other types by reference.
* function'
* null'
* nonEnumerableObject - Checks if the input is both an object and has a Symbol.iterator.
* number
* object
* promise
* string
* symbol
* undefined
