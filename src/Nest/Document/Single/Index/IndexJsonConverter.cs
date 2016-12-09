namespace Nest
{
	internal class IndexJsonConverter : DocumentJsonConverterBase<IIndexRequest>
	{
		public IndexJsonConverter() : base(typeof(IndexRequest<>)) { }
	}
}
