namespace Nest
{
	public partial interface IGetMappingRequest { }

	public partial class GetMappingRequest { }
	
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor<T> where T : class { }
}
