using System;
using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles.Term
{
	[TestFixture]
	public class TermToStringJson : BaseJsonTests
	{
		[Test]
		public void IntToStringTest()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p=>p.LOC, 20000)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void DoubleToStringTest()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p => p.DoubleValue, 20.5)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void FloatToStringTest()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p => p.FloatValue, 20.9f)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void LongToStringTest()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p => p.LongValue, 20000L)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void DateTimeToStringTest()
		{
			//this should serialize to ISO NOT simply datetime.tostring()!
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p => p.StartedOn, new DateTime(1999,2,2))
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void DateTimeWithCustomStringValue()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Term(p => p.StartedOn, "1986-03-08")
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
