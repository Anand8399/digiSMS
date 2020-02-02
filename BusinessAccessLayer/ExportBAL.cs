using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
 public   class ExportBAL
    {
     public IQueryable<Export> getExportRecords( int schoolId)
     {
         ExportDAL dalObject = new ExportDAL();
         IQueryable<Export> results = dalObject.getExportRecords(schoolId);
         return results;
     }
    }
}
