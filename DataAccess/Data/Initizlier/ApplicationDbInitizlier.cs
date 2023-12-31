using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace DataAccess.Data.Initizlier;

public interface IDbInitializer
{
    public void Initialize();
}

public class ApplicationDbInitizlier : IDbInitializer
{
    private readonly ApplicationDbContext _dbContext;

    public ApplicationDbInitizlier(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {
        if (!DoesDatabaseExist())
        {
            CreateDbIfNotExist();
        }
        else
        {
            CreateTablesAndInsertData();
        }
    }


    public bool DoesDatabaseExist()
    {
        return _dbContext.Database.CanConnect();
    }

    private void CreateDbIfNotExist()
    {
        //File Paths
        string sqlFilePath = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateDBandIdentity.sql";
        string sqlFilePath2 = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateCoreSchemaAndTables.sql";
        string sqlFilePath3 = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\MergeIntoCoreSchemaTestData.sql";

        //Read Files
        string sqlScript1 = File.ReadAllText(sqlFilePath);
        string sqlScript2 = File.ReadAllText(sqlFilePath2);
        string sqlScript3 = File.ReadAllText(sqlFilePath3);

        //Turn into a list and add Scripts
        var sqlScriptList = new List<string>();
        sqlScriptList.Add(sqlScript1);
        sqlScriptList.Add(sqlScript2);
        sqlScriptList.Add(sqlScript3);

        //Turn into script batch
        var scriptBatches = new List<string>();
        foreach (var script in sqlScriptList)
        {
            var sqlCommand = Regex.Split(script, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var b in sqlCommand)
            {
                scriptBatches.Add(b);
            }
        }

        //Open Connection, run script batches, close connection
        using var connection =
            new SqlConnection(
                "Server=DESKTOP-H9FBDS7;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        connection.Open();
        foreach (string batch in scriptBatches)
        {
            using (var command = new SqlCommand(batch, connection))
            {
                if (!string.IsNullOrWhiteSpace(batch))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        connection.Close();
    }

    private void CreateTablesAndInsertData()
    {
        //File Paths
        //string sqlFilePath = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateDBandIdentity.sql";
        string sqlFilePath2 = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateCoreSchemaAndTables.sql";
        string sqlFilePath3 = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\MergeIntoCoreSchemaTestData.sql";

        //Read Files
        //string sqlScript1 = File.ReadAllText(sqlFilePath);
        string sqlScript2 = File.ReadAllText(sqlFilePath2);
        string sqlScript3 = File.ReadAllText(sqlFilePath3);

        //Turn into a list and add Scripts
        var sqlScriptList = new List<string>();
        //sqlScriptList.Add(sqlScript1);
        sqlScriptList.Add(sqlScript2);
        sqlScriptList.Add(sqlScript3);

        //Turn into script batch
        var scriptBatches = new List<string>();
        foreach (var script in sqlScriptList)
        {
            var sqlCommand = Regex.Split(script, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var b in sqlCommand)
            {
                scriptBatches.Add(b);
            }
        }

        //Open Connection, run script batches, close connection
        using var connection =
            new SqlConnection(
                "Server=DESKTOP-H9FBDS7;Database=MovieWebAssemblyAppDatabase;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        connection.Open();
        foreach (string batch in scriptBatches)
        {
            using (var command = new SqlCommand(batch, connection))
            {
                if (!string.IsNullOrWhiteSpace(batch))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        connection.Close();
    }
}