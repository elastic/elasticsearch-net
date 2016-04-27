using System;
using System.Collections.Generic;

namespace Nest
{
	public class ProcessorsDescriptor : DescriptorPromiseBase<ProcessorsDescriptor, IList<IProcessor>>
	{
		public ProcessorsDescriptor() : base(new List<IProcessor>()) { }

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


		public ProcessorsDescriptor Split<T>(Func<SplitProcessorDescriptor<T>, ISplitProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SplitProcessorDescriptor<T>())));


		public ProcessorsDescriptor Trim<T>(Func<TrimProcessorDescriptor<T>, ITrimProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new TrimProcessorDescriptor<T>())));


		public ProcessorsDescriptor Uppercase<T>(Func<UppercaseProcessDescriptor<T>, IUppercaseProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new UppercaseProcessDescriptor<T>())));
	}
}
