// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public static class IndexSortSettings
	{
		public const string Fields = "index.sort.field";
		public const string Missing = "index.sort.missing";
		public const string Mode = "index.sort.mode";
		public const string Order = "index.sort.order";
	}

	[StringEnum]
	public enum IndexSortMode
	{
		[EnumMember(Value = "min")]
		Minimum,

		[EnumMember(Value = "max")]
		Maximum
	}

	[StringEnum]
	public enum IndexSortMissing
	{
		[EnumMember(Value = "_first")]
		First,

		[EnumMember(Value = "_last")]
		Last
	}

	[StringEnum]
	public enum IndexSortOrder
	{
		[EnumMember(Value = "asc")]
		Ascending,

		[EnumMember(Value = "desc")]
		Descending
	}

	public interface ISortingSettings
	{
		/// <summary>
		/// The list of fields used to sort the index. Only boolean, numeric, date and keyword fields with doc_values are allowed
		/// here.
		/// </summary>
		Fields Fields { get; set; }

		/// <summary>
		/// The missing parameter specifies how docs which are missing the field should be treated. The missing value can have the
		/// following values:
		/// <see cref="IndexSortMissing.Last" />: Documents without value for the field are sorted last.
		/// <see cref="IndexSortMissing.First" />: Documents without value for the field are sorted first.
		/// </summary>
		IndexSortMissing[] Missing { get; set; }

		/// <summary>
		/// The mode option controls what value, from a multi-value field, is picked to sort the document.
		/// The mode option can have the following values:
		/// <see cref="IndexSortMode.Minimum" />: Pick the lowest value.
		/// <see cref="IndexSortMode.Maximum" />: Pick the highest value.
		/// </summary>
		IndexSortMode[] Mode { get; set; }

		/// <summary>
		/// The sort order to use for each field. The order option can have the following values:
		/// <see cref="IndexSortOrder.Ascending" /> and
		/// <see cref="IndexSortOrder.Descending" />.
		/// </summary>
		IndexSortOrder[] Order { get; set; }
	}

	public class SortingSettings : ISortingSettings
	{
		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public IndexSortMissing[] Missing { get; set; }

		/// <inheritdoc />
		public IndexSortMode[] Mode { get; set; }

		/// <inheritdoc />
		public IndexSortOrder[] Order { get; set; }
	}

	public class SortingSettingsDescriptor<T> : DescriptorBase<SortingSettingsDescriptor<T>, ISortingSettings>, ISortingSettings where T : class
	{
		Fields ISortingSettings.Fields { get; set; }
		IndexSortMissing[] ISortingSettings.Missing { get; set; }
		IndexSortMode[] ISortingSettings.Mode { get; set; }
		IndexSortOrder[] ISortingSettings.Order { get; set; }

		public SortingSettingsDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SortingSettingsDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		public SortingSettingsDescriptor<T> Order(params IndexSortOrder[] order) => Assign(order, (a, v) => a.Order = v);

		public SortingSettingsDescriptor<T> Mode(params IndexSortMode[] mode) => Assign(mode, (a, v) => a.Mode = v);

		public SortingSettingsDescriptor<T> Missing(params IndexSortMissing[] missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}
