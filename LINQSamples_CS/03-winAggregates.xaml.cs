using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace LINQSamples_CS
{
  public partial class winAggregates : Window
  {
    #region Constructor
    public winAggregates()
    {
      InitializeComponent();
    }
    #endregion

    #region Min Method
    private void btnMin_Click(object sender, RoutedEventArgs e)
    {
      //AggregateMinWrong();
      AggregateMin();
    }

    private void AggregateMin()
    {
      XElement doc = XElement.Load(AppConfig.GetMusicFile());

      int value =
        (from node in doc.Elements("Song")
         select GetInt32Value(node.Element("PlayCount"))).Min();

      txtXML.Text = value.ToString();
    }

    private void AggregateMinWrong()
    {
      XElement doc = XElement.Load(AppConfig.GetMusicFile());

      int value =
        (from node in doc.Elements("Song")
         select Convert.ToInt32(node.Element("PlayCount").Value)).Min();

      txtXML.Text = value.ToString();
    }
    #endregion

    #region Max Method
    private void btnMax_Click(object sender, RoutedEventArgs e)
    {
      AggregateMax();
    }

    private void AggregateMax()
    {
      XElement doc = XElement.Load(AppConfig.GetMusicFile());

      int value =
        (from node in doc.Elements("Song")
         select GetInt32Value(node.Element("PlayCount"))).Max();

      txtXML.Text = value.ToString();
    }
    #endregion

    #region Sum Method
    private void btnSum_Click(object sender, RoutedEventArgs e)
    {
      AggregateSum();
    }

    private void AggregateSum()
    {
      XElement doc = XElement.Load(AppConfig.GetMusicFile());

      int value =
        (from node in doc.Elements("Song")
         select GetInt32Value(node.Element("PlayCount"))).Sum();

      txtXML.Text = value.ToString();
    }
    #endregion

    #region Average Method
    private void btnAvg_Click(object sender, RoutedEventArgs e)
    {
      AggregateAvg();
    }

    private void AggregateAvg()
    {
      XElement doc = XElement.Load(AppConfig.GetMusicFile());

      double value =
        (from node in doc.Elements("Song")
         select GetInt32Value(node.Element("PlayCount"))).Average();

      txtXML.Text = value.ToString();
    }
    #endregion

    #region GetInt32Value Method
    private int GetInt32Value(XElement elem)
    {
      int value = 0;

      if (elem != null)
        if (!string.IsNullOrEmpty(elem.Value))
          value = Convert.ToInt32(elem.Value);

      return value;
    }
    #endregion
  }
}
