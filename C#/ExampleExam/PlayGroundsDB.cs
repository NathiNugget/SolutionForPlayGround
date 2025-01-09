using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleExam
{
    public class PlayGroundsDB : IPlayGroundRepository
    {
        private string CONNECTIONSTRING = "Data Source=mssql3.unoeuro.com;Initial Catalog=nathanielrisum_dk_db_dev;User ID=nathanielrisum_dk;Password=HknxBh2wDertbf93azm4;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public PlayGroundsDB() { }

        public PlayGround Add(PlayGround pgr)
        {
            string query = "insert into PlayGrounds (Name, MaxChildren, MinAge) values (@pname, @pmaxchildren, @pminage)";
            using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@pname", pgr.Name);
                cmd.Parameters.AddWithValue("@pmaxchildren", pgr.MaxChildren);
                cmd.Parameters.AddWithValue("@pminage", pgr.MinAge);
                cmd.ExecuteNonQuery();
            }

            pgr = null!;
            query = "select * from PlayGrounds where Id = (SELECT max(Id) from PlayGrounds)";
            using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    pgr = ReadFromReader(reader);
                }

            }
            return pgr;
        }

        private PlayGround ReadFromReader(SqlDataReader reader)
        {
            return new PlayGround(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
        }

        

        public PlayGround GetById(int id)
        {
            PlayGround pgr = null!;
            string query = "select * from PlayGrounds where Id = @pid";
            using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@pid", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    pgr = ReadFromReader(reader);
                }
            }

            return pgr;
        }

        public PlayGround Update(int id, PlayGround pgr)
        {
            string query = "update PlayGrounds set Name = @pname, MaxChildren = @pmaxchildren, MinAge = @pminage where Id = @pid";
            using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pid", id);
                cmd.Parameters.AddWithValue("@pname", pgr.Name);
                cmd.Parameters.AddWithValue("@pmaxchildren", pgr.MaxChildren);
                cmd.Parameters.AddWithValue("@pminage", pgr.MinAge);
                cmd.ExecuteNonQuery();
            }

            query = "select * from PlayGrounds where Id = @pid";
            using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pid", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pgr = ReadFromReader(reader);
                }
            }
            return pgr; 
            
        }

        public void Nuke()
        {
            string query = "delete from PlayGrounds";
            using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }


            query = "DBCC CHECKIDENT (PlayGrounds, RESEED, 0) "; 
            using(SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Setup()
        {
            Add(new PlayGround(1, "TEST1", 10, 10));
            Add(new PlayGround(-1, "TEST2", 11, 11)); 
        }

        public List<PlayGround> GetAll()
        {
            string query = "select * from PlayGrounds"; 
            List<PlayGround> list = new List<PlayGround>();
            using (SqlConnection con = new SqlConnection(CONNECTIONSTRING))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    PlayGround pgr = ReadFromReader(reader);
                    list.Add(pgr);
                }

            }
            return list;
        }
    }
}
