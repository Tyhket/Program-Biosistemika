using ConsoleApp1.Experiment_Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Experiment_Tests
{
    public static class TestPlateSize {
        public static Dictionary<int, ExperimentPlateSize> PlateSizeDict { get; } = new Dictionary<int, ExperimentPlateSize> {
            {96, new ExperimentPlateSize { Rows = 8, Columns = 12 } },
            {384, new ExperimentPlateSize { Rows = 16, Columns = 24 }  },
        };
    }

    public static class TestExperimentInput
    {
        public static Dictionary<string, ExperimentInput> Experiments { get; set; } = new Dictionary<string, ExperimentInput>
        {
           { "experiment1", new ExperimentInput {
                Title = "Experiment1",
                PlateSize = 96,
                Samples = new string[][] {
                    new string[] { "1", "2", "3" },
                    new string[] { "1", "4", "3" },
                },
                Reagents = new string[][] {
                    new string[] { "P" },
                    new string[] { "G", "R", "7" },
                },
                Replicants = new int[] { 24, 10 },
                NumOfPlates = 2
                }
           },
           { "experiment2", new ExperimentInput {
                Title = "Experiment2",
                PlateSize = 96,
                Samples = new string[][] {
                    new string[] { "1", "2", "3" },
                    new string[] { "1", "3", "4" },
                },
                Reagents = new string[][] {
                    new string[] { "X", "Y" },
                    new string[] { "Y", "Z" },
                },
                Replicants = new int[] { 1, 3 },
                NumOfPlates = 1
           }
           },
            { "invalid1", new ExperimentInput {
                Title = "Invalid Experiment 1",
                PlateSize = 96,
                Samples = new string[][] {
                    new string[] { "1", "2", "3" },
                    new string[] { "1", "3", "1" },
                },
                Reagents = new string[][] {
                    new string[] { "X", "Y" },
                    new string[] { "Y", "Z" },
                },
                Replicants = new int[] { 1, 3 },
                NumOfPlates = 1
            }
            },
            { "invalid2", new ExperimentInput {
                Title = "Invalid Experiment 2",
                PlateSize = 196,
                Samples = new string[][] {
                    new string[] { "1", "2", "3" },
                    new string[] { "1", "3", "4" },
                },
                Reagents = new string[][] {
                    new string[] { "X", "Y" },
                    new string[] { "Y", "Z" },
                },
                Replicants = new int[] { 1 },
                NumOfPlates = 5
            }
            },
            { "invalid-null", null },
        };
    }
}
