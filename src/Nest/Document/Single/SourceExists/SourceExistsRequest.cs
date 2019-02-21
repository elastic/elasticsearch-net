namespace Nest
{
	[MapsApi("exists_source.json")]
	public partial interface ISourceExistsRequest { }

	public partial interface ISourceExistsRequest<TDocument> where TDocument : class { }

	public partial class SourceExistsRequest { }

	public partial class SourceExistsRequest<TDocument> where TDocument : class { }

	public partial class SourceExistsDescriptor<TDocument> where TDocument : class { }
}
