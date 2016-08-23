# OrderScores

Solution to the coding problem "Requirements – Programming test for Level 1s"

Reads text lines in the format \<Last name\>, \<first name\>, \<score\> where names are upper case and score in an integer.

**Class Library Usage** (*ScoreSorter.dll*) -

Create a `new Scorer` object using `Scorer.createScorer`, providing a `TextReader` object from which single line extries as above will be read.

Scores can be used as is or sorted by descending score, by last name, by first name.

**Executable Usage** (*OrderScores.exe*) -

```
OrderScores <filename-with-scores>
```

**Continuous Integration** (*travis*) -

Note that the solution contains a build configuration specific to the travis CI build *Release-Mono* that has mono specific commands for running the unit tests and the exe with test data. This is also the reason for using nunit as the unit test gframework.
