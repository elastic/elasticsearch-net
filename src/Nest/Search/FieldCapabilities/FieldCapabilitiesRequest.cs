
using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IFieldCapabilitiesRequest { }

	public partial class FieldCapabilitiesRequest
	{
		protected override HttpMethod HttpMethod => HttpMethod.GET;
	}

	[DescriptorFor("FieldCaps")]
	public partial class FieldCapabilitiesDescriptor
	{
		protected override HttpMethod HttpMethod => HttpMethod.GET;
	}
}
