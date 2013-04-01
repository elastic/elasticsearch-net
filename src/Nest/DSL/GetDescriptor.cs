using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	public abstract class BaseSimpleGetDescriptor
	{

		internal virtual string _Index { get; set; }
		internal virtual TypeNameMarker _Type { get; set; }
		internal virtual string _Id { get; set; }
		internal virtual IList<string> _Fields { get; set; }

		internal virtual Type _ClrType { get; set; }
	}

	public class SimpleGetDescriptor<T> : BaseSimpleGetDescriptor
	where T : class
	{
		internal override string _Index { get; set; }
		internal override TypeNameMarker _Type { get; set; }
		internal override string _Id { get; set; }
		internal override IList<string> _Fields { get; set; }

		internal override Type _ClrType { get { return typeof(T); } }

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

	public class GetDescriptor<T> : SimpleGetDescriptor<T>
		where T : class
	{

		internal bool? _Realtime;
		internal string _Routing { get; set; }
		internal string _Preference { get; set; }
		internal bool? _Refresh { get; set; }


		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public new GetDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed.
		/// </summary>
		public new GetDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public new GetDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Type = type;
			return this;
		}

		public new GetDescriptor<T> Id(int id)
		{
			return this.Id(id.ToString());
		}

		public new GetDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// The refresh parameter can be set to true in order to refresh the relevant shard before the get operation and make it searchable. Setting it to true should be done after careful thought and verification that this does not cause a heavy load on the system (and slows down indexing).
		/// </summary>
		public GetDescriptor<T> Refresh(bool refresh = true)
		{
			this._Refresh = refresh;
			return this;
		}

		/// <summary>
		/// When indexing using the ability to control the routing, in order to get a document, the routing value should also be provided.
		/// </summary>
		public GetDescriptor<T> Routing(string routing)
		{
			this._Routing = routing;

			return this;
		}

		/// <summary>
		/// By default, the get API is realtime, and is not affected by the refresh rate of the index (when data will become visible for search).
		/// </summary>
		public GetDescriptor<T> Realtime(bool realtime = true)
		{
			this._Realtime = realtime;
			return this;
		}

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public new GetDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions)
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
		public new GetDescriptor<T> Fields(params string[] fields)
		{
			this._Fields = fields;
			return this;
		}

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Custom (string) value: A custom value will be used to guarantee that the same shards
		/// will be used for the same custom value. This can help with “jumping values” 
		/// when hitting different shards in different refresh states. 
		/// A sample value can be something like the web session id, or the user name.
		/// </para>
		/// </summary>
		public GetDescriptor<T> Preference(string preference)
		{
			this._Preference = preference;
			return this;
		}

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed only on the primary shards.
		/// </para>
		/// </summary>
		public GetDescriptor<T> ExecuteOnPrimary()
		{
			this._Preference = "_primary";
			return this;
		}

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will prefer to be executed on a local allocated shard is possible.
		/// </para>
		/// </summary>
		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			this._Preference = "_local";
			return this;
		}

	}
}
