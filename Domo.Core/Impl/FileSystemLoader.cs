using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core.Impl
{
    public class FileSystemLoader : IArtifactLoader
    {
        private IArtifactFactory factory;

        public FileSystemLoader(IArtifactFactory factory)
        {
            this.factory = factory;
        }

        public IArtifact Load(string url)
        {
            var root = factory.CreateRoot();
            LoadInto(root, new DirectoryInfo(url));
            return root;   
        }

        private void LoadInto(IArtifact root, DirectoryInfo directoryInfo)
        {
            var directory = factory.CreateDirectory(directoryInfo.Name, root);
            foreach (var child in directoryInfo.GetDirectories())
                LoadInto(directory, child);
            foreach (var child in directoryInfo.GetFiles())
                LoadInto(directory, child);
        }

        private void LoadInto(IArtifact root, FileInfo fileInfo)
        {
            factory.CreateDirectory(fileInfo.Name, root);
        }
    }
}
