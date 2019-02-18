namespace Nest
{
	[MapsApi("exists_source.json")]
	public partial interface ISourceExistsRequest { }

	public interface ISourceExistsRequest<T> : ISourceExistsRequest where T : class { }

	public partial class SourceExistsRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class SourceExistsRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	public partial class SourceExistsDescriptor<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}
}
