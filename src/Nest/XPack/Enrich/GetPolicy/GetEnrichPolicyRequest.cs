// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Gets one or more enrich policies
	/// </summary>
	[MapsApi("enrich.get_policy")]
	[ReadAs(typeof(GetEnrichPolicyRequest))]
	public partial interface IGetEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IGetEnrichPolicyRequest"/>
	public partial class GetEnrichPolicyRequest : IGetEnrichPolicyRequest
	{
	}

	/// <inheritdoc cref="IGetEnrichPolicyRequest"/>
	public partial class GetEnrichPolicyDescriptor
	{
	}
}
