using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class Prosjektvalg : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        string description;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetProject();
            }     
            
        }
        private void GetProject()
        {
            string query = "SELECT * FROM Project";
            Dropdown_prosjekt.DataSource = db.getAll(query);
            Dropdown_prosjekt.DataValueField = "projectDescription";
            Dropdown_prosjekt.DataTextField = "projectName";
            Dropdown_prosjekt.Items.Insert(0, new ListItem("<Velg prosjekt>", "0"));
            Dropdown_prosjekt.DataBind();
        }

        protected void Dropdown_prosjekt_SelectedIndexChanged(object sender, EventArgs e)
        {
           Tb_description.Text = Dropdown_prosjekt.SelectedValue;
        }
    }
}