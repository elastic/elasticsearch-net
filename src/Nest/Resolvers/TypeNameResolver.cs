using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
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
			else
				typeName = Inflector.MakePlural(type.Name).ToLower();
			return typeName;
		}

		
	}
}
