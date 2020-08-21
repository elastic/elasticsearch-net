// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	public enum PipelineFailure
	{
		BadAuthentication,
		BadResponse,
		PingFailure,
		SniffFailure,
		CouldNotStartSniffOnStartup,
		MaxTimeoutReached,
		MaxRetriesReached,
		Unexpected,
		BadRequest,
		NoNodesAttempted
	}
}
