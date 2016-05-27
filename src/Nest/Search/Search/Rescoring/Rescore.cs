using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Rescore>))]
	public interface IRescore
	{
		[JsonProperty("window_size")]
		int? WindowSize { get; set; }

		[JsonProperty("query")]
		IRescoreQuery Query { get; set; }
	}

	public class Rescore : IRescore
	{
		public int? WindowSize { get; set; }
		public IRescoreQuery Query { get; set; }
	}

	public class RescoringDescriptor<T> : DescriptorPromiseBase<RescoringDescriptor<T>, IList<IRescore>>
		where T : class
	{
		public RescoringDescriptor() : base(new List<IRescore>()) { }

		public RescoringDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> selector) =>
			AddRescore(selector?.Invoke(new RescoreDescriptor<T>()));

		private RescoringDescriptor<T> AddRescore(IRescore rescore) => rescore == null ? this : this.Assign(a => a.Add(rescore));
	}

	public class RescoreDescriptor<T> : DescriptorBase<RescoreDescriptor<T>, IRescore>, IRescore
		where T : class
	{
		int? IRescore.WindowSize { get; set; }

		IRescoreQuery IRescore.Query { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, IRescoreQuery> rescoreQuerySelector) =>
			Assign(a => a.Query = rescoreQuerySelector?.Invoke(new RescoreQueryDescriptor<T>()));

		public virtual RescoreDescriptor<T> WindowSize(int? windowSize) => Assign(a => a.WindowSize = windowSize);

	}
}
