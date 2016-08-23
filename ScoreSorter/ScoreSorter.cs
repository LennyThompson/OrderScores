using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace ScoreSorter
{
    public class Score
    {
        public Score(string strFirstName, string strLastName, int nScore)
        {
            firstName = strFirstName;
            lastName = strLastName;
            score = nScore;
        }
        public string firstName
        {
            get;
            set;
        }

        public string lastName
        {
            get;
            set;
        }

        public int score
        {
            get;
            set;
        }
    }
    public class Scorer
    {
        static Regex SCORE_PARSER = new Regex("^([A-Z]+),\\s([A-Z]+),\\s(\\d+)");
        public static Scorer createScorer(TextReader readScores, Action<string> errorHandler)
        {
            Scorer scrorerReturn = new Scorer();
            string strNextScore;
            int nLineCount = 1;
            while ((strNextScore = readScores.ReadLine()) != null)
            {
                Match matchScore = SCORE_PARSER.Match(strNextScore);
                if(matchScore.Success)
                {
                    int nScore = 0;
                    if(!Int32.TryParse(matchScore.Groups[3].ToString(), out nScore))
                    {
                        errorHandler("Unable to match score " + strNextScore + " formatting, on line " + nLineCount);
                    }
                    scrorerReturn.addScore(new Score(matchScore.Groups[2].ToString(), matchScore.Groups[1].ToString(), nScore));
                }
                else
                {
                    errorHandler("Unable to match score to " + strNextScore + ", on line " + nLineCount);
                }
                ++nLineCount;
            }
            if(scrorerReturn.m_listScores.Count == 0)
            {
                errorHandler("No scores were recorded");
            }
            return scrorerReturn;
        }

        public List<Score> scores
        {
            get { return m_listScores; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Score> sortedScores
        {
            get
            {
                return m_listScores.OrderByDescending(score => score.score).ThenBy(score => score.lastName).ThenBy(score => score.firstName).ToList();
            }
        }
        private void addScore(Score score)
        {
            m_listScores.Add(score);
        }

        private Scorer()
        {
            m_listScores = new List<Score>();
        }

        private List<Score> m_listScores;
    }
}
