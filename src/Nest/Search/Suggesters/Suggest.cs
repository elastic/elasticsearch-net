using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Nest
{
	public class Suggest<T> where T : class
	{
		[DataMember(Name ="length")]
		public int Length { get; internal set; }

		[DataMember(Name ="offset")]
		public int Offset { get; internal set; }

		[DataMember(Name ="options")]
		public IReadOnlyCollection<SuggestOption<T>> Options { get; internal set; } = EmptyReadOnly<SuggestOption<T>>.Collection;

		[DataMember(Name ="text")]
		public string Text { get; internal set; }
	}
}
