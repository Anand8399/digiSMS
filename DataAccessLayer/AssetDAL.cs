using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AssetDAL : IGenericRepository<Asset>
    {
        public IQueryable<Asset> GetAll(int schoolId)
        {
            try
            {
                List<Asset> entites = new List<Asset>();
                String SqlQuery = "select * from viewGetAllAssetDetails";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId + ";";

                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            Asset entity = new Asset();
                            entity.SrNo = Convert.ToInt32(dr["SrNo"]);
                            entity.TypeofAsset = Convert.ToString(dr["TypeofAsset"]);
                            entity.AssetName = Convert.ToString(dr["AssetName"]);
                            entity.Quantity = Convert.ToInt32(dr["Quantity"]);
                            entity.PricePerItem = Convert.ToDecimal(dr["PricePerItem"]);
                            entity.Total = Convert.ToDecimal(dr["Total"]);
                            entity.PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]);
                            entity.Condition = Convert.ToString(dr["Condition"]);
                            entity.AssesmentYear = Convert.ToDateTime(dr["AssesmentYear"]);
                            entity.Status = Convert.ToBoolean(dr["Status"].ToString());
                            entity.Remark = dr["Remark"].ToString();
                            entites.Add(entity);
                        }
                    }
                }

                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Asset> GetAll()
        {
            return GetAll(0);
        }
        public IQueryable<Asset> FindBy(System.Linq.Expressions.Expression<Func<Asset, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Asset entity, int schoolId)
        {
            try
            {
                Hashtable parameterlist = new Hashtable();

                parameterlist.Add("@SchoolId", schoolId);
                parameterlist.Add("@TypeofAsset", entity.TypeofAsset);
                parameterlist.Add("@AssetName", entity.AssetName);
                parameterlist.Add("@Quantity", entity.Quantity);
                parameterlist.Add("@PricePerItem", entity.PricePerItem);
                parameterlist.Add("@Total", entity.Quantity*entity.PricePerItem);// entity.Total);
                parameterlist.Add("@PurchaseDate", entity.PurchaseDate);
                parameterlist.Add("@Condition", entity.Condition);
                parameterlist.Add("@AssesmentYear", entity.AssesmentYear);
                parameterlist.Add("@Status", entity.Status == true ? 1 : 0);
                parameterlist.Add("@Remark", entity.Remark);
              
                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_InsertAssetDetails", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Asset entity)
        {
            Add(entity, 0);
        }
        public void Delete(int Id)
        {
            try
            {
                string queryString = string.Format(" DELETE FROM [AssetDetails] WHERE AssetId  = {0};", Id);
                int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Edit(Asset entity)
        {
            try
            {

        //        string queryString = string.Format(" UPDATE [AssetDetails] " +
        //" SET [SrNo] = {0}" +
        //",[Asset] = '{1}'" +
        //  ",[TypeofAsset] = '{2}'" +
        //  ",[Measure] = '{3}'" +
        //  ",[Credit] = '{4}'" +
        //  ",[Debit]='{5}'"+
        //  ",[Total]='{6}'" +
        //  ",[Status]='{7}'" +
        //  ",[Remark]='{8}'" +

        //" WHERE SrNo  = {9};", entity.SrNo, entity.AssetName, entity.TypeofAsset, entity.Measure, entity.Credit, entity.Debit, entity.Total, entity.Status, entity.Remark, entity.SrNo);
        //        int effetedRows = CommanMethodsForSQL.ExecuteNonQuery(queryString);
                Hashtable parameterlist = new Hashtable();
                parameterlist.Add("@SrNo", entity.SrNo);
                parameterlist.Add("@TypeofAsset", entity.TypeofAsset);
                parameterlist.Add("@AssetName", entity.AssetName);
                parameterlist.Add("@Quantity", entity.Quantity);
                parameterlist.Add("@PricePerItem", entity.PricePerItem);
                parameterlist.Add("@Total", entity.Quantity * entity.PricePerItem );//entity.Total);
                parameterlist.Add("@PurchaseDate", entity.PurchaseDate);
                parameterlist.Add("@Condition", entity.Condition);
                parameterlist.Add("@AssesmentYear", entity.AssesmentYear);
                parameterlist.Add("@Status", entity.Status);
                parameterlist.Add("@Remark", entity.Remark);

                int effetedRows = CommanMethodsForSQL.ExecuteNonQueryProcedure("sp_AssetDetailsUpdate", parameterlist);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
