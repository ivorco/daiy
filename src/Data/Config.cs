using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy.Data
{
    // Tasks
    // TODO: When adding non-daily tasks, enable deadlines so tasks come first f.e.
    // TODO: Focus every day for an hour or two, Block calendar for that
    // TODO: Tasks that carry the same timing settings as routines

    public class daiyConfig
    {
        /// <summary>
        /// A list of schemes (work, pet, food, sport)
        /// </summary>
        public List<DailyScheme> Schemes { get; set; }
    }

    public class DailyScheme : IDescription
    {
        public string Description { get; set; }
        public List<DailyRoutine> Routines { get; set; }
        /// <summary>
        /// For schemes that trigger on event (abroad), 
        /// those on UI (enable adding checkboxes so I can say if I'm employed)
        /// those on certain days of the week (work)
        /// </summary>
        private IRoutineTrigger _schemeTrigger;
        public IRoutineTrigger SchemeTrigger
        {
            get { return _schemeTrigger; }
            set
            {
                if (value is IDescription descriptable)
                    DescriptionExtension.SetDescription(this, descriptable);

                _schemeTrigger = value;
            }
        }
    }

    public class DailyRoutine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// What kind of routine is it
        /// Based on timing, is food routine breakfest or dinner
        /// </summary>
        public List<Descriptor> Descriptors { get; set; }
        public RoutineAlarm ShouldAlarm { get; set; }

        /// <summary>
        /// How many times a day
        /// </summary>
        public Reccurence Reccurence { get; set; }
        /// <summary>
        /// Before/After other routines
        /// Used to find a suitable time every day
        /// </summary>
        public List<Constraint> Constraints { get; set; }

        public Duration Duration { get; set; }
        /// <summary>
        /// External trigger to start a routine
        /// Stove ended for start eating
        /// </summary>
        public IRoutineTrigger StartedRoutineTrigger { get; set; }
        /// <summary>
        /// External trigger to mark a routine as done
        /// Got back home after walking the dog
        /// </summary>
        public IRoutineTrigger DoneRoutineTrigger { get; set; }
    }

    public struct Duration
    {
        public TimeSpan DurationTime { get; set; }
        /// <summary>
        /// The amount of time that if other Routines are recognized, will end this routine
        /// Also, for recognized routines, will only Trigger if minimum has reached
        /// </summary>
        public TimeSpan MinimumInterval { get; set; }

        // TODO: Add duration fit
        // TODO: Need to rethink about this - do I want to automatically end routines and how exactly?
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

        public string Description { get; set; }
        public MinMaxRange<DateTime> TimeRange { get; set; }
    }

    public class Constraint
    {
        public Guid AtRoutine { get; set; }
        public ConstraintRelation Relation { get; set; }
        public ConstraintFit ConstraintFit { get; set; }

        // Sport happens before I eat, or after I eat something small
        // I have to take the dog out before going out or after waking up (???)
        // I want to drink coffee after I eat
        // If I ate an ice cream, I might want a meal with that
        // Before drinking coffee - eat something
        // I eat right after I cook
    }

    public enum ConstraintRelation
    {
        Before = 0,
        After = 1,
    }

    public enum ConstraintFit
    {
        RightAway = 0,
        TakeSomeTime = 1,
        AnytimeInConstriant = 2,
    }

    public class Reccurence
    {
        public MinMaxRange<int> TimesADay { get; set; }
        public ReccurenceFit Fit { get; set; }
    }

    public enum ReccurenceFit
    {
        Exactly = 0,
        AtLeast = 1,
        Approximately = 2,
        AsMuchAsPossible = 3,
    }
}