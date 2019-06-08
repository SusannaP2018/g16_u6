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
        // Metod för att Updatera schemat

        public void Updatefrukost(bool frukost,int barn_id)
        {

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update dagsschema set (frukost) = (@frukostbool) where barn_id = @hej";
                    cmd.Parameters.AddWithValue("frukostbool", frukost);
                    cmd.Parameters.AddWithValue("hej", barn_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
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

            string stmt = "SELECT v.vh_id, v.fornamn, v.efternamn FROM vardnadshavare v";

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
                        };
                        vardnadshavares.Add(v);
                    }

                return vardnadshavares;
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
                                    s.Narvarodag = null;
                                }
                            if (!reader.IsDBNull(1))
                            {
                                    s.LedigDag = reader.GetDateTime(1);

                            }
                            else
                            {
                                    s.LedigDag = null;

                            }
                            if (!reader.IsDBNull(2))
                            {
                                    s.Sjukdag = reader.GetDateTime(2);

                            }
                            else
                            {
                                    s.Sjukdag = null;

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

            string stmt = "SELECT * FROM personal WHERE NOT personal_id = 100";

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
        public List<Barn> GetBarnByAvdelning(int avd)
        {
            Barn b;
            List<Barn> barn = new List<Barn>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM barn WHERE avdelning = @avd";
                    cmd.Parameters.AddWithValue("avd", avd);
                    cmd.ExecuteNonQuery();

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

        //metod som hämtar VH för ett barn 
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

        //Metod för att hämta barn baserat på VH
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

        //metod som hämtar det högsta befintliga id numret från närvarotabellen
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

        //metod som hämtar det högsta befintliga id numret från gatt_hem tabellen
        public int gattHemIDMax()
        {
            int nm;
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT MAX(gatt_hem_id) FROM gatt_hem; ";

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

        //Metod som lägger till fårnvarodag (personalvyn)
        public void AddfranvaroDag(int narvaro_id, int barn_id, int personal_id, DateTime franvarodag)
        {
            using (var conn = new
                        NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO narvaro(narvaro_id, barn_id, personal_id, franvarodag) VALUES (@narvaro_id, @barn_id, @personal_id, @franvarodag) ";
                    cmd.Parameters.AddWithValue("narvaro_id", narvaro_id);
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.Parameters.AddWithValue("franvarodag", franvarodag);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Metod som lägger till sjukdag
        public void AddSjukdag(int narvaro_id, int barn_id, int personal_id, DateTime sjukdag)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO narvaro(narvaro_id, barn_id, personal_id, sjukdag) VALUES (@narvaro_id, @barn_id, @personal_id, @sjukdag) ";
                    cmd.Parameters.AddWithValue("narvaro_id", narvaro_id);
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.Parameters.AddWithValue("sjukdag", sjukdag);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Metod som lägger till ledigdag
        public void AddLedigdag(int narvaro_id, int barn_id, int personal_id, DateTime ledigdag)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO narvaro(narvaro_id, barn_id, personal_id, ledigdag) VALUES (@narvaro_id, @barn_id, @personal_id, @ledigdag) ";
                    cmd.Parameters.AddWithValue("narvaro_id", narvaro_id);
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.Parameters.AddWithValue("ledigdag", ledigdag);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // metod som lägger till narvarodag
        public void AddNarvaroDag(int narvaro_id, int barn_id, int personal_id, DateTime narvarodag)
        {
            using (var conn = new
            NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO narvaro(narvaro_id, barn_id, personal_id, narvarodag) VALUES (@narvaro_id, @barn_id, @personal_id, @narvarodag) ";
                    cmd.Parameters.AddWithValue("narvaro_id", narvaro_id);
                    cmd.Parameters.AddWithValue("barn_id", barn_id);
                    cmd.Parameters.AddWithValue("personal_id", personal_id);
                    cmd.Parameters.AddWithValue("narvarodag", narvarodag);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Metod som skriver ut hemgångna barn
        public List<Gatthem> hemgangnaBarn()
        {
            Gatthem gh;
            List<Gatthem> gattHem = new List<Gatthem>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["ik102g_db16"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT " +
                        "g.gatt_hem_id, " +
                        "g.gatt_hem, " +
                        "b.barn_id, " +
                        "p.personal_id, " +
                        "b.fornamn, " +
                        "b.efternamn, " +
                        "p.fornamn " +
                        "FROM gatt_hem g " +
                        "JOIN barn b ON b.barn_id = g.barn_id " +
                        "JOIN personal p ON p.personal_id = g.personal_id ";

                    using (var reader = cmd.ExecuteReader())

                        while (reader.Read())
                        {
                            gh = new Gatthem()
                            {
                                gattHemID = reader.GetInt32(0),
                                gattHem = reader.GetBoolean(1),
                                barnID = reader.GetInt32(2),
                                personalID = reader.GetInt32(3),
                                barnFornamn = reader.GetString(4),
                                barnEfternamn = reader.GetString(5),
                                persFornamn = reader.GetString(6)
                            };
                            gattHem.Add(gh);
                        }
                }
                return gattHem;
            }
        }

        /* //metod som hämtar alla BARN från BARNTABELLEN

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
       }*/
    }
}