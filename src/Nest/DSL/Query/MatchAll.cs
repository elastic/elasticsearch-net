using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MatchAll>))]
	public interface IMatchAll : IQuery
	{
		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; }

		[JsonProperty(PropertyName = "norm_field")]
		string NormField { get; }
	}

	public class MatchAll : IMatchAll
	{
		public double? Boost { get; internal set; }

		public string NormField { get; internal set; }

		bool IQuery.IsConditionless { get { return false; } }

	}
}
