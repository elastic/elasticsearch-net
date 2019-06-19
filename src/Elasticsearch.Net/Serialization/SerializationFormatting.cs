namespace Elasticsearch.Net
{
	/// <summary>
	/// A hint to <see cref="IElasticsearchSerializer"/> how to format the json.
	/// Implementation of <see cref="IElasticsearchSerializer"/> might choose to ignore this hint though.
	/// </summary>
	public enum SerializationFormatting
	{
		None,
		Indented
	}
}
