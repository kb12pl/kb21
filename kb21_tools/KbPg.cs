using Npgsql;
using Dapper;
using System.Xml;
using static kb21_tools.KbLog;

namespace kb21_tools
{

    public class KbPg
    {
        static string connString;

        static KbPg()
        {
            connString += "Host=" + KbConf.GetSecret("pg.host") + ";";
            connString += "Database=" + KbConf.GetSecret("pg.database") + ";";
            connString += "Username=" + KbConf.GetSecret("pg.user") + ";";
            connString += "Password=" + KbConf.GetSecret("pg.pass") + ";";

        }
        public static async Task InsertAsync(string query, object[] rows)
        {


            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //await using var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES ($1)", conn);
            //cmd.Parameters.AddWithValue("Hello world");
            //await cmd.ExecuteNonQueryAsync();

            await conn.ExecuteAsync(query, rows);
            conn.Close();
        }

        public static void Insert(string query, IEnumerable<dynamic> rows)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();


            conn.Execute(query, rows);

            conn.Close();
        }



        public static async Task ExecuteAsync(string query)
        {
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await conn.ExecuteAsync(query);
            conn.Close();
        }



        static public async Task<Ret> SelectAsync(MyArg arg)
        {
            Ret ret = new();

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand(arg.Get("query"), conn);

            cmd.AllResultTypesAreUnknown = true;
            await using var reader = await cmd.ExecuteReaderAsync();

            int row = 0;
            while (await reader.ReadAsync())
            {
                if (row == 0)
                {
                    ret.SetCols(reader.FieldCount);

                }

                for (int i = 0; i < reader.FieldCount; i++)
                    ret.Set(row, i, reader.GetString(i));
                row++;
            }
            //ret.SetRows(row);
            conn.Close();
            return ret;
        }

        static public Ret Execute(string query)
        {
            Ret ret = new();
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    conn.Execute(query);                 
                }
            }
            catch (NpgsqlException ex)
            {
                ret.SetError(ex.Message);                
                return ret;
            }
            catch (Exception ex)
            {
                ret.SetError("Unexpected error occured:" + ex.Message);
                return ret;
            }

            return ret;
        }

        static public Ret Select(MyArg arg)
        {
            Ret ret = new();
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(arg.Get("query"), conn))
                    {
                        cmd.AllResultTypesAreUnknown = true;
                        using (var reader = cmd.ExecuteReader())
                        {

                            int row = 0;
                            while (reader.Read())
                            {
                                if (row == 0)
                                {
                                    ret.SetCols(reader.FieldCount);
                                    for (int i = 0; i < reader.FieldCount; i++)
                                        ret.labels[i] = reader.GetName(i);
                                }

                                for (int i = 0; i < reader.FieldCount; i++)
                                    if (reader.IsDBNull(i))
                                        ret.Set(row, i, "");
                                    else
                                        ret.Set(row, i, reader.GetString(i));
                                row++;
                            }
                            ret.SetRows(row);
                        }
                    }
                }
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