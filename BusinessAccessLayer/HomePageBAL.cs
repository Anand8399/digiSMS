using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class HomePageBAL
    {

        public IQueryable<HomePage> getChartData(int schoolId)
        {
            HomePageDAL dalObject = new HomePageDAL();
            IQueryable<HomePage> results = dalObject.getChartData(schoolId);
            return results;
        }

        public IQueryable<HomePage> getClasswiseMaleFemaleList(int schoolId)
        {
            HomePageDAL dalObject = new HomePageDAL();
            IQueryable<HomePage> results = dalObject.getClasswiseMaleFemaleList(schoolId);
            return results;
        }

    }
}
