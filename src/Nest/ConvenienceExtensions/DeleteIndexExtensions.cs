using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Provides convenience extension methods that make it easier to delete existing indices.
	/// </summary>
	public static class DeleteIndexExtensions
	{
		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index to be deleted</param>
		/// <param name="deleteIndexSelector">A descriptor that further describes the parameters for the delete index operation</param>
		public static IIndicesResponse DeleteIndex(this IElasticClient client, string index,
			Func<DeleteIndexDescriptor, DeleteIndexDescriptor> deleteIndexSelector = null)
		{
			index.ThrowIfNullOrEmpty("index");
			deleteIndexSelector = deleteIndexSelector ?? (d => d);
			return client.DeleteIndex(d => deleteIndexSelector(d).Index(index));
		}
	}
}
