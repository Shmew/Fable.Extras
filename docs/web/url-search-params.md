# URL Search Params

Utility methods to work with the query string of a URL.

The constructor does not parse full URLs. However, it will strip an initial 
leading `?` off of a string, if present.

```fsharp
type URLSearchParams =
    new ()
    new (urlEncoded: string)
    new (pairs: seq<string * string>)
    new (urlSearchParams: Browser.Types.URLSearchParams)

    /// Appends a specified key/value pair as a new search parameter.
    member Append (key: string, value: string) : unit
        
    /// Deletes the given search parameter and all its associated 
    /// values, from the list of all search parameters.
    member Delete (key: string) : unit
        
    /// Returns a sequence of all key/value pairs contained in this object. 
    member Entries () : seq<string * string>

    /// Iterates through all keys and values in the object, respectively.
    member ForEach (f: string -> string -> unit) : unit
        
    /// Returns the first value associated to the given search parameter.
    member Get (key: string) : string option
        
    /// Returns all values associated to the given search parameter.
    member GetAll (key: string) : seq<string option>
        
    /// Returns a Boolean that indicates whether a parameter with the specified name exists.
    member Has (key: string) : bool
        
    /// Returns a sequence of all keys contained in this object.
    member Keys () : seq<string>
        
    /// Sets the value associated with a given search parameter to the given value. 
    ///
    /// If there were several matching values, this method deletes the others. 
    ///
    /// If the search parameter doesn't exist, this method creates it.
    member Set (key: string, value: string) : unit

    /// Sorts all key/value pairs contained in this object in place. 
    /// The sort order is according to unicode code points of the keys. 
    ///
    /// This method uses a stable sorting algorithm (i.e. the relative order between 
    /// key/value pairs with equal keys will be preserved).
    member Sort () : unit

    override ToString () : string
        
    /// Returns a sequence of all values of the key/value pairs contained in this object.
    member Values () : seq<string>

    interface Browser.Types.URLSearchParams
    interface Collections.Generic.IEnumerable<string * string>
    interface Collections.IEnumerable
```

The functions outlined below are located in the `URLSearchParams` module.

## append

Appends a specified key/value pair as a new search parameter.

Signature:
```fsharp
(key: string) -> (value: string) -> (sp: URLSearchParams) -> URLSearchParams
```

## delete

Deletes the given search parameter and all its associated 
values, from the list of all search parameters.

Signature:
```fsharp
(key: string) -> (sp: URLSearchParams) -> URLSearchParams
```

## entries

Returns a sequence of all key/value pairs contained in this object. 

Signature:
```fsharp
(sp: URLSearchParams) -> seq<string * string>
```

## forEach

Iterates through all keys and values in the object, respectively.

Signature:
```fsharp
(f: string -> string -> unit) -> (sp: URLSearchParams) -> unit
```

## get

Returns the first value associated to the given search parameter.

Signature:
```fsharp
(key: string) -> (sp: URLSearchParams) -> string option
```

## getAll

Returns all values associated to the given search parameter.

Signature:
```fsharp
(key: string) -> (sp: URLSearchParams) -> seq<string option>
```

## has

Returns a Boolean that indicates whether a parameter with the specified name exists.

Signature:
```fsharp
(key: string) -> (sp: URLSearchParams) -> bool
```

## keys

Returns a sequence of all keys contained in this object.

Signature:
```fsharp
(sp: URLSearchParams) -> seq<string>
```

## set

Sets the value associated with a given search parameter to the given value. 

If there were several matching values, this function deletes the others. 

If the search parameter doesn't exist, this function creates it.

Signature:
```fsharp
(key: string) -> (value: string) -> (sp: URLSearchParams) -> URLSearchParams
```

## sort

Sorts all key/value pairs contained in this object in place. 

The sort order is according to unicode code points of the keys. 

This function uses a stable sorting algorithm (i.e. the relative order between 
key/value pairs with equal keys will be preserved).

Signature:
```fsharp
(key: string) -> (value: string) -> (sp: URLSearchParams) -> URLSearchParams
```

## toString

Returns the string value of the url search parameters.

Signature:
```fsharp
(sp: URLSearchParams) -> string
```

## values

Returns a sequence of all values of the key/value pairs contained in this object.

Signature:
```fsharp
(sp: URLSearchParams) -> seq<string>
```
