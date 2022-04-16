using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daiy.Data
{
    public interface IDescription
    {
        string Description { get; set; }
    }

    public static class DescriptionExtension
    {
        public static void SetDescription(IDescription parent, IDescription child)
        {
            if (child.Description == null)
                child.Description = parent.Description;
        }
    }
}
