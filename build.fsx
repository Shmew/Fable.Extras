// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#nowarn "0213"
#r "paket: groupref FakeBuild //"
#load "./tools/FSharpLint.fs"
#load "./tools/Web.fs"
#load "./.fake/build.fsx/intellisense.fsx"

open BlackFox.CommandLine
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.JavaScript
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Tools
open Tools.Linting
open Tools.Web
open System
open System.IO

// The name of the project
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "Fable.Extras"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = "Fable bindings for jest and friends"

// Author(s) of the project
let author = "Cody Johnson"

// File system information
let solutionFile = "Fable.Extras.sln"

// Github repo
let repo = "https://github.com/Shmew/Fable.Extras"

// Files that have bindings to other languages where name linting needs to be more relaxed.
let relaxedNameLinting = 
    [ __SOURCE_DIRECTORY__ @@ "src/Fable.*/*.fs"
      __SOURCE_DIRECTORY__ @@ "tests/Fable.*/*.fs" ]

// Read additional information from the release notes document
let release = ReleaseNotes.load (__SOURCE_DIRECTORY__ @@ "RELEASE_NOTES.md")

// Helper active pattern for project types
let (|Fsproj|Csproj|Vbproj|Shproj|) (projFileName:string) =
    match projFileName with
    | f when f.EndsWith("fsproj") -> Fsproj
    | f when f.EndsWith("csproj") -> Csproj
    | f when f.EndsWith("vbproj") -> Vbproj
    | f when f.EndsWith("shproj") -> Shproj
    | _                           -> failwith (sprintf "Project file %s not supported. Unknown project type." projFileName)
    
let srcGlob        = __SOURCE_DIRECTORY__ @@ "src/**/*.??proj"
let fsSrcGlob      = __SOURCE_DIRECTORY__ @@ "src/**/*.fs"
let fsTestGlob     = __SOURCE_DIRECTORY__ @@ "tests/**/*.fs"
let bin            = __SOURCE_DIRECTORY__ @@ "bin"
let docs           = __SOURCE_DIRECTORY__ @@ "docs"
let temp           = __SOURCE_DIRECTORY__ @@ "temp"
let objFolder      = __SOURCE_DIRECTORY__ @@ "obj"
let dist           = __SOURCE_DIRECTORY__ @@ "dist"
let libGlob        = __SOURCE_DIRECTORY__ @@ "src/**/*.fsproj"
let wasmDir        = __SOURCE_DIRECTORY__ @@ "tests/Fable.Extras.Tests/wasm"
let wasmSrcDir     = __SOURCE_DIRECTORY__ @@ "src/fable-wasm/pkg"
let wasmImportDir  = __SOURCE_DIRECTORY__ @@ "src/fable-wasm-import/pkg"
let wasmGlob       = wasmSrcDir @@ "*.wasm"
let wasmImportGlob = wasmImportDir @@ "*.wasm"

let foldExcludeGlobs (g: IGlobbingPattern) (d: string) = g -- d
let foldIncludeGlobs (g: IGlobbingPattern) (d: string) = g ++ d

let fsSrcAndTest =
    !! fsSrcGlob
    ++ fsTestGlob
    -- (__SOURCE_DIRECTORY__  @@ "src/**/obj/**")
    -- (__SOURCE_DIRECTORY__  @@ "tests/**/obj/**")
    -- (__SOURCE_DIRECTORY__  @@ "src/**/AssemblyInfo.*")
    -- (__SOURCE_DIRECTORY__  @@ "src/**/**/AssemblyInfo.*")

let fsRelaxedNameLinting =
    let baseGlob s =
        !! s
        -- (__SOURCE_DIRECTORY__  @@ "src/**/AssemblyInfo.*")
        -- (__SOURCE_DIRECTORY__  @@ "src/**/obj/**")
        -- (__SOURCE_DIRECTORY__  @@ "tests/**/obj/**")
    match relaxedNameLinting with
    | [h] when relaxedNameLinting.Length = 1 -> baseGlob h |> Some
    | h::t -> List.fold foldIncludeGlobs (baseGlob h) t |> Some
    | _ -> None

