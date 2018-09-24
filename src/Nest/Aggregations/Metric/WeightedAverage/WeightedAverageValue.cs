using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<WeightedAverageAggregation>))]
	public interface IWeightedAverageValue
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty("missing")]
		double? Missing { get; set; }
	}

	public class WeightedAverageValue : IWeightedAverageValue
	{
		public WeightedAverageValue(Field field) => this.Field = field;
		public WeightedAverageValue(IScript script) => this.Script = script;

		public Field Field { get; set; }
		public virtual IScript Script { get; set; }
		public double? Missing { get; set; }
	}

	public class WeightedAverageValueDescriptor<T> : DescriptorBase<WeightedAverageValueDescriptor<T>, IWeightedAverageValue>
			, IWeightedAverageValue
		where T : class
	{
		Field IWeightedAverageValue.Field { get; set; }
		IScript IWeightedAverageValue.Script { get; set; }
		double? IWeightedAverageValue.Missing { get; set; }

		public WeightedAverageValueDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public WeightedAverageValueDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public virtual WeightedAverageValueDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public virtual WeightedAverageValueDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public WeightedAverageValueDescriptor<T> Missing(double? missing) => Assign(a => a.Missing = missing);
	}

}
