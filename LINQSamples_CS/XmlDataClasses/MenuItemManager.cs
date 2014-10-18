using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public class MenuItemManager
  {
    #region Constructors
    public MenuItemManager()
    {
    }

    public MenuItemManager(string fileName)
    {
      _FileName = fileName;
    }
    #endregion

    #region Private Variables
    private StringBuilder _Messages = new StringBuilder(1024);
    private string _FileName;
    private XElement _Document;
    private MenuItem _CurrentMenu = new MenuItem();
    #endregion

    #region Public Properties
    public string FileName
    {
      get { return _FileName; }
      set { _FileName = value; }
    }

    public string Messages
    {
      get { return _Messages.ToString(); }
    }

    public MenuItem CurrentMenu
    {
      get { return _CurrentMenu; }
      set { _CurrentMenu = value; }
    }
    #endregion

    #region Init Method
    protected void Init()
    {
      _CurrentMenu.Clear();
    }
    #endregion

    #region GetMenusAsXElement Method
    public XElement GetMenusAsXElement()
    {
      if (_Document == null)
      {
        _Document = XElement.Load(_FileName);
      }

      return _Document;
    }
    #endregion

    #region GetMenus Method
    public IEnumerable<MenuItem> GetMenus()
    {
      GetMenusAsXElement();

      IEnumerable<MenuItem> nodes =
        (from node in _Document.Elements("Menu")
         select new MenuItem
         {
           MenuID = node.Element("MenuID").Value,
           MenuText = node.Element("MenuText").Value,
           DisplayOrder = node.Element("DisplayOrder").Value,
           Action = node.Element("Action").Value
         });

      return nodes;
    }
    #endregion

    #region SaveMenus Method
    protected void SaveMenus()
    {
      GetMenusAsXElement();

      _Document.Save(_FileName);
    }
    #endregion

    #region MenuIdExists Method
    public bool MenuIDExists(string menuID)
    {
      var menu = GetMenu(menuID);

      return (menu != null);
    }
    #endregion

    #region LoadByMenuID Method
    public bool LoadByMenuID(string menuID)
    {
      bool ret = false;
      XElement elem;

      elem = GetMenu(menuID);
      _CurrentMenu = new MenuItem();

      if (elem != null)
      {
        ret = true;

        _CurrentMenu.MenuID = menuID;
        _CurrentMenu.ParentMenuID = elem.Element("ParentMenuID").Value;
        _CurrentMenu.MenuText = elem.Element("MenuText").Value;
        _CurrentMenu.DisplayOrder = elem.Element("DisplayOrder").Value;
        _CurrentMenu.Action = elem.Element("Action").Value;
        _CurrentMenu.Enabled = elem.Element("Enabled").Value;
      }
      else
      {
        ret = false;

        Init();
      }

      return ret;
    }
    #endregion

    #region GetMenu Method
    public XElement GetMenu(string menuID)
    {
      GetMenusAsXElement();

      XElement elem = 
        (from node in _Document.Elements("Menu")
         where node.Element("MenuID").Value == menuID
         select node).SingleOrDefault();

      return elem;
    }
    #endregion

    #region Validate Method
    public bool Validate(bool isAdding)
    {
      // Validate Current Menu Item
      _CurrentMenu.Validate(isAdding);
      _Messages = _CurrentMenu.MessageCollection;

      if (_CurrentMenu.ParentMenuID != string.Empty)
      {
        // The mnu id must exist
        if (!MenuIDExists(_CurrentMenu.MenuID))
        {
          _Messages.Append("Parent Menu ID must be be a valid Menu ID" + Environment.NewLine);
        }
      }

      return (_Messages.ToString() == string.Empty);
    }
    #endregion

    #region Insert Method
    public bool Insert()
    {
      bool ret;

      if (Validate(true))
      {
        _Document = GetMenusAsXElement();

        // Create Next Menu ID
        _CurrentMenu.MenuID = GetNextMenuID();

        XElement elem =
           new XElement("Menu",
              new XElement("MenuID", _CurrentMenu.MenuID),
              new XElement("Enabled", _CurrentMenu.Enabled),
              new XElement("DisplayOrder", _CurrentMenu.DisplayOrder),
              new XElement("MenuText", _CurrentMenu.MenuText),
              new XElement("Action", _CurrentMenu.Action),
              new XElement("ParentMenuID", _CurrentMenu.ParentMenuID)
            );

        _Document.Add(elem);

        _Document.Save(_FileName);

        ret = true;
      }
      else
      {
        ret = false;
      }

      return ret;
    }
    #endregion

    #region Update Method
    public bool Update(string menuID)
    {
      bool ret;
      XElement elem;

      if (Validate(true))
      {
        GetMenusAsXElement();

        elem = GetMenu(menuID);

        elem.Element("Enabled").Value = _CurrentMenu.Enabled;
        elem.Element("DisplayOrder").Value = _CurrentMenu.DisplayOrder;
        elem.Element("MenuText").Value = _CurrentMenu.MenuText;
        elem.Element("Action").Value = _CurrentMenu.Action;
        elem.Element("ParentMenuID").Value = _CurrentMenu.ParentMenuID;

        SaveMenus();

        ret = true;
      }
      else
      {
        ret = false;
      }

      return ret;
    }
    #endregion

    #region Delete Method
    public void Delete(string menuID)
    {
      GetMenusAsXElement();

      XElement elem = GetMenu(menuID);

      if (elem != null)
      {
        elem.Remove();

        _CurrentMenu = new MenuItem();

        SaveMenus();
      }
    }
    #endregion

    #region GetNextMenuID Method
    public string GetNextMenuID()
    {
      GetMenusAsXElement();

      int value = 
        (from node in _Document.Elements("Menu")
         select Convert.ToInt32(node.Element("MenuID").Value)).Max();

      return Convert.ToString(value + 10);
    }
    #endregion
  }
}
