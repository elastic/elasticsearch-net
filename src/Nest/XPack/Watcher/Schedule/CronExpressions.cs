// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScheduleFormatter<ICronExpressions, CronExpressions, CronExpression>))]
	public interface ICronExpressions : ISchedule, IEnumerable<CronExpression> { void Add(CronExpression expression); }

	public class CronExpressions : ScheduleBase, ICronExpressions
	{
		private List<CronExpression> _expressions;

		public CronExpressions(IEnumerable<CronExpression> expressions) => _expressions = expressions?.ToList();

		public CronExpressions(params CronExpression[] expressions) => _expressions = expressions?.ToList();

		public CronExpressions() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<CronExpression> GetEnumerator() => _expressions?.GetEnumerator()
			?? Enumerable.Empty<CronExpression>().GetEnumerator();

		public void Add(CronExpression expression)
		{
			if (_expressions == null)
				_expressions = new List<CronExpression>();
			_expressions.Add(expression);
		}

		internal override void WrapInContainer(IScheduleContainer container) => container.CronExpressions = this;

		public static implicit operator CronExpressions(CronExpression[] cronExpressions) => new(cronExpressions);
	}

	public class CronExpressionsDescriptor : DescriptorPromiseBase<CronExpressionsDescriptor, ICronExpressions>
	{
		public CronExpressionsDescriptor() : base(new CronExpressions()) { }

		public CronExpressionsDescriptor Add(CronExpression cronExpression) =>	Assign(cronExpression, (a, v) => a.Add(v));
	}
}
