using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\MuratYucedag\Library.mdb");

        void list()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Books", connection);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'libraryDataSet1.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter.Fill(this.libraryDataSet1.Books);
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\MuratYucedag\Library.mdb

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            list();
        }
        string status = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("insert into Books (BookName,Author,Type,Page,Status) values (@p1,@p2,@p3,@p4,@p5)", connection);
            command.Parameters.AddWithValue("@p1", txtBookName.Text);
            command.Parameters.AddWithValue("@p2", txtAuthor.Text);
            command.Parameters.AddWithValue("@p3", comboBoxType.Text);
            command.Parameters.AddWithValue("@p4", txtPage.Text);
            command.Parameters.AddWithValue("@p5", status);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Book Added", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }

        private void checkBoxUsed_CheckedChanged(object sender, EventArgs e)
        {
            status = "0";
        }

        private void checkBoxUnused_CheckedChanged(object sender, EventArgs e)
        {
            status = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;

            txtBookid.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            txtBookName.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            txtAuthor.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            comboBoxType.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            txtPage.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
            if (dataGridView1.Rows[selected].Cells[5].Value.ToString() == "True")
            {
                checkBoxUsed.Checked = true;
                checkBoxUnused.Checked = false;
            }
            else
            {
                checkBoxUsed.Checked = false;
                checkBoxUnused.Checked = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("Delete From Books Where Bookid=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtBookid.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Book Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("Update Books Set BookName=@p1,Author=@p2,Type=@p3,Page=@p4,Status=@p5 Where Bookid=@p6", connection);
            command.Parameters.AddWithValue("@p1", txtBookName.Text);
            command.Parameters.AddWithValue("@p2", txtAuthor.Text);
            command.Parameters.AddWithValue("@p3", comboBoxType.Text);
            command.Parameters.AddWithValue("@p4", txtPage.Text);
            if(checkBoxUsed.Checked == true)
            {
                command.Parameters.AddWithValue("@p5", status);
            }
            else
            {
                command.Parameters.AddWithValue("@p5", status);
            }
            command.Parameters.AddWithValue("@p6", txtBookid.Text);
            command.ExecuteNonQuery();  
            connection.Close();
            MessageBox.Show("Book Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("Select * From Books Where BookName=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtFind.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("Select * From Books Where BookName like '%"+ txtFind.Text + "%'", connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            list();
        }
    }
}
