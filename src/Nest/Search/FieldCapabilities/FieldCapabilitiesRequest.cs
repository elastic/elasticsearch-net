using Elasticsearch.Net;

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

		public FieldCapabilitiesDescriptor Fields(Fields fields) =>
			AssignParam(p => p.AddQueryString("fields", fields));
	}
}
