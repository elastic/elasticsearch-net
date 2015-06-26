using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Unit.Internals.Serialize
{
	[TestFixture]
	public class CustomConvertersTests : BaseJsonTests
	{
		private readonly ElasticClient _serializationClient;

		public class MyObject
		{
			public MyEnum MyEnum { get; set; }
			public NestedObject Nested { get; set; }
		}
		public enum MyEnum
		{
			Foo = 0,
			Bar = 1
		}

		public class NestedObject
		{
			public string Name { get; set; }
		}

		public CustomConvertersTests()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.SetDefaultIndex("nest_test_data")
				.AddContractJsonConverters(
					t => typeof(Enum).IsAssignableFrom(t) ? new Newtonsoft.Json.Converters.StringEnumConverter() : null,
					t => typeof(NestedObject).IsAssignableFrom(t) ? new CustomConverter() : null
				);

			 _serializationClient = new ElasticClient(settings);
		}


		[Test]
		public void ObjectSerializationTakesConvertersIntoAccount()
		{
			var o = new MyObject() { MyEnum = MyEnum.Foo, Nested = new NestedObject() };
			var serializedObject = _serializationClient.Serializer.Serialize(o).Utf8String();
			serializedObject.JsonEquals(@"
				{
					""myEnum"" : ""Foo"",
					""nested"" : ""something""
				}").Should().BeTrue();

		}

		[Test]
		public void StringificationTakesConvertersIntoAccount()
		{
			var query = new QueryDescriptor<MyObject>().Term(t => t.MyEnum, MyEnum.Foo);
			var serialized = _serializationClient.Serializer.Serialize(query).Utf8String();
			serialized.JsonEquals(@"
				{
					""term"" : {
						""myEnum"" : ""Foo""
					}
				}").Should().BeTrue();
		}

		public class CustomConverter : JsonConverter
		{
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteValue("something");
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				return new NestedObject();
			}

			public override bool CanConvert(Type objectType)
			{
				return true;
			}
		}
	}
}
