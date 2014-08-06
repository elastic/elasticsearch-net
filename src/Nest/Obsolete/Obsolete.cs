using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Obsolete
{
	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsDescriptor")]
	public class IndicesExistsAliasDescriptor : AliasExistsDescriptor {}

	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequest")]
	public class IndicesExistsAliasRequest : AliasExistsRequest 
	{
		public IndicesExistsAliasRequest() : base("") {}
		public IndicesExistsAliasRequest(string name) : base(name) { }
		public IndicesExistsAliasRequest(IndexNameMarker index, string name) : base(index, name) { }
	}



}
