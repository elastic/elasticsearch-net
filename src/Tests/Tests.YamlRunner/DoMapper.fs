module Tests.YamlRunner.DoMapper

open System
open System.Reflection
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Collections.ObjectModel
open System.IO
open System.Linq.Expressions
open Elasticsearch.Net

type ApiInvoke = delegate of Object * Object[] -> IElasticsearchResponse
type FastApiInvoke(instance: Object, restName:string, pathParams:KeyedCollection<string, string>, methodInfo:MethodInfo) =
    member this.ClientMethodName = methodInfo.Name
    member this.ApiName = restName
    member this.PathParameters = pathParams
    member private this.Delegate =
        let instanceExpression = Expression.Parameter(typeof<Object>, "instance");
        let argumentsExpression = Expression.Parameter(typeof<Object[]>, "arguments");
        let argumentExpressions = new List<Expression>();
        methodInfo.GetParameters()
            |> Array.indexed
            |> Array.iter (fun (i, p) ->
                let constant = Expression.Constant i
                let index = Expression.ArrayIndex (argumentsExpression, constant)
                let convert = Expression.Convert (index, p.ParameterType)
                argumentExpressions.Add convert
            )
        let callExpression =
            let instance = Expression.Convert(instanceExpression, methodInfo.ReflectedType)
            Expression.Call(instance, methodInfo, argumentExpressions)
        let invokeExpression = Expression.Convert(callExpression, typeof<Object>)
        Expression.Lambda<ApiInvoke>(invokeExpression, instanceExpression, argumentsExpression).Compile();
        
    member this.Invoke arguments = this.Delegate.Invoke(instance, arguments)


let getProp (t:Type) prop = t.GetProperty(prop).GetGetMethod()
let getRestName (t:Type) a = (getProp t "RestSpecName").Invoke(a, null) :?> String
let getParameters (t:Type) a = (getProp t "Parameters").Invoke(a, null) :?> KeyedCollection<string, string>

let methodsWithAttribute instance mapsApiAttribute  =
    let clientType = instance.GetType()
    clientType.GetMethods()
    |> Array.map (fun m -> (m, m.GetCustomAttributes(mapsApiAttribute, false)))
    |> Array.filter (fun (_, a) -> a.Length > 0)
    |> Array.map (fun (m, a) -> (m, a.[0] :?> Attribute))
    |> Array.map (fun (m, a) -> (m, getRestName mapsApiAttribute a, getParameters mapsApiAttribute a))
    |> Array.map (fun (m, restName, pathParams) -> (FastApiInvoke(instance, restName, pathParams, m)))
    
    
let clientApiDispatch client =
    let t = client.GetType()
    let mapsApiAttribute = t.Assembly.GetType("Elasticsearch.Net.MapsApiAttribute")
    
    let rootMethods = methodsWithAttribute client mapsApiAttribute
    let namespaces =
        t.GetProperties()
        |> Array.filter (fun p -> typeof<NamespacedClientProxy>.IsAssignableFrom(p.PropertyType))
        |> Array.map (fun p -> methodsWithAttribute (p.GetGetMethod().Invoke(client, null)) mapsApiAttribute)
        |> Array.concat
        |> Array.append rootMethods
    
    for m in namespaces do
        
        printfn "method: %O api: %s len: %i" m.ClientMethodName m.ApiName m.PathParameters.Count
        

