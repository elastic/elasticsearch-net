using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using NUnit.Framework;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class PropertyNameResolverTests : BaseJsonTests
	{
		[ElasticType(IdProperty = "Guid")]
		internal class SomeClass
		{
			public MyCustomClass MyCustomClass { get; set; }
			[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
			public Dictionary<string, SomeOtherClass> StringDict { get; set; }
			public Dictionary<int, MyCustomClass> IntDict { get; set; }
			public IList<MyCustomClass> ListOfCustomClasses { get; set; } 
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

		internal class UserItemData
		{
			public string Id { get; set; }
			public string UserId { get; set; }
			public string Title { get; set; }
			public string Hidden { get; set; }
			public string UserLabels { get; set; }
		}

		[Test]
		public void TestUsesDefaultPropertyNameResolver()
		{
			var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
				.SetDefaultPropertyNameInferrer(p => p);
			var client = new ElasticClient(settings);
			Expression<Func<UserItemData, object>> exp = (m) => m.UserLabels;
			var propertyName = client.Infer.PropertyPath(exp);
			Assert.AreEqual("UserLabels", propertyName);

		}

		[Test]
		public void TestUsesElasticProperty()
		{
			Expression<Func<SomeClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "myCustomClass.MID";
			Assert.AreEqual(expected, propertyName);
		}
		
		[Test]
		public void TestUsesOtherElasticProperty()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "custom.MID";
			Assert.AreEqual(expected, propertyName);
		}
		
		[Test]
		public void TestUsesOtherElasticTypePropertyIsIgnored()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomOtherClass.MyProperty;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "myCustomOtherClass.MID";
			Assert.AreEqual(expected, propertyName);
		}
		
		[Test]
		public void TestCreatedDate()
		{
			Expression<Func<SomeOtherClass, object>> exp = (m) => m.CreateDate;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "CreateDate";
			Assert.AreEqual(expected, propertyName);
		}
		
		[Test]
		public void TestDictionaryConstStringExpression()
		{
			Expression<Func<SomeClass, object>> exp = (m) => m.StringDict["someValue"].CreateDate;
			var propertyName =_client.Infer.PropertyPath(exp);
			var expected = "stringDict.someValue.CreateDate";
			Assert.AreEqual(expected, propertyName);
		}

		[Test]
		public void TestDictionaryConstIntExpression()
		{
			Expression<Func<SomeClass, object>> exp = (m) => m.IntDict[101].MyProperty;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "intDict.101.MID";
			Assert.AreEqual(expected, propertyName);
		}

        [Test]
        public void TestDictionaryStringExpression()
        {
            string index = "someValue";
            Expression<Func<SomeClass, object>> exp = (m) => m.StringDict[index].CreateDate;
            var propertyName = _client.Infer.PropertyPath(exp);
            var expected = "stringDict.someValue.CreateDate";
            Assert.AreEqual(expected, propertyName);
        }

        [Test]
        public void TestDictionaryIntExpression()
        {
            var index = 101;
            Expression<Func<SomeClass, object>> exp = (m) => m.IntDict[index].MyProperty;
            var propertyName =_client.Infer.PropertyPath(exp);
            var expected = "intDict.101.MID";
            Assert.AreEqual(expected, propertyName);
        }

        [Test]
        public void TestDictionaryStringDiffValues()
        {
            string index = "someValue1";
            Expression<Func<SomeClass, object>> exp = (m) => m.StringDict[index].CreateDate;
            var propertyName =_client.Infer.PropertyPath(exp);
            var expected1 = "stringDict.someValue1.CreateDate";
            Assert.AreEqual(expected1, propertyName);
            index = "someValue2";
            exp = (m) => m.StringDict[index].CreateDate;
            propertyName = _client.Infer.PropertyPath(exp);
            var expected2 = "stringDict.someValue2.CreateDate";
            Assert.AreEqual(expected2, propertyName);
        }

		[Test]
		public void TestCollectionIndexExpressionDoesNotEndUpInPath()
		{
			Expression<Func<SomeClass, object>> exp = (m) => m.ListOfCustomClasses[101].MyProperty;
			var propertyName = _client.Infer.PropertyPath(exp);
			var expected = "listOfCustomClasses.MID";
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
			var request = result.ConnectionStatus.Request.Utf8String();
			StringAssert.Contains("custom.MID", request);
			StringAssert.Contains("myCustomOtherClass.MID", request);
			StringAssert.Contains("CreateDate", request);
		}

		[Test] 
		public void SearchDoesntLowercaseStringFieldOverload()
		{
			var result = this._client.Search<SomeOtherClass>(s => s
			  .SortDescending("CreateDate2")
			  .FacetDateHistogram("CreateDate2", fd => fd.OnField("CreateDate2").Interval(DateInterval.Hour))
			  .MatchAll()
			);
			var request = result.ConnectionStatus.Request.Utf8String();
			StringAssert.DoesNotContain("createDate2", request);
		}
		[Test]
		public void SearchDoesntLowercaseStringFieldOverloadInSearch()
		{
			var result = this._client.Search<SomeOtherClass>(s => s
			  .SortDescending("CreateDate2")
			  .FacetDateHistogram("CreateDate2", fd => fd.OnField("CreateDate2").Interval(DateInterval.Hour))
			  .Query(query => query.Range(r => r
				  .OnField("CreateDate2")
				  .Greater(DateTime.UtcNow.AddYears(-1))
				)
			  )
			);
			StringAssert.DoesNotContain("createDate2", result.ConnectionStatus.Request.Utf8String());
		}
	}
}
