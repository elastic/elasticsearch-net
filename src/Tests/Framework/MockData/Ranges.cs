using System;
using Bogus;
using Nest;

namespace Tests.Framework.MockData
{
	public class Ranges
	{
		public DateRange Dates { get; set; }
		public DoubleRange Doubles { get; set; }
		public FloatRange Floats { get; set; }
		public IntegerRange Integers { get; set; }
		public LongRange Longs { get; set; }

		//for deserialization
		public Ranges() { }

		private Ranges(Faker faker)
		{
			Func<bool> r = () => faker.Random.Bool();
			SetDates(faker, r);
			SetDoubles(faker, r);
			SetFloats(faker, r);
			SetIntegers(faker, r);
			SetLongs(faker, r);
		}

		private void SetDates(Faker faker, Func<bool> r)
		{
			var past = faker.Date.Past(faker.Random.Int(1, 19));
			var future = faker.Date.Future(faker.Random.Int(1, 10), past);
			var d = new DateRange();
			SwapAssign(r(), past, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
			SwapAssign(r(), future, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
			this.Dates = d;
		}
		private void SetDoubles(Faker faker, Func<bool> r)
		{
			var low = faker.Random.Double(-121, 10000);
			var high = faker.Random.Double(low, Math.Abs(low * 10)) + 2;
			var d = new DoubleRange();
			SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
			SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
			this.Doubles = d;
		}
		private void SetFloats(Faker faker, Func<bool> r)
		{
			var low = faker.Random.Float(-2000, 10000);
			var high = faker.Random.Float(low, Math.Abs(low * 10)) + 2;
			var d = new FloatRange();
			SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
			SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
			this.Floats = d;
		}
		private void SetIntegers(Faker faker, Func<bool> r)
		{
			var low = faker.Random.Int(-100, 10000);
			var high = faker.Random.Int(low, Math.Abs(low * 10)) + 2;
			var d = new FloatRange();
			SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
			SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
			this.Floats = d;
		}
		private void SetLongs(Faker faker, Func<bool> r)
		{
			var low = faker.Random.Long(-100, 10000);
			var high = faker.Random.Long(low, Math.Abs(low * 10)) + 2;
			var d = new LongRange();
			SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
			SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
			this.Longs = d;
		}

		private static void SwapAssign<T>(bool b, T value, Action<T> first, Action<T> second)
		{
			if (b) first(value);
			else second(value);
		}

		public static Faker<Ranges> Generator { get; } =
			new Faker<Ranges>()
				.CustomInstantiator((f) => new Ranges(f))
			;
	}
}
