// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net;
using System.Runtime.Serialization;


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
		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		/// <summary>
		/// Setting to <see cref="Elasticsearch.Net.OpType.Create" /> will cause reindex to only
		/// create missing documents in the destination index.
		/// </summary>
		[DataMember(Name ="op_type")]

		OpType? OpType { get; set; }

		/// <summary>
		/// Id of the pipeline to use to process documents
		/// </summary>
		string Pipeline { get; set; }

		/// <summary>
		/// The routing to use when reindexing
		/// </summary>
		[DataMember(Name ="routing")]
		ReindexRouting Routing { get; set; }

		/// <summary>
		/// Setting to <see cref="Elasticsearch.Net.VersionType.External" /> will cause Elasticsearch
		/// to preserve the version from the source, create any documents that are missing,
		/// and update any documents that have an older version in the destination index
		/// than they do in the source index
		/// </summary>
		[DataMember(Name ="version_type")]

		VersionType? VersionType { get; set; }
	}

	/// <inheritdoc />
	public class ReindexDestination : IReindexDestination
	{
		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public OpType? OpType { get; set; }

		/// <inheritdoc />
		public string Pipeline { get; set; }

		/// <inheritdoc />
		public ReindexRouting Routing { get; set; }

		/// <inheritdoc />
		public VersionType? VersionType { get; set; }
	}

	/// <inheritdoc cref="IReindexDestination" />
	public class ReindexDestinationDescriptor : DescriptorBase<ReindexDestinationDescriptor, IReindexDestination>, IReindexDestination
	{
		IndexName IReindexDestination.Index { get; set; }
		OpType? IReindexDestination.OpType { get; set; }
		string IReindexDestination.Pipeline { get; set; }
		ReindexRouting IReindexDestination.Routing { get; set; }
		VersionType? IReindexDestination.VersionType { get; set; }

		/// <inheritdoc cref="IReindexDestination.Routing" />
		public ReindexDestinationDescriptor Routing(ReindexRouting routing) => Assign(routing, (a, v) => a.Routing = v);

		/// <inheritdoc cref="IReindexDestination.Pipeline" />
		public ReindexDestinationDescriptor Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);

		/// <inheritdoc cref="IReindexDestination.OpType" />
		public ReindexDestinationDescriptor OpType(OpType? opType) => Assign(opType, (a, v) => a.OpType = v);

		/// <inheritdoc cref="IReindexDestination.VersionType" />
		public ReindexDestinationDescriptor VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a.VersionType = v);

		/// <inheritdoc cref="IReindexDestination.Index" />
		public ReindexDestinationDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

	}
}
