using System;
using System.Globalization;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexOperation<T> : IBulkOperation
	{
		[JsonProperty(PropertyName = "_percolate")]
		string Percolate { get; set; }

		T Document { get; set; }
	}

	public class BulkIndexOperation<T> : BulkOperationBase, IIndexOperation<T>
		where T : class
	{
		public BulkIndexOperation(T document)
		{
			this.Document = document;
		}

		public override string Operation { get { return "index"; } }

		public override Type ClrType { get { return typeof(T); } }

		public override object GetBody()
		{
			return this.Document;
		}
		public override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.Id ?? inferrer.Id(this.Document);
		}
		public string Percolate { get; set; }

		public T Document { get; set; }
	}


	public class BulkIndexDescriptor<T> : BulkOperationDescriptorBase, IIndexOperation<T> 
		where T : class
	{
		private IIndexOperation<T> Self { get { return this; } } 

		protected override string BulkOperationType { get { return "index"; } }
		protected override Type BulkOperationClrType { get { return typeof(T); } }
		
		string IIndexOperation<T>.Percolate { get; set; }
		T IIndexOperation<T>.Document { get; set; }

		protected override object GetBulkOperationBody()
		{
			return Self.Document;
		}

		protected override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return Self.Id ?? inferrer.Id(Self.Document);
		}
		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkIndexDescriptor<T> Index(string index)
		{
			Self.Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkIndexDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkIndexDescriptor<T> Type(Type type)
		{
			Self.Type = type;
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
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to index, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkIndexDescriptor<T> Document(T @object)
		{
			Self.Document = @object;
			return this;
		}


        public BulkIndexDescriptor<T> Version(long version)
		{
			Self.Version = version; 
			return this;
		}

		public BulkIndexDescriptor<T> VersionType(VersionType versionType)
		{
			Self.VersionType = versionType;
			return this;
		}

		public BulkIndexDescriptor<T> Routing(string routing)
		{
			Self.Routing = routing; 
			return this;
		}

		public BulkIndexDescriptor<T> Percolate(string percolate)
		{
			Self.Percolate = percolate; 
			return this;
		}

		public BulkIndexDescriptor<T> Parent(string parent)
		{
			Self.Parent = parent;
			return this;
		}

		public BulkIndexDescriptor<T> Parent(int parent)
		{
			this.Parent(parent.ToString(CultureInfo.InvariantCulture));
			return this;
		}

		public BulkIndexDescriptor<T> Timestamp(long timestamp)
		{
			Self.Timestamp = timestamp; 
			return this;
		}

		public BulkIndexDescriptor<T> Ttl(string ttl)
		{
			Self.Ttl = ttl; 
			return this;
		}

	}
}