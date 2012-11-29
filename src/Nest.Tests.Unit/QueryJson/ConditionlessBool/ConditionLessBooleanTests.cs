using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryJson.ConditionlessBool
{
	[TestFixture]
	public class ConditionlessBoolTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoSemiConditionlessQuery(Func<QueryDescriptor<ElasticSearchProject>, BaseQuery> query, string Filename= "MatchAll")
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
			 .From(0)
			 .Take(10)
			 .Query(query);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), Filename);
		}

		[Test]
		public void CombinationResultsInNullQuery()
		{
			this.DoSemiConditionlessQuery(q => q.Terms(p => p.Name, new string[] { _c.Name1 }) && q.Term(p => p.Name, _c.Name1));
			this.DoSemiConditionlessQuery(q => q.Terms(p => p.Name, new string[] { _c.Name1 }) || q.Term(p => p.Name, _c.Name1));
			this.DoSemiConditionlessQuery(q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) || !q.Term(p => p.Name, _c.Name1));
		}
		[Test]
		public void IfOneOfTwoIsNotConditionlessKeepIt()
		{
			this.DoSemiConditionlessQuery(Filename: "MyTerm", query: q => q.Terms(p => p.Name, new string[] { _c.Name1 }) && q.Term(p => p.Name, "myterm"));
			this.DoSemiConditionlessQuery(Filename: "MyTerm", query: q => q.Terms(p => p.Name, new string[] { _c.Name1 }) || q.Term(p => p.Name, "myterm"));
			this.DoSemiConditionlessQuery(Filename: "MyTermNot", query: q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) || !q.Term(p => p.Name, "myterm"));
		}
		[Test]
		public void IfOneOfThreeIsNotConditionlessKeepIt()
		{
			this.DoSemiConditionlessQuery(
				Filename: "MyTerm", 
				query: 
					q => q.Terms(p => p.Name, new string[] { _c.Name1 }) && q.Terms(p => p.Name, new string[] { _c.Name2 }) && q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTerm", 
				query:
					q => q.Terms(p => p.Name, new string[] { _c.Name1 }) || q.Terms(p => p.Name, new string[] { _c.Name2 }) || q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermNot",
				query: q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) || !q.Terms(p => p.Name, new string[] { _c.Name2 }) || !q.Term(p => p.Name, "myterm")
			);
		}
		[Test]
		public void IfTwoOfThreeIsNotConditionlessKeepThem()
		{
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsAnd",
				query:
					q => q.Terms(p => p.Name, new string[] { _c.Name1 }) && q.Terms(p => p.Name, new string[] { "myterm2" }) && q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsOr",
				query:
					q => q.Terms(p => p.Name, new string[] { _c.Name1 }) || q.Terms(p => p.Name, new string[] { "myterm2" }) || q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsNot",
				query: q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) || !q.Terms(p => p.Name, new string[] { "myterm2" }) || !q.Term(p => p.Name, "myterm")
			);
		}
		[Test]
		public void Combination()
		{
			this.DoSemiConditionlessQuery(
				Filename: "Combination1",
				query:
					q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) && q.Terms(p => p.Name, new string[] { "myterm2" }) || q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "Combination2",
				query:
					q => !q.Terms(p => p.Name, new string[] { _c.Name1 }) || q.Terms(p => p.Name, new string[] { "myterm2" }) && q.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "Combination3",
				query:
					q => !q.Terms(p => p.Name, new string[] { "myterm" }) || q.Terms(p => p.Name, new string[] { "myterm2" }) || q.Term(p => p.Name, _c.Name1)
			);
		}
	}
}
