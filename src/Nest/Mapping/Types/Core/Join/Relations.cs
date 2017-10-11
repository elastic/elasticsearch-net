using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Relations, TypeName, Children>))]
	public interface IRelations : IIsADictionary<TypeName, Children> { }

	public class Relations : IsADictionaryBase<TypeName, Children>, IRelations
	{
		public Relations() {}
		public Relations(IDictionary<TypeName, Children> container) : base(container) { }
		public Relations(Dictionary<TypeName, Children> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName type, Children children) => BackingDictionary.Add(type, children);
		public void Add(TypeName type, TypeName child, params TypeName[] moreChildren) =>
			BackingDictionary.Add(type, Types.Type(child).And(moreChildren));
	}

	public class RelationsDescriptor : IsADictionaryDescriptorBase<RelationsDescriptor,	IRelations, TypeName, Children>
	{
		public RelationsDescriptor() : base(new Relations()) { }
		internal RelationsDescriptor(IRelations relations) : base(relations) { }

		public RelationsDescriptor Join(TypeName parent, TypeName child, params TypeName[] moreChildren) =>
			Assign(parent, child.And(moreChildren));
		public RelationsDescriptor Join<TParent>(TypeName child, params TypeName[] moreChildren) =>
			Assign(typeof(TParent), child.And(moreChildren));
		public RelationsDescriptor Join<TParent, TChild>() => Assign(typeof(TParent), typeof(TChild));
	}

	[JsonConverter(typeof(ChildrenJsonConverter))]
	public class Children : List<TypeName>
	{
		public static implicit operator Children(TypeName[] types)
		{
			if (types == null) return null;
            var children = new Children();
            children.AddRange(types);
            return children;
		}

		public static implicit operator Children(Types types)
		{
			return types?.Match(a => null, m =>
			{
				var children = new Children();
				children.AddRange(m.Types);
				return children;
			});
		}

		public static implicit operator Children(TypeName type) => type == null ? null : new Children { type };
		public static implicit operator Children(Type type) => type == null ? null : new Children { type };
		public static implicit operator Children(string type) => type == null ? null : new Children { type };
	}

	internal class ChildrenJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var children = value as Children;
			if (children == null || children.Count == 0)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			var resolved = children.Cast<IUrlParameter>().ToList();
			if (resolved.Count == 1)
			{
				writer.WriteValue(resolved[0].GetString(settings));
				return;
			}
			writer.WriteStartArray();
			foreach(var r in resolved)
				writer.WriteValue(r.GetString(settings));
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
            var c = new Children();
			if (reader.TokenType == JsonToken.String)
			{
				var type = reader.Value.ToString();
                c.Add(type);
                return c;
			}
			if (reader.TokenType != JsonToken.StartArray) return null;
			var types = new List<TypeName> { };
			while (reader.TokenType != JsonToken.EndArray)
			{
				var type = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					types.Add(type);
			}
			c.AddRange(types);
			return c;
		}

	}
}
