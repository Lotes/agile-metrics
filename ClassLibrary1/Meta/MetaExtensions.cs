using System;

namespace ClassLibrary1.N00_Config.Meta
{
    public static class MetaExtensions
    {
        public static void ForEachInput(this IMetaNode @this, Action<IMetaDependency, IMetaNode> action)
        {
            if (@this is IMetaSelfNode)
            {
                var self = @this as IMetaSelfNode;
                foreach (var input in self.Inputs.Values)
                    action(input, input.Source);
            }
        }

        public static void ForEachOutput(this IMetaNode @this, Action<IMetaDependency, IMetaNode> action)
        {
            foreach (var input in @this.Outputs)
                action(input, input.Target);
        }
    }
}