// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Specification.ClusterApi;

namespace Nest
{
	[MapsApi("cluster.post_voting_config_exclusions")]
	public partial interface IPostVotingConfigExclusionsRequest { }

	public partial class PostVotingConfigExclusionsRequest
	{
		protected sealed override void RequestDefaults(PostVotingConfigExclusionsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new PostVotingConfigExclusionsResponseBuilder();
	}

	public partial class PostVotingConfigExclusionsDescriptor
	{
		protected sealed override void RequestDefaults(PostVotingConfigExclusionsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new PostVotingConfigExclusionsResponseBuilder();
	}

	/// <summary>
	/// Response is a single newline character, so skip trying to deserialize to an object
	/// </summary>
	internal class PostVotingConfigExclusionsResponseBuilder : CustomResponseBuilderBase
	{
		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			new PostVotingConfigExclusionsResponse();

		public override Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default) =>
			Task.FromResult<object>(new PostVotingConfigExclusionsResponse());
	}
}
