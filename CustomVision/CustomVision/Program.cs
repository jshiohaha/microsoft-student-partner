using System;
using System.IO;
using System.Threading;

using Microsoft.Cognitive.CustomVision;
using Microsoft.Cognitive.CustomVision.Models;

namespace CustomVision
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a local image URL: ");
            var imagePath = Console.ReadLine();
            MemoryStream testImage = null;

            try
            {
                testImage = new MemoryStream(File.ReadAllBytes(imagePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Console.WriteLine();
                exitInNSeconds(5);
            }

            Console.Write("Enter Project Id Guid: ");
            var projectIdGuid = Console.ReadLine();
            Guid projectId = Guid.Parse(projectIdGuid);

            Console.Write("Enter Prediction Key Guid: ");
            var predictionKey = Console.ReadLine();


            Console.WriteLine();
            Console.WriteLine();

            // Utilizing the Microsoft.Cognitive.CustomVision NuGet Package to establish Credentials and the Endpoint
            PredictionEndpointCredentials predictionEndpointCredentials = new PredictionEndpointCredentials(predictionKey);
            PredictionEndpoint endpoint = new PredictionEndpoint(predictionEndpointCredentials);

            ImagePredictionResultModel result = endpoint.PredictImage(projectId, testImage);

            Console.WriteLine("Prediction results for the given image are...");
            foreach (ImageTagPrediction prediction in result.Predictions)
            {
                Console.WriteLine($"{prediction.Tag}: {prediction.Probability:P1}");
            }

            exitInNSeconds(5);
        }

        public static void exitInNSeconds(int seconds)
        {
            Console.Write("Exiting in... ");

            for (int a = seconds; a >= 0; a--)
            {
                Console.CursorLeft = 15;
                Console.Write("{0} ", a);    // Add space to make sure to override previous contents
                Thread.Sleep(1000);
            }

            Environment.Exit(0);
        }
    }
}
