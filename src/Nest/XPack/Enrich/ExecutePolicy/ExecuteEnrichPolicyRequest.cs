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
