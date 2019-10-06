module Tests.YamlRunner.TestsExporter

open System
open System.Globalization
open Tests.YamlRunner.OperationExecutor
open Tests.YamlRunner.TestsReader
open System.Xml.Linq
open System.Xml.XPath

type SectionResults = string * ExecutionResult list
type FileResults = SectionResults list
type FolderResults = (YamlTestDocument * FileResults) list
type RunResults = (YamlTestFolder * FolderResults) list

let XElement(name, content: 'a list) = new XElement(XName.Get name, Seq.toArray content)
let XAttribute(name, value) = new XAttribute(XName.Get name, value)

let mapExecutionResult result =
    match result with
    | ExecutionResult.Succeeded f -> None
    | ExecutionResult.Failed f ->
        let c = f.Context
        let message =
            let m = sprintf "%s: %s %s" result.Name (c.Operation.Log()) (f.Log())
            XAttribute("message", m)
        match f with
        | SeenException (c, e) -> 
            let error = XElement("error", [message])
            error.Value <- e.ToString()
            Some error
        | ValidationFailure (c, f) -> 
            let failure = XElement("failure", [message])
            failure.Value <- c.Stashes.Response().DebugInformation
            Some failure
    | ExecutionResult.Skipped s ->
        Some <| XElement("skipped", [])
        
let private timeOf result =
    match result with
    | ExecutionResult.Succeeded f -> !f.Elapsed
    | ExecutionResult.Failed f -> !f.Context.Elapsed
    | ExecutionResult.Skipped s -> !s.Elapsed

let testCasesSection document (results: FileResults) =
    results
    |> List.map (fun (section, results) ->
       let name = sprintf "%s - %s" document.FileInfo.Name section
       let time =
           let totalMs = results |> List.sumBy timeOf
           Convert.ToDouble(totalMs) / 1000.0
       let results =
           results
           |> List.map(fun r -> mapExecutionResult r)
           |> List.choose id
       let name = XAttribute("name", name)
       let time = XAttribute("time", time.ToString(CultureInfo.InvariantCulture))
    
       let testCase = XElement("testcase", [])
       testCase.Add(results, name, time)
       testCase
    )
    
let testCases (results: FolderResults) =
    results
    |> List.map (fun (document, sections) ->
       testCasesSection document sections
    )
    |> List.concat
    
let countTests (xElement:XElement) =
    let testCases = xElement.XPathSelectElements "//testcase"
    let errors = xElement.XPathSelectElements "//testcase[errors]"
    let failed = xElement.XPathSelectElements "//testcase[failure]"
    let skipped = xElement.XPathSelectElements "//testcase[skipped]"
    let time =
        xElement.XPathSelectElements "//testcase[@time]"
        |> Seq.map (fun a -> Double.Parse (a.Attribute(XName.Get "time")).Value)
        |> Seq.sum
        
    let tests = XAttribute("tests", testCases |> Seq.length)
    let failed = XAttribute("failures", failed |> Seq.length)
    let errors = XAttribute("errors", errors |> Seq.length)
    let skipped = XAttribute("disabled", skipped |> Seq.length)
    let time = XAttribute("time", time)
    xElement.Add(tests, failed, errors, skipped, time)

let testSuites (results: RunResults) =
    results
    |> List.map (fun (folder, documents) ->
        let name = XAttribute("name", folder.Folder)
        let testCases = testCases documents
        let suite = XElement("testsuite", [name])
        suite.Add(testCases)
        countTests suite
        suite
    )

let Export (results: RunResults) =
    let suites = testSuites results
    
    let xml = XElement("testsuites", suites)
    countTests xml
    
    let fileName = System.IO.Path.GetTempFileName()
    xml.Save(fileName)
    fileName
