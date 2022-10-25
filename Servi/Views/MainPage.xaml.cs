using System.Collections.ObjectModel;
using ExpiClient.DataAccessLayer;
using Microsoft.UI.Xaml.Controls;
using Servi.DataAccessLayer;
using Servi.ViewModels;

namespace Servi.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
            public ObservableCollection<String> vendors = new DataAccess().ComboboxFromSql("select distinct vendor_name from vendors");

}

private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        button.Content = "Hola";
    }
    public void getStoreName()
    {
        using EXPIContext context = new EXPIContext();
        /*
        int pk = 2146;
        var name = context.Stores.FromSqlRaw("SELECT * FROM Stores Where storage_pk = {0}", pk);
        */
        var query1 = from b in context.Stores
                     where context.ActivePostCountForBlog(b.StoragePk) == 2145
                     select b.StorageName;

    }
    public ObservableCollection<String> ComboboxFromSql(String SqlSelectQuery)
    {
        var SOURCE = "Data Source=localhost;Initial Catalog=EXPI;Integrated Security=SSPI;";
        SqlConnection Connection = new SqlConnection(SOURCE);
        Connection.Open();
        //var SqlSelectQuery = "select distinct vendor_name from vendors";
        SqlCommand Command = new SqlCommand(SqlSelectQuery, Connection);
        ObservableCollection<String> vendors = new ObservableCollection<String>();
        SqlDataReader reader = Command.ExecuteReader();
        while (reader.Read())
        {
            vendors.Add(reader.GetString(0));
        }
        Connection.Close();
        return vendors;
    }
}
