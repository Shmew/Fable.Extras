# Reflect

Provides methods for interceptable operations.

The functions outlined below are located in `Reflect`.

## apply

Calls a target function with arguments as specified by the arguments parameter.

Has 2-7 variants for additional arity.

Signature:
```fsharp
(target: 'a -> 'b) -> (thisArg: obj) -> (arguments: obj []) -> 'b
(target: 'a -> 'b -> 'c) -> (thisArg: obj) -> (arguments: obj []) -> 'c
(target: 'a -> 'b -> 'c -> 'd) -> (thisArg: obj) -> (arguments: obj []) -> 'd
...
```

## construct

Acts like the new operator, but as a function.

Has 2-7 variants for additional arity.

Signature:
```fsharp
(target: 'a -> 'b) -> (arguments: obj []) -> 'b
(target: 'a -> 'b -> 'c) -> (arguments: obj []) -> 'c
(target: 'a -> 'b -> 'c -> 'd) -> (arguments: obj []) -> 'd
...
```

## constructNewTarget

Calls a target function with arguments as specified by the arguments parameter.

Has 2-7 variants for additional arity.

Signature:
```fsharp
(target: 'a -> 'b) -> (arguments: obj []) -> (newTarget: 'a -> 'b) -> 'b
(target: 'a -> 'b -> 'c) -> (arguments: obj []) -> (newTarget: 'a -> 'b -> 'c) -> 'c
(target: 'a -> 'b -> 'c -> 'd) -> (arguments: obj []) -> (newTarget: 'a -> 'b -> 'c -> 'd) -> 'd
...
```

## defineProperty

Defines a new property directly on an object, or modifies an existing 
property on an object, and returns the object.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> (propertyDescriptor: #JS.PropertyDescriptor) -> bool
```

## deleteProperty

The delete operator as a function. Equivalent to calling delete target[propertyKey].

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> (propertyDescriptor: #JS.PropertyDescriptor) -> bool
```

## get

Returns the value of the property. Works like getting a property from an object (target[propertyKey]) as a function.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> 'U
```

## getWithReceiver

Returns the value of the property. Works like getting a property from an object (target[propertyKey]) as a function.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> (receiver: obj) -> 'U
```

## getOwnPropertyDescriptor

Returns a property descriptor of the given property if it exists on the object or None.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> JSe.PropertyDescriptor<'U> option
```

## getPrototypeOf

Returns the prototype (i.e. the value of the internal [[Prototype]] property) of the specified object.

Signature:
```fsharp
(target: 'T) -> 'U
```

## has

Returns a Boolean indicating whether the target has the property. Either as own or inherited. 

Works like the in operator as a function.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> bool
```

## isExtensible

Returns a Boolean that is true if the target is extensible.

Signature:
```fsharp
(target: 'T) -> bool
```

## ownKeys

Returns an array of the target object's own (not inherited) property keys.

Signature:
```fsharp
(target: 'T) -> string []
```

## preventExtensions

Prevents new properties from ever being added to an object (i.e. prevents future extensions to the object).

Signature:
```fsharp
(target: 'T) -> bool
```

## set

Assigns values to properties. Returns a Boolean that is true if the update was successful.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> (value: 'U) -> bool
```

## set

Assigns values to properties. Returns a Boolean that is true if the update was successful.

Signature:
```fsharp
(target: 'T) -> (propertyName: string) -> (value: 'U) -> (receiver: obj) -> bool
```

## setPrototypeOf

It sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to another object, 
and returns true if the operation was successful, or false otherwise.

Signature:
```fsharp
(target: 'T) -> (prototype: 'U) -> bool
```

## setPrototypeOfNull

It sets the prototype (i.e., the internal [[Prototype]] property) of a specified object to another object, 
and returns true if the operation was successful, or false otherwise.

Signature:
```fsharp
(target: 'T) -> bool
```
