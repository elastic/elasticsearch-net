using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// The settings for an index
	/// </summary>
	[ContractJsonConverter(typeof(IndexSettingsConverter))]
	public interface IIndexSettings : IDynamicIndexSettings
	{
		/// <summary>
		/// The number of primary shards that an index should have. Defaults to 5.
		/// This setting can only be set at index creation time. It cannot be changed on a closed index.
		/// </summary>
		int? NumberOfShards { get; set; }


		/// <summary>
		/// The number of routing shards. Used in conjunction with the Split Index API. If specified, must be
		/// greater than or equal to <see cref="NumberOfShards"/>
		/// </summary>
		int? NumberOfRoutingShards { get; set; }

		/// <summary>
		/// By defaulting, routing resolves to a single shard. Use this settings to have it resolve to a set of shards instead.
		/// This mitigates creating hotspots and very large shards if you have a few routing keys generating the significant data.
		/// </summary>
		int? RoutingPartitionSize { get; set; }

		/// <summary>
		/// The store module allows you to control how index data is stored and accessed on disk.
		/// <para>EXPERT MODE toggle</para>
		/// </summary>
		FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <summary>
		/// Settings associated with queries.
		/// </summary>
		IQueriesSettings Queries { get; set; }

		/// <summary>
		///  Settings associated with index sorting.
		/// https://www.elastic.co/guide/en/elasticsearch/reference/6.0/index-modules-index-sorting.html
		/// </summary>
		ISortingSettings Sorting { get; set; }
	}

	/// <inheritdoc cref="IIndexSettings"/>
	public class IndexSettings: DynamicIndexSettings, IIndexSettings
	{
		public IndexSettings() { }
		public IndexSettings(IDictionary<string, object> container) : base(container) { }

		/// <inheritdoc />
		public int? NumberOfShards { get; set; }

		/// <inheritdoc />
		public int? NumberOfRoutingShards { get; set; }

		/// <inheritdoc />
		public int? RoutingPartitionSize { get; set; }

		/// <inheritdoc />
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <inheritdoc />
		public IQueriesSettings Queries { get; set; }

		/// <inheritdoc />
		public ISortingSettings Sorting { get; set; }
	}

	/// <inheritdoc cref="IIndexSettings"/>
	public class IndexSettingsDescriptor: DynamicIndexSettingsDescriptorBase<IndexSettingsDescriptor, IndexSettings>
	{
		public IndexSettingsDescriptor() : base(new IndexSettings()) { }

		/// <inheritdoc cref="IIndexSettings.NumberOfShards"/>
		public IndexSettingsDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(a => a.NumberOfShards = numberOfShards);

		/// <inheritdoc cref="IIndexSettings.NumberOfRoutingShards"/>
		public IndexSettingsDescriptor NumberOfRoutingShards(int? numberOfRoutingShards) =>
			Assign(a => a.NumberOfRoutingShards = numberOfRoutingShards);

		/// <inheritdoc cref="IIndexSettings.RoutingPartitionSize"/>
		public IndexSettingsDescriptor RoutingPartitionSize(int? routingPartitionSize) =>
			Assign(a => a.RoutingPartitionSize = routingPartitionSize);

		/// <inheritdoc cref="IIndexSettings.FileSystemStorageImplementation"/>
		public IndexSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(a => a.FileSystemStorageImplementation = fs);

		/// <inheritdoc cref="IIndexSettings.Queries"/>
		public IndexSettingsDescriptor Queries(Func<QueriesSettingsDescriptor, IQueriesSettings> selector) =>
			Assign(a => a.Queries = selector?.Invoke(new QueriesSettingsDescriptor()));

		/// <inheritdoc cref="IIndexSettings.Sorting"/>
		public IndexSettingsDescriptor Sorting<T>(Func<SortingSettingsDescriptor<T>, ISortingSettings> selector) where T : class =>
			Assign(a => a.Sorting = selector?.Invoke(new SortingSettingsDescriptor<T>()));
	}
}
