using ConsoleApp1.Experiment_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Experiment_Process
{
    public static class ExperimentProcessor {
        public static ExperimentOutput CreateExperiment(ExperimentInput experimentInput) {
            var experiment = new ExperimentOutput {
                Title = experimentInput?.Title,
                PlateSize = experimentInput?.PlateSize ?? 0,
                NumOfPlates = experimentInput?.NumOfPlates ?? 0,
                IsValid = true,
                Errors = "",
                Results = null,
            };

            try {
                // Validate input
                ValidateAllInput(experimentInput);
                // Calculate experiment
                experiment.Results = CalculateExperiment(experimentInput);
            } catch (Exception e) {
                experiment.IsValid = false;
                experiment.Errors = e.Message;
            }
            return experiment;
        }

        public static ExperimentResult[,,] CalculateExperiment(ExperimentInput experimentInput)
        {
            // Calculate result
            var plateDimension = TestPlateSize.PlateSizeDict[experimentInput.PlateSize];
            var experimentResult = new ExperimentResult[experimentInput.NumOfPlates, plateDimension.Rows, plateDimension.Columns];

            // Experiment Result

            var plate = 0;
            var row = 0;
            var col = 0;
            var batch = 0;

            var expCount = experimentInput.Samples.Length;

            for (int expIndex = 0; expIndex < expCount; expIndex++) {
                for (int replIndex = 0; replIndex < experimentInput.Replicants[expIndex]; replIndex++) {
                    // new Batch is created
                    batch++;
                    for (int sampleIndex = 0; sampleIndex < experimentInput.Samples[expIndex].Length; sampleIndex++) {
                        for (int reagentIndex = 0; reagentIndex < experimentInput.Reagents[expIndex].Length; reagentIndex++) {
                            experimentResult[plate, row, col] = new ExperimentResult {
                                Batch = batch,
                                ExperimentId = expIndex + 1,
                                Reagent = experimentInput.Reagents[expIndex][reagentIndex],
                                Sample = experimentInput.Samples[expIndex][sampleIndex],
                                Replications = replIndex + 1,
                            }; 

                            col++;
                            // Check that indexes are inside the plate dimensions
                            if (col >= plateDimension.Columns) {
                                col = 0;
                                row++;

                                if (row >= plateDimension.Rows) {
                                    col = 0;
                                    row = 0;
                                    plate++;
                                }
                            }
                        }
                    } 
                }
            }
            return experimentResult;
        }

        public static void ValidateAllInput(ExperimentInput experimentInput) {
            var errors = new List<string>();

            if(experimentInput == null){
                throw new InvalidOperationException("Experiment input is empty.");
            }

            //check valid plate size
            try{
                var plateSize = TestPlateSize.PlateSizeDict[experimentInput.PlateSize];
            }
            catch (Exception) {
                errors.Add($"Invalid plate size ({ experimentInput.PlateSize }).");
            }

            //check valid number of plates
            if(experimentInput.NumOfPlates <= 0){
                errors.Add($"Invalid number of plates ({ experimentInput.NumOfPlates }).");
            }

            //Check all data has same number of experiments
            if(experimentInput.Samples?.Length != experimentInput.Reagents?.Length ||
               experimentInput.Samples?.Length != experimentInput.Replicants.Length){
                errors.Add("Invalid input: Number of Samples, Reagents or Replications is not the same.");
            }

            //check that experiment data exists
            if(experimentInput.Samples?.Length == 0 || experimentInput.Reagents?.Length == 0 || experimentInput.Replicants?.Length == 0)
            {
                errors.Add("Invalid input: Samples, Reagents or Replications canno't be empty.");
            }

            var maxPlateSpaces = experimentInput.PlateSize * experimentInput.NumOfPlates;
            var numOfResults = 0;

            //get number of all results, assume array elements are not empty
            try
            {
                for (int exp = 0; exp < experimentInput.Samples.Length; exp++) {
                    numOfResults += experimentInput.Samples[exp].Length * experimentInput.Reagents[exp].Length * experimentInput.Replicants[exp];

                    //check if sample names are uniqe(case-sensitive)
                    if(experimentInput.Samples[exp].Length != experimentInput.Samples[exp].Distinct().Count())
                    {
                        errors.Add("Invalid Input: Samples must be unique");
                    }

                    //check if reagent names are unique(case-sensitive)
                    if(experimentInput.Reagents[exp].Length != experimentInput.Reagents[exp].Distinct().Count())
                    {
                        errors.Add("Invalid Input: Reagents must be uniqe");
                    }

                    //check that repetitions are not 0
                    if(experimentInput.Replicants[exp] <= 0)
                    {
                        errors.Add("Invalid Input: Number of Replications must be greater than 0.");
                    }
                }
            }
            catch (Exception e)
            {
                errors.Add($"Error validating: ({ e.Message }).");
            }
        
            //check that number of results is less than max number of available spaces
            if(numOfResults > maxPlateSpaces)
            {
                errors.Add($"Number of results ({ numOfResults }) is greater than number of available spaces ({ maxPlateSpaces }).");
            }

            if(errors.Count > 0)
            {
                throw new InvalidOperationException($"Invalid experiment input { experimentInput.Title }:\n{ string.Join("\n", errors) }");
            }

        }  
    }
}



