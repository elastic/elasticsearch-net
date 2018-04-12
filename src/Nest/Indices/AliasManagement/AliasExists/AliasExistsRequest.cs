using System;

namespace Nest
{
	public partial interface IAliasExistsRequest { }

	public partial class AliasExistsRequest { }

	[DescriptorFor("IndicesExistsAlias")]
	public partial class AliasExistsDescriptor
	{
		[Obsolete("Maintained for binary compatibility. Use the constructor that accepts Names. Will be removed in 7.0")]
		public AliasExistsDescriptor() { }

		public AliasExistsDescriptor Name(Names name)
		{
			this.RequestState.RouteValues.Required("name", name);
			return this;
		}
	}
}
