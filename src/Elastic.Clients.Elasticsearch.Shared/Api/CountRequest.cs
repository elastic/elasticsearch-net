// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
using Elastic.Clients.Elasticsearch.QueryDsl;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public sealed partial class CountRequest<TDocument> : CountRequest
{
	//protected CountRequest<TDocument> TypedSelf => this;

	///<summary>/{index}/_count</summary>
	public CountRequest() : base(typeof(TDocument))
	{
	}

	///<summary>/{index}/_count</summary>
	///<param name = "index">Optional, accepts null</param>
	public CountRequest(Indices index) : base(index)
	{
	}
}

public partial class CountRequestDescriptor
{
	public CountRequestDescriptor Index(Indices indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	public CountRequestDescriptor Query(Func<QueryDescriptor, Query> configure)
	{
		var container = configure?.Invoke(new QueryDescriptor());
		QueryValue = container;
		return Self;
	}
}
