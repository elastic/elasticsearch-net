using System;
using System.Runtime.Serialization;

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
