using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class GetCertificatesResponseBuilder : CustomResponseBuilderBase
	{
		public static GetCertificatesResponseBuilder Instance { get; } = new GetCertificatesResponseBuilder();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var result = builtInSerializer.Deserialize<ClusterCertificateInformation[]>(stream);
			return new GetCertificatesResponse { Certificates = result };
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			var result = await builtInSerializer.DeserializeAsync<ClusterCertificateInformation[]>(stream);
			return new GetCertificatesResponse { Certificates = result };
		}
	}
}
