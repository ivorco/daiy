using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy.Data
{
    public interface IRange<T> where T : IComparable
    {
        bool IsInRange(T val);
    }

    public class InListRange<T> : IRange<T> where T : IComparable
    {
        public InListRange(List<T> values)
        {
            Values = values;
        }

        public List<T> Values { get; set; }

        public bool IsInRange(T val) => Values.Any(listVal => val.CompareTo(listVal) == 0);
    }

    public class IsRange<T> : IRange<T> where T : IComparable
    {
        public IsRange(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        public bool IsInRange(T val) => val.CompareTo(Value) == 0;
    }

    public class MinMaxRange<T> : IRange<T> where T : IComparable
    {
        public MinMaxRange(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min { get; set; }
        public T Max { get; set; }

        public bool IsInRange(T val) => val.CompareTo(Min) >= 0 && val.CompareTo(Max) <= 0;
    }
}
