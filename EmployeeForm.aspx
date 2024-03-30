<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="Maven.EmployeeForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Form</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Insert Form -->
            <h2>Insert New Employee</h2>
            <div>
                <label for="txtEmpCode">Employee Code:</label>
                <asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtEname">Employee Name:</label>
                <asp:TextBox ID="txtEname" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="ddlDept">Department:</label>
                <asp:DropDownList ID="ddlDept" runat="server"></asp:DropDownList>
            </div>
            <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
            <hr />

            <!-- GridView to Display Employee Information -->
            <h2>Employee Information</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblEname" runat="server" Text='<%# Bind("Ename") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEname" runat="server" Text='<%# Bind("Ename") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DeptName" HeaderText="Department" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
