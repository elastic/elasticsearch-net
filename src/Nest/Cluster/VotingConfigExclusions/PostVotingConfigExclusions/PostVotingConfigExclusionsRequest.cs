/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
