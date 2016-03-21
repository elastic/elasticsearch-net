using System;
#pragma warning disable 612

namespace Nest
{
	[Obsolete()]
	public partial interface ICatNodeattrsRequest { }

	[Obsolete()]
	public partial class CatNodeattrsRequest { }

	[Obsolete()]
	public partial class CatNodeattrsDescriptor { }


	public interface ICatNodeAttributesRequest : ICatNodeattrsRequest { }
	public class CatNodeAttributesRequest : CatNodeattrsRequest, ICatNodeAttributesRequest { }
	public class CatNodeAttributesDescriptor : CatNodeattrsDescriptor, ICatNodeAttributesRequest { }
}
