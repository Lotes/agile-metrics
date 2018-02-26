using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class MetaBuilder<TResult> : IMetaBuilder<TResult>
    {
        private readonly IMetaGraph metaGraph;

        public MetaBuilder(IMetaGraph metaGraph, TypedKey<TResult> key, IEnumerable<ArtifactType> targetArtifactTypes)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
        }

        public TypedKey<TResult> Key { get; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }

        public IMetaBuilder<TOne[], TResult> AddChildrenInput<TOne>(TypedKey<TOne> first, string parameterName)
        {
            return new MetaBuilder<TOne[], TResult>(metaGraph, Key, TargetArtifactTypes, new InputConfiguration(first, DependencyLocality.Children, parameterName));
        }

        public IMetaBuilder<TOne, TResult> AddSelfInput<TOne>(TypedKey<TOne> first, string parameterName)
        {
            return new MetaBuilder<TOne, TResult>(metaGraph, Key, TargetArtifactTypes, new InputConfiguration(first, DependencyLocality.Self, parameterName));
        }

        public void Define(string sourceCode)
        {
            var inputs = new InputConfiguration[0];
            metaGraph.CreateSelfNode(Key, TargetArtifactTypes.ToArray(), inputs, sourceCode);
        }
    }

    public class MetaBuilder<TOne, TResult> : IMetaBuilder<TOne, TResult>
    {
        private readonly IMetaGraph metaGraph;

        public MetaBuilder(IMetaGraph metaGraph, TypedKey<TResult> key, IEnumerable<ArtifactType> targetArtifactTypes, InputConfiguration argument1)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
            Argument1 = argument1;
        }

        public TypedKey<TResult> Key { get; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        public InputConfiguration Argument1 { get; }

        public IMetaBuilder<TOne, TTwo[], TResult> AddChildrenInput<TTwo>(TypedKey<TTwo> second, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo[], TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, new InputConfiguration(second, DependencyLocality.Children, parameterName));
        }

        public IMetaBuilder<TOne, TTwo, TResult> AddSelfInput<TTwo>(TypedKey<TTwo> second, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo, TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, new InputConfiguration(second, DependencyLocality.Self, parameterName));
        }

        public void Define(string sourceCode)
        {
            var inputs = new [] {Argument1};
            metaGraph.CreateSelfNode(Key, TargetArtifactTypes.ToArray(), inputs, sourceCode);
        }
    }

    public class MetaBuilder<TOne, TTwo, TResult> : IMetaBuilder<TOne, TTwo, TResult>
    {
        private readonly IMetaGraph metaGraph;
        public MetaBuilder(IMetaGraph metaGraph, TypedKey<TResult> key, IEnumerable<ArtifactType> targetArtifactTypes, InputConfiguration argument1, InputConfiguration argument2)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
            Argument1 = argument1;
            Argument2 = argument2;
        }

        public TypedKey<TResult> Key { get; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        public InputConfiguration Argument1 { get; }
        public InputConfiguration Argument2 { get; }

        public IMetaBuilder<TOne, TTwo, TThree[], TResult> AddChildrenInput<TThree>(TypedKey<TThree> third, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo, TThree[], TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, Argument2, new InputConfiguration(third, DependencyLocality.Children, parameterName));
        }

        public IMetaBuilder<TOne, TTwo, TThree, TResult> AddSelfInput<TThree>(TypedKey<TThree> third, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo, TThree, TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, Argument2, new InputConfiguration(third, DependencyLocality.Self, parameterName));
        }

        public void Define(string sourceCode)
        {
            var inputs = new [] {Argument1, Argument2};
            metaGraph.CreateSelfNode(Key, TargetArtifactTypes.ToArray(), inputs, sourceCode);
        }
    }

    public class MetaBuilder<TOne, TTwo, TThree, TResult> : IMetaBuilder<TOne, TTwo, TThree, TResult>
    {
        private readonly IMetaGraph metaGraph;
        public MetaBuilder(IMetaGraph metaGraph, TypedKey<TResult> key, IEnumerable<ArtifactType> targetArtifactTypes, InputConfiguration argument1, InputConfiguration argument2, InputConfiguration argument3)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
            Argument1 = argument1;
            Argument2 = argument2;
            Argument3 = argument3;
        }

        public TypedKey<TResult> Key { get; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        public InputConfiguration Argument1 { get; }
        public InputConfiguration Argument2 { get; }
        public InputConfiguration Argument3 { get; }
   
        public IMetaBuilder<TOne, TTwo, TThree, TFour[], TResult> AddChildrenInput<TFour>(TypedKey<TFour> fourth, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo, TThree, TFour[], TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, Argument2, Argument3, new InputConfiguration(fourth, DependencyLocality.Children, parameterName));
        }

        public IMetaBuilder<TOne, TTwo, TThree, TFour, TResult> AddSelfInput<TFour>(TypedKey<TFour> fourth, string parameterName)
        {
            return new MetaBuilder<TOne, TTwo, TThree, TFour, TResult>(metaGraph, Key, TargetArtifactTypes, Argument1, Argument2, Argument3, new InputConfiguration(fourth, DependencyLocality.Self, parameterName));
        }

        public void Define(string sourceCode)
        {
            var inputs = new [] {Argument1, Argument2, Argument3};
            metaGraph.CreateSelfNode(Key, TargetArtifactTypes.ToArray(), inputs, sourceCode);
        }
    }

    public class MetaBuilder<TOne, TTwo, TThree, TFour, TResult> : IMetaBuilder<TOne, TTwo, TThree, TFour, TResult>
    {
        private readonly IMetaGraph metaGraph;
        public MetaBuilder(IMetaGraph metaGraph, TypedKey<TResult> key, IEnumerable<ArtifactType> targetArtifactTypes, InputConfiguration argument1, InputConfiguration argument2, InputConfiguration argument3, InputConfiguration argument4)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
            Argument1 = argument1;
            Argument2 = argument2;
            Argument3 = argument3;
            Argument4 = argument4;
        }

        public TypedKey<TResult> Key { get; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        public InputConfiguration Argument1 { get; }
        public InputConfiguration Argument2 { get; }
        public InputConfiguration Argument3 { get; }
        public InputConfiguration Argument4 { get; }

        public void Define(string sourceCode)
        {
            var inputs = new [] {Argument1, Argument2, Argument3, Argument4};
            metaGraph.CreateSelfNode(Key, TargetArtifactTypes.ToArray(), inputs, sourceCode);
        }
    }
}
