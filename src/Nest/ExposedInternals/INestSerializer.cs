using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface INestSerializer : IElasticsearchSerializer
	{
		IQueryResponse<TResult> DeserializeSearchResponse<T, TResult>(ElasticsearchResponse status, SearchDescriptor<T> originalSearchDescriptor)
			where TResult : class
			where T : class;

		string SerializeBulkDescriptor(BulkDescriptor bulkDescriptor);

		/// <summary>
		/// _msearch needs a specialized json format in the body
		/// </summary>
		string SerializeMultiSearch(MultiSearchDescriptor multiSearchDescriptor);

		TemplateResponse DeserializeTemplateResponse(ElasticsearchResponse c, GetTemplateDescriptor d);
		GetMappingResponse DeserializeGetMappingResponse(ElasticsearchResponse c);
		MultiGetResponse DeserializeMultiGetResponse(ElasticsearchResponse c, MultiGetDescriptor d);
		MultiSearchResponse DeserializeMultiSearchResponse(ElasticsearchResponse c, MultiSearchDescriptor d);
		WarmerResponse DeserializeWarmerResponse(ElasticsearchResponse connectionStatus, GetWarmerDescriptor getWarmerDescriptor);

		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		R ToParsedResponse<R>(
			ElasticsearchResponse status, 
			bool notFoundIsAValidResponse = false,
			JsonConverter piggyBackJsonConverter = null
			) where R : BaseResponse;
	}
}
