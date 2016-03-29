using System;
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

	public class FieldLookupDescriptor<T> : DescriptorBase<FieldLookupDescriptor<T>,IFieldLookup>, IFieldLookup
		where T : class
	{
		internal Type _ClrType => typeof(T);

		IndexName IFieldLookup.Index { get; set; }
		
		TypeName IFieldLookup.Type { get; set; }
		
		Id IFieldLookup.Id { get; set; }
		
		Field IFieldLookup.Path { get; set; }

		public FieldLookupDescriptor()
		{
			Self.Type = new TypeName { Type = this._ClrType };
			Self.Index = new IndexName { Type = this._ClrType };
		}

		public FieldLookupDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public FieldLookupDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public FieldLookupDescriptor<T> Type(TypeName type) => Assign(a => a.Type = type);

		public FieldLookupDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public FieldLookupDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}