using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ITypeExistsRequest { }

	public partial class TypeExistsRequest { }

	[DescriptorFor("IndicesExistsType")]
	public partial class TypeExistsDescriptor { }
}
