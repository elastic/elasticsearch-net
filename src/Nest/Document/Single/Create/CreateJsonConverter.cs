namespace Nest
{
	internal class CreateJsonConverter : GenericProxyRequestConverterBase
	{
		public CreateJsonConverter() : base(typeof(CreateRequest<>)) { }
	}
}
