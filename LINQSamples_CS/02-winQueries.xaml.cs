using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public partial class winQueries : Window
  {
    #region Constructor
    public winQueries()
    {
      InitializeComponent();
    }
    #endregion

    #region Select Single Node using XDocument and LINQ
    private void btnLinqSingleXDocument_Click(object sender, RoutedEventArgs e)
    {
      LinqQuerySingleXDocument();
    }

    private void LinqQuerySingleXDocument()
    {
      XDocument doc = XDocument.Load(AppConfig.GetMenuFile());

      // Have to use Descendants when using XDocument class
      XElement elem = (from node in doc.Descendants("Menu")
                       where node.Element("MenuID").Value == "10"
                       select node).SingleOrDefault();

      // OR, you must use Root.Elements
      //XElement elem = (from node in doc.Root.Elements("Menu")
      //         where node.Element("MenuID").Value == "10"
      //         select node).SingleOrDefault();


      if (elem != null)
      {
        txtXML.Text = elem.Element("MenuText").Value;
      }
    }
    #endregion

    #region Select Single Node using XElement and LINQ
    private void btnLinqSingleElement_Click(object sender, RoutedEventArgs e)
    {
      LinqQuerySingleXElement();
    }

    private void LinqQuerySingleXElement()
    {
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // Just use Elements() when using XElement class
      XElement elem = (from node in doc.Elements("Menu")
                       where node.Element("MenuID").Value == "10"
                       select node).SingleOrDefault();

      if (elem != null)
        txtXML.Text = elem.Element("MenuText").Value;
    }
    #endregion

    #region Select All Nodes
    private void btnLinqQueryMultiple_Click(object sender, RoutedEventArgs e)
    {
      GetAllMenus();
    }

    private void GetAllMenus()
    {
      StringBuilder sb = new StringBuilder(1024);
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // Get All Menus
      IEnumerable<XElement> nodes =
              from node in doc.Elements("Menu")
              select node;

      foreach (XElement node in nodes)
      {
        sb.Append(node.Element("MenuText").Value);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region Where Clause
    private void btnLinqWhere_Click(object sender, RoutedEventArgs e)
    {
      LinqWhereClause();
    }

    private void LinqWhereClause()
    {
      StringBuilder sb = new StringBuilder(1024);
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // Get All Top Level Menus
      IEnumerable<XElement> nodes =
              from node in doc.Descendants("Menu")
              where string.IsNullOrEmpty(node.Element("ParentMenuID").Value)
              select node;

      foreach (XElement node in nodes)
      {
        sb.Append(node.Element("MenuText").Value);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region Select Node using Attributes
    private void btnLinqAttribute_Click(object sender, RoutedEventArgs e)
    {
      LinqQuerySingleAttribute();
    }

    private void LinqQuerySingleAttribute()
    {
      XElement doc = XElement.Load(AppConfig.GetVisitsFile());

      XElement elem = (from node in doc.Elements("Visit")
                       where node.Attribute("Name").Value == "Ken Getz"
                       select node).SingleOrDefault();

      if (elem != null)
        txtXML.Text = elem.Attribute("Date").Value;
    }
    #endregion

    #region Order By Clause
    private void btnOrderBy_Click(object sender, RoutedEventArgs e)
    {
      OrderBySample();
    }

    private void OrderBySample()
    {
      StringBuilder sb = new StringBuilder(1024);
      XElement doc = XElement.Load(AppConfig.GetMenuFile());

      // Order By Display Order
      IEnumerable<XElement> nodes =
              from node in doc.Descendants("Menu")
              orderby Convert.ToInt32(node.Element("DisplayOrder").Value)
              select node;

      foreach (var node in nodes)
      {
        sb.Append(node.Element("MenuText").Value);
        sb.Append(" (");
        sb.Append(node.Element("DisplayOrder").Value);
        sb.Append(")");
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region RSS Feed Sample
    private void btnRSSFeed_Click(object sender, RoutedEventArgs e)
    {
      RSSFeedSample();
    }

    private void RSSFeedSample()
    {
      StringBuilder sb = new StringBuilder(1024);
      XElement doc = XElement.Load(
        "http://feeds.feedburner.com/PaulSheriffsOuterCircleBlog");

      // Have to use 'var' keyword because we 
      // are building an anonymous type
      var nodes =
          from node in doc.Elements("channel").Elements("item")
          select new
          {
            Title = node.Element("title").Value,
            Url = node.Element("link").Value,
            PublicationDate = node.Element("pubDate").Value
          };

      foreach (var node in nodes)
      {
        sb.Append("Title: " + node.Title);
        sb.Append(Environment.NewLine);
        sb.Append("Url: " + node.Url);
        sb.Append(Environment.NewLine);
        sb.Append("Publication Date: " + node.PublicationDate);
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
      }

      txtXML.Text = sb.ToString();
    }
    #endregion

    #region Join Sample
    private void btnJoin_Click(object sender, RoutedEventArgs e)
    {
      JoinSample();
    }

    /// <summary>
    /// Join customers and orders, and create a new XML document with a different shape.
    /// NOTE: The following sample comes from http://msdn.microsoft.com/en-us/library/bb387080.aspx 
    /// </summary>
    private void JoinSample()
    {
      XElement doc;

      // Load Customer and Orders XML File
      doc = XElement.Load(AppConfig.GetCustomerOrdersFile());

      // Create new XElement object that has new shape
      XElement newDoc =
        new XElement("Root",
          from c in doc.Element("Customers").Elements("Customer")
          join o in doc.Element("Orders").Elements("Order")
                    on (string)c.Attribute("CustomerID") equals
                      (string)o.Element("CustomerID")
          where c.Attribute("CustomerID").Value == "GREAL"
          select new XElement("Order",
              new XElement("CustomerID", (string)c.Attribute("CustomerID")),
              new XElement("CompanyName", (string)c.Element("CompanyName")),
              new XElement("ContactName", (string)c.Element("ContactName")),
              new XElement("EmployeeID", (string)o.Element("EmployeeID")),
              new XElement("OrderDate", (DateTime)o.Element("OrderDate"))
          )
      );

      txtXML.Text = newDoc.ToString();
    }
    #endregion

    #region File System Sample
    private void btnFileSystem_Click(object sender, RoutedEventArgs e)
    {
      XElement doc;

      doc = CreateFileSystemXml(AppConfig.GetCurrentDirectory());

      txtXML.Text = doc.ToString();
    }

    private XElement CreateFileSystemXml(string path)
    {
      DirectoryInfo di = new DirectoryInfo(path);

      return new XElement("Folder",
          new XAttribute("Name", di.Name),
          from d in Directory.GetDirectories(path)
          select CreateFileSystemXml(d),
          from fi in di.GetFiles()
          select new XElement("File",
              new XElement("Name", fi.Name),
              new XElement("Length", fi.Length)
          )
      );
    }
    #endregion
  }
}
