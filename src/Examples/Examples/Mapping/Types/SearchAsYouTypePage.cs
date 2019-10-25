using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;
using Newtonsoft.Json.Linq;

namespace Examples.Mapping.Types
{
	public class SearchAsYouTypePage : ExampleBase
	{
		[U]
		public void Line18()
		{
			// tag::6f31f9cfe0dd741ccad4af62ba8f815e[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map<MyDocument>(m => m
					.Properties(p => p
						.SearchAsYouType(t => t
							.Name(n => n.MyField)
						)
					)
				)
			);
			// end::6f31f9cfe0dd741ccad4af62ba8f815e[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_field"": {
			        ""type"": ""search_as_you_type""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line71()
		{
			// tag::867e5fad9c57055712fe2b69fa69a97c[]
			var indexResponse = client.Index(new MyDocument
			{
				MyField = "quick brown fox jump lazy dog"
			}, i => i
				.Index("my_index")
				.Id(1)
				.Refresh(Refresh.True)
			);
			// end::867e5fad9c57055712fe2b69fa69a97c[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""my_field"": ""quick brown fox jump lazy dog""
			}", e =>
			{
				// query string params always need a value
				var uri = e.Uri.ToString().Replace("refresh", "refresh=true");
				e.Uri = new Uri(uri);
				return e;
			});
		}

		[U]
		public void Line87()
		{
			// tag::9bd25962f177e86dbc5a8030a420cc31[]
			var query = client.Search<MyDocument>(s => s
				.Index("my_index")
				.Query(q => q
					.MultiMatch(mm => mm
						.Query("brown f")
						.Type(TextQueryType.BoolPrefix)
						.Fields(f => f
							.Field(ff => ff.MyField)
							.Field(ff => ff.MyField.Suffix("_2gram"))
							.Field(ff => ff.MyField.Suffix("_3gram"))
						)
					)
				)
			);
			// end::9bd25962f177e86dbc5a8030a420cc31[]

			query.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""multi_match"": {
			      ""query"": ""brown f"",
			      ""type"": ""bool_prefix"",
			      ""fields"": [
			        ""my_field"",
			        ""my_field._2gram"",
			        ""my_field._3gram""
			      ]
			    }
			  }
			}");
		}

		[U]
		public void Line147()
		{
			// tag::0ced86822f8c0a479af5e1fe28dfc2ec[]
			var searchResponse = client.Search<MyDocument>(s => s
				.Index("my_index")
				.Query(q => q
					.MatchPhrasePrefix(mpp => mpp
						.Field(f => f.MyField)
						.Query("brown f")
					)
				)
			);
			// end::0ced86822f8c0a479af5e1fe28dfc2ec[]

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match_phrase_prefix"": {
			      ""my_field"": ""brown f""
			    }
			  }
			}", e =>
			{
				// client does not support short form match_phrase_prefix
				e.ApplyBodyChanges(body =>
				{
					var value = body["query"]["match_phrase_prefix"]["my_field"];
					body["query"]["match_phrase_prefix"]["my_field"] = new JObject { { "query", value } };
				});
				return e;
			});
		}
	}
}
