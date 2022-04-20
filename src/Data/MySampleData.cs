using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy.Data
{
    public class MySampleData
    {
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
        // Duolingo
        // Go out/do personal projects
        // Take out the dog
        // Go out/do personal projects
        // Stretches
        // Sleep

        public MySampleData()
        {
            var uiSelectables = new UISelectables();

            var config = new daiyConfig
            {
                Schemes = new List<DailyScheme> {
                    new DailyScheme{Description="Home", SchemeTrigger= uiSelectables.CreateRoutineTrigger()},
                    new DailyScheme{Description="Work", SchemeTrigger= uiSelectables.CreateRoutineTrigger()},
                    new DailyScheme{Description="Sport", SchemeTrigger= uiSelectables.CreateRoutineTrigger(),
                        Routines= new List<DailyRoutine>{
                            new DailyRoutine{Id=Guid.NewGuid(), Name="Training", ShouldAlarm= RoutineAlarm.Yes,
                                Reccurence= new Reccurence{ TimesADay= new MinMaxRange<int>(1,2), Fit=ReccurenceFit.AsMuchAsPossible },
                                Duration= new Duration{ DurationTime= TimeSpan.FromMinutes(15) } } } },
            }
            };
        }
    }

    internal class UISelectables
    {
        public UISelectables()
        {
        }

        internal IRoutineTrigger CreateRoutineTrigger()
        {
            throw new NotImplementedException();
        }
    }
}
