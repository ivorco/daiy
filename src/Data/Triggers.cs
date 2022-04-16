using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy.Data
{
    public interface IRoutineTrigger
    {
        bool Validate();

        // Smart routine so cooking is trigger when the stove is on
        // Day routine that is active only on certain days
    }

    public class RoutineTriggerOperation : IRoutineTrigger
    {
        public IRoutineTrigger RoutineTrigger1 { get; set; }
        public IRoutineTrigger RoutineTrigger2 { get; set; }
        private Func<IRoutineTrigger, IRoutineTrigger, bool> ValidateAction { get; set; }

        internal RoutineTriggerOperation(IRoutineTrigger routineTrigger1, IRoutineTrigger routineTrigger2,
                                            Func<IRoutineTrigger, IRoutineTrigger, bool> validate)
        {
            RoutineTrigger1 = routineTrigger1;
            RoutineTrigger2 = routineTrigger2;
            ValidateAction = validate;
        }

        public bool Validate()
        {
            return ValidateAction.Invoke(RoutineTrigger1, RoutineTrigger2);
        }
    }

    public static class RoutineTriggerOR
    {
        public static RoutineTriggerOperation OR(this IRoutineTrigger routineTrigger, IRoutineTrigger otherRoutineTrigger) =>
            new RoutineTriggerOperation(routineTrigger, otherRoutineTrigger,
                (r1, r2) => r1.Validate() || r2.Validate());
    }

    public static class RoutineTriggerAND
    {
        public static RoutineTriggerOperation OR(this IRoutineTrigger routineTrigger, IRoutineTrigger otherRoutineTrigger) =>
            new RoutineTriggerOperation(routineTrigger, otherRoutineTrigger,
                (r1, r2) => r1.Validate() && r2.Validate());
    }

    /// <summary>
    /// For routines where I need to manually enable/disable
    /// F.E. work routine when employed or unemployed
    /// </summary>
    public class RoutineTriggerSelectable : IDescription, IRoutineTrigger
    {
        public string Description { get; set; } = null;
        public string ID { get; set; }
        public Func<bool> Select { get; set; }

        public bool Validate() => Select.Invoke();
    }

    public class WebhookTrigger<T> : IRoutineTrigger where T : IComparable
    {
        public string ID { get; set; }
        public IRange<T> Range { get; set; }

        public bool Validate() => false;
    }

    public class DaysOfTheWeekRoutineTrigger : IRoutineTrigger
    {
        public InListRange<DayOfWeek> Days { get; set; }

        public bool Validate() => Days.IsInRange(DateTime.Now.DayOfWeek);
    }
}
