using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    class DataBase
    {
        private MySqlConnection mysql = new MySqlConnection("server=localhost;user id=root; password=password;persistsecurityinfo=True;database=notesdb");

        public void OpenDB()
        {
            mysql.Open();
        }

        public void CloseDB()
        {
            mysql.Close();
        }

        public DataTable Data(string username , string password)
        {
            
            DataTable dt = new DataTable();
            MySqlDataAdapter msda = new MySqlDataAdapter("select username, password from users where username='"+ username+ "' and password='" + password + "'", mysql);
            msda.Fill(dt);

            return dt;
        }


        public DataTable noteData()
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter msda = new MySqlDataAdapter("select * from notes", mysql);
            msda.Fill(dt);

            return dt;
        }

        public DataTable noteAdd(string title, string contents, string name)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            MySqlDataAdapter msda = new MySqlDataAdapter("insert into notes (title,datecreated, contents,owner) values ('"+title+ "','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + contents+"','"+name+"')", mysql);
            msda.Fill(ds);
            msda = new MySqlDataAdapter("select * from notes", mysql);
            msda.Fill(dt);
            return dt;
        }

        public DataTable noteDelete(string noteId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            MySqlDataAdapter msda = new MySqlDataAdapter("delete from notes where noteid="+ Int32.Parse(noteId), mysql);
            msda.Fill(ds);
            msda = new MySqlDataAdapter("select * from notes", mysql);
            msda.Fill(dt);
            return dt;
        }
    }
}
