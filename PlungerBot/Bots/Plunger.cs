using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlungerBot.Bots
{
    [Serializable]
    public class Plunger
    {
        [Prompt("What should I calculate? {||}")]
        public PlungerCalculationType CalculationType { get; set; }

        [Prompt("Please enter the given {&}:")]
        public double? Force { get; set; }

        public double? Area { get; set; }

        public double? Pressure { get; set; }


        public void Calculate()
        {
            switch (CalculationType)
            {
                case PlungerCalculationType.Force:
                    if (Area.HasValue && Pressure.HasValue)
                    {
                        Force = Area * Pressure;
                    }
                    break;
                case PlungerCalculationType.Area:
                    if (Force.HasValue && Pressure.HasValue)
                    {
                        Area = Force / Pressure;
                    }
                    break;
                case PlungerCalculationType.Pressure:
                    if (Force.HasValue && Area.HasValue)
                    {
                        Pressure = Force / Area;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            switch (CalculationType)
            {
                case PlungerCalculationType.Force:
                    return $"The force is {Force:f2} N.";
                case PlungerCalculationType.Area:
                    return $"The area is {Area:f2} cm².";
                case PlungerCalculationType.Pressure:
                    return $"The pressure is {Pressure:f2} bar.";
            }
            return "No result";
        }
    }
}