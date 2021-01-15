# Intersection Observer

Provides a way to asynchronously observe changes in the intersection of 
a target element with an ancestor element or with a top-level document's 
viewport.

```fsharp
type IntersectionObserver<'Root when 'Root :> Node and 'Root :> NodeSelector and 'Root :> GlobalEventHandlers> =
    /// <param name="callback">
    /// A callback function to be run whenever a threshold is crossed in one direction or the other.
    /// </param>
    /// <param name="root">
    /// The document or element that is used as the viewport for checking visibility of the target. 
    ///
    /// Must be the ancestor of the target. 
    ///
    /// Defaults to the browser viewport if not specified.
    /// </param>
    /// <param name="rootMargin">
    /// Margin around the root. 
    ///
    /// Can have values similar to the CSS margin property, e.g. "10px 20px 30px 40px" (top, right, bottom, left). 
    ///
    /// The values can be percentages. 
    ///
    /// This set of values serves to grow or shrink each side of the root element's bounding box before computing 
    /// intersections. 
    ///
    /// Defaults to all zeros.
    /// </param>
    /// <param name="threshold">
    /// A single number which indicates at what percentage of the target's visibility the observer's callback 
    /// should be executed. 
    ///
    /// If you only want to detect when visibility passes the 50% mark, you can use a value of 0.5. 
    ///
    /// The default is 0 (meaning as soon as even one pixel is visible, the callback will be run). 
    ///
    /// A value of 1.0 means that the threshold isn't considered passed until every pixel is visible.
    /// </param>
    new (callback: IntersectionObserverEntry list -> IntersectionObserver<'Root> -> unit, ?root: 'Root, ?rootMargin: string, ?threshold: float) = IntersectionObserver()
        
    /// <param name="callback">
    /// A callback function to be run whenever a threshold is crossed in one direction or the other.
    /// </param>
    /// <param name="root">
    /// The element that is used as the viewport for checking visibility of the target. 
    ///
    /// Must be the ancestor of the target. 
    ///
    /// Defaults to the browser viewport if not specified.
    /// </param>
    /// <param name="rootMargin">
    /// Margin around the root. 
    ///
    /// Can have values similar to the CSS margin property, e.g. "10px 20px 30px 40px" (top, right, bottom, left). 
    ///
    /// The values can be percentages. 
    ///
    /// This set of values serves to grow or shrink each side of the root element's bounding box before computing 
    /// intersections. 
    ///
    /// Defaults to all zeros.
    /// </param>
    /// <param name="threshold">
    /// A sequence of numbers which indicate at what percentage of the target's visibility the observer's callback should be executed. 
    ///
    // If you want the callback to run every time visibility passes another 25%, you would specify a seqeunce like: [0.; 0.25; 0.5; 0.75; 1.]. 
    ///
    /// The default is 0 (meaning as soon as even one pixel is visible, the callback will be run). 
    ///
    /// A value of 1.0 means that the threshold isn't considered passed until every pixel is visible.
    /// </param>
    new (callback: IntersectionObserverEntry list -> IntersectionObserver<'Root> -> unit, ?root: 'Root, ?rootMargin: string, ?threshold: seq<float>) = IntersectionObserver()
        
    /// The Element or Document whose bounds are used as the bounding box when testing for intersection. 
    /// 
    /// If no root value was passed to the constructor or its value is None, the top-level document's viewport is used.
    member Root : 'Root option

    /// An offset rectangle applied to the root's bounding box when calculating intersections, effectively shrinking 
    /// or growing the root for calculation purposes. 
    ///
    /// The value returned by this property may not be the same as the one specified when calling the constructor as it 
    /// may be changed to match internal requirements. 
    ///
    /// Each offset can be expressed in pixels (px) or as a percentage (%). The default is "0px 0px 0px 0px".
    member RootMargin : string

    /// A sequence of thresholds, sorted in increasing numeric order, where each threshold is a ratio of intersection area to bounding box 
    /// area of an observed target. 
    ///
    /// Notifications for a target are generated when any of the thresholds are crossed for that target. 
    ///
    /// If no value was passed to the constructor, 0 is used.
    member Thresholds : seq<float>

    /// Stops the IntersectionObserver object from observing any target.
    member Disconnect () : unit

    /// Tells the IntersectionObserver a target element to observe.
    member Observe (element: #Element) : unit

    /// Returns an array of IntersectionObserverEntry objects for all observed targets.
    member TakeRecords () : seq<IntersectionObserverEntry>

    /// Tells the IntersectionObserver to stop observing a particular target element.
    member Unobserve (element: #Element) : unit

    /// Creates an IDisposable that disconnects the IntersectionObserver when disposed.
    member GetDisposable () : System.IDisposable
```

The functions outlined below are located in the `IntersectionObserver` module.

## root

The Element or Document whose bounds are used as the bounding box when testing for intersection. 

If no root value was passed to the constructor or its value is None, the top-level document's viewport is used.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> 'Root
```

## rootMargin

An offset rectangle applied to the root's bounding box when calculating intersections, effectively shrinking 
or growing the root for calculation purposes. 

The value returned by this property may not be the same as the one specified when calling the constructor as it 
may be changed to match internal requirements. 

Each offset can be expressed in pixels (px) or as a percentage (%). The default is "0px 0px 0px 0px".

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> string
```

## thresholds

A sequence of thresholds, sorted in increasing numeric order, where each threshold is a ratio of intersection area to bounding box 
area of an observed target. 

Notifications for a target are generated when any of the thresholds are crossed for that target. 

If no value was passed to the constructor, 0 is used.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> seq<float>
```

## disconnect

Stops the IntersectionObserver object from observing any target.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> IntersectionObserver<'Root>
```

## observe

Tells the IntersectionObserver a target element to observe.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> (element: #Element) -> IntersectionObserver<'Root>
```

## takeRecords

Returns an array of IntersectionObserverEntry objects for all observed targets.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> seq<IntersectionObserverEntry>
```

## unobserve

Tells the IntersectionObserver to stop observing a particular target element.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> (element: #Element) -> IntersectionObserver<'Root>
```

## getDisposable

Creates an IDisposable that disconnects the IntersectionObserver when disposed.

Signature:
```fsharp
(observer: IntersectionObserver<'Root>) -> System.IDisposable
```
