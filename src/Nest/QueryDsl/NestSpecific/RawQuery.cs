
using System;

namespace Nest
{
	public interface IRawQuery : IQuery
	{
		string Raw { get; set; }
	}

	public class RawQuery : QueryBase, IRawQuery
	{
		public RawQuery() { }
		public RawQuery(string rawQuery)
		{
			this.Raw = rawQuery;
		}
		public string Raw { get; set; }

		protected override bool Conditionless => this.Raw.IsNullOrEmpty();
		internal override void InternalWrapInContainer(IQueryContainer container) => container.RawQuery = this;
	}

	public class RawQueryDescriptor : QueryDescriptorBase<RawQueryDescriptor, IRawQuery> , IRawQuery 
	{
		string IRawQuery.Raw { get; set; }

		protected override bool Conditionless => Self.Raw.IsNullOrEmpty();

		public RawQueryDescriptor Raw(string rawQuery) =>
			Assign(a => a.Raw = rawQuery);
	}
}
