using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapiclient2.Utility
{
    public class Chicken : DishTemplate
    {
        public override string HaveAccompaniment()
        {
            return "potato gratin and carrot";
        }

        public override string HaveDessert()
        {
            return "a vanilla ice cream";
        }

        public override string HaveMainMenu()
        {
            return "a chicken leg";
        }

        public override string HaveStarter()
        {
            return "a mixed salat";
        }
    }
}
