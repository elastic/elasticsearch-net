namespace Nest
{
	internal class IndexJsonConverter : GenericProxyRequestConverterBase
	{
		public IndexJsonConverter() : base(typeof(IndexRequest<>)) { }
	}
}
