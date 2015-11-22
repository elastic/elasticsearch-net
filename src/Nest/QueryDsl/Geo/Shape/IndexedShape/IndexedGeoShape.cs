using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public interface IIndexedGeoShape 
	{
		[JsonProperty("id")]
		Id Id { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

	}

	public class IndexedGeoShape : IIndexedGeoShape
	{
		public Id Id { get; set; }
		public TypeName Type { get; set; }
		public IndexName Index { get; set; }
		public Field Path { get; set; }
	}

	public class IndexedGeoShapeDescriptor<T> : DescriptorBase<IndexedGeoShapeDescriptor<T>, IIndexedGeoShape>, IIndexedGeoShape
		where T : class
	{
		Id IIndexedGeoShape.Id { get; set; }
		TypeName IIndexedGeoShape.Type { get; set; } = typeof (T);
		IndexName IIndexedGeoShape.Index { get; set; } = typeof (T);
		Field IIndexedGeoShape.Path { get; set; }

		public IndexedGeoShapeDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public IndexedGeoShapeDescriptor<T> Type(TypeName type) => Assign(a => a.Type = type);

		public IndexedGeoShapeDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public IndexedGeoShapeDescriptor<T> Path(Field field) => Assign(a => a.Path = field);

		public IndexedGeoShapeDescriptor<T> Path(Expression<Func<T, object>> field) => Assign(a => a.Path = field);
	}
}
