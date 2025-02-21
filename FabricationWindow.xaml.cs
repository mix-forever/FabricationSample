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

using FabricationSample.UserControls.DatabaseEditor;
using FabricationSample.UserControls.ItemEditor;
using FabricationSample.UserControls.ServiceEditor;
using FabricationSample.Manager;

namespace FabricationSample
{
  public partial class FabricationWindow : Window
  {
    #region ctor

    public FabricationWindow()
    {
      InitializeComponent();
      FabricationManager.ParentWindow = this;
      LoadDBEditorControl();
      //Ensure all Job Items have developments/costs as CADmep usually discards the developments.
      foreach (Item itm in Job.Items)
      {
        itm.Update();
      }
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

    public void LoadDBEditorControl()
    {
      FabricationManager.DBEditor = new DatabaseEditor();
      ControlHost.Content = FabricationManager.DBEditor;
    }

    public void LoadItemEditorControl()
    {
      FabricationManager.ItemEditor = new ItemEditor();
      ControlHost.Content = FabricationManager.ItemEditor;
    }

    public void LoadServiceEditorControl()
    {
      FabricationManager.ServiceEditor = new ServiceEditor();
      ControlHost.Content = FabricationManager.ServiceEditor;
    }

    #endregion

  }
}
