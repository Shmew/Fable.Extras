namespace Fable.Generator.MIME

[<AutoOpen>]
module Common =
    /// Trims outer white space and " characters
    let trimQuotes (s: string) = s.Trim().Trim('"')

    let spaceCaseTokebabCase (s: string) = s.Replace(' ', '-')

    let snakeCaseToCamelCase (s: string) =
        let pieces =
            s.Split('_')
            |> Seq.trimEmptyLines
            |> Array.ofSeq
        if pieces.Length > 1 then
            pieces
            |> Array.iteri (fun i piece ->
                if i > 0 then pieces.[i] <- piece.Substring(0, 1).ToUpper() + piece.Substring(1))
            pieces |> String.concat ""
        else
            s

    let kebabCaseToCamelCase (s: string) =
        let pieces =
            s.Split('-')
            |> Seq.trimEmptyLines
            |> Array.ofSeq
        if pieces.Length > 1 then
            pieces
            |> Array.iteri (fun i piece ->
                if i > 0 then pieces.[i] <- piece.Substring(0, 1).ToUpper() + piece.Substring(1))
            pieces |> String.concat ""
        else
            s

    let appendApostropheToReservedKeywords (s: string) =
        let reserved =
            [ "checked"
              "static"
              "fixed"
              "inline"
              "default"
              "component"
              "inherit"
              "open"
              "type"
              "true"
              "false"
              "in"
              "end"
              "match"
              "base"
              "include"
              "constraint"
              "null"
              "if"
              "else"
              "elif"
              "function"
              "let"
              "val"
              "member"
              "private"
              "internval"
              "global"
              "sealed"
              "parallel" ] |> Set.ofList
        if reserved.Contains s then s + "'"
        else s

    /// Replace + symbols with "And"
    let replaceAddSymbol (s: string) =
        let trimOuter = s.Trim('+')
        if trimOuter.Contains "+" then
            let head, tail = trimOuter.Split('+') |> Array.splitAt 1
            [ head |> Array.map String.lowerFirst
              [| "And" |]
              tail |> Array.map String.upperFirst ]
            |> Array.concat
            |> Array.reduce (sprintf "%s%s")
        else
            trimOuter
