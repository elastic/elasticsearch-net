using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class RoleMappingRuleBaseJsonConverter : IJsonFormatter<RoleMappingRuleBase>
	{
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
							var fieldRuleFormatter = formatterResolver.GetFormatter<FieldRuleBase>();
							var fieldRule = fieldRuleFormatter.Deserialize(ref reader, formatterResolver);
							rule = new FieldRoleMappingRule(fieldRule);
							break;
						case 3:
							var exceptRule = Deserialize(ref reader, formatterResolver);
							rule = new ExceptRoleMappingRole(exceptRule);
							break;
					}
				}
			}

			return rule;
		}

		public void Serialize(ref JsonWriter writer, RoleMappingRuleBase value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
