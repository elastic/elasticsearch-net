// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IRawQuery : IQuery
	{
		string Raw { get; set; }
	}

	public class RawQuery : QueryBase, IRawQuery
	{
		public RawQuery() { }

		public RawQuery(string rawQuery) => Raw = rawQuery;

		public string Raw { get; set; }

		protected override bool Conditionless => Raw.IsNullOrEmpty();

		internal override void InternalWrapInContainer(IQueryContainer container) => container.RawQuery = this;
	}

	public class RawQueryDescriptor : QueryDescriptorBase<RawQueryDescriptor, IRawQuery>, IRawQuery
	{
		public RawQueryDescriptor() { }

		public RawQueryDescriptor(string rawQuery) => Self.Raw = rawQuery;

		protected override bool Conditionless => Self.Raw.IsNullOrEmpty();
		string IRawQuery.Raw { get; set; }

		public RawQueryDescriptor Raw(string rawQuery) =>
			Assign(rawQuery, (a, v) => a.Raw = v);
	}
}
