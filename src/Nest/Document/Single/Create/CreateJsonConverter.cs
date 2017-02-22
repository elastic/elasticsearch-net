namespace Nest_5_2_0
{
	internal class CreateJsonConverter : DocumentJsonConverterBase<ICreateRequest>
	{
		public CreateJsonConverter() : base(typeof(CreateRequest<>)) { }
	}
}
