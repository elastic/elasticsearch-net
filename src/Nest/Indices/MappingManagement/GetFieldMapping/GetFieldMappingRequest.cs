namespace Nest
{
	public partial interface IGetFieldMappingRequest { }

	public partial class GetFieldMappingRequest { }
	
	[DescriptorFor("IndicesGetFieldMapping")]
	public partial class GetFieldMappingDescriptor<T> where T : class { }
}
