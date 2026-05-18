using Or.Business;
using Or.Models;
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

namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour ListeBenef.xaml
    /// </summary>
    public partial class ListeBenef : PageFunction<long>
    {
        public ListeBenef(long numCarte)
        {
            InitializeComponent();
            Carte carte = SqlRequests.InfosCarte(numCarte);
            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiaireAssocieClient(numCarte);

            NomCarte.Text = carte.NomClient;
            PrenomCarte.Text = carte.PrenomClient;
            IdCarte.Text = numCarte.ToString();

            listView.ItemsSource = beneficiaires;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }


        private void DeleteBenef(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var benef = btn.DataContext as Beneficiaire;

            SqlRequests.SuppressionBeneficiaire(benef._idCarte, benef.IdCompte);
            listView.ItemsSource = SqlRequests.ListeBeneficiaireAssocieClient(benef._idCarte);
        }

        private void AddBenef(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.10; // 10%
                gridView.Columns[1].Width = totalWidth * 0.30; // 40%
                gridView.Columns[2].Width = totalWidth * 0.30; // 20%
                gridView.Columns[3].Width = totalWidth * 0.30; // 20%
            }
        }
    }
}
