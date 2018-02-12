using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVGViewer
{
    public partial class MainWindow : Window
    {
        public class ViewModel: NotifyPropertyChangedBase
        {
            private string script = null;
            private DrawingGroup image = null;

            public ViewModel()
			{
			}
			
            public string Script 
            {
                get { return script; }
                set
                {
                    script = value;
                    NotifyPropertyChanged("Script");
                }
            }
            public DrawingGroup	Image { get { return image; } }
			public void Render()
			{
                //save file
                using (var tw = new StreamWriter("graph.dot"))
                {
                    tw.Write(script);
                    tw.Close();
                }

                //start "dot" process, process SVG file
                var svgFileInfo = new FileInfo("graph.svg");
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dot",
                        Arguments = "-o" + svgFileInfo.FullName+ " -Tsvg graph.dot",
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                string error = "";
                while (!proc.StandardError.EndOfStream)
                {
                    string line = proc.StandardError.ReadLine();
                    error += line + "\r\n";
                }
                if (error != "")
                {
                    MessageBox.Show(error);
                    return;
                }

				SVGRender render = new SVGRender();
                image = render.LoadDrawing(svgFileInfo.FullName);
                NotifyPropertyChanged("Image");
			}
        }

        private ViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            var script = @"digraph G {
    main -> parse -> execute;            
    main -> init;
    main -> cleanup;
    execute -> make_string;
    execute -> printf;
    init -> make_string;
    main -> printf;
    execute -> compare;
}";
            DataContext = viewModel = new ViewModel() { Script = script };            
        }

        private void buttonRender_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Render();
        }
    }
}
