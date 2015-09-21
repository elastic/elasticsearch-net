using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IMultiTermVectorsRequest 
	{
		[JsonProperty("docs")]
		IEnumerable<MultiTermVectorDocument> Documents { get; set; }
	}

	public partial class MultiTermVectorsRequest 
	{
		public MultiTermVectorsRequest() { }

		public MultiTermVectorsRequest(Indices indices, Types types)
			: base(p => p.Required(indices).Required(types))
		{ }

		public MultiTermVectorsRequest(Indices indices)
			: base(p => p.Required(indices))
		{ }

		public MultiTermVectorsRequest(Types types)
			: base(p => p.Required(types))
		{ }

		public IEnumerable<MultiTermVectorDocument> Documents { get; set; }
	}

	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor<T> where T : class
	{
		private IMultiTermVectorsRequest Self => this;

		IEnumerable<MultiTermVectorDocument> IMultiTermVectorsRequest.Documents { get; set; }

		public MultiTermVectorsDescriptor()
			: base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
		{ }

		public MultiTermVectorsDescriptor(Indices indices, Types types)
			: base(p => p.Required(indices).Required(types))
		{ }

		public MultiTermVectorsDescriptor(Indices indices)
			: base(p => p.Required(indices).Required(Types.Single<T>()))
		{ }

		public MultiTermVectorsDescriptor(Types types)
			: base(p => p.Required(types).Required(Indices.Single<T>()))
		{ }

		public MultiTermVectorsDescriptor<T> Documents(params Func<MultiTermVectorDocumentDescriptor<T>, IMultiTermVectorDocumentDescriptor>[] documentSelectors)
		{
			Self.Documents = documentSelectors.Select(s => s(new MultiTermVectorDocumentDescriptor<T>()).GetDocument()).Where(d => d != null).ToList();
			return this;
		}

		public MultiTermVectorsDescriptor<T> Documents(IEnumerable<MultiTermVectorDocument> documents)
		{
			Self.Documents = documents;
			return this;
		}

		public MultiTermVectorsDescriptor<T> Ids(params string[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}

		public MultiTermVectorsDescriptor<T> Ids(params long[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
		}

		public MultiTermVectorsDescriptor<T> Ids(IEnumerable<string> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}

		public MultiTermVectorsDescriptor<T> Ids(IEnumerable<long> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
		}
	}
}
