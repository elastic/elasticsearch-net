using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public static class IndexSortSettings
	{
		public const string Fields = "index.sort.field";
		public const string Order = "index.sort.order";
		public const string Mode = "index.sort.mode";
		public const string Missing = "index.sort.missing";
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum IndexSortMode
	{
		[EnumMember(Value = "min")]
		Minimum,
		[EnumMember(Value = "max")]
		Maximum
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum IndexSortMissing
	{
		[EnumMember(Value = "_first")]
		First,
		[EnumMember(Value = "_last")]
		Last
	}

	[JsonConverter(typeof(StringEnumConverter))]
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
		/// The list of fields used to sort the index. Only boolean, numeric, date and keyword fields with doc_values are allowed here.
		/// </summary>
		string[] Fields { get; set; }

		/// <summary>
		/// The sort order to use for each field. The order option can have the following values: <see cref="IndexSortOrder.Ascending"/> and <see cref="IndexSortOrder.Descending"/>.
		/// </summary>
		IndexSortOrder[] Order { get; set; }

		/// <summary>
		/// The mode option controls what value, from a multi-value field, is picked to sort the document.
		/// The mode option can have the following values:
		/// <see cref="IndexSortMode.Minimum" />: Pick the lowest value.
		/// <see cref="IndexSortMode.Maximum" />: Pick the highest value.
		/// </summary>
		IndexSortMode[] Mode { get; set; }

		/// <summary>
		/// The missing parameter specifies how docs which are missing the field should be treated. The missing value can have the following values:
		/// <see cref="IndexSortMissing.Last"/>: Documents without value for the field are sorted last.
		/// <see cref="IndexSortMissing.First"/>: Documents without value for the field are sorted first.
		/// </summary>
		IndexSortMissing[] Missing { get; set; }
	}

	public class SortingSettings : ISortingSettings
	{
		/// <inheritdoc/>
		public string[] Fields { get; set; }

		/// <inheritdoc/>
		public IndexSortOrder[] Order { get; set; }

		/// <inheritdoc/>
		public IndexSortMode[] Mode { get; set; }

		/// <inheritdoc/>
		public IndexSortMissing[] Missing { get; set; }
	}

	public class SortingSettingsDescriptor : DescriptorBase<SortingSettingsDescriptor, ISortingSettings>, ISortingSettings
	{
		string[] ISortingSettings.Fields { get; set; }
		IndexSortOrder[] ISortingSettings.Order { get; set; }
		IndexSortMode[] ISortingSettings.Mode { get; set; }
		IndexSortMissing[] ISortingSettings.Missing { get; set; }

		/// <inheritdoc/>
		public SortingSettingsDescriptor Fields(params string[] fields) => Assign(a => a.Fields = fields);

		/// <inheritdoc/>
		public SortingSettingsDescriptor Order(params IndexSortOrder[] order) => Assign(a => a.Order = order);

		/// <inheritdoc/>
		public SortingSettingsDescriptor Mode(params IndexSortMode[] mode) => Assign(a => a.Mode = mode);

		/// <inheritdoc/>
		public SortingSettingsDescriptor Missing(params IndexSortMissing[] missing) => Assign(a => a.Missing = missing);
	}
}
