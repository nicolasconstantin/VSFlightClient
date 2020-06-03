using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapiclient2.Utility
{
    public abstract class DishTemplate
    {
        public string[] MyDish()
        {
            string Starter = HaveStarter();
            string Main = HaveMainMenu();
            string Accompaniment = HaveAccompaniment();
            string Dessert = HaveDessert();
            string Drink = HaveDrink();

            return new string[] { Starter, Main, Accompaniment, Dessert, Drink };
        }

        string HaveDrink() { return "some water"; }

        public abstract string HaveStarter();
        public abstract string HaveMainMenu();
        public abstract string HaveAccompaniment();
        public abstract string HaveDessert();
    }
}
