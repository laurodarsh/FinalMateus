﻿using FinalMateus.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalMateus.Forms
{
    public partial class ProductDetailsForm : Form
    {
        string name = "";
        bool active = false;
        float price = 0;
        string category = "";
        List<Category> categories = new List<Category>();
        string connectionString = "workstation id=StockControl.mssql.somee.com;packet size=4096;user id=levelupacademy_SQLLogin_1;pwd=3wwate8gu1;data source=StockControl.mssql.somee.com;persist security info=False;initial catalog=StockControl";
        public ProductDetailsForm()
        {
            InitializeComponent();
            cmbCategory.DisplayMember = "NAME";
            LoadComboBox();
 
        }

        void GetData()
        {
            name = tbxName.Text;
            price = float.Parse(tbxPrice.Text);
            category = cmbCategory.Text;
            active = cbxActive.Checked ? true : false;
        }
        void ClearData()
        {
            tbxName.Text = "";
            tbxPrice.Text = "";
            cmbCategory.Text = "";
            cbxActive.Checked = false;
        }


        private void pbxBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbxSave_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                GetData();
                Category c = (Category)cmbCategory.SelectedItem;
                Product p = new Product(name, price, c, active);
                sqlConnect.Open();
                string sql = "INSERT INTO PRODUCT(NAME, PRICE, ACTIVE, FK_PRODUCT) VALUES (@name, @price, @active, @category)";

                SqlCommand cmd = new SqlCommand(sql, sqlConnect);

                cmd.Parameters.Add(new SqlParameter("@name", p.Name));
                cmd.Parameters.Add(new SqlParameter("@price", p.Price));
                cmd.Parameters.Add(new SqlParameter("@active", p.Active));
                cmd.Parameters.Add(new SqlParameter("@category", p.Category.Id));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Adicionado com sucesso!");
                ClearData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar categoria!" + ex.Message);
                ClearData();
            }
            finally
            {
                sqlConnect.Close();

            }
        }

        void LoadComboBox()
        {
            SqlConnection cn = new SqlConnection(connectionString);

            try
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM CATEGORY", cn);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Category c = new Category(Int32.Parse(reader["ID"].ToString()),reader["NAME"].ToString(), bool.Parse(reader["ACTIVE"].ToString()));
                    categories.Add(c);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }
            foreach (Category c in categories)
            {
                cmbCategory.Items.Add(c);
            }
        }
        private void pbxDelete_Click(object sender, EventArgs e)
        {

        }

        private void pbxBack_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbxBack_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbxSave_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbxSave_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbxDelete_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbxDelete_MouseLeave(object sender, EventArgs e)
        {
            
        }
    }
}