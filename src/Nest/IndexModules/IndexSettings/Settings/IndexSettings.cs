using System;
using System.Collections.Generic;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexSettingsConverter))]
	public interface IIndexSettings : IDynamicIndexSettings
	{
		/// <summary>
		/// The store module allows you to control how index data is stored and accessed on disk.
		/// <para>EXPERT MODE toggle</para>
		/// </summary>
		FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <summary>
		/// The number of primary shards that an index should have. Defaults to 5.
		/// This setting can only be set at index creation time. It cannot be changed on a closed index.
		/// </summary>
		int? NumberOfShards { get; set; }

		/// <summary>
		/// Settings associated with queries.
		/// </summary>
		IQueriesSettings Queries { get; set; }


		//TODO remove pre note with 6.0
		/// <summary>
		/// By defaulting, routing resolves to a single shard. Use this settings to have it resolve to a set of shards instead.
		/// This mitigates creating hotspots and very large shards if you have a few routing keys generating the significant data.
		/// <pre>Added in Elasticsearch 5.3.0</pre>
		/// </summary>
		int? RoutingPartitionSize { get; set; }
	}

	/// <inheritdoc />
	public class IndexSettings : DynamicIndexSettings, IIndexSettings
	{
		public IndexSettings() { }

		public IndexSettings(IDictionary<string, object> container) : base(container) { }

		/// <inheritdoc />
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <inheritdoc />
		public int? NumberOfShards { get; set; }

		/// <inheritdoc />
		public IQueriesSettings Queries { get; set; }

		/// <inheritdoc />
		public int? RoutingPartitionSize { get; set; }
	}

	/// <inheritdoc />
	public class IndexSettingsDescriptor : DynamicIndexSettingsDescriptorBase<IndexSettingsDescriptor, IndexSettings>
	{
		public IndexSettingsDescriptor() : base(new IndexSettings()) { }

		/// <inheritdoc />
		public IndexSettingsDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(a => a.NumberOfShards = numberOfShards);

		/// <inheritdoc />
		public IndexSettingsDescriptor RoutingPartitionSize(int? routingPartitionSize) =>
			Assign(a => a.RoutingPartitionSize = routingPartitionSize);

		/// <inheritdoc />
		public IndexSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(a => a.FileSystemStorageImplementation = fs);

		public IndexSettingsDescriptor Queries(Func<QueriesSettingsDescriptor, IQueriesSettings> selector) =>
			Assign(a => a.Queries = selector?.Invoke(new QueriesSettingsDescriptor()));
	}
}
