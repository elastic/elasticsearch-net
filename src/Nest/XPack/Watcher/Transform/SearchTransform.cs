using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SearchTransform))]
	public interface ISearchTransform : ITransform
	{
		[DataMember(Name ="request")]
		ISearchInputRequest Request { get; set; }

		[DataMember(Name ="timeout")]
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
			Assign(selector.InvokeOrDefault(new SearchInputRequestDescriptor()), (a, v) => a.Request = v);

		public SearchTransformDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);
	}
}
