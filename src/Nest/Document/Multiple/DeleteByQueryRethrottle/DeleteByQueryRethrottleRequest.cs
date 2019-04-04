namespace Nest
{
	/// <summary>
	/// Rethrottles a running delete by query
	/// </summary>
	[MapsApi("delete_by_query_rethrottle")]
	public partial interface IDeleteByQueryRethrottleRequest { }

	/// <inheritdoc cref="IDeleteByQueryRethrottleRequest" />
	public partial class DeleteByQueryRethrottleRequest : IDeleteByQueryRethrottleRequest { }

	/// <inheritdoc cref="IDeleteByQueryRethrottleRequest" />
	public partial class DeleteByQueryRethrottleDescriptor : IDeleteByQueryRethrottleRequest { }
}
