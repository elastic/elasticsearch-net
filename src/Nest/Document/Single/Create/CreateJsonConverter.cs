namespace Nest
{
	internal class CreateJsonConverter : DocumentProxyRequestConverterBase
	{
		public CreateJsonConverter() : base(typeof(CreateRequest<>)) { }
	}
}
