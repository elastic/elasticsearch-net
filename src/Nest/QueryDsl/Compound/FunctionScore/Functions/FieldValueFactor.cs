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
	public class FieldValueFactor<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "field_value_factor")]
		internal FieldValueFactorDescriptor<T> _FieldValueFactor { get; set; }

		public FieldValueFactor(Func<FieldValueFactorDescriptor<T>, FieldValueFactorDescriptor<T>> descriptorBuilder)
		{
			var descriptor = descriptorBuilder(new FieldValueFactorDescriptor<T>());
			if (descriptor._Field.IsConditionless())
				throw new DslException("Field name not set for field value factor function");
			this._FieldValueFactor = descriptor;
		}
	}

	public class FieldValueFactorDescriptor<T>
	{
		[JsonProperty("field")]
		internal Field _Field { get; set; }

		[JsonProperty("factor")]
		internal double? _Factor { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal FieldValueFactorModifier? _Modifier { get; set; }

		[JsonProperty("missing")]
		internal double? _Missing { get; set; }

		[JsonProperty("default")]
		internal object _Default { get; set; }

		public FieldValueFactorDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		public FieldValueFactorDescriptor<T> Default(object defaultValue)
		{
			this._Default = defaultValue;
			return this;
		}

		public FieldValueFactorDescriptor<T> Factor(double factor)
		{
			this._Factor = factor;
			return this;
		}

		public FieldValueFactorDescriptor<T> Modifier(FieldValueFactorModifier modifier)
		{
			this._Modifier = modifier;
			return this;
		}

		public FieldValueFactorDescriptor<T> Missing(double missing)
		{
			this._Missing = missing;
			return this;
		}
	}
}
