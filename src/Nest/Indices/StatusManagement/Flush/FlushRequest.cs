namespace Nest
{
	public partial interface IFlushRequest { }

	public partial class FlushRequest { }

	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor { }
}
