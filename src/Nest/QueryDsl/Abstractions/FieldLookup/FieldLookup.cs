using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(FieldLookup))]
	public interface IFieldLookup
	{
		[DataMember(Name ="id")]
		Id Id { get; set; }

		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		[DataMember(Name ="path")]
		Field Path { get; set; }

		[DataMember(Name ="routing")]
		Routing Routing { get; set; }
	}

	public class FieldLookup : IFieldLookup
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }
		public Field Path { get; set; }
		public Routing Routing { get; set; }
	}

	public class FieldLookupDescriptor<T> : DescriptorBase<FieldLookupDescriptor<T>, IFieldLookup>, IFieldLookup
		where T : class
	{
		public FieldLookupDescriptor() => Self.Index = _ClrType;

		internal Type _ClrType => typeof(T);

		Id IFieldLookup.Id { get; set; }

		IndexName IFieldLookup.Index { get; set; }

		Field IFieldLookup.Path { get; set; }

		Routing IFieldLookup.Routing { get; set; }

		public FieldLookupDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public FieldLookupDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public FieldLookupDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public FieldLookupDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public FieldLookupDescriptor<T> Routing(Routing routing) => Assign(a => a.Routing = routing);
	}
}
