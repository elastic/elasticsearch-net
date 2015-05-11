namespace ``Search APIs``

open Xunit
open Nest
open System
open Nest.Tests
open FsUnit

type Rec = { x: int }

module ``Request Body Search`` =
    type ``From and Size``() =
        inherit BaseTest<ISearchRequest, SearchDescriptor<Rec>, SearchRequest>()

(**
Pagination of results can be done by using the from and size parameters. 
The from parameter defines the offset from the first result you want to fetch. 
The size parameter allows you to configure the maximum amount of hits to be returned.
*)
        override test.ExpectedJson(map) = 
            map
                .Add("from", 12)
                .Add("size", 10)

        override test.Initializer = new SearchRequest (From= Nullable 12, Size= Nullable 10);

        override test.Fluent(descriptor) = 
            descriptor 
                .From(12)
                .Size(10)
        
        [<Fact>]
        member test.``Search behaves properly``() = 
            let result = test.Client.Search<Rec>(test.Fluent)
            result.IsValid |> should be True

