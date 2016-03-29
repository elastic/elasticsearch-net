using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TranslogWriteMode
	{
		/// <summary>
		/// (default) Translog writes first go to a 64kB buffer in memory, and are only written 
		/// to the disk when the buffer is full, or when an fsync is triggered by a 
		/// write request or the sync_interval.
		/// </summary>
		[EnumMember(Value = "buffered")]
		Buffered,

		/// <summary>
		/// Translog writes are written to the file system immediately, without buffering. However, 
		/// these writes will only be persisted to disk when an fsync and commit is triggered by a write request
		/// or the sync_interval.
		/// </summary>
		[EnumMember(Value = "simple")]
		Simple
	}
}