using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{

	public interface IPreviewDatafeedResponse<out TDocument> : IResponse where TDocument : class
	{
		IReadOnlyCollection<TDocument> Data { get; }
	}

	public class PreviewDatafeedResponse<TDocument> : ResponseBase, IPreviewDatafeedResponse<TDocument> where TDocument : class
	{
		public IReadOnlyCollection<TDocument> Data { get; internal set; } = EmptyReadOnly<TDocument>.Collection;
	}
}
