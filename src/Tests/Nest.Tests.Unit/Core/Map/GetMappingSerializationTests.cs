using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Core.Map
{
	class GetMappingSerializationTests : BaseJsonTests
	{
		/// <summary>
		/// Verify that we can serialize/deserialize a populated root mapping.
		/// If we can round trip a full root mapping, we're in a pretty good place.
		/// </summary>
		[Test]
		public void CanDeserializeRootMapping()
		{
			var rootMapping = new RootObjectMapping()
			{
				AllFieldMapping = new AllFieldMapping()
				{
					Enabled = true,
					IndexAnalyzer = "index_analyzer"
				},
				SourceFieldMappingDescriptor = new SourceFieldMapping()
				{
					Compress = false,
					Excludes = new[] { "excluded" }
				},
				RoutingFieldMapping = new RoutingFieldMapping()
				{
					Path = "routing_path"
				},
				SizeFieldMapping = new SizeFieldMapping()
				{
					Enabled = true,
				},
				TtlFieldMappingDescriptor = new TtlFieldMapping()
				{
					Enabled = true,
				},
				IdFieldMappingDescriptor = new IdFieldMapping()
				{
					Index = "not_analyzed",
					Store = false,
					Path = "id_field",
				},
				TimestampFieldMapping = new TimestampFieldMapping()
				{
					Enabled = true,
					Format = "YYY-MM-dd",
					Path = "the_timestamp",
				},
				IndexFieldMapping = new IndexFieldMapping()
				{
					Enabled = true,
				},
				AnalyzerFieldMapping = new AnalyzerFieldMapping()
				{
					Path = "index_path",
				},
				BoostFieldMapping = new BoostFieldMapping()
				{
					Name = "boost",
					NullValue = 2.0,
				},
				Parent = new ParentFieldMapping
				{
					Type = "type"
				},
				TypeFieldMappingDescriptor = new TypeFieldMapping()
				{
					Index = NonStringIndexOption.No, 
					Store = false,
				}
			};
			var json = TestElasticClient.Serialize(rootMapping);

			var mapping = TestElasticClient.Deserialize<RootObjectMapping>(json);
			TestElasticClient.Serialize(mapping).JsonEquals(json);
		}

		/// <summary>
		/// Verify that we can serialize/deserialize an empty root mapping. One of the
		/// failure modes of serialization/deserialization is handling data that's not there.
		/// </summary>
		[Test]
		public void CanDeserializeEmptyRootMapping()
		{
			var rootMapping = new RootObjectMapping()
			{
			};
			var json = TestElasticClient.Serialize(rootMapping);

			var mapping = TestElasticClient.Deserialize<RootObjectMapping>(json);
			TestElasticClient.Serialize(mapping).JsonEquals(json);
		}
		
	}
}
