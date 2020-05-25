// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public partial interface ISearchShardsRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial interface ISearchShardsRequest<TDocument> : ISearchShardsRequest { }

	public partial class SearchShardsRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class SearchShardsRequest<TDocument> where TDocument : class { }

	/// <summary>
	/// A descriptor which describes a search operation for _search_shards
	/// </summary>
	// ReSharper disable once UnusedTypeParameter
	public partial class SearchShardsDescriptor<TDocument> where TDocument : class { }
}
