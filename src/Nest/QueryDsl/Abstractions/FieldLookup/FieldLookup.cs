using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldLookup>))]
	public interface IFieldLookup
	{
		[JsonProperty("id")]
		Id Id { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

		[JsonProperty("routing")]
		Routing Routing { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

	public class FieldLookup : IFieldLookup
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }
		public Field Path { get; set; }
		public Routing Routing { get; set; }
		public TypeName Type { get; set; }
	}

	public class FieldLookupDescriptor<T> : DescriptorBase<FieldLookupDescriptor<T>, IFieldLookup>, IFieldLookup
		where T : class
	{
		public FieldLookupDescriptor()
		{
			Self.Type = _ClrType;
			Self.Index = _ClrType;
		}

		internal Type _ClrType => typeof(T);

		Id IFieldLookup.Id { get; set; }

		IndexName IFieldLookup.Index { get; set; }

		Field IFieldLookup.Path { get; set; }

		Routing IFieldLookup.Routing { get; set; }

		TypeName IFieldLookup.Type { get; set; }

		public FieldLookupDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public FieldLookupDescriptor<T> Id(Id id) => Assign(id, (a, v) => a.Id = v);

		public FieldLookupDescriptor<T> Type(TypeName type) => Assign(type, (a, v) => a.Type = v);

		public FieldLookupDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public FieldLookupDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);

		public FieldLookupDescriptor<T> Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);
	}
}
