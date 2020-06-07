using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTvContents.collection;
using WpfTvContents.common;
using WpfTvContents.data;
using WpfTvContents.service;

namespace WpfTvContents
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContentsCollection ColViewContents;

        ContentsService service;

        private ContentsData _DispInfoSelectGridMainContents = null;
        private Player _Player = new Player();

        public MainWindow()
        {
            InitializeComponent();
            service = new ContentsService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColViewContents = new ContentsCollection();
            GridMainContents.ItemsSource = ColViewContents.ColViewListData;
        }

        private void BtnMainSearch_Click(object sender, RoutedEventArgs e)
        {
            ColViewContents.SearchByText(TxtMainSearch.Text);
        }

        private void GridMainContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _DispInfoSelectGridMainContents = (ContentsData)GridMainContents.SelectedItem;

            if (String.IsNullOrEmpty(_DispInfoSelectGridMainContents.Path))
                txtStatusBar.Text = "Pathなし";

            cmbLargeRating1.SelectedItem = _DispInfoSelectGridMainContents.Rating1;
            cmbLargeRating2.SelectedItem = _DispInfoSelectGridMainContents.Rating2;
            txtLargeComment.Text = _DispInfoSelectGridMainContents.Comment;
        }


        private void GridMainContents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _DispInfoSelectGridMainContents = (ContentsData)GridMainContents.SelectedItem;

            try
            {
                _Player.Execute(_DispInfoSelectGridMainContents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private int GetComboboxRating(int myBeforeRating, object mySender)
        {
            ComboBox combo = mySender as ComboBox;

            int changeRating = Convert.ToInt32(combo.SelectedItem);

            if (changeRating == myBeforeRating)
                return -1;

            return changeRating;
        }

        private void OnChangedRating1(object sender, SelectionChangedEventArgs e)
        {
            int newRating1 = GetComboboxRating(_DispInfoSelectGridMainContents.Rating1, sender);

            if (newRating1 < 0)
                return;

            service.UpdateRatingComment(newRating1, _DispInfoSelectGridMainContents.Rating2, _DispInfoSelectGridMainContents.Comment, _DispInfoSelectGridMainContents.Id, new MySqlDbConnection());
            _DispInfoSelectGridMainContents.Rating1 = newRating1;
        }

        private void OnChangedRating2(object sender, SelectionChangedEventArgs e)
        {
            int newRating2 = GetComboboxRating(_DispInfoSelectGridMainContents.Rating2, sender);

            if (newRating2 < 0)
                return;

            service.UpdateRatingComment(_DispInfoSelectGridMainContents.Rating1, newRating2, _DispInfoSelectGridMainContents.Comment, _DispInfoSelectGridMainContents.Id, new MySqlDbConnection());
            _DispInfoSelectGridMainContents.Rating2 = newRating2;
        }


        private void OnEditEndComment(object sender, RoutedEventArgs e)
        {
            if (_DispInfoSelectGridMainContents.Comment.Equals(txtLargeComment.Text))
                return;

            service.UpdateRatingComment(_DispInfoSelectGridMainContents.Rating1, _DispInfoSelectGridMainContents.Rating2, txtLargeComment.Text, _DispInfoSelectGridMainContents.Id, new MySqlDbConnection());
            _DispInfoSelectGridMainContents.Comment = txtLargeComment.Text;
        }
    }
}
