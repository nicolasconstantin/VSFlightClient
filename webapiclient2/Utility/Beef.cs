using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapiclient2.Utility
{
    public class Beef : DishTemplate
    {
        public override string HaveAccompaniment()
        {
            return "rice and carrot";
        }

        public override string HaveDessert()
        {
            return "a blueberry cake";
        }

        public override string HaveMainMenu()
        {
            return "a beef steak";
        }

        public override string HaveStarter()
        {
            return "a Swiss chocolate";
        }
    }
}
