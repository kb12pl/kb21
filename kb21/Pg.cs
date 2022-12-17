using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
//using Faithlife.Utility.Dapper;
using kb_ret;
using static kb21.Log;
using System.Data;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace kb21
{

    public class Pg
    {
        static string connString; 
          
        static Pg()
        {
            connString += "Host=" + Conf.Secret("pg.host") + ";";
            connString += "Database=" + Conf.Secret("pg.database") + ";";
            connString+= "Username="+Conf.Secret("pg.user")+";";
            connString+= "Password="+Conf.Secret("pg.pass")+";";            

        }
        public static async Task InsertAsync(string query, object[] rows)
        {
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //await using var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES ($1)", conn);
            //cmd.Parameters.AddWithValue("Hello world");
            //await cmd.ExecuteNonQueryAsync();
                        
            await conn.ExecuteAsync(query,rows);
            conn.Close();
        }

        public static void Insert(string query,IEnumerable<dynamic> rows)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();


            conn.Execute(query,rows);           
                        
            conn.Close();
        }

        

        public static async Task ExecuteAsync(string query)
        {
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await conn.ExecuteAsync(query);
            conn.Close();
        }
        
        public static void Execute(string query)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            conn.Execute(query);
            conn.Close();
        }

        static public async Task<Ret> SelectAsync(MyArg arg)
        {
            Ret ret=new();            

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand(arg.Get("query"), conn);

            cmd.AllResultTypesAreUnknown = true;
            await using var reader = await cmd.ExecuteReaderAsync();
            
            int row=0;            
            while (await reader.ReadAsync())
            {
                if(row==0)
                {
                    ret.SetCols(reader.FieldCount);

                }

                for (int i = 0; i < reader.FieldCount; i++)
                    ret.Set(row, i, reader.GetString(i));
                row++;
            }
            ret.SetRows(row);
            conn.Close();
            return ret;
        }
        static public Ret Select(MyArg arg)
        {
            Ret ret = new();
            try
            {
                var conn = new NpgsqlConnection(connString);
                conn.Open();
               
                var cmd = new NpgsqlCommand(arg.Get("query"), conn);
                cmd.AllResultTypesAreUnknown = true;
                var reader = cmd.ExecuteReader();

                int row = 0;
                while (reader.Read())
                {
                    if (row == 0)
                    {
                        ret.SetCols(reader.FieldCount);
                        for (int i = 0; i < reader.FieldCount; i++)
                            ret.labels[i]=reader.GetName(i);
                    }

                    for (int i = 0; i < reader.FieldCount; i++)
                        ret.Set(row, i, reader.GetString(i));
                    row++;
                }
                ret.SetRows(row);
                conn.Close();
            }
            catch (NpgsqlException ex)
            {
                ret.SetError(ex.Message);
                return ret;
            }

            return ret;
        }
    }
}