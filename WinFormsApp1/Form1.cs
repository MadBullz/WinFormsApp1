using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class frmEmployee : Form
    {
        EmployeeDAO edb = new EmployeeDAO();

        public frmEmployee()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            dgvEmployee.DataSource = null;
            dgvEmployee.DataSource = edb.getEmployee();
            dgvEmployee.Columns[0].HeaderText = "ID";
            dgvEmployee.Columns[0].ReadOnly = true;
            dgvEmployee.Columns[1].HeaderText = "Name";
            dgvEmployee.Columns[1].ReadOnly = true;
            dgvEmployee.Columns[2].HeaderText = "Gender";
            dgvEmployee.Columns[2].ReadOnly = true;
            dgvEmployee.Columns[3].HeaderText = "Salary";
            dgvEmployee.Columns[3].ReadOnly = true;
            dgvEmployee.Columns[4].HeaderText = "Phone";
            dgvEmployee.Columns[4].ReadOnly = true;
            txtName.Clear();
            nudSalary.Value = 1000;
            mtxtPhone.Clear();
            rbFemale.Checked = false;
            rbMale.Checked = true;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            bool gen = rbMale.Checked;
            int sal = (int) nudSalary.Value;
            string phoneNum = mtxtPhone.Text;
            Employee emp = new Employee()
            {
                empName = name,
                gender = gen,
                salary = sal,
                phone = phoneNum
            };
            edb.InsertEmployee(emp);
            Reload();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Int32.Parse(txtID.Text);
            string name = txtName.Text;
            bool gender = rbMale.Checked;
            int salary = (int) nudSalary.Value;
            string phone = mtxtPhone.Text;
            Employee emp = new Employee()
            {
                empID = ID,
                empName = name,
                gender = gender,
                salary = salary,
                phone = phone
            };
            edb.UpdateEmployee(emp);
            Reload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = Int32.Parse(txtID.Text);
            edb.DeleteEmployee(ID);
            Reload();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void dgvEmployee_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtID.Text = this.dgvEmployee.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = this.dgvEmployee.CurrentRow.Cells[1].Value.ToString();
            String gender = this.dgvEmployee.CurrentRow.Cells[2].Value.ToString();
            if (gender == "Male") rbMale.Checked = true;
            else rbFemale.Checked = true;
            nudSalary.Value = (int) this.dgvEmployee.CurrentRow.Cells[3].Value;
            mtxtPhone.Text = this.dgvEmployee.CurrentRow.Cells[4].Value.ToString();
        }

    }
}
