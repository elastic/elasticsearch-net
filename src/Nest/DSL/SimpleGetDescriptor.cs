using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class SimpleGetDescriptor<T> : ISimpleGetDescriptor
		where T : class
	{
		IndexNameMarker ISimpleGetDescriptor._Index { get; set; }
		TypeNameMarker ISimpleGetDescriptor._Type { get; set; }
		string ISimpleGetDescriptor._Id { get; set; }
		string ISimpleGetDescriptor._Routing { get; set; }
		IList<string> ISimpleGetDescriptor._Fields { get; set; }
		Type ISimpleGetDescriptor._ClrType { get { return typeof(T); } }

		protected readonly TypeNameResolver typeNameResolver;

		public SimpleGetDescriptor()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public SimpleGetDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed.
		/// </summary>
		public SimpleGetDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public SimpleGetDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Type = type;
			return this;
		}

		public SimpleGetDescriptor<T> Id(int id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		public SimpleGetDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// Set the routing for the get operation
		/// </summary>
		public SimpleGetDescriptor<T> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			this._Routing = routing;
			return this;
		}

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SimpleGetDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions)
		{
			if (this._Fields == null)
				this._Fields = new List<string>();
			foreach (var e in expressions)
				this._Fields.Add(new PropertyNameResolver().Resolve(e));
			return this;
		}

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SimpleGetDescriptor<T> Fields(params string[] fields)
		{
			this._Fields = fields;
			return this;
		}
	}
}