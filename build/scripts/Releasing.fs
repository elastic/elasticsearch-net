namespace Scripts

open System
open System.IO
open System.Linq
open System.Text
open System.Xml
open System.Xml.Linq
open System.Xml.XPath
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Fake

open Paths
open Projects
open Tooling
open Versioning

module Release =
    
    let private year = sprintf "%i" DateTime.UtcNow.Year
    
    let private jsonNetVersionCurrent p = 
        let xName n = XName.op_Implicit n
        use stream = File.OpenRead <| Paths.ProjFile p
        let doc = XDocument.Load(stream)
        let packageReference = 
            doc.Descendants(xName "PackageReference")
               .FirstOrDefault(fun e -> e.Attribute(xName "Include").Value = "Newtonsoft.Json")
        if (packageReference <> null) then packageReference.Attribute(xName "Version").Value
        else String.Empty
        
    let private jsonNetVersionNext p =
        match jsonNetVersionCurrent p with
        | "" -> String.Empty
        | version -> 
            let semanticVersion = SemVerHelper.parse version
            sprintf "%i" (semanticVersion.Major + 1)
            
    let private addKeyValue (e:Expr<string>) (builder:StringBuilder) =
        // the binding for this tuple looks like key/value should 
        // be round the other way (but it's correct as is)...
        let (value,key) = 
            match e with
            | PropertyGet (eo, pi, li) -> (pi.Name, (pi.GetValue(e) |> string))
            | ValueWithName (obj,ty,nm) -> ((obj |> string), nm)
            | _ -> failwith (sprintf "%A is not a let-bound value. %A" e (e.GetType()))
            
        if (isNotNullOrEmpty value) then builder.AppendFormat("{0}=\"{1}\";", key, value)
        else builder

    let private currentMajorVersion version = sprintf "%i" <| version.Full.Major
    let private nextMajorVersion version = sprintf "%i" <| version.Full.Major + 1

    let private props version =
        let currentMajorVersion = currentMajorVersion version
        let nextMajorVersion = nextMajorVersion version
        new StringBuilder()
        |> addKeyValue <@currentMajorVersion@>
        |> addKeyValue <@nextMajorVersion@>
        |> addKeyValue <@year@>

    let pack file n properties version  = 
        Tooling.Nuget.Exec [ "pack"; file; 
             "-version"; version.Full.ToString(); 
             "-outputdirectory"; Paths.BuildOutput; 
             "-properties"; properties; 
        ] |> ignore
        traceFAKE "%s" Paths.BuildOutput
        let nugetOutFile = Paths.Output(sprintf "%s.%O.nupkg" n version.Full)
        MoveFile Paths.NugetOutput nugetOutFile

    let private nugetPackMain (p:DotNetProject) nugetId nuspec properties version = 
        pack nuspec nugetId properties version
        
    let private nugetPackVersioned (p:DotNetProject) nugetId nuspec properties version =
        let currentMajorVersion = currentMajorVersion version
        let newId = sprintf "%s.v%s" nugetId currentMajorVersion;
        let nuspecVersioned = sprintf @"build/%s.nuspec" newId
            
        let xName n = XName.op_Implicit n
        use stream = File.OpenRead <| nuspec 
        let doc = XDocument.Load(stream) 
        let nsManager = new XmlNamespaceManager(doc.CreateNavigator().NameTable);
        nsManager.AddNamespace("x", "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd")

        doc.XPathSelectElement("/x:package/x:metadata/x:id", nsManager).Value <- newId
        let titleNode = doc.XPathSelectElement("/x:package/x:metadata/x:title", nsManager) 
        titleNode.Value <- sprintf "%s.x namespaced package, can be installed alongside %s" currentMajorVersion nugetId
        let descriptionNode = doc.XPathSelectElement("/x:package/x:metadata/x:description", nsManager) 
        descriptionNode.Value <- sprintf "%s.x namespaced package, can be installed alongside %s" currentMajorVersion nugetId
        let iconNode = doc.XPathSelectElement("/x:package/x:metadata/x:iconUrl", nsManager) 
        iconNode.Value <- replace "icon" "icon-aux" iconNode.Value 
        let xmlConfig = sprintf "/x:package//x:file[contains(@src, '%s.xml')]" p.Name
        doc.XPathSelectElements(xmlConfig, nsManager).Remove();

        let dll n = sprintf "%s.dll" n
        let rewriteDllFile name = 
            let d = dll name
            let r = (dll (p.Versioned name (Some currentMajorVersion)))
            let x = sprintf "/x:package//x:file[contains(@src, '%s')]" d
            let dllNodes = doc.XPathSelectElements(x, nsManager)
            dllNodes |> Seq.iter (fun e -> 
                let src = e.Attribute(xName "src");
                src.Value <- replace d r src.Value 
            )

        match p with 
        | Project Nest -> 
            let esDeps = doc.XPathSelectElements("/x:package/x:metadata//x:dependency[@id='Elasticsearch.Net']", nsManager);
            esDeps |> Seq.iter(fun e ->
                let esDep = e.Attribute(xName "id");
                esDep.Value <- sprintf "Elasticsearch.Net.v%s" currentMajorVersion
            )
            rewriteDllFile p.Name
        | Project ElasticsearchNet ->
            rewriteDllFile p.Name
            ignore()
        | Project NestJsonNetSerializer -> 
            let nestDeps = doc.XPathSelectElements("/x:package/x:metadata//x:dependency[@id='NEST']", nsManager);
            nestDeps |> Seq.iter (fun e ->
                let idAtt = e.Attribute(xName "id");
                idAtt.Value <- sprintf "NEST.v%s" currentMajorVersion
            )
            rewriteDllFile p.Name
        | _ -> traceError (sprintf "%s still needs special canary handling" p.Name)
        doc.Save(nuspecVersioned) 

        pack nuspecVersioned newId properties version 
        DeleteFile nuspecVersioned 
    
    let private packProjects version callback  =
        CreateDir Paths.NugetOutput
            
        DotNetProject.AllPublishable
        |> Seq.iter(fun p ->

            let jsonDotNetCurrentVersion = jsonNetVersionCurrent p
            let jsonDotNetNextVersion = jsonNetVersionNext p

            let properties =
                props version
                |> addKeyValue <@jsonDotNetCurrentVersion@>
                |> addKeyValue <@jsonDotNetNextVersion@>
                |> toText
            let nugetId = p.NugetId 
            let nuspec = (sprintf @"build/%s.nuspec" nugetId)

            callback p nugetId nuspec properties version
        )
            
    let NugetPack (ArtifactsVersion(version))  = packProjects version nugetPackMain 

    let NugetPackVersioned (ArtifactsVersion(version))  = packProjects version nugetPackVersioned
