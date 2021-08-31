using System;
using System.Data;
using System.Data.SqlClient;

public class CuentasManager {
    const string connectionString = "Data Source=.\\sql2016;Initial Catalog=StressTesting;Integrated Security=SSPI";
    public static void Transaccion (
        int cuentaId,
        decimal valor,
        int signo
    ) {

        //Console.WriteLine($"Cuenta: {cuentaId} Valor: {valor} Signo: {signo}");

        string SQL = "spCuentaTransaccion";
        // Create ADO.NET objects.
        SqlConnection con = new SqlConnection (connectionString);
        
        SqlCommand cmd = new SqlCommand (SQL, con);
        con.Open ();
        SqlTransaction trans = con.BeginTransaction(); 
        cmd.Transaction = trans;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter param;
        try {
            param = cmd.Parameters.Add ("@cuentaId", SqlDbType.Int);
            param.Value = cuentaId;
            param = cmd.Parameters.Add ("@valor", SqlDbType.Decimal);
            param.Value = valor;
            param = cmd.Parameters.Add ("@signo", SqlDbType.Int);
            param.Value = signo;
            // Execute the command.
            int rowsAffected = cmd.ExecuteNonQuery ();
            trans.Commit();

            // Display the result of the operation.
            //Console.WriteLine (rowsAffected.ToString () + " row(s) affected");

        }
        catch (System.Exception) {
            trans.Rollback();            
            throw;
        }
        finally {
            con.Close ();
        }
    }
}