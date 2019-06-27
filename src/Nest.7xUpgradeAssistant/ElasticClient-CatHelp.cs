using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Help(), please update this usage.")]
		public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client, Func<CatHelpDescriptor, ICatHelpRequest> selector = null)
			=> client.Cat.Help(selector);

		[Obsolete("Moved to client.Cat.Help(), please update this usage.")]
		public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client, ICatHelpRequest request)
			=> client.Cat.Help(request);

		[Obsolete("Moved to client.Cat.HelpAsync(), please update this usage.")]
		public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client,
			Func<CatHelpDescriptor, ICatHelpRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.HelpAsync(selector, ct);

		[Obsolete("Moved to client.Cat.HelpAsync(), please update this usage.")]
		public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client, ICatHelpRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.HelpAsync(request, ct);
	}
}
