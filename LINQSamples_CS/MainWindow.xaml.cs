using System;
using System.Windows;

namespace LINQSamples_CS
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnCreation_Click(object sender, RoutedEventArgs e)
    {
      winCreate win = new winCreate();

      win.Show();
    }

    private void btnQuery_Click(object sender, RoutedEventArgs e)
    {
      winQueries win = new winQueries();

      win.Show();
    }

    private void btnAggregate_Click(object sender, RoutedEventArgs e)
    {
      winAggregates win = new winAggregates();

      win.Show();
    }

    private void btnTypes_Click(object sender, RoutedEventArgs e)
    {
      winTypes win = new winTypes();

      win.Show();
    }

    private void btnModification_Click(object sender, RoutedEventArgs e)
    {
      winModification win = new winModification();

      win.Show();
    }

    private void btnWrapperClass_Click(object sender, RoutedEventArgs e)
    {
      winWrapperClass win = new winWrapperClass();

      win.Show();
    }
  }
}
