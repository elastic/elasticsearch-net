using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetIndexRequest 
	{
	}

	public partial class GetIndexRequest 
	{
	}

	[DescriptorFor("IndicesGet")]
	public partial class GetIndexDescriptor 
	{
	}
}
