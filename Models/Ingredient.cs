﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Measure
    {
        public double Amount { get; set; }
        public UnitMeasure Us { get; set; }
        public UnitMeasure Metric { get; set; }
    }

    public class UnitMeasure
    {
        public double Amount { get; set; }
        public string UnitShort { get; set; }
        public string UnitLong { get; set; }
    }

    public class ExtendedIngredient
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public Measure Measures { get; set; }
    }
}
