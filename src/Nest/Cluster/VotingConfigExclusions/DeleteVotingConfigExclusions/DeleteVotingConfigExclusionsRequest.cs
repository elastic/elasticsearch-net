// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Elasticsearch.Net.Specification.ClusterApi;

namespace Nest
{
	[MapsApi("cluster.delete_voting_config_exclusions")]
	public partial interface IDeleteVotingConfigExclusionsRequest { }

	public partial class DeleteVotingConfigExclusionsRequest
	{
		protected sealed override void RequestDefaults(DeleteVotingConfigExclusionsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new DeleteVotingConfigExclusionsResponseBuilder();
	}

	public partial class DeleteVotingConfigExclusionsDescriptor
	{
		protected sealed override void RequestDefaults(DeleteVotingConfigExclusionsRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new DeleteVotingConfigExclusionsResponseBuilder();
	}

	/// <summary>
	/// Response is a single newline character, so skip trying to deserialize to an object
	/// </summary>
	internal class DeleteVotingConfigExclusionsResponseBuilder : CustomResponseBuilderBase
	{
		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			new DeleteVotingConfigExclusionsResponse();

		public override Task<object> DeserializeResponseAsync(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default) =>
			Task.FromResult<object>(new DeleteVotingConfigExclusionsResponse());
	}
}
