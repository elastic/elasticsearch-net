using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFieldValueFactor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("factor")]
		double? Factor { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof(StringEnumConverter))]
		FieldValueFactorModifier? Modifier { get; set; }

		[JsonProperty("missing")]
		double? Missing { get; set; }

		[JsonProperty("default")]
		object Default { get; set; }
	}


	public class FieldValueFactor : IFieldValueFactor
	{
		public Field Field { get; set; }

		public double? Factor { get; set; }

		public FieldValueFactorModifier? Modifier { get; set; }

		public double? Missing { get; set; }

		public object Default { get; set; }
	}

	public class FieldValueFactorDescriptor<T> : DescriptorBase<FieldValueFactorDescriptor<T>, IFieldValueFactor>, IFieldValueFactor
	{
		Field IFieldValueFactor.Field { get; set; }
		double? IFieldValueFactor.Factor { get; set; }
		FieldValueFactorModifier? IFieldValueFactor.Modifier { get; set; }
		double? IFieldValueFactor.Missing { get; set; }
		object IFieldValueFactor.Default { get; set; }

		public FieldValueFactorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);
		public FieldValueFactorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public FieldValueFactorDescriptor<T> Default(object defaultValue) => Assign(a => a.Default = defaultValue);

		public FieldValueFactorDescriptor<T> Factor(double? factor) => Assign(a => a.Factor = factor);

		public FieldValueFactorDescriptor<T> Modifier(FieldValueFactorModifier? modifier) => Assign(a => a.Modifier = modifier);

		public FieldValueFactorDescriptor<T> Missing(double? missing) => Assign(a => a.Missing = missing);
	}
}
