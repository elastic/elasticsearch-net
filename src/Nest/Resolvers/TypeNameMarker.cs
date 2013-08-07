using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest.Resolvers
{
	[JsonConverter(typeof(TypeNameMarkerConverter))]
	public class TypeNameMarker : IEquatable<TypeNameMarker>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public bool IsConditionless ()
		{
			return this.Name.IsNullOrEmpty() && this.Type == null;
		}


		public string Resolve(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("connectionSettings");

			string typeName = this.Name;
			if (this.Type == null)
				return this.Name;
			if (connectionSettings.DefaultTypeNames.TryGetValue(this.Type, out typeName))
				return typeName;

			if (this.Type != null)
			{
				var att = new PropertyNameResolver().GetElasticPropertyFor(this.Type);
				if (att != null && !att.TypeNameMarker.IsNullOrEmpty())
					typeName = att.TypeNameMarker.Name;
				else if (att != null && !string.IsNullOrEmpty(att.Name))
					typeName = att.Name;
				else
					typeName = connectionSettings.DefaultTypeNameInferrer(this.Type);
				return typeName;
			}
			return this.Name;
		}

		public static implicit operator TypeNameMarker(string typeName)
		{
			return new TypeNameMarker {Name = typeName};
		}
	
		public static implicit operator TypeNameMarker(Type type)
		{
			return new TypeNameMarker { Type = type };
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			if (this.Type != null)
				return this.Type.GetHashCode();
			return 0;
		}
		public bool Equals(TypeNameMarker other)
		{
			return other != null && this.GetHashCode() == other.GetHashCode();
		}
	}
}