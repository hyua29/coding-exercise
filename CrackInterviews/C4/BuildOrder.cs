using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public class BuildOrder
    {
        public static IList<string> Calculate(string[] projects, IList<BuildDependency> dependencies)
        {
            if (projects == null) return new List<string>();

            var graph = new BuildGraph();

            foreach (var p in projects)
            {
                graph.AddProject(p);                
            }

            foreach (var d in dependencies)
            {
                graph.AddEdge(d.ParentProjectName, d.DependentProjectName);
            }

            var result = new Stack<string>();

            var hasCycle = false;
            foreach (var pair in graph.ProjectMap)
            {
                if (pair.Value.Status != Status.Visited)
                {
                    hasCycle = hasCycle || BuildOrder.DoDFS(pair.Value, result);
                    if (hasCycle) return new List<string>();
                }
            }

            return result.ToList();
        }

        private static bool DoDFS(Project project, Stack<string> result)
        {
            // Circle detected
            if (project.Status == Status.Visiting) return true;

            // Project has been explored
            if (project.Status == Status.Visited) return false;

            // Start to process current project
            project.Status = Status.Visiting;

            var children = project.ChildrenProjectMap.Select(x => x.Value).ToList();
            var hasCycle = false;
            foreach (var p in children)
            {
                hasCycle = hasCycle || BuildOrder.DoDFS(p, result);
                if (hasCycle) return true;
            }

            result.Push(project.ProjectName);
            project.Status = Status.Visited;

            return false;
        }
    }

    public enum Status
    {
        New,

        Visiting,

        Visited
    }

    public class Project
    {
        public IDictionary<string, Project> ChildrenProjectMap { get; } = new Dictionary<string, Project>();

        public string ProjectName { get; }

        public Status Status { get; set; }

        public Project(string projectName)
        {
            ProjectName = projectName;
            Status = Status.New;
        }
    }

    public class BuildDependency
    {
        public string ParentProjectName { get; }

        public string DependentProjectName { get; }

        public BuildDependency(string parentProjectName, string dependentProjectName)
        {
            this.ParentProjectName = parentProjectName;
            this.DependentProjectName = dependentProjectName;
        }
    }

    public class BuildGraph
    {
        public IDictionary<string, Project> ProjectMap { get; } = new Dictionary<string, Project>();

        public void AddProject(string project)
        {
            var newProject = new Project(project);
            this.ProjectMap.Add(newProject.ProjectName, newProject);
        }

        public void AddEdge(string parentProjectName, string dependentProjectName)
        {
            var parentProject = this.ProjectMap[parentProjectName];
            var dependentProject = this.ProjectMap[dependentProjectName];
            parentProject.ChildrenProjectMap.Add(dependentProject.ProjectName, dependentProject);
        }
    }

    [TestFixture]
    public class BuildOrderTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(string[] project, IList<BuildDependency> dependencies, bool hasCycle)
        {
            var result = BuildOrder.Calculate(project, dependencies);
            if (project == null || hasCycle)
            {
                Assert.IsEmpty(result);
                return;
            }
            
            foreach (var d in dependencies)
            {
                var parentProjectIndex = -1;
                var dependentProjectIndex = -1;
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i] == d.ParentProjectName) parentProjectIndex = i;
                    if (result[i] == d.DependentProjectName) dependentProjectIndex = i;
                }
                Assert.That(parentProjectIndex, Is.LessThan(dependentProjectIndex));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null, false);
            
            yield return new TestCaseData(
                new [] {"1", "2", "3", "4"},
                new List<BuildDependency>() { new BuildDependency("3", "4") },
                false);
            
            yield return new TestCaseData(
                new [] {"a", "b", "c", "d", "e", "f", "g", "h"},
                new List<BuildDependency>()
                {
                    new BuildDependency("d", "g"),
                    new BuildDependency("f", "c"),
                    new BuildDependency("f", "b"),
                    new BuildDependency("f", "g"),
                    new BuildDependency("b", "h"),
                    new BuildDependency("b", "e"),
                    new BuildDependency("a", "e"),
                    new BuildDependency("c", "a"),
                },
                false);
            
            yield return new TestCaseData(
                new [] {"a", "b", "c", "d", "e", "f", "g", "h"},
                new List<BuildDependency>()
                {
                    new BuildDependency("d", "g"),
                    new BuildDependency("f", "c"),
                    new BuildDependency("f", "b"),
                    new BuildDependency("f", "g"),
                    new BuildDependency("b", "h"),
                    new BuildDependency("b", "e"),
                    new BuildDependency("a", "e"),
                    new BuildDependency("c", "a"),
                    new BuildDependency("e", "d"),
                    new BuildDependency("g", "f"),
                },
                true);
        }
    }
}