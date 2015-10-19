using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public static class Static
	{
		public static IndexName Index(IndexName index) => index;
		public static IndexName Index<T>() => typeof(T);

		public static Indices Indices(params IndexName[] indices) => indices;
		public static Indices Indices(IEnumerable<IndexName> indices) => indices.ToArray();
		public static Indices AllIndices = Nest.Indices.All;

		public static TypeName Type(TypeName type) => type;
		public static TypeName Type<T>() => typeof(T);
		public static Types Type(IEnumerable<TypeName> types) => new Types.ManyTypes(types);
		public static Types Type(params TypeName[] types) => new Types.ManyTypes(types);
		public static Types AllTypes = Nest.Types.All;
	}
}
