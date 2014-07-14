using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Serialization;
using Newtonsoft.Json;

namespace Nest
{

	//TODO It would be very nice if we can get rid of this interface
	public interface INestSerializer : IElasticsearchSerializer
	{
		string SerializeBulkDescriptor(IBulkRequest bulkRequest);

		string SerializeMultiSearch(IMultiSearchRequest multiSearchRequest);

		T DeserializeInternal<T>(Stream stream, JsonConverter converter);
	}
}
