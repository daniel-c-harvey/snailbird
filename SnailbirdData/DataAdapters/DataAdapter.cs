using Core;
using DataAccess;
using SnailbirdData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.DataAdapters
{
    public abstract class DataAdapter<TDatabase, TDataAccess, TQueryBuilder, TModel> : IDataAdapter<TModel>
        where TDatabase : class
        where TDataAccess : IDataAccess<TDatabase>
        where TQueryBuilder : IQueryBuilder<TDatabase>
        where TModel : class
    {
        protected TDataAccess DataAccess;
        protected TQueryBuilder QueryBuilder;
        protected DataSchema Schema;

        public DataAdapter(TDataAccess dataAccess, TQueryBuilder queryBuilder, DataSchema schema)
        {
            DataAccess = dataAccess;
            QueryBuilder = queryBuilder;
            Schema = schema;
        }

        ResultContainer<TModel> IDataAdapter<TModel>.GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public ResultContainer<IEnumerable<TModel>> GetPage(int pageIndex, int pageSize)
        {
            var modelResults = new ResultContainer<IEnumerable<TModel>>();
            try
            {
                IEnumerable<TModel> models = DataAccess.ExecQuery(QueryBuilder.BuildRetrieve<TModel>(Schema.Collection, pageIndex, pageSize));
                modelResults.Value = models;
                return modelResults;
            }
            catch (Exception ex) 
            {
                return modelResults.Fail(ex.Message);
            }
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Result Insert(TModel model)
        {
            try
            {
                DataAccess.ExecNonQuery(QueryBuilder.BuildInsert(Schema.Collection, model));
            }
            catch (Exception e) { return Result.CreateFailResult($"Database error: {e.Message}"); }
            return Result.CreatePassResult();
        }

        public Result Insert(IEnumerable<TModel> models)
        {
            throw new NotImplementedException();
        }

        
        public Result Update(TModel model)
        {
            throw new NotImplementedException();
        }

        
    }
}
