using System;
using System.Reflection;
using System.Windows.Interop;
using System.Diagnostics;
using System.Collections.Specialized;

using Autodesk.Fabrication.UI;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Fabrication.DB;
using System.Windows.Threading;

[assembly: ExtensionApplication(typeof(FabricationSample.ACADSample))]

namespace FabricationSample
{
  public class Sample : IExternalApplication
  {
    FabricationWindow win = null;
    public Sample()
    {
    }

    public void Execute()
    {
      win = new FabricationWindow();
      WindowInteropHelper wih = new WindowInteropHelper(win);
      wih.Owner = Process.GetCurrentProcess().MainWindowHandle;

      win.ShowDialog();
    }

    public void Terminate()
    {
      Database.Clear();
      ProductDatabase.Clear();

      Dispatcher.CurrentDispatcher.InvokeShutdown();
      
      win.Close();
    }
  }

  public class ACADSample : IExtensionApplication
  {
    FabricationWindow _win = null;
    private readonly string _apiPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}" +
                                       $"\\Autodesk\\Fabrication SPECTRE\\CADmep\\FabricationAPI.dll";
    private Assembly _asm = null;
    private const string CadMepArx = "cadmep23.arx";

    [CommandMethod("FabAPI", "FabAPI", CommandFlags.Modal)]
    public void RunFabApi()
    {
      if (CheckCadMepLoaded() && CheckApiLoaded())
      {
        _win = new FabricationWindow();
        _win.ShowDialog();
      }
    }

    public void Initialize()
    {
      CheckApiLoaded();
    }

    public void Terminate()
    {
       Database.Clear();
       ProductDatabase.Clear();

       Dispatcher.CurrentDispatcher.InvokeShutdown();
    }

    #region Fabrication API Checking routines
    private bool CheckApiLoaded()
    {
      _asm = Assembly.LoadFrom(_apiPath);
      bool loaded = _asm != null;

      if (!loaded)
        System.Windows.Forms.MessageBox.Show("FabricationAPI.dll could not be loaded", "Fabrication API",
          System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

      return loaded;
    }

    private bool CheckCadMepLoaded()
    {
      StringCollection modules = SystemObjects.DynamicLinker.GetLoadedModules();
      bool cadMepLoaded = modules.Contains(CadMepArx);

      if (!cadMepLoaded)
        System.Windows.Forms.MessageBox.Show("CADmep is not loaded and is required to run this addin", "Fabrication API",
          System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

      return cadMepLoaded;
    }
    #endregion

  }
}

