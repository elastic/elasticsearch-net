using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client, Func<CatMasterDescriptor, ICatMasterRequest> selector = null)
			=> client.Cat.Master(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client, ICatMasterRequest request)
			=> client.Cat.Master(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client,
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.MasterAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client, ICatMasterRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.MasterAsync(request, ct);
	}
}
