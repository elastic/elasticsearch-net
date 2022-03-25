// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Transport.Products.Elasticsearch;

namespace Tests.Framework.EndpointTests
{
	public static class UrlTesterExtensions
	{
		public static async Task<UrlTester> RequestAsync<TResponse>(this Task<UrlTester> tester, Func<ElasticsearchClient, Task<TResponse>> call)
			where TResponse : IElasticsearchResponse => await (await tester).WhenCallingAsync(call, "request async");

		public static async Task<UrlTester> FluentAsync<TResponse>(this Task<UrlTester> tester, Func<ElasticsearchClient, Task<TResponse>> call)
			where TResponse : IElasticsearchResponse => await (await tester).WhenCallingAsync(call, "fluent async");
	}
}
