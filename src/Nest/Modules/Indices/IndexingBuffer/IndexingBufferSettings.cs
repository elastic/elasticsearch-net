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

namespace Nest
{
	/// <summary>
	/// The indexing buffer is used to store newly indexed documents. When it fills up, the documents in the buffer are written to a segment on
	/// disk. It is divided between all shards on the node.
	/// <para>The following settings are static and must be configured on every data node in the cluster</para>
	/// </summary>
	public class IndexingBufferSettings
	{
		/// <summary>
		/// Accepts either a percentage or a byte size value. It defaults to 10%, meaning that 10% of the total heap allocated to a node will
		/// be used as the indexing buffer size.
		/// </summary>
		public string IndexBufferSize { get; internal set; }

		/// <summary>
		/// If the index_buffer_size is specified as a percentage, then this setting can be used to specify an absolute maximum. Defaults to
		/// unbounded.
		/// </summary>
		public string IndexBufferSizeMaximum { get; internal set; }

		/// <summary>
		/// If the index_buffer_size is specified as a percentage, then this setting can be used to specify an absolute minimum. Defaults to
		/// 48mb.
		/// </summary>
		public string IndexBufferSizeMinimum { get; internal set; }

		/// <summary>Sets a hard lower limit for the memory allocated per shard for its own indexing buffer. Defaults to 4mb.</summary>
		public string ShardBufferSizeMinimum { get; internal set; }
	}
}
