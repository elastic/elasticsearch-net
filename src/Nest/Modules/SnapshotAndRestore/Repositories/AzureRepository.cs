// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot repository that stores snapshots in an Azure storage account
	/// <para />
	/// Requires the repository-azure plugin to be installed on the cluster
	/// </summary>
	public interface IAzureRepository : IRepository<IAzureRepositorySettings> { }

	/// <inheritdoc />
	public class AzureRepository : IAzureRepository
	{
		public AzureRepository() { }

		public AzureRepository(IAzureRepositorySettings settings) => Settings = settings;

		public IAzureRepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "azure";
	}

	/// <summary>
	/// Snapshot repository settings for <see cref="IAzureRepository"/>
	/// </summary>
	public interface IAzureRepositorySettings : IRepositorySettings
	{
		/// <summary>
		/// The path within the container on which to store the snapshot data.
		/// </summary>
		[DataMember(Name ="base_path")]
		string BasePath { get; set; }

		/// <summary>
		///  Big files can be broken down into chunks during the snapshot process, if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Defaults to 64m (64m max)
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
		/// Tha name of the container
		/// </summary>
		[DataMember(Name ="container")]
		string Container { get; set; }
	}

	/// <inheritdoc />
	public class AzureRepositorySettings : IAzureRepositorySettings
	{
		/// <inheritdoc />
		[DataMember(Name ="base_path")]
		public string BasePath { get; set; }

		/// <inheritdoc />
		[DataMember(Name ="chunk_size")]
		public string ChunkSize { get; set; }

		/// <inheritdoc />
		[DataMember(Name ="compress")]
		public bool? Compress { get; set; }

		/// <inheritdoc />
		[DataMember(Name ="container")]
		public string Container { get; set; }
	}

	public class AzureRepositorySettingsDescriptor
		: DescriptorBase<AzureRepositorySettingsDescriptor, IAzureRepositorySettings>, IAzureRepositorySettings
	{
		string IAzureRepositorySettings.BasePath { get; set; }
		string IAzureRepositorySettings.ChunkSize { get; set; }
		bool? IAzureRepositorySettings.Compress { get; set; }
		string IAzureRepositorySettings.Container { get; set; }

		/// <inheritdoc cref="IAzureRepositorySettings.Container"/>
		public AzureRepositorySettingsDescriptor Container(string container) => Assign(container, (a, v) => a.Container = v);

		/// <inheritdoc cref="IAzureRepositorySettings.BasePath"/>
		public AzureRepositorySettingsDescriptor BasePath(string basePath) => Assign(basePath, (a, v) => a.BasePath = v);

		/// <inheritdoc cref="IAzureRepositorySettings.Compress"/>
		public AzureRepositorySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <inheritdoc cref="IAzureRepositorySettings.ChunkSize"/>
		public AzureRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(chunkSize, (a, v) => a.ChunkSize = v);
	}

	/// <inheritdoc cref="IAzureRepository"/>
	public class AzureRepositoryDescriptor
		: DescriptorBase<AzureRepositoryDescriptor, IAzureRepository>, IAzureRepository
	{
		IAzureRepositorySettings IRepository<IAzureRepositorySettings>.Settings { get; set; }
		string ISnapshotRepository.Type { get; } = "azure";
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;

		/// <inheritdoc cref="IAzureRepositorySettings"/>
		public AzureRepositoryDescriptor Settings(Func<AzureRepositorySettingsDescriptor, IAzureRepositorySettings> settingsSelector) =>
			Assign(settingsSelector, (a, v) => a.Settings = v?.Invoke(new AzureRepositorySettingsDescriptor()));
	}
}
