using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Fielddata(), please update this usage.")]
		public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client,
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null
		)
			=> client.Cat.Fielddata(selector);

		[Obsolete("Moved to client.Cat.Fielddata(), please update this usage.")]
		public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client, ICatFielddataRequest request)
			=> client.Cat.Fielddata(request);

		[Obsolete("Moved to client.Cat.FielddataAsync(), please update this usage.")]
		public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client,
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.FielddataAsync(selector, ct);

		[Obsolete("Moved to client.Cat.FielddataAsync(), please update this usage.")]
		public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client, ICatFielddataRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.FielddataAsync(request, ct);
	}
}
