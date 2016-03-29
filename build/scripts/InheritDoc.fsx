#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r "System.Xml.Linq.dll"

open System.Xml
open System.Xml.XPath
open System.Linq
open System.Xml.Linq
open System.Text.RegularExpressions

let PatchXmlDoc = fun (file: string) ->
  let xml = XDocument.Load file
  let nodes = xml.XPathSelectElements("//inheritdoc") 
              |> Seq.map (fun n -> 
                let methodName = n.Parent.Attribute(XName.Get("name")).Value
                let interfaceName = Regex.Replace(methodName, @"\.([^.]+\.[^.]+\()", ".I$1")
                let interfaceNode = xml.XPathSelectElement (sprintf "//member[@name='%s']" interfaceName)
                (n.Parent, interfaceNode)
              ) 
              |> Seq.filter (fun (implementationElement, interfaceElement) -> 
                interfaceElement <> null && implementationElement.HasElements && interfaceElement.HasElements
              )
  let nodesReplace = nodes 
                      |> Seq.iter (fun (implementationElement, interfaceElement) ->
                        implementationElement.Add (interfaceElement.Descendants().ToList())
                      ) 
                    
  xml.Save file