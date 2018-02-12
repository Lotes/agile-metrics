﻿using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaRawNode : IMetaRawNode
    {
        public MetaRawNode(TypedKey key, ImporterType source, IEnumerable<ArtifactType> targetArtifactTypes)
        {
            Key = key;
            InvalidValue = null; //TODO
            Source = source;
            TargetArtifactTypes = targetArtifactTypes;
        }

        public TypedKey Key { get; private set; }
        public object InvalidValue { get; private set; }
        public ImporterType Source { get; private set; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; private set; }
    }
}