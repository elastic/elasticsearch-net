using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettings connectionSettings;
		public TypeNameResolver(IConnectionSettings connectionSettings)
		{
			this.connectionSettings = connectionSettings;
		}
		public string GetTypeNameFor(Type type)
		{
			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");
			return this.GetTypeNameForType(type);
		}

		public string GetTypeNameFor<T>() where T : class
		{
			return this.GetTypeNameForType(typeof(T));
		}

		public string GetTypeNameForType(Type type)
		{
			var typeName = type.Name;
			var att = new PropertyNameResolver().GetElasticPropertyFor(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else if (this.connectionSettings == null)
			{
				typeName = Inflector.MakePlural(type.Name).ToLower();
			}
			else
			{

				if (this.connectionSettings.TypeNameInferrer != null)
					typeName = this.connectionSettings.TypeNameInferrer(typeName);
				if (this.connectionSettings.TypeNameInferrer == null || string.IsNullOrEmpty(typeName))
					typeName = Inflector.MakePlural(type.Name).ToLower();
			}
			return typeName;
		}
	}
}
