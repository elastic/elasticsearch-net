// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;

namespace Elasticsearch.Net
{
	public interface IConnectionConfigurationValues : ITransportConfigurationValues
	{
		/// <summary>
		/// Forces all requests to have ?error_trace=true querystring parameter appended,
		/// causing Elasticsearch to return stack traces as part of serialized exceptions
		/// Defaults to <c>false</c>
		/// </summary>
		bool IncludeServerStackTraceOnError { get; }

	}
}
