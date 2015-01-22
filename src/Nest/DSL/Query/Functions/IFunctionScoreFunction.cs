using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FunctionScoreFunction<object>>))]
	public interface IFunctionScoreFunction
	{
		FilterContainer Filter { get; set; }
		float? Weight { get; set; }
	}
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFunction<T> : IFunctionScoreFunction
		where T : class
	{
		IFunctionScoreFunction Self { get { return this; } }
		
		[JsonProperty("filter")]
		FilterContainer IFunctionScoreFunction.Filter { get; set; }

		[JsonProperty("weight")]
		float? IFunctionScoreFunction.Weight { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this.Self.Filter = f;
			return this;
		}

		public FunctionScoreFunction<T> Weight(float weight)
		{
			this.Self.Weight = weight;
			return this;
		}
	}
}