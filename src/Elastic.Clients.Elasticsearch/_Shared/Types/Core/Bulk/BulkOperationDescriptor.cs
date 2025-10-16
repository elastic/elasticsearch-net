// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public abstract class BulkOperationDescriptor<TDescriptor>
	where TDescriptor : BulkOperationDescriptor<TDescriptor>
{
	protected TDescriptor Self => (TDescriptor)this;
	internal BulkOperation Instance { get; }

	public static implicit operator BulkOperation(BulkOperationDescriptor<TDescriptor> descriptor) => descriptor.Instance;

	internal BulkOperationDescriptor(BulkOperation instance)
	{
		Instance = instance;
	}

	public TDescriptor Id(Id id)
	{
		Instance.Id = id;
		return Self;
	}

	public TDescriptor IfSequenceNumber(long? ifSequenceNumber)
	{
		Instance.IfSequenceNumber = ifSequenceNumber;
		return Self;
	}

	public TDescriptor IfPrimaryTerm(long? ifPrimaryTerm)
	{
		Instance.IfPrimaryTerm = ifPrimaryTerm;
		return Self;
	}

	public TDescriptor Index(IndexName index)
	{
		Instance.Index = index;
		return Self;
	}

	public TDescriptor Index<T>()
	{
		Instance.Index = typeof(T);
		return Self;
	}

	public TDescriptor RequireAlias(bool? requireAlias = true)
	{
		Instance.RequireAlias = requireAlias;
		return Self;
	}

	public TDescriptor Routing(Routing routing)
	{
		Instance.Routing = routing;
		return Self;
	}

	public TDescriptor Version(long version)
	{
		Instance.Version = version;
		return Self;
	}

	public TDescriptor VersionType(VersionType? versionType)
	{
		Instance.VersionType = versionType;
		return Self;
	}
}
