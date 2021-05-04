// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(Rescore))]
	public interface IRescore
	{
		[DataMember(Name ="query")]
		IRescoreQuery Query { get; set; }

		[DataMember(Name ="window_size")]
		int? WindowSize { get; set; }
	}

	public class Rescore : IRescore
	{
		public IRescoreQuery Query { get; set; }
		public int? WindowSize { get; set; }
	}

	public class RescoringDescriptor<T> : DescriptorPromiseBase<RescoringDescriptor<T>, IList<IRescore>>
		where T : class
	{
		public RescoringDescriptor() : base(new List<IRescore>()) { }

		public RescoringDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> selector) =>
			AddRescore(selector?.Invoke(new RescoreDescriptor<T>()));

		private RescoringDescriptor<T> AddRescore(IRescore rescore) => rescore == null ? this : Assign(rescore, (a, v) => a.Add(v));
	}

	public class RescoreDescriptor<T> : DescriptorBase<RescoreDescriptor<T>, IRescore>, IRescore
		where T : class
	{
		IRescoreQuery IRescore.Query { get; set; }
		int? IRescore.WindowSize { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, IRescoreQuery> rescoreQuerySelector) =>
			Assign(rescoreQuerySelector, (a, v) => a.Query = v?.Invoke(new RescoreQueryDescriptor<T>()));

		public virtual RescoreDescriptor<T> WindowSize(int? windowSize) => Assign(windowSize, (a, v) => a.WindowSize = v);
	}
}
