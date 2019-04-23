using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<SearchInputRequest>))]
	public interface ISearchInputRequest
	{
		[JsonProperty("body")]
		ISearchRequest Body { get; set; }

		[JsonProperty("indices")]
		IEnumerable<IndexName> Indices { get; set; }

		[JsonProperty("indices_options")]
		IIndicesOptions IndicesOptions { get; set; }

		[JsonProperty("search_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		SearchType? SearchType { get; set; }

		[JsonProperty("template")]
		ISearchTemplateRequest Template { get; set; }

		[JsonProperty("types")]
		IEnumerable<TypeName> Types { get; set; }
	}

	public class SearchInputRequest : ISearchInputRequest
	{
		public ISearchRequest Body { get; set; }
		public IEnumerable<IndexName> Indices { get; set; }

		public IIndicesOptions IndicesOptions { get; set; }

		public SearchType? SearchType { get; set; }

		public ISearchTemplateRequest Template { get; set; }

		public IEnumerable<TypeName> Types { get; set; }
	}

	public class SearchInputRequestDescriptor : DescriptorBase<SearchInputRequestDescriptor, ISearchInputRequest>, ISearchInputRequest
	{
		ISearchRequest ISearchInputRequest.Body { get; set; }
		IEnumerable<IndexName> ISearchInputRequest.Indices { get; set; }
		IIndicesOptions ISearchInputRequest.IndicesOptions { get; set; }
		SearchType? ISearchInputRequest.SearchType { get; set; }
		ISearchTemplateRequest ISearchInputRequest.Template { get; set; }
		IEnumerable<TypeName> ISearchInputRequest.Types { get; set; }

		public SearchInputRequestDescriptor Indices(IEnumerable<IndexName> indices) =>
			Assign(indices, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor Indices(params IndexName[] indices) =>
			Assign(indices, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor Indices<T>() =>
			Assign(new[] { (IndexName)typeof(T) }, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor Types(IEnumerable<TypeName> types) =>
			Assign(types, (a, v) => a.Types = v);

		public SearchInputRequestDescriptor Types(params TypeName[] types) =>
			Assign(types, (a, v) => a.Types = v);

		public SearchInputRequestDescriptor Types<T>() =>
			Assign(new[] { (TypeName)typeof(T) }, (a, v) => a.Types = v);

		public SearchInputRequestDescriptor SearchType(SearchType? searchType) =>
			Assign(searchType, (a, v) => a.SearchType = v);

		public SearchInputRequestDescriptor IndicesOptions(Func<IndicesOptionsDescriptor, IIndicesOptions> selector) =>
			Assign(selector(new IndicesOptionsDescriptor()), (a, v) => a.IndicesOptions = v);

		public SearchInputRequestDescriptor Body<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(selector, (a, v) => a.Body = v?.InvokeOrDefault(new SearchDescriptor<T>()));

		public SearchInputRequestDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			Assign(selector, (a, v) => a.Template = v?.InvokeOrDefault(new SearchTemplateDescriptor<T>()));
	}
}
