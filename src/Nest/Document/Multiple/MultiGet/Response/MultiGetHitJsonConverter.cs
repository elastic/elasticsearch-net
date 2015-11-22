using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Nest.Resolvers;

namespace Nest
{
	internal class MultiGetHitJsonConverter : JsonConverter
	{
		private class MultiHitTuple
		{
			public JToken Hit { get; set; }
			public IMultiGetOperation Descriptor { get; set; }
		}

		private readonly IMultiGetRequest _request;

		private static MethodInfo MakeDelegateMethodInfo = typeof(MultiGetHitJsonConverter).GetMethod("CreateMultiHit", BindingFlags.Static | BindingFlags.NonPublic);
		
		internal MultiGetHitJsonConverter()
		{
			
		}

		public MultiGetHitJsonConverter(IMultiGetRequest request)
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
				var realConverter = (
					(serializer.ContractResolver as SettingsContractResolver)
					?.PiggyBackState?.ActualJsonConverter as MultiGetHitJsonConverter
				);
				if (realConverter == null)
					throw new DslException("could not find a stateful multigethit converter");

				return realConverter.ReadJson(reader, objectType, existingValue, serializer);
			}

			var response = new MultiGetResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["docs"];
			var multiGetDescriptor = this._request;
			if (this._request == null || docsJarray == null)
				return response;

			var withMeta = docsJarray.Zip(this._request.Documents, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
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