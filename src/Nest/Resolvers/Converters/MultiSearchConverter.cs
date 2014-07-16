using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Nest.Resolvers.Converters
{

	public class MultiSearchConverter : JsonConverter
	{
		private class MultiHitTuple
		{
			public JToken Hit { get; set; }
			public KeyValuePair<string, ISearchRequest> Descriptor { get; set; }
		}

		private readonly IMultiSearchRequest _request;

		private static MethodInfo MakeDelegateMethodInfo = typeof(MultiSearchConverter).GetMethod("CreateMultiHit", BindingFlags.Static | BindingFlags.NonPublic);
		private readonly IConnectionSettingsValues _settings;

		internal MultiSearchConverter()
		{
			
		}

		public MultiSearchConverter(IConnectionSettingsValues settings, IMultiSearchRequest request)
		{
			this._settings = settings;
			_request = request;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
		
		private static void CreateMultiHit<T>(
			MultiHitTuple tuple, 
			JsonSerializer serializer, 
			IDictionary<string, object> collection, 
			IConnectionSettingsValues settings
		)
			where T : class
		{
			var hit = new SearchResponse<T>();
			var reader = tuple.Hit.CreateReader();
			serializer.Populate(reader, hit);

			var errorProperty = tuple.Hit.Children<JProperty>().FirstOrDefault(c=>c.Name == "error");
			if (errorProperty != null)
			{
				hit.IsValid = false;
				//TODO es 1.0 will return statuscode pass that into exception
				
			}

			collection.Add(tuple.Descriptor.Key, hit);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (this._settings == null)
			{
				var elasticContractResolver = serializer.ContractResolver as SettingsContractResolver;
				if (elasticContractResolver == null)
					return new MultiSearchResponse { IsValid = false };
				var piggyBackState = elasticContractResolver.PiggyBackState;
				if (piggyBackState == null || piggyBackState.ActualJsonConverter == null)
					return new MultiSearchResponse { IsValid = false };

				var realConverter = piggyBackState.ActualJsonConverter as MultiSearchConverter;
				if (realConverter == null)
					return new MultiSearchResponse { IsValid = false };

				var mr = realConverter.ReadJson(reader, objectType, existingValue, serializer) as MultiSearchResponse;
				return mr;
			}


			var response = new MultiSearchResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["responses"];
			if (docsJarray == null)
				return response;
			var multiSearchDescriptor = this._request;
			if (this._request == null)
				return multiSearchDescriptor;

			var withMeta = docsJarray.Zip(this._request.Operations, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			var originalResolver = serializer.ContractResolver;
			foreach (var m in withMeta)
			{
				var descriptor = m.Descriptor.Value;
				var concreteTypeSelector = descriptor.TypeSelector;
				var baseType = m.Descriptor.Value.ClrType;
				
				var generic = MakeDelegateMethodInfo.MakeGenericMethod(baseType);

				if (concreteTypeSelector != null)
				{
					var elasticSerializer = new NestSerializer(this._settings);
					var state = typeof(ConcreteTypeConverter<>).CreateGenericInstance(baseType, concreteTypeSelector) as JsonConverter;
					if (state != null)
					{
						var settings = elasticSerializer.CreateSettings(piggyBackJsonConverter: state);

						var jsonSerializer = new JsonSerializer()
						{
							NullValueHandling = settings.NullValueHandling,
							DefaultValueHandling = settings.DefaultValueHandling,
							ContractResolver = settings.ContractResolver,
						};
						foreach (var converter in settings.Converters.EmptyIfNull())
							jsonSerializer.Converters.Add(converter);
						generic.Invoke(null, new object[] { m, jsonSerializer, response._Responses, this._settings });
						continue;
					}
				}
				generic.Invoke(null, new object[] { m, serializer, response._Responses, this._settings });
			}
			
			return response;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(MultiSearchResponse);
		}
	}
}
