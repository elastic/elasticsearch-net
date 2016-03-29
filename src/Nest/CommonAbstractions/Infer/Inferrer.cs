using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nest
{
	public class Inferrer
	{
		private IdResolver IdResolver { get; }
		private IndexNameResolver IndexNameResolver { get; }
		private TypeNameResolver TypeNameResolver { get; }
		private FieldResolver FieldResolver { get; }

		public Inferrer(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this.IdResolver = new IdResolver(connectionSettings);
			this.IndexNameResolver = new IndexNameResolver(connectionSettings);
			this.TypeNameResolver = new TypeNameResolver(connectionSettings);
			this.FieldResolver = new FieldResolver(connectionSettings);
		}

		public string Field(Field field) => this.FieldResolver.Resolve(field);

		public string PropertyName(PropertyName property) => this.FieldResolver.Resolve(property);

		public string IndexName<T>() where T : class => this.IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => this.IndexNameResolver.Resolve(index);
		
		public string Id<T>(T obj) where T : class => this.IdResolver.Resolve(obj);

		public string Id(Type objType, object obj) => this.IdResolver.Resolve(objType, obj);

		public string TypeName<T>() where T : class => this.TypeNameResolver.Resolve<T>();

		public string TypeName(TypeName type) => this.TypeNameResolver.Resolve(type);
	}
}
