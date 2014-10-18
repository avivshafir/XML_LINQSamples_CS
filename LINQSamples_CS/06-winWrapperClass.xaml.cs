using System;
using System.Windows;

namespace LINQSamples_CS
{
  public partial class winWrapperClass : Window
  {
    #region Constructor
    public winWrapperClass()
    {
      InitializeComponent();
    }
    #endregion

    // Menu Items Manager Object
    private MenuItemManager _Menus = null;

    #region GetMenuItems Method
    private MenuItemManager GetMenuItems()
    {
      if (_Menus == null)
        _Menus = new MenuItemManager(AppConfig.GetMenuFile());

      return _Menus;
    }
    #endregion

    #region Loading Methods
    private void winMenuItemsTest_Load(object sender, RoutedEventArgs e)
    {
      MenusLoad();
    }

    private void MenusLoad()
    {
      GetMenuItems();

      var allMenus = _Menus.GetMenus();

      lstMenus.DisplayMemberPath = "MenuText";
      lstMenus.SelectedValuePath = "MenuID";
      lstMenus.ItemsSource = allMenus;
    }

    private void lstMenus_SelectionChanged(object sender, RoutedEventArgs e)
    {
      GetMenuItems();

      if (lstMenus.SelectedValue != null)
      {
        _Menus.LoadByMenuID(lstMenus.SelectedValue.ToString());

        FormShow(_Menus.CurrentMenu);
      }
    }

    private void FormShow(MenuItem mi)
    {
      txtMenuID.Text = mi.MenuID;
      txtMenuText.Text = mi.MenuText;
      txtParentMenuID.Text = mi.ParentMenuID;
      txtAction.Text = mi.Action;
      txtDisplayOrder.Text = mi.DisplayOrder;
      chkEnabled.IsChecked = Convert.ToBoolean(mi.Enabled);
    }
    #endregion

    #region Insert Method
    private void btnInsert_Click(object sender, RoutedEventArgs e)
    {
      GetMenuItems();

      MoveToMenuItemsClass(_Menus.CurrentMenu);

      if (_Menus.Insert())
      {
        MenusLoad();
        MessageBox.Show("Menu Inserted");
      }
      else
      {
        MessageBox.Show(_Menus.Messages);
      }
    }
    #endregion

    #region Update Method
    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
      GetMenuItems();

      MoveToMenuItemsClass(_Menus.CurrentMenu);

      if (_Menus.Update(txtMenuID.Text))
      {
        MenusLoad();
        MessageBox.Show("Menu Updated");
      }
      else
      {
        MessageBox.Show(_Menus.Messages);
      }
    }
    #endregion

    #region Delete Method
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
      GetMenuItems();

      MoveToMenuItemsClass(_Menus.CurrentMenu);

      _Menus.Delete(txtMenuID.Text);

      MenusLoad();
    }
    #endregion

    #region MoveToMenuItemsClass Method
    private void MoveToMenuItemsClass(MenuItem mi)
    {
      mi.MenuID = txtMenuID.Text;
      mi.ParentMenuID = txtParentMenuID.Text;
      mi.DisplayOrder = txtDisplayOrder.Text;
      mi.MenuText = txtMenuText.Text;
      mi.Action = txtAction.Text;
      mi.Enabled = chkEnabled.IsChecked.ToString();
    }
    #endregion

    #region Clear Method
    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
      txtMenuID.Text = string.Empty;
      txtMenuText.Text = string.Empty;
      txtParentMenuID.Text = string.Empty;
      txtAction.Text = string.Empty;
      txtDisplayOrder.Text = string.Empty;
      chkEnabled.IsChecked = true;
    }
    #endregion
  }
}
