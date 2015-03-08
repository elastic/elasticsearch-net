using FluentAssertions;
using Elasticsearch.Net.Connection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class MapIdPropertyForTests : BaseJsonTests
	{
		[Test]
		public void MapNumericIdProperty()
		{
			var settings = new ConnectionSettings()
				.MapIdPropertyFor<ElasticsearchProject>(p => p.LongValue);

			var client = new ElasticClient(settings, connection: new InMemoryConnection());

			var project = new ElasticsearchProject { LongValue = 123 };

			Assert.AreEqual(project.LongValue.ToString(), client.Infer.Id<ElasticsearchProject>(project));
		}

		[Test]
		public void MapStringIdProperty()
		{
			var settings = new ConnectionSettings()
				.MapIdPropertyFor<ElasticsearchProject>(p => p.Name);

			var client = new ElasticClient(settings, connection: new InMemoryConnection());

			var project = new ElasticsearchProject { Name = "foo" };

			Assert.AreEqual(project.Name, client.Infer.Id<ElasticsearchProject>(project));
		}

		[Test]
		public void MapIdPropertiesForMultipleTypes()
		{
			var settings = new ConnectionSettings()
				.MapIdPropertyFor<ElasticsearchProject>(p => p.LongValue)
				.MapIdPropertyFor<Person>(p => p.Id)
				.MapIdPropertyFor<Product>(p => p.Name);

			var client = new ElasticClient(settings, connection: new InMemoryConnection());

			var project = new ElasticsearchProject { LongValue = 1 };
			Assert.AreEqual(project.LongValue.ToString(), client.Infer.Id<ElasticsearchProject>(project));

			var person = new Person { Id = 2 };
			Assert.AreEqual(person.Id.ToString(), client.Infer.Id<Person>(person));

			var product = new Product { Name = "foo" };
			Assert.AreEqual(product.Name, client.Infer.Id<Product>(product));
		}

		[Test]
		public void IdPropertyNotMapped_IdIsInferred()
		{
			var settings = new ConnectionSettings();
			var client = new ElasticClient(settings, connection: new InMemoryConnection());
			var project = new ElasticsearchProject { Id = 123 };

			Assert.AreEqual(project.Id.ToString(), client.Infer.Id<ElasticsearchProject>(project));
		}

		[Test]
		public void DifferentIdPropertyMappedTwice_ThrowsException()
		{
			var e = Assert.Throws<ArgumentException>(() =>
			{
				var settings = new ConnectionSettings()
					.MapIdPropertyFor<ElasticsearchProject>(p => p.Id)
					.MapIdPropertyFor<ElasticsearchProject>(p => p.Name);
			});

			e.Message
				.Should()
				.Be("Cannot map 'Name' as the id property for type 'ElasticsearchProject': it already has 'Id' mapped.");
		}

		[Test]
		public void SameIdPropertyMappedTwice_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() =>
			{
				var settings = new ConnectionSettings()
					.MapIdPropertyFor<ElasticsearchProject>(p => p.Id)
					.MapIdPropertyFor<ElasticsearchProject>(p => p.Id);
			});
		}


		[ElasticType(IdProperty = "Name")]
		public class IdPropertyTestWithAttribute
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}

		[Test]
		public void IdPropertyMapped_And_TypeHasIdPropertyAttribute_MappingTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.MapIdPropertyFor<IdPropertyTestWithAttribute>(o => o.Id);

			var client = new ElasticClient(settings, connection: new InMemoryConnection());

			var doc = new IdPropertyTestWithAttribute
			{
				Id = "should-be-the-id",
				Name = "should-not-be-the-id"
			};

			Assert.AreEqual(doc.Id, client.Infer.Id<IdPropertyTestWithAttribute>(doc));
		}
	}
}
