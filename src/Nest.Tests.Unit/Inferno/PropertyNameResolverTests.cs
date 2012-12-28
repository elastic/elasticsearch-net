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
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Inferno
{
	[TestFixture]
	public class PropertyNameResolverTests : BaseJsonTests
	{
		[ElasticType(IdProperty = "Guid")]
		internal class SomeClass
		{
			public MyCustomClass MyCustomClass { get; set; }
		}
		[ElasticType(IdProperty = "Guid")]
		internal class SomeOtherClass
		{
			[ElasticProperty(Name = "custom")]
			public MyCustomClass MyCustomClass { get; set; }
			[ElasticProperty(Name = "CreateDate")]
			public DateTime CreateDate { get; set; }

			public MyCustomOtherClass MyCustomOtherClass { get; set; }
		}
		internal class MyCustomClass
		{
			[ElasticProperty(Name = "MID")]
			public string MyProperty { get; set; }

			public override string ToString()
			{
				return "static id ftw";
			}
		}
		[ElasticType(IdProperty = "Guid", Name = "mycustomother")]
		internal class MyCustomOtherClass
		{
			[ElasticProperty(Name = "MID")]
			public string MyProperty { get; set; }

			public override string ToString()
			{
				return "static id ftw";
			}
		}
		[Test]
		public void TestUsesElasticProperty()
		{
			Expression<Func<SomeClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			var propertyName = new PropertyNameResolver().Resolve(exp);
			var expected = "myCustomClass.MID";
			Assert.AreEqual(expected, propertyName);
		}
		[Test]
		public void TestUsesOtherElasticProperty()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			var propertyName = new PropertyNameResolver().Resolve(exp);
			var expected = "custom.MID";
			Assert.AreEqual(expected, propertyName);
		}
		[Test]
		public void TestUsesOtherElasticTypePropertyIsIgnored()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomOtherClass.MyProperty;
			var propertyName = new PropertyNameResolver().Resolve(exp);
			var expected = "myCustomOtherClass.MID";
			Assert.AreEqual(expected, propertyName);
		}
		[Test]
		public void TestCreatedDate()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.CreateDate;
			var propertyName = new PropertyNameResolver().Resolve(exp);
			var expected = "CreateDate";
			Assert.AreEqual(expected, propertyName);
		}
		[Test] 
		public void SearchUsesTheProperResolver()
		{
			var result = this._client.Search<SomeOtherClass>(s => s
			  .SortDescending(f => f.MyCustomOtherClass.MyProperty)
			  .Query(query => query
				.Bool(bq => bq
				  .Must(
					mq => mq.ConstantScore(cs => cs.Filter(filter => filter.Term(x => x.MyCustomClass.MyProperty, "meesageid"))),
					mp => mp.ConstantScore(cs => cs.Filter(filter => filter.Term(x => x.MyCustomOtherClass.MyProperty, "serverid")))
				  )
				)
				&& query.Term(f=>f.CreateDate, "x")
			  )
			);
			StringAssert.Contains("custom.MID", result.ConnectionStatus.Request);
			StringAssert.Contains("myCustomOtherClass.MID", result.ConnectionStatus.Request);
			StringAssert.Contains("CreateDate", result.ConnectionStatus.Request);
		}

		[Test] 
		public void SearchDoesntLowercaseStringFieldOverload()
		{
			var result = this._client.Search<SomeOtherClass>(s => s
			  .SortDescending("CreateDate2")
			  .FacetDateHistogram("CreateDate2", fd => fd.OnField("CreateDate2").Interval(DateInterval.Hour))
			  .MatchAll()
			);
			StringAssert.DoesNotContain("createDate2", result.ConnectionStatus.Request);
		}
		[Test]
		public void SearchDoesntLowercaseStringFieldOverloadInSearch()
		{
			var result = this._client.Search<SomeOtherClass>(s => s
			  .SortDescending("CreateDate2")
			  .FacetDateHistogram("CreateDate2", fd => fd.OnField("CreateDate2").Interval(DateInterval.Hour))
			  .Query(query => query.Range(r => r
				  .OnField("CreateDate2")
				  .From(DateTime.UtcNow.AddYears(-1))

				)
			  )
			);
			StringAssert.DoesNotContain("createDate2", result.ConnectionStatus.Request);
		}
	}
}
