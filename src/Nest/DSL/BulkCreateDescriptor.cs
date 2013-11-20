using System;
using System.Globalization;
using Nest.Resolvers;

namespace Nest
{
	public class BulkCreateDescriptor<T> : BaseBulkOperation
		 where T : class
	{

		internal override Type _ClrType { get { return typeof(T); } }
		internal override string _Operation { get { return "create"; } }
		internal override object _Object { get; set; }


		private readonly TypeNameResolver _typeNameResolver;

		public BulkCreateDescriptor()
		{
			this._typeNameResolver = new TypeNameResolver();
		}

		internal override string GetIdForObject(ElasticInferrer inferrer)
		{
			if (!this._Id.IsNullOrEmpty())
				return this._Id;

			return inferrer.Id((T)_Object);

		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkCreateDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkCreateDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkCreateDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkCreateDescriptor<T> Id(int id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkCreateDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkCreateDescriptor<T> Object(T @object)
		{
			this._Object = @object;
			return this;
		}


		public BulkCreateDescriptor<T> Version(string version)
		{
			this._Version = version; 
			return this;
		}

		public BulkCreateDescriptor<T> VersionType(string versionType)
		{
			this._VersionType = versionType;
			return this;
		}

		public BulkCreateDescriptor<T> Routing(string routing)
		{
			this._Routing = routing; 
			return this;
		}

		public BulkCreateDescriptor<T> Percolate(string percolate)
		{
			this._Percolate = percolate; 
			return this;
		}

		public BulkCreateDescriptor<T> Parent(string parent)
		{
			this._Parent = parent; 
			return this;
		}

		public BulkCreateDescriptor<T> Timestamp(long timestamp)
		{
			this._Timestamp = timestamp; 
			return this;
		}

		public BulkCreateDescriptor<T> Ttl(string ttl)
		{
			this._Ttl = ttl; 
			return this;
		}
	}
}