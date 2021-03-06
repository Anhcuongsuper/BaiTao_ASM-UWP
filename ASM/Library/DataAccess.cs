﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Library
{
    class DataAccess
    {

        public static string SQL_CONNECTION_STRING = "Filename=song_manager.db";
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection(SQL_CONNECTION_STRING))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT" + "EXITS Songs(Primary_Key INTEGER PRIMARY KEY," + " name NVARCHAR(100), thumbail NVARCHAR(100)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

    }
}
