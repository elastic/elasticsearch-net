using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Domain;
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
			public KeyValuePair<string, SearchDescriptorBase> Descriptor { get; set; }
		}

		private readonly MultiSearchDescriptor _descriptor;

		private static MethodInfo MakeDelegateMethodInfo = typeof(MultiSearchConverter).GetMethod("CreateMultiHit", BindingFlags.Static | BindingFlags.NonPublic);
		private readonly IConnectionSettingsValues _settings;

		internal MultiSearchConverter()
		{
			
		}

		public MultiSearchConverter(IConnectionSettingsValues settings, MultiSearchDescriptor descriptor)
		{
			this._settings = settings;
			_descriptor = descriptor;
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
			try
			{

			var hit = new QueryResponse<T>();
			var reader = tuple.Hit.CreateReader();
			serializer.Populate(reader, hit);

			var errorProperty = tuple.Hit.Children<JProperty>().FirstOrDefault(c=>c.Name == "error");
			if (errorProperty != null)
			{
				hit.IsValid = false;
				hit.ConnectionStatus = new NestElasticsearchResponse(settings, new ConnectionException(
					msg: errorProperty.Value.ToString(),
					response: errorProperty.Value.ToString()
				));
			}

			collection.Add(tuple.Descriptor.Key, hit);
			}
			catch (Exception e)
			{

				throw;
			}

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (this._settings == null)
			{
				var elasticContractResolver = serializer.ContractResolver as ElasticContractResolver;
				if (elasticContractResolver == null)
					return new MultiSearchResponse { IsValid = false };
				var piggyBackState = elasticContractResolver.PiggyBackState;
				if (piggyBackState == null || piggyBackState.ActualJsonConverter == null)
					return new MultiSearchResponse { IsValid = false };

				var realConverter = piggyBackState.ActualJsonConverter as MultiSearchConverter;
				if (realConverter == null)
					return new MultiSearchResponse { IsValid = false };

				return realConverter.ReadJson(reader, objectType, existingValue, serializer);
			}


			var response = new MultiSearchResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["responses"];
			if (docsJarray == null)
				return response;
			var multiSearchDescriptor = this._descriptor;
			if (this._descriptor == null)
				return multiSearchDescriptor;

			var withMeta = docsJarray.Zip(this._descriptor._Operations, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			var originalResolver = serializer.ContractResolver;
			foreach (var m in withMeta)
			{
				var descriptor = m.Descriptor.Value;
				var concreteTypeSelector = descriptor._ConcreteTypeSelector;
				var baseType = m.Descriptor.Value._ClrType;
				var types = m.Descriptor.Value._Types.EmptyIfNull().ToList();

				//if we dont already have a concrete type converter but we have selected more types then
				//just the base return type automagically create our own concrete type converter
				if (concreteTypeSelector == null
					&& types.HasAny() 
					&& types.Count() > types.Count(x => x.Type == baseType))
				{
					var inferrer = new ElasticInferrer(this._settings);
					var typeDict = types.ToDictionary(inferrer.TypeName, t => t.Type);
					concreteTypeSelector = (o, h) =>
					{
						Type t;
						return !typeDict.TryGetValue(h.Type, out t) ? baseType : t;
					};
						
				}
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
