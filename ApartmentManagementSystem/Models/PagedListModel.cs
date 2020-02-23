using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ApartmentManagementSystem.Models
{
    public class PagedListModel
    {
        public List<string> lstblock = new List<string>();
        public List<FamilyModel> lstmodel = new List<FamilyModel>();
        public string Block { get; set; }
        public int? Flat_no { get; set; }
        public IPagedList<VisitorModel> pagedlst { get; set; }
       
    }
}