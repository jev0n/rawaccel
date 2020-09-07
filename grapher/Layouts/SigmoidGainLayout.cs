﻿using grapher.Models.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapher.Layouts
{
    public class SigmoidGainLayout : LayoutBase
    {
        public SigmoidGainLayout()
            : base()
        {
            Name = "SigmoidGain";
            Index = (int)AccelMode.sigmoidgain;

            AccelLayout = new OptionLayout(true, Acceleration);
            CapLayout = new OptionLayout(false, string.Empty);
            WeightLayout = new OptionLayout(false, string.Empty);
            OffsetLayout = new OptionLayout(true, Offset);
            LimExpLayout = new OptionLayout(true, Limit);
            MidpointLayout = new OptionLayout(true, Midpoint);
        }
    }
}
