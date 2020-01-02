using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class GeoboundsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::34cabdecfe9c2cb8dd929853882564eb[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::34cabdecfe9c2cb8dd929853882564eb[]

			response0.MatchesExample(@"PUT /museums
			{
			    ""mappings"": {
			        ""properties"": {
			            ""location"": {
			                ""type"": ""geo_point""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"POST /museums/_bulk?refresh
			{""index"":{""_id"":1}}
			{""location"": ""52.374081,4.912350"", ""name"": ""NEMO Science Museum""}
			{""index"":{""_id"":2}}
			{""location"": ""52.369219,4.901618"", ""name"": ""Museum Het Rembrandthuis""}
			{""index"":{""_id"":3}}
			{""location"": ""52.371667,4.914722"", ""name"": ""Nederlands Scheepvaartmuseum""}
			{""index"":{""_id"":4}}
			{""location"": ""51.222900,4.405200"", ""name"": ""Letterenhuis""}
			{""index"":{""_id"":5}}
			{""location"": ""48.861111,2.336389"", ""name"": ""Musée du Louvre""}
			{""index"":{""_id"":6}}
			{""location"": ""48.860000,2.327000"", ""name"": ""Musée d'Orsay""}");

			response2.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""query"" : {
			        ""match"" : { ""name"" : ""musée"" }
			    },
			    ""aggs"" : {
			        ""viewport"" : {
			            ""geo_bounds"" : {
			                ""field"" : ""location"", \<1>
			                ""wrap_longitude"" : true \<2>
			            }
			        }
			    }
			}");
		}
	}
}