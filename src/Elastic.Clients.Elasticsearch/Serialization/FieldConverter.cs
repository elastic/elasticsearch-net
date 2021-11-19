namespace Elastic.Clients.Elasticsearch
{
	//public class FieldConverterFactory : JsonConverterFactory
	//{
	//	private readonly IElasticsearchClientSettings _settings;

	//	public FieldConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	//	public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Field);

	//	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
	//		new FieldConverter(_settings);
	//}

	//public class FieldConverter : JsonConverter<Field?>
	//{
	//	private readonly IElasticsearchClientSettings _settings;

	//	public FieldConverter(IElasticsearchClientSettings settings) => _settings = settings;

	//	public override Field? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
	//		throw new NotImplementedException();

	//	public override void Write(Utf8JsonWriter writer, Field? value, JsonSerializerOptions options)
	//	{
	//		 TODO - This needs to be more thorough! See the original formatter

	//		if (value is null)
	//		{
	//			writer.WriteNullValue();
	//			return;
	//		}

	//		var fieldName = _settings.Inferrer.Field(value);
	//		writer.WriteStringValue(fieldName);
	//	}
	//}
}
