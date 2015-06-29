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
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _LinearDescriptor { get; set; }

		public LinearFunction(
			Expression<Func<T, object>> objectPath, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
			)
		{
			this._LinearDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>
			{
				[objectPath] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}

		public LinearFunction(
			string field, 
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector
			)
		{
			this._LinearDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>
			{
				[field] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}
	}
}