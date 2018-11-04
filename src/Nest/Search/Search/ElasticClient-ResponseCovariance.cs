using System;
using System.IO;
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
			d.RequestParameters.DeserializationOverride = CreateSearchDeserializer<T, TResult, TRequest, TRequestParameters>(d);
			;
			return d;
		}

		private Func<IApiCallDetails, Stream, SearchResponse<TResult>> CreateSearchDeserializer<T, TResult, TRequest, TRequestParameters>(
			TRequest request
		)
			where T : class
			where TResult : class
			where TRequest : IRequest<TRequestParameters>, ICovariantSearchRequest
			where TRequestParameters : IRequestParameters, new()
		{
			CovariantSearch.CloseOverAutomagicCovariantResultSelector(Infer, request);
			if (request.TypeSelector == null) return null;

			return (r, s) => FieldsSearchDeserializer<T, TResult, TRequest, TRequestParameters>(r, s, request);
		}

		private SearchResponse<TResult> FieldsSearchDeserializer<T, TResult, TRequest, TRequestParameters>(IApiCallDetails response, Stream stream,
			TRequest d
		)
			where T : class
			where TResult : class
			where TRequest : IRequest<TRequestParameters>, ICovariantSearchRequest
			where TRequestParameters : IRequestParameters, new() =>
			!response.Success
				? null
				: ConnectionSettings.StatefulSerializer(new ConcreteTypeConverter<TResult>(d.TypeSelector))
					.Deserialize<SearchResponse<TResult>>(stream);
	}
}
