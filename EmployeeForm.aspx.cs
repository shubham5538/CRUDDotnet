using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Maven
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        // Connection string to your database
        string connectionString = @"Data Source=LAPTOP-QNOKS7JJ\SQLEXPRESS01;Initial Catalog=Task;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                PopulateDepartments();
            }
        }

        protected void BindGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT e.EmpId, e.EmpCode, e.Ename, d.DeptName " +
                               "FROM Employee e INNER JOIN Department d ON e.DeptId = d.DeptId";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                GridView1.DataSource = reader;
                GridView1.DataBind();

                reader.Close();
            }
        }

        protected void PopulateDepartments()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DeptId, DeptName FROM Department";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ddlDept.DataSource = reader;
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptId";
                ddlDept.DataBind();

                ddlDept.Items.Insert(0, new ListItem("--Select Department--", "0"));

                reader.Close();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string empCode = txtEmpCode.Text;
            string empName = txtEname.Text;
            int deptId = Convert.ToInt32(ddlDept.SelectedValue);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee (EmpCode, Ename, DeptId) VALUES (@EmpCode, @Ename, @DeptId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpCode", empCode);
                command.Parameters.AddWithValue("@Ename", empName);
                command.Parameters.AddWithValue("@DeptId", deptId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindGridView();
            ClearInsertFields();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int empId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["EmpId"]);
            string newName = ((TextBox)row.FindControl("txtEditEname")).Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET Ename = @NewName WHERE EmpId = @EmpId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewName", newName);
                command.Parameters.AddWithValue("@EmpId", empId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int empId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["EmpId"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employee WHERE EmpId = @EmpId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", empId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindGridView();
        }

        protected void ClearInsertFields()
        {
            txtEmpCode.Text = "";
            txtEname.Text = "";
            ddlDept.SelectedIndex = 0;
        }
    }
}
