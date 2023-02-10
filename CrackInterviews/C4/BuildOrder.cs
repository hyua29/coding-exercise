using System.Collections.ObjectModel;

namespace C4
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    public class BuildOrder
    {
        public static IList<string> CalculateWithDfs(string[] projects, IList<BuildDependency> dependencies)
        {
            if (projects == null) return null;

            var graph = new BuildOrderGraph<ProjectForDfs>();

            foreach (var p in projects) graph.AddProject(p, new ProjectForDfs(p));

            foreach (var d in dependencies) graph.AddEdge(d.ParentProjectName, d.DependentProjectName);

            var result = new Stack<string>();

            foreach (var pair in graph.ProjectMap)
                if (pair.Value.Status != Status.Visited)
                {
                    var hasCycle = DoDfs(pair.Value, result);
                    if (hasCycle) return null;
                }

            return result.ToList();
        }

        public static IList<string> CalculateWithLayer(string[] projects, IList<BuildDependency> dependencies)
        {
            if (projects == null) return null;

            var graph = new BuildOrderGraph<ProjectForLayer>();

            foreach (var p in projects) graph.AddProject(p, new ProjectForLayer(p));

            foreach (var d in dependencies) graph.AddEdge(d.ParentProjectName, d.DependentProjectName);

            var results = new List<string>();

            IList<ProjectForLayer> toBeProcessed = null;
            do
            {
                toBeProcessed = GetToBeProcessed(graph);
                foreach (var p in toBeProcessed)
                {
                    results.Add(p.ProjectName);

                    foreach (var child in p.ChildProjects)
                    {
                        child.DecrementParentCount();
                    }
                }
            } while (toBeProcessed.Count > 0);

            if (results.Count != projects.Length)
            {
                // A loop is detected
                return null;
            }

            return results;
        }

        private static IList<ProjectForLayer> GetToBeProcessed(BuildOrderGraph<ProjectForLayer> graph)
        {
            var toBeProcessed = new List<ProjectForLayer>();
            foreach (var project in graph.ProjectMap.Values)
            {
                if (project.ParentCount == 0)
                {
                    toBeProcessed.Add(project);
                }
            }

            foreach (var project in toBeProcessed)
            {
                graph.ProjectMap.Remove(project.ProjectName);
            }

            return toBeProcessed;
        }

        private static bool DoDfs(ProjectForDfs projectForDfs, Stack<string> result)
        {
            // Circle detected
            if (projectForDfs.Status == Status.Visiting) return true;

            // Project has been explored
            if (projectForDfs.Status == Status.Visited) return false;

            // Start to process current project
            projectForDfs.Status = Status.Visiting;

            foreach (var p in projectForDfs.ChildProjects)
            {
                if (DoDfs(p, result)) return true;
            }

            result.Push(projectForDfs.ProjectName);
            projectForDfs.Status = Status.Visited;

            return false;
        }
    }

    public enum Status
    {
        New,

        Visiting,

        Visited
    }

    public abstract class Project<T> where T : Project<T>
    {
        private readonly IDictionary<string, T> _childrenProjectMap = new Dictionary<string, T>();

        protected Project(string projectName)
        {
            ProjectName = projectName;
        }

        public ICollection<T> ChildProjects => _childrenProjectMap.Values;

        public virtual void AddChild(T child)
        {
            _childrenProjectMap.Add(child.ProjectName, child);
        }

        public string ProjectName { get; }
    }

    public class ProjectForDfs : Project<ProjectForDfs>
    {
        public ProjectForDfs(string projectName) : base(projectName)
        {
            Status = Status.New;
        }

        public Status Status { get; set; }
    }

    public class ProjectForLayer : Project<ProjectForLayer>
    {
        public ProjectForLayer(string projectName) : base(projectName)
        {
            ParentCount = 0;
        }

        public override void AddChild(ProjectForLayer child)
        {
            base.AddChild(child);

            child.IncrementParentCount();
        }

        public int ParentCount { get; private set; }

        private void IncrementParentCount()
        {
            ParentCount++;
        }

        public void DecrementParentCount()
        {
            if (ParentCount > 0)
            {
                ParentCount--;
            }
        }
    }

    public class BuildDependency
    {
        public BuildDependency(string parentProjectName, string dependentProjectName)
        {
            ParentProjectName = parentProjectName;
            DependentProjectName = dependentProjectName;
        }

        public string ParentProjectName { get; }

        public string DependentProjectName { get; }
    }

    public class BuildOrderGraph<T> where T : Project<T>
    {
        public IDictionary<string, T> ProjectMap { get; } = new Dictionary<string, T>();

        public void AddProject(string key, T project)
        {
            ProjectMap.Add(key, project);
        }

        public void AddEdge(string parentProjectName, string dependentProjectName)
        {
            var parentProject = ProjectMap[parentProjectName];
            var dependentProject = ProjectMap[dependentProjectName];

            parentProject.AddChild(dependentProject);
        }
    }

    [TestFixture]
    public class BuildOrderTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void CalculateLayer_Test(string[] project, IList<BuildDependency> dependencies, bool hasCycle)
        {
            var result = BuildOrder.CalculateWithLayer(project, dependencies);
            if (project == null || hasCycle)
            {
                Assert.IsNull(result);
                return;
            }

            Assert.That(result.Count, Is.EqualTo(project.Length));
        }

        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(string[] project, IList<BuildDependency> dependencies, bool hasCycle)
        {
            var result = BuildOrder.CalculateWithDfs(project, dependencies);
            if (project == null || hasCycle)
            {
                Assert.IsNull(result);
                return;
            }

            Assert.That(result.Count, Is.EqualTo(project.Length));

            foreach (var d in dependencies)
            {
                var parentProjectIndex = -1;
                var dependentProjectIndex = -1;
                for (var i = 0; i < result.Count; i++)
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
                new[] {"1", "2", "3", "4"},
                new List<BuildDependency> {new BuildDependency("3", "4")},
                false);

            yield return new TestCaseData(
                new[] {"a", "b", "c", "d", "e", "f", "g", "h"},
                new List<BuildDependency>
                {
                    new BuildDependency("d", "g"),
                    new BuildDependency("f", "c"),
                    new BuildDependency("f", "b"),
                    new BuildDependency("f", "g"),
                    new BuildDependency("b", "h"),
                    new BuildDependency("b", "e"),
                    new BuildDependency("a", "e"),
                    new BuildDependency("c", "a")
                },
                false);

            yield return new TestCaseData(
                new[] {"a", "b", "c", "d", "e", "f", "g", "h"},
                new List<BuildDependency>
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
                    new BuildDependency("g", "f")
                },
                true);
        }
    }
}