using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Master(), please update this usage.")]
		public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client, Func<CatMasterDescriptor, ICatMasterRequest> selector = null)
			=> client.Cat.Master(selector);

		[Obsolete("Moved to client.Cat.Master(), please update this usage.")]
		public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client, ICatMasterRequest request)
			=> client.Cat.Master(request);

		[Obsolete("Moved to client.Cat.MasterAsync(), please update this usage.")]
		public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client,
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.MasterAsync(selector, ct);

		[Obsolete("Moved to client.Cat.MasterAsync(), please update this usage.")]
		public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client, ICatMasterRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.MasterAsync(request, ct);
	}
}
