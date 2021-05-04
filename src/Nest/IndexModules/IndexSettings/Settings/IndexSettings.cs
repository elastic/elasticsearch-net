// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The settings for an index
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(IndexSettingsFormatter))]
	public interface IIndexSettings : IDynamicIndexSettings
	{
		/// <summary>
		/// The store module allows you to control how index data is stored and accessed on disk.
		/// <para>EXPERT MODE toggle</para>
		/// </summary>
		FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }


		/// <summary>
		/// The number of routing shards. Used in conjunction with the Split Index API. If specified, must be
		/// greater than or equal to <see cref="NumberOfShards" />
		/// </summary>
		int? NumberOfRoutingShards { get; set; }

		/// <summary>
		/// The number of primary shards that an index should have. Defaults to 1.
		/// This setting can only be set at index creation time. It cannot be changed on a closed index.
		/// </summary>
		int? NumberOfShards { get; set; }

		/// <summary> Settings associated with queries. </summary>
		IQueriesSettings Queries { get; set; }

		/// <summary>
		/// By defaulting, routing resolves to a single shard. Use this settings to have it resolve to a set of shards instead.
		/// This mitigates creating hotspots and very large shards if you have a few routing keys generating the significant data.
		/// </summary>
		int? RoutingPartitionSize { get; set; }

		/// <summary>
		/// Indicates whether the index should be hidden by default.
		/// Hidden indices are not returned by default when using a wildcard expression.
		/// </summary>
		bool? Hidden { get; set; }

		/// <summary>
		///  Settings associated with index sorting.
		/// https://www.elastic.co/guide/en/elasticsearch/reference/6.0/index-modules-index-sorting.html
		/// </summary>
		ISortingSettings Sorting { get; set; }

		/// <summary> Soft delete settings for the index </summary>
		ISoftDeleteSettings SoftDeletes { get; set; }
	}

	/// <inheritdoc cref="IIndexSettings" />
	public class IndexSettings : DynamicIndexSettings, IIndexSettings
	{
		public IndexSettings() { }

		public IndexSettings(IDictionary<string, object> container) : base(container) { }

		/// <inheritdoc cref="IIndexSettings.FileSystemStorageImplementation" />
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		/// <inheritdoc cref="IIndexSettings.NumberOfRoutingShards" />
		public int? NumberOfRoutingShards { get; set; }

		/// <inheritdoc cref="IIndexSettings.NumberOfShards" />
		public int? NumberOfShards { get; set; }

		/// <inheritdoc cref="IIndexSettings.Queries" />
		public IQueriesSettings Queries { get; set; }

		/// <inheritdoc cref="IIndexSettings.RoutingPartitionSize" />
		public int? RoutingPartitionSize { get; set; }

		/// <inheritdoc cref="IIndexSettings.Hidden" />
		public bool? Hidden { get; set; }

		/// <inheritdoc cref="IIndexSettings.Sorting" />
		public ISortingSettings Sorting { get; set; }

		/// <inheritdoc />
		public ISoftDeleteSettings SoftDeletes { get; set; }
	}

	/// <inheritdoc cref="IIndexSettings" />
	public class IndexSettingsDescriptor : DynamicIndexSettingsDescriptorBase<IndexSettingsDescriptor, IndexSettings>
	{
		public IndexSettingsDescriptor() : base(new IndexSettings()) { }

		/// <inheritdoc cref="IIndexSettings.NumberOfShards" />
		public IndexSettingsDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(numberOfShards, (a, v) => a.NumberOfShards = v);

		/// <inheritdoc cref="IIndexSettings.NumberOfRoutingShards" />
		public IndexSettingsDescriptor NumberOfRoutingShards(int? numberOfRoutingShards) =>
			Assign(numberOfRoutingShards, (a, v) => a.NumberOfRoutingShards = v);

		/// <inheritdoc cref="IIndexSettings.RoutingPartitionSize" />
		public IndexSettingsDescriptor RoutingPartitionSize(int? routingPartitionSize) =>
			Assign(routingPartitionSize, (a, v) => a.RoutingPartitionSize = v);

		/// <inheritdoc cref="IIndexSettings.Hidden" />
		public IndexSettingsDescriptor Hidden(bool? hidden = true) =>
			Assign(hidden, (a, v) => a.Hidden = v);

		/// <inheritdoc cref="IIndexSettings.FileSystemStorageImplementation" />
		public IndexSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(fs, (a, v) => a.FileSystemStorageImplementation = v);

		/// <inheritdoc cref="IIndexSettings.Queries" />
		public IndexSettingsDescriptor Queries(Func<QueriesSettingsDescriptor, IQueriesSettings> selector) =>
			Assign(selector, (a, v) => a.Queries = v?.Invoke(new QueriesSettingsDescriptor()));

		/// <inheritdoc cref="IIndexSettings.Sorting" />
		public IndexSettingsDescriptor Sorting<T>(Func<SortingSettingsDescriptor<T>, ISortingSettings> selector) where T : class =>
			Assign(selector, (a, v) => a.Sorting = v?.Invoke(new SortingSettingsDescriptor<T>()));

		/// <inheritdoc cref="IIndexSettings.SoftDeletes" />
		public IndexSettingsDescriptor SoftDeletes(Func<SoftDeleteSettingsDescriptor, ISoftDeleteSettings> selector) =>
			Assign(selector, (a, v) => a.SoftDeletes = v?.Invoke(new SoftDeleteSettingsDescriptor()));
	}
}
