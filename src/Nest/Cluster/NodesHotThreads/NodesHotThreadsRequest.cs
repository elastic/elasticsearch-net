// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net;
using Elasticsearch.Net.Specification.NodesApi;

namespace Nest
{
	[MapsApi("nodes.hot_threads.json")]
	public partial interface INodesHotThreadsRequest { }

	public partial class NodesHotThreadsRequest
	{
		protected override string ContentType => RequestData.MimeTypeTextPlain;

		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}

	public partial class NodesHotThreadsDescriptor
	{
		protected override string ContentType => RequestData.MimeTypeTextPlain;
		
		protected sealed override void RequestDefaults(NodesHotThreadsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = NodeHotThreadsResponseBuilder.Instance;
	}
}
