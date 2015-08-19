using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPhraseSuggestHighlight
	{
		[JsonProperty(PropertyName = "pre_tag")]
		string PreTag { get; set; }

		[JsonProperty(PropertyName = "post_tag")]
		string PostTag { get; set; }
	}

	public class PhraseSuggestHighlight : IPhraseSuggestHighlight
	{
		public string PreTag { get; set; }

		public string PostTag { get; set; }
	}

	public class PhraseSuggestHighlightDescriptor : IPhraseSuggestHighlight
	{
		internal IPhraseSuggestHighlight Highlight = new PhraseSuggestHighlight();

		string IPhraseSuggestHighlight.PreTag { get; set; }

		string IPhraseSuggestHighlight.PostTag { get; set; }

		public PhraseSuggestHighlightDescriptor PreTag(string preTag)
		{
			this.Highlight.PreTag = preTag;
			return this;
		}
		
		public PhraseSuggestHighlightDescriptor PostTag(string postTag)
		{
			this.Highlight.PostTag = postTag;
			return this;
		}
	}
}
