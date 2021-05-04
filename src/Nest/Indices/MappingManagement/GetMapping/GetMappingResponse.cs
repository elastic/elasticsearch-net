// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetMappingResponse, IndexName, IndexMappings>))]
	public class GetMappingResponse : DictionaryResponseBase<IndexName, IndexMappings>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexMappings> Indices => Self.BackingDictionary;

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}

	public class IndexMappings
	{
		[Obsolete("Mapping are no longer grouped by type, this indexer is ignored and simply returns Mapppings")]
		public TypeMapping this[string type] => Mappings;

		[DataMember(Name = "mappings")]
		public TypeMapping Mappings { get; internal set; }
	}


	public static class GetMappingResponseExtensions
	{
		public static ITypeMapping GetMappingFor<T>(this GetMappingResponse response) => response.GetMappingFor(typeof(T));

		public static ITypeMapping GetMappingFor(this GetMappingResponse response, IndexName index)
		{
			if (index.IsNullOrEmpty()) return null;

			return response.Indices.TryGetValue(index, out var indexMappings) ? indexMappings.Mappings : null;
		}
	}
}
