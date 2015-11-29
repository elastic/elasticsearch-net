using System;
using System.Globalization;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldLookup>))]
	public interface IFieldLookup
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }
		
		[JsonProperty("type")]
		TypeName Type { get; set; }
		
		[JsonProperty("id")]
		Id Id { get; set; }
		
		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public class FieldLookup : IFieldLookup
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public Id Id { get; set; }
		public Field Path { get; set; }
	}

	public class FieldLookupDescriptor<T> : IFieldLookup 
		where T : class
	{
		internal Type _ClrType => typeof(T);

		private IFieldLookup Self => this;

		IndexName IFieldLookup.Index { get; set; }
		
		TypeName IFieldLookup.Type { get; set; }
		
		Id IFieldLookup.Id { get; set; }
		
		Field IFieldLookup.Path { get; set; }

		public FieldLookupDescriptor()
		{
			Self.Type = new TypeName { Type = this._ClrType };
			Self.Index = new IndexName { Type = this._ClrType };
		}

		public FieldLookupDescriptor<T> Index(IndexName index)
		{
			Self.Index = index;
			return this;
		}
		public FieldLookupDescriptor<T> Id(Id id)
		{
			Self.Id = id;
			return this;
		}
		public FieldLookupDescriptor<T> Type(TypeName type)
		{
			Self.Type = type;
			return this;
		}
		public FieldLookupDescriptor<T> Path(Field path)
		{
			Self.Path = path;
			return this;
		}
		public FieldLookupDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}
	}
}