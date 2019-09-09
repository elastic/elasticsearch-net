module Tests.YamlRunner.DoMapper

open System
open System.Reflection
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Globalization
open System.Linq.Expressions
open System.Threading.Tasks
open Elasticsearch.Net

type ApiInvoke = delegate of Object * Object[] -> Task<StringResponse>

type RequestParametersInvoke = delegate of unit ->  IRequestParameters

type FastApiInvoke(instance: Object, restName:string, pathParams:KeyedCollection<string, string>, methodInfo:MethodInfo) =
    member this.ClientMethodName = methodInfo.Name
    member this.ApiName = restName
    member private this.IndexOfParam p = pathParams.IndexOf p
    member private this.SupportsBody = pathParams.IndexOf "body" >= 0
    member this.PathParameters =
        pathParams |> Seq.map (fun k -> k) |> Seq.filter (fun k -> k <> "body") |> Set.ofSeq
        
    member private this.CreateRequestParameters = 
        let t = methodInfo.GetParameters() |> Array.find (fun p -> typeof<IRequestParameters>.IsAssignableFrom(p.ParameterType))
        let c = t.ParameterType.GetConstructors() |> Array.head
        
        let newExp = Expression.New(c)
        Expression.Lambda<RequestParametersInvoke>(newExp).Compile()
        
    ///<summary> Create a call into a specific client method </summary>
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
        let x = [|typeof<StringResponse>|] 
        let callExpression =
            let instance = Expression.Convert(instanceExpression, methodInfo.ReflectedType)
            Expression.Call(instance, methodInfo.Name, x, argumentExpressions.ToArray())
            
        let invokeExpression = Expression.Convert(callExpression, typeof<Task<StringResponse>>)
        Expression.Lambda<ApiInvoke>(invokeExpression, instanceExpression, argumentsExpression).Compile();
        
    member this.CanInvoke (o:Map<string, Object>) =
        let operationKeys =
            o
            |> Seq.map (fun k -> k.Key)
            |> Seq.filter (fun k -> k <> "body")
            |> Set.ofSeq
        this.PathParameters.IsSubsetOf operationKeys
    
    member this.Invoke (o:Map<string, Object>) =
        let foundBody, body = o.TryGetValue "body"
        
        let arguments =
            o
            |> Map.toSeq
            |> Seq.filter (fun (k, v) -> this.PathParameters.Contains(k))
            |> Seq.sortBy (fun (k, v) -> this.IndexOfParam k)
            |> Seq.map (fun (k, v) ->
                let toString (value:Object) = 
                    match value with
                    | :? String as s -> s
                    | :? int32 as i -> i.ToString(CultureInfo.InvariantCulture)
                    | :? double as i -> i.ToString(CultureInfo.InvariantCulture)
                    | :? int64 as i -> i.ToString(CultureInfo.InvariantCulture)
                    | :? Boolean as b -> if b then "false" else "true"
                    | e -> failwithf "unknown type %s " (e.GetType().Name)
                
                match v with
                | :? List<Object> as a ->
                    let values = a |> Seq.map toString |> Seq.toArray
                    String.Join(',', values)
                | e -> toString e
                ) 
            |> Seq.cast<Object>
            |> Seq.toArray
        
        let requestParameters = this.CreateRequestParameters.Invoke();
        o
        |> Map.toSeq
        |> Seq.filter (fun (k, v) -> not <| this.PathParameters.Contains(k))
        |> Seq.filter (fun (k, v) -> k <> "body")
        |> Seq.iter (fun (k, v) -> requestParameters.SetQueryString(k, v))
        
        match (foundBody, this.SupportsBody) with
        | (true, true) ->
            let arguments = Array.append arguments [|PostData.Serializable body; requestParameters; Async.DefaultCancellationToken|]
            this.Delegate.Invoke(instance, arguments)
        | (false, true) ->
            let arguments = Array.append arguments [|null ; requestParameters; Async.DefaultCancellationToken|]
            this.Delegate.Invoke(instance, arguments)
        | (false, false) ->
            let arguments = Array.append arguments [|requestParameters; Async.DefaultCancellationToken|]
            this.Delegate.Invoke(instance, arguments)
        | (true, false) ->
            failwithf "found a body but this method does not take a body"


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

exception ParamException of string 

let createApiLookup (invokers: FastApiInvoke list) : (Map<string, Object> -> FastApiInvoke) =
    let first = invokers |> List.head
    let name = first.ApiName
    let clientMethod = first.ClientMethodName
    
    let lookup (o:Map<string, Object>) =
        
        let invokers =
            invokers
            |> Seq.sortByDescending (fun i -> i.PathParameters.Count)
            |> Seq.filter (fun i -> i.CanInvoke o)
            |> Seq.toList
        
        match invokers with
        | [] ->
            raise <| ParamException(sprintf "%s matched no method on %s: %O " name clientMethod o)
        | invoker::tail ->
           invoker 
    lookup
    
    
let clientApiDispatch (client:IElasticLowLevelClient) =
    let t = client.GetType()
    let mapsApiAttribute = t.Assembly.GetType("Elasticsearch.Net.MapsApiAttribute")
    
    let rootMethods = methodsWithAttribute client mapsApiAttribute
    let namespaces =
        t.GetProperties()
        |> Array.filter (fun p -> typeof<NamespacedClientProxy>.IsAssignableFrom(p.PropertyType))
        |> Array.map (fun p -> methodsWithAttribute (p.GetGetMethod().Invoke(client, null)) mapsApiAttribute)
        |> Array.concat
        |> Array.append rootMethods
    
    namespaces
    |> List.ofArray
    |> List.groupBy (fun n -> n.ApiName)
    |> Map.ofList<String, FastApiInvoke list>
    |> Map.map<String, FastApiInvoke list, (Map<String, Object> -> FastApiInvoke)>(fun k v -> createApiLookup v)
    
let DoMap =
    let settings = new ConnectionConfiguration(new Uri("http://ipv4.fiddler:9200"))
    let x = settings.Proxy(Uri("http://ipv4.fiddler:8080"), String(null), String(null))
    clientApiDispatch (new ElasticLowLevelClient(x))
        

