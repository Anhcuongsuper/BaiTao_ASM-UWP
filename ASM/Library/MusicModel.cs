using ASM.Entity;
using ASM.Library;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.MusicLibrary
{
    class MusicModel
    {
        public static void Add(Song song)
        {
            using (SqliteConnection db = new SqliteConnection(DataAccess.SQL_CONNECTION_STRING))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                //
                insertCommand.CommandText = "INSERT INTO Songs (name, thumbail) VALUES (@name, @thumbail);";
                insertCommand.Parameters.AddWithValue("@name", song.name);
                insertCommand.Parameters.AddWithValue("@thumbail", song.thumbnail);
                insertCommand.ExecuteReader();
            }
        }
    }
}
