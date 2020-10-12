# Regular Expressions

There are a couple types that go alongside `RegExp`:

## RegExpFlag

Helper to safetly construct regex flags.

```fsharp
type RegExpFlag () =
    /// Global match
    ///
    /// Find all matches rather than stopping after the first match.
    member g : RegExpFlag
        
    /// Ignore case
    ///
    /// If u flag is also enabled, usees Unicode case folding.
    member i : RegExpFlag

    /// Multiline
    ///
    /// Treat beginning and end characters (^ and $) as working over multiple 
    /// lines. 
    ///
    /// In other words, match the beginning or end of each line (delimited by \n 
    /// or \r), not only the very beginning or end of the whole input string.
    member m : RegExpFlag
        
    /// Dot All
    ///
    /// Allows . to match newlines.
    member s : RegExpFlag
        
    /// Unicode
    ///
    /// Treat pattern as a sequence of Unicode code points.
    member u : RegExpFlag
        
    /// Sticky
    ///
    /// Matches only from the index indicated by the LastIndex property of this 
    /// regular expression in the target string. 
    ///
    /// Does not attempt to match from any later indexes.
    member y : RegExpFlag
```

You can use this like so:

```fsharp
JSe.RegExp(myPattern, RegExpFlag().g.m)
// or
JSe.RegExp(myPattern, JSe.RegExp.flag.g.m)
```

## RegExpReplacer

Represents an object that some `RegExp` methods return/use.

```fsharp
type RegExpReplacer =
    member match' : string
    member captures : string []
    member offset : int
    member string : string
    member groups : (string * string) [] option
```

## RegExp

```fsharp
type RegExp =
        new (pattern: string) = RegExp(pattern)
        new (pattern: string, flags: string) = RegExp(pattern, flags = flags)
        new (pattern: string, flags: RegExpFlag) = RegExp(pattern, flags = flags)

        /// Converts this object to the System.Text.RegularExpressions representation.
        ///
        /// No runtime cost.
        member AsRegex () : Regex

        /// Indicates whether or not the "s" flag is used with this regular expression.
        member DotAll : bool

        /// Executes a search for a match in a specified string.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        member Exec (value: string) : seq<string> option

        /// Returns a string consisting of the flags used with this regular expression.
        member Flags : string
        
        /// Indicates whether or not the "g" flag is used with this regular expression.
        member Global : bool

        /// Indicates whether or not the "i" flag is used with this regular expression.
        member IgnoreCase : bool
        
        /// A mutable integer property that specifies the index at which to start the next match.
        ///
        /// This property is set only if the regular expression instance used the g flag to 
        /// indicate a global search, or the y flag to indicate a sticky search.
        ///
        /// If LastIndex is greater than the length of the string, test() and exec() fail, then 
        /// LastIndex is set to 0.
        ///
        /// If LastIndex is equal to or less than the length of the string and if the regular 
        /// expression matches the empty string, then the regular expression matches input 
        /// starting from LastIndex.
        ///
        /// If LastIndex is equal to the length of the string and if the regular expression 
        /// does not match the empty string, then the regular expression mismatches input, 
        /// and LastIndex is reset to 0.
        ///
        /// Otherwise, LastIndex is set to the next position following the most recent match.
        member LastIndex
            with get () : int
            and set (x: int) : unit

        /// Retrieves the matches when matching a string against the regular expression.
        member Match (value: string) : seq<string>
        
        /// Retrieves all matches when matching a string against the regular expression.
        member MatchAll (value: string) : seq<seq<string>>

        /// Indicates whether or not the "m" flag is used with this regular expression.
        member Multiline : bool
        
        /// Returns a new string with some or all matches of a pattern replaced by a replacement.
        member Replace (source: string, newSubString: string) : string
        member Replace (source: string, replacer: RegExpReplacer -> string) : string
        
        /// Returns the number of matches found in a given string.
        member Search (source: string) : int

        /// Pattern of the regular expression without any forward slashes or flags.
        member Source : string
        
        /// Indicates whether or not the "y" flag is used with this regular expression.
        member Sticky : bool
        
        /// Executes a search for a match in a specified string and returns if it was 
        /// successful or not.
        ///
        /// Be aware that JS regular expressions are *stateful* when using the global 
        /// or sticky flags.
        member Test (value: string) : bool

        /// Returns a string representing the regular expression.
        override ToString () : string

        /// Indicates whether or not the "u" flag is used with this regular expression.
        member Unicode : bool
```

