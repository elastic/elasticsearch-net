using System;
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class IndexName : IEquatable<IndexName>, IUrlParameter
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		internal string DebugDisplay => Type == null ? Name : $"{nameof(IndexName)} for typeof: {Type?.Name}";

		public static implicit operator IndexName(string typeName) => typeName.IsNullOrEmpty()
			? null
			: new IndexName { Name = typeName.Trim() };

		public static implicit operator IndexName(Type type) => type == null
			? null
			: new IndexName { Type = type };

		bool IEquatable<IndexName>.Equals(IndexName other) => EqualsMarker(other);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as IndexName;
			return EqualsMarker(pp);
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			if (this.Type != null)
				return this.Type.GetHashCode();
			return 0;
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return this.Name;
			if (this.Type != null)
				return this.Type.Name;
			return string.Empty;
		}

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}

		public bool EqualsMarker(IndexName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other != null && other.Type != null)
				return this.GetHashCode() == other.GetHashCode();
			return false;
		}

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass index name on querysting but it could not be resolved because no nest settings are available");

			return nestSettings.Inferrer.IndexName(this);
		}

		public static IndexName From<T>() => typeof(T);

		public Indices And<T>() => new Indices(new IndexName[] { this, typeof(T) });
		public Indices And(IndexName index) => new Indices(new IndexName[] { this, index });
	}
}
