/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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
