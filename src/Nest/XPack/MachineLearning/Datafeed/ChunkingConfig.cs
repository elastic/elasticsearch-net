// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Datafeeds might be required to search over long time periods, for several months or years.
	/// This search is split into time chunks in order to ensure the load on Elasticsearch is managed.
	/// Chunking configuration controls how the size of these time chunks are calculated.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(ChunkingConfig))]
	public interface IChunkingConfig
	{
		/// <summary>
		/// The chunking mode
		/// </summary>
		[DataMember(Name = "mode")]
		ChunkingMode? Mode { get; set; }

		/// <summary>
		/// The time span that each search will be querying.
		/// This setting is only applicable when <see cref="Mode" /> is set to <see cref="ChunkingMode.Manual" />.
		/// </summary>
		[DataMember(Name = "time_span")]
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
		public ChunkingConfigDescriptor Mode(ChunkingMode? mode) => Assign(mode, (a, v) => a.Mode = v);

		/// <inheritdoc />
		public ChunkingConfigDescriptor TimeSpan(Time timeSpan) => Assign(timeSpan, (a, v) => a.TimeSpan = v);
	}


	/// <summary>
	/// The chunking mode
	/// </summary>
	[StringEnum]
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
