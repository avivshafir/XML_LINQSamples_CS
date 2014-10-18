using System;
using System.Windows;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public partial class winCreate : Window
  {
    #region Constructor
    public winCreate()
    {
      InitializeComponent();
    }
    #endregion

    #region Build Empty XML Document
    private void btnBuildXML1_Click(object sender, RoutedEventArgs e)
    {
      BuildXML1();
    }

    private void BuildXML1()
    {
      XDocument doc =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Prototype Menus"),
          new XElement("Menus"));

      txtXML.Text = doc.ToString();

      // Could also write to File
      // menu.Save(AppConfig.SimpleMenuFileName);
      // txtXML.Text = File.ReadAllText(AppConfig.SimpleMenuFileName);
    }
    #endregion

    #region Build Populated XML Document
    private void btnBuildXML2_Click(object sender, RoutedEventArgs e)
    {
      BuildXML2();
    }

    private void BuildXML2()
    {
      XDocument doc =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Prototype Menus"),
          new XElement("Menus",
            new XElement("Menu",
            new XElement("MenuID", "10"),
            new XElement("Enabled", "true"),
            new XElement("DisplayOrder", "10"),
            new XElement("MenuText", "Home"),
            new XElement("Action", "NextPage.aspx"),
            new XElement("ParentMenuID", "")
            )
          )
        );

      txtXML.Text = doc.ToString();

      // Could also write to File
      // menu.Save(AppConfig.SimpleMenuFileName);
      // txtXML.Text = File.ReadAllText(AppConfig.SimpleMenuFileName);
    }
    #endregion

    #region Load using XDocument
    private void btnLoadXDocument_Click(object sender, RoutedEventArgs e)
    {
      //XDocument doc = XDocument.Load(AppConfig.GetMenuFile());
      var doc = XDocument.Load(AppConfig.GetMenuFile());

      txtXML.Text = doc.ToString();
    }
    #endregion

    #region Load using XElement
    private void btnLoadXElement_Click(object sender, RoutedEventArgs e)
    {
      //XElement doc = XElement.Load(AppConfig.GetMenuFile());
      var doc = XElement.Load(AppConfig.GetMenuFile());

      txtXML.Text = doc.ToString();
    }
    #endregion
  }
}
