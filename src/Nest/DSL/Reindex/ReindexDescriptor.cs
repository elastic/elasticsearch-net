using System;
namespace Nest
{
	public class ReindexDescriptor<T> where T : class
	{
		internal string _ToIndexName { get; set; }
		internal string _FromIndexName { get; set; }
		internal string _Scroll { get; set; }
		
		internal Func<QueryDescriptor<T>, QueryContainer> _QuerySelector { get; set; }

		internal Func<CreateIndexDescriptor, CreateIndexDescriptor> _CreateIndexSelector { get; set; }

		internal PutMappingDescriptor<T> _RootObjectMappingDescriptor { get; set; } 

		/// <summary>
		/// The index into which we're indexing
		/// </summary>
		public ReindexDescriptor<T> ToIndex(string name)
		{
			this._ToIndexName = name;
			return this;
		}

		/// <summary>
		/// The index from which we're reindexing 
		/// </summary>
		public ReindexDescriptor<T> FromIndex(string name)
		{
			this._FromIndexName = name;
			return this;
		}

		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. The scroll parameter is a time value parameter (for example: scroll=5m), indicating for how long the nodes that participate in the search will maintain relevant resources in order to continue and support it. This is very similar in its idea to opening a cursor against a database.
		/// </summary>
		/// <param name="scrollTime">The scroll parameter is a time value parameter (for example: scroll=5m)</param>
		/// <returns></returns>
		public ReindexDescriptor<T> Scroll(string scrollTime)
		{
			scrollTime.ThrowIfNullOrEmpty("scrollTime");
			this._Scroll = scrollTime;
			return this;
		}

		/// <summary>
		/// A query to optionally limit the documents to use for the reindex operation.  
		/// </summary>
		public ReindexDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			this._QuerySelector = querySelector;
			return this;
		}

		/// <summary>
		/// The new index name to reindex too. 
		/// </summary>
		public ReindexDescriptor<T> NewIndexName(string name)
		{
			this._ToIndexName = name;
			return this;
		}

		/// <summary>
		/// CreateIndex selector, will be passed the a descriptor initialized with the settings from
		/// the index we're reindexing from
		/// </summary>
		public ReindexDescriptor<T> CreateIndex(Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector)
		{
			createIndexSelector.ThrowIfNull("createIndexSelector");
			this._CreateIndexSelector = createIndexSelector;
			return this;
		}

	}
}