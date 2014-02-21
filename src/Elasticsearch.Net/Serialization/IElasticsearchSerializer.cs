namespace Nest
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(byte[] bytes) where T : class;
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);

	}
}