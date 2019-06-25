using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class GetCertificatesResponseBuilder : CustomResponseBuilderBase
	{
		public static GetCertificatesResponseBuilder Instance { get; } = new GetCertificatesResponseBuilder();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? new GetCertificatesResponse { Certificates = builtInSerializer.Deserialize<ClusterCertificateInformation[]>(stream) }
				: new GetCertificatesResponse();

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? new GetCertificatesResponse
				{
					Certificates = await builtInSerializer.DeserializeAsync<ClusterCertificateInformation[]>(stream, ctx).ConfigureAwait(false)
				}
				: new GetCertificatesResponse();
	}
}
