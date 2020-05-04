// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IIndicesRecoverySettings
	{
		/// <summary> defaults to true</summary>
		bool? Compress { get; set; }

		/// <summary> defaults to 2</summary>
		int? ConcurrentSmallFileStreams { get; set; }

		/// <summary> defaults to 3</summary>
		int? ConcurrentStreams { get; set; }

		/// <summary> defaults to 512kb</summary>
		string FileChunkSize { get; set; }

		/// <summary> defaults to 40mb</summary>
		string MaxBytesPerSecond { get; set; }

		/// <summary> defaults to 1000</summary>
		int? TranslogOperations { get; set; }

		/// <summary> defaults to 512kb</summary>
		string TranslogSize { get; set; }
	}

	public class IndicesRecoverySettings : IIndicesRecoverySettings
	{
		/// <inheritdoc />
		public bool? Compress { get; set; }

		/// <inheritdoc />
		public int? ConcurrentSmallFileStreams { get; set; }

		/// <inheritdoc />
		public int? ConcurrentStreams { get; set; }

		/// <inheritdoc />
		public string FileChunkSize { get; set; }

		/// <inheritdoc />
		public string MaxBytesPerSecond { get; set; }

		/// <inheritdoc />
		public int? TranslogOperations { get; set; }

		/// <inheritdoc />
		public string TranslogSize { get; set; }
	}

	public class IndicesRecoverySettingsDescriptor
		: DescriptorBase<IndicesRecoverySettingsDescriptor, IIndicesRecoverySettings>, IIndicesRecoverySettings
	{
		bool? IIndicesRecoverySettings.Compress { get; set; }
		int? IIndicesRecoverySettings.ConcurrentSmallFileStreams { get; set; }
		int? IIndicesRecoverySettings.ConcurrentStreams { get; set; }
		string IIndicesRecoverySettings.FileChunkSize { get; set; }
		string IIndicesRecoverySettings.MaxBytesPerSecond { get; set; }
		int? IIndicesRecoverySettings.TranslogOperations { get; set; }
		string IIndicesRecoverySettings.TranslogSize { get; set; }

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor ConcurrentStreams(int? streams) => Assign(streams, (a, v) => a.ConcurrentStreams = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor ConcurrentSmallFileStreams(int? streams) => Assign(streams, (a, v) => a.ConcurrentSmallFileStreams = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor FileChunkSize(string size) => Assign(size, (a, v) => a.FileChunkSize = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor TranslogOperations(int? ops) => Assign(ops, (a, v) => a.TranslogOperations = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor TranslogSize(string size) => Assign(size, (a, v) => a.TranslogSize = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <inheritdoc />
		public IndicesRecoverySettingsDescriptor MaxBytesPerSecond(string max) => Assign(max, (a, v) => a.MaxBytesPerSecond = v);
	}
}
