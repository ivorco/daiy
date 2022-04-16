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
                    new DailyScheme{Description="Daily"},
                    new DailyScheme{Description="Work"},
                    new DailyScheme{Description="Pets", SchemeTrigger= uiSelectables.CreateRoutineTrigger() },
                    new DailyScheme{Description="Sport"},
            }
            };
        }
    }
}
