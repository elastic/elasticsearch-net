namespace Nest
{
	[MapsApi("exists_source.json")]
	public partial interface ISourceExistsRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial interface ISourceExistsRequest<TDocument> where TDocument : class { }

	public partial class SourceExistsRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class SourceExistsRequest<TDocument> where TDocument : class { }

	// ReSharper disable once UnusedTypeParameter
	public partial class SourceExistsDescriptor<TDocument> where TDocument : class { }
}
