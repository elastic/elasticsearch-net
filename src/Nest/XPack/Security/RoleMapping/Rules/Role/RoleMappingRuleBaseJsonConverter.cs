using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class RoleMappingRuleBaseJsonConverter: ReserializeJsonConverter<RoleMappingRuleBase, RoleMappingRuleBase>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var depth = reader.Depth;
			if (reader.TokenType != JsonToken.StartObject) return reader.ReadToEnd<object>(depth);
			reader.Read();
			if (reader.TokenType != JsonToken.PropertyName) return reader.ExhaustTo(depth);
			var propertyName = (string) reader.Value;
			switch (propertyName)
			{
				case "all":
					return TryReadArray(reader, objectType, existingValue, serializer, out var all)
						? reader.ExhaustTo(depth, new AllRoleMappingRule(all) )
						: reader.ExhaustTo(depth);
				case "any":
					return TryReadArray(reader, objectType, existingValue, serializer, out var any)
						? reader.ExhaustTo(depth, new AnyRoleMappingRule(any))
						: reader.ExhaustTo(depth);
				case "field":
					reader.Read();
					var fieldRule = FieldRuleBaseJsonConverter.ReadFieldRule(reader, objectType, existingValue, serializer);
					//var fieldRule = serializer.Deserialize<FieldRuleBase>(reader);
					return reader.ExhaustTo(depth, new FieldRoleMappingRule(fieldRule));
				case "except":
					reader.Read(); //{
					var exceptRule = ReadJson(reader, objectType, existingValue, serializer) as RoleMappingRuleBase;
					return reader.ExhaustTo(depth, new ExceptRoleMappingRole(exceptRule));

				default: return reader.ExhaustTo(depth);
			}
		}

		private bool TryReadArray(JsonReader reader, Type t, object v, JsonSerializer s, out IEnumerable<RoleMappingRuleBase> rules)
		{
			rules = Enumerable.Empty<RoleMappingRuleBase>();
			reader.Read();
			var anyDepth = reader.Depth;
			if (reader.TokenType != JsonToken.StartArray) return false;

			var l = new List<RoleMappingRuleBase>();
			while (reader.Depth >= anyDepth && reader.TokenType != JsonToken.EndArray)
			{
				reader.Read();
				if (reader.Depth == anyDepth && reader.TokenType == JsonToken.EndArray) break;
				var subRule = ReadJson(reader, t, v, s) as RoleMappingRuleBase;
				if (subRule == null) break;
				l.Add(subRule);
			}
			rules = l;
			return true;
		}
	}
}