let configuration() =
    FakeVar.getOrDefault "configuration" "Release"

let getEnvFromAllOrNone (s: string) =
    let envOpt (envVar: string) =
        if String.isNullOrEmpty envVar then None
        else Some(envVar)

    let procVar = Environment.GetEnvironmentVariable(s) |> envOpt
    let userVar = Environment.GetEnvironmentVariable(s, EnvironmentVariableTarget.User) |> envOpt
    let machVar = Environment.GetEnvironmentVariable(s, EnvironmentVariableTarget.Machine) |> envOpt

    match procVar,userVar,machVar with
    | Some(v), _, _
    | _, Some(v), _
    | _, _, Some(v)
        -> Some(v)
    | _ -> None

// Set default
FakeVar.set "configuration" "Release"

// --------------------------------------------------------------------------------------
// Set configuration mode based on target

Target.create "ConfigDebug" <| fun _ ->
    FakeVar.set "configuration" "Debug"

Target.create "ConfigRelease" <| fun _ ->
    FakeVar.set "configuration" "Release"

// --------------------------------------------------------------------------------------
// Copies binaries from default VS location to expected bin folder
// But keeps a subdirectory structure for each project in the
// src folder to support multiple project outputs

Target.create "CopyBinaries" <| fun _ ->
    !! libGlob
    -- (__SOURCE_DIRECTORY__ @@ "src/**/*.shproj")
    |> Seq.map (fun f -> ((Path.getDirectory f) @@ "bin" @@ configuration(), "bin" @@ (Path.GetFileNameWithoutExtension f)))
    |> Seq.iter (fun (fromDir, toDir) -> Shell.copyDir toDir fromDir (fun _ -> true))

Target.create "CopyWasm" <| fun _ ->
    !! wasmGlob
    ++ wasmImportGlob
    |> Shell.copy wasmDir

// --------------------------------------------------------------------------------------
// Clean tasks

Target.create "Clean" <| fun _ ->
    let clean() =
        !! (__SOURCE_DIRECTORY__ @@ "tests/**/bin")
        ++ (__SOURCE_DIRECTORY__ @@ "tests/**/obj")
        ++ (__SOURCE_DIRECTORY__ @@ "tools/bin")
        ++ (__SOURCE_DIRECTORY__ @@ "tools/obj")
        ++ (__SOURCE_DIRECTORY__ @@ "src/**/bin")
        ++ (__SOURCE_DIRECTORY__ @@ "src/**/obj")
        ++ (__SOURCE_DIRECTORY__ @@ ".fable")
        ++ wasmDir
        ++ wasmSrcDir
        ++ wasmImportDir
        |> Seq.toList
        |> List.append [bin; temp; objFolder; dist]
        |> Shell.cleanDirs
    TaskRunner.runWithRetries clean 10

Target.create "CleanDocs" <| fun _ ->
    let clean() =
        !! (docs @@ "RELEASE_NOTES.md")
        |> List.ofSeq
        |> List.iter Shell.rm

    TaskRunner.runWithRetries clean 10

Target.create "CopyDocFiles" <| fun _ ->
    [ docs @@ "RELEASE_NOTES.md", __SOURCE_DIRECTORY__ @@ "RELEASE_NOTES.md" ]
    |> List.iter (fun (target, source) -> Shell.copyFile target source)

Target.create "PrepDocs" ignore

Target.create "PostBuildClean" <| fun _ ->
    let clean() =
        !! srcGlob
        -- (__SOURCE_DIRECTORY__ @@ "src/**/*.shproj")
        |> Seq.map (
            (fun f -> (Path.getDirectory f) @@ "bin" @@ configuration()) 
            >> (fun f -> Directory.EnumerateDirectories(f) |> Seq.toList )
            >> (fun fL -> fL |> List.map (fun f -> Directory.EnumerateDirectories(f) |> Seq.toList)))
        |> (Seq.concat >> Seq.concat)
        |> Seq.iter Directory.delete
    TaskRunner.runWithRetries clean 10

