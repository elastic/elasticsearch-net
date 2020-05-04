// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Executes an enrich policy
	/// </summary>
	[MapsApi("enrich.execute_policy")]
	[ReadAs(typeof(ExecuteEnrichPolicyRequest))]
	public partial interface IExecuteEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IExecuteEnrichPolicyRequest"/>
	public partial class ExecuteEnrichPolicyRequest : IExecuteEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IExecuteEnrichPolicyRequest"/>
	public partial class ExecuteEnrichPolicyDescriptor
	{
	}
}
