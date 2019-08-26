module Tests.YamlRunner.Models

open System
open System.IO

type TestSuite = OpenSource | XPack

type Skip = { Version:string; Reason:string option }

type DoCatch =
    | BadRequest // bad_request, 400 response from ES
    | Unauthorized //unauthorized a 401 response from ES
    | Forbidden //forbidden a 403 response from ES
    | Missing //missing a 404 response from ES
    | RequestTimeout //request_timeout a 408 response from ES
    | Conflict //conflict a 409 response from ES
    | Unavailable//unavailable a 503 response from ES
    | UnknownParameter //param a client-side error indicating an unknown parameter has been passed to the method
    | OtherBadResponse //request 4xx-5xx error response from ES, not equal to any named response above
    | Regex // /foo bar/ the text of the error message matches this regular expression
    
let (|IsDoCatch|_|) (s:string) =
    match s with
    | "bad_request" -> Some BadRequest 
    | "unauthorized " -> Some Unauthorized 
    | "forbidden " -> Some Forbidden 
    | "missing " -> Some Missing 
    | "request_timeout " -> Some RequestTimeout 
    | "conflict " -> Some Conflict 
    | "unavailable " -> Some Unavailable
    | "param " -> Some UnknownParameter
    | "request " -> Some OtherBadResponse 
    | "regex" -> Some Regex
    | _ -> None
    
type NodeSelector =
    | NodeVersionSelector of string
    | NodeAttributeSelector of string * string
    | NodeSelector of string * string * string

type ResponseProperty = ResponseProperty of string

type StashedId = private StashedId of string
    with static member Create s = StashedId <| sprintf "$%s" s
    
type SetTransformation = private SetTransformation of string
    with static member Create s = SetTransformation <| sprintf "$%s" s
type AssertPath = AssertPath of string

type Set = Map<ResponseProperty, StashedId>
type TransformAndSet = Map<StashedId, SetTransformation>
type Match = Map<AssertPath, Object>
type NumericMatch = Map<AssertPath, int64>
    
type Do = {
    ApiCall: string * Object
    Catch:DoCatch option
    Warnings:option<string list>
    NodeSelector:NodeSelector option
}

type NumericAssert = 
    | LowerThan 
    | GreaterThan 
    | GreaterThanOrEqual 
    | LowerThanOrEqual 
    | Equal 
    | Length

let (|IsNumericAssert|_|) (s:string) =
    match s with
    | "length" -> Some Length
    | "eq" -> Some Equal
    | "gte" -> Some GreaterThanOrEqual
    | "lte" -> Some LowerThanOrEqual
    | "gt" -> Some GreaterThan
    | "lt" -> Some LowerThan
    | _ -> None
    
type Assert = 
    | IsTrue of AssertPath
    | IsFalse of AssertPath
    | Match of Match
    | NumericAssert of NumericAssert * NumericMatch

type Operation =
    | Unknown of string
    | Skip of Skip
    | Do of Do
    | Set of Set
    | TransformAndSet of TransformAndSet
    | Assert of Assert
type Operations = Operation list

type YamlTest = {
    Name:string
    Operations: Operations
}
    
type YamlTestSection = | Setup of Operations | Teardown of Operations | YamlTest of YamlTest

