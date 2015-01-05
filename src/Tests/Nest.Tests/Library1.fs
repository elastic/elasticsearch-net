module Nest.Tests

open Xunit
open Nest
open Elasticsearch.Net.Connection
open System.Text
open System.IO
open FluentAssertions
open System
open Newtonsoft.Json.Linq
open FsUnit

[<AbstractClass>]
type BaseTest<'i, 'descriptor, 'ois when 'descriptor : (new : unit ->  'descriptor)>() = 
    let settings = new ConnectionSettings()
    let connection = new InMemoryConnection(settings)
    let client = new ElasticClient(settings, connection)
    
    member x.Client = client

    abstract member Initializer: 'ois

    abstract member Fluent: 'descriptor -> 'descriptor
    
    abstract member ExpectedJson: Map<string, Object> -> Map<string, Object>
    
    member private x._fluent = x.Fluent(new 'descriptor())

    member private x._fluentSerialized = client.Serializer.Serialize(x._fluent) |> Encoding.UTF8.GetString
    member private x._objectInitializerSerialized = client.Serializer.Serialize(x.Initializer) |> Encoding.UTF8.GetString
    member private x._expected = 
        x.ExpectedJson(Map.empty<string,Object>) 
            |> client.Serializer.Serialize
            |> Encoding.UTF8.GetString
    
    member private x._jsonEquals (actual) = 
        let expectedJson = JObject.Parse(x._expected)
        let actualJson = JObject.Parse(actual)
        let matches = JToken.DeepEquals(expectedJson, actualJson)  
        match matches with 
        | false -> failwithf "expected:\r\n%s\r\nactual:\r\n%s" x._expected actual |> ignore
        | _ -> "" |> ignore
        

    [<Fact>]
    member x.``Fluent Syntax serializes to expect json``() = x._jsonEquals x._fluentSerialized

    [<Fact>]
    member x.``Initializer Syntax serializes to expect json``() = x._jsonEquals x._objectInitializerSerialized

    [<Fact>]
    member x.``Initializer Syntax serialization roundtrips successfully``() = 
        let serialized = client.Serializer.Serialize(x.Initializer)
        let deserialized = client.Serializer.Deserialize<'ois>(new MemoryStream(serialized))
        x.Initializer.ShouldBeEquivalentTo(deserialized, "") |> ignore

    [<Fact>]
    member x.``Fluent Syntax serialization roundtrips successfully``() = 
        let o = x.Fluent(new 'descriptor()) 
        let serialized = client.Serializer.Serialize(o)
        let deserialized = client.Serializer.Deserialize<'descriptor>(new MemoryStream(serialized))
        o.ShouldBeEquivalentTo(deserialized, "") |> ignore