# Proxy

There are two types of `Proxy`:

The normal "erased" proxy `Proxy<'T>`.

```fsharp
type Proxy<'T> = 'T
```

The `RevocableProxy<'T>` which can be disabled.

```fsharp
type RevocableProxy<'T> =
    /// The proxied item.
    member proxy : Proxy<'T>

    /// Invalidates the proxy.
    ///
    /// Any future calls to the proxy will throw an exception after this
    /// has been called.
    member revoke () : unit
```

The functions outlined below are located in the `Proxy` module.

## create

Creates a proxy from the specified handler.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> Proxy<'T>
```

## createRevocable

Creates a proxy from the specified handler that can be disabled.

Signature:
```fsharp
(ph: ProxyHandler<'T>) -> RevocableProxy<'T>
```
