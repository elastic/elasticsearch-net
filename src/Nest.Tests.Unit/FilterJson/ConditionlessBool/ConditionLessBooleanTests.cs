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

namespace Nest.Tests.Unit.FilterJson.ConditionlessBool
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

		private void DoSemiConditionlessQuery(Func<FilterDescriptor<ElasticSearchProject>, BaseFilter> filter, string Filename= "MatchAll")
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
			 .From(0)
			 .Take(10)
			 .Filter(filter);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), Filename);
		}

		[Test]
		public void CombinationResultsInNullQuery()
		{
			this.DoSemiConditionlessQuery(ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) && ff.Term(p => p.Name, _c.Name1));
			this.DoSemiConditionlessQuery(ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) || ff.Term(p => p.Name, _c.Name1));
			this.DoSemiConditionlessQuery(ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) || !ff.Term(p => p.Name, _c.Name1));
		}
		[Test]
		public void IfOneOfTwoIsNotConditionlessKeepIt()
		{
			this.DoSemiConditionlessQuery(Filename: "MyTerm", filter:ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) && ff.Term(p => p.Name, "myterm"));
			this.DoSemiConditionlessQuery(Filename: "MyTerm", filter:ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) || ff.Term(p => p.Name, "myterm"));
			this.DoSemiConditionlessQuery(Filename: "MyTermNot", filter:ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) || !ff.Term(p => p.Name, "myterm"));
		}
		[Test]
		public void IfOneOfThreeIsNotConditionlessKeepIt()
		{
			this.DoSemiConditionlessQuery(
				Filename: "MyTerm", 
				filter:
					ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) && ff.Terms(p => p.Name, new string[] { _c.Name2 }) && ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTerm", 
				filter:
					ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) || ff.Terms(p => p.Name, new string[] { _c.Name2 }) || ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermNot",
				filter:ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) || !ff.Terms(p => p.Name, new string[] { _c.Name2 }) || !ff.Term(p => p.Name, "myterm")
			);
		}
		[Test]
		public void IfTwoOfThreeIsNotConditionlessKeepThem()
		{
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsAnd",
				filter:
					ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) && ff.Terms(p => p.Name, new string[] { "myterm2" }) && ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsOr",
				filter:
					ff => ff.Terms(p => p.Name, new string[] { _c.Name1 }) || ff.Terms(p => p.Name, new string[] { "myterm2" }) || ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "MyTermsNot",
				filter:ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) || !ff.Terms(p => p.Name, new string[] { "myterm2" }) || !ff.Term(p => p.Name, "myterm")
			);
		}
		[Test]
		public void Combination()
		{
			this.DoSemiConditionlessQuery(
				Filename: "Combination1",
				filter:
					ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) && ff.Terms(p => p.Name, new string[] { "myterm2" }) || ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "Combination2",
				filter:
					ff => !ff.Terms(p => p.Name, new string[] { _c.Name1 }) || ff.Terms(p => p.Name, new string[] { "myterm2" }) && ff.Term(p => p.Name, "myterm")
			);
			this.DoSemiConditionlessQuery(
				Filename: "Combination3",
				filter:
					ff => !ff.Terms(p => p.Name, new string[] { "myterm" }) || ff.Terms(p => p.Name, new string[] { "myterm2" }) || ff.Term(p => p.Name, _c.Name1)
			);
		}
	}
}
