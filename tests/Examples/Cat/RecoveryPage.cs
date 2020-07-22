// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class RecoveryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/recovery.asciidoc:75")]
		public void Line75()
		{
			// tag::0497361415e63295f2151b34818ad1ab[]
			var response0 = new SearchResponse<object>();
			// end::0497361415e63295f2151b34818ad1ab[]

			response0.MatchesExample(@"GET _cat/recovery?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/recovery.asciidoc:104")]
		public void Line104()
		{
			// tag::be9390dd19d724364b72bf081b3593d7[]
			var response0 = new SearchResponse<object>();
			// end::be9390dd19d724364b72bf081b3593d7[]

			response0.MatchesExample(@"GET _cat/recovery?v&h=i,s,t,ty,st,shost,thost,f,fp,b,bp");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/recovery.asciidoc:134")]
		public void Line134()
		{
			// tag::a213728fa704ca23c5983809332d3fb3[]
			var response0 = new SearchResponse<object>();
			// end::a213728fa704ca23c5983809332d3fb3[]

			response0.MatchesExample(@"GET _cat/recovery?v&h=i,s,t,ty,st,rep,snap,f,fp,b,bp");
		}
	}
}
