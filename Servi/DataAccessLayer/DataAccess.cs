using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpiClient.Models;
using Microsoft.Data.SqlClient;

namespace ExpiClient.DataAccessLayer;
internal class DataAccess
{
    public DataAccess()
    {
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

    public IEnumerable<decimal> GetProductPk(String Description)
    {
        using (EXPIContext context = new EXPIContext())
        {

            var pk = from product in context.Products
                     where product.ProductName.Equals(Description)
                     select product.ProductPk;

            return (IEnumerable<decimal>)pk;

        }
    }
    public IEnumerable<decimal> GetStorePk(String Description)
    {
        using (EXPIContext context = new EXPIContext())
        {

            var pk = from store in context.Stores
                     where store.StorageName.Equals(Description)
                     select store.StoragePk;

            return (IEnumerable<decimal>)pk;

        }
    }
    public IEnumerable<decimal> GetMarketPk(String Description)
    {
        using (EXPIContext context = new EXPIContext())
        {

            var pk = from market in context.Markets
                     where market.MarketName.Equals(Description)
                     select market.MarketPk;

            return (IEnumerable<decimal>)pk;

        }
    }
    public IEnumerable<decimal> GetVendortPk(String Description)
    {
        using (EXPIContext context = new EXPIContext())
        {

            var pk = from vendor in context.Vendors
                     where vendor.VendorName.Equals(Description)
                     select vendor.VendorPk;

            return (IEnumerable<decimal>)pk;

        }
    }
}