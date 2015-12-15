namespace Nest
{
	public partial interface IRefreshRequest { }

	public partial class RefreshRequest { }

	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor { }
}
