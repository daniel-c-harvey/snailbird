using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace SnailbirdData.DataAdapters
{
    public interface IDataAdapter<TModel>
    {
        ResultContainer<IEnumerable<TModel>> GetPage(int page, int pageSize);
        ResultContainer<TModel> GetByID(int id);
        Result Insert(TModel model);
        Result Insert(IEnumerable<TModel> models);
        Result Update(TModel model);
        Result Delete(int id);
    }
}
