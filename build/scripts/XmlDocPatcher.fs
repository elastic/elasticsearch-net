// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System
open System.Linq
open System.Text.RegularExpressions
open System.Xml
open System.Xml.Linq
open System.Xml.XPath

module InheritDoc =
    
    let private apiName n = Regex.Replace(n, @"^\w\:(.+?)(?:\(.+$|$)", "$1")
    let private relatedInterface n = Regex.Replace(n, @"\.([^.]+\.[^.]+)$", ".I$1")
    let private relatedInterfaceAsync n = (relatedInterface n).Replace("Async", "")
    let private relatedInterfaceDescriptor n = Regex.Replace(n, @"\.([^.]+)Descriptor\.([^.]+)$", ".I$1.$2")
    let private relatedInterfaceDescriptorRequest n = Regex.Replace(n, @"\.([^.]+)Descriptor\.([^.]+)$", ".I$1Request.$2")
    let private relatedInterfaceDescriptorGeneric n = Regex.Replace(n, @"\.([^.]+)Descriptor[^.]+.([^.]+)$", ".I$1.$2")
    let private manualMapping (n : string) = 
        //this sucks but untill roslyn gets coderewriting API's this is the best we got without making it 
        //a bigger thing than it already is
        match n with 
        | n when n.Contains("PutMapping") -> n.Replace("PutMapping", "TypeMapping")
        | _ -> n

    let private relatedApiLookups = [
        relatedInterface;
        relatedInterfaceAsync;
        relatedInterfaceDescriptor;
        relatedInterfaceDescriptorRequest;
        relatedInterfaceDescriptorGeneric;
        manualMapping
    ]
    
    let private documentedApis (file:string) =
        use reader = XmlReader.Create file
        seq {
            while reader.ReadToFollowing("member") do
                let name = apiName(reader.GetAttribute("name"))
                let innerXml = reader.ReadInnerXml().Trim();
                if (not (String.IsNullOrEmpty innerXml) && not (innerXml.Contains("<inheritdoc"))) then
                    let xdoc = XDocument.Parse("<x>" + innerXml + "</x>")
                    yield (name, xdoc)
        } |> Map.ofSeq

    let private patchInheritDoc file = 
        printfn "Rewriting xmldoc:  %s" file

        let mapOfDocumentedApis = documentedApis file

        let findDocumentation (apiElement:string) = 
            relatedApiLookups
            |> Seq.map (fun f -> f apiElement)
            |> (Seq.map <| mapOfDocumentedApis.TryFind)
            |> Seq.choose id
            |> Seq.tryHead

        let xml = XDocument.Load file

        xml.XPathSelectElements("//inheritdoc") 
        |> Seq.iter (fun inheritDocElement -> 
                let parent =inheritDocElement.Parent 
                let name = apiName (parent.Attribute(XName.Get "name").Value)
                let documentation = findDocumentation name
                
                match documentation with
                | Some d -> 
                    let elements = d.Element(XName.Get("x")).Elements().ToList();
                    inheritDocElement.AddBeforeSelf(elements)
                | _ -> 
                    //unignore the following to find undocumented/badly inherited methods
                    //tracefn "not inherited: %s" apiElement
                    ignore()
           )

        use writer = new XmlTextWriter(file,null)
        writer.Formatting <- Formatting.Indented;
        xml.Save(writer);

    let PatchInheritDocs() =
        ignore()
        //TODO VALIDATE this still works as expected
//        AllPublishableProjectsWithSupportedFrameworks
//        |> Seq.map (fun p -> 
//            let folder = Paths.ProjectOutputFolder p.project p.framework
//            Path.Combine(folder, p.project.Name) + ".xml"
//        )
//        |> Seq.filter File.Exists
//        |> Seq.iter patchInheritDoc
