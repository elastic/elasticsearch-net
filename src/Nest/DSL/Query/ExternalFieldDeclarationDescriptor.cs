using System;
using System.Globalization;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class ExternalFieldDeclarationDescriptor<T> : IExternalFieldDeclarationDescriptor 
		where T : class
	{
		internal Type _ClrType { get { return typeof(T);  } }

		[JsonProperty("index")]
		public IndexNameMarker _Index { get; set; }
		[JsonProperty("type")]
		public TypeNameMarker _Type { get; set; }
		[JsonProperty("id")]
		public string _Id { get; set; }
		[JsonProperty("path")]
		public string _Path { get; set; }

		public ExternalFieldDeclarationDescriptor()
		{
			this._Type = new TypeNameMarker { Type = this._ClrType };
			this._Index = new IndexNameMarker { Type = this._ClrType };
		}

		public ExternalFieldDeclarationDescriptor<T> Index(string index)
		{
			this._Index = index;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Id(int id)
		{
			this._Id = id.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Type(string type)
		{
			this._Type = new TypeNameMarker() { Name = type };
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Path(string path)
		{
			this._Path = path;
			return this;
		}
		public ExternalFieldDeclarationDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			this._Path = fieldName;
			return this;
		}
	}
}