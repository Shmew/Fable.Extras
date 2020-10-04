namespace Fable.Extras.Web

open Fable.Core
open Fable.Extras.Media
open FSharp.Core
open System

[<Erase;RequireQualifiedAccess>]
module JSe =
    /// <summary>
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    ///
    /// Note that encodeURI by itself cannot form proper HTTP, GET, and POST 
    /// requests, such as for XMLHttpRequest requests. encodeURIComponent, 
    /// however, does encode these characters.
    /// </summary>
    /// <exception cref="System.Exception">Throws a "malformed URI sequence" 
    /// exception when encodedURI contains invalid character sequences.</exception>
    [<Emit("encodeURI($0)")>]
    let encodeURI (s: string) : string = jsNative
    
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    let inline tryEncodeURI (s: string) =
        try encodeURI s |> Ok
        with e -> Error e
        
    /// <summary>
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    ///
    /// Note that encodeURI by itself cannot form proper HTTP, GET, and POST 
    /// requests, such as for XMLHttpRequest requests. encodeURIComponent, 
    /// however, does encode these characters.
    /// </summary>
    /// <exception cref="System.Exception">Throws a "malformed URI sequence" 
    /// exception when encodedURI contains invalid character sequences.</exception>
    [<Emit("encodeURI($0).replace(/%5B/g, '[').replace(/%5D/g, ']')")>]
    let encodeURIIPv6 (s: string) : string = jsNative
    
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    let inline tryEncodeURIIPv6 (s: string) =
        try encodeURIIPv6 s |> Ok
        with e -> Error e

    /// <summary>
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    /// </summary>
    /// <exception cref="System.Exception">Throws a "malformed URI sequence" 
    /// exception when encodedURI contains invalid character sequences.</exception>
    [<Emit("encodeURIComponent($0)")>]
    let encodeURIComponent (s: string) : string = jsNative
    
    /// Encodes a URI by replacing each instance of certain characters by 
    /// one, two, three, or four escape sequences representing the UTF-8 
    /// encoding of the character (will only be four escape sequences for 
    /// characters composed of two "surrogate" characters).
    let inline tryEncodeURIComponent (s: string) =
        try encodeURIComponent s |> Ok
        with e -> Error e

    /// <summary>
    /// Decodes a Uniform Resource Identifier (URI) previously created by 
    /// encodeURI() or by a similar routine.
    /// </summary>
    /// <exception cref="System.Exception">Throws a "malformed URI sequence" 
    /// exception when encodedURI contains invalid character sequences.</exception>
    [<Emit("decodeURI($0)")>]
    let decodeURI (s: string) : string = jsNative
    
    /// Decodes a Uniform Resource Identifier (URI) component previously 
    /// created by encodeURIComponent or by a similar routine.
    let inline tryDecodeURI (s: string) =
        try decodeURI s |> Ok
        with e -> Error e
        
    /// <summary>
    /// Decodes a Uniform Resource Identifier (URI) component previously 
    /// created by encodeURIComponent or by a similar routine.
    /// </summary>
    /// <exception cref="System.Exception">Throws a "malformed URI sequence" 
    /// exception when encodedURI contains invalid character sequences.</exception>
    [<Emit("decodeURIComponent($0)")>]
    let decodeURIComponent (s: string) : string = jsNative
    
    /// Decodes a Uniform Resource Identifier (URI) component previously 
    /// created by encodeURIComponent or by a similar routine.
    let inline tryDecodeURIComponent (s: string) =
        try decodeURIComponent s |> Ok
        with e -> Error e

    /// Utility methods to work with the query string of a URL.
    ///
    /// The constructor does not parse full URLs. However, it will strip an initial 
    /// leading ? off of a string, if present.
    [<Global>]
    type URLSearchParams private (o: obj) =
        [<Emit("new URLSearchParams($0)")>]
        new (urlEncoded: string) = URLSearchParams(urlEncoded)

        [<Emit("new URLSearchParams(Array.from($0))")>]
        new (pairs: seq<string * string>) = URLSearchParams(pairs)

        [<Emit("$0")>]
        new (urlSearchParams: Browser.Types.URLSearchParams) = URLSearchParams(urlSearchParams)

        /// Appends a specified key/value pair as a new search parameter.
        [<Emit("$0.append($1, $2)")>]
        member _.Append (key: string, value: string) : unit = jsNative
        
        /// Deletes the given search parameter and all its associated 
        /// values, from the list of all search parameters.
        [<Emit("$0.delete($1)")>]
        member _.Delete (key: string) : unit = jsNative
        
        /// Returns a sequence of all key/value pairs contained in this object. 
        [<Emit("$0.entries()")>]
        member _.Entries () : seq<string * string> = jsNative

        /// Iterates through all keys and values in the object, respectively.
        [<Emit("$0.forEach($0)")>]
        member _.ForEach (f: string -> string -> unit) : unit = jsNative
        
        /// Returns the first value associated to the given search parameter.
        [<Emit("$0.get($0)")>]
        member _.Get (key: string) : string option = jsNative
        
        /// Returns all values associated to the given search parameter.
        [<Emit("$0.getAll($0)")>]
        member _.GetAll (key: string) : seq<string option> = jsNative
        
        /// Returns a Boolean that indicates whether a parameter with the specified name exists.
        [<Emit("$0.has($0)")>]
        member _.Has (key: string) : bool = jsNative
        
        /// Returns a sequence of all keys contained in this object.
        [<Emit("$0.keys()")>]
        member _.Keys () : seq<string> = jsNative
        
        /// Sets the value associated with a given search parameter to the given value. 
        ///
        /// If there were several matching values, this method deletes the others. 
        ///
        /// If the search parameter doesn't exist, this method creates it.
        [<Emit("$0.set($1, $2)")>]
        member _.Set (key: string, value: string) : unit = jsNative

        /// Sorts all key/value pairs contained in this object in place. 
        /// The sort order is according to unicode code points of the keys. 
        ///
        /// This method uses a stable sorting algorithm (i.e. the relative order between 
        /// key/value pairs with equal keys will be preserved).
        [<Emit("$0.sort()")>]
        member _.Sort () : unit = jsNative

        [<Emit("$0.toString()")>]
        override _.ToString () : string = jsNative
        
        /// Returns a sequence of all values of the key/value pairs contained in this object.
        [<Emit("$0.sort()")>]
        member _.Values () : seq<string> = jsNative

        interface Browser.Types.URLSearchParams with
            member this.append (name, value) = this.Append(name, value)
            member this.delete name = this.Delete(name)
            member this.get name = this.Get(name)
            member this.getAll name = unbox <| this.GetAll(name)
            member this.has name = this.Has(name)
            member this.set (name, value) = this.Set(name, value)

        interface Collections.Generic.IEnumerable<string * string> with
            member _.GetEnumerator () = unbox<Collections.Generic.IEnumerator<string * string>>()

        interface Collections.IEnumerable with
            member _.GetEnumerator () = unbox<Collections.IEnumerator>()

    /// Utility methods to work with the query string of a URL.
    [<Erase;RequireQualifiedAccess>]
    module URLSearchParams =
        /// Appends a specified key/value pair as a new search parameter.
        let inline append (key: string) (value: string) (sp: URLSearchParams) = 
            sp.Append(key, value)
            sp

        /// Deletes the given search parameter and all its associated 
        /// values, from the list of all search parameters.
        let inline delete (key: string) (sp: URLSearchParams) =
            sp.Delete(key)
            sp
        
        /// Returns a sequence of all key/value pairs contained in this object. 
        let inline entries (sp: URLSearchParams) = sp.Entries()

        /// Iterates through all keys and values in the object, respectively.
        let inline forEach (f: string -> string -> unit) (sp: URLSearchParams) = sp.ForEach(f)
        
        /// Returns the first value associated to the given search parameter.
        let inline get (key: string) (sp: URLSearchParams) = sp.Get(key)
        
        /// Returns all values associated to the given search parameter.
        let inline getAll (key: string) (sp: URLSearchParams) = sp.GetAll(key)
        
        /// Returns a Boolean that indicates whether a parameter with the specified name exists.
        let inline has (key: string) (sp: URLSearchParams) = sp.Has(key)
        
        /// Returns a sequence of all keys contained in this object.
        let inline keys (sp: URLSearchParams) = sp.Keys()
        
        /// Sets the value associated with a given search parameter to the given value. 
        ///
        /// If there were several matching values, this method deletes the others. 
        ///
        /// If the search parameter doesn't exist, this method creates it.
        let inline set (key: string) (value: string) (sp: URLSearchParams) = 
            sp.Set(key, value)
            sp

        /// Sorts all key/value pairs contained in this object in place. 
        /// The sort order is according to unicode code points of the keys. 
        ///
        /// This method uses a stable sorting algorithm (i.e. the relative order between 
        /// key/value pairs with equal keys will be preserved).
        let inline sort (sp: URLSearchParams) = 
            sp.Sort()
            sp

        let inline toString (sp: URLSearchParams) = sp.ToString()
        
        /// Returns a sequence of all values of the key/value pairs contained in this object.
        let inline values (sp: URLSearchParams) = sp.Values()

    /// <summary>
    /// Used to parse, construct, normalize, and encode URLs. 
    ///
    /// It works by providing properties which allow you to easily read and modify 
    /// the components of a URL.
    /// </summary>
    /// <exception cref="System.Exception">url (in the case of absolute URLs) or base + url 
    /// (in the case of relative URLs) is not a valid URL.</exception>
    [<Global>]
    type URL private (o: obj) =
        [<Emit("new URL($0...)")>]
        new (url: string, ?base': string) = URL(url)

        [<Emit("$0")>]
        new (url: Browser.Types.URL) = URL(url)

        /// A string containing a '#' followed by the fragment identifier of the URL.
        /// 
        /// The fragment is not percent-decoded. If the URL does not have a fragment 
        /// identifier, this property contains an empty string - "".
        member _.Hash
            with [<Emit("$0.hash")>] get () : string = jsNative
            and [<Emit("$0.hash = $1")>] set (x: string) = jsNative
            
        /// A string containing the domain (that is the hostname) followed by (if a 
        /// port was specified) a ':' and the port of the URL.
        member _.Host
            with [<Emit("$0.host")>] get () : string = jsNative
            and [<Emit("$0.host = $1")>] set (x: string) = jsNative

        /// A string containing the domain of the URL.
        member _.HostName
            with [<Emit("$0.hostName")>] get () : string = jsNative
            and [<Emit("$0.hostName = $1")>] set (x: string) = jsNative

        /// A stringifier that returns a string containing the whole URL.
        member _.Href
            with [<Emit("$0.href")>] get () : string = jsNative
            and [<Emit("$0.href = $1")>] set (x: string) = jsNative

        /// A string containing the origin of the URL, that is its scheme, its domain and its port.
        [<Emit("$0.origin")>]
        member _.Origin : string = jsNative

        /// A string containing the password specified before the domain name.
        member _.Password
            with [<Emit("$0.password")>] get () : string = jsNative
            and [<Emit("$0.password = $1")>] set (x: string) = jsNative

        /// A string containing an initial '/' followed by the path of the URL.
        member _.Pathname
            with [<Emit("$0.pathname")>] get () : string = jsNative
            and [<Emit("$0.pathname = $1")>] set (x: string) = jsNative

        /// A string containing the port number of the URL.
        member _.Port
            with [<Emit("$0.port")>] get () : string = jsNative
            and [<Emit("$0.port = $1")>] set (x: string) = jsNative

        /// A string containing the protocol scheme of the URL, including the final ':'.
        member _.Protocol
            with [<Emit("$0.protocol")>] get () : string = jsNative
            and [<Emit("$0.protocol = $1")>] set (x: string) = jsNative

        /// A string indicating the URL's parameter string; if any parameters are provided, 
        /// this string includes all of them, beginning with the leading ? character.
        member _.Search
            with [<Emit("$0.search")>] get () : string = jsNative
            and [<Emit("$0.search = $1")>] set (x: string) = jsNative

        /// A URLSearchParams object which can be used to access the individual query 
        /// parameters found in search.
        [<Emit("$0.searchParams")>]
        member _.SearchParams : URLSearchParams = jsNative

        /// A string containing the username specified before the domain name.
        member _.Username
            with [<Emit("$0.username")>] get () : string = jsNative
            and [<Emit("$0.username = $1")>] set (x: string) = jsNative

        [<Emit("$0.toString()")>]
        override _.ToString () : string = jsNative

        /// Returns a string containing the whole URL. 
        ///
        /// It returns the same string as the href property.
        [<Emit("$0.toJSON()")>]
        member _.ToJSON () : string = jsNative

        /// Creates a string containing a URL representing the object given in the parameter. 
        ///
        /// The URL lifetime is tied to the document in the window on which it was created.
        ///
        /// Each time you call createObjectURL, a new object URL is created, even if you've 
        /// already created one for the same object. Each of these must be released by 
        /// calling URL.revokeObjectURL when you no longer need them.
        static member createObjectURL (o: Browser.Types.File) : string = jsNative
        /// Creates a string containing a URL representing the object given in the parameter. 
        ///
        /// The URL lifetime is tied to the document in the window on which it was created. 
        ///
        /// Each time you call createObjectURL, a new object URL is created, even if you've 
        /// already created one for the same object. Each of these must be released by 
        /// calling URL.revokeObjectURL when you no longer need them.
        static member createObjectURL (o: Browser.Types.Blob) : string = jsNative
        /// Creates a string containing a URL representing the object given in the parameter. 
        ///
        /// The URL lifetime is tied to the document in the window on which it was created. 
        ///
        /// Each time you call createObjectURL, a new object URL is created, even if you've 
        /// already created one for the same object. Each of these must be released by 
        /// calling URL.revokeObjectURL when you no longer need them.
        static member createObjectURL (o: JSe.MediaSource) : string = jsNative

        /// Releases an existing object URL which was previously created by calling 
        /// URL.createObjectURL. Call this method when you've finished using an object URL to 
        /// let the browser know not to keep the reference to the file any longer.
        static member revokeObjectURL (objURL: string) : unit = jsNative

        interface Browser.Types.URL with
            member this.hash
                with get () = this.Hash
                and set x = this.Hash <- x
            member this.host
                with get () = this.Host
                and set x = this.Host <- x
            member this.hostname
                with get () = this.HostName
                and set x = this.HostName <- x
            member this.href
                with get () = this.Href
                and set x = this.Href <- x
            member this.origin = this.Origin
            member this.password
                with get () = this.Password
                and set x = this.Password <- x
            member this.pathname
                with get () = this.Pathname
                and set x = this.Pathname <- x
            member this.port
                with get () = this.Port
                and set x = this.Port <- x
            member this.protocol
                with get () = this.Protocol
                and set x = this.Protocol <- x
            member this.search
                with get () = this.Search
                and set x = this.Search <- x
            member this.username
                with get () = this.Username
                and set x = this.Username <- x
            member this.searchParams = upcast this.SearchParams
            member this.toString () = this.ToString()
            member this.toJSON () = this.ToJSON()

    [<Erase;RequireQualifiedAccess>]
    module URL =
        /// A string containing a '#' followed by the fragment identifier of the URL.
        /// 
        /// The fragment is not percent-decoded. If the URL does not have a fragment 
        /// identifier, this property contains an empty string - "".
        let inline hash (url: URL) = url.Hash

        /// A string containing a '#' followed by the fragment identifier of the URL.
        /// 
        /// The fragment is not percent-decoded. If the URL does not have a fragment 
        /// identifier, this property contains an empty string - "".
        let inline setHash (hash: string) (url: URL) = 
            url.Hash <- hash
            url
            
        /// A string containing the domain (that is the hostname) followed by (if a 
        /// port was specified) a ':' and the port of the URL.
        let inline host (url: URL) = url.Host
        
        /// A string containing the domain (that is the hostname) followed by (if a 
        /// port was specified) a ':' and the port of the URL.
        let inline setHost (host: string) (url: URL) = 
            url.Host <- host
            url

        /// A string containing the domain of the URL.
        let inline hostName (url: URL) = url.HostName

        /// A string containing the domain of the URL.
        let inline setHostName (hostName: string) (url: URL) = 
            url.HostName <- hostName
            url

        /// A stringifier that returns a string containing the whole URL.
        let inline href (url: URL) = url.Href

        /// A stringifier that returns a string containing the whole URL.
        let inline setHref (href: string) (url: URL) = 
            url.Href <- href
            url

        /// A string containing the origin of the URL, that is its scheme, its domain and its port.
        let inline origin (url: URL) = url.Origin

        /// A string containing the password specified before the domain name.
        let inline password (url: URL) = url.Password
        
        /// A string containing the password specified before the domain name.
        let inline setPassword (password: string) (url: URL) = 
            url.Password <- password
            url

        /// A string containing an initial '/' followed by the path of the URL.
        let inline pathname (url: URL) = url.Pathname

        /// A string containing an initial '/' followed by the path of the URL.
        let inline setPathname (pathname: string) (url: URL) = 
            url.Pathname <- pathname
            url

        /// A string containing the port number of the URL.
        let inline port (url: URL) = url.Port

        /// A string containing the port number of the URL.
        let inline setPort (port: int) (url: URL) = 
            url.Port <- unbox<string> port
            url

        /// A string containing the protocol scheme of the URL, including the final ':'.
        let inline protocol (url: URL) = url.Protocol

        /// A string containing the protocol scheme of the URL, including the final ':'.
        let inline setProtocol (protocol: string) (url: URL) = 
            url.Protocol <- protocol
            url

        /// A string indicating the URL's parameter string; if any parameters are provided, 
        /// this string includes all of them, beginning with the leading ? character.
        let inline search (url: URL) = url.Search

        /// A string indicating the URL's parameter string; if any parameters are provided, 
        /// this string includes all of them, beginning with the leading ? character.
        let inline setSearch (search: string) (url: URL) = 
            url.Search <- search
            url

        /// A URLSearchParams object which can be used to access the individual query 
        /// parameters found in search.
        let inline searchParams (url: URL) = url.SearchParams

        /// A string containing the username specified before the domain name.
        let inline username (url: URL) = url.Username
        
        /// A string containing the username specified before the domain name.
        let inline setUsername (username: string) (url: URL) = 
            url.Username <- username
            url

        let inline toString (url: URL) = url.ToString()

        /// Returns a string containing the whole URL. 
        ///
        /// It returns the same string as the href property.
        let inline toJSON (url: URL) = url.ToJSON()
