using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
   public class AssetBAL
    {
        public IQueryable<Asset> GetAll(int schoolId)
        {
            AssetDAL dalObject = new AssetDAL();
            IQueryable<Asset> results = dalObject.GetAll(schoolId);
            return results;
        }
        public IQueryable<Asset> FindBy(System.Linq.Expressions.Expression<Func<Asset, bool>> predicate)
        {
            AssetDAL dalObject = new AssetDAL();
            IQueryable<Asset> results = dalObject.FindBy(predicate);
            return results;
        }

        public void Add(Asset entity, int schoolId)
        {
            AssetDAL dalObject = new AssetDAL();
            dalObject.Add(entity, schoolId);
        }

        public void Add(Asset entity)
        {
            AssetDAL dalObject = new AssetDAL();
            dalObject.Add(entity);
        }

        public void Edit(Asset entity)
        {
            AssetDAL dalObject = new AssetDAL();
            dalObject.Edit(entity);
        }

        public void Delete(int id)
        {
            AssetDAL dalObject = new AssetDAL();
            dalObject.Delete(id);
        }
    }
}
