using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client,Func<CatHelpDescriptor, ICatHelpRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client,ICatHelpRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client,Func<CatHelpDescriptor, ICatHelpRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client,ICatHelpRequest request, CancellationToken ct = default);
	}

	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public CatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null) =>
			public static CatHelp(selector.InvokeOrDefault(new CatHelpDescriptor(this IElasticClient client,)));

		/// <inheritdoc />
		public CatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request)
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatHelpResponse;
			return DoRequest<ICatHelpRequest, CatResponse<CatHelpRecord>>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<CatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null, CancellationToken ct = default) =>
			public static CatHelpAsync(selector.InvokeOrDefault(new CatHelpDescriptor(this IElasticClient client,)), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken ct = default)
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatHelpResponse;
			return DoRequestAsync<ICatHelpRequest, CatResponse<CatHelpRecord>>(request, request.RequestParameters, ct);
		}

		private static CatResponse<CatHelpRecord> DeserializeCatHelpResponse(IApiCallDetails response, Stream stream)
		{
			using (stream)
			using (var ms = response.ConnectionConfiguration.MemoryStreamFactory.Create())
			{
				stream.CopyTo(ms);
				var body = ms.ToArray().Utf8String();
				return new CatResponse<CatHelpRecord>
				{
					Records = body.Split('\n')
						.Skip(1)
						.Select(f => new CatHelpRecord { Endpoint = f.Trim() })
						.ToList()
				};
			}
		}
	}
}
