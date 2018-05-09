using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An input to load the results of an Elasticsearch search request into the execution
	/// context when a watch is triggered.
	/// </summary>
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchInput>))]
	public interface ISearchInput : IInput
	{
		/// <summary>
		/// A array of json keys to extract from the search response and load as the payload.
		/// When a search generates a large response, you can use extract
		/// to select the relevant fields instead of loading the entire response
		/// </summary>
		[JsonProperty("extract")]
		IEnumerable<string> Extract { get; set; }

		/// <summary>
		/// The search request details
		/// </summary>
		[JsonProperty("request")]
		ISearchInputRequest Request { get; set; }

		/// <summary>
		/// The timeout for waiting for the search api call to return.
		/// If no response is returned within this time, the search input times out and fails.
		/// This setting overrides the default search operations timeouts.
		/// </summary>
		[JsonProperty("timeout")]
		Time Timeout { get; set; }
	}

	/// <inheritdoc cref="ISearchInput"/>
	public class SearchInput : InputBase, ISearchInput
	{
		/// <inheritdoc />
		public IEnumerable<string> Extract { get; set; }

		/// <inheritdoc />
		public ISearchInputRequest Request { get; set; }

		/// <inheritdoc />
		public Time Timeout { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Search = this;
	}

	/// <inheritdoc cref="ISearchInput"/>
	public class SearchInputDescriptor
		: DescriptorBase<SearchInputDescriptor, ISearchInput>, ISearchInput
	{
		IEnumerable<string> ISearchInput.Extract { get; set; }
		ISearchInputRequest ISearchInput.Request { get; set; }
		Time ISearchInput.Timeout { get; set; }

		/// <inheritdoc cref="ISearchInput.Request"/>
		public SearchInputDescriptor Request(Func<SearchInputRequestDescriptor, ISearchInputRequest> selector) =>
			Assign(a => a.Request = selector?.InvokeOrDefault(new SearchInputRequestDescriptor()));

		/// <inheritdoc cref="ISearchInput.Extract"/>
		public SearchInputDescriptor Extract(IEnumerable<string> extract) => Assign(a => a.Extract = extract);

		/// <inheritdoc cref="ISearchInput.Extract"/>
		public SearchInputDescriptor Extract(params string[] extract) => Assign(a => a.Extract = extract);

		/// <inheritdoc cref="ISearchInput.Timeout"/>
		public SearchInputDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);
	}
}
