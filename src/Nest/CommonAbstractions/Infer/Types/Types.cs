using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(TypesJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Types : Union<Types.AllTypesMarker, Types.ManyTypes>, IUrlParameter, IEquatable<Types>
	{
		public class AllTypesMarker { internal AllTypesMarker() { } }
		public static AllTypesMarker All { get; } = new AllTypesMarker();
		public static AllTypesMarker AllTypes { get; } = new AllTypesMarker();
		public class ManyTypes
		{
			private readonly List<TypeName> _types = new List<TypeName>();
			public IReadOnlyList<TypeName> Types => _types;
			internal ManyTypes(IEnumerable<TypeName> types)
			{
				types.ThrowIfEmpty(nameof(types));
				this._types.AddRange(types);
			}

			internal ManyTypes(IEnumerable<string> types)
			{
				types.ThrowIfEmpty(nameof(types));
				this._types.AddRange(types.Select(s=>(TypeName)s));
			}

			public ManyTypes And<T>()
			{
				this._types.Add(typeof(T));
				return this;
			}
		}

		internal Types(AllTypesMarker all) : base(all) { }
		internal Types(ManyTypes types) : base(types) { }
		internal Types(IEnumerable<TypeName> types) : base(new ManyTypes(types)) { }

		public static TypeName Type(TypeName type) => type;
		public static TypeName Type<T>() => typeof(T);
		public static ManyTypes Type(IEnumerable<TypeName> types) => new ManyTypes(types);
		public static ManyTypes Type(params TypeName[] types) => new ManyTypes(types);
		public static ManyTypes Type(IEnumerable<string> indices) => new ManyTypes(indices);
		public static ManyTypes Type(params string[] indices) => new ManyTypes(indices);

		public static Types Parse(string typesString) => typesString.IsNullOrEmptyCommaSeparatedList(out var types) ? null : Type(types.Select(i => (TypeName)i));

		public static implicit operator Types(string typesString) => Parse(typesString);
		public static implicit operator Types(AllTypesMarker all) => all == null ? null : new Types(all);
		public static implicit operator Types(ManyTypes many) => many == null ? null : new Types(many);
		public static implicit operator Types(TypeName type) => type == null ? null : new ManyTypes(new[] { type });
		public static implicit operator Types(TypeName[] type) => type.IsEmpty() ? null : new ManyTypes(type);
		public static implicit operator Types(string[] many) => many.IsEmpty() ? null : new ManyTypes(many);
		public static implicit operator Types(Type type) => type == null ? null : new ManyTypes(new TypeName[] { type });

		private string DebugDisplay => this.Match(
			all => "_all",
			types => $"Count: {types.Types.Count} [" + string.Join(",", types.Types.Select((t, i) => $"({i+1}: {t.DebugDisplay})")) + "]"
		);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => this.Match(
			all => null,
			many =>
			{
				if (!(settings is IConnectionSettingsValues nestSettings))
					throw new ArgumentNullException(nameof(settings), $"Can not resolve Types if no {nameof(IConnectionSettingsValues)} is provided");

				var types = many.Types.Select(t => nestSettings.Inferrer.TypeName(t)).Distinct();
				return string.Join(",", types);
			}
		);

		public static bool operator ==(Types left, Types right) => Equals(left, right);

		public static bool operator !=(Types left, Types right) => !Equals(left, right);

		public bool Equals(Types other)
		{
			if (other == null) return false;
			return this.Match(
				all => other.Match(a => true, m => false),
				many => other.Match(
					a => false,
					m => EqualsAllTypes(m.Types, many.Types)
				)
			);
		}

		public override bool Equals(object obj)
		{
			Types other = null;
			if (obj is Types t) other = t;
			else if (obj is string s) other = s;
			else if (obj is TypeName n) other = n;
			return other != null && Equals(other);
		}

		private static bool EqualsAllTypes(IReadOnlyList<TypeName> thisTypes, IReadOnlyList<TypeName> otherTypes)
		{
			if (thisTypes == null && otherTypes == null) return true;
			if (thisTypes == null || otherTypes == null) return false;
			if (thisTypes.Count != otherTypes.Count) return false;
			return thisTypes.Count == otherTypes.Count && !thisTypes.Except(otherTypes).Any();
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
