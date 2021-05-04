// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.MultiTermVectors
{
	public class MultiTermVectorsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_mtermvectors")
					.Fluent(c => c.MultiTermVectors())
					.Request(c => c.MultiTermVectors(new MultiTermVectorsRequest()))
					.FluentAsync(c => c.MultiTermVectorsAsync())
					.RequestAsync(c => c.MultiTermVectorsAsync(new MultiTermVectorsRequest()))
				;

			await POST("/project/_mtermvectors")
					.Fluent(c => c.MultiTermVectors(m => m.Index<Project>()))
					.Request(c => c.MultiTermVectors(new MultiTermVectorsRequest(typeof(Project))))
					.FluentAsync(c => c.MultiTermVectorsAsync(m => m.Index<Project>()))
					.RequestAsync(c => c.MultiTermVectorsAsync(new MultiTermVectorsRequest(typeof(Project))))
				;
		}
	}
}
