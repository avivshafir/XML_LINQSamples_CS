using System;
using System.Text;

namespace LINQSamples_CS
{
  public class MenuItem
  {
    #region Constructor
    public MenuItem()
    {
    }
    #endregion

    #region Private Variables
    private StringBuilder _Messsages = new StringBuilder(1024);

    private string _MenuID;
    private string _DisplayOrder;
    private string _ParentMenuID;
    private string _MenuText;
    private string _Action;
    private string _Enabled;
    #endregion

    #region Public Properties
    public string MenuID
    {
      get { return _MenuID; }
      set { _MenuID = value; }
    }

    public string DisplayOrder
    {
      get { return _DisplayOrder; }
      set { _DisplayOrder = value; }
    }

    public string ParentMenuID
    {
      get { return _ParentMenuID; }
      set { _ParentMenuID = value; }
    }

    public string MenuText
    {
      get { return _MenuText; }
      set { _MenuText = value; }
    }

    public string Action
    {
      get { return _Action; }
      set { _Action = value; }
    }

    public string Enabled
    {
      get { return _Enabled; }
      set { _Enabled = value; }
    }

    public string Messages
    {
      get { return _Messsages.ToString(); }
    }

    public StringBuilder MessageCollection
    {
      get { return _Messsages; }
    }
    #endregion

    #region Clear Method
    public void Clear()
    {
      _MenuID = string.Empty;
      _DisplayOrder = string.Empty;
      _ParentMenuID = string.Empty;
      _MenuText = string.Empty;
      _Action = string.Empty;
      _Enabled = string.Empty;
    }
    #endregion

    #region Validate Method
    public bool Validate(bool isAdding)
    {
      int value = 0;

      _Messsages = new StringBuilder(1024);

      // Check data passed in from UI
      _Enabled = _Enabled.Trim().ToLower();
      if (_Enabled == string.Empty || _Enabled == "false" || _Enabled == "0")
      {
        _Enabled = "false";
      }
      else
      {
        _Enabled = "true";
      }

      if (_MenuText == string.Empty)
      {
        _Messsages.Append("Menu Text must be filled in" + Environment.NewLine);
      }
      if (_Action == string.Empty)
      {
        _Messsages.Append("Action must be filled in" + Environment.NewLine);
      }
      if (!Int32.TryParse(_DisplayOrder, out value))
      {
        _Messsages.Append("Display Order must be a numeric value" + Environment.NewLine);
      }
      if (!isAdding)
      {
        if (_MenuID.Trim() == string.Empty)
        {
          _Messsages.Append("Menu ID must be filled in" + Environment.NewLine);
        }
      }

      return (_Messsages.ToString() == string.Empty);
    }
    #endregion
  }
}
