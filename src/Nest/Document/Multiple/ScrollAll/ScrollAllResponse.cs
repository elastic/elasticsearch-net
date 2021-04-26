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

namespace Nest
{
	/// <summary> A response returned for each scroll in ScrollAll() </summary>
	public interface IScrollAllResponse<out T> where T : class
	{
		/// <summary>
		/// The nth scroll this response represents
		/// </summary>
		long Scroll { get; }

		/// <summary>
		/// The scroll result
		/// </summary>
		ISearchResponse<T> SearchResponse { get; }

		/// <summary>
		/// The nth slice this response belongs to
		/// </summary>
		int Slice { get; }
	}

	/// <summary> A response returned for each scroll in ScrollAll() </summary>
	[DataContract]
	public class ScrollAllResponse<T> : IScrollAllResponse<T> where T : class
	{
		/// <inheritdoc />
		public bool IsValid => SearchResponse != null && SearchResponse.IsValid;

		/// <inheritdoc />
		public long Scroll { get; internal set; }

		/// <inheritdoc />
		public ISearchResponse<T> SearchResponse { get; internal set; }

		/// <inheritdoc />
		public int Slice { get; internal set; }
	}
}
