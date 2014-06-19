using System;
using System.Globalization;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public class BulkIndexDescriptor<T> : BaseBulkOperation
		 where T : class
	{

		internal override Type _ClrType { get { return typeof(T); } }
		internal override string _Operation { get { return "index"; } }
		internal override object _Object { get; set; }

		[JsonProperty(PropertyName = "_percolate")]
		internal string _Percolate { get; set; }

		internal override string GetIdForObject(ElasticInferrer inferrer)
		{
			if (!this._Id.IsNullOrEmpty())
				return this._Id;
			
			return inferrer.Id((T)_Object);

		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkIndexDescriptor<T> Index(string index)
		{
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkIndexDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkIndexDescriptor<T> Type(Type type)
		{
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkIndexDescriptor<T> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkIndexDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// The object to index, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkIndexDescriptor<T> Object(T @object)
		{
			this._Object = @object;
			return this;
		}


		public BulkIndexDescriptor<T> Version(string version)
		{
			this._Version = version; 
			return this;
		}

		public BulkIndexDescriptor<T> VersionType(string versionType)
		{
			this._VersionType = versionType;
			return this;
		}

		public BulkIndexDescriptor<T> VersionType(VersionTypeOptions versionType)
		{
			this._VersionType = versionType.GetStringValue();
			return this;
		}

		public BulkIndexDescriptor<T> Routing(string routing)
		{
			this._Routing = routing; 
			return this;
		}

		public BulkIndexDescriptor<T> Percolate(string percolate)
		{
			this._Percolate = percolate; 
			return this;
		}

		public BulkIndexDescriptor<T> Parent(string parent)
		{
			this._Parent = parent;
			return this;
		}

		public BulkIndexDescriptor<T> Parent(int parent)
		{
			this.Parent(parent.ToString(CultureInfo.InvariantCulture));
			return this;
		}

		public BulkIndexDescriptor<T> Timestamp(long timestamp)
		{
			this._Timestamp = timestamp; 
			return this;
		}

		public BulkIndexDescriptor<T> Ttl(string ttl)
		{
			this._Ttl = ttl; 
			return this;
		}
	}
}