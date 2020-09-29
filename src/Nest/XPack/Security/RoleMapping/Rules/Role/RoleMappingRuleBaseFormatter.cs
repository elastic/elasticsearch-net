// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;
namespace Nest
{
	internal class RoleMappingRuleBaseFormatter : IJsonFormatter<RoleMappingRuleBase>
	{
		private static readonly FieldRuleBaseFormatter FieldRuleBaseFormatter =
			new FieldRuleBaseFormatter();

		private static readonly AutomataDictionary Rules = new AutomataDictionary
		{
			{ "all", 0 },
			{ "any", 1 },
			{ "field", 2 },
			{ "except", 3 },
		};

		private static readonly SingleOrEnumerableFormatter<RoleMappingRuleBase> SingleOrEnumerableFormatter =
			new SingleOrEnumerableFormatter<RoleMappingRuleBase>();

		public RoleMappingRuleBase Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			RoleMappingRuleBase rule = null;

			while (reader.ReadIsInObject(ref count))
			{
				var field = reader.ReadPropertyNameSegmentRaw();
				if (Rules.TryGetValue(field, out var value))
				{
					switch (value)
					{
						case 0:
							var allRules = SingleOrEnumerableFormatter.Deserialize(ref reader, formatterResolver);
							rule = new AllRoleMappingRule(allRules);
							break;
						case 1:
							var anyRules = SingleOrEnumerableFormatter.Deserialize(ref reader, formatterResolver);
							rule = new AnyRoleMappingRule(anyRules);
							break;
						case 2:
							var fieldRule = FieldRuleBaseFormatter.Deserialize(ref reader, formatterResolver);
							rule = new FieldRoleMappingRule(fieldRule);
							break;
						case 3:
							var exceptRule = Deserialize(ref reader, formatterResolver);
							rule = new ExceptRoleMappingRole(exceptRule);
							break;
					}
				}
				else
					reader.ReadNextBlock();
			}

			return rule;
		}

		public void Serialize(ref JsonWriter writer, RoleMappingRuleBase value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();

			if (value.AllRules != null)
			{
				writer.WritePropertyName("all");
				writer.WriteBeginArray();
				var count = 0;
				foreach (var rule in value.AllRules)
				{
					if (count++ > 0)
						writer.WriteValueSeparator();

					Serialize(ref writer, rule, formatterResolver);
				}

				writer.WriteEndArray();
			}
			else if (value.AnyRules != null)
			{
				writer.WritePropertyName("any");
				writer.WriteBeginArray();
				var count = 0;
				foreach (var rule in value.AnyRules)
				{
					if (count++ > 0)
						writer.WriteValueSeparator();

					Serialize(ref writer, rule, formatterResolver);
				}
				writer.WriteEndArray();
			}
			else if (value.ExceptRule != null)
			{
				writer.WritePropertyName("except");
				Serialize(ref writer, value.ExceptRule, formatterResolver);
			}
			else
			{
				writer.WritePropertyName("field");
				FieldRuleBaseFormatter.Serialize(ref writer, value.FieldRule, formatterResolver);
			}

			writer.WriteEndObject();
		}
	}
}
