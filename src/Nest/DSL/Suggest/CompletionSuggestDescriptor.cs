using Nest.Resolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CompletionSuggestDescriptor<T> : BaseSuggestDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "fuzzy")]
		internal FuzzySuggestDescriptor<T> _Fuzzy { get; set; }

		public CompletionSuggestDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		} 

		public CompletionSuggestDescriptor<T> Text(string text)
		{
			this._Text = text;
			return this;
		}

		public CompletionSuggestDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}

		public CompletionSuggestDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public CompletionSuggestDescriptor<T> Fuzzy(Func<FuzzySuggestDescriptor<T>, FuzzySuggestDescriptor<T>> fuzzyDescriptor)
		{
			this._Fuzzy = fuzzyDescriptor(new FuzzySuggestDescriptor<T>());
			return this;
		}

		public CompletionSuggestDescriptor<T> Fuzzy()
		{
			this._Fuzzy = new FuzzySuggestDescriptor<T>();
			return this;
		}
	}
}
