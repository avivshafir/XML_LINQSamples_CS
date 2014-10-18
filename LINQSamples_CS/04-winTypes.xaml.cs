using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public partial class winTypes : Window
  {
    #region Constructor
    public winTypes()
    {
      InitializeComponent();
    }
    #endregion

    #region Anonymous Type Method
    private void btnAnonymous_Click(object sender, RoutedEventArgs e)
    {
      AnonymousType();
    }

    private void AnonymousType()
    {
      StringBuilder sb = new StringBuilder(1024);

      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // NOTE: 'menus' variable only exists within this scope.
      //       It has no type, thus you can't pass it out of this method
      var nodes =
        from node in doc.Elements("Menu")
        where string.IsNullOrEmpty(node.Element("ParentMenuID").Value)
        select new
        {
          MenuID = node.Element("MenuID").Value,
          MenuText = node.Element("MenuText").Value,
          DisplayOrder = Convert.ToInt32(node.Element("DisplayOrder").Value),
          Action = node.Element("Action").Value
        };

      foreach (var node in nodes)
      {
        sb.Append(node.MenuText);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region LoadAClass Method
    private void btnLoadAClass_Click(object sender, RoutedEventArgs e)
    {
      LoadAClass();
    }

    private void LoadAClass()
    {
      StringBuilder sb = new StringBuilder(1024);
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      IEnumerable<MenuItem> nodes =
        from node in doc.Elements("Menu")
        where string.IsNullOrEmpty(node.Element("ParentMenuID").Value)
        select new MenuItem
        {
          MenuID = node.Element("MenuID").Value,
          MenuText = node.Element("MenuText").Value,
          DisplayOrder = node.Element("DisplayOrder").Value,
          Action = node.Element("Action").Value
        };

      foreach (var node in nodes)
      {
        sb.Append(node.MenuText);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region Pass Back Type
    private void btnTypePass_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder sb = new StringBuilder(1024);
      IEnumerable<MenuItem> nodes;

      nodes = GetMenus();

      foreach (var node in nodes)
      {
        sb.Append(node.MenuText);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }

    private IEnumerable<MenuItem> GetMenus()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      IEnumerable<MenuItem> nodes =
        from node in doc.Elements("Menu")
        where string.IsNullOrEmpty(
              node.Element("ParentMenuID").Value)
        select new MenuItem
        {
          MenuID = node.Element("MenuID").Value,
          MenuText = node.Element("MenuText").Value,
          DisplayOrder = node.Element("DisplayOrder").Value,
          Action = node.Element("Action").Value
        };

      return nodes;
    }
    #endregion
  }
}
