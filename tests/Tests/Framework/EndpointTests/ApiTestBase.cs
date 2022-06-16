// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Transport.Extensions;
using Elastic.Transport.Products.Elasticsearch;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Extensions;
using VerifyXunit;
using HttpMethod = Elastic.Transport.HttpMethod;

namespace Tests.Framework.EndpointTests
{
	[UsesVerify]
	public abstract class ApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		: RequestResponseApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
		where TResponse : class, IElasticsearchResponse
		where TDescriptor : class
		where TInitializer : class
	{
		protected ApiTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = null;
		protected virtual bool VerifyNdJson { get; } = false;
		protected virtual bool VerifyJson { get; } = false;
		protected virtual bool VerifyResponseObjects { get; } = false;

		protected override IReadOnlyList<object> ExpectNdjson { get; } = null;

		protected abstract HttpMethod HttpMethod { get; }
		protected abstract string ExpectedUrlPathAndQuery { get; }

		[U]
		protected virtual async Task VerifyInitializerJson()
		{
			if (VerifyJson)
				await Verifier.VerifyJson(SerializeUsingClient(Initializer));
		}

		[U]
		protected virtual async Task VerifyDescriptorJson()
		{
			if (VerifyJson)
			{
				var descriptor = NewDescriptor();
				Fluent?.Invoke(descriptor);
				await Verifier.VerifyJson(SerializeUsingClient(descriptor));
			}
		}

		[U]
		protected virtual async Task VerifyInitializerNdJson()
		{
			if (VerifyNdJson)
				await Verifier.Verify(SerializeUsingClient(Initializer));
		}

		[U]
		protected virtual async Task VerifyDescriptorNdJson()
		{
			if (VerifyNdJson)
			{
				var descriptor = NewDescriptor();
				Fluent?.Invoke(descriptor);
				await Verifier.Verify(SerializeUsingClient(descriptor));
			}
		}

		[U]
		protected virtual async Task VerifyResponses()
		{
			if (VerifyResponseObjects)
			{
				var responses = await Responses;
				await Verifier.Verify(responses);
			}
		}

		[U] protected virtual async Task HitsTheCorrectUrl() => await AssertOnAllResponses(r => AssertUrl(r.ApiCall.Uri));

		[U] protected virtual async Task UsesCorrectHttpMethod() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpMethod.Should().Be(HttpMethod, UniqueValues.CurrentView.GetStringValue()));

		[U] protected virtual void SerializesInitializer() => RoundTripsOrSerializes(Initializer);

		[U]
		protected virtual void SerializesFluent()
		{
			var descriptor = NewDescriptor();
			Fluent?.Invoke(descriptor);
			RoundTripsOrSerializes(descriptor, false);
		}

		private void AssertUrl(Uri u) => u.PathEquals(ExpectedUrlPathAndQuery, UniqueValues.CurrentView.GetStringValue());
	}

	public abstract class NdJsonApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		: RequestResponseApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
		where TResponse : class, IElasticsearchResponse
		where TDescriptor : class
		where TInitializer : class
	{
		protected NdJsonApiTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = null;

		protected override IReadOnlyList<object> ExpectNdjson { get; } = null;

		protected abstract HttpMethod HttpMethod { get; }
		protected abstract string ExpectedUrlPathAndQuery { get; }

		[U] protected virtual async Task HitsTheCorrectUrl() => await AssertOnAllResponses(r => AssertUrl(r.ApiCall.Uri));

		[U]
		protected virtual async Task UsesCorrectHttpMethod() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpMethod.Should().Be(HttpMethod, UniqueValues.CurrentView.GetStringValue()));

		[U] protected virtual void SerializesInitializer() => SerializesNdjson(Initializer);

		[U]
		protected virtual void SerializesFluent()
		{
			var descriptor = NewDescriptor();
			Fluent?.Invoke(descriptor);
			SerializesNdjson(descriptor);
		}

		private void AssertUrl(Uri u) => u.PathEquals(ExpectedUrlPathAndQuery, UniqueValues.CurrentView.GetStringValue());
	}
}
