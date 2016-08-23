using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ScoreSorter;
using System.Collections.Generic;

namespace TestScoreSorter
{
    [TestClass]
    public class TestScoreSorter
    {
        [TestMethod]
        public void TestLoadScore()
        {
            string strInputScore = "BUNDY, TERESSA, 88";
            TextReader readRoutes = new StringReader(strInputScore);
            Scorer scorerCreate = Scorer.createScorer(readRoutes, (error) => {  });

            Assert.AreEqual(1, scorerCreate.scores.Count);
            Assert.AreEqual("BUNDY", scorerCreate.scores[0].lastName);
            Assert.AreEqual("TERESSA", scorerCreate.scores[0].firstName);
            Assert.AreEqual(88, scorerCreate.scores[0].score);
        }

        [TestMethod]
        public void TestLoadDodgyScore()
        {
            string strInputDodgyScore = "B_UNDY, TERESSA, 88";
            TextReader readRoutes = new StringReader(strInputDodgyScore);
            List<string> listErrors = new List<string>();
            Scorer scorerCreate = Scorer.createScorer(readRoutes, (error) => { listErrors.Add(error); });

            Assert.AreEqual(0, scorerCreate.scores.Count);
            Assert.AreEqual(2, listErrors.Count);
            Assert.AreEqual("Unable to match score to B_UNDY, TERESSA, 88", listErrors[0]);
            Assert.AreEqual("No scores were recorded", listErrors[1]);
        }

        [TestMethod]
        public void TestLoadManyScores()
        {
            string strInputScores = "BUNDY, TERESSA, 88\nSMITH, ALLAN, 70\nKING, MADISON, 88\nSMITH, FRANCIS, 85\n";
            TextReader readRoutes = new StringReader(strInputScores);
            List<string> listErrors = new List<string>();
            Scorer scorerCreate = Scorer.createScorer(readRoutes, (error) => { listErrors.Add(error); });

            Assert.AreEqual(4, scorerCreate.scores.Count);
            Assert.AreEqual(0, listErrors.Count);

            Assert.AreEqual("BUNDY", scorerCreate.scores[0].lastName);
            Assert.AreEqual("TERESSA", scorerCreate.scores[0].firstName);
            Assert.AreEqual(88, scorerCreate.scores[0].score);

            Assert.AreEqual("SMITH", scorerCreate.scores[1].lastName);
            Assert.AreEqual("ALLAN", scorerCreate.scores[1].firstName);
            Assert.AreEqual(70, scorerCreate.scores[1].score);

            Assert.AreEqual("KING", scorerCreate.scores[2].lastName);
            Assert.AreEqual("MADISON", scorerCreate.scores[2].firstName);
            Assert.AreEqual(88, scorerCreate.scores[2].score);

            Assert.AreEqual("SMITH", scorerCreate.scores[3].lastName);
            Assert.AreEqual("FRANCIS", scorerCreate.scores[3].firstName);
            Assert.AreEqual(85, scorerCreate.scores[3].score);
        }

        [TestMethod]
        public void TestGradedScores()
        {
            string strInputScores = "BUNDY, TERESSA, 88\nSMITH, ALLAN, 70\nKING, MADISON, 88\nSMITH, FRANCIS, 85\n";
            TextReader readRoutes = new StringReader(strInputScores);
            List<string> listErrors = new List<string>();
            Scorer scorerCreate = Scorer.createScorer(readRoutes, (error) => { listErrors.Add(error); });

            Assert.AreEqual(4, scorerCreate.scores.Count);
            Assert.AreEqual(0, listErrors.Count);

            List<Score> listSorted = scorerCreate.sortedScores;

            Assert.AreEqual("BUNDY", listSorted[0].lastName);
            Assert.AreEqual("TERESSA", listSorted[0].firstName);
            Assert.AreEqual(88, listSorted[0].score);

            Assert.AreEqual("KING", listSorted[1].lastName);
            Assert.AreEqual("MADISON", listSorted[1].firstName);
            Assert.AreEqual(88, listSorted[1].score);

            Assert.AreEqual("SMITH", listSorted[2].lastName);
            Assert.AreEqual("FRANCIS", listSorted[2].firstName);
            Assert.AreEqual(85, listSorted[2].score);

            Assert.AreEqual("SMITH", listSorted[3].lastName);
            Assert.AreEqual("ALLAN", listSorted[3].firstName);
            Assert.AreEqual(70, listSorted[3].score);
        }
    }
}