Target.create "PostPublishClean" <| fun _ ->
    let clean() =
        !! (__SOURCE_DIRECTORY__ @@ "src/**/bin" @@ configuration() @@ "/**/publish")
        |> Seq.iter Directory.delete
    TaskRunner.runWithRetries clean 10

// --------------------------------------------------------------------------------------
// Restore tasks

let restoreSolution () =
    solutionFile
    |> DotNet.restore id

Target.create "Restore" <| fun _ ->
    TaskRunner.runWithRetries restoreSolution 5

Target.create "YarnInstall" <| fun _ ->
    let setParams (defaults:Yarn.YarnParams) =
        { defaults with
            Yarn.YarnParams.YarnFilePath = (__SOURCE_DIRECTORY__ @@ "packages/tooling/Yarnpkg.Yarn/content/bin/yarn.cmd")
        }
    Yarn.install setParams

Target.create "RustUpgrade" <| fun _ ->
    CmdLine.empty
    |> CmdLine.append "update"
    |> CmdLine.toString
    |> CreateProcess.fromRawCommandLine "rustup"
    |> CreateProcess.ensureExitCodeWithMessage "Rust update failed."
    |> Proc.run
    |> ignore

// --------------------------------------------------------------------------------------
// Build tasks

Target.create "Build" <| fun _ ->
    let setParams (defaults: MSBuildParams) =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", configuration()
                    "Version", release.AssemblyVersion
                    "GenerateDocumentationFile", "true"
                    "DependsOnNETStandard", "true"
                ]
         }
    restoreSolution()

    !! libGlob
    |> List.ofSeq
    |> List.iter (MSBuild.build setParams)

Target.create "BuildWasm" <| fun _ ->
    Yarn.exec "build-wasm" id
    Yarn.exec "build-wasm-import" id

// --------------------------------------------------------------------------------------
// Publish net core applications

Target.create "PublishDotNet" <| fun _ ->
    let runPublish (project: string) (framework: string) =
        let setParams (defaults:MSBuildParams) =
            { defaults with
                Verbosity = Some(Quiet)
                Targets = ["Publish"]
                Properties =
                    [
                        "Optimize", "True"
                        "DebugSymbols", "True"
                        "Configuration", configuration()
                        "Version", release.AssemblyVersion
                        "GenerateDocumentationFile", "true"
                        "TargetFramework", framework
                    ]
            }
        MSBuild.build setParams project

    !! libGlob
    |> Seq.map
        ((fun f -> (((Path.getDirectory f) @@ "bin" @@ configuration()), f) )
        >>
        (fun f ->
            Directory.EnumerateDirectories(fst f) 
            |> Seq.filter (fun frFolder -> frFolder.Contains("netcoreapp"))
            |> Seq.map (fun frFolder -> DirectoryInfo(frFolder).Name), snd f))
    |> Seq.iter (fun (l,p) -> l |> Seq.iter (runPublish p))

// --------------------------------------------------------------------------------------
// Lint source code

Target.create "Lint" <| fun _ ->
    fsSrcAndTest
    -- (__SOURCE_DIRECTORY__  @@ "src/**/AssemblyInfo.*")
    |> (fun src -> List.fold foldExcludeGlobs src relaxedNameLinting)
    |> (fun fGlob ->
        match fsRelaxedNameLinting with
        | Some(glob) ->
            [(false, fGlob); (true, glob)]
        | None -> [(false, fGlob)])
    |> Seq.map (fun (b,glob) -> (b,glob |> List.ofSeq))
    |> List.ofSeq
    |> FSharpLinter.lintFiles

// --------------------------------------------------------------------------------------
// Run the unit tests

Target.create "RunTests" <| fun _ ->
    Yarn.exec "test" id

// --------------------------------------------------------------------------------------
// Update package.json version & name    

