using System;
using System.Globalization;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ExternalFieldDeclaration>))]
	public interface IExternalFieldDeclaration
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }
		
		[JsonProperty("type")]
		TypeName Type { get; set; }
		
		[JsonProperty("id")]
		string Id { get; set; }
		
		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public class ExternalFieldDeclaration : IExternalFieldDeclaration
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public string Id { get; set; }
		public Field Path { get; set; }
	}

	public class ExternalFieldDeclarationDescriptor<T> : IExternalFieldDeclaration 
		where T : class
	{
		internal Type _ClrType => typeof(T);

		private IExternalFieldDeclaration Self => this;

		IndexName IExternalFieldDeclaration.Index { get; set; }
		
		TypeName IExternalFieldDeclaration.Type { get; set; }
		
		string IExternalFieldDeclaration.Id { get; set; }
		
		Field IExternalFieldDeclaration.Path { get; set; }

		public ExternalFieldDeclarationDescriptor()
		{
			Self.Type = new TypeName { Type = this._ClrType };
			Self.Index = new IndexName { Type = this._ClrType };
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
			Self.Type = new TypeName() { Name = type };
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