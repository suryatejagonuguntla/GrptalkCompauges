using CompaugesWebApi.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CompaugesWebApi.Models
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure , IEnumerable<SqlParameter> parameters = null)
        {
            return new MultipleResultSetWrapper(db, storedProcedure , parameters);
        }

        public class MultipleResultSetWrapper
        {
            private readonly DbContext _db;
            private readonly string _storedProcedure;
            private readonly IEnumerable<SqlParameter> _parameters;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure , IEnumerable<SqlParameter> parameters = null)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _parameters = parameters;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();
                
                try
                {
                    using (var connection = _db.Database.Connection)
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "EXEC " + _storedProcedure + " @SearchInput , @retVal OUTPUT";
                        command.CommandText =  _storedProcedure ;
                        
                        if (_parameters?.Any() ?? false) {
                            command.Parameters.AddRange(_parameters.ToArray());
                        }

                        using (var reader = command.ExecuteReader())
                        {
                           
                            var adapter = ((IObjectContextAdapter)_db);
                            foreach (var resultSet in _resultSets)
                            {
                                results.Add(resultSet(adapter, reader));
                                reader.NextResult();
                            }
                        }
                        var output = Convert.ToInt16(command.Parameters["@retval"].Value);

                    }
                }
                catch (Exception ex)
                {

                    string s = "";
                    Logger.ExceptionLog("while filling data Set " + ex.ToString() );
                }
                return results;
            }
        }
    }
}