﻿#pragma checksum "C:\Users\jelong\Documents\Visual Studio 2010\Projects\EightPuzzle\SilverlightApplication2\ResultsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4BA81AB3611D8ECAA95CF38D8A7B08AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace EightPuzzle {
    
    
    public partial class ResultsWindow : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock movesMade;
        
        internal System.Windows.Controls.TextBlock expandedCount;
        
        internal System.Windows.Controls.TextBlock genCount;
        
        internal System.Windows.Controls.TextBlock avgBranchingFactor;
        
        internal System.Windows.Controls.TextBlock effBranchingFactor;
        
        internal System.Windows.Controls.TextBlock actionsToGoal;
        
        internal System.Windows.Controls.DataGrid grid;
        
        internal System.Windows.Controls.Button CancelButton;
        
        internal System.Windows.Controls.Button OKButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SilverlightApplication2;component/ResultsWindow.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.movesMade = ((System.Windows.Controls.TextBlock)(this.FindName("movesMade")));
            this.expandedCount = ((System.Windows.Controls.TextBlock)(this.FindName("expandedCount")));
            this.genCount = ((System.Windows.Controls.TextBlock)(this.FindName("genCount")));
            this.avgBranchingFactor = ((System.Windows.Controls.TextBlock)(this.FindName("avgBranchingFactor")));
            this.effBranchingFactor = ((System.Windows.Controls.TextBlock)(this.FindName("effBranchingFactor")));
            this.actionsToGoal = ((System.Windows.Controls.TextBlock)(this.FindName("actionsToGoal")));
            this.grid = ((System.Windows.Controls.DataGrid)(this.FindName("grid")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
            this.OKButton = ((System.Windows.Controls.Button)(this.FindName("OKButton")));
        }
    }
}
