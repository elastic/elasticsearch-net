using System;
using System.Globalization;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class ExternalFieldDeclaration : IExternalFieldDeclaration
	{
		public IndexNameMarker Index { get; set; }
		public TypeNameMarker Type { get; set; }
		public string Id { get; set; }
		public PropertyPathMarker Path { get; set; }
	}

	public class ExternalFieldDeclarationDescriptor<T> : IExternalFieldDeclaration 
		where T : class
	{
		internal Type _ClrType { get { return typeof(T);  } }

		private IExternalFieldDeclaration Self { get { return this; } }

		IndexNameMarker IExternalFieldDeclaration.Index { get; set; }
		
		TypeNameMarker IExternalFieldDeclaration.Type { get; set; }
		
		string IExternalFieldDeclaration.Id { get; set; }
		
		PropertyPathMarker IExternalFieldDeclaration.Path { get; set; }

		public ExternalFieldDeclarationDescriptor()
		{
			Self.Type = new TypeNameMarker { Type = this._ClrType };
			Self.Index = new IndexNameMarker { Type = this._ClrType };
		}

		public ExternalFieldDeclarationDescriptor<T> Index(string index)
		{
			Self.Index = index;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Id(string id)
		{
			Self.Id = id;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Id(long id)
		{
			Self.Id = id.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Type(string type)
		{
			Self.Type = new TypeNameMarker() { Name = type };
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}
	}
}