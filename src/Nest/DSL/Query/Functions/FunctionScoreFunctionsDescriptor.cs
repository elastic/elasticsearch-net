using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class FunctionScoreFunctionsDescriptor<T> : IEnumerable<FunctionScoreFunction<T>> where T : class
	{
		internal List<FunctionScoreFunction<T>> _Functions { get; set; }

		public FunctionScoreFunctionsDescriptor()
		{
			this._Functions = new List<FunctionScoreFunction<T>>();
		}

		public FunctionScoreFunction<T> Gauss(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new GaussFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new LinearFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new ExpFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> BoostFactor(double value)
		{
			var fn = new BoostFactorFunction<T>(value);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> ScriptScore(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var fn = new ScriptScoreFunction<T>(scriptSelector);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> FieldValueFactor(Action<FieldValueFactorDescriptor<T>> db)
		{
			var fn = new FieldValueFactor<T>(db);
			this._Functions.Add(fn);
			return fn;
		}
		public IEnumerator<FunctionScoreFunction<T>> GetEnumerator()
		{
			return _Functions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Functions.GetEnumerator();
		}
	}


	public class FieldValueFactorDescriptor<T>
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty("factor")]
		internal double? _Factor { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof (StringEnumConverter))]
		internal FieldValueFactorModifier? _Modifier { get; set; }

		public FieldValueFactorDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
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
	}
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FieldValueFactor<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "field_value_factor")]
		internal FieldValueFactorDescriptor<T> _FieldValueFactor { get; set; }

		public FieldValueFactor(Action<FieldValueFactorDescriptor<T>> descriptorBuilder)
		{
			var descriptor = new FieldValueFactorDescriptor<T>();
			descriptorBuilder(descriptor);
			if (descriptor._Field.IsConditionless())
				throw new DslException("Field name not set for field value factor function");

			this._FieldValueFactor = descriptor;
		}
	}
}