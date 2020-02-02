using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;


namespace DataAccessLayer
{
    public class HomePageDAL
    {

        public IQueryable<HomePage> getClasswiseMaleFemaleList(int schoolId)
        {
            HomePage entity = new HomePage();

            entity.classList = new List<String>();
            entity.classwiseBoysList = new List<int>();
            entity.classwiseGirlsList = new List<int>();

            List<HomePage> entites = new List<HomePage>();  

            try
            {
                String SqlQuery = "select class, countMale, CountFemale from view_ClassWiseMaleFemale";
                if (schoolId != 0)
                    SqlQuery = SqlQuery + " where SchoolId=" + schoolId ;
                
                SqlQuery = SqlQuery + " order by LEN(Class), Class;";

                DataTable dataTable = CommanMethodsForSQL.GetDataTable(SqlQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            entity.classList.Add(Convert.ToString((dr["Class"])));
                            entity.classwiseBoysList.Add(Convert.ToInt32((dr["CountMale"])));
                            entity.classwiseGirlsList.Add(Convert.ToInt32((dr["CountFemale"])));
                            entites.Add(entity);
                        }
                    }
                }
                else
                {
                    entity.classList.Add("");
                    entity.classwiseBoysList.Add(0);
                    entity.classwiseGirlsList.Add(0);
                    entites.Add(entity);
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }

            return entites.AsQueryable();
        }
        public IQueryable<HomePage> getChartData(int schoolId)
        {
            try
            {
                HomePage entity = new HomePage();
                // Default values
                entity.CastGeneralCount = 0;
                entity.CastNT1Count = 0;
                entity.CastNT2Count = 0;
                entity.CastNT3Count = 0;
                entity.CastNT4Count = 0;
                entity.CastOBCCount = 0;
                entity.CastSBCCount = 0;                
                entity.CastSCCount = 0;
                entity.CastSTCount = 0;
                entity.CastVJCount = 0;
                entity.CastVJ1Count = 0;

                List<HomePage> entites = new List<HomePage>();

                String strQuery = "select ReserveCategory, Count from viewCasteChart";
                if (schoolId != 0)
                    strQuery = strQuery + " where SchoolId=" + schoolId + " ;";


                DataTable dataTable = CommanMethodsForSQL.GetDataTable(strQuery);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr != null)
                        {
                            //HomePage entity = new HomePage();

                            switch((dr["ReserveCategory"]).ToString())
                            {
                                case ("General"):
                                case ("सामान्य"):
                                    entity.CastGeneralCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("NT-1"):
                                case ("एनटी-1"):
                                    entity.CastNT1Count = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("NT-2"):
                                case ("एनटी-2"):
                                    entity.CastNT2Count = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("NT-3"):
                                case ("एनटी-3"):
                                    entity.CastNT3Count = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("NT-4"):
                                case ("एनटी-4"):
                                    entity.CastNT4Count = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("OBC"):
                                case ("ओबीसी"):
                                    entity.CastOBCCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("SBC"):
                                case ("एसबीसी"):
                                    entity.CastSBCCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("SC"):
                                case ("अनुसूचित जाती"):
                                    entity.CastSCCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("ST"):
                                case ("एसटी"):
                                    entity.CastSTCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("VJ"):
                                case ("व्हीजे"):
                                    entity.CastVJCount = Convert.ToInt32((dr["Count"]));
                                    break;
                                case ("VJ-1"):
                                case ("व्हीजे-1"):
                                    entity.CastVJ1Count = Convert.ToInt32((dr["Count"]));
                                    break;


                                default:
                                    break;
                            }


                           // entity.SrNo = Convert.ToInt32(iCnt++);
                            //entity.StudentName = Convert.ToString((dr["StudentName"]));

                            entites.Add(entity);
                        }
                    }
                }

                entites.Add(entity);
                
                return entites.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
