// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Nest.Infer;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingUrlTests
	{
		[U]
		public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			var fields = Field<Project>(p => p.Name).And("field");
			await GET($"/_mapping/field/name%2Cfield")
					.Request(c => c.Indices.GetFieldMapping(new GetFieldMappingRequest(fields)))
					.RequestAsync(c => c.Indices.GetFieldMappingAsync(new GetFieldMappingRequest(fields)))
				;

			await GET($"/index1%2Cindex2/_mapping/field/name%2Cfield")
					.Fluent(c => c.Indices.GetFieldMapping<Project>(fields, g => g.Index(index)))
					.Request(c => c.Indices.GetFieldMapping(new GetFieldMappingRequest(indices, fields)))
					.FluentAsync(c => c.Indices.GetFieldMappingAsync<Project>(fields, g => g.Index(index)))
					.RequestAsync(c => c.Indices.GetFieldMappingAsync(new GetFieldMappingRequest(indices, fields)))
				;

		}
	}
}
