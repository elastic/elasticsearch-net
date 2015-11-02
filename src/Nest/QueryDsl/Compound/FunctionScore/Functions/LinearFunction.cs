using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class LinearFunction<T> : FunctionScoreDecayFunction<T>
		where T : class
	{
		[JsonProperty(PropertyName = "linear")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		internal IDictionary<Field, FunctionScoreDecayFieldDescriptor> _LinearDescriptor { get; set; }

		public LinearFunction(
			Expression<Func<T, object>> objectPath, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
			)
		{
			this._LinearDescriptor = new Dictionary<Field, FunctionScoreDecayFieldDescriptor>
			{
				[objectPath] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}

		public LinearFunction(
			string field, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
			)
		{
			this._LinearDescriptor = new Dictionary<Field, FunctionScoreDecayFieldDescriptor>
			{
				[field] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}
	}
}