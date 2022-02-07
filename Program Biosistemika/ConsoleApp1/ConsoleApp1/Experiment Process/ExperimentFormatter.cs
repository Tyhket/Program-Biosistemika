using ConsoleApp1.Experiment_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Experiment_Process
{
    public static class ExperimentFormatter{
        public static void PrintToConsole(ExperimentOutput experimentOutput){
            Console.WriteLine($"Experiment [{experimentOutput.Title}]: ({experimentOutput.NumOfPlates} / {experimentOutput.PlateSize})");

            if (experimentOutput.IsValid){
                Console.WriteLine("Result OK ...");

                var plateDimension = TestPlateSize.PlateSizeDict[experimentOutput.PlateSize];

                for(int plateIndex = 0; plateIndex < experimentOutput.NumOfPlates; plateIndex++) {
                    Console.WriteLine($"Plate No. {plateIndex + 1}");
                    for(int rowIndex = 0; rowIndex < plateDimension.Rows; rowIndex++) {
                        for(int colIndex = 0; colIndex < plateDimension.Columns; colIndex++) {
                            var cup = experimentOutput.Results[plateIndex, rowIndex, colIndex];
                            if (cup != null) {
                                Console.Write($" {cup.Batch}-{cup.ExperimentId}-{cup.Replications}: {cup.Sample}-{cup.Reagent}");
                            }
                            else {
                                Console.Write(" Null ");
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("*****************************************************");
                    Console.WriteLine();
                    Console.WriteLine("*****************************************************");
                }
            }
            else{
                Console.WriteLine(experimentOutput.Errors);
            }
        }

        //Create HTML output for better representation

        public static string GetResultHtml(ExperimentOutput experimentOutput, string v)
        {
            var sb = new StringBuilder();

            if (experimentOutput.IsValid) {
                
                sb.AppendLine($"<h2>{experimentOutput.Title} (plates: {experimentOutput.NumOfPlates}, Platesize: {experimentOutput.PlateSize}");

                var plateDimensions = TestPlateSize.PlateSizeDict[experimentOutput.PlateSize];
                for(int plateIndex = 0; plateIndex < experimentOutput.NumOfPlates; plateIndex++) {
                    sb.AppendLine($"<h3>Plate No. {plateIndex + 1}</h3>");

                    sb.AppendLine("<table>");
                    for(int rowIndex = 0; rowIndex < plateDimensions.Rows; rowIndex++) {
                        sb.AppendLine("<tr>");
                        for(int colIndex = 0; colIndex < plateDimensions.Columns; colIndex++) {
                            var cup = experimentOutput.Results?[plateIndex, rowIndex, colIndex];
                            if (cup != null) {
                                sb.Append($"<td class='batch-{(cup.Batch - 1) % 10}'>");

                                // sb.Append("<div>batch: ");
                                sb.Append("<div>B ");
                                sb.Append(cup.Batch);
                                sb.Append("</div>");

                                // sb.Append("<div>exp: ");
                                sb.Append("<div>E ");
                                sb.Append(cup.ExperimentId);
                                sb.Append("</div>");

                                // sb.Append("<div>repl: ");
                                sb.Append("<div>R ");
                                sb.Append(cup.Replications);
                                sb.Append("</div>");

                                sb.Append("<div class='result'>");
                                sb.Append(cup.Sample);
                                sb.Append('-');
                                sb.Append(cup.Reagent);
                                sb.AppendLine("</div>");

                                sb.AppendLine("</td>");
                            }
                            else{
                                sb.AppendLine("<td>null</td>");
                            }
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.AppendLine("</table>");
                }
            }
            else {
                sb.AppendLine($"<h2>Error: Invalid experiment input data for experiment {experimentOutput.Title}</h2>");
                sb.AppendLine($"<div>{experimentOutput.Errors}</div>");
            }
            return sb.ToString();
        }
        public static string ResultStyles { get; } = @"
            <style>
                body {
                font-family: Arial;
                }

                 table {
                 border-collapse: collapse;
                 font-size: 12px;
                }

                td {
                border: 1px solid #777777;
                padding: 5px 10px;
                }

                .result {
                font-weight: bold;
                font-size: 16px;
                border-top: 1px solid #aaaaaa;
                text-align: center;
                }

                .batch-0 { 
                background-color: #FF0000;
                }

                .batch-1 { 
                background-color: #FF8F00;
                }

                .batch-2 { 
                background-color: #FFFB00;
                }

                .batch-3 { 
                background-color: #A7FF00;
                }

                .batch-4 { 
                background-color: #3FFF00;
                }

                .batch-5 { 
                background-color: #00FF82;
                }

                .batch-6 { 
                background-color: #00FFCB;
                }

                .batch-7 { 
                background-color: #00A4FF;
                }

                .batch-8 { 
                background-color: #E900FF;
                }

                .batch-9 { 
                background-color: #FF00B5;
                }

                </style>
                ";
    }
}
