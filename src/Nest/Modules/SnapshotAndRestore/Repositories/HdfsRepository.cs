// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot repository that stores snapshot data within a Hadoop HDFS filesystem
	/// <para />
	/// Requires the repository-hdfs plugin to be installed on the cluster
	/// </summary>
	public interface IHdfsRepository : IRepository<IHdfsRepositorySettings> { }

	/// <inheritdoc />
	public class HdfsRepository : IHdfsRepository
	{
		public HdfsRepository(HdfsRepositorySettings settings) => Settings = settings;

		public IHdfsRepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "hdfs";
	}

	/// <summary>
	/// Snapshot repository settings for <see cref="IHdfsRepository"/>
	/// </summary>
	public interface IHdfsRepositorySettings : IRepositorySettings
	{
		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Disabled by default
		/// </summary>
		[DataMember(Name ="chunk_size")]
		string ChunkSize { get; set; }

		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't
		/// affect index files that are already compressed by default. Defaults to <c>false</c>.
		/// </summary>
		[DataMember(Name ="compress")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Compress { get; set; }

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		[DataMember(Name ="concurrent_streams")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? ConcurrentStreams { get; set; }

		/// <summary>
		/// Hadoop configuration XML to be loaded (use commas for multi values)
		/// </summary>
		[DataMember(Name ="conf_location")]
		string ConfigurationLocation { get; set; }

		/// <summary>
		/// 'inlined' key=value added to the Hadoop configuration
		/// </summary>
		[IgnoreDataMember]
		Dictionary<string, object> InlineHadoopConfiguration { get; set; }

		/// <summary>
		/// Whether to load the default Hadoop configuration (default) or not
		/// </summary>
		[DataMember(Name ="load_defaults")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? LoadDefaults { get; set; }

		/// <summary>
		/// The path with the file-system where data is stored/loaded. Required
		/// </summary>
		[DataMember(Name ="path")]
		string Path { get; set; }

		/// <summary>
		/// The Hadoop file-system URI. Optional
		/// </summary>
		[DataMember(Name ="uri")]
		string Uri { get; set; }
	}

	/// <inheritdoc />
	public class HdfsRepositorySettings : IHdfsRepositorySettings
	{
		internal HdfsRepositorySettings() { }

		public HdfsRepositorySettings(string path) => Path = path;

		/// <inheritdoc />
		public string ChunkSize { get; set; }
		/// <inheritdoc />
		public bool? Compress { get; set; }
		/// <inheritdoc />
		public int? ConcurrentStreams { get; set; }
		/// <inheritdoc />
		public string ConfigurationLocation { get; set; }
		/// <inheritdoc />
		public Dictionary<string, object> InlineHadoopConfiguration { get; set; }
		/// <inheritdoc />
		public bool? LoadDefaults { get; set; }
		/// <inheritdoc />
		public string Path { get; set; }
		/// <inheritdoc />
		public string Uri { get; set; }
	}

	/// <inheritdoc cref="IHdfsRepositorySettings"/>
	public class HdfsRepositorySettingsDescriptor
		: DescriptorBase<HdfsRepositorySettingsDescriptor, IHdfsRepositorySettings>, IHdfsRepositorySettings
	{
		string IHdfsRepositorySettings.ChunkSize { get; set; }
		bool? IHdfsRepositorySettings.Compress { get; set; }
		int? IHdfsRepositorySettings.ConcurrentStreams { get; set; }
		string IHdfsRepositorySettings.ConfigurationLocation { get; set; }
		Dictionary<string, object> IHdfsRepositorySettings.InlineHadoopConfiguration { get; set; }
		bool? IHdfsRepositorySettings.LoadDefaults { get; set; }
		string IHdfsRepositorySettings.Path { get; set; }
		string IHdfsRepositorySettings.Uri { get; set; }

		/// <inheritdoc cref="IHdfsRepositorySettings.Uri"/>
		public HdfsRepositorySettingsDescriptor Uri(string uri) => Assign(uri, (a, v) => a.Uri = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.Path"/>
		public HdfsRepositorySettingsDescriptor Path(string path) => Assign(path, (a, v) => a.Path = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.LoadDefaults"/>
		public HdfsRepositorySettingsDescriptor LoadDefaults(bool? loadDefaults = true) => Assign(loadDefaults, (a, v) => a.LoadDefaults = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.ConfigurationLocation"/>
		public HdfsRepositorySettingsDescriptor ConfigurationLocation(string configurationLocation) =>
			Assign(configurationLocation, (a, v) => a.ConfigurationLocation = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.InlineHadoopConfiguration"/>
		public HdfsRepositorySettingsDescriptor InlinedHadoopConfiguration(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> inlineConfig
		) => Assign(inlineConfig, (a, v) => a.InlineHadoopConfiguration = v(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IHdfsRepositorySettings.Compress"/>
		public HdfsRepositorySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.ConcurrentStreams"/>
		public HdfsRepositorySettingsDescriptor ConcurrentStreams(int? concurrentStreams) => Assign(concurrentStreams, (a, v) => a.ConcurrentStreams = v);

		/// <inheritdoc cref="IHdfsRepositorySettings.ChunkSize"/>
		public HdfsRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(chunkSize, (a, v) => a.ChunkSize = v);
	}

	/// <inheritdoc cref="IHdfsRepository"/>
	public class HdfsRepositoryDescriptor
		: DescriptorBase<HdfsRepositoryDescriptor, IHdfsRepository>, IHdfsRepository
	{
		IHdfsRepositorySettings IRepository<IHdfsRepositorySettings>.Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;
		string ISnapshotRepository.Type => "hdfs";

		/// <inheritdoc cref="IHdfsRepositorySettings"/>
		public HdfsRepositoryDescriptor Settings(string path, Func<HdfsRepositorySettingsDescriptor, IHdfsRepositorySettings> settingsSelector = null
		) =>
			Assign(settingsSelector.InvokeOrDefault(new HdfsRepositorySettingsDescriptor().Path(path)), (a, v) => a.Settings = v);
	}
}
