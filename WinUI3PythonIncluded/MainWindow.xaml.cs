using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading.Tasks;

using Python.Included;
using Python.Runtime;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3PythonIncluded
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow( )
        {
            this.InitializeComponent( );
        }

        private void myButton_Click( object sender, RoutedEventArgs e )
        {
            myButton.Content = "Clicked";

            /*
            string original_path = System.Environment.GetEnvironmentVariable( "PATH" );
            System.Diagnostics.Debug.WriteLine( "\n\nPATH: {0}", original_path, null );

            string python_path = Installer.EmbeddedPythonHome;
            string new_path = original_path + ";" + python_path;

            System.Diagnostics.Debug.WriteLine( "New_path: {0}", new_path, null );
            System.Environment.SetEnvironmentVariable( "PATH", new_path );
            */

            
            Installer.InstallPath = @"C:\Testing\";

            System.Diagnostics.Debug.WriteLine( "\n\n\nPythonDLL: {0}", Runtime.PythonDLL, null );
            System.Diagnostics.Debug.WriteLine( "PythonHome: {0}", Installer.EmbeddedPythonHome, null );

            string python_dll_path = Path.Combine( Installer.EmbeddedPythonHome, "python310.dll" );
            System.Diagnostics.Debug.WriteLine( "Path: {0}", python_dll_path, null );


            var task = Installer.SetupPython( );
            Task.WhenAll( task );
            System.Diagnostics.Debug.WriteLine( "\n\n\nSetup finished\n\n\n" );

            Runtime.PythonDLL = python_dll_path;

            PythonEngine.Initialize( );
            using ( Py.GIL( ) )
            {
                dynamic sys = Py.Import( "sys" );
                string version = sys.version;
                System.Diagnostics.Debug.WriteLine( "VERSIOn: {0}", version, null );
            }


            //Task.Run( ( ) => this.SetupPython( ) );
        }



        private async Task SetupPython( )
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            System.Diagnostics.Debug.WriteLine( "\n\n\nPath: {0}\n\n\n", path, null );

            string python_path = Path.Combine( path, "python-3.7.3-embed-amd64", "python37.dll" );

            System.Diagnostics.Debug.WriteLine( "Pythonpath: {0}", python_path, null );

            System.Diagnostics.Debug.WriteLine( "InstallPath: {0}", Installer.InstallPath, null );
            System.Diagnostics.Debug.WriteLine( "EmbeddedPythonHome: {0}", Installer.EmbeddedPythonHome, null );

            //Installer.InstallPath = path;
            await Installer.SetupPython( force: true );
            PythonEngine.Initialize( );

            //PythonEngine.BeginAllowThreads( );

            using ( Py.GIL( ) )
            {
                dynamic sys = Py.Import( "sys" );
                string version = sys.version;
                System.Diagnostics.Debug.WriteLine( "version: {0}", version, null );
            }
        }
    }
}
