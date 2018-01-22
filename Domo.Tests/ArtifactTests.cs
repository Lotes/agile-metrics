using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domo.Core.Impl;
using Domo.Core;

namespace Domo.Tests
{
    [TestClass]
    public class ArtifactTests
    {
        private ArtifactFactory factory;

        [TestInitializeAttribute]
        public void Setup()
        {
            factory = new ArtifactFactory();
        }

        [TestMethod]
        public void CreateTreeTest()
        {
            var root = factory.CreateRoot();
            var cpp  = factory.CreateFile("main.cpp", root);
            var include = factory.CreateDirectory("include", root);
            var h = factory.CreateFile("main.h", include);
            Assert.AreEqual("/include/main.h", h.GetPath());
            Assert.AreEqual("/include", include.GetPath());
            Assert.AreEqual("/main.cpp", cpp.GetPath());
            Assert.AreEqual("", root.GetPath());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PassNullParent_Fail()
        {
            factory.CreateFile("valid_name", null);
        }
    }
}
