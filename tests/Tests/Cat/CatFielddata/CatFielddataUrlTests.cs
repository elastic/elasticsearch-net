// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/fielddata")
					.Fluent(c => c.Cat.Fielddata())
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest()))
					.FluentAsync(c => c.Cat.FielddataAsync())
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest()))
				;

			var fields = new[] { "name", "startedOn" };

			await GET("/_cat/fielddata/name%2CstartedOn")
					.Fluent(c => c.Cat.Fielddata(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest(fields)))
					.FluentAsync(c => c.Cat.FielddataAsync(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest(fields)))
				;
		}
	}
}
