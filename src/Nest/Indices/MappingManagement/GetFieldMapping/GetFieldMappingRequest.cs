namespace Nest
{
	public partial interface IGetFieldMappingRequest { }

	public partial class GetFieldMappingRequest { }
	
	//TODO Removed typed request validate this is still valid

	[DescriptorFor("IndicesGetFieldMapping")]
	public partial class GetFieldMappingDescriptor<T> where T : class { }
}
