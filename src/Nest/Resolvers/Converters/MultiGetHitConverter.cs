using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Nest.Resolvers.Converters
{

	public class MultiGetHitConverter : JsonConverter
	{
		private class MultiHitTuple
		{
			public JToken Hit { get; set; }
			public IMultiGetOperation Descriptor { get; set; }
		}

		private readonly IMultiGetRequest _request;

		private static MethodInfo MakeDelegateMethodInfo = typeof(MultiGetHitConverter).GetMethod("CreateMultiHit", BindingFlags.Static | BindingFlags.NonPublic);
		
		internal MultiGetHitConverter()
		{
			
		}

		public MultiGetHitConverter(IMultiGetRequest request)
		{
			_request = request;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
		private static void CreateMultiHit<T>(MultiHitTuple tuple, JsonSerializer serializer, ICollection<IMultiGetHit<object>> collection) where T : class
		{
			var hit = new MultiGetHit<T>();
			var reader = tuple.Hit.CreateReader();	
			serializer.Populate(reader, hit);

			var contract = serializer.ContractResolver as SettingsContractResolver;
			var settings = contract.ConnectionSettings;
			var f = new FieldSelection<T>(settings);
			var source = tuple.Hit["fields"];
			if (source != null)
			{
				((IFieldSelection<T>)f).FieldValuesDictionary = serializer.Deserialize<Dictionary<string, object>>( source.CreateReader());
				hit.FieldSelection = f;
			}

			collection.Add(hit);

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (this._request == null)
			{
				var elasticContractResolver = serializer.ContractResolver as SettingsContractResolver;
				if (elasticContractResolver == null)
					return new MultiGetResponse { IsValid = false };
				var piggyBackState = elasticContractResolver.PiggyBackState;
				if (piggyBackState == null || piggyBackState.ActualJsonConverter == null)
					return new MultiGetResponse { IsValid = false };

				var realConverter = piggyBackState.ActualJsonConverter as MultiGetHitConverter;
				if (realConverter == null)
					return new MultiGetResponse { IsValid = false };

				return realConverter.ReadJson(reader, objectType, existingValue, serializer);
			}

			var response = new MultiGetResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["docs"];
			var multiGetDescriptor = this._request;
			if (this._request == null)
				return multiGetDescriptor;

			var withMeta = docsJarray.Zip(this._request.GetOperations, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			foreach (var m in withMeta)
			{
				var generic = MakeDelegateMethodInfo.MakeGenericMethod(m.Descriptor.ClrType);
				generic.Invoke(null, new object[] { m, serializer, response._Documents });
			}

			return response;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}