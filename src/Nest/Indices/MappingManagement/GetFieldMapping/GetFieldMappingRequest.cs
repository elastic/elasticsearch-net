using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetFieldMappingRequest { }

	public partial class GetFieldMappingRequest { }
	
	//TODO Removed typed request validate this is still valid

	[DescriptorFor("IndicesGetFieldMapping")]
	public partial class GetFieldMappingDescriptor<T> where T : class { }
}
