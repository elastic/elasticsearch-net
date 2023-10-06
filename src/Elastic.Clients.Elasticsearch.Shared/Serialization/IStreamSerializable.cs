// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Transport;
using System.IO;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

/// <summary>
/// Used to mark types which expect to directly serialize into a stream. This supports non-json compliant output such as NDJSON.
/// </summary>
internal interface IStreamSerializable
{
	/// <summary>
	/// Serialize the object into the supplied <see cref="Stream"/>.
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="settings"></param>
	/// <param name="formatting"></param>
	void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

	/// <summary>
	/// Asynchronously serialize the object into the supplied <see cref="Stream"/>.
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="settings"></param>
	/// <param name="formatting"></param>
	/// <returns></returns>
	Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);
}
