using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IConnection
	{

		Task<ElasticsearchResponse<T>> Get<T>(Uri uri, object deserializationState = null);
		ElasticsearchResponse<T> GetSync<T>(Uri uri, object deserializationState = null);

		Task<ElasticsearchResponse<T>> Head<T>(Uri uri, object deserializationState = null);
		ElasticsearchResponse<T> HeadSync<T>(Uri uri, object deserializationState = null);

		Task<ElasticsearchResponse<T>> Post<T>(Uri uri, byte[] data, object deserializationState = null);
		ElasticsearchResponse<T> PostSync<T>(Uri uri, byte[] data, object deserializationState = null);

		Task<ElasticsearchResponse<T>> Put<T>(Uri uri, byte[] data, object deserializationState = null);
		ElasticsearchResponse<T> PutSync<T>(Uri uri, byte[] data, object deserializationState = null);

		Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, object deserializationState = null);
		ElasticsearchResponse<T> DeleteSync<T>(Uri uri, object deserializationState = null);

		Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, byte[] data, object deserializationState = null);
		ElasticsearchResponse<T> DeleteSync<T>(Uri uri, byte[] data, object deserializationState = null);

		bool Ping(Uri uri);
		IList<Uri> Sniff(Uri uri);

	}
}
