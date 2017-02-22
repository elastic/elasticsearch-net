namespace Nest_5_2_0
{
	internal class IndexJsonConverter : DocumentJsonConverterBase<IIndexRequest>
	{
		public IndexJsonConverter() : base(typeof(IndexRequest<>)) { }
	}
}
