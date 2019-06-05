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
        // Metod som returnerar ett BARN ID
        public int BarnIDForSchema(int id)
        {
            return id;
        }
        // Metod som hämtar ett valt barns SCHEMA
        public List<Schema> GetOneBarnSchema(int id)
        {
            List<Schema> schemas = GetSchemaBarn();
            List<Schema> barnSchema = new List<Schema>();

            foreach (var s in schemas)
            {
                if (id.Equals(s.Barn_id))
                {
                    barnSchema.Add(s);
                }
            }
            return barnSchema;
        }

        //metod som hämtar alla VARDNADSHAVARE från VARDNADSHAVARETABELLEN
        public List<Vardnadshavare> GetAllVardnadshavare()
        {
            
            Vardnadshavare v;
            List<Vardnadshavare> vardnadshavares = new List<Vardnadshavare>();

            string stmt = "SELECT * FROM vardnadshavare";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
                
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
        }
        //metod som hämtar alla BARN från BARNTABELLEN

        public List<Barn> GetAllBarn()
        {
            Barn b;
            List<Barn> barns = new List<Barn>();

            string stmt = "SELECT * FROM barn";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
                
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                    cmd.CommandText = "select narvaro.narvarodag, narvaro.ledigdag, narvaro.sjukdag, dagsschema.frukost, dagsschema.far_hamta, narvaro.barn_id from narvaro full join dagsschema on narvaro.barn_id = dagsschema.barn_id";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                                s = new Schema();
                                if (!reader.IsDBNull(0))
                                {
                                    s.Narvarodag = reader.GetDateTime(0);
                                }
                                else
                                {
                                    s.Narvarodag = s.Narvarodag.GetValueOrDefault();
                                }
                            if (!reader.IsDBNull(1))
                            {
                                    s.LedigDag = reader.GetDateTime(1);

                            }
                            else
                            {
                                    s.LedigDag = s.LedigDag.GetValueOrDefault();

                            }
                            if (!reader.IsDBNull(2))
                            {
                                    s.Sjukdag = reader.GetDateTime(2);

                            }
                            else
                            {
                                    s.Sjukdag = s.LedigDag.GetValueOrDefault();

                            }

                                    s.Frukost = reader.GetBoolean(3);
                                    s.Far_hamta = reader.GetString(4);
                                    s.Barn_id = reader.GetInt32(5);
                            scheman.Add(s);
                           
                            }

                            
                        }
                        return scheman;
                    }
                }
            }      
        


        //För att hämta all personal
        public List<Personal> GetAllPersonal()
        {
            Personal p;
            List<Personal> personal = new List<Personal>();

            string stmt = "SELECT * FROM personal";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

                    while (reader.Read())
                    {
                        p = new Personal()
                        {
                            id = reader.GetInt32(0),
                            firstname = reader.GetString(1),
                            lastname = reader.GetString(2),
                            avdelning = reader.GetInt32(3)


                        };
                        personal.Add(p);
                    }

                return personal;
            }
        }

        //Hämta barn per avdelning
        public List<Barn> GetBarnAvdelning1()
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            string stmt = "SELECT * FROM barn WHERE avdelning = 1";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                        barn.Add(b);
                    }

                return barn;
            }
        }
        public List<Barn> GetBarnAvdelning2()
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            string stmt = "SELECT * FROM barn WHERE avdelning = 2";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                        barn.Add(b);
                    }

                return barn;
            }
        }
        public List<Barn> GetBarnAvdelning3()
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            string stmt = "SELECT * FROM barn WHERE avdelning = 3";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                        barn.Add(b);
                    }

                return barn;
            }
        }
        public List<Barn> GetBarnAvdelning4()
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            string stmt = "SELECT * FROM barn WHERE avdelning = 4";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                        barn.Add(b);
                    }

                return barn;
            }
        }

        //Metod för att registrera hemgång
        public void Hemgang(int gatt_hem_id, bool gatt_hem, int barn_id, int personal_id)
        {
            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO gatt_hem(gatt_hem_id, gatt_hem, barn_id, personal_id) VALUES (@gatt_hem_id, @gatt_hem, @barn_id, @personal_id)";
                    cmd.Parameters.AddWithValue("gatt_hem_id", gatt_hem_id);
                    cmd.Parameters.AddWithValue("gatt_hem", gatt_hem );
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //metod som hämtar VH för ett barn (funkar ej!!!!!) Susanna funderar
        public List<Vardnadshavare> GetVhByBarn(int barn_id)
        {
            Vardnadshavare vh;
            List<Vardnadshavare> vardnadshavare = new List<Vardnadshavare>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT " +
                        "v.vh_id, " +
                        "v.fornamn, " +
                        "v.efternamn, " +
                        "v.tel " +
                        "FROM vardnadshavare v " +
                        "JOIN barn_vh b ON v.vh_id = b.vh_id " +
                        "WHERE b.barn_id = @barn_id " +
                        "GROUP BY v.vh_id;";
                    cmd.Parameters.AddWithValue("barn_id", barn_id);

                    using (var reader = cmd.ExecuteReader())

                        while (reader.Read())
                        {
                            vh = new Vardnadshavare()
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Telephone = reader.GetString(3)
                            };
                            vardnadshavare.Add(vh);                   
                        }
                }
                return vardnadshavare;
            }
        }

        public List<Barn> GetBarnByVh(int vh_id)
        {
            Barn b;
            List<Barn> bs = new List<Barn>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT " +
                        "b.barn_id, " +
                        "b.fornamn, " +
                        "b.efternamn, " +
                        "b.lokal, " +
                        "b.avdelning " +
                        "FROM barn b " +
                        "JOIN barn_vh v ON b.barn_id = v.barn_id " +
                        "WHERE v.vh_id = @vh_id " +
                        "GROUP BY b.barn_id; ";
                    cmd.Parameters.AddWithValue("vh_id", vh_id);

                    using (var reader = cmd.ExecuteReader())

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
                            bs.Add(b);
                        }
                }
                return bs;
            }
        }


        //metod som hämtar alla barn till en VH(vh)
        public List<Barn> GetBarnByVHOld(int vh)
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            string stmt = "SELECT " +
                        "b.barn_id, " +
                        "b.fornamn, " +
                        "b.efternamn, " +
                        "b.lokal, " +
                        "b.avdelning " +
                        "FROM barn b " +
                        "JOIN barn_vh v ON b.barn_id = v.barn_id " +
                        "WHERE v.vh_id = 3" +
                        ";";

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))

                using (var reader = cmd.ExecuteReader())

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
                        barn.Add(b);
                    }

                return barn;
            }
        }

        //metod för att ändra tabellen
        public void UpdateNarvaroJa(int n_id, int barn_id, int personal_id, DateTime narvarodag)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO narvaro (narvaro_id, barn_id, personal_id, narvarodag)" +
                        "VALUES (@narvaro_id, @barn_id, @personal_id, @narvarodag)";
                    cmd.Parameters.AddWithValue("narvaro_id", n_id);
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.Parameters.AddWithValue("narvarodag", narvarodag);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //metod som hämtar det högsta befintliga id numret
        public int narvaroMax()
        {
            int nm;
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT MAX(narvaro_id) FROM narvaro; ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        nm = new int();
                        while (reader.Read())
                        {
                            nm = reader.GetInt32(0);
                        }
                    }
                }
                return nm;
            }
        }


        /*
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
            } */
    }
}