Target.create "PackageJson" <| fun _ ->
    let setValues (current: Json.JsonPackage) =
        { current with
            Name = Str.toKebabCase project |> Some
            Version = release.NugetVersion |> Some
            Description = summary |> Some
            Homepage = repo |> Some
            Repository = 
                { Json.RepositoryValue.Type = "git" |> Some
                  Json.RepositoryValue.Url = repo |> Some
                  Json.RepositoryValue.Directory = None }
                |> Some
            Bugs = 
                { Json.BugsValue.Url = 
                    @"https://github.com/Shmew/Fable.Extras/issues/new/choose" |> Some } |> Some
            License = "MIT" |> Some
            Author = author |> Some
            Private = true |> Some }
    
    Json.setJsonPkg setValues

Target.create "Start" <| fun _ ->
    Yarn.exec "start" id 

Target.create "PublishPages" <| fun _ ->
    Yarn.exec "publish-docs" id

// --------------------------------------------------------------------------------------
// Build and release NuGet targets

Target.create "NuGet" <| fun _ ->
    Paket.pack(fun p ->
        { p with
            OutputPath = bin
            Version = release.NugetVersion
            ReleaseNotes = Fake.Core.String.toLines release.Notes
            ProjectUrl = repo
            MinimumFromLockFile = true
            IncludeReferencedProjects = true })

Target.create "NuGetPublish" <| fun _ ->
    Paket.push(fun p ->
        { p with
            ApiKey = 
                match getEnvFromAllOrNone "NUGET_KEY" with
                | Some key -> key
                | None -> failwith "The NuGet API key must be set in a NUGET_KEY environment variable"
            WorkingDir = bin })

// --------------------------------------------------------------------------------------
// Release Scripts

let gitPush msg =
    Git.Staging.stageAll ""
    Git.Commit.exec "" msg
    Git.Branches.push ""

Target.create "GitPush" <| fun p ->
    p.Context.Arguments
    |> List.choose (fun s ->
        match s.StartsWith("--Msg=") with
        | true -> Some(s.Substring 6)
        | false -> None)
    |> List.tryHead
    |> function
    | Some(s) -> s
    | None -> (sprintf "Bump version to %s" release.NugetVersion)
    |> gitPush

Target.create "GitTag" <| fun _ ->
    Git.Branches.tag "" release.NugetVersion
    Git.Branches.pushTag "" "origin" release.NugetVersion

Target.create "PublishDocs" <| fun _ ->
    gitPush "Publishing docs"

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build -t <Target>' to override

Target.create "All" ignore
Target.create "Dev" ignore
Target.create "Release" ignore
Target.create "Publish" ignore

"Clean"
  ==> "Restore"
  ==> "PackageJson"
  ==> "RustUpgrade"
  ==> "YarnInstall"
  ==> "Build"
  ==> "BuildWasm"
  ==> "CopyWasm"
  ==> "PostBuildClean"
  ==> "CopyBinaries"

"CopyWasm" ==> "RunTests"

"Build"
  ==> "PostBuildClean"
  ==> "PublishDotNet"
  ==> "PostPublishClean"
  ==> "CopyBinaries"

"Restore" ==> "Lint"

"Lint" 
  ?=> "Build"
  ?=> "RunTests"
  ?=> "CleanDocs"

"All"
  ==> "GitPush"
  ?=> "GitTag"

"All" <== ["Lint"; "RunTests"; "CopyBinaries" ]

"CleanDocs"
  ==> "CopyDocFiles"
  ==> "PrepDocs"

"All"
 ==> "NuGet"
 ==> "NuGetPublish"

"PrepDocs" 
 ==> "PublishPages"
 ==> "PublishDocs"

"All" 
  ==> "PrepDocs"

"All" 
  ==> "PrepDocs"
  ==> "Start"

"All" ==> "PublishPages"

"ConfigDebug" ?=> "Clean"
"ConfigRelease" ?=> "Clean"

"Dev" <== ["All"; "ConfigDebug"; "Start"]

"Release" <== ["All"; "NuGet"; "ConfigRelease"; "PrepDocs"]

"Publish" <== ["Release"; "ConfigRelease"; "NuGetPublish"; "PublishDocs"; "GitTag"; "GitPush" ]

Target.runOrDefaultWithArguments "Dev"
