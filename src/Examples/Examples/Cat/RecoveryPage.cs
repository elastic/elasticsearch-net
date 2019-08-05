using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class RecoveryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line16()
		{
			// tag::0497361415e63295f2151b34818ad1ab[]
			var response0 = new SearchResponse<object>();
			// end::0497361415e63295f2151b34818ad1ab[]

			response0.MatchesExample(@"GET _cat/recovery?v");
		}

		[U]
		[SkipExample]
		public void Line43()
		{
			// tag::be9390dd19d724364b72bf081b3593d7[]
			var response0 = new SearchResponse<object>();
			// end::be9390dd19d724364b72bf081b3593d7[]

			response0.MatchesExample(@"GET _cat/recovery?v&h=i,s,t,ty,st,shost,thost,f,fp,b,bp");
		}

		[U]
		[SkipExample]
		public void Line71()
		{
			// tag::a213728fa704ca23c5983809332d3fb3[]
			var response0 = new SearchResponse<object>();
			// end::a213728fa704ca23c5983809332d3fb3[]

			response0.MatchesExample(@"GET _cat/recovery?v&h=i,s,t,ty,st,rep,snap,f,fp,b,bp");
		}
	}
}