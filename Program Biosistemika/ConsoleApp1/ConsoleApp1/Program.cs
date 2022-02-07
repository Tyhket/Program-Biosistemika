using ConsoleApp1.Experiment_Process;
using ConsoleApp1.Experiment_Tests;
using System;
using System.IO;
using System.Text;
//Html is in folder: ConsoleApp1\bin\debug\net6.0
namespace ProgramBioSistemika
{
    public static class Program
    {
        static void Main(string[] args) {
            
            var sb = new StringBuilder();
            sb.AppendLine(ExperimentFormatter.ResultStyles);
            sb.AppendLine("<h3>Legend: B: Batch, E: Experiment, R: Replicant</h3>");

            foreach (var exp in TestExperimentInput.Experiments) {
                var experiment = ProcessExperiment(exp.Value);
                var html = ExperimentFormatter.GetResultHtml(experiment, "experiment-results.html");
                sb.AppendLine(html);
            }

            File.WriteAllText("experiment-results.html", sb.ToString());
        }

        private static ExperimentOutput? ProcessExperiment(ExperimentInput experimentToProcess) {
            try {
                Console.WriteLine($"Processing: {experimentToProcess?.Title}");
                var experiment = ExperimentProcessor.CreateExperiment(experimentToProcess);
                ExperimentFormatter.PrintToConsole(experiment);
                return experiment;
            }catch(Exception e) {
                Console.WriteLine($"Error processing: {e.Message}");
            }
            Console.WriteLine();

            return default;
        }
    }
}
