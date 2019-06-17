using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client, Func<CatHelpDescriptor, ICatHelpRequest> selector = null)
			=> client.Cat.Help(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatHelpRecord> CatHelp(this IElasticClient client, ICatHelpRequest request)
			=> client.Cat.Help(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client,
			Func<CatHelpDescriptor, ICatHelpRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.HelpAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatHelpRecord>> CatHelpAsync(this IElasticClient client, ICatHelpRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.HelpAsync(request, ct);
	}
}
