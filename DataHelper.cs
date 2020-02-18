using Intranet2.Common.Attributes;
using Intranet2.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intranet2.Helpers
{
    public class DataHelper<T> where T : new()
    {
        public DataHelper()
        {
        }

        public IEnumerable<T> QueryToObjects(string query, params SqlParameter[] sqlParameters)
        {
            List<T> r = new List<T>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    if (sqlParameters != null)
                    {
                        foreach (var p in sqlParameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        IList<PropertyInfo> props = new List<PropertyInfo>(typeof(T).GetProperties());
                        var result = Activator.CreateInstance<T>();

                        foreach (PropertyInfo prop in props)
                        {
                            SetValue(prop, reader, result);
                        }
                        
                        r.Add(result);
                    }

                    cmd.Parameters.Clear();
                }
            }
            return r.AsEnumerable();
        }

        public void ExecuteQuery(string query, ICollection<SqlParameter> parameters)
        {
            ExecuteQuery(query, parameters.ToArray());
        }

        public bool ExecuteQuery(string query, params SqlParameter[] sqlParameters)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    if (sqlParameters != null)
                    {
                        foreach (var p in sqlParameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        StringBuilder error = new StringBuilder("An error occurred.<br/><br/>" + ex.StackTrace + "<br/><br/> Query:<br/>" + query + "<br/><br/>Params:<br/>");
                        foreach(var param in sqlParameters)
                        {
                            error.Append(param.ParameterName + " : " + param.Value + "<br/>");
                        }
                        // todo: log exception
                        EmailHelper.SendAdminEmail("Intranet Query Error", error.ToString());
                        return false;
                    }
                }
            }
        }

        private void SetValue(PropertyInfo prop, SqlDataReader reader, T instance)
        {
            SqlAttribute sqlAttribute = (SqlAttribute)Attribute.GetCustomAttribute(prop, typeof(SqlAttribute));

            if (sqlAttribute == null)
            {
                if (reader.HasColumn(prop.Name))
                {
                    var value = reader[prop.Name] == DBNull.Value ? default(T) : reader[prop.Name];
                    
                    Type propertyType = prop.PropertyType;
                    
                    var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                    
                    var propertyVal = value == null ? null : Convert.ChangeType(value, targetType);
                    
                    prop.SetValue(instance, propertyVal, null);
                }
            }
            else
            {
                if (reader.HasColumn(sqlAttribute.ColumnName))
                {
                    var value = reader[sqlAttribute.ColumnName] == DBNull.Value ? default(T) : reader[sqlAttribute.ColumnName];
                    prop.SetValue(instance, value, null);
                }
            }
        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }
}
