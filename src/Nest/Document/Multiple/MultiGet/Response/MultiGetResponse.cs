// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(MultiGetResponseFormatter))]
	public class MultiGetResponse : ResponseBase
	{
		public IReadOnlyCollection<IMultiGetHit<object>> Hits => InternalHits.ToList().AsReadOnly();
		public override bool IsValid => base.IsValid && !InternalHits.HasAny(d => d.Error != null);

		internal ICollection<IMultiGetHit<object>> InternalHits { get; set; } = new List<IMultiGetHit<object>>();

		public MultiGetHit<T> Get<T>(string id) where T : class => Hits.OfType<MultiGetHit<T>>().FirstOrDefault(m => m.Id == id);

		public MultiGetHit<T> Get<T>(long id) where T : class => Get<T>(id.ToString(CultureInfo.InvariantCulture));

		public FieldValues GetFieldSelection<T>(long id) where T : class => GetFieldValues<T>(id.ToString(CultureInfo.InvariantCulture));

		public FieldValues GetFieldValues<T>(string id) where T : class
		{
			var multiHit = Get<T>(id);
			return multiHit?.Fields ?? FieldValues.Empty;
		}

		/// <summary>
		/// Retrieves the hits for each distinct id.
		/// </summary>
		/// <param name="ids">The ids to retrieve source for</param>
		/// <typeparam name="T">The document type for the hits to return</typeparam>
		/// <returns>An IEnumerable{T} of hits</returns>
		public IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<string> ids) where T : class
		{
			HashSet<string> seenIndices = null;
			foreach (var id in ids.Distinct())
			{
				if (seenIndices == null)
					seenIndices = new HashSet<string>();
				else
					seenIndices.Clear();

				foreach (var doc in Hits.OfType<IMultiGetHit<T>>())
				{
					if (string.Equals(doc.Id, id) && seenIndices.Add(doc.Index))
						yield return doc;
				}
			}
		}

		public IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<long> ids) where T : class =>
			GetMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)));

		public T Source<T>(string id) where T : class
		{
			var multiHit = Get<T>(id);
			return multiHit?.Source;
		}

		public T Source<T>(long id) where T : class => Source<T>(id.ToString(CultureInfo.InvariantCulture));

		/// <summary>
		/// Retrieves the source, if available, for each distinct id.
		/// </summary>
		/// <param name="ids">The ids to retrieve source for</param>
		/// <typeparam name="T">The document type for the hits to return</typeparam>
		/// <returns>An IEnumerable{T} of sources</returns>
		public IEnumerable<T> SourceMany<T>(IEnumerable<string> ids) where T : class =>
			from hit in GetMany<T>(ids)
			where hit.Found
			select hit.Source;

		public IEnumerable<T> SourceMany<T>(IEnumerable<long> ids) where T : class =>
			SourceMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)));
	}
}
