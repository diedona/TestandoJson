using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class QueryContext
    {
        private readonly string _ConnectionString;

        public QueryContext(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public async Task<IEnumerable<TransactionHistory>> GetAll()
        {
            using (var conexao = new SqlConnection(_ConnectionString))
            {
                return await conexao.QueryAsync<TransactionHistory>("select * from Production.TransactionHistory");
            }
        }
    }
}