The functions outlined below are located in the `RegExp` module.

## flag

Creates an empty RegExpFlag

Signature:
```fsharp
RegExpFlag
```

## asRegex

Converts a JS regular expression to the System.Text.RegularExpressions representation.
No runtime cost.

Signature:
```fsharp
(re: RegExp) -> Regex
```

## dotAll

Indicates whether or not the "s" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```

## exec

Executes a search for a match in a specified string.

Be aware that JS regular expressions are *stateful* when using the global 
or sticky flags.

Signature:
```fsharp
(value: string) -> (re: RegExp) -> seq<string> option
```

## flags

Returns a string consisting of the flags used with this regular expression.

Signature:
```fsharp
(re: RegExp) -> string
```

## fromRegex

Converts this object to the System.Text.RegularExpressions representation.

No runtime cost.

Signature:
```fsharp
(re: Regex) -> RegExp
```

## global'

Indicates whether or not the "g" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```

## ignoreCase

Indicates whether or not the "i" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```

## lastIndex

A mutable integer property that specifies the index at which to start the next match.

This property is set only if the regular expression instance used the g flag to 
indicate a global search, or the y flag to indicate a sticky search.

If LastIndex is greater than the length of the string, test() and exec() fail, then 
LastIndex is set to 0.

If LastIndex is equal to or less than the length of the string and if the regular expression 
matches the empty string, then the regular expression matches input starting from LastIndex.

If LastIndex is equal to the length of the string and if the regular expression does not match 
the empty string, then the regular expression mismatches input, and LastIndex is reset to 0.

Otherwise, LastIndex is set to the next position following the most recent match.

Signature:
```fsharp
(re: RegExp) -> int
```

## setLastIndex

A mutable integer property that specifies the index at which to start the next match.

This property is set only if the regular expression instance used the g flag to 
indicate a global search, or the y flag to indicate a sticky search.

If LastIndex is greater than the length of the string, test() and exec() fail, then 
LastIndex is set to 0.

If LastIndex is equal to or less than the length of the string and if the regular expression 
matches the empty string, then the regular expression matches input starting from LastIndex.

If LastIndex is equal to the length of the string and if the regular expression does not match 
the empty string, then the regular expression mismatches input, and LastIndex is reset to 0.

Otherwise, LastIndex is set to the next position following the most recent match.

Signature:
```fsharp
(value: int) -> (re: RegExp) -> RegExp
```

## match'

Retrieves the matches when matching a string against a regular expression.

Signature:
```fsharp
(value: string) -> (re: RegExp) -> seq<string>
```

## matchAll

Retrieves all matches when matching a string against a regular expression.

Signature:
```fsharp
(value: string) -> (re: RegExp) -> seq<seq<string>>
```

## multiline

Indicates whether or not the "m" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```

## replace

Returns a new string with some or all matches of a pattern replaced by a replacement.

Signature:
```fsharp
(source: string) -> (newSubString: string) -> (re: RegExp) -> string
```

## replaceFun

Returns a new string with some or all matches of a pattern replaced by a replacement.

Signature:
```fsharp
(source: string) -> (replacer: RegExpReplacer -> string) -> (re: RegExp) -> string
```

## search

Returns the number of matches found in a given string.

Signature:
```fsharp
(source: string) -> (re: RegExp) -> int
```

## source

Pattern of the regular expression without any forward slashes or flags.

Signature:
```fsharp
(re: RegExp) -> string
```

## sticky

Indicates whether or not the "y" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```

## test

Executes a search for a match in a specified string and returns if it was successful or not.

Be aware that JS regular expressions are *stateful* when using the global 
or sticky flags.

Signature:
```fsharp
(value: string) -> (re: RegExp) -> bool
```

## toString

Returns a string representing the regular expression.

Signature:
```fsharp
(re: RegExp) -> string
```

## unicode

Indicates whether or not the "u" flag is used with a regular expression.

Signature:
```fsharp
(re: RegExp) -> bool
```
