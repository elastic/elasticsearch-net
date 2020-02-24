using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class MultiGetHitJsonConverter : JsonConverter
	{
		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiGetHitJsonConverter).GetMethod(nameof(CreateMultiHit), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IMultiGetRequest _request;

		internal MultiGetHitJsonConverter() { }

		public MultiGetHitJsonConverter(IMultiGetRequest request) => _request = request;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		private static void CreateMultiHit<T>(MultiHitTuple tuple, JsonSerializer serializer, ICollection<IMultiGetHit<object>> collection)
			where T : class
		{
			var hit = tuple.Hit.ToObject<MultiGetHit<T>>(serializer);
			var settings = serializer.GetConnectionSettings();
			var s = serializer.GetConnectionSettings().SourceSerializer;
			var r = serializer.GetConnectionSettings().RequestResponseSerializer;

			if (tuple.Hit["fields"] is JObject fields)
			{
				var fieldsDictionary = fields.Properties().ToDictionary(p => p.Name, p => new LazyDocument(p.Value, s, r));
				hit.Fields = new FieldValues(settings.Inferrer, fieldsDictionary);
			}

			collection.Add(hit);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (_request == null)
			{
				var realConverter = serializer.GetStatefulConverter<MultiGetHitJsonConverter>();
				return realConverter.ReadJson(reader, objectType, existingValue, serializer);
			}

			var response = new MultiGetResponse();

			var dateParseHandling = reader.DateParseHandling;
			reader.DateParseHandling = DateParseHandling.None;
			var jsonObject = JObject.Load(reader);
			reader.DateParseHandling = dateParseHandling;

			var docsJarray = (JArray)jsonObject["docs"];
			if (_request == null || docsJarray == null)
				return response;

			var withMeta = docsJarray.Zip(_request.Documents, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			foreach (var m in withMeta)
			{
				var cachedDelegate = serializer.GetConnectionSettings()
					.Inferrer.CreateMultiHitDelegates.GetOrAdd(m.Descriptor.ClrType, t =>
					{
						// Compile a delegate from an expression
						var methodInfo = MakeDelegateMethodInfo.MakeGenericMethod(t);
						var tupleParameter = Expression.Parameter(typeof(MultiHitTuple), "tuple");
						var serializerParameter = Expression.Parameter(typeof(JsonSerializer), "serializer");
						var multiHitCollection = Expression.Parameter(typeof(ICollection<IMultiGetHit<object>>), "collection");
						var parameterExpressions = new[] { tupleParameter, serializerParameter, multiHitCollection };
						var call = Expression.Call(null, methodInfo, parameterExpressions);
						var lambda =
							Expression.Lambda<Action<MultiHitTuple, JsonSerializer, ICollection<IMultiGetHit<object>>>>(call, parameterExpressions);
						return lambda.Compile();
					});

				cachedDelegate(m, serializer, response.InternalHits);
			}

			return response;
		}

		public override bool CanConvert(Type objectType) => true;

		internal class MultiHitTuple
		{
			public IMultiGetOperation Descriptor { get; set; }
			public JToken Hit { get; set; }
		}
	}
}
