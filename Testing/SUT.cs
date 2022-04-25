using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notely_OOD_Project;
using NUnit.Framework;

namespace projectTesting
{
    public class SUT
    {
        public string GetImageLocation(Note.Priority hold)
        {
            string image = "hold";

            if (hold == Note.Priority.Relaxed)
            {
                image = "images/relaxedW.png";

            }
            else if (hold == Note.Priority.Important)
            {
                image = "images/importantW.png";
            }
            else if (hold == Note.Priority.Urgent)
            {
                image = "images/urgentW.png";
            }
            else if (hold == Note.Priority.Critical)
            {
                image = "images/criticalW.png";
            }

            return image;

        }
    }
}
