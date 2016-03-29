namespace Nest
{
	/// <summary>
	/// The indexing buffer is used to store newly indexed documents. When it fills up, the documents in the buffer are written to a segment on disk. It is divided between all shards on the node.
	/// <para>The following settings are static and must be configured on every data node in the cluster</para>
	/// </summary>
	public class IndexingBufferSettings 
	{
		/// <summary>Accepts either a percentage or a byte size value. It defaults to 10%, meaning that 10% of the total heap allocated to a node will be used as the indexing buffer size.</summary>
		public string IndexBufferSize { get; internal set; }

		/// <summary>If the index_buffer_size is specified as a percentage, then this setting can be used to specify an absolute minimum. Defaults to 48mb.</summary>
		public string IndexBufferSizeMinimum { get; internal set; }

		/// <summary>If the index_buffer_size is specified as a percentage, then this setting can be used to specify an absolute maximum. Defaults to unbounded.</summary>
		public string IndexBufferSizeMaximum { get; internal set; }

		/// <summary>Sets a hard lower limit for the memory allocated per shard for its own indexing buffer. Defaults to 4mb.</summary>
		public string ShardBufferSizeMinimum { get; internal set; }

	}
}