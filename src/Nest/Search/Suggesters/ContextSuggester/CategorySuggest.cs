using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public interface ICategorySuggestContext : ISuggestContext
	{
		[JsonProperty("default")]
		IEnumerable<string> Default { get; set; }
	}

	[JsonObject]
	public class CategorySuggestContext : ICategorySuggestContext
	{
		public string Type { get { return "category"; } }
		public Field Path { get; set; }
		public IEnumerable<string> Default { get; set; }
	}

	public class CategorySuggestDescriptor<T>
		where T : class
	{
		internal CategorySuggestContext _Context = new CategorySuggestContext();

		public CategorySuggestDescriptor<T> Path(string path)
		{
			this._Context.Path = path;
			return this;
		}

		public CategorySuggestDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			this._Context.Path = objectPath;
			return this;
		}

		public CategorySuggestDescriptor<T> Default(params string[] defaults)
		{
			this._Context.Default = defaults;
			return this;
		}
	}
}
