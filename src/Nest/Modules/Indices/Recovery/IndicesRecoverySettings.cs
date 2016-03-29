namespace Nest
{
	public interface IIndicesRecoverySettings
	{
		/// <summary> defaults to 3</summary>
		int? ConcurrentStreams { get; set; }
		/// <summary> defaults to 2</summary>
		int? ConcurrentSmallFileStreams { get; set; }
		/// <summary> defaults to 512kb</summary>
		string FileChunkSize { get; set; }
		/// <summary> defaults to 1000</summary>
		int? TranslogOperations { get; set; }
		/// <summary> defaults to 512kb</summary>
		string TranslogSize { get; set; }
		/// <summary> defaults to true</summary>
		bool? Compress { get; set; }
		/// <summary> defaults to 40mb</summary>
		string MaxBytesPerSecond { get; set; }
	}

	public class IndicesRecoverySettings : IIndicesRecoverySettings
	{
		/// <inheritdoc/>
		public int? ConcurrentStreams { get; set; }
		/// <inheritdoc/>
		public int? ConcurrentSmallFileStreams { get; set; }
		/// <inheritdoc/>
		public string FileChunkSize { get; set; }
		/// <inheritdoc/>
		public int? TranslogOperations { get; set; }
		/// <inheritdoc/>
		public string TranslogSize { get; set; }
		/// <inheritdoc/>
		public bool? Compress { get; set; }
		/// <inheritdoc/>
		public string MaxBytesPerSecond { get; set; }
	}

	public class IndicesRecoverySettingsDescriptor 
		: DescriptorBase<IndicesRecoverySettingsDescriptor, IIndicesRecoverySettings>, IIndicesRecoverySettings
	{
		int? IIndicesRecoverySettings.ConcurrentStreams { get; set; }
		int? IIndicesRecoverySettings.ConcurrentSmallFileStreams { get; set; }
		string IIndicesRecoverySettings.FileChunkSize { get; set; }
		int? IIndicesRecoverySettings.TranslogOperations { get; set; }
		string IIndicesRecoverySettings.TranslogSize { get; set; }
		bool? IIndicesRecoverySettings.Compress { get; set; }
		string IIndicesRecoverySettings.MaxBytesPerSecond { get; set; }

		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor ConcurrentStreams(int? streams) => Assign(a => a.ConcurrentStreams = streams);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor ConcurrentSmallFileStreams(int? streams)=> Assign(a => a.ConcurrentSmallFileStreams = streams);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor FileChunkSize(string size) => Assign(a => a.FileChunkSize = size);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor TranslogOperations(int? ops) => Assign(a => a.TranslogOperations = ops);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor TranslogSize(string size) => Assign(a => a.TranslogSize = size);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor Compress(bool? compress) => Assign(a => a.Compress = compress);
		/// <inheritdoc/>
		public IndicesRecoverySettingsDescriptor MaxBytesPerSecond(string max) => Assign(a => a.MaxBytesPerSecond = max);
	}
}