// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
