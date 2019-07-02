namespace Nest
{
	[MapsApi("indices.get_mapping.json")]
	public partial interface IGetMappingRequest { }

	public partial class GetMappingRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class GetMappingDescriptor<TDocument> where TDocument : class { }
}
