using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Datafeeds might be required to search over long time periods, for several months or years.
	/// This search is split into time chunks in order to ensure the load on Elasticsearch is managed.
	/// Chunking configuration controls how the size of these time chunks are calculated.
	/// </summary>
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ChunkingConfig>))]
	public interface IChunkingConfig
	{
		/// <summary>
		/// The chunking mode
		/// </summary>
		[JsonProperty("mode")]
		ChunkingMode? Mode { get; set; }

		/// <summary>
		/// The time span that each search will be querying.
		/// This setting is only applicable when <see cref="Mode"/> is set to <see cref="ChunkingMode.Manual"/>.
		/// </summary>
		[JsonProperty("time_span")]
		Time TimeSpan { get; set; }
	}

	/// <inheritdoc />
	public class ChunkingConfig : IChunkingConfig
	{
		/// <inheritdoc />
		public ChunkingMode? Mode { get; set; }

		/// <inheritdoc />
		public Time TimeSpan { get; set; }
	}

	/// <inheritdoc />
	public class ChunkingConfigDescriptor : DescriptorBase<ChunkingConfigDescriptor, IChunkingConfig>, IChunkingConfig
	{
		ChunkingMode? IChunkingConfig.Mode { get; set; }
		Time IChunkingConfig.TimeSpan { get; set; }

		/// <inheritdoc />
		public ChunkingConfigDescriptor Mode(ChunkingMode? mode) => Assign(a => a.Mode = mode);

		/// <inheritdoc />
		public ChunkingConfigDescriptor TimeSpan(Time timeSpan) => Assign(a => a.TimeSpan = timeSpan);
	}


	/// <summary>
	/// The chunking mode
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ChunkingMode
	{
		/// <summary>
		/// The chunk size will be dynamically calculated. This is the default and recommended value.
		/// </summary>
		[EnumMember(Value = "auto")]
		Auto,

		/// <summary>
		/// Chunking will be applied according to the specified time span.
		/// </summary>
		[EnumMember(Value = "manual")]
		Manual,

		/// <summary>
		/// No chunking will be applied.
		/// </summary>
		[EnumMember(Value = "off")]
		Off
	}
}
