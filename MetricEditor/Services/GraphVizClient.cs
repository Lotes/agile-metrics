using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MetricEditor.Services
{
    /// <summary>
    /// This client class is used to make calls to GraphViz "dot", a graph layouter. The client takes a dot script, 
    /// calls dot and outputs an image file at the specified location. The output format can be choosen (SVG, PNG, ...).
    /// </summary>
    public class GraphVizClient
    {        
        public const string EMPTY_GRAPH_SCRIPT = "digraph G { }";
        private static readonly string DOT_PATH = @"..\..\..\Tools\graphviz\bin\dot.exe";

        public enum OutputFormat 
        {
            SVG,
            PNG
        }

        public static void Execute(string script, string outputFileName, OutputFormat format)
        {
            //save file
            using (var tw = new StreamWriter("graph.dot"))
            {
                tw.Write(script);
                tw.Close();
            }

            //convert format
            string strFormat = format.ToString().ToLower();

            //start "dot" process, process SVG file
            var outputFileInfo = new FileInfo(outputFileName);
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = new FileInfo(DOT_PATH).FullName,
                    Arguments = "-o" + outputFileInfo.FullName + " -T"+strFormat+" graph.dot",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            string error = proc.StandardError.ReadToEnd();
            if (error != "")
                throw new Exception("Error while using GraphViz's dot: "+error);
        }
    }
}
