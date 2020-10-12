# URL

Used to parse, construct, normalize, and encode URLs. 

It works by providing properties which allow you to easily read and modify 
the components of a URL.

```fsharp
type URL =
    new (url: string, ?base': string)
    new (url: Browser.Types.URL)

    /// A string containing a '#' followed by the fragment identifier of the URL.
    /// 
    /// The fragment is not percent-decoded. If the URL does not have a fragment 
    /// identifier, this property contains an empty string - "".
    member Hash
        with get () : string
        and set (x: string)
            
    /// A string containing the domain (that is the hostname) followed by (if a 
    /// port was specified) a ':' and the port of the URL.
    member Host
        with get () : string
        and set (x: string)

    /// A string containing the domain of the URL.
    member HostName
        with get () : string
        and set (x: string)

    /// A stringifier that returns a string containing the whole URL.
    member Href
        with get () : string
        and set (x: string)

    /// A string containing the origin of the URL, that is its scheme, its domain and its port.
    member Origin : string

    /// A string containing the password specified before the domain name.
    member Password
        with get () : string
        and set (x: string)

    /// A string containing an initial '/' followed by the path of the URL.
    member Pathname
        with get () : string
        and set (x: string)

    /// A string containing the port number of the URL.
    member Port
        with get () : string
        and set (x: string)

    /// A string containing the protocol scheme of the URL, including the final ':'.
    member Protocol
        with get () : string
        and set (x: string)

    /// A string indicating the URL's parameter string; if any parameters are provided, 
    /// this string includes all of them, beginning with the leading ? character.
    member Search
        with get () : string
        and set (x: string)

    /// A URLSearchParams object which can be used to access the individual query 
    /// parameters found in search.
    member SearchParams : URLSearchParams

    /// A string containing the username specified before the domain name.
    member Username
        with get () : string
        and set (x: string)

    override ToString () : string

    /// Returns a string containing the whole URL. 
    ///
    /// It returns the same string as the href property.
    member ToJSON () : string

    interface Browser.Types.URL
```

The functions outlined below are located in the `URL` module.

## createObjectURL

Creates a string containing a URL representing the object given in the parameter. 

The URL lifetime is tied to the document in the window on which it was created.

Each time you call `createObjectURL`, a new object URL is created, even if you've 
already created one for the same object. Each of these must be released by 
calling [revokeObjectURL](#revokeobjecturl) when you no longer need them.

Signature:
```fsharp
(o: Browser.Types.Blob) -> string
(o: Browser.Types.File) -> string
(o: MediaSource) -> string
```

## revokeObjectURL

Releases an existing object URL which was previously created by calling 
[createObjectURL](#createobjecturl). Call this function when you've finished using an object URL to 
let the browser know not to keep the reference to the file any longer.

Signature:
```fsharp
(o: Browser.Types.Blob) -> string
(o: Browser.Types.File) -> string
(o: MediaSource) -> string
```

## hash

A string containing a '#' followed by the fragment identifier of the URL.

The fragment is not percent-decoded. If the URL does not have a fragment 
identifier, this property contains an empty string - "".

Signature:
```fsharp
(url: URL) -> string
```

## setHash

A string containing a '#' followed by the fragment identifier of the URL.

The fragment is not percent-decoded. If the URL does not have a fragment 
identifier, this property contains an empty string - "".

Signature:
```fsharp
(hash: string) -> (url: URL) -> URL
```

## host

A string containing the domain (that is the hostname) followed by (if a 
port was specified) a ':' and the port of the URL.

Signature:
```fsharp
(url: URL) -> string
```

## setHost

A string containing the domain (that is the hostname) followed by (if a 
port was specified) a ':' and the port of the URL.

Signature:
```fsharp
(host: string) -> (url: URL) -> URL
```

## hostName

A string containing the domain of the URL.

Signature:
```fsharp
(url: URL) -> string
```

## setHostName

A string containing the domain of the URL.

Signature:
```fsharp
(hostName: string) -> (url: URL) -> URL
```

## href

A stringifier that returns a string containing the whole URL.

Signature:
```fsharp
(url: URL) -> string
```

## setHref

A stringifier that returns a string containing the whole URL.

Signature:
```fsharp
(href: string) -> (url: URL) -> URL
```

## origin

A string containing the origin of the URL, that is its scheme, its domain and its port.

Signature:
```fsharp
(url: URL) -> string
```

## password

A string containing the password specified before the domain name.

Signature:
```fsharp
(url: URL) -> string
```

## setPassword

A string containing the password specified before the domain name.

Signature:
```fsharp
(password: string) -> (url: URL) -> URL
```

## pathname

A string containing an initial '/' followed by the path of the URL.

Signature:
```fsharp
(url: URL) -> string
```

## setPathname

A string containing an initial '/' followed by the path of the URL.

Signature:
```fsharp
(pathname: string) -> (url: URL) -> URL
```

## port

A string containing the port number of the URL.

Signature:
```fsharp
(url: URL) -> string
```

## setPort

A string containing the port number of the URL.

Signature:
```fsharp
(port: int) -> (url: URL) -> URL
```

## protocol

A string containing the protocol scheme of the URL, including the final ':'.

Signature:
```fsharp
(url: URL) -> string
```

## setPort

A string containing the protocol scheme of the URL, including the final ':'.

Signature:
```fsharp
(protocol: string) -> (url: URL) -> URL
```

## search

A string indicating the URL's parameter string; if any parameters are provided, 
this string includes all of them, beginning with the leading ? character.

Signature:
```fsharp
(url: URL) -> string
```

## setSearch

A string indicating the URL's parameter string; if any parameters are provided, 
this string includes all of them, beginning with the leading ? character.

Signature:
```fsharp
(search: string) -> (url: URL) -> URL
```

## searchParams

A [URLSearchParams](/web/url-search-params) object which can be used to access the individual query 
parameters found in search.

Signature:
```fsharp
(url: URL) -> URLSearchParams
```

## username

A string containing the username specified before the domain name.

Signature:
```fsharp
(url: URL) -> string
```

## setUsername

A string containing the username specified before the domain name.

Signature:
```fsharp
(username: string) -> (url: URL) -> URL
```

## toString

Returns the entire URL.

Signature:
```fsharp
(url: URL) -> string
```

## toJSON

Returns a string containing the whole URL. 

It returns the same string as the href property.

Signature:
```fsharp
(url: URL) -> string
```
