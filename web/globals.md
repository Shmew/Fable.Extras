# Globals

Global web related functions.

## encodeURI

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

<Note type="tip">encodeURI by itself cannot form proper HTTP, GET, and POST 
requests, such as for XMLHttpRequest requests. encodeURIComponent, 
however, does encode these characters.</Note>

Signature:
```fsharp
(s: string) -> string
```

## tryEncodeURI

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

<Note type="tip">encodeURI by itself cannot form proper HTTP, GET, and POST 
requests, such as for XMLHttpRequest requests. encodeURIComponent, 
however, does encode these characters.</Note>

Signature:
```fsharp
(s: string) -> Result<string,exn>
```

## encodeURIIPv6

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

This implementation follows [RFC3986](https://tools.ietf.org/html/rfc3986) by encoding brackets reserved for IPv6.

<Note type="tip">encodeURI by itself cannot form proper HTTP, GET, and POST 
requests, such as for XMLHttpRequest requests. encodeURIComponent, 
however, does encode these characters.</Note>

Signature:
```fsharp
(s: string) -> string
```

## tryEncodeURIIPv6

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

This implementation follows [RFC3986](https://tools.ietf.org/html/rfc3986) by encoding brackets reserved for IPv6.

<Note type="tip">encodeURI by itself cannot form proper HTTP, GET, and POST 
requests, such as for XMLHttpRequest requests. encodeURIComponent, 
however, does encode these characters.</Note>

Signature:
```fsharp
(s: string) -> Result<string,exn>
```

## encodeURIComponent

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

Signature:
```fsharp
(s: string) -> string
```

## tryEncodeURIComponent

Encodes a URI by replacing each instance of certain characters by 
one, two, three, or four escape sequences representing the UTF-8 
encoding of the character (will only be four escape sequences for 
characters composed of two "surrogate" characters).

Signature:
```fsharp
(s: string) -> Result<string,exn>
```

## decodeURI

Decodes a Uniform Resource Identifier (URI) previously created by 
[encodeURI](#encodeuri) or by a similar routine.

Signature:
```fsharp
(s: string) -> string
```

## tryDecodeURI

Decodes a Uniform Resource Identifier (URI) previously created by 
[encodeURI](#encodeuri) or by a similar routine.

Signature:
```fsharp
(s: string) -> Result<string,exn>
```

## decodeURIComponent

Decodes a Uniform Resource Identifier (URI) component previously 
created by [encodeURIComponent](#encodeuricomponent) or by a similar routine.

Signature:
```fsharp
(s: string) -> string
```

## tryDecodeURIComponent

Decodes a Uniform Resource Identifier (URI) component previously 
created by [encodeURIComponent](#encodeuricomponent) or by a similar routine.

Signature:
```fsharp
(s: string) -> Result<string,exn>
```
