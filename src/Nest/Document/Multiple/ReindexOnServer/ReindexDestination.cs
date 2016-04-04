using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IReindexDestination
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("routing")]
		ReindexRouting Routing { get; set; }

		[JsonProperty("op_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		OpType? OpType { get; set; }

		[JsonProperty("version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }
	}

	public class ReindexDestination : IReindexDestination
	{
		public IndexName Index { get; set; }

		public TypeName Type { get; set; }


		public ReindexRouting Routing { get; set; }

		public OpType? OpType { get; set; }

		public VersionType? VersionType { get; set; }
	}

	public class ReindexDestinationDescriptor : DescriptorBase<ReindexDestinationDescriptor, IReindexDestination>, IReindexDestination
	{
		IndexName IReindexDestination.Index { get; set; }
		TypeName IReindexDestination.Type { get; set; }
		ReindexRouting IReindexDestination.Routing { get; set; }
		OpType? IReindexDestination.OpType { get; set; }
		VersionType? IReindexDestination.VersionType { get; set; }

		public ReindexDestinationDescriptor Routing(ReindexRouting routing) => Assign(a => a.Routing = routing);

		public ReindexDestinationDescriptor OpType(OpType opType) => Assign(a => a.OpType = opType);

		public ReindexDestinationDescriptor VersionType(VersionType versionType) => Assign(a => a.VersionType = versionType);

		public ReindexDestinationDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public ReindexDestinationDescriptor Type(TypeName type) => Assign(a => a.Type = type);

	}
}
