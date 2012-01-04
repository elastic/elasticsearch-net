using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.DSL;
using System.IO;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	internal class TypeMappingWriter<T> where T : class
	{
		private string TypeName { get; set; }
		private PropertyNameResolver PropertyNameResolver { get; set; }
		public TypeMappingWriter(string typeName, PropertyNameResolver propertyNameResolver)
		{
			this.TypeName = typeName;
			this.PropertyNameResolver = propertyNameResolver;
		}

		internal string MapFromAttributes()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonTextWriter(sw))
			{
				jsonWriter.Formatting = Formatting.Indented;
				jsonWriter.WriteStartObject();
				{
					var typeName = this.TypeName;
					jsonWriter.WritePropertyName(typeName);
					jsonWriter.WriteStartObject();
					{
						this.WriteRootObjectProperties(jsonWriter);
							
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
		private void WriteRootObjectProperties(JsonWriter jsonWriter)
		{
			var att = this.PropertyNameResolver.GetElasticPropertyFor<T>();
			if (att == null)
				return;

			if (!att.DateDetection)
			{
				jsonWriter.WritePropertyName("date_detection");
				jsonWriter.WriteRawValue("false");
			}
			if (att.NumericDetection)
			{
				jsonWriter.WritePropertyName("date_detection");
				jsonWriter.WriteRawValue("true");
			}
			if (!att.IndexAnalyzer.IsNullOrEmpty())
			{
				jsonWriter.WritePropertyName("index_analyzer");
				jsonWriter.WriteValue(att.IndexAnalyzer);
			}
			if (!att.SearchAnalyzer.IsNullOrEmpty())
			{
				jsonWriter.WritePropertyName("search_analyzer");
				jsonWriter.WriteValue(att.SearchAnalyzer);
			}
			if (!att.SearchAnalyzer.IsNullOrEmpty())
			{
				jsonWriter.WritePropertyName("search_analyzer");
				jsonWriter.WriteValue(att.SearchAnalyzer);
			}
			if (!att.ParentType.IsNullOrEmpty())
			{
				jsonWriter.WritePropertyName("_parent");
				jsonWriter.WriteStartObject();
				{
					jsonWriter.WritePropertyName("type");
					jsonWriter.WriteValue(att.ParentType);
				}
				jsonWriter.WriteEndObject();
			}
			if (att.DynamicDateFormats != null && att.DynamicDateFormats.Any())
			{
				jsonWriter.WritePropertyName("dynamic_date_formats");
				jsonWriter.WriteStartArray();
				foreach(var d in att.DynamicDateFormats)
				{
					jsonWriter.WriteValue(d);	
				}
				jsonWriter.WriteEndArray();
				
			}
		}

		private void WriteProperties(JsonWriter jsonWriter)
		{
			var properties = typeof(T).GetProperties();
			foreach (var p in properties)
			{
				var att = this.PropertyNameResolver.GetElasticProperty(p);
				if (att != null && att.OptOut)
					continue;

				var propertyName = this.PropertyNameResolver.Resolve(p);
				var type = this.GetElasticSearchTypeFromType(p.PropertyType);
				if (att != null && att.Type != FieldType.none)
					type = Enum.GetName(typeof(FieldType), att.Type);

				if (type == null) //could not get type from attribute or infer from CLR type.
					continue;

				jsonWriter.WritePropertyName(propertyName);
				jsonWriter.WriteStartObject();
				{
					if (att == null) //properties that follow can not be inferred from the CLR.
					{
						jsonWriter.WritePropertyName("type");
						jsonWriter.WriteValue(type);
						jsonWriter.WriteEnd();
						continue;
					}

					if (att.AddSortField)
					{
						jsonWriter.WritePropertyName("type");
						jsonWriter.WriteValue("multi_field");
						jsonWriter.WritePropertyName("fields");
						jsonWriter.WriteStartObject();
						jsonWriter.WritePropertyName(propertyName);
						jsonWriter.WriteStartObject();
					}

					

					if (att.NumericType != NumericType.Default)
					{
						jsonWriter.WritePropertyName("type");
						var numericType = Enum.GetName(typeof(NumericType), att.NumericType);
						jsonWriter.WriteValue(numericType.ToLower());
					}
					else
					{
						jsonWriter.WritePropertyName("type");
						jsonWriter.WriteValue(type);
					}
					if (!att.Analyzer.IsNullOrEmpty())
					{
						jsonWriter.WritePropertyName("analyzer");
						jsonWriter.WriteValue(att.Analyzer);
					}
					if (!att.IndexAnalyzer.IsNullOrEmpty())
					{
						jsonWriter.WritePropertyName("index_analyzer");
						jsonWriter.WriteValue(att.IndexAnalyzer);
					}
					if (!att.IndexAnalyzer.IsNullOrEmpty())
					{
						jsonWriter.WritePropertyName("index_analyzer");
						jsonWriter.WriteValue(att.IndexAnalyzer);
					}
					if (!att.NullValue.IsNullOrEmpty())
					{
						jsonWriter.WritePropertyName("null_value");
						jsonWriter.WriteValue(att.NullValue);
					}
					if (!att.SearchAnalyzer.IsNullOrEmpty())
					{
						jsonWriter.WritePropertyName("search_analyzer");
						jsonWriter.WriteValue(att.SearchAnalyzer);
					}
					if (att.Index != FieldIndexOption.analyzed)
					{
						jsonWriter.WritePropertyName("index");
						jsonWriter.WriteValue(Enum.GetName(typeof(FieldIndexOption), att.Index));
					}
					if (att.TermVector != TermVectorOption.no)
					{
						jsonWriter.WritePropertyName("term_vector");
						jsonWriter.WriteValue(Enum.GetName(typeof(TermVectorOption), att.TermVector));
					}
					if (att.OmitNorms)
					{
						jsonWriter.WritePropertyName("omit_norms");
						jsonWriter.WriteValue("true");
					}
					if (att.OmitTermFrequencyAndPositions)
					{
						jsonWriter.WritePropertyName("omit_term_freq_and_positions");
						jsonWriter.WriteValue("true");
					}
					if (!att.IncludeInAll)
					{
						jsonWriter.WritePropertyName("include_in_all");
						jsonWriter.WriteValue("false");
					}
					if (att.Store)
					{
						jsonWriter.WritePropertyName("store");
						jsonWriter.WriteValue("yes");
					}
					if (att.Boost != 1)
					{
						jsonWriter.WritePropertyName("boost");
						jsonWriter.WriteRawValue(att.Boost.ToString());
					}
					if (att.PrecisionStep != 4)
					{
						jsonWriter.WritePropertyName("precision_step");
						jsonWriter.WriteRawValue(att.PrecisionStep.ToString());
					}

					if (att.AddSortField)
					{
						jsonWriter.WriteEnd();
						jsonWriter.WritePropertyName("sort");
						jsonWriter.WriteStartObject();

						if (att.NumericType != NumericType.Default)
						{
							jsonWriter.WritePropertyName("type");
							var numericType = Enum.GetName(typeof(NumericType), att.NumericType);
							jsonWriter.WriteValue(numericType.ToLower());
						}
						else
						{
							jsonWriter.WritePropertyName("type");
							jsonWriter.WriteValue(type);
						}
						jsonWriter.WritePropertyName("index");
						jsonWriter.WriteValue(Enum.GetName(typeof(FieldIndexOption), FieldIndexOption.not_analyzed));
						jsonWriter.WriteEnd();
						jsonWriter.WriteEnd();
					}
				}
				jsonWriter.WriteEnd();
			}
		}


		private string GetElasticSearchTypeFromType(Type t)
		{
			if (t == typeof(string))
				return "string";
			if (t.IsValueType)
			{
				switch (t.Name)
				{
					case "Int32":
						return "integer";
					case "Int64":
						return "long";
					case "Single":
						return "float";
					case "Double":
						return "double";
					case "DateTime":
						return "date";
				}
			}
			return null;
		}
	}
}
