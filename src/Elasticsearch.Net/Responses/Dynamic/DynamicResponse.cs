using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class DynamicResponse : ElasticsearchResponse<dynamic>
	{
		public DynamicResponse() { }

		public DynamicResponse(DynamicDictionary dictionary)
		{
			Body = dictionary;
			Dictionary = dictionary;
		}

		public DynamicDictionary Dictionary { get; }

		/// <summary>
		/// Traverses data using path notation.
		/// <para><c>e.g some.deep.nested.json.path</c></para>
		/// <para> A special lookup is available for ANY key using <c>_arbitrary_key_</c> <c>e.g some.deep._arbitrary_key_.json.path</c> which will traverse into the first key</para>
		/// </summary>
		/// <param name="path">path into the stored object, keys are separated with a dot and the last key is returned as T</param>
		/// <typeparam name="T"></typeparam>
		/// <returns>T or default</returns>
		public T Get<T>(string path) => Dictionary.Get<T>(path);
	}
}
