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
            Assert.That(BuildOrder.Calculate(project, dependencies), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, 8, 0);
            yield return new TestCaseData(new BinaryTreeNode<int>(10)
            {
                LeftNode = new BinaryTreeNode<int>(5)
                {
                    LeftNode = new BinaryTreeNode<int>(3)
                    {
                        LeftNode = new BinaryTreeNode<int>(3),
                        RightNode = new BinaryTreeNode<int>(-2)
                    },
                    RightNode = new BinaryTreeNode<int>(2)
                    {
                        RightNode = new BinaryTreeNode<int>(1)
                    }
                },
                RightNode = new BinaryTreeNode<int>(-3)
                {
                    RightNode = new BinaryTreeNode<int>(11)
                }
            }, 8, 3);
        }
    }
}