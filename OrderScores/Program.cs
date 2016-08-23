using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScoreSorter;

namespace OrderScores
{
    class Program
    {
        static void Main(string[] args)
        {
            if (String.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("No file provided.");
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("File provided does not exist.");
            }
            else
            {
                List<ScoreSorter.Score> listScores = null;
                using (TextReader textReader = File.OpenText(args[0]))
                {
                    List<string> listErrors = new List<string>();
                    ScoreSorter.Scorer scorer = ScoreSorter.Scorer.createScorer(textReader, (error) => { listErrors.Add(error); });

                    if (listErrors.Count > 0)
                    {
                        Console.WriteLine("Errors in the input file were detected");
                        listErrors.ForEach(error => Console.WriteLine(error));
                    }
                    else
                    {
                        listScores = scorer.sortedScores;
                    }
                }

                if (listScores != null)
                {
                    string strOutFile = Path.GetFileNameWithoutExtension(args[0]) + "-graded.txt";
                    if (File.Exists(strOutFile))
                    {
                        File.Delete(strOutFile);
                    }
                    using (TextWriter textWriter = File.CreateText(strOutFile))
                    {
                        listScores.ForEach(score => textWriter.WriteLine(score.lastName + ", " + score.firstName + ", " + score.score));
                    }
                    Console.WriteLine("Scores have been graded");
                }
            }
        }
    }
}
