using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class NestSerializer : INestSerializer
	{
		private readonly IConnectionSettingsValues _settings;
		private readonly JsonSerializerSettings _serializationSettings;
		private readonly ElasticInferrer _infer;

		public NestSerializer(IConnectionSettingsValues settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
			this._infer = new ElasticInferrer(this._settings);
		}

		public virtual byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var format = formatting == SerializationFormatting.None ? Formatting.None : Formatting.Indented;
			var serialized = JsonConvert.SerializeObject(data, format, this._serializationSettings);
			return serialized.Utf8Bytes();
		}

		public string Stringify(object valueType)
		{
			if (valueType == null)
				return null;
			var s = valueType as string;
			if (s != null)
				return s;
			var ss = valueType as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pns = valueType as IEnumerable<object>;
			if (pns != null)
				return string.Join(",", pns.Select(
					oo =>
					{
						if (oo is PropertyNameMarker)
							return this._infer.PropertyName(oo as PropertyNameMarker);
						if (oo is PropertyPathMarker)
							return this._infer.PropertyPath(oo as PropertyPathMarker);
						return oo.ToString();
					})
				);

			var e = valueType as Enum;
			if (e != null) return KnownEnums.Resolve(e);
			if (valueType is bool)
				return ((bool)valueType) ? "true" : "false";

			var pn = valueType as PropertyNameMarker;
			if (pn != null)
				return this._infer.PropertyName(pn);

			var pp = valueType as PropertyPathMarker;
			if (pp != null)
				return this._infer.PropertyPath(pp);

			return valueType.ToString();
		}

		/// <summary>
		/// Deserialize an object 
		/// </summary>
		/// <typeparam name="T">The type you want to deserialize too</typeparam>
		/// <param name="response">If the type you want is a Nest Response you have to pass a response object</param>
		/// <param name="stream">The stream to deserialize off</param>
		/// <param name="deserializationState">Optional deserialization state</param>
		public virtual T Deserialize<T>(Stream stream)
		{
			var settings = this._serializationSettings;

			return _Deserialize<T>(stream, settings);
		}

		/// <summary>
		/// Deserialize to type T bypassing checks for custom deserialization state and or BaseResponse return types.
		/// </summary>
		public T DeserializeInternal<T>(Stream stream, JsonConverter converter)
		{
			if (converter == null) return this.Deserialize<T>(stream);

			var serializer = JsonSerializer.Create(this.CreateSettings(converter));
			var jsonTextReader = new JsonTextReader(new StreamReader(stream));
			var t = (T)serializer.Deserialize(jsonTextReader, typeof(T));
			return t;

		}

		protected internal T _Deserialize<T>(Stream stream, JsonSerializerSettings settings = null)
		{
			settings = settings ?? _serializationSettings;
			var serializer = JsonSerializer.Create(settings);
			var jsonTextReader = new JsonTextReader(new StreamReader(stream));
			var t = (T)serializer.Deserialize(jsonTextReader, typeof(T));
			//var r = t as BaseResponse;
			//if (r != null)
			//{
			//	r.ConnectionStatus = response;
			//}
			return t;
		}

		public virtual Task<T> DeserializeAsync<T>(Stream stream)
		{
			//TODO sadly json .net async does not read the stream async so 
			//figure out wheter reading the stream async on our own might be beneficial 
			//over memory possible memory usage
			var tcs = new TaskCompletionSource<T>();
			var r = this.Deserialize<T>(stream);
			tcs.SetResult(r);
			return tcs.Task;
		}

		internal JsonSerializerSettings CreateSettings(JsonConverter piggyBackJsonConverter = null)
		{
			var piggyBackState = new JsonConverterPiggyBackState { ActualJsonConverter = piggyBackJsonConverter };
			var settings = new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings),
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

			if (_settings.ModifyJsonSerializerSettings != null)
				_settings.ModifyJsonSerializerSettings(settings);

			settings.ContractResolver = new SettingsContractResolver(settings.ContractResolver, this._settings) { PiggyBackState = piggyBackState };

			return settings;
		}

		public string SerializeBulkDescriptor(IBulkRequest bulkRequest)
		{
			bulkRequest.ThrowIfNull("bulkDescriptor");
			bulkRequest.Operations.ThrowIfEmpty("Bulk descriptor does not define any operations");
			var sb = new StringBuilder();
			var inferrer = new ElasticInferrer(this._settings);

			foreach (var operation in bulkRequest.Operations)
			{
				var command = operation.Operation;
				var index = operation.Index
							?? inferrer.IndexName(operation.Index)
							?? inferrer.IndexName(operation.ClrType);
				var typeName = operation.Type
							   ?? inferrer.TypeName(operation.Type)
							   ?? inferrer.TypeName(operation.ClrType);
			

				var id = operation.GetIdForOperation(inferrer);
				operation.Index = index;
				operation.Type = typeName;
				operation.Id = id;

				var opJson = this.Serialize(operation, SerializationFormatting.None).Utf8String();

				var action = "{{ \"{0}\" :  {1} }}\n".F(command, opJson);
				sb.Append(action);
				var body = operation.GetBody();
				if (body == null)
					continue;
				var jsonCommand = this.Serialize(body, SerializationFormatting.None).Utf8String();
				sb.Append(jsonCommand + "\n");
			}
			var json = sb.ToString();
			return json;
		}

		/// <summary>
		/// _msearch needs a specialized json format in the body
		/// </summary>
		public string SerializeMultiSearch(MultiSearchDescriptor multiSearchDescriptor)
		{
			var sb = new StringBuilder();
			var inferrer = new ElasticInferrer(this._settings);
			foreach (var operation in multiSearchDescriptor._operations.Values)
			{
				var path = operation.ToPathInfo(this._settings);
				var op = new
				{
					index = path.Index,
					type = path.Type,
					search_type = this.GetSearchType(operation, multiSearchDescriptor),
					preference = operation._Preference,
					routing = operation._Routing
				};
				var opJson = this.Serialize(op, SerializationFormatting.None).Utf8String();

				var action = "{0}\n".F(opJson);
				sb.Append(action);
				var searchJson = this.Serialize(operation, SerializationFormatting.None).Utf8String();
				sb.Append(searchJson + "\n");

			}
			var json = sb.ToString();
			return json;
		}


		protected string GetSearchType(ISearchRequest descriptor, MultiSearchDescriptor multiSearchDescriptor)
		{
			if (descriptor._SearchType != null)
			{
				return descriptor._SearchType.Value.GetStringValue();
			}
			IRequest<MultiSearchRequestParameters> request = multiSearchDescriptor;
			return request.RequestParameters.GetQueryStringValue<string>("search_type");
		}

	}
}