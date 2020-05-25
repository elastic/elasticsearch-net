// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Sql
{
	public class GettingStartedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("sql/getting-started.asciidoc:10")]
		public void Line10()
		{
			// tag::0a46ac2968a574ce145f197f10d30152[]
			var response0 = new SearchResponse<object>();
			// end::0a46ac2968a574ce145f197f10d30152[]

			response0.MatchesExample(@"PUT /library/_bulk?refresh
			{""index"":{""_id"": ""Leviathan Wakes""}}
			{""name"": ""Leviathan Wakes"", ""author"": ""James S.A. Corey"", ""release_date"": ""2011-06-02"", ""page_count"": 561}
			{""index"":{""_id"": ""Hyperion""}}
			{""name"": ""Hyperion"", ""author"": ""Dan Simmons"", ""release_date"": ""1989-05-26"", ""page_count"": 482}
			{""index"":{""_id"": ""Dune""}}
			{""name"": ""Dune"", ""author"": ""Frank Herbert"", ""release_date"": ""1965-06-01"", ""page_count"": 604}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/getting-started.asciidoc:23")]
		public void Line23()
		{
			// tag::53b14d640c4c48a5e7ea86ddc26bee64[]
			var response0 = new SearchResponse<object>();
			// end::53b14d640c4c48a5e7ea86ddc26bee64[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library WHERE release_date < '2000-01-01'""
			}");
		}
	}
}
