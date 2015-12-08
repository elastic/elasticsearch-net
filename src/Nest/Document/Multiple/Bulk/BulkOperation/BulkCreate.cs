using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IBulkCreateOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }
	}

	public class BulkCreateOperation<T> : BulkOperationBase, IBulkCreateOperation<T>
		where T : class
	{
		public BulkCreateOperation(T document)
		{
			this.Document = document;
		}

		public override string Operation { get { return "create"; } }

		public override Type ClrType { get { return typeof(T); } } 
		
		public override object GetBody()
		{
			return this.Document;
		}
		public override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.Id ?? inferrer.Id(this.Document);
		}
		public T Document { get; set; }
	}


	public class BulkCreateDescriptor<T> : BulkOperationDescriptorBase, IBulkCreateOperation<T> 
		where T : class
	{
		private IBulkCreateOperation<T> Self => this; 

		protected override string BulkOperationType => "create";
		protected override Type BulkOperationClrType => typeof(T);

		protected override object GetBulkOperationBody()
		{
			return Self.Document;
		}

		protected override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return Self.Id ?? inferrer.Id(Self.Document);
		}

		T IBulkCreateOperation<T>.Document { get; set; }

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkCreateDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty(nameof(index));
			Self.Index = index;
			return this;
		}

		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkCreateDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty(nameof(type));
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkCreateDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull(nameof(type));
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkCreateDescriptor<T> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkCreateDescriptor<T> Id(string id)
		{
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkCreateDescriptor<T> Document(T @object)
		{
			Self.Document = @object;
			return this;
		}


        public BulkCreateDescriptor<T> Version(long? version)
		{
			Self.Version = version; 
			return this;
		}

		public BulkCreateDescriptor<T> VersionType(VersionType versionType)
		{
			Self.VersionType = versionType;
			return this;
		}

		public BulkCreateDescriptor<T> Routing(string routing)
		{
			Self.Routing = routing; 
			return this;
		}

		public BulkCreateDescriptor<T> Parent(string parent)
		{
			Self.Parent = parent; 
			return this;
		}

		public BulkCreateDescriptor<T> Timestamp(long timestamp)
		{
			Self.Timestamp = timestamp; 
			return this;
		}

		public BulkCreateDescriptor<T> Ttl(string ttl)
		{
			Self.Ttl = ttl; 
			return this;
		}

	}
}