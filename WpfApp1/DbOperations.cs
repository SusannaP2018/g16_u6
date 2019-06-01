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
        //metod som hämtar alla personer från persontabellen
        public List<Person> GetAllPersons()
        {
            Person p;
            List<Person> persons = new List<Person>();

            string stmt = "SELECT * FROM person";

            var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(stmt, conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                p = new Person()
                {
                    id = reader.GetInt32(0),
                    firstname = reader.GetString(1),
                    lastname = reader.GetString(2)
                };
                persons.Add(p);
            }

            return persons;

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
