namespace Nest
{
	[MapsApi("exists.json")]
	public partial interface IDocumentExistsRequest { }

	public interface IDocumentExistsRequest<T> : IDocumentExistsRequest where T : class { }

	public partial class DocumentExistsRequest
	{
		private object AutoRouteDocument() => null;
	}

	public partial class DocumentExistsRequest<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}

	public partial class DocumentExistsDescriptor<T> where T : class
	{
		private object AutoRouteDocument() => null;
	}
}
