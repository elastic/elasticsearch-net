using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	/// <summary> This class allows a serializer to report back on a properties behavior </summary>
	public interface IPropertyMapping
	{
		/// <summary> Override the json property name of a type </summary>
		string Name { get; set; }
		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		bool Ignore { get; set; }
	}

	/// <summary> This class allows a serializer to report back on a properties behavior </summary>
	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

		/// <summary> Override the json property name of a type </summary>
		public string Name { get; set; }

		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		public bool Ignore { get; set; }

	}
}
