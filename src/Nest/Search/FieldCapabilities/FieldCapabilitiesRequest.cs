using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("field_caps.json")]
	public partial interface IFieldCapabilitiesRequest { }

	public partial class FieldCapabilitiesRequest
	{
		protected override HttpMethod HttpMethod => HttpMethod.GET;
	}

	public partial class FieldCapabilitiesDescriptor
	{
		protected override HttpMethod HttpMethod => HttpMethod.GET;
	}
}
