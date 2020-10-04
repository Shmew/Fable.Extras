namespace Fable.Generator.MIME

open FSharp.Data

module Parser =
    open Fake.IO
    open Fake.IO.Globbing.Operators
    open Fake.IO.FileSystemOperators

    type private MimeSchema = CsvProvider<"Schema.csv">
    
    let private sanitizePath (path: string) =
        path
        |> trimQuotes
        |> spaceCaseTokebabCase
        |> kebabCaseToCamelCase
        |> snakeCaseToCamelCase
        |> replaceAddSymbol
        |> appendApostropheToReservedKeywords
        |> String.lowerFirst

    let rec private getPath (paths: string list) (literalValue: string) (tree: MimeTree) =
        match paths with
        | [ path ] -> MimeTree.Add.literal (sanitizePath path) literalValue tree
        | path::newPaths ->
            let path = sanitizePath path

            match tree.Paths.TryFind path with
            | Some subTree -> subTree
            | None -> MimeTree.create path
            |> getPath newPaths literalValue
            |> MimeTree.Add.path tree
        | [] -> tree

    let private getLiteralValue (row: MimeSchema.Row) (root: string) =
        match row.Template with
        | "" -> sprintf "%s/%s" root row.Name
        | value -> value

    let private getMimes () =
        !! (__SOURCE_DIRECTORY__ @@ "../../paket-files/fable.generator.mime/www.iana.org/*.csv")
        |> List.ofSeq
        |> List.map (fun path ->
            printfn "Found file: %s" path
            (FileInfo.ofPath path).Name.Split('.') |> Array.head |> sanitizePath,
            MimeSchema.Load path)

    let getTree () =
        getMimes()
        |> List.fold (fun (tree: MimeTree) (path, csv) ->
            printfn "Processing %s" path

            csv.Rows
            |> Seq.fold (fun tree row ->
                let rowName = row.Name.ToLower()
                if List.exists (fun (s: string) -> rowName.Contains(s)) [ "obsolete" ;"obsoleted"; "deprecated" ] then tree
                else getPath (path::(rowName.Split('.') |> List.ofArray)) (getLiteralValue row path) tree
            ) tree
        ) { Key = "MIME"; Literals = Map.empty<string,string>; Paths = Map.empty<string,MimeTree> }
