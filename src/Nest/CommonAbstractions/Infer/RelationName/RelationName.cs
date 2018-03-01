using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(RelationNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class RelationName : IEquatable<RelationName>, IUrlParameter
	{
		private static int TypeHashCode { get; } = typeof(RelationName).GetHashCode();

		public string Name { get; }
		public Type Type { get; }

		private RelationName(string type)
		{
			this.Name = type;
		}

		private RelationName(Type type)
		{
			this.Type = type;
		}

		internal string DebugDisplay => Type == null ? Name : $"{nameof(RelationName)} for typeof: {Type?.Name}";

		public static RelationName Create(Type type) => GetRelationNameForType(type);

		public static RelationName Create<T>() where T : class => GetRelationNameForType(typeof(T));

		private static RelationName GetRelationNameForType(Type type) => new RelationName(type);

		public static implicit operator RelationName(string typeName) =>
			typeName == null ? null : new RelationName(typeName);

		public static implicit operator RelationName(Type type) => type == null ? null : new RelationName(type);

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (this.Name?.GetHashCode() ?? this.Type?.GetHashCode() ?? 0);
				return result;
			}
		}

		bool IEquatable<RelationName>.Equals(RelationName other) => Equals(other);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			if (obj is RelationName pp) return this.EqualsMarker(pp);
			return base.Equals(obj);
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return this.Name;
			return this.Type != null ? this.Type.Name : string.Empty;
		}

		public bool EqualsMarker(RelationName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other?.Type != null)
				return this.Type == other.Type;
			return false;
		}

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass type name on querystring but it could not be resolved because no nest settings are available");

			return nestSettings.Inferrer.RelationName(this);
		}

		public static RelationName From<T>() => typeof(T);
	}
}
