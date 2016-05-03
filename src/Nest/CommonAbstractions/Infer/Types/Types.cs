using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TypesJsonConverter))]
	public class Types : Union<Types.AllTypesMarker, Types.ManyTypes>, IUrlParameter
	{
		public class AllTypesMarker { internal AllTypesMarker() { } }
		public static AllTypesMarker All { get; } = new AllTypesMarker();
		public static AllTypesMarker AllTypes { get; } = new AllTypesMarker();
		public class ManyTypes
		{
			private readonly List<TypeName> _types = new List<TypeName>();
			public IReadOnlyList<TypeName> Types => _types;
			internal ManyTypes(IEnumerable<TypeName> types) { this._types.AddRange(types); }

			public ManyTypes And<T>()
			{
				this._types.Add(typeof(T));
				return this;
			}
		}

		internal Types(Types.AllTypesMarker all) : base(all) { }
		internal Types(Types.ManyTypes types) : base(types) { }
		internal Types(IEnumerable<TypeName> types) : base(new ManyTypes(types)) { }

		public static TypeName Type(TypeName type) => type;
		public static TypeName Type<T>() => typeof(T);
		public static ManyTypes Type(IEnumerable<TypeName> types) => new ManyTypes(types);
		public static ManyTypes Type(params TypeName[] types) => new ManyTypes(types);

		public static Types Parse(string typesString)
		{
			if (typesString.IsNullOrEmpty()) return Types.All;
			var types = typesString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return Type(types.Select(i => (TypeName)i));
		}

		public static implicit operator Types(string typesString) => Parse(typesString);
		public static implicit operator Types(AllTypesMarker all) => new Types(all);
		public static implicit operator Types(ManyTypes many) => new Types(many);
		public static implicit operator Types(TypeName type) => new ManyTypes(new[] { type });
		public static implicit operator Types(TypeName[] type) => new ManyTypes(type);
		public static implicit operator Types(Type type) => new ManyTypes(new TypeName[] { type });

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			return this.Match(
				all => null,
				many =>
				{
					var nestSettings = settings as IConnectionSettingsValues;
					if (nestSettings == null)
						throw new Exception("Tried to pass field name on querystring but it could not be resolved because no nest settings are available");
					var infer = new Inferrer(nestSettings);
					var types = this.Item2.Types.Select(t => infer.TypeName(t)).Distinct();
					return string.Join(",", types);
				}
			);

		}

		public override bool Equals(object obj)
		{
			var other = obj as Types;
			if (other == null) return false;
			return this.Match(
				all => other.Match(a => true, m => false),
				many => other.Match(
					a => false,
					m => this.GetHashCode().Equals(other.GetHashCode())
				)
			);
		}

		public override int GetHashCode()
		{
			return this.Match(
				all => "_all".GetHashCode(),
				many => string.Concat(many.Types).GetHashCode()
			);
		}
	}
}
