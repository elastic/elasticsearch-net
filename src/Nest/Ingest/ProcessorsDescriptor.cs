using System;
using System.Collections.Generic;

namespace Nest
{
	public class ProcessorsDescriptor : DescriptorPromiseBase<ProcessorsDescriptor, IList<IProcessor>>
	{
		public ProcessorsDescriptor() : base(new List<IProcessor>()) { }

		/// <summary>
		/// The ingest attachment plugin lets Elasticsearch extract file attachments in common formats (such as PPT, XLS, and PDF)
		/// by using the Apache text extraction library Tika. You can use the ingest attachment plugin as a replacement
		/// for the mapper attachment plugin. The ingest-attachment plugin must be installed for this functionality.
		/// </summary>
		public ProcessorsDescriptor Attachment<T>(Func<AttachmentProcessorDescriptor<T>, IAttachmentProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new AttachmentProcessorDescriptor<T>())));

		public ProcessorsDescriptor Append<T>(Func<AppendProcessorDescriptor<T>, IAppendProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new AppendProcessorDescriptor<T>())));


		public ProcessorsDescriptor Convert<T>(Func<ConvertProcessorDescriptor<T>, IConvertProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ConvertProcessorDescriptor<T>())));


		public ProcessorsDescriptor Date<T>(Func<DateProcessorDescriptor<T>, IDateProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new DateProcessorDescriptor<T>())));

		public ProcessorsDescriptor Fail(Func<FailProcessorDescriptor, IFailProcessor> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new FailProcessorDescriptor())));

		public ProcessorsDescriptor Foreach<T>(Func<ForeachProcessorDescriptor<T>, IForeachProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ForeachProcessorDescriptor<T>())));

		/// <summary>
		/// The GeoIP processor adds information about the geographical location of IP addresses, based on data from the Maxmind databases.
		/// This processor adds this information by default under the geoip field. The ingest-geoip plugin must be installed for this
		/// functionality.
		/// </summary>
		public ProcessorsDescriptor GeoIp<T>(Func<GeoIpProcessorDescriptor<T>, IGeoIpProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GeoIpProcessorDescriptor<T>())));

		public ProcessorsDescriptor Grok<T>(Func<GrokProcessorDescriptor<T>, IGrokProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GrokProcessorDescriptor<T>())));

		public ProcessorsDescriptor Gsub<T>(Func<GsubProcessorDescriptor<T>, IGsubProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GsubProcessorDescriptor<T>())));


		public ProcessorsDescriptor Join<T>(Func<JoinProcessorDescriptor<T>, IJoinProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new JoinProcessorDescriptor<T>())));

		public ProcessorsDescriptor Lowercase<T>(Func<LowercaseProcessorDescriptor<T>, ILowercaseProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LowercaseProcessorDescriptor<T>())));


		public ProcessorsDescriptor Remove<T>(Func<RemoveProcessorDescriptor<T>, IRemoveProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new RemoveProcessorDescriptor<T>())));


		public ProcessorsDescriptor Rename<T>(Func<RenameProcessorDescriptor<T>, IRenameProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new RenameProcessorDescriptor<T>())));

		public ProcessorsDescriptor Set<T>(Func<SetProcessorDescriptor<T>, ISetProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SetProcessorDescriptor<T>())));

		/// <summary>
		/// The Sort processor sorts the elements of an array ascending or descending. Homogeneous arrays of numbers
		/// will be sorted numerically, while arrays of strings or heterogeneous arrays
		///  of strings and numbers will be sorted lexicographically.
		/// </summary>
		public ProcessorsDescriptor Sort<T>(Func<SortProcessorDescriptor<T>, ISortProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SortProcessorDescriptor<T>())));

		public ProcessorsDescriptor Split<T>(Func<SplitProcessorDescriptor<T>, ISplitProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SplitProcessorDescriptor<T>())));


		public ProcessorsDescriptor Trim<T>(Func<TrimProcessorDescriptor<T>, ITrimProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new TrimProcessorDescriptor<T>())));


		public ProcessorsDescriptor Uppercase<T>(Func<UppercaseProcessDescriptor<T>, IUppercaseProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new UppercaseProcessDescriptor<T>())));
	}
}
