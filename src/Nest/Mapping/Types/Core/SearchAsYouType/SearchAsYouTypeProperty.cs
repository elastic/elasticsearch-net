using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISearchAsYouTypeProperty : ITextProperty
	{
		/// <summary>
		/// The largest shingle size to index the input with and create subfields for.
		/// </summary>
		[DataMember(Name = "max_shingle_size")]
		int? MaxShingleSize { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class SearchAsYouTypeProperty : TextProperty, ISearchAsYouTypeProperty
	{
		public SearchAsYouTypeProperty() : base(FieldType.SearchAsYouType) { }

		/// <inheritdoc />
		public int? MaxShingleSize { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class SearchAsYouTypePropertyDescriptor<T>
		: TextPropertyDescriptorBase<SearchAsYouTypePropertyDescriptor<T>, ISearchAsYouTypeProperty, T>, ISearchAsYouTypeProperty
		where T : class
	{
		public SearchAsYouTypePropertyDescriptor() : base(FieldType.SearchAsYouType) { }

		int? ISearchAsYouTypeProperty.MaxShingleSize { get; set; }

		/// <inheritdoc cref="ISearchAsYouTypeProperty.MaxShingleSize"/>
		public SearchAsYouTypePropertyDescriptor<T> MaxShingleSize(int? maxShingleSize) => Assign(maxShingleSize, (a, v) => a.MaxShingleSize = v);
	}
}
