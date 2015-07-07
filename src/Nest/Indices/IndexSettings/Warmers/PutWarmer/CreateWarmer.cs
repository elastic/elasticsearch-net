using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class CreateWarmerDescriptor
	{
		internal IEnumerable<TypeName> _Types { get; set; }
		internal string _WarmerName { get; set; }

		internal ISearchRequest _SearchDescriptor { get; set; }

		/// <summary>
		/// The name of the warmer
		/// </summary>
		public CreateWarmerDescriptor WarmerName(string warmerName)
		{
			_WarmerName = warmerName;
			return this;
		}

		public CreateWarmerDescriptor AllTypes()
		{
			this._Types = null;
			return this;
		}

		public CreateWarmerDescriptor Types(params string[] types)
		{
			return this.Types((IEnumerable<string>)types);
		}

		public CreateWarmerDescriptor Types(IEnumerable<string> types)
		{
			this._Types = types.Select(s => (TypeName)s);
			return this;
		}

		public CreateWarmerDescriptor Types(params Type[] types)
		{
			return this.Types((IEnumerable<Type>)types);
		}

		public CreateWarmerDescriptor Types(IEnumerable<Type> types)
		{
			this._Types = types.Select(t => (TypeName)t);
			return this;
		}

		public CreateWarmerDescriptor Type(string type)
		{
			return this.Types(new[] {type});
		}

		public CreateWarmerDescriptor Type(Type type)
		{
			return this.Types(new[] {type});
		}

		public CreateWarmerDescriptor Type<T>()
		{
			return this.Type(typeof (T));
		}

		public CreateWarmerDescriptor Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> selector)
		{
			this._SearchDescriptor = selector(new SearchDescriptor<dynamic>());
			return this;
		}

		public CreateWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			this._SearchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}
	}
}
