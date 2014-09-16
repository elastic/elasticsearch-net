using System;
using System.Collections.Generic;
using System.Globalization;

namespace Nest
{
	public class MoreLikeThisQueryDocumentsDescriptor<T> where T : class
	{
		private readonly bool _allowExplicitIndex;
		internal IList<IMultiGetOperation> GetOperations { get; set; }

		public MoreLikeThisQueryDocumentsDescriptor(bool allowExplicitIndex = true)
		{
			_allowExplicitIndex = allowExplicitIndex;
			this.GetOperations = new List<IMultiGetOperation>();
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get(long id, Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector = null)
		{
			return this.Get(id.ToString(CultureInfo.InvariantCulture), getSelector);
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get(string id, Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector = null) 
		{
			getSelector = getSelector ?? (s => s);
			var descriptor = getSelector(new MultiGetOperationDescriptor<T>(_allowExplicitIndex).Id(id));
			this.GetOperations.Add(descriptor);
			return this;
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		/// <typeparam name="TDocument">Use a different type to lookup</typeparam>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get<TDocument>(long id, Func<MultiGetOperationDescriptor<TDocument>, MultiGetOperationDescriptor<TDocument>> getSelector = null)
			where TDocument : class
		{
			return this.Get<TDocument>(id.ToString(CultureInfo.InvariantCulture), getSelector);
		}
		
		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		/// <typeparam name="TDocument">Use a different type to lookup</typeparam>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get<TDocument>(string id, Func<MultiGetOperationDescriptor<TDocument>, MultiGetOperationDescriptor<TDocument>> getSelector = null) 
			where TDocument : class
		{
			getSelector = getSelector ?? (s => s);
			var descriptor = getSelector(new MultiGetOperationDescriptor<TDocument>(_allowExplicitIndex).Id(id));
			this.GetOperations.Add(descriptor);
			return this;

		}
	}
}