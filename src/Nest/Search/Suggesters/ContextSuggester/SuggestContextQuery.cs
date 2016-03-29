using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISuggestContextQuery
	{
		[JsonProperty("context")]
		Union<string, GeoLocation> Context { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	public abstract class SuggestContextQueryBase : ISuggestContextQuery
	{
		public double? Boost { get; set; }

		public Union<string, GeoLocation> Context { get; set; }
	}

	public abstract class SuggestContextQueryDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ISuggestContextQuery
		where TDescriptor : SuggestContextQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggestContextQuery
		where TInterface : class, ISuggestContextQuery
	{
		double? ISuggestContextQuery.Boost { get; set; }
		Union<string, GeoLocation> ISuggestContextQuery.Context { get; set; }

		public TDescriptor Boost(double boost) => Assign(a => a.Boost = boost);

		public TDescriptor Context(string context) => Assign(a => a.Context = context);

		public TDescriptor Context(GeoLocation context) => Assign(a => a.Context = context);
	}
}
