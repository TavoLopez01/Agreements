using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Agreements.Models;
using Microsoft.Extensions.Configuration;

namespace Agreements.Data
{
    public class AgreementsData
    {

        private readonly string _connection;

        public AgreementsData(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<AgreementsModel>> GetAll()
        {

            using (SqlConnection sql = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllAgreements", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<AgreementsModel>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AgreementsModel agreement = GetRow(reader);
                            Console.WriteLine(agreement);
                            response.Add(agreement);
                        }
                    }

                    return response;
                }
            }
        }

        private AgreementsModel GetRow(SqlDataReader dataReader)
        {
            return new AgreementsModel()
            {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
                Description = dataReader.GetString(2),
                Amount = dataReader.GetInt32(3)
            };
        }
    }
}
