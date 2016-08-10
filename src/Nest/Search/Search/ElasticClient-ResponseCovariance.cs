using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		private TRequest CovariantConverterWhenNeeded<T, TResult, TRequest, TRequestParameters>(RouteValues p, TRequest d)
			where T : class
			where TResult : class
			where TRequest : IRequest<TRequestParameters>, ICovariantSearchRequest
			where TRequestParameters : IRequestParameters, new()
		{
			d.RequestParameters.DeserializationOverride = this.CreateSearchDeserializer<T, TResult, TRequest, TRequestParameters>(d);;
			return d;
		}

		private Func<IApiCallDetails, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult, TRequest, TRequestParameters>(TRequest request)
			where T : class
			where TResult : class
			where TRequest : IRequest<TRequestParameters>, ICovariantSearchRequest
			where TRequestParameters : IRequestParameters, new()
		{
			CovariantSearch.CloseOverAutomagicCovariantResultSelector(this.Infer, request);
			if (request.TypeSelector == null) return null;
			return (r, s) => this.FieldsSearchDeserializer<T, TResult, TRequest, TRequestParameters>(r, s, request);
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult, TRequest, TRequestParameters>(IApiCallDetails response, Stream stream, TRequest d)
			where T : class
			where TResult : class
			where TRequest : IRequest<TRequestParameters>, ICovariantSearchRequest
			where TRequestParameters : IRequestParameters, new()
		{
			var converter = new ConcreteTypeConverter<TResult>(d.TypeSelector);
			var serializer = this.Serializer as JsonNetSerializer ?? new JsonNetSerializer(this.ConnectionSettings);
			serializer.Initialize(converter);
			return !response.Success
				? null
				: serializer.Deserialize<SearchResponse<TResult>>(stream);
		}
	}
}
