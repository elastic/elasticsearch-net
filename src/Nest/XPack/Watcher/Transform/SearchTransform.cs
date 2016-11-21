using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchTransform>))]
	public interface ISearchTransform : ITransform
	{
		[JsonProperty("request")]
		ISearchInputRequest Request { get; set; }

		[JsonProperty("timeout")]
		Time Timeout { get; set; }
	}

	public class SearchTransform : TransformBase, ISearchTransform
	{
		public ISearchInputRequest Request { get; set; }
		public Time Timeout { get; set; }

		internal override void WrapInContainer(ITransformContainer container) => container.Search = this;
	}

	public class SearchTransformDescriptor : DescriptorBase<SearchTransformDescriptor, ISearchTransform>, ISearchTransform
	{
		ISearchInputRequest ISearchTransform.Request { get; set; }
		Time ISearchTransform.Timeout { get; set; }

		public SearchTransformDescriptor Request(Func<SearchInputRequestDescriptor, ISearchInputRequest> selector) =>
			Assign(a => a.Request = selector.InvokeOrDefault(new SearchInputRequestDescriptor()));

		public SearchTransformDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);
	}
}
