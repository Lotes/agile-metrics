using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IMetaBuilder<TResult>
    {
        TypedKey<TResult> Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        IMetaBuilder<TOne, TResult> AddSelfInput<TOne>(TypedKey<TOne> first, string parameterName);
        IMetaBuilder<TOne[], TResult> AddChildrenInput<TOne>(TypedKey<TOne> first, string parameterName);
        void Define(string sourceCode);
    }

    public interface IMetaBuilder<TOne, TResult>
    {
        TypedKey<TResult> Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        InputConfiguration Argument1 { get; }
        IMetaBuilder<TOne, TTwo, TResult> AddSelfInput<TTwo>(TypedKey<TTwo> second, string parameterName);
        IMetaBuilder<TOne, TTwo[], TResult> AddChildrenInput<TTwo>(TypedKey<TTwo> second, string parameterName);
        void Define(string sourceCode);
    }

    public interface IMetaBuilder<TOne, TTwo, TResult>
    {
        TypedKey<TResult> Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        InputConfiguration Argument1 { get; }
        InputConfiguration Argument2 { get; }
        IMetaBuilder<TOne, TTwo, TThree, TResult> AddSelfInput<TThree>(TypedKey<TThree> third, string parameterName);
        IMetaBuilder<TOne, TTwo, TThree[], TResult> AddChildrenInput<TThree>(TypedKey<TThree> third, string parameterName);
        void Define(string sourceCode);
    }

    public interface IMetaBuilder<TOne, TTwo, TThree, TResult>
    {
        TypedKey<TResult> Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        InputConfiguration Argument1 { get; }
        InputConfiguration Argument2 { get; }
        InputConfiguration Argument3 { get; }
        IMetaBuilder<TOne, TTwo, TThree, TFour, TResult> AddSelfInput<TFour>(TypedKey<TFour> fourth, string parameterName);
        IMetaBuilder<TOne, TTwo, TThree, TFour[], TResult> AddChildrenInput<TFour>(TypedKey<TFour> fourth, string parameterName);
        void Define(string sourceCode);
    }

    public interface IMetaBuilder<TOne, TTwo, TThree, TFour, TResult>
    {
        TypedKey<TResult> Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        InputConfiguration Argument1 { get; }
        InputConfiguration Argument2 { get; }
        InputConfiguration Argument3 { get; }
        InputConfiguration Argument4 { get; }
        void Define(string sourceCode);
    }
}
