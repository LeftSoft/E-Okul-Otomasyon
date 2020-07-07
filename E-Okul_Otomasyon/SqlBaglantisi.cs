using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace E_Okul_Otomasyon
{
    class SqlBaglantisi
    {
        public OleDbConnection sqlbaglan()
        {
            // Sql Bağlantı Sınıfı //
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=eokul.mdb");
            baglan.Open();
            return baglan;
        }
    }
}
