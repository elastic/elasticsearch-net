namespace Nest
{
	[MapsApi("exists.json")]
	public partial interface IDocumentExistsRequest { }

	// ReSharper disable once UnusedMember.Global
	// ReSharper disable once UnusedTypeParameter
	public partial interface IDocumentExistsRequest<TDocument> where TDocument : class { }

	public partial class DocumentExistsRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class DocumentExistsRequest<TDocument> where TDocument : class { }

	// ReSharper disable once UnusedTypeParameter
	public partial class DocumentExistsDescriptor<TDocument> where TDocument : class { }
}
