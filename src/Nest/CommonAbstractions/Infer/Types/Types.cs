using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TypesJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Types : Union<Types.AllTypesMarker, Types.ManyTypes>, IUrlParameter
	{
		internal Types(AllTypesMarker all) : base(all) { }

		internal Types(ManyTypes types) : base(types) { }

		internal Types(IEnumerable<TypeName> types) : base(new ManyTypes(types)) { }

		public static AllTypesMarker All { get; } = new AllTypesMarker();
		public static AllTypesMarker AllTypes { get; } = new AllTypesMarker();

		private string DebugDisplay => Match(
			all => "_all",
			types => $"Count: {types.Types.Count} [" + string.Join(",", types.Types.Select((t, i) => $"({i + 1}: {t.DebugDisplay})")) + "]"
		);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => Match(
			all => null,
			many =>
			{
				var nestSettings = settings as IConnectionSettingsValues;
				if (nestSettings == null)
					throw new Exception(
						"Tried to pass field name on querystring but it could not be resolved because no nest settings are available");

				var types = many.Types.Select(t => nestSettings.Inferrer.TypeName(t)).Distinct();
				return string.Join(",", types);
			}
		);

		public static TypeName Type(TypeName type) => type;

		public static TypeName Type<T>() => typeof(T);

		public static ManyTypes Type(IEnumerable<TypeName> types) => new ManyTypes(types);

		public static ManyTypes Type(params TypeName[] types) => new ManyTypes(types);

		public static ManyTypes Type(IEnumerable<string> indices) => new ManyTypes(indices);

		public static ManyTypes Type(params string[] indices) => new ManyTypes(indices);


		public static Types Parse(string typesString)
		{
			if (typesString.IsNullOrEmpty()) return All;

			var types = typesString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return Type(types.Select(i => (TypeName)i));
		}

		public static implicit operator Types(string typesString) => Parse(typesString);

		public static implicit operator Types(AllTypesMarker all) => new Types(all);

		public static implicit operator Types(ManyTypes many) => new Types(many);

		public static implicit operator Types(TypeName type) => new ManyTypes(new[] { type });

		public static implicit operator Types(TypeName[] type) => new ManyTypes(type);

		public static implicit operator Types(string[] many) => new ManyTypes(many);

		public static implicit operator Types(Type type) => new ManyTypes(new TypeName[] { type });

		public static bool operator ==(Types left, Types right) => Equals(left, right);

		public static bool operator !=(Types left, Types right) => !Equals(left, right);

		public override bool Equals(object obj)
		{
			if (!(obj is Types other)) return false;

			return Match(
				all => other.Match(a => true, m => false),
				many => other.Match(
					a => false,
					m => EqualsAllTypes(m.Types, many.Types)
				)
			);
		}

		private static bool EqualsAllTypes(IReadOnlyList<TypeName> thisTypes, IReadOnlyList<TypeName> otherTypes)
		{
			if (thisTypes == null && otherTypes == null) return true;
			if (thisTypes == null || otherTypes == null) return false;
			if (thisTypes.Count != otherTypes.Count) return false;

			return thisTypes.Count == otherTypes.Count && !thisTypes.Except(otherTypes).Any();
		}

		public override int GetHashCode() => Match(
			all => "_all".GetHashCode(),
			many => string.Concat(many.Types).GetHashCode()
		);

		public class AllTypesMarker
		{
			internal AllTypesMarker() { }
		}

		public class ManyTypes
		{
			private readonly List<TypeName> _types = new List<TypeName>();

			internal ManyTypes(IEnumerable<TypeName> types)
			{
				types.ThrowIfEmpty(nameof(types));
				_types.AddRange(types);
			}

			internal ManyTypes(IEnumerable<string> types)
			{
				types.ThrowIfEmpty(nameof(types));
				_types.AddRange(types.Select(s => (TypeName)s));
			}

			public IReadOnlyList<TypeName> Types => _types;

			public ManyTypes And<T>()
			{
				_types.Add(typeof(T));
				return this;
			}
		}
	}
}
