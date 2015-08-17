using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Elasticsearch.Net;

namespace Tests.Framework
{
	public abstract class ApiCallIntegration<TResponse, TInterface, TDescriptor, TInitializer> : 
		ApiCallExample<TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface, new() 
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private LazyResponses _responses;

		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }

		protected ApiCallIntegration(IIntegrationCluster cluster, ApiUsage usage)
			: base(cluster, usage) { }

		private void AssertUrl(Uri u)
		{
			var paths = (this.UrlPath ?? "").Split(new [] { '?' }, 2);
			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http","localhost", IntegrationPort, path,  "?" + query).Uri;

			u.AbsolutePath.Should().Be(expectedUri.AbsolutePath);
			u = new UriBuilder(u.Scheme, u.Host, u.Port, u.AbsolutePath, u.Query.Replace("pretty=true", "")).Uri;

			var queries = new[] {u.Query, expectedUri.Query};
			if (queries.All(string.IsNullOrWhiteSpace)) return;
			if (queries.Any(string.IsNullOrWhiteSpace))
			{
				queries.Last().Should().Be(queries.First());
				return;
			}

			var clientKeyValues = u.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k=>!string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());
			var expectedKeyValues = expectedUri.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k=>!string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());

			clientKeyValues.Count().Should().Be(expectedKeyValues.Count());
			clientKeyValues.Should().ContainKeys(expectedKeyValues.Keys.ToArray());
			clientKeyValues.Should().Equal(expectedKeyValues);
		}

		[I] protected async void HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async void ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

	}

}
