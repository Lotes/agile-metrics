using System;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E01_Artifacts.Impl;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ClassLibrary1.N00_Config.Facade.Impl;
using ClassLibrary1.N00_Config.Meta.Impl;
using ClassLibrary1.E03_Tags;
using ClassLibrary1.N03_Average;
using Environment.Impl;

namespace ClassLibrary1.T00_Usage
{
    [TestClass]
    public class UsageTests
    {
        [TestMethod]
        public void BasicUsage()
        {
            // Artifact tree:
            //         Root: Directory
            //          /           \
            //    mainH:File    mainCPP:File
            var root = new Artifact("Root", "DIRECTORY");
            var mainH = new Artifact("mainH", "FILE", root);
            var mainCPP = new Artifact("mainCPP", "FILE", root);

            // Metrics:
            var fileLOC = TypedKey.Create<double>("FileLOC");
            var commentLines = TypedKey.Create<double>("CommentLines");
            var sumFileLOC = TypedKey.Create<double>("FileLOC.SUM");
            var commentRatio = TypedKey.Create<double>("CommentRatio");

            // Metric definitions:
            var metricModel = new MetricModel(new MetaGraphFactory(), new ValueStorageFactory());
            metricModel.DeclareRaw(fileLOC, "SOTOGRAPH", "FILE");
            metricModel.DeclareRaw(commentLines, "SOTOGRAPH", "FILE");
            metricModel.BeginDefinition(sumFileLOC, "FILE", "DIRECTORY")
                .AddSelfInput(fileLOC, "current")
                .AddChildrenInput(sumFileLOC, "children")
                .Define(@"var currentPart = double.IsNaN(current) ? 0 : current;
                    var childrenPart = children.Where(d => !double.IsNaN(d)).Aggregate(0.0, (a, b) => a + b);
                    return currentPart + childrenPart;");
            metricModel.BeginDefinition(commentRatio, "FILE")
                .AddSelfInput(fileLOC, "loc")
                .AddSelfInput(commentLines, "comments")
                .Define(@"if (double.IsNaN(loc) || double.IsNaN(comments))
                        return double.NaN;
                    return loc > 0 ? comments / loc : 0.0;");

            // Raw metric values:
            metricModel.SetRawValue(fileLOC, mainH, 100.0);
            metricModel.SetRawValue(fileLOC, mainCPP, 10000.0);
            metricModel.SetRawValue(commentLines, mainH, 20.0);
            metricModel.SetRawValue(commentLines, mainCPP, 4000.0);

            var all = new TagExpression();
            var commentRatioSubscriptions = metricModel.SubscribeOn(all, commentRatio, new[] { root, mainCPP, mainH });
            var sumLOCSubscriptions = metricModel.SubscribeOn(all, sumFileLOC, new[] { root, mainCPP, mainH });

            Assert.IsNull(commentRatioSubscriptions[root]);
            Assert.AreEqual(0.2, commentRatioSubscriptions[mainH]?.ValueSync);
            Assert.AreEqual(0.4, commentRatioSubscriptions[mainCPP]?.ValueSync);
            Assert.AreEqual(10100.0, sumLOCSubscriptions[root]?.ValueSync);

            metricModel.SetRawValue(commentLines, mainH, 50.0);
            Assert.AreEqual(0.5, commentRatioSubscriptions[mainH]?.ValueSync);
        }

        [TestMethod]
        public void BasicTagging()
        {
            // Artifact tree:
            //         Root: Directory
            //          /           \
            //    mainH:File    mainCPP:File
            var catalog = new ArtifactCatalog();
            var root = catalog.Add("Root", "DIRECTORY");
            var mainH = catalog.Add("mainH", "FILE", root);
            var mainCPP = catalog.Add("mainCPP", "FILE", root);

            // Metrics:
            var fileLOC = TypedKey.Create<double>("FileLOC");
            var commentLines = TypedKey.Create<double>("CommentLines");
            var sumFileLOC = TypedKey.Create<double>("FileLOC.SUM");
            var commentRatio = TypedKey.Create<double>("CommentRatio");

            // Metric definitions:
            var metricModel = new MetricModel(new MetaGraphFactory(), new ValueStorageFactory());
            metricModel.DeclareRaw(fileLOC, "SOTOGRAPH", "FILE");
            metricModel.DeclareRaw(commentLines, "SOTOGRAPH", "FILE");
            metricModel.BeginDefinition(sumFileLOC, "FILE", "DIRECTORY")
                .AddSelfInput(fileLOC, "current")
                .AddChildrenInput(sumFileLOC, "children")
                .Define(@"var currentPart = double.IsNaN(current) ? 0 : current;
                    var childrenPart = children.Where(d => !double.IsNaN(d)).Aggregate(0.0, (a, b) => a + b);
                    return currentPart + childrenPart;");
            metricModel.BeginDefinition(commentRatio, "FILE")
                .AddSelfInput(fileLOC, "loc")
                .AddSelfInput(commentLines, "comments")
                .Define(@"if (double.IsNaN(loc) || double.IsNaN(comments))
                        return double.NaN;
                    return loc > 0 ? comments / loc : 0.0;");

            // Raw metric values:
            metricModel.SetRawValue(fileLOC, mainH, 100.0);
            metricModel.SetRawValue(fileLOC, mainCPP, 10000.0);
            metricModel.SetRawValue(commentLines, mainH, 20.0);
            metricModel.SetRawValue(commentLines, mainCPP, 4000.0);

            var tagFuSi = new Tag("FUSI");
            catalog.Tag(new[] { mainH }, tagFuSi, Metrics.E01_Artifacts.SetterMode.Set);
            var noHeaders = new TagExpression(a => !a.Tags.Contains(tagFuSi));
            var commentRatioSubscriptions = metricModel.SubscribeOn(noHeaders, commentRatio, new[] { root, mainCPP, mainH });
            var sumLOCSubscriptions = metricModel.SubscribeOn(noHeaders, sumFileLOC, new[] { root, mainCPP, mainH });

            Assert.IsNull(commentRatioSubscriptions[root]);
            Assert.IsNotNull(commentRatioSubscriptions[mainH]);
            Assert.IsNull(commentRatioSubscriptions[mainH].Value);
            Assert.AreEqual(0.4, commentRatioSubscriptions[mainCPP]?.Value);
            Assert.AreEqual(10000.0, sumLOCSubscriptions[root]?.Value);

            catalog.Tag(new[] { mainH }, tagFuSi, Metrics.E01_Artifacts.SetterMode.Unset);
            Assert.IsNotNull(commentRatioSubscriptions[mainH]);
            Assert.AreEqual(0.2, commentRatioSubscriptions[mainH]?.Value);
            Assert.AreEqual(10100.0, sumLOCSubscriptions[root]?.Value);
        }

        [TestMethod]
        public void BigTest()
        {
            // Metrics:
            var fileLOC = TypedKey.Create<double>("FileLOC");
            var sumFileLOC = TypedKey.Create<double>("FileLOC.SUM");
            var avgFileLOCHelper = TypedKey.Create<AverageData>("FileLOC.AVG.HELPER");
            var avgFileLOC = TypedKey.Create<double>("FileLOC.AVG");
            var root = new Artifact("Root", "DIRECTORY");
            var metricModel = new MetricModel(new MetaGraphFactory(), new ValueStorageFactory());
            metricModel.DeclareRaw(fileLOC, "SOTOGRAPH", "FILE");
            var count = CreateABCDirectories(root, metricModel, fileLOC, 4);
            Console.WriteLine("Created "+count+" files!");

            // Metric definitions:
            metricModel.BeginDefinition(sumFileLOC, "FILE", "DIRECTORY")
                .AddSelfInput(fileLOC, "current")
                .AddChildrenInput(sumFileLOC, "children")
                .Define(@"var currentPart = double.IsNaN(current) ? 0 : current;
                    var childrenPart = children.Where(d => !double.IsNaN(d)).Aggregate(0.0, (a, b) => a + b);
                    return currentPart + childrenPart;");
            metricModel.BeginDefinition(avgFileLOCHelper, "FILE", "DIRECTORY")
                .AddSelfInput(fileLOC, "current")
                .AddChildrenInput(avgFileLOCHelper, "children")
                .Define(@"var currentPart = double.IsNaN(current) ? new AverageData(0, 0) : new AverageData(current, 1);
                    var childrenPart = children.Where(d => d != null).Aggregate(currentPart, (a, b) => new AverageData(a.Sum+b.Sum, a.Weight+b.Weight));
                    return childrenPart;");
            metricModel.BeginDefinition(avgFileLOC, "FILE", "DIRECTORY")
                .AddSelfInput(avgFileLOCHelper, "current")
                .Define(@"return current.Sum / current.Weight;");

            var all = new TagExpression();
            var sumLOCSubscriptions = metricModel.SubscribeOn(all, sumFileLOC, new[] { root });
            var avgLOCSubscriptions = metricModel.SubscribeOn(all, avgFileLOC, new[] { root });

            Console.WriteLine(avgLOCSubscriptions[root].Value);
            Assert.AreEqual(10.0*count, sumLOCSubscriptions[root]?.Value);
            Assert.AreEqual(10.0, avgLOCSubscriptions[root]?.Value);
        }

        private int CreateABCDirectories(Artifact root, IMetricModel mmodel, TypedKey loc, int layers)
        {
            layers--;
            var sum = 0;
            for (var index = 1; index <= 26; index++)
            {
                var next = new Artifact(index.ToString(), layers == 0 ? "FILE" : "DIRECTORY", root);
                if(layers > 0)
                    sum += CreateABCDirectories(next, mmodel, loc, layers);
                else
                {
                    mmodel.SetRawValue(loc, next, 10.0);
                    sum++;
                }
            }
            return sum;
        }
    }
}
