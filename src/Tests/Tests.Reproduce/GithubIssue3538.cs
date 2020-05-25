using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Tests.Core.Client.Settings;

namespace Tests.Reproduce
{
	public class GithubIssue3538
	{
		[U] public void EmptyPolicyCausesNullReferenceException()
		{
			var converter = new MyPocoJsonConverter();
			var connectionSettings = new AlwaysInMemoryConnectionSettings(
					sourceSerializerFactory: (builtin, settings) => new JsonNetSerializer(builtin, settings,
						() =>
						{
							var jSettings = new JsonSerializerSettings();
							jSettings.Converters.Add(converter);
							return jSettings;
						}))
				.DefaultIndex("blah");

			var client = new ElasticClient(connectionSettings);

			var result = client.IndexDocument(new MyPoco() { Id = 1, Name = "x" });
			converter.CanConvertCallCount.Should().Be(1);

			client.Bulk(b => b.Index<MyPoco>(i=>i.Document(new MyPoco { Id = 2, Name = "y" })));
			converter.CanConvertCallCount.Should().Be(2);

			client.Bulk(b => b.Update<MyPoco>(i=>i.Doc(new MyPoco { Id = 3, Name = "z" })));
			converter.CanConvertCallCount.Should().Be(3);

			client.Bulk(b => b.Update<MyPoco>(i=>i.Upsert(new MyPoco { Id = 4, Name = "a" })));
			converter.CanConvertCallCount.Should().Be(4);

			converter.WriteCount.Should().Be(4);
			converter.SeenIds.Should().BeEquivalentTo(1, 2, 3, 4);

		}

		public class MyPoco
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class MyPocoJsonConverter : JsonConverter
		{
			private int _canConvertCallCount;
			public int CanConvertCallCount => _canConvertCallCount;

			private int _writeCount;
			public int WriteCount => _writeCount;

			public List<int> SeenIds { get; } = new List<int>();


			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				Interlocked.Increment(ref _writeCount);

				if (!(value is MyPoco poco))
				{
					writer.WriteNull();
					return;
				}

				writer.WriteStartObject();
				writer.WritePropertyName("id");
				writer.WriteValue(poco.Id);
				SeenIds.Add(poco.Id);
				writer.WritePropertyName("name");
				writer.WriteValue(poco.Name);
				writer.WriteEndObject();
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
				new MyPoco { };

			public override bool CanConvert(Type objectType)
			{
				Interlocked.Increment(ref _canConvertCallCount);

				return objectType == typeof(MyPoco);
			}
		}
	}
}
