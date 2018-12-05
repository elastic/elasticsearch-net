namespace Nest
{
	internal class IndexRequestFormatter<TDocument> : ProxyRequestFormatterBase<IIndexRequest<TDocument>, IndexRequest<TDocument>>
		where TDocument : class
	{
	}
}
