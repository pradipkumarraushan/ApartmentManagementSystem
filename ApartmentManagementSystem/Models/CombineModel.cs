using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class CombineModel
    {
        public FamilyModel GetModel { get; set; } = new FamilyModel();
        public List<FamilyModel> lstmodel = new List<FamilyModel>();
        public IPagedList<FamilyModel> familypagedlst { get; set; }
        public string Search_bydate { get; set; }
    }
}