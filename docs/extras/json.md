# JSON

Functions for parsing JavaScript Object Notation (JSON) and converting values to JSON.

<br>
<Note type="tip">

You should try to avoid using these and instead checkout these great projects:
* [Fable.SimpleJson](https://github.com/Zaid-Ajaj/Fable.SimpleJson)
* [Thoth.Json](https://github.com/thoth-org/Thoth.Json)
</Note>

## parse

Parse the string text as JSON, optionally transform the produced value and its properties, 
and return the value. 

The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
Which is a function to map each key (first parameter) and value (second parameter) pair before the end result.

<Note type="warning">Any violations of the JSON syntax, including those pertaining to the differences between 
JavaScript and JSON, cause a SyntaxError to be thrown.</Note>

Signature:
```fsharp
(text: string, ?reviver: obj -> obj -> obj) -> obj
```

## tryParse

Parse the string text as JSON, optionally transform the produced value and its properties, 
and return the value. 

The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
Which is a function to map each key (first parameter) and value (second parameter) pair before the end result.

Signature:
```fsharp
(text: string, ?reviver: obj -> obj -> obj) -> Result<obj,exn>
```

## parseAs

Parse the string text as JSON, optionally transform the produced value and its properties, 
and return the value. 

The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
Which is a function to map each key (first parameter) and value (second parameter) pair before the end result.

<Note type="warning">Any violations of the JSON syntax, including those pertaining to the differences between 
JavaScript and JSON, cause a SyntaxError to be thrown.</Note>

<Note type="warning">This does not validate the returned type.</Note>

Signature:
```fsharp
<'T> (text: string, ?reviver: obj -> obj -> obj) -> 'T
```

## tryParseAs

Parse the string text as JSON, optionally transform the produced value and its properties, 
and return the value. 

The reviver option allows for interpreting what the replacer has used to stand in for other datatypes.
Which is a function to map each key (first parameter) and value (second parameter) pair before the end result.

<Note type="warning">This does not validate the returned type.</Note>

Signature:
```fsharp
<'T> (text: string, ?reviver: obj -> obj -> obj) -> Result<'T,exn>
```

## stringify

Return a JSON string corresponding to the specified value, optionally including only certain properties 
or replacing property values in a user-defined manner. 

By default, all instances of undefined are replaced with null, and other unsupported native data types 
are censored. The replacer option allows for specifying other behavior via a function taking a key (string) 
and value (obj) and returning a new value or a sequence of whitelisted values (of numbers and/or strings).

The space option indicates how to insert white space into the output of the JSON for readability.

If an integer it indicates the number of space characters to use as white space; this number is capped at 
10 (if it is greater, the value is just 10). Values less than 1 indicate that no space should be used.

If a string (or the first 10 characters of the string, if it's longer than that) is used as white space. 

If no space is provided, no white space is used.

Signature:
```fsharp
(value: obj) -> string
(value: obj, space: int) -> string
(value: obj, space: string) -> string
(value: obj, replacer: string -> obj -> obj) -> string
(value: obj, replacer: seq<obj>) -> string
(value: obj, replacer: string -> obj -> obj, space: int) -> string
(value: obj, replacer: seq<obj>, space: int) -> string
(value: obj, replacer: string -> obj -> obj, space: string) -> string
(value: obj, replacer: seq<obj>, space: string) -> string
```
