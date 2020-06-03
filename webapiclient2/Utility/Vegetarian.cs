using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapiclient2.Utility
{
    public class Vegetarian : DishTemplate
    {
        public override string HaveAccompaniment()
        {
            return "tomato sauce";
        }

        public override string HaveDessert()
        {
            return "a lemon cake";
        }

        public override string HaveMainMenu()
        {
            return "spaghetti";
        }

        public override string HaveStarter()
        {
            return "a vegetable soup";
        }
    }
}
