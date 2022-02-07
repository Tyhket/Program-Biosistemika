using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Experiment_Process
{
    public class ExperimentPlateSize
    {
        public int Rows { get; set; }
        public int Columns { get; set;}
    }

    public class ExperimentInput
    {
        public string Title { get; set; }
        public int PlateSize { get; set; }
        public string[][] Samples { get; set; }
        public string[][] Reagents  { get; set; }
        public int[] Replicants { get; set; }
        public int NumOfPlates { get; set; }

    }

    public class ExperimentResult
    {
        public string Sample { get; set; }
        public string Reagent { get; set; }
        public int Replications { get; set; }
        public int Batch { get; set; }
        public int ExperimentId { get; set; }
    }

    public class ExperimentOutput
    {
        public string Title { get; set; }
        public int PlateSize { get; set; }
        public int NumOfPlates { get; set; }
        public ExperimentResult[,,] Results { get; set; }
        public bool IsValid { get; set; }
        public string Errors { get; set; }
    }
}
