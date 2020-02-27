using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SQLiteApplication
{
    public class SQLiteDataAccess
    {
        public static List<PersonModel> LoadPeople()
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>("SELECT * FROM Person", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void LoadGridPeople(DataGrid dg) 
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());

            cnn.Open();

            string Query = "SELECT * FROM Person";

            SQLiteCommand createCommand = new SQLiteCommand(Query, cnn);
            createCommand.ExecuteNonQuery();


            SQLiteDataAdapter dataAdapt = new SQLiteDataAdapter(createCommand);
            DataTable dt = new DataTable("Person");
            dataAdapt.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
            dataAdapt.Update(dt);
            cnn.Close();
        }

        public static void SavePerson (PersonModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Person (FirstName, LastName) values (@FirstName, @LastName)", person);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
