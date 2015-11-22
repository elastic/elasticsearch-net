using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface IDeleteAliasRequest { }

	public partial class DeleteAliasRequest { }

	[DescriptorFor("IndicesDeleteAlias")]
	public partial class DeleteAliasDescriptor { }
}
