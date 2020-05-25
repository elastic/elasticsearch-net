// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot repository that uses a shared file system to store snapshot data.
	/// The path specified in the location parameter should point to the same location in the shared
	/// filesystem and be accessible on all data and master nodes.
	/// </summary>
	public interface IFileSystemRepository : IRepository<IFileSystemRepositorySettings> { }

	/// <inheritdoc />
	public class FileSystemRepository : IFileSystemRepository
	{
		public FileSystemRepository(FileSystemRepositorySettings settings) => Settings = settings;

		public IFileSystemRepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "fs";
	}

	/// <summary>
	/// Repository settings for <see cref="IFileSystemRepository"/>
	/// </summary>
	public interface IFileSystemRepositorySettings : IRepositorySettings
	{
		/// <summary>
		/// Big files can be broken down into chunks during the snapshot process, if needed.
		/// The chunk size can be specified in bytes or by using size value notation, i.e. 1g, 10m, 5k.
		/// Defaults to null (unlimited chunk size).
		/// </summary>
		[DataMember(Name ="chunk_size")]
		string ChunkSize { get; set; }

		/// <summary>
		/// Turns on compression of the snapshot files. Defaults to true.
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
		/// Location of the snapshots. Mandatory.
		/// </summary>
		[DataMember(Name ="location")]
		string Location { get; set; }

		/// <summary>
		/// Throttles per node restore rate. Defaults to 20mb per second.
		/// </summary>
		[DataMember(Name ="max_restore_bytes_per_second")]
		string RestoreBytesPerSecondMaximum { get; set; }

		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 20mb per second.
		/// </summary>
		[DataMember(Name ="max_snapshot_bytes_per_second")]
		string SnapshotBytesPerSecondMaximum { get; set; }
	}

	/// <inheritdoc cref="IFileSystemRepositorySettings"/>
	public class FileSystemRepositorySettings : IFileSystemRepositorySettings
	{
		internal FileSystemRepositorySettings() { }

		public FileSystemRepositorySettings(string location) => Location = location;

		public string ChunkSize { get; set; }

		public bool? Compress { get; set; }

		public int? ConcurrentStreams { get; set; }

		public string Location { get; set; }

		public string RestoreBytesPerSecondMaximum { get; set; }

		public string SnapshotBytesPerSecondMaximum { get; set; }
	}

	/// <inheritdoc cref="IFileSystemRepositorySettings"/>
	public class FileSystemRepositorySettingsDescriptor
		: DescriptorBase<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings>, IFileSystemRepositorySettings
	{
		string IFileSystemRepositorySettings.ChunkSize { get; set; }
		bool? IFileSystemRepositorySettings.Compress { get; set; }
		int? IFileSystemRepositorySettings.ConcurrentStreams { get; set; }
		string IFileSystemRepositorySettings.Location { get; set; }
		string IFileSystemRepositorySettings.RestoreBytesPerSecondMaximum { get; set; }
		string IFileSystemRepositorySettings.SnapshotBytesPerSecondMaximum { get; set; }

		/// <inheritdoc cref="IFileSystemRepositorySettings.Location"/>
		public FileSystemRepositorySettingsDescriptor Location(string location) => Assign(location, (a, v) => a.Location = v);

		/// <inheritdoc cref="IFileSystemRepositorySettings.Compress"/>
		public FileSystemRepositorySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <inheritdoc cref="IFileSystemRepositorySettings.ConcurrentStreams"/>
		public FileSystemRepositorySettingsDescriptor ConcurrentStreams(int? concurrentStreams) =>
			Assign(concurrentStreams, (a, v) => a.ConcurrentStreams = v);

		/// <inheritdoc cref="IFileSystemRepositorySettings.ChunkSize"/>
		public FileSystemRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(chunkSize, (a, v) => a.ChunkSize = v);

		/// <inheritdoc cref="IFileSystemRepositorySettings.RestoreBytesPerSecondMaximum"/>
		public FileSystemRepositorySettingsDescriptor RestoreBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(maximumBytesPerSecond, (a, v) => a.RestoreBytesPerSecondMaximum = v);

		/// <inheritdoc cref="IFileSystemRepositorySettings.SnapshotBytesPerSecondMaximum"/>
		public FileSystemRepositorySettingsDescriptor SnapshotBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(maximumBytesPerSecond, (a, v) => a.SnapshotBytesPerSecondMaximum = v);
	}

	/// <inheritdoc cref="IFileSystemRepository"/>
	public class FileSystemRepositoryDescriptor
		: DescriptorBase<FileSystemRepositoryDescriptor, IFileSystemRepository>, IFileSystemRepository
	{
		IFileSystemRepositorySettings IRepository<IFileSystemRepositorySettings>.Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;
		string ISnapshotRepository.Type { get; } = "fs";

		/// <inheritdoc cref="IFileSystemRepositorySettings"/>
		public FileSystemRepositoryDescriptor Settings(string location,
			Func<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings> settingsSelector = null
		) =>
			Assign(settingsSelector.InvokeOrDefault(new FileSystemRepositorySettingsDescriptor().Location(location)), (a, v) => a.Settings = v);
	}
}
