using Elasticsearch.Net.Connection;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Internals.Serialize
{
	public class PartialDoc
	{
		public string Name { get; set; }
		public DateTime? StartedOn { get; set; }
	}

	[TestFixture]
	public class NullValueHandlingTests : BaseJsonTests
	{
		[Test]
		public void IncludeNullValues_Dictionary_Test()
		{
			var client = GetTestClient(NullValueHandling.Include);

			var doc = new Dictionary<string, object>
			{
				{ "name", "newname" },
				{ "startedOn", null }
			};

			var expected = @"{ name: ""newname"", startedOn: null }";
			var json = Encoding.UTF8.GetString(client.Serializer.Serialize(doc));

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void IncludeNullValues_Object_Test()
		{
			var client = GetTestClient(NullValueHandling.Include);

			var doc = new PartialDoc
			{
				Name = "newname",
				StartedOn = null
			};

			var expected = @"{ name: ""newname"", startedOn: null }";
			var json = Encoding.UTF8.GetString(client.Serializer.Serialize(doc));

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void IgnoreNullValues_Dictionary_Test()
		{
			var client = GetTestClient(NullValueHandling.Ignore);

			var doc = new Dictionary<string, object>
			{
				{ "name", "newname" },
				{ "startedOn", null }
			};

			var expected = @"{ name: ""newname"" }";
			var json = Encoding.UTF8.GetString(client.Serializer.Serialize(doc));

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void IgnoreNullValues_Object_Test()
		{
			var client = GetTestClient(NullValueHandling.Ignore);

			var doc = new PartialDoc
			{
				Name = "newname",
				StartedOn = null
			};

			var expected = @"{ name: ""newname"" }";
			var json = Encoding.UTF8.GetString(client.Serializer.Serialize(doc));

			Assert.True(json.JsonEquals(expected), json);
		}

		private IElasticClient GetTestClient(NullValueHandling nullValueHandling)
		{
			var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
				.SetJsonSerializerSettingsModifier(m => m
					.NullValueHandling = nullValueHandling
				);
			var connection = new InMemoryConnection(settings);
			var client = new ElasticClient(settings, connection);

			return client;
		}
	}
}
