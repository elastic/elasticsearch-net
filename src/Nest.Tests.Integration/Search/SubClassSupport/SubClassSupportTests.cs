using System;
using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.SearchType
{
	[TestFixture]
	public class SubClassSupportTests : IntegrationTests
	{
		private Random _random = new Random(1337);

		public abstract class MyBaseClass
		{
			public string Title { get; set; }
		}
		public class ClassA : MyBaseClass
		{
			public string ClassAProperty { get; set; }
		}
		public class ClassB : MyBaseClass
		{
			public string ClassBProperty { get; set; }
		}

		[Test]
		public void SingleIndexWithMultipleTypes()
		{
			var data = Enumerable.Range(0, 100)
				.Select(i =>
				{
					MyBaseClass o = null;
					if (i % 2 == 0)
						o = new ClassA() { ClassAProperty = Guid.NewGuid().ToString() };
					else
						o = new ClassB() { ClassBProperty = Guid.NewGuid().ToString() };
					o.Title = Guid.NewGuid().ToString();
					return o;
				});

			var result = this._client.Bulk(b => b.IndexMany(data).Refresh());

			result.IsValid.Should().BeTrue();
			result.Items.Count().Should().Be(100);

			var queryResults = this._client.Search<MyBaseClass>(s => s
				.From(0)
				.Size(100)
				.MatchAll()
				.ConcreteTypeSelector((o, h) => o.classBProperty != null ? typeof(ClassB) : typeof(ClassA))
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Documents.Any());

			queryResults.Documents.OfType<ClassA>().Any().Should().BeTrue();
			queryResults.Documents.OfType<ClassB>().Any().Should().BeTrue();
		}

		[Test]
		public void MultipleTypesUsingBaseClass()
		{
			var data = Enumerable.Range(0, 100)
				.Select(i =>
				{
					MyBaseClass o = null;
					if (i % 2 == 0)
						o = new ClassA() { ClassAProperty = Guid.NewGuid().ToString() };
					else
						o = new ClassB() { ClassBProperty = Guid.NewGuid().ToString() };
					o.Title = Guid.NewGuid().ToString();
					return o;
				});

			var resulta = this._client.Bulk(b => b.IndexMany(data.OfType<ClassA>()).Refresh());
			var resultb = this._client.Bulk(b => b.IndexMany(data.OfType<ClassB>()).Refresh());

			var queryResults = this._client.Search<MyBaseClass>(s => s
				.Types(typeof(ClassA), typeof(ClassB))
				.From(0)
				.Size(100)
				.MatchAll()
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Documents.Any());

			queryResults.Documents.OfType<ClassA>().Any().Should().BeTrue();
			queryResults.Documents.OfType<ClassB>().Any().Should().BeTrue();
		}

		[Test]
		public void MultipleTypesUsingBaseClassMultiSearch()
		{
			var data = Enumerable.Range(0, 100)
				.Select(i =>
				{
					MyBaseClass o = null;
					if (i % 2 == 0)
						o = new ClassA() { ClassAProperty = Guid.NewGuid().ToString() };
					else
						o = new ClassB() { ClassBProperty = Guid.NewGuid().ToString() };
					o.Title = Guid.NewGuid().ToString();
					return o;
				});

			var resulta = this._client.Bulk(b => b.IndexMany(data.OfType<ClassA>()).Refresh());
			var resultb = this._client.Bulk(b => b.IndexMany(data.OfType<ClassB>()).Refresh());

			var queryResults = this._client.MultiSearch(ms => ms
				.Search<MyBaseClass>("using_types", s => s.AllIndices()
					.Types(typeof(ClassA), typeof(ClassB))
					.From(0)
					.Size(100)
					.MatchAll()
				)
				.Search<MyBaseClass>("using_selector", s => s.AllIndices()
					.Types("classa", "classb")
					.ConcreteTypeSelector((o, h) => o.classBProperty != null ? typeof(ClassB) : typeof(ClassA))
					.From(0)
					.Size(100)
					.MatchAll()
				)
			);
			Assert.True(queryResults.IsValid);
			var firstResult = queryResults.GetResponse<MyBaseClass>("using_types");

			Assert.True(firstResult.Documents.Any());
			firstResult.Documents.OfType<ClassA>().Any().Should().BeTrue();
			firstResult.Documents.OfType<ClassB>().Any().Should().BeTrue();

			var secondResult = queryResults.GetResponse<MyBaseClass>("using_selector");

			Assert.True(secondResult.Documents.Any());
			secondResult.Documents.OfType<ClassA>().Any().Should().BeTrue();
			secondResult.Documents.OfType<ClassB>().Any().Should().BeTrue();
		}
	}
}