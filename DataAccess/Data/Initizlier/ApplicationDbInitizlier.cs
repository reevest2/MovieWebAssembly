using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

    public bool DoesDatabaseExist()
    {
        return _dbContext.Database.CanConnect();
    }

    public void Initialize()
    {
        CreateDb();
        CreateTables();
    }

    private void CreateDb()
    {
        if (!DoesDatabaseExist())
        {
            var assembly = Assembly.GetExecutingAssembly();
            string sqlFilePath = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateDBandIdentity.sql";
            string sqlScript = File.ReadAllText(sqlFilePath);

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(sqlScript, (SqlConnection)connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }

    private void CreateTables()
    {
        if (DoesDatabaseExist())
        {
            var assembly = Assembly.GetExecutingAssembly();
            string sqlFilePath = @"C:\Repos\MovieWebAssembly\DataAccess\SQL\CreateCoreSchemaAndTables.sql";
            string sqlScript = File.ReadAllText(sqlFilePath);
            string[] scriptBatches = Regex.Split(sqlScript, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
           
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                foreach (string batch in scriptBatches)
                {
                    if (!string.IsNullOrWhiteSpace(batch))
                    {
                        using (var command = new SqlCommand(batch, (SqlConnection)connection))
                        {
                            // Execute the batch.
                            command.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}