namespace Nest
{
	internal class CreateRequestFormatter<TDocument> : ProxyRequestFormatterBase<ICreateRequest<TDocument>, CreateRequest<TDocument>>
		where TDocument : class
	{
	}
}
