namespace Nest
{
	internal class CreateJsonConverter : DocumentJsonConverterBase<ICreateRequest>
	{
		public CreateJsonConverter() : base(typeof(CreateRequest<>)) { }
	}
}
