using System;

namespace Nest
{
	public class SearchAsYouTypeAttribute : TextAttribute, ISearchAsYouTypeProperty
	{
		public SearchAsYouTypeAttribute() : base(FieldType.SearchAsYouType) { }

		public int MaxShingleSize
		{
			get => Self.MaxShingleSize.GetValueOrDefault(3);
			set => Self.MaxShingleSize = value;
		}

		int? ISearchAsYouTypeProperty.MaxShingleSize { get; set; }

		private ISearchAsYouTypeProperty Self => this;
	}
}
