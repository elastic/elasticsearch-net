namespace Nest
{
	[MapsApi("indices.get_field_mapping.json")]
	public partial interface IGetFieldMappingRequest { }

	public partial class GetFieldMappingRequest { }

	// ReSharper disable once UnusedTypeParameter
	public partial class GetFieldMappingDescriptor<TDocument> where TDocument : class { }
}
