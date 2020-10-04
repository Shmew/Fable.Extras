namespace Fable.Generator.MIME

module Program =
    open Fake.IO
    open Fake.IO.FileSystemOperators

    [<EntryPoint>]
    let main _ =
        let mimeFile  = __SOURCE_DIRECTORY__ @@ "../Fable.Extras/MIME.fs"
        
        Parser.getTree()
        |> Render.mimeDocument 
        |> File.writeString false mimeFile

        0
