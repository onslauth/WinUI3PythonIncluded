using Python.Included;
using Python.Runtime;

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
            Task.Run( ( ) => this.SetupPython( ) );
        }

        private async Task SetupPython( )
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            System.Diagnostics.Debug.WriteLine( "\n\n\nPath: {0}\n\n\n", path, null );


            Installer.InstallPath = path;
            await Installer.SetupPython( force: true );
            PythonEngine.Initialize( );
            PythonEngine.BeginAllowThreads( );

            using ( Py.GIL( ) )
            {
                dynamic sys = Py.Import( "sys" );
                string version = $"{ sys.version }";
                System.Diagnostics.Debug.WriteLine( "version: {0}", version, null );
            }
        }
    }
}
