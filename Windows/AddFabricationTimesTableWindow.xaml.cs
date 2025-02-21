﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

using Autodesk.Fabrication;
using Autodesk.Fabrication.DB;
using Autodesk.Fabrication.Results;
using Autodesk.Fabrication.Geometry;

using FabricationSample.FunctionExamples;
using FabricationSample.Data;

using FabricationSample.Manager;

namespace FabricationSample
{
  public partial class AddFabricationTimesTableWindow : Window
  {
    #region Private Members
    FabricationTimesTableBase _ft;
    #endregion

    #region Public Properties
    public FabricationTimesTableBase FabricationTimesTable { get { return _ft; } }
    #endregion

    #region ctor

    public AddFabricationTimesTableWindow(FabricationTimesTableBase ft)
    {
      InitializeComponent();

      _ft = ft;
    }

    #endregion

    #region Window Methods

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ButtonState == MouseButtonState.Pressed)
      {
        base.OnMouseLeftButtonDown(e);
        DragMove();
      }
    }

    private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void btnAddTimesTable_Click(object sender, RoutedEventArgs e)
    {
      TableType tableType;
      if (cmbTableTypes.SelectedIndex == 0)
        tableType = TableType.BreakPoint;
      else
        tableType = TableType.ProductId;

      _ft = FabricationAPIExamples.AddNewFabricationTimesTable(txtNewName.Text, txtNewGroup.Text, tableType);
      this.Close();
    }

    #endregion

  }
}
