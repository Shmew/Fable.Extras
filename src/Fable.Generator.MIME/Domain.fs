namespace Fable.Generator.MIME

type MimeTree =
    { Key: string
      Literals: Map<string,string>
      Paths: Map<string,MimeTree> }

[<RequireQualifiedAccess>]
module MimeTree =
    let create (key: string) =
        { Key = key
          Literals = Map.empty<string,string>
          Paths = Map.empty<string,MimeTree> }

    module Set =
        let literals (literals: Map<string,string>) (tree: MimeTree) =
            { tree with Literals = literals }

        let paths (paths: Map<string,MimeTree>) (tree: MimeTree) =
            { tree with Paths = paths }

    module Add =
        let rec private mergeTrees (tree1: MimeTree) (tree2: MimeTree) =
            if tree1.Key <> tree2.Key then failwithf "Invalid tree merge: %A - %A" tree1 tree2
            
            { Key = tree1.Key
              Literals =
                tree2.Literals
                |> Seq.fold (fun (m: Map<string,string>) kvPair ->
                    m.Add(kvPair.Key, kvPair.Value)
                ) tree1.Literals
              Paths =
                tree2.Paths
                |> Seq.fold (fun (m: Map<string,MimeTree>) kvPair ->
                    match m.TryFind kvPair.Key with
                    | Some v -> mergeTrees v kvPair.Value
                    | None -> kvPair.Value
                    |> fun v -> Map.add kvPair.Key v m
                ) tree1.Paths }

        let literal (name: string) (value: string) (tree: MimeTree) =
            { tree with Literals = tree.Literals.Add(name, value) }

        let path (tree: MimeTree) (subTree: MimeTree) =
            match tree.Paths.TryFind subTree.Key with
            | Some existingSubTree -> mergeTrees existingSubTree subTree
            | None -> subTree
            |> fun v -> Map.add subTree.Key v tree.Paths
            |> fun newPaths -> { tree with Paths = newPaths }
