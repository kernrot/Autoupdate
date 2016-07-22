#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: MainWindow.xaml.cs
// Author: Conrad Kernrot (gitlab_autoupdate@kernrot.de)
// -------------------------------------------------------------------------------
// Licensed under the MIT License,
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.opensource.org/licenses/mit-license.php
// -------------------------------------------------------------------------------

#endregion

namespace Kernrot.GitLab.Autoupdate
{
    using System.Windows;

    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register(nameof(Process), typeof (UpdateProcess),
                typeof (MainWindow), new PropertyMetadata(new UpdateProcess()));

        public MainWindow()
        {
            InitializeComponent();
        }

        public UpdateProcess Process
        {
            get { return (UpdateProcess) GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
    }
}