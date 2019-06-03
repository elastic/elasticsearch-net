using Elasticsearch.Net.Specification.SecurityApi;

namespace Nest
{
	[MapsApi("ssl.certificates.json")]
	public partial interface IGetCertificatesRequest { }

	public partial class GetCertificatesRequest
	{
		protected sealed override void RequestDefaults(GetCertificatesRequestParameters parameters) =>
			parameters.CustomResponseBuilder = GetCertificatesResponseBuilder.Instance;
	}

	public partial class GetCertificatesDescriptor
	{
		protected sealed override void RequestDefaults(GetCertificatesRequestParameters parameters) =>
			parameters.CustomResponseBuilder = GetCertificatesResponseBuilder.Instance;
	}
}
