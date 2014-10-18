using System;
using System.Configuration;

namespace LINQSamples_CS
{
  public class AppConfig
  { 
    public static string SimpleMenuFileName
    {
      get { return @"D:\Samples\SimpleMenu.xml"; }
    }

    public static string GetCustomerOrdersFile()
    {
      string file = null;

      file = GetCurrentDirectory();
      file += "\\XML\\CustomersOrders.xml";

      return file;
    }

    public static string GetMusicFile()
    {
      string file = null;

      file = GetCurrentDirectory();
      file += "\\XML\\Music.xml";

      return file;
    }

    public static string GetMenuFile()
    {
      string file = null;

      file = GetCurrentDirectory();
      file += "\\XML\\Menu.xml";

      return file;
    }

    public static string GetVisitsFile()
    {
      string file = null;

      file = GetCurrentDirectory();
      file += "\\XML\\CustomerVisitAttrib.xml";

      return file;
    }

    public static string GetCurrentDirectory()
    {
      string file;

      file = AppDomain.CurrentDomain.BaseDirectory;
      if (file.IndexOf(@"\bin") > 0)
      {
        file = file.Substring(0, file.LastIndexOf(@"\bin"));
      }

      return file;
    }
  }
}
