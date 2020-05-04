// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Deletes an enrich policy
	/// </summary>
	[MapsApi("enrich.delete_policy")]
	[ReadAs(typeof(DeleteEnrichPolicyRequest))]
	public partial interface IDeleteEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IDeleteEnrichPolicyRequest"/>
	public partial class DeleteEnrichPolicyRequest : IDeleteEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IDeleteEnrichPolicyRequest"/>
	public partial class DeleteEnrichPolicyDescriptor
	{
	}
}
