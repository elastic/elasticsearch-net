// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Transport.Products.Elasticsearch;

namespace Tests.Framework.EndpointTests.TestState
{
	public class LazyResponses : AsyncLazy<Dictionary<ClientMethod, ElasticsearchResponse>>
	{
		public LazyResponses(Func<Dictionary<ClientMethod, ElasticsearchResponse>> factory) : this("__ignored__", factory) {}

		public LazyResponses(Func<Task<Dictionary<ClientMethod, ElasticsearchResponse>>> factory) : this("__ignored__", factory) {}

		public LazyResponses(string name, Func<Dictionary<ClientMethod, ElasticsearchResponse>> factory) : base(factory) => Name = name;

		public LazyResponses(string name, Func<Task<Dictionary<ClientMethod, ElasticsearchResponse>>> factory) : base(factory) => Name = name;

		public static LazyResponses Empty { get; } = new("__empty__", () => new Dictionary<ClientMethod, ElasticsearchResponse>());

		public string Name { get; }
	}
}
