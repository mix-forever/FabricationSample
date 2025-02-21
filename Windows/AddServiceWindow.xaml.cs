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
  public partial class AddServiceWindow : Window
  {
    #region ctor

    public AddServiceWindow()
    {
      InitializeComponent();
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
      List<ServiceTemplate> templates = Database.ServiceTemplates.OrderBy(x => x.Name).ToList();
      cmbServiceTemplates.ItemsSource = new ObservableCollection<ServiceTemplate>(templates);
    }

    private void btnAddService_Click(object sender, RoutedEventArgs e)
    {
      string name = txtNewServiceName.Text;
      string group = txtNewServiceGroup.Text;

      if (string.IsNullOrWhiteSpace(name))
      {
        MessageBox.Show("Service name cannot be empty", "Service", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //FabricationManager.DBEditor.addService(serviceTab);
        return;
      }
      else if (cmbServiceTemplates.SelectedItem == null)
      {
        MessageBox.Show("A service template needs to be selected", "Service", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        return;
      }

      FabricationManager.DBEditor.addService(name, group, cmbServiceTemplates.SelectedItem as ServiceTemplate);
      this.Close();
    }

    #endregion


  }
}
