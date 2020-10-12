# Math

A built-in object that has properties and methods for mathematical constants and functions.

## E

Represents the base of natural logarithms, e, approximately 2.718.

Signature:
```fsharp
float
```

## LN10

Represents the natural logarithm of 10, approximately 2.302.

Signature:
```fsharp
float
```

## LN2

Represents the natural logarithm of 2, approximately 0.693.

Signature:
```fsharp
float
```

## LOG2E

Represents the base 2 logarithm of e, approximately 1.442.

Signature:
```fsharp
float
```

## LOG10E

Represents the base 10 logarithm of e, approximately 0.434.

Signature:
```fsharp
float
```

## PI

Represents the ratio of the circumference of a circle to its diameter, approximately 3.14159.

Signature:
```fsharp
float
```

## SQRT1_2

Represents the square root of 1/2 which is approximately 0.707.

Signature:
```fsharp
float
```

## SQRT2

Represents the square root of 2, approximately 1.414.

Signature:
```fsharp
float
```

## abs

Returns the absolute value of a number.

Signature:
```fsharp
(x: decimal) -> decimal
(x: float) -> float
(x: int16) -> int16
(x: int) -> int
(x: int64) -> int64
(x: sbyte) -> sbyte
```

## acos

Returns the arccosine (in radians) of a number.

Signature:
```fsharp
(x: float) -> float
```

## acosh

Returns the hyperbolic arc-cosine of a number.

Signature:
```fsharp
(x: float) -> float
```

## asin

Returns the arcsine (in radians) of a number.

Signature:
```fsharp
(x: float) -> float
```

## asinh

Returns the hyperbolic sine of a number.

Signature:
```fsharp
(x: float) -> float
```

## atan

Returns the arctangent (in radians) of a number.

Signature:
```fsharp
(x: float) -> float
```

## atanh

Returns the hyperbolic arctangent of a number.

Signature:
```fsharp
(x: float) -> float
```

## atan2

Returns the angle in the plane (in radians) between the 
positive x-axis and the ray from (0,0) to the point (x,y).

Signature:
```fsharp
(y: float, x: float) -> float
```

## cbrt

Returns the cube root of a number.

Signature:
```fsharp
(x: float) -> float
```

## ceil

Always rounds a number up to the next largest integer.

Signature:
```fsharp
(x: decimal) -> int
(x: float) -> int
```

## clz32

Returns the number of leading zero bits in the 32-bit binary representation of a number.

Signature:
```fsharp
(x: float) -> float
```

## cos

Returns the cosine of the specified angle, which must be specified in radians.

Signature:
```fsharp
(x: float) -> float
```

## cosh

Returns the hyperbolic cosine of a number.

Signature:
```fsharp
(x: float) -> float
```

## exp

Returns e^x, where x is the argument, and e is Euler's number (also known as Napier's constant), 
the base of the natural logarithms.

Signature:
```fsharp
(x: float) -> float
```

## expm1

Returns ex - 1, where x is the argument, and e the base of the natural logarithms.

Signature:
```fsharp
(x: float) -> float
```

## floor

Returns the largest integer less than or equal to a given number.

Signature:
```fsharp
(x: decimal) -> int
(x: float) -> int
```

## fround

Returns the nearest 32-bit single precision float representation of a Number.

Signature:
```fsharp
(x: float) -> float32
(x: float32) -> float32
```

## hypot

Returns the square root of the sum of squares of its arguments.

Signature:
```fsharp
([<ParamArray>] values: float []) -> float
```

## imul

Returns the result of the C-like 32-bit multiplication of the two parameters.

Signature:
```fsharp
(x: float, y: float) -> float
```

## log

Returns the natural logarithm (base e) of a number.

Signature:
```fsharp
(x: float) -> float
```

## log10

Returns the base 10 logarithm of a number.

Signature:
```fsharp
(x: float) -> float
```

## log1p

Returns the natural logarithm (base e) of 1 + a number.

Signature:
```fsharp
(x: float) -> float
```

## log2

Returns the base 2 logarithm of a number.

Signature:
```fsharp
(x: float) -> float
```

## max

Returns the largest of the zero or more numbers given as input parameters.

Signature:
```fsharp
([<ParamArray>] values: byte []) -> byte
([<ParamArray>] values: decimal []) -> decimal
([<ParamArray>] values: float []) -> float
([<ParamArray>] values: int16 []) -> int16
([<ParamArray>] values: int []) -> int
([<ParamArray>] values: int64 []) -> int64
([<ParamArray>] values: sbyte []) -> sbyte
([<ParamArray>] values: uint16 []) -> uint16
([<ParamArray>] values: uint32 []) -> uint32
([<ParamArray>] values: uint64 []) -> uint64
```

## min

Returns the lowest of the zero or more numbers given as input parameters.

Signature:
```fsharp
([<ParamArray>] values: byte []) -> byte
([<ParamArray>] values: decimal []) -> decimal
([<ParamArray>] values: float []) -> float
([<ParamArray>] values: int16 []) -> int16
([<ParamArray>] values: int []) -> int
([<ParamArray>] values: int64 []) -> int64
([<ParamArray>] values: sbyte []) -> sbyte
([<ParamArray>] values: uint16 []) -> uint16
([<ParamArray>] values: uint32 []) -> uint32
([<ParamArray>] values: uint64 []) -> uint64
```

## pow

Returns the base to the exponent power.

Signature:
```fsharp
(x: float, y: float) -> float
```

## random

Returns a floating-point, pseudo-random number in the range 0 to less than 1 
(inclusive of 0, but not 1) with approximately uniform distribution over that range.

Signature:
```fsharp
unit -> float
```

## round

Returns the value of a number rounded to the nearest integer.

Signature:
```fsharp
(x: decimal) -> int
(x: float) -> int
```

## sign

Returns either a positive or negative +/- 1, indicating the sign of a number passed into 
the argument. 

If the number passed into Math.sign() is 0, it will return a +/- 0. 

Signature:
```fsharp
(x: decimal) -> int
(x: float32) -> int
(x: float) -> int
(x: int16) -> int
(x: int) -> int
(x: int64) -> int
(x: sbyte) -> int
```

## sin

Returns the sine of a number.

Signature:
```fsharp
(x: float) -> float
```

## sinh

Returns the hyperbolic sine of a number.

Signature:
```fsharp
(x: float) -> float
```

## sqrt

Returns the square root of a number.

Signature:
```fsharp
(x: float) -> float
```

## tan

Returns the tangent of a number.

Signature:
```fsharp
(x: float) -> float
```

## tanh

Returns the hyperbolic tangent of a number.

Signature:
```fsharp
(x: float) -> float
```

## trunc

Returns the integer part of a number by removing any fractional digits.

Signature:
```fsharp
(x: decimal) -> int
(x: float) -> int
```
