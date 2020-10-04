namespace Fable.Generator.MIME

module Render =
    open System
    open Utils

    /// Indents a string with 4 spaces per `numLevels` given
    let private indent numLevels = String.indent 4 numLevels

    let private mkLiteral (name: string) (value: string) (indentLevel: int) =
        [ sprintf "/// %s" value
          sprintf "let [<Literal>] %s = \"%s\"" (String.wrapNumFirst name) value ]
        |> List.map (indent indentLevel)
        |> String.concat Environment.NewLine

    let rec private mkPath (tree: MimeTree) (indentLevel: int) =
        let literals =
            tree.Literals
            |> Map.toList
            |> List.map (fun (name, value) -> mkLiteral name value (indentLevel + 1))

        let paths =
            tree.Paths
            |> Map.toList
            |> List.collect (fun (_, subTree) -> mkPath subTree (indentLevel + 1))

        if literals.IsEmpty && paths.IsEmpty then []
        else
            [ "[<Erase;RequireQualifiedAccess>]" |> indent indentLevel
              sprintf "module %s =" (String.wrapNumFirst tree.Key) |> indent indentLevel
              yield! literals
              if not literals.IsEmpty then ""
              yield! paths ]

    /// Generate the MIME file
    let mimeDocument (tree: MimeTree) =
        [ "namespace Fable.Extras.MIME"
          ""
          "(*////////////////////////////////"
          "/// THIS FILE IS AUTO-GENERATED //"
          "////////////////////////////////*)"
          ""
          "#nowarn \"3190\""
          ""
          "open Fable.Core"
          ""
          "[<Erase;RequireQualifiedAccess>]"
          "module JSe ="
          yield! mkPath tree 1 ]
        |> String.concat Environment.NewLine
