using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Serialization;
using Tests.Framework.Integration;

namespace Tests.Analysis
{
	public interface IAnalysisAssertion
	{
		string Name { get; }
		object Json { get; }
	}
	public interface IAnalysisAssertion<out TComponent, out TContainer, in TDescriptor> : IAnalysisAssertion
		where TContainer : class
	{
		TComponent Initializer { get; }
		Func<string, TDescriptor, IPromise<TContainer>> Fluent { get; }
	}

	[IntegrationTestCluster(typeof(WritableCluster))]
	public abstract class AnalysisComponentTestBase<TAssertion, TComponent, TContainer, TDescriptor>
		: IAnalysisAssertion<TComponent, TContainer, TDescriptor>
		where TAssertion : AnalysisComponentTestBase<TAssertion, TComponent, TContainer, TDescriptor>, new()
		where TContainer : class
	{
		private static readonly SingleEndpointUsage<ICreateIndexResponse> Usage = new SingleEndpointUsage<ICreateIndexResponse>
		(
			fluent: (s, c) => c.CreateIndex(s, AssertionSetup.FluentCall),
			fluentAsync: (s, c) => c.CreateIndexAsync(s, AssertionSetup.FluentCall),
			request: (s, c) => c.CreateIndex(AssertionSetup.InitializerCall(s)),
			requestAsync: (s, c) => c.CreateIndexAsync(AssertionSetup.InitializerCall(s)),
			valuePrefix: $"test-{typeof(TAssertion).Name.ToLowerInvariant()}"
		)
		{
			OnAfterCall = c=> c.DeleteIndex(Usage.CallUniqueValues.Value)
		};
		protected static TAssertion AssertionSetup { get; } = new TAssertion();

		protected AnalysisComponentTestBase()
		{
			this.Client = (ElasticXunitRunner.CurrentCluster as ReadOnlyCluster)?.Client ?? TestClient.DefaultInMemoryClient;
			Usage.KickOffOnce(this.Client, oneRandomCall: true);
		}

		private IElasticClient Client { get; }

		public abstract string Name { get; }
		public abstract TComponent Initializer { get; }
		public abstract Func<string, TDescriptor, IPromise<TContainer>> Fluent { get; }
		public abstract object Json { get; }

		private Func<CreateIndexDescriptor, ICreateIndexRequest> FluentCall => i =>i.Settings(s => s.Analysis(this.FluentAnalysis));
		protected abstract IAnalysis FluentAnalysis(AnalysisDescriptor an);

		private CreateIndexRequest InitializerCall(string index) => new CreateIndexRequest(index)
		{
			Settings = new IndexSettings { Analysis = this.InitializerAnalysis() }
		};
		protected abstract Nest.Analysis InitializerAnalysis();

		[U] public virtual async Task TestPutSettingsRequest() => await Usage.AssertOnAllResponses(r =>
		{
			var json = new { settings = new { analysis = this.AnalysisJson } };
			SerializationTestHelper.Expect(json).FromRequest(r);
		});

		protected abstract object AnalysisJson { get; }

		[I] public virtual async Task TestPutSettingsResponse() => await Usage.AssertOnAllResponses(r =>
		{
			r.ApiCall.HttpStatusCode.Should().Be(200);
		});

	}
}
