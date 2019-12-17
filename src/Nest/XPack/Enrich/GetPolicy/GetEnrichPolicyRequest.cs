using System;
using System.Runtime.Serialization;

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
