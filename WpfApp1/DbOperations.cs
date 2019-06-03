using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;

namespace WpfApp1
{
    class DbOperations
    {
        //metod som hämtar alla VARDNADSHAVAR från VARDNADSHAVARETABELLEN
        public List<Vardnadshavare> GetAllVardnadshavare()
        {
            Vardnadshavare v;
            List<Vardnadshavare> vardnadshavares = new List<Vardnadshavare>();

            string stmt = "SELECT * FROM vardnadshavare";

            var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(stmt, conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                v = new Vardnadshavare()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Telephone = reader.GetString(3)
                    
                };
                vardnadshavares.Add(v);
            }

            return vardnadshavares;

        }
        //metod som hämtar alla BARN från BARNTABELLEN

        public List<Barn> GetAllBarn()
        {
            Barn b;
            List<Barn> barns = new List<Barn>();

            string stmt = "SELECT * FROM barn";

            var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(stmt, conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                b = new Barn()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Lokal = reader.GetString(3),
                    Avdelning = reader.GetInt32(4)


                };
                barns.Add(b);
            }

            return barns;

        }
        // Metod som hämtar barnens SCHEMA
        public List<Schema> GetSchemaBarn()
        {
            Schema s;
            List<Schema> scheman = new List<Schema>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {

                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select narvaro.narvarodag, narvaro.ledigdag, narvaro.sjukdag, dagsschema.frukost, dagsschema.far_hamta, narvaro.barn_id from narvaro inner join dagsschema on narvaro.barn_id = dagsschema.barn_id";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Schema()
                            {
                                Narvarodag = reader.GetDateTime(0),
                                LedigDag = reader.GetDateTime(1),
                                Sjukdag = reader.GetDateTime(2),
                                Frukost = reader.GetBoolean(3),
                                Far_hamta = reader.GetString(4),
                                Barn_id = reader.GetInt32(5)

                            };
                            scheman.Add(s);
                        }
                        return scheman;
                    }

                }



            }


                



        }

        //metod som hämtar en person baserat på ID

        public Person GetPersonById(int id)
        {
            Person p = new Person();
            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM person WHERE person_id = @person_id";

                    cmd.Parameters.AddWithValue("person_id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p.id = reader.GetInt32(0);
                            p.firstname = reader.GetString(1);
                            p.lastname = reader.GetString(2);
                        }
                    }
                }
                return p;
            }
        }

        //metod för att lägga till personer i tabellen
        public void AddNewPerson(int person_id, string firstname, string lastname)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO person(person_id, firstname, lastname) VALUES (@person_id, @firstname, @lastname) ";
                    cmd.Parameters.AddWithValue("person_id", person_id);
                    cmd.Parameters.AddWithValue("firstname", firstname);
                    cmd.Parameters.AddWithValue("lastname", lastname);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //metod för att ändra tabellen
        public void UpdatePerson(int person_id, string firstname, string lastname)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE person SET (firstname, lastname) = " +
                        "(@firstname, @lastname) WHERE person_id = @person_id";
                    cmd.Parameters.AddWithValue("person_id", person_id);
                    cmd.Parameters.AddWithValue("firstname", firstname);
                    cmd.Parameters.AddWithValue("lastname", lastname);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //metod för att ta bort data i tabellen
        public void DeletePerson(int id)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM person WHERE person_id = @person_id";
                    cmd.Parameters.AddWithValue("@person_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
