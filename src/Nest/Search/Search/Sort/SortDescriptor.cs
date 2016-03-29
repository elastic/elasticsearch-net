using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class SortDescriptor<T> : DescriptorPromiseBase<SortDescriptor<T>, IList<ISort>>
		where T : class
	{
		public SortDescriptor() : base(new List<ISort>()) { }

		public SortDescriptor<T> Ascending(Expression<Func<T, object>> objectPath) => Assign(a => a.Add(new SortField { Field = objectPath, Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending(Expression<Func<T, object>> objectPath)=> Assign(a => a.Add(new SortField { Field = objectPath, Order = SortOrder.Descending }));

		public SortDescriptor<T> Ascending(Field field) => Assign(a => a.Add(new SortField { Field = field, Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending(Field field)=> Assign(a => a.Add(new SortField { Field = field, Order = SortOrder.Descending }));

		public SortDescriptor<T> Ascending(SortSpecialField field) => Assign(a => a.Add(new SortField { Field = field.GetStringValue(), Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending(SortSpecialField field) => Assign(a => a.Add(new SortField { Field = field.GetStringValue(), Order = SortOrder.Descending }));

		public SortDescriptor<T> Field(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector) => AddSort(sortSelector?.Invoke(new SortFieldDescriptor<T>()));

		public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new SortField { Field = field, Order = order });

		public SortDescriptor<T> Field(Expression<Func<T, object>> field, SortOrder order) => 
			AddSort(new SortField { Field = field, Order = order });

		public SortDescriptor<T> GeoDistance(Func<SortGeoDistanceDescriptor<T>, IGeoDistanceSort> sortSelector) => AddSort(sortSelector?.Invoke(new SortGeoDistanceDescriptor<T>()));

		public SortDescriptor<T> Script(Func<SortScriptDescriptor<T>, IScriptSort> sortSelector) => AddSort(sortSelector?.Invoke(new SortScriptDescriptor<T>()));

		private SortDescriptor<T> AddSort(ISort sort) => sort == null ? this : this.Assign(a => a.Add(sort));
	}
}