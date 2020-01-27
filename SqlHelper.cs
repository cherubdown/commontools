using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet2.Helpers
{
    public static class SqlHelper
    {
        public static int GetCountFromQuery(string query, SqlParameter[] parameters = null)
        {
            int result = 0;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    if (parameters != null)
                    {
                        foreach (var p in parameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    result = (int)cmd.ExecuteScalar();

                    cmd.Parameters.Clear();
                }
            }
            return result;
        }
    }
}
