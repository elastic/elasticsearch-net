using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace Nest.Resolvers.Writers
{
	public class TypeMappingWriter
	{
		private readonly Type _type;
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly NestSerializer _elasticSerializer;

		private readonly static string _noFieldTypeMessage =
			"Property {0} on type {1} has an ElasticProperty attribute but its FieldType (Type = ) can not be inferred and is not set explicitly while calling MapFromAttributes";

		private ElasticInferrer Infer { get; set; }

		private int MaxRecursion { get; set; }
		private TypeNameMarker TypeName { get; set; }
		private ConcurrentDictionary<Type, int> SeenTypes { get; set; }

		public TypeMappingWriter(Type t, TypeNameMarker typeName, IConnectionSettingsValues connectionSettings, int maxRecursion)
		{
			this._type = t;
			this._connectionSettings = connectionSettings;

			this.TypeName = typeName;
			this.MaxRecursion = maxRecursion;

			this.SeenTypes = new ConcurrentDictionary<Type, int>();
			this.SeenTypes.TryAdd(t, 0);

			this._elasticSerializer = new NestSerializer(this._connectionSettings);
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}

		/// <summary>
		/// internal constructor by TypeMappingWriter itself when it recurses, passes seenTypes as safeguard agains maxRecursion
		/// </summary>
		internal TypeMappingWriter(Type t, string typeName, IConnectionSettingsValues connectionSettings, int maxRecursion, ConcurrentDictionary<Type, int> seenTypes)
		{
			this._type = GetUnderlyingType(t);
			this._connectionSettings = connectionSettings;

			this.TypeName = typeName;
			this.MaxRecursion = maxRecursion;
			this.SeenTypes = seenTypes;

			this._elasticSerializer = new NestSerializer(this._connectionSettings);
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}

		internal JObject MapPropertiesFromAttributes()
		{
			int seen;
			if (this.SeenTypes.TryGetValue(this._type, out seen) && seen > MaxRecursion)
				return JObject.Parse("{}");


			var sb = new StringBuilder();
			using (StringWriter sw = new StringWriter(sb))
			using (JsonWriter jsonWriter = new JsonTextWriter(sw))
			{

				jsonWriter.WriteStartObject();
				{
					this.WriteProperties(jsonWriter);
				}
				jsonWriter.WriteEnd();
				return JObject.Parse(sw.ToString());
			}
		}

		internal RootObjectMapping RootObjectMappingFromAttributes()
		{
			var json = JObject.Parse(this.MapFromAttributes());

			var nestedJson = json.Properties().First().Value.ToString();
			using (var ms = new MemoryStream(nestedJson.Utf8Bytes()))
				return this._elasticSerializer.Deserialize<RootObjectMapping>(ms);
		}

		internal ObjectMapping ObjectMappingFromAttributes()
		{
			var json = JObject.Parse(this.MapFromAttributes());

			var nestedJson = json.Properties().First().Value.ToString();
			using (var ms = new MemoryStream(nestedJson.Utf8Bytes()))
				return this._elasticSerializer.Deserialize<ObjectMapping>(ms);
		}

		internal NestedObjectMapping NestedObjectMappingFromAttributes()
		{
			var json = JObject.Parse(this.MapFromAttributes());

			var nestedJson = json.Properties().First().Value.ToString();
			using (var ms = new MemoryStream(nestedJson.Utf8Bytes()))
				return this._elasticSerializer.Deserialize<NestedObjectMapping>(ms);
		}

		public string MapFromAttributes()
		{
			var sb = new StringBuilder();
			using (var sw = new StringWriter(sb))
			using (var jsonWriter = new JsonTextWriter(sw))
			{
				jsonWriter.Formatting = Formatting.Indented;
				jsonWriter.WriteStartObject();
				{
					var typeName = this.Infer.TypeName(this.TypeName);
					jsonWriter.WritePropertyName(typeName);
					jsonWriter.WriteStartObject();
					{
						jsonWriter.WritePropertyName("properties");
						jsonWriter.WriteStartObject();
						{
							this.WriteProperties(jsonWriter);
						}
						jsonWriter.WriteEnd();
					}
					jsonWriter.WriteEnd();
				}
				jsonWriter.WriteEndObject();

				return sw.ToString();
			}
		}

		internal void WriteProperties(JsonWriter jsonWriter)
		{
			var properties = this._type.GetProperties();
			foreach (var p in properties)
			{
				var att = ElasticAttributes.Property(p, this._connectionSettings);
				if (att != null && att.OptOut)
					continue;

				var propertyName = this.Infer.PropertyName(p);
				var type = GetElasticsearchTypeName(att, p);

				if (type == null) //could not get type from attribute or infer from CLR type.
					continue;

				jsonWriter.WritePropertyName(propertyName);
				jsonWriter.WriteStartObject();
				{
					if (att == null) //properties that follow can not be inferred from the CLR.
					{
						jsonWriter.WritePropertyName("type");
						jsonWriter.WriteValue(type);
						//jsonWriter.WriteEnd();
					}
					if (att != null)
						this.WritePropertiesFromAttribute(jsonWriter, att, propertyName, type);
					if (type == "object" || type == "nested")
					{

						var deepType = GetUnderlyingType(p.PropertyType);
						var deepTypeName = this.Infer.TypeName(deepType);
						var seenTypes = new ConcurrentDictionary<Type, int>(this.SeenTypes);
						seenTypes.AddOrUpdate(deepType, 0, (t, i) => ++i);

						var newTypeMappingWriter = new TypeMappingWriter(deepType, deepTypeName, this._connectionSettings, MaxRecursion, seenTypes);
						var nestedProperties = newTypeMappingWriter.MapPropertiesFromAttributes();

						jsonWriter.WritePropertyName("properties");
						nestedProperties.WriteTo(jsonWriter);
					}
				}
				jsonWriter.WriteEnd();
			}
		}

		private void WritePropertiesFromAttribute(JsonWriter jsonWriter, IElasticPropertyAttribute att, string propertyName, string type)
		{
			var visitor = new WritePropertiesFromAttributeVisitor(jsonWriter, propertyName, type);
			att.Accept(visitor);

		}

		/// <summary>
		/// Gets the Elasticsearch type name for a given ElasticPropertyAttribute.
		/// </summary>
		/// <param name="attribute">ElasticPropertyAttribute</param>
		/// <param name="propertyInfo">Property field</param>
		/// <returns>String containing the type name, or null if can not be inferred.</returns>
		private string GetElasticsearchTypeName(IElasticPropertyAttribute attribute, PropertyInfo propertyInfo)
		{
			FieldType? fieldType = null;
			
			if (attribute != null)
				fieldType = attribute.Type;

			if (fieldType == null || fieldType == FieldType.None)
			{
				fieldType = this.GetFieldType(propertyInfo.PropertyType);
				if (fieldType == null && attribute != null)
				{
					var message = _noFieldTypeMessage.F(propertyInfo.Name, this._type.Name);
					throw new DslException(message);
				}
			}

			return this.GetElasticsearchType(fieldType);
		}

		/// <summary>
		/// Gets the Elasticsearch type name for a given FieldType.
		/// </summary>
		/// <param name="fieldType">FieldType</param>
		/// <returns>String containing the type name, or null if can not be inferred.</returns>
		private string GetElasticsearchType(FieldType? fieldType)
		{
			switch (fieldType)
			{
				case FieldType.GeoPoint:
					return "geo_point";
				case FieldType.GeoShape:
					return "geo_shape";
				case FieldType.Attachment:
					return "attachment";
				case FieldType.Ip:
					return "ip";
				case FieldType.Binary:
					return "binary";
				case FieldType.String:
					return "string";
				case FieldType.Integer:
					return "integer";
				case FieldType.Short:
					return "short";
				case FieldType.Byte:
					return "byte";
				case FieldType.Long:
					return "long";
				case FieldType.Float:
					return "float";
				case FieldType.Double:
					return "double";
				case FieldType.Date:
					return "date";
				case FieldType.Boolean:
					return "boolean";
				case FieldType.Completion:
					return "completion";
				case FieldType.Nested:
					return "nested";
				case FieldType.Object:
					return "object";
				case FieldType.Murmur3Hash:
					return "murmur3";
				default:
					return null;
			}
		}

		/// <summary>
		/// Gets the FieldType for a given CLR type.
		/// </summary>
		/// <param name="propertyType">CLR type of the property</param>
		/// <returns>The FieldType, or null if can not be inferred.</returns>
		private FieldType? GetFieldType(Type propertyType)
		{
			propertyType = GetUnderlyingType(propertyType);

			if (propertyType == typeof(string))
				return FieldType.String;

			if (propertyType.IsEnum)
				return FieldType.Integer;

			if (propertyType.IsValueType)
			{
				switch (propertyType.Name)
				{
					case "Int32":
					case "UInt16":
						return FieldType.Integer;
					case "Int16":
					case "Byte":
						return FieldType.Short;
					case "SByte":
						return FieldType.Byte;
					case "Int64":
					case "UInt32":
						return FieldType.Long;
					case "Single":
						return FieldType.Float;
					case "Decimal":
					case "Double":
                    case "UInt64":
						return FieldType.Double;
					case "DateTime":
                    case "DateTimeOffset":
						return FieldType.Date;
					case "Boolean":
						return FieldType.Boolean;
                    case "Char":
                    case "Guid":
						return FieldType.String;
				}
			}
			else
				return FieldType.Object;
			return null;
		}

		private static Type GetUnderlyingType(Type type)
		{
			if (type.IsArray)
				return type.GetElementType();

			if (type.IsGenericType && type.GetGenericArguments().Length == 1 && (type.GetInterface("IEnumerable") != null || Nullable.GetUnderlyingType(type) != null))
				return type.GetGenericArguments()[0];

			return type;
		}
	}
}
