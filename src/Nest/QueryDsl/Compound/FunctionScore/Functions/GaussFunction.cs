using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GaussFunction<T> : FunctionScoreDecayFunction<T>
		where T : class
	{
		[JsonProperty(PropertyName = "gauss")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		internal IDictionary<Field, FunctionScoreDecayFieldDescriptor> _GaussDescriptor { get; set; }

		public GaussFunction(
			Expression<Func<T, object>> objectPath, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
		)
		{
			this._GaussDescriptor = new Dictionary<Field, FunctionScoreDecayFieldDescriptor>
			{
				[objectPath] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}

		public GaussFunction(
			string field, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
			)
		{
			this._GaussDescriptor = new Dictionary<Field, FunctionScoreDecayFieldDescriptor>
			{
				[field] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}
	}
}