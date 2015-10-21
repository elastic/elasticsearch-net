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
	public abstract class ApiIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer> 
		: ApiTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract int ExpectStatusCode { get; }
		protected abstract bool ExpectIsValid { get; }

		protected ApiIntegrationTestBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		[I] protected async Task HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		protected override Task AssertOnAllResponses(Action<TResponse> assert)
		{
			if (!this.ExpectIsValid) return base.AssertOnAllResponses(assert);

			return base.AssertOnAllResponses((r) =>
			{
				if (!r.IsValid && r.CallDetails.OriginalException != null)
				{
					throw r.CallDetails.OriginalException;
				}

				assert(r);
			});
		}
	}
}
