using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchInputRequest>))]
	public interface ISearchInputRequest
	{
		[JsonProperty("indices")]
		IEnumerable<IndexName> Indices { get; set; }

		[JsonProperty("types")]
		IEnumerable<TypeName> Types { get; set; }

		[JsonProperty("search_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		SearchType? SearchType { get; set; }

		[JsonProperty("indices_options")]
		IIndicesOptions IndicesOptions { get; set; }

		[JsonProperty("body")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchRequest>))]
		ISearchRequest Body { get; set; }

		[JsonProperty("template")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchTemplateRequest>))]
		ISearchTemplateRequest Template { get; set; }
	}

	public class SearchInputRequest : ISearchInputRequest
	{
		public IEnumerable<IndexName> Indices { get; set; }

		public IEnumerable<TypeName> Types { get; set; }

		public SearchType? SearchType { get; set; }

		public IIndicesOptions IndicesOptions { get; set; }

		public ISearchRequest Body { get; set; }

		public ISearchTemplateRequest Template { get; set; }
	}

	public class SearchInputRequestDescriptor :
		DescriptorBase<SearchInputRequestDescriptor, ISearchInputRequest>, ISearchInputRequest
	{
		IEnumerable<IndexName> ISearchInputRequest.Indices { get; set; }
		IEnumerable<TypeName> ISearchInputRequest.Types { get; set; }
		SearchType? ISearchInputRequest.SearchType { get; set; }
		IIndicesOptions ISearchInputRequest.IndicesOptions { get; set; }
		ISearchRequest ISearchInputRequest.Body { get; set; }
		ISearchTemplateRequest ISearchInputRequest.Template { get; set; }

		public SearchInputRequestDescriptor Indices(IEnumerable<IndexName> indices) =>
			Assign(a => a.Indices = indices);

		public SearchInputRequestDescriptor Indices(params IndexName[] indices) =>
			Assign(a => a.Indices = indices);

		public SearchInputRequestDescriptor Indices<T>() =>
			Assign(a => a.Indices = new [] { (IndexName)typeof(T) });

		public SearchInputRequestDescriptor Types(IEnumerable<TypeName> types) =>
			Assign(a => a.Types = types);

		public SearchInputRequestDescriptor Types(params TypeName[] types) =>
			Assign(a => a.Types = types);

		public SearchInputRequestDescriptor Types<T>() =>
			Assign(a => a.Types = new[] { (TypeName)typeof(T) });

		public SearchInputRequestDescriptor SearchType(SearchType? searchType) =>
			Assign(a => a.SearchType = searchType);

		public SearchInputRequestDescriptor IndicesOptions(Func<IndicesOptionsDescriptor, IIndicesOptions> selector) =>
			Assign(a => a.IndicesOptions = selector(new IndicesOptionsDescriptor()));

		public SearchInputRequestDescriptor Body<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(a => a.Body = selector?.InvokeOrDefault(new SearchDescriptor<T>()));

		public SearchInputRequestDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			Assign(a => a.Template = selector?.InvokeOrDefault(new SearchTemplateDescriptor<T>()));
	}
}
