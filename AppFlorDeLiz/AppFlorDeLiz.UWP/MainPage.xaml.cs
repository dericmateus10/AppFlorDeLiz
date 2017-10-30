using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AppFlorDeLiz.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init("hZquJOh79rAkwQNvknu4~IEV41uYX5i7CrQbnUBb-RQ~Aof2A_nIHLS_1-dCMLGQbMgF5kETTNlet7O9b-kiiRnjjo9cDXjAaa-EmszL5wcp");

            LoadApplication(new AppFlorDeLiz.App());
        }
    }
}
