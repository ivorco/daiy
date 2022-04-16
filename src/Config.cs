using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy
{
    // The app's config is a list of schemes (work, pet, food, sport)

    // TODO: When adding non-daily tasks, enable deadlines so tasks come first f.e.
    // TODO: Focus every day for an hour or two, Block calendar for that
    // TODO: Tasks that carry the same timing settings as routines

    public class daiyConfig
    {
        public List<DailyScheme> Schemes { get; set; }
    }

    public class DailyScheme
    {
        public string Description { get; set; }
        public List<DailyRoutine> Routines { get; set; }
        public List<IRoutineTrigger> RoutineTriggers { get; set; }
        /// <summary>
        /// For schemes that trigger on event (abroad), 
        /// those on UI (enable adding checkboxes so I can say if I'm employed)
        /// those on certain days of the week (work)
        /// </summary>
        public IRoutineTrigger SchemeTrigger { get; set; }
    }

    public interface IRoutineTrigger
    {
        bool Validate();

        // I have started eating when I am finished cooking
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
    public class RoutineTriggerSelectable : IRoutineTrigger
    {
        /// <summary>
        /// Default description is inherited from routine/scheme
        /// </summary>
        public string Description { get; set; } = null;
        public bool DefaultValue { get; set; }
        public bool Value { get; set; }

        public bool Validate()
        {
            // TODO: Is checkbox checked
            throw new NotImplementedException();
        }
    }

    public class DailyRoutine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Reccurence Reccurence { get; set; }
        public List<Constraint> Constraints { get; set; }
        public List<Descriptor> Descriptors { get; set; }
        public RoutineAlarm ShouldAlarm { get; set; }
        public Duration Duration { get; set; }

        // Add duration fit
        // Doing tasks while at work
    }

    public struct Duration
    {
        public TimeSpan DurationTime { get; set; }
        /// <summary>
        /// The amount of time that if other Routines are recognized, will end this routine
        /// Also, for recognized routines, will only Trigger if minimum has reached
        /// </summary>
        public TimeSpan MinimumInterval { get; set; }
    }

    public enum RoutineAlarm
    {
        No = 0,
        Yes = 1,
        IfForgot = 2,
    }

    public class Descriptor
    {
        // If it's food that's in morning hours, it's probably breakfest
        // Sometimes it's not a meal but a snack
        // Training late is stretches
    }

    public class Constraint
    {
        public Guid BeforeRoutine { get; set; }
        public Guid WhileRoutine { get; set; }
        public Guid AfterRoutine { get; set; }
        public ConstraintFit ConstraintFit { get; set; }

        // Sport happens before I eat, or after I eat something small
        // I have to take the dog out before going out or after waking up (???)
        // I want to drink coffee after I eat
        // If I ate an ice cream, I might want a meal with that
        // Before drinking coffee - eat something
        // I eat right after I cook
    }

    public enum ConstraintFit
    {
        RightAway = 0,
        TakeSomeTime = 1,
        AnytimeInConstriant = 2,
    }

    public class Reccurence
    {
        public Range<int> TimesADay { get; set; }
        public ReccurenceFit Fit { get; set; }
    }

    public enum ReccurenceFit
    {
        Exactly = 0,
        AtLeast = 1,
        Approximately = 2,
        AsMuchAsPossible = 3,
    }

    public struct Range<T> where T : IComparable
    {
        public T Min { get; set; }
        public T Max { get; set; }

        public bool IsInRange(T val) => val.CompareTo(Min) >= 0 && val.CompareTo(Max) <= 0;
    }

    // Day for example
    // ---------------
    // Do stretches and training
    // Take the dog out - around morning
    // Eat something
    // Drink coffee - right after I take out the dog (the machine has to be turn on when I take the dog out)
    // Work - on workdays, Personal projects on weekends
    // Cook lunch
    // Eat lunch
    // Work
    // Do some personal tasks
    // Drink more coffee
    // Work
    // Take the dog out
    // Do sport
    // Cook dinner
    // Eat dinner
    // Do personal tasks
    // Go out/do personal projects
    // Take out the dog
    // Go out/do personal projects
    // Stretches
    // Sleep

    // Duolingo
    // Doing tasks while other tasks (at work f.e.)
}
