using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace ProcesadorTicket.Core.DataBase
{
    class DB
    {

        private OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["cnDefault"].ToString());


        public OleDbConnection getCon()
        {
            try
            {
                return con;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public Boolean fNull(string xstring)
        {
            try
            {
                Boolean res = true;
                if (xstring != string.Empty)
                {
                    res = true;
                }
                if (xstring.Trim().Length == 0 || xstring == "__/__/____" || xstring == "__-___.___" || xstring == "__:__")
                {
                    res = true;
                }
                else { res = false; }

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        

        public DataTable ejecutarConsultaDT(string query, int tiempo = 0)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                if (!fNull(query))
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = tiempo;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (con.State == ConnectionState.Open) con.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void ejecutarConsulta(string query, int tiempo = 0)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                if (!fNull(query))
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = tiempo;
                    cmd.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}
