using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
//using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Ohoud10108WebApp.App_Code;

namespace Ohoud10108WebApp.Demo
{
    public partial class explorePlants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPlants();
            }
        }

        private void loadPlants()
        {
            CRUD myCrud = new CRUD();
            string sql = @"SELECT p.plantId, p.plantName, pt.typeName, p.typeId, 
                   p.sunlightNeed, p.waterFreq, p.isIndoor, p.notes, p.imageUrl
                   FROM plants p 
                   INNER JOIN plantTypes pt ON p.typeId = pt.typeId";

            DataTable dt = myCrud.getDT(sql);
            rptPlants.DataSource = dt;
            rptPlants.DataBind();
        }

    }
}