using System;
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

	public class RescoreDescriptor<T> : DescriptorBase<RescoreDescriptor<T>, IRescore>, IRescore 
		where T : class
	{
		int? IRescore.WindowSize { get; set; }
		IRescoreQuery IRescore.Query { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, IRescoreQuery> rescoreQuerySelector) =>
			Assign(a=>a.Query = rescoreQuerySelector?.Invoke(new RescoreQueryDescriptor<T>()));

		public virtual RescoreDescriptor<T> WindowSize(int? windowSize) => Assign(a => a.WindowSize = windowSize);
	
	}
}
