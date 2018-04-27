using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Configures the destination for a reindex API request
	/// </summary>
	public interface IReindexDestination
	{
		/// <summary>
		/// The index to reindex into
		/// </summary>
		[JsonProperty("index")]
		IndexName Index { get; set; }

		/// <summary>
		/// The type to reindex into
		/// </summary>
		[JsonProperty("type")]
		TypeName Type { get; set; }

		/// <summary>
		/// The routing to use when reindexing
		/// </summary>
		[JsonProperty("routing")]
		ReindexRouting Routing { get; set; }

		/// <summary>
		/// Setting to <see cref="Elasticsearch.Net.OpType.Create"/> will cause reindex to only
		/// create missing documents in the destination index.
		/// </summary>
		[JsonProperty("op_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		OpType? OpType { get; set; }

		/// <summary>
		/// Setting to <see cref="Elasticsearch.Net.VersionType.External"/> will cause Elasticsearch
		/// to preserve the version from the source, create any documents that are missing,
		/// and update any documents that have an older version in the destination index
		/// than they do in the source index
		/// </summary>
		[JsonProperty("version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }
	}

	/// <inheritdoc />
	public class ReindexDestination : IReindexDestination
	{
		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public TypeName Type { get; set; }

		/// <inheritdoc />
		public ReindexRouting Routing { get; set; }

		/// <inheritdoc />
		public OpType? OpType { get; set; }

		/// <inheritdoc />
		public VersionType? VersionType { get; set; }
	}

	/// <inheritdoc cref="IReindexDestination"/>
	public class ReindexDestinationDescriptor : DescriptorBase<ReindexDestinationDescriptor, IReindexDestination>, IReindexDestination
	{
		IndexName IReindexDestination.Index { get; set; }
		TypeName IReindexDestination.Type { get; set; }
		ReindexRouting IReindexDestination.Routing { get; set; }
		OpType? IReindexDestination.OpType { get; set; }
		VersionType? IReindexDestination.VersionType { get; set; }

		/// <inheritdoc cref="IReindexDestination.Routing"/>
		public ReindexDestinationDescriptor Routing(ReindexRouting routing) => Assign(a => a.Routing = routing);

		/// <inheritdoc cref="IReindexDestination.OpType"/>
		public ReindexDestinationDescriptor OpType(OpType? opType) => Assign(a => a.OpType = opType);

		/// <inheritdoc cref="IReindexDestination.VersionType"/>
		public ReindexDestinationDescriptor VersionType(VersionType? versionType) => Assign(a => a.VersionType = versionType);

		/// <inheritdoc cref="IReindexDestination.Index"/>
		public ReindexDestinationDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		/// <inheritdoc cref="IReindexDestination.Type"/>
		public ReindexDestinationDescriptor Type(TypeName type) => Assign(a => a.Type = type);

	}
}
