namespace Nest
{
	public partial interface IGetMappingRequest { }

	//TODO removed typed request validate this is ok in new stup

	public partial class GetMappingRequest { }
	
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor<T> where T : class { }
}
