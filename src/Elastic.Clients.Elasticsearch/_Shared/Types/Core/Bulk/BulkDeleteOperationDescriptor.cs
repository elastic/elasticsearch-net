// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public class BulkDeleteOperationDescriptor :
	BulkOperationDescriptor<BulkDeleteOperationDescriptor>
{
	private new BulkDeleteOperation Instance => (BulkDeleteOperation)base.Instance;

	public static implicit operator BulkDeleteOperation(BulkDeleteOperationDescriptor descriptor) => descriptor.Instance;

	public BulkDeleteOperationDescriptor() :
		base(new BulkDeleteOperation(null!))
	{
	}

	public BulkDeleteOperationDescriptor(Id id) :
		base(new BulkDeleteOperation(id))
	{
	}
}

public sealed class BulkDeleteOperationDescriptor<TDocument> :
	BulkOperationDescriptor<BulkDeleteOperationDescriptor>
{
	private new BulkDeleteOperation<TDocument> Instance => (BulkDeleteOperation<TDocument>)base.Instance;

	public static implicit operator BulkDeleteOperation<TDocument>(BulkDeleteOperationDescriptor<TDocument> descriptor) => descriptor.Instance;

	public BulkDeleteOperationDescriptor() :
		base(new BulkDeleteOperation<TDocument>(null!))
	{
	}

	public BulkDeleteOperationDescriptor(Id id) :
		base(new BulkDeleteOperation<TDocument>(id))
	{
	}

	public BulkDeleteOperationDescriptor(TDocument documentToDelete) :
		this(new Id(documentToDelete))
	{
		Instance.Routing = new Routing(documentToDelete);
		Instance.Index = IndexName.From<TDocument>();
	}
}
