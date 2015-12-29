namespace Nest
{
	public partial interface IScrollRequest 
	{
		Time Scroll { get; set; }
	}
	
	//TODO signal to codegen to not generate constructors for this one
	// Expose scroll id as a property to send via the body
	public partial class ScrollRequest 
	{
		public Time Scroll { get; set; }

		public ScrollRequest(ScrollId scrollId, Time scrollTimeout) : this(scrollId)
		{
			this.Scroll = scrollTimeout;
		}
	}

	public partial class ScrollDescriptor<T> where T : class
	{
		Time IScrollRequest.Scroll { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(Time scroll) => Assign(a => a.Scroll = scroll);
	}
}
