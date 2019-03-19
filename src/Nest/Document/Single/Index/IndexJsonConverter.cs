namespace Nest
{
	internal class IndexJsonConverter : DocumentProxyRequestConverterBase
	{
		public IndexJsonConverter() : base(typeof(IndexRequest<>)) { }
	}
}
