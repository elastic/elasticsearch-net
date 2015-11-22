using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteIndexRequest { }

	public partial class DeleteIndexRequest { }

	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor { }
}
