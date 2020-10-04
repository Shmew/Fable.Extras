namespace Fable.Generator.MIME

[<AutoOpen>]
module Utils =
    open System.Text.RegularExpressions

    let flip f x y = f y x

    module String =
        let trim (s: string) = s.Trim()

        /// Converts the first character to lowercase.
        let lowerFirst (s: string) = s.Substring(0, 1).ToLower() + s.Substring(1)

        /// Converts the first character to uppercase.
        let upperFirst (s: string) = s.Substring(0, 1).ToUpper() + s.Substring(1)

        /// Adds a prefix to the given string
        let prefix (prefix: string) s = prefix + s

        /// Indents a string based on `spacesPerLevel` and `numLevels`
        let indent spacesPerLevel numLevels = prefix (String.replicate (numLevels * spacesPerLevel) " ")

        /// Splits the string on the specified separator.
        let split (sep: string) (s: string) =
            match sep, s with
            | ((null | ""), _)
            | (_, (null | "")) -> seq [ s ]
            | _ ->
                let n, j = s.Length, sep.Length

                let rec loop p =
                    seq {
                        if p < n then
                            let i =
                                match s.IndexOf(sep, p) with
                                | -1 -> n
                                | i -> i
                            yield s.Substring(p, i - p)
                            yield! loop (i + j)
                    }
                loop 0

        /// Wraps a string in backticks if it starts with a number.
        let wrapNumFirst (str: string) =
            if Regex("^[0-9]").IsMatch(str) then sprintf "``%s``" str
            else str

    module List =
        /// Returns the list after removing the first N elements. If the list has less
        /// than N elements, returns an empty list.
        let trySkip count list =
            if List.length list < count then []
            else List.skip count list

        let trimEmptyLines list =
            list
            |> List.skipWhile ((=) "")
            |> List.rev
            |> List.skipWhile ((=) "")
            |> List.rev

        let allCombinations list =
            let rec comb accList elemList =
                match elemList with
                | h :: t ->
                    let next = [ h ] :: List.map (fun el -> h :: el) accList @ accList
                    comb next t
                | _ -> accList
            comb [] list

    module Seq =
        let trimEmptyLines list =
            list
            |> Seq.skipWhile ((=) "")
            |> Seq.rev
            |> Seq.skipWhile ((=) "")
            |> Seq.rev
