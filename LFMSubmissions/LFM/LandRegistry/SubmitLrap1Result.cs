﻿using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry
{
    public class SubmitLrap1Result
    {
        public SubmitLrap1Command Command { get; set; }
        public override string ToString()
        {
            return Command.ApplicationId;
        }
    }
}