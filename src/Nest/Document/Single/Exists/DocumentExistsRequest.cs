namespace Nest
{
	[MapsApi("exists.json")]
	public partial interface IDocumentExistsRequest { }

	public partial interface IDocumentExistsRequest<TDocument> where TDocument : class { }

	public partial class DocumentExistsRequest { }

	public partial class DocumentExistsRequest<TDocument> where TDocument : class { }

	public partial class DocumentExistsDescriptor<TDocument> where TDocument : class { }
}
