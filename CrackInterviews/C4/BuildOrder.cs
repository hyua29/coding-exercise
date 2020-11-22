using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    internal class BuildOrder
    {
        internal static IList<string> Calculate(string[] project, IList<BuildDependency> dependencies)
        {
            if (project == null) return new List<string>();

            var result = new List<string>();
            return result;
        }
    }

    public class BuildDependency
    {
        public string DependentProject { get; }

        public string Project { get; }

        public BuildDependency(string dependentProject, string project)
        {
            this.DependentProject = dependentProject;
            this.Project = project;
        }
    }

    [TestFixture]
    public class BuildOrderTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(string[] project, IList<BuildDependency> dependencies, IList<string> expectedResult)
        {
            var result = BuildOrder.Calculate(project, dependencies);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i], Is.EqualTo(expectedResult[i]));   
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
           yield return new TestCaseData(null, null, new List<string>());
        }
    }
}