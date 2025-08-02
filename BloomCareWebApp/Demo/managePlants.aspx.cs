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
using Ohoud10108WebApp.App_Code; // Assuming CRUD.cs is here


namespace Ohoud10108WebApp.Demo
{
    public partial class managePlants : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPlantTypes();
                loadPlantsGrid();
            }
        }

        private void loadPlantTypes()
        {
            CRUD crud = new CRUD();
            string sql = "SELECT * FROM plantTypes";
            DataTable dt = crud.getDT(sql);
            ddlPlantType.DataSource = dt;
            ddlPlantType.DataTextField = "typeName";
            ddlPlantType.DataValueField = "typeId";
            ddlPlantType.DataBind();
            ddlPlantType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Type", ""));
        }

        private void loadPlantsGrid()
        {
            CRUD crud = new CRUD();
            string sql = @"SELECT p.plantId, p.plantName, t.typeName, p.typeId, 
                      p.sunlightNeed, p.waterFreq, p.isIndoor, p.notes, p.imageUrl
               FROM plants p 
               INNER JOIN plantTypes t ON p.typeId = t.typeId";

            DataTable dt = crud.getDT(sql);
            gvPlants.DataSource = dt;
            gvPlants.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CRUD crud = new CRUD();
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"@plantName", txtPlantName.Text.Trim()},
                {"@typeId", ddlPlantType.SelectedValue},
                {"@sunlightNeed", rblSunlight.SelectedValue},
                {"@waterFreq", rblWaterFreq.SelectedValue},
                {"@isIndoor", rblIsIndoor.SelectedValue},
                {"@notes", txtNotes.Text.Trim()},
                {"@imageUrl", txtImageUrl.Text.Trim()},
            };

            string sql = "INSERT INTO plants (plantName, typeId, sunlightNeed, waterFreq, isIndoor, notes, imageUrl) VALUES (@plantName, @typeId, @sunlightNeed, @waterFreq, @isIndoor, @notes,@imageUrl)";
           int rtn = crud.InsertUpdateDelete(sql, data);
            if (rtn>=1)
            { lblMessage.Text = "Congrats New Plant Added ✨🌱"; }
            else
            { lblMessage.Text = "Something Wrong Happend Plant Adding failed"; }
            clearForm();
            loadPlantsGrid();
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            LoadPlantData();
            pnlGrid.Visible = true;
            lblMsg.Text = gvPlants.Rows.Count + " rows loaded.";
        }

        private void LoadPlantData()
        {
            CRUD myCrud = new CRUD();
            string sql = @"SELECT p.plantId, p.plantName, p.typeId, t.typeName, 
                      p.sunlightNeed, p.waterFreq, p.isIndoor, p.notes, p.imageUrl
               FROM plants p 
               INNER JOIN plantTypes t ON p.typeId = t.typeId";


            DataTable dt = myCrud.getDT(sql);
            gvPlants.DataSource = dt;
            gvPlants.DataBind();
        }

        protected void gvPlants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPlant")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPlants.Rows[index];

                hiddenPlantId.Value = gvPlants.DataKeys[index].Values["plantId"].ToString();
                txtPlantName.Text = row.Cells[0].Text;
                ddlPlantType.SelectedValue = gvPlants.DataKeys[index].Values["typeId"].ToString();
                rblSunlight.SelectedValue = row.Cells[2].Text;
                rblWaterFreq.SelectedValue = row.Cells[3].Text;
                rblIsIndoor.SelectedValue = row.Cells[4].Text;
                txtNotes.Text = row.Cells[5].Text;
                txtImageUrl.Text = row.Cells[6].Text;

                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }

            else if (e.CommandName == "DeletePlant")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string plantId = gvPlants.DataKeys[index].Values["plantId"].ToString();

                string sql = "DELETE FROM plants WHERE plantId=@plantId";
                Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "@plantId", plantId }
        };

                CRUD crud = new CRUD();
                crud.InsertUpdateDelete(sql, data);

                loadPlantsGrid();

                lblMessage.Text = "Plant deleted successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }



        /* protected void gvPlants_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             if (e.CommandName == "EditPlant")
             {
                 int index = Convert.ToInt32(e.CommandArgument);
                 GridViewRow row = gvPlants.Rows[index];

                 // تعبئة الحقول بالبيانات من الصف المختار
                 hiddenPlantId.Value = gvPlants.DataKeys[index].Value.ToString();
                 txtPlantName.Text = row.Cells[0].Text;
                 ddlPlantType.SelectedItem.Text = row.Cells[1].Text;
                 rblSunlight.SelectedValue = row.Cells[2].Text;
                 rblWaterFreq.SelectedValue = row.Cells[3].Text;
                 rblIsIndoor.SelectedValue = row.Cells[4].Text;

                 // التبديل بين الأزرار
                 btnAdd.Visible = false;
                 btnUpdate.Visible = true;
             }
             else if (e.CommandName == "DeletePlant")
             {
                 int index = Convert.ToInt32(e.CommandArgument);
                 string plantId = gvPlants.DataKeys[index].Value.ToString();

                 string sql = "DELETE FROM plants WHERE plantId=@plantId";
                 Dictionary<string, object> data = new Dictionary<string, object>
         {
             {"@plantId", plantId}
         };
                 CRUD crud = new CRUD();
                 crud.InsertUpdateDelete(sql, data);
                 loadPlantsGrid();
                 lblMessage.Text = "Plant deleted successfully!";
                 lblMessage.ForeColor = System.Drawing.Color.Green;
             }
         }*/


        protected void gvPlants_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvPlants.EditIndex = e.NewEditIndex;
            loadPlantsGrid();
        }

        protected void gvPlants_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvPlants.EditIndex = -1;
            loadPlantsGrid();
        }

        protected void gvPlants_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvPlants.Rows[e.RowIndex];
            string plantId = gvPlants.DataKeys[e.RowIndex].Value.ToString();

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"@plantId", plantId},
                {"@plantName", ((System.Web.UI.WebControls.TextBox)row.FindControl("txtEditPlantName")).Text},
                {"@typeId", ((System.Web.UI.WebControls.DropDownList)row.FindControl("ddlEditPlantType")).SelectedValue},
                {"@sunlightNeed", ((System.Web.UI.WebControls.RadioButtonList)row.FindControl("rblEditSunlightNeed")).SelectedValue},
                {"@waterFreq", ((System.Web.UI.WebControls.RadioButtonList)row.FindControl("rblEditWaterFreq")).SelectedValue},
                {"@isIndoor", ((System.Web.UI.WebControls.RadioButtonList)row.FindControl("rblEditIsIndoor")).SelectedValue},
                {"@notes", ((System.Web.UI.WebControls.TextBox)row.FindControl("txtEditnotes")).Text},
                {"@imageUrl", ((System.Web.UI.WebControls.TextBox)row.FindControl("txtEditimageUrl")).Text},
            };
            CRUD crud = new CRUD();
            string sql = "UPDATE plants SET plantName=@plantName, typeId=@typeId, sunlightNeed=@sunlightNeed, waterFreq=@waterFreq, isIndoor=@isIndoor, notes=@notes, imageUrl=@imageUrl  WHERE plantId=@plantId";
            crud.InsertUpdateDelete(sql, data);
            gvPlants.EditIndex = -1;
            loadPlantsGrid();
        }

        protected void gvPlants_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            CRUD crud = new CRUD();
            string plantId = gvPlants.DataKeys[e.RowIndex].Value.ToString();
            string sql = "DELETE FROM plants WHERE plantId=@plantId";
            Dictionary<string, object> data = new Dictionary<string, object> { { "@plantId", plantId } };
            crud.InsertUpdateDelete(sql, data);
            loadPlantsGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "@plantId", hiddenPlantId.Value },
                { "@plantName", txtPlantName.Text.Trim() },
                { "@typeId", ddlPlantType.SelectedValue },
                { "@sunlightNeed", rblSunlight.SelectedValue },
                { "@waterFreq", rblWaterFreq.SelectedValue },
                { "@isIndoor", rblIsIndoor.SelectedValue },
                { "@notes", txtNotes.Text.Trim() },
                { "@imageUrl", txtImageUrl.Text.Trim() }
            };

            string sql = "UPDATE plants SET plantName=@plantName, typeId=@typeId, sunlightNeed=@sunlightNeed, " +
                         "waterFreq=@waterFreq, isIndoor=@isIndoor, notes=@notes, imageUrl=@imageUrl " +
                         "WHERE plantId=@plantId";

            CRUD crud = new CRUD();
            crud.InsertUpdateDelete(sql, data);

            clearForm();
            loadPlantsGrid();

            lblMessage.Text = "Plant updated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }


        /* protected void btnUpdate_Click(object sender, EventArgs e)
         {
             Dictionary<string, object> data = new Dictionary<string, object>
     {
         {"@plantId", hiddenPlantId.Value},
         {"@plantName", txtPlantName.Text.Trim()},
         {"@typeId", ddlPlantType.SelectedValue},
         {"@sunlightNeed", rblSunlight.SelectedValue},
         {"@waterFreq", rblWaterFreq.SelectedValue},
         {"@isIndoor", rblIsIndoor.SelectedValue},
         {"@notes", txtNotes.Text.Trim()},
         {"@imageUrl", txtImageUrl.Text.Trim()},
     };
             CRUD crud = new CRUD();
             string sql = "UPDATE plants SET plantName=@plantName, typeId=@typeId, sunlightNeed=@sunlightNeed, waterFreq=@waterFreq, isIndoor=@isIndoor isIndoor=@isIndoor, notes=@notes, imageUrl=@imageUrl WHERE plantId=@plantId";
             crud.InsertUpdateDelete(sql, data);
             clearForm();
             loadPlantsGrid();

             lblMessage.Text = "Plant updated successfully!";
             lblMessage.ForeColor = System.Drawing.Color.Green;

             btnAdd.Visible = true;
             btnUpdate.Visible = false;
         }*/


        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPlantName.Text = "";
            ddlPlantType.SelectedIndex = 0;
            rblSunlight.ClearSelection();
            rblWaterFreq.ClearSelection();
            rblIsIndoor.ClearSelection();
            txtNotes.Text = "";
            txtImageUrl.Text = "";
            hiddenPlantId.Value = "";

            btnAdd.Visible = true;
            btnUpdate.Visible = false;

            lblMessage.Text = "";
        }



        private void clearForm()
        {
            txtPlantName.Text = string.Empty;
            ddlPlantType.SelectedIndex = 0;
            rblSunlight.ClearSelection();
            rblWaterFreq.ClearSelection();
            rblIsIndoor.ClearSelection();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PlantsExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvPlants.AllowPaging = false;
            loadPlantsGrid();

            // Remove all controls inside GridView rows (convert to plain text)
            foreach (GridViewRow row in gvPlants.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Text = row.Cells[i].Text;
                    row.Cells[i].Controls.Clear(); // removes buttons or controls
                }
            }

            gvPlants.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that gvPlants is rendered explicitly for export
        }
    }


}
