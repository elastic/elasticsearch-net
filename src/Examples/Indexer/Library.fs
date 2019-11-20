module Indexer
open System
open Nest

type Foobar =
    { nomnom: int
      data: Map<string, string>
      reallySo: bool
    }
    
let client = lazy (
    let password = "ladmkf3290adsk"
    let node = Uri (sprintf "http://elastic:%s@localhost:30200" password)
    let settings = (new ConnectionSettings(node)).DefaultIndex("foobars")
    new ElasticClient(settings)
)

let insert () =
    let resp = client.Value.Indices.Create("foobars-2018-09-02", fun c ->
        c.Map<Foobar>(fun m -> m.AutoMap())
    )
    resp
