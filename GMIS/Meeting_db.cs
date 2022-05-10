﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMIS
{
    abstract public class Meeting_db
    {
        private const string db = "gmis";
        private const string user = "gmis";
        private const string pass = "gmis";
        private const string server = "alacritas.cis.utas.edu.au";

        static MySqlConnection? conn;

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2}; Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static ObservableCollection<Meeting> LoadAll()
        {
            ObservableCollection<Meeting> meetings = new ObservableCollection<Meeting>();

            // Declare a data reader
            MySqlDataReader? rdr = null;

            try
            {
                // Instantiate a connection
                conn = GetConnection();
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                MySqlCommand cmd = new MySqlCommand("select meeting_id, group_id, day, start, end, room from meeting", conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    
                    Meeting m = new Meeting { meeting_id = rdr.GetInt32(0), day = rdr.GetString(2), start = rdr.GetString(3), end = rdr.GetString(4), room = rdr.GetString(5) };
                    if (!rdr.IsDBNull(1))
                    {
                        m.group_id = rdr.GetString(1);
                    }
                    else
                    {
                        m.group_id = null;
                    }
                    meetings.Add(m);
                }
            }
            catch (MySqlException m)
            {
                Console.WriteLine("Error connecting to database: " + m);
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return meetings;
        }
    }
}

