namespace Nest
{
	public abstract class QueryDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IQuery
		where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IQuery
	{
		protected abstract bool Conditionless { get; }

		double? IQuery.Boost { get; set; }

		bool IQuery.Conditionless => Conditionless;

		bool IQuery.IsStrict { get; set; }

		bool IQuery.IsVerbatim { get; set; }

		bool IQuery.IsWritable => Self.IsVerbatim || !Self.Conditionless;
		string IQuery.Name { get; set; }

		public TDescriptor Boost(double? boost) => Assign(a => a.Boost = boost);

		public TDescriptor Name(string name) => Assign(a => a.Name = name);

		public TDescriptor Strict(bool strict = true) => Assign(a => a.IsStrict = strict);

		public TDescriptor Verbatim(bool verbatim = true) => Assign(a => a.IsVerbatim = verbatim);
	}
}
