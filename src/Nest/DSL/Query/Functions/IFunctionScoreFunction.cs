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
		long? Weight { get; set; }

		/// <summary>
		/// This property is added for backwards compatibility reasons and will most likely
		/// be renamed to Weight when we release NEST 2.0
		/// </summary>
		double? WeightAsDouble { get; set; }
	}
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFunction<T> : IFunctionScoreFunction
		where T : class
	{
		IFunctionScoreFunction Self { get { return this; } }
		
		[JsonProperty("filter")]
		FilterContainer IFunctionScoreFunction.Filter { get; set; }

		long? IFunctionScoreFunction.Weight
		{
			get { return Convert.ToInt64(Self.WeightAsDouble); }
			set { Self.WeightAsDouble = value; }
		}

		[JsonProperty("weight")]
		double? IFunctionScoreFunction.WeightAsDouble { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this.Self.Filter = f;
			return this;
		}

		public FunctionScoreFunction<T> Weight(double weight)
		{
			this.Self.WeightAsDouble = weight;
			return this;
		}

		public FunctionScoreFunction<T> Weight(long weight)
		{
			this.Self.Weight = weight;
			return this;
		}
	}
}