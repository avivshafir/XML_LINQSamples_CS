using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public partial class winModification : Window
  {
    #region Constructor
    public winModification()
    {
      InitializeComponent();
    }
    #endregion

    #region Add Method using XElement Constructor
    private void btnAddMenu_Click(object sender, RoutedEventArgs e)
    {
      AddMenuConstuctor();
    }

    private void AddMenuConstuctor()
    {
      var doc = XElement.Load(AppConfig.GetMenuFile());

      var menu =
          new XElement("Menu",
            new XElement("MenuID", "910"),
            new XElement("Enabled", "true"),
            new XElement("DisplayOrder", "10"),
            new XElement("MenuText", "Another Top Menu"),
            new XElement("Action", "NextPage.aspx"),
            new XElement("ParentMenuID", "")
          );

      doc.Add(menu);

      doc.Save(AppConfig.GetMenuFile());
    }
    #endregion

    #region Add by Cloning
    private void btnAddClone_Click(object sender, RoutedEventArgs e)
    {
      AddMenuClone();
    }

    private void AddMenuClone()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());
      XElement newElem;

      // This assumes at least one element exists
      // Must use FirstOrDefault(), not First() as we 
      // want a return value, even if is Nothing
      XElement elem =
        (from node in doc.Elements("Menu")
         select node).FirstOrDefault();

      if (elem != null)
      {
        // Clone the Element
        newElem = new XElement(elem);

        newElem.Element("MenuID").Value = "920";
        newElem.Element("Enabled").Value = "true";
        newElem.Element("DisplayOrder").Value = "30";
        newElem.Element("MenuText").Value = "An Added LINQ Menu";
        newElem.Element("Action").Value = "NextPage.aspx";
        newElem.Element("ParentMenuID").Value = "";
      }
      else
      {
        // Did not find an element, build new one using Constructor
        newElem =
          new XElement("Menu",
            new XElement("MenuID", "910"),
            new XElement("Enabled", "true"),
            new XElement("DisplayOrder", "10"),
            new XElement("MenuText", "Another Top Menu"),
            new XElement("Action", "NextPage.aspx"),
            new XElement("ParentMenuID", "")
        );
      }

      // Add new element to document
      doc.Add(newElem);
      // Save the Document
      doc.Save(AppConfig.GetMenuFile());
    }
    #endregion

    #region Update Using LINQ
    private void btnUpdateLinq_Click(object sender, RoutedEventArgs e)
    {
      UpdateMenu();
    }

    private void UpdateMenu()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      XElement elem =
        (from node in doc.Elements("Menu")
         where node.Element("MenuID").Value == "10"
         select node).SingleOrDefault();

      if (elem != null)
      {
        elem.Element("MenuText").Value = "Changed Menu";
      }

      doc.Save(AppConfig.GetMenuFile());
    }
    #endregion

    #region Update Using Anonymous - (NOTE: DOES NOT WORK in C#)
    private void btnUpdateAnonymous_Click(object sender, RoutedEventArgs e)
    {
      UpdateMenuAnonymous();
    }

    private void UpdateMenuAnonymous()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // This does NOT work!
      // Creates new Type and all properties are read-only
      var elem =
        (from node in doc.Elements("Menu")
         where node.Element("MenuID").Value == "10"
         select new
         {
           MenuID = node.Element("MenuID").Value,
           MenuText = node.Element("MenuText").Value,
           Action = node.Element("Action").Value,
           Enabled = node.Element("Enabled").Value,
           DisplayOrder = node.Element("DisplayOrder").Value,
           ParentMenuID = node.Element("ParentMenuID").Value
         }).SingleOrDefault();

      if (elem != null)
      {
        // This is READ ONLY in C#
        // elem.MenuText = "Changed";
      }

      doc.Save(AppConfig.GetMenuFile());
    }
    #endregion

    #region Delete a Node
    private void btnDeleteLinq_Click(object sender, RoutedEventArgs e)
    {
      DeleteMenu();
    }

    private void DeleteMenu()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      XElement elem = (from node in doc.Elements("Menu")
                       where node.Element("MenuID").Value == "910"
                       select node).SingleOrDefault();

      if (elem != null)
      {
        elem.Remove();

        doc.Save(AppConfig.GetMenuFile());
      }
    }
    #endregion
  }
}
