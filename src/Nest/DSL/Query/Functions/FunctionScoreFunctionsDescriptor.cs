using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class FunctionScoreFunctionsDescriptor<T> : IEnumerable<FunctionScoreFunction<T>> where T : class
	{
		internal List<FunctionScoreFunction<T>> _Functions { get; set; }

		public FunctionScoreFunctionsDescriptor()
		{
			this._Functions = new List<FunctionScoreFunction<T>>();
		}

		public FunctionScoreFunction<T> Gauss(string field, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new GaussFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Gauss(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new GaussFunction<T>(objectPath, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(string field, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new LinearFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new LinearFunction<T>(objectPath, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(string field, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			var fn = new ExpFunction<T>(field, descriptorBuilder);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
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
}