using System;
#pragma warning disable 612, 618

namespace Nest
{
	[Obsolete("Scheduled to be removed in 5.0.  Use ICatNodeAttributesRequest instead.")]
	public partial interface ICatNodeattrsRequest { }

	[Obsolete("Scheduled to be removed in 5.0.  Use CatNodeAttributesRequest instead.")]
	public partial class CatNodeattrsRequest { }

	[Obsolete("Scheduled to be removed in 5.0.  Use CatNodeAttributesDescriptor instead.")]
	public partial class CatNodeattrsDescriptor { }


	public interface ICatNodeAttributesRequest : ICatNodeattrsRequest { }
	public class CatNodeAttributesRequest : CatNodeattrsRequest, ICatNodeAttributesRequest { }
	public class CatNodeAttributesDescriptor : CatNodeattrsDescriptor, ICatNodeAttributesRequest { }
}

#pragma warning restore 612, 618
