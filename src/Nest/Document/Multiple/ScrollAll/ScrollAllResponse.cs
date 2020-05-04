// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

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
