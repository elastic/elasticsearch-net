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

		public string Name { get; }
		public Type Type { get; }

		private RelationName(string type) => this.Name = type;
		private RelationName(Type type) => this.Type = type;

		internal string DebugDisplay => Type == null ? Name : $"{nameof(RelationName)} for typeof: {Type?.Name}";

		public static RelationName From<T>() => typeof(T);

		public static RelationName Create(Type type) => GetRelationNameForType(type);

		public static RelationName Create<T>() where T : class => GetRelationNameForType(typeof(T));

		private static RelationName GetRelationNameForType(Type type) => new RelationName(type);

		public static implicit operator RelationName(string typeName) => typeName.IsNullOrEmpty() ? null : new RelationName(typeName);

		public static implicit operator RelationName(Type type) => type == null ? null : new RelationName(type);

		private static int TypeHashCode { get; } = typeof(RelationName).GetHashCode();
		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (this.Name?.GetHashCode() ?? this.Type?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(RelationName left, RelationName right) => Equals(left, right);

		public static bool operator !=(RelationName left, RelationName right) => !Equals(left, right);

		public bool Equals(RelationName other) => EqualsMarker(other);

		public override bool Equals(object obj) =>
			obj is string s ? this.EqualsString(s) : (obj is RelationName r) && this.EqualsMarker(r);

		public bool EqualsMarker(RelationName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other?.Type != null)
				return this.Type == other.Type;
			return false;
		}

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == this.Name;

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty()) return this.Name;
			return this.Type != null ? this.Type.Name : string.Empty;
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings), $"Can not resolve {nameof(RelationName)} if no {nameof(IConnectionSettingsValues)} is provided");

			return nestSettings.Inferrer.RelationName(this);
		}

	}
}
