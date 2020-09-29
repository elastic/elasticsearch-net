// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<PrefixQuery, IPrefixQuery>))]
	public interface IPrefixQuery : ITermQuery
	{
		[DataMember(Name ="rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }
	}

	[DataContract]
	public class PrefixQuery : FieldNameQueryBase, IPrefixQuery
	{
		public MultiTermQueryRewrite Rewrite { get; set; }
		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Prefix = this;
	}

	public class PrefixQueryDescriptor<T>
		: TermQueryDescriptorBase<PrefixQueryDescriptor<T>, IPrefixQuery, T>,
			IPrefixQuery where T : class
	{
		MultiTermQueryRewrite IPrefixQuery.Rewrite { get; set; }

		public PrefixQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.Rewrite = v);
	}
}
