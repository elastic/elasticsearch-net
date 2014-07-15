using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICompletionSuggester : ISuggester
	{
		[JsonProperty(PropertyName = "fuzzy")]
		IFuzzySuggester Fuzzy { get; set; }
	}

	public class CompletionSuggester : Suggester, ICompletionSuggester
	{
		public IFuzzySuggester Fuzzy { get; set; }
	}

	public class CompletionSuggestDescriptor<T> : BaseSuggestDescriptor<T>, ICompletionSuggester where T : class
	{
		public ICompletionSuggester Self { get { return this; } }

		IFuzzySuggester ICompletionSuggester.Fuzzy { get; set; }

		public CompletionSuggestDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}

		public CompletionSuggestDescriptor<T> Text(string text)
		{
			Self.Text = text;
			return this;
		}

		public CompletionSuggestDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public CompletionSuggestDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public CompletionSuggestDescriptor<T> Fuzzy(Func<FuzzySuggestDescriptor<T>, FuzzySuggestDescriptor<T>> fuzzyDescriptor = null)
		{
			if (fuzzyDescriptor == null)
			{
				Self.Fuzzy = new FuzzySuggester();
				return this;
			}
			Self.Fuzzy = fuzzyDescriptor(new FuzzySuggestDescriptor<T>());
			return this;
		}

		
	}
}
