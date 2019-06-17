using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client,
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null
		)
			=> client.Cat.Fielddata(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client, ICatFielddataRequest request)
			=> client.Cat.Fielddata(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client,
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.FielddataAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client, ICatFielddataRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.FielddataAsync(request, ct);
	}
}
