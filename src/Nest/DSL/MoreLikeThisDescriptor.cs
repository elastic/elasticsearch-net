using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class MoreLikeThisDescriptor<T> where T : class
	{
		protected readonly TypeNameResolver typeNameResolver;

		public MoreLikeThisDescriptor()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

		internal string _Index { get; set; }
		internal string _Type { get; set; }
		internal string _Id { get; set; }
		internal SearchDescriptor<T> _Search { get; set; }
		internal MoreLikeThisQueryDescriptor<T> _Options { get; set; }

		/// <summary>
		/// Explicitly specify an index, otherwise the default index for T is used.
		/// </summary>
		public MoreLikeThisDescriptor<T> Index(string index)
		{
			this._Index = index;
			return this;
		}
		
		/// <summary>
		/// Explicitly specify an type, otherwise the default type for T is used.
		/// </summary>
		public MoreLikeThisDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Explicitly specify an id, otherwise the default index for T is used.
		/// Either Id() or Object() MUST be called for Nest to infer an id. 
		/// If both are specified Id() will win
		/// </summary>
		public MoreLikeThisDescriptor<T> Id(int id)
		{
			this._Id = id.ToString();
			return this;
		}

		/// <summary>
		/// Explicitly specify an id, otherwise the default index for T is used.
		/// Either Id() or Object() MUST be called for Nest to infer an id. 
		/// If both are specified Id() will win
		/// </summary>
		public MoreLikeThisDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// Specify an object to infer the id from
		/// Either Id() or Object() MUST be called for Nest to infer an id. 
		/// If both are specified Id() will win
		/// </summary>
		public MoreLikeThisDescriptor<T> Object(T @object)
		{
			this._Id = new IdResolver().GetIdFor(@object);
			return this;
		}

		/// <summary>
		/// Specify on which fields the _mlt should act and how it should behave
		/// </summary>
		public MoreLikeThisDescriptor<T> Options(Func<MoreLikeThisQueryDescriptor<T>, MoreLikeThisQueryDescriptor<T>> optionsSelector)
		{
			optionsSelector.ThrowIfNull("optionsSelector");
			var d = optionsSelector(new MoreLikeThisQueryDescriptor<T>());
			this._Options = d;
			return this;
		}

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public MoreLikeThisDescriptor<T> Search(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchDescriptor)
		{
			searchDescriptor.ThrowIfNull("searchDescriptor");
			var d = searchDescriptor(new SearchDescriptor<T>());
			this._Search = d;
			return this;
		}
	}
}
