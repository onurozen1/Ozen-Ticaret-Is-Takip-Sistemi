using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ozenticaret_Proje
{
    class BAGLANTI
    {
        public static string Adres = ConfigurationManager.ConnectionStrings["ozenticaret_Proje.Properties.Settings.ozenticaret_istakipConnectionString"].ConnectionString;

        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(Adres);
            baglan.Open();
            return baglan;
        }
    }
}
