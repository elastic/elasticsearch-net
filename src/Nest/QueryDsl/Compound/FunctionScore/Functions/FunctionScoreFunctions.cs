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

		public FunctionScoreFunction<T> Gauss(
			string field, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new GaussFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Gauss(
			Expression<Func<T, object>> objectPath,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new GaussFunction<T>(objectPath, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(
			string field,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new LinearFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(
			Expression<Func<T, object>> objectPath,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new LinearFunction<T>(objectPath, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(
			string field,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new ExpFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(
			Expression<Func<T, object>> objectPath, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new ExpFunction<T>(objectPath, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> BoostFactor(double value)
		{
			var fn = new BoostFactorFunction<T>(value);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> ScriptScore(Func<ScriptQueryDescriptor<T>, IScriptQuery> scriptSelector)
		{
			var fn = new ScriptScoreFunction<T>(scriptSelector);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> FieldValueFactor(Func<FieldValueFactorDescriptor<T>, FieldValueFactorDescriptor<T>> db)
		{
			var fn = new FieldValueFactor<T>(db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Weight(double weight)
		{
			var fn = new WeightFunction<T>(weight);
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
}