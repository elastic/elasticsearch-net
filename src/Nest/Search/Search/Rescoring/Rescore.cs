using System;
using System.Collections.Generic;
using System.Linq;
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

	public class RescoreDescriptor<T> : IRescore where T : class
	{
		protected IRescore Self { get { return this;  } }

		int? IRescore.WindowSize { get; set; }

		IRescoreQuery IRescore.Query { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, RescoreQueryDescriptor<T>> rescoreQuerySelector)
		{
			rescoreQuerySelector.ThrowIfNull("rescoreQuerySelector");
			Self.Query = rescoreQuerySelector(new RescoreQueryDescriptor<T>());
			return this;
		}

		public virtual RescoreDescriptor<T> WindowSize(int windowSize)
		{
			Self.WindowSize = windowSize;
			return this;
		}

	
	}
}
