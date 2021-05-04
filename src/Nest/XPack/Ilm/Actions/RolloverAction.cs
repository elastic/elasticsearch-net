// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The Rollover Action rolls an alias over to a new index when the existing index meets one of the rollover conditions.
	/// </summary>
	/// <remarks>
	/// Phases allowed: hot.
	/// </remarks>
	public interface IRolloverLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// Max time elapsed from index creation.
		/// </summary>
		[DataMember(Name = "max_age")]
		Time MaximumAge { get; set; }

		/// <summary>
		/// Max number of documents an index is to contain before rolling over.
		/// </summary>
		[DataMember(Name = "max_docs")]
		long? MaximumDocuments { get; set; }

		/// <summary>
		/// Max primary shard index storage size using byte notation (e.g. $0gb, 100mb...)
		/// </summary>
		[DataMember(Name = "max_size")]
		string MaximumSize { get; set; }
	}

	public class RolloverLifecycleAction : IRolloverLifecycleAction
	{
		/// <inheritdoc />
		public Time MaximumAge { get; set; }

		/// <inheritdoc />
		public long? MaximumDocuments { get; set; }

		/// <inheritdoc />
		public string MaximumSize { get; set; }
	}

	public class RolloverLifecycleActionDescriptor
		: DescriptorBase<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction>, IRolloverLifecycleAction
	{
		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumAge" />
		Time IRolloverLifecycleAction.MaximumAge { get; set; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumDocuments" />
		long? IRolloverLifecycleAction.MaximumDocuments { get; set; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSize" />
		string IRolloverLifecycleAction.MaximumSize { get; set; }

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumSize" />
		public RolloverLifecycleActionDescriptor MaximumSize(string maximumSize) => Assign(maximumSize, (a, v) => a.MaximumSize = maximumSize);

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumAge" />
		public RolloverLifecycleActionDescriptor MaximumAge(Time maximumAge) => Assign(maximumAge, (a, v) => a.MaximumAge = maximumAge);

		/// <inheritdoc cref="IRolloverLifecycleAction.MaximumDocuments" />
		public RolloverLifecycleActionDescriptor MaximumDocuments(long? maximumDocuments)
			=> Assign(maximumDocuments, (a, v) => a.MaximumDocuments = maximumDocuments);
	}
}
