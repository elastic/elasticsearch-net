using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Serialization;
using Tests.QueryDsl.Geo.Shape;
using Tests.XPack.MachineLearning;

namespace Tests.CodeStandards.Serialization
{
	public class Enums
	{
		[U]
		public void EnumsWithEnumMembersShouldBeMarkedWithStringEnumAttribute()
		{
			var exceptions = new HashSet<Type>
			{
				typeof(SimpleQueryStringFlags)
			};

			var enums = typeof(IElasticClient).Assembly
				.GetTypes()
				.Where(t => t.IsEnum)
				.Except(exceptions)
				.ToList();
			var notMarkedStringEnum = new List<string>();
			foreach (var e in enums)
			{
				if (e.GetFields().Any(fi => fi.FieldType == e && fi.GetCustomAttribute<EnumMemberAttribute>() != null)
					&& e.GetCustomAttribute<StringEnumAttribute>() == null)
					notMarkedStringEnum.Add(e.Name);
			}
			notMarkedStringEnum.Should().BeEmpty();
		}

		[U]
		public void CanSerializeEnumsWithMultipleMembersMappedToSameValue()
		{
			var document = new EnumDocument
			{
				Int = HttpStatusCode.Moved,
				String = AnotherEnum.Value1
			};

			var client = new ElasticClient();

			var json = client.RequestResponseSerializer.SerializeToString(document);

			// "Value2" will be written for both "Value1" and "Value2" because the underlying integer value
			// for both is the same, and "Value2" field member is listed after "Value1", overwriting
			// the value mapping.
			//
			// Json.Net behaves similarly, except the first string mapped for a value
			// is not overwritten i.e. "Value1" will be written for both "Value1" and "Value2"
			json.Should().Be("{\"int\":301,\"string\":\"Value2\"}");

			EnumDocument deserializedDocument;
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
				deserializedDocument = client.RequestResponseSerializer.Deserialize<EnumDocument>(stream);

			deserializedDocument.Int.Should().Be(document.Int);
			deserializedDocument.String.Should().Be(document.String);
		}

		private class EnumDocument
	    {
			public HttpStatusCode Int { get;set;}

			public AnotherEnum String { get; set; }
		}

		[StringEnum]
		public enum AnotherEnum : int
		{
			Value1 = 1,
			Value2 = 1
		}
	}
}
