# Extensions

By opening `Fable.Extras` the following types will be extended with the 
features listed below.

## AsyncBuilder

Overloads to bind promises.

## PromiseBuilder

Overloads to bind async computations.

## String

```fsharp
type System.String with
    /// Retrieves the matches when matching a string against a regular expression.
    member Match (regExp: JSe.RegExp) : seq<string>
    /// Retrieves the matches when matching a string against a regular expression.
    member Match (regex: Regex) : seq<string>

    /// Retrieves all matches when matching a string against a regular expression.
    member MatchAll (regExp: JSe.RegExp) : seq<seq<string>>
    /// Retrieves all matches when matching a string against a regular expression.
    member MatchAll (regex: Regex) : seq<seq<string>>
        
    /// Returns a new string with some or all matches of a pattern replaced by a replacement.
    member Replace (regExp: JSe.RegExp, newSubString: string) : string
    /// Returns a new string with some or all matches of a pattern replaced by a replacement.
    member Replace (regex: Regex, newSubString: string) : string
    /// Returns a new string with some or all matches of a pattern replaced by a replacement.
    member Replace (regExp: JSe.RegExp, replacer: JSe.RegExpReplacer -> string) : string
    /// Returns a new string with some or all matches of a pattern replaced by a replacement.
    member Replace (regex: Regex, replacer: JSe.RegExpReplacer -> string) : string

    /// The index of the first match between the regular expression and the given string, 
    /// or -1 if no match was found.
    member Search (regExp: JSe.RegExp) : int
    /// The index of the first match between the regular expression and the given string, 
    /// or -1 if no match was found.
    member Search (regex: Regex) : int

    /// Splits a string into substrings based on regular expression matches.
    member _.Split (regExp: JSe.RegExp) : string []
    /// Splits a string into substrings based on regular expression matches.
    member Split (regex: Regex) : string []
```
