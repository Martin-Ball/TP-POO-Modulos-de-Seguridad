﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Controladora;
using Modelo.Enum;
using System.IO;

namespace Vista
{
    public partial class Products : Form
    {

        bool firstTime = false;

        public Products()
        {
            InitializeComponent();

            try
            {
               List<Modelo.DTO.ProductosDto> listProducts = Controladora.ControladoraProductos.GetProducts();
               dgvProducts.DataSource = null;
               dgvProducts.DataSource = listProducts;
                //agregar botones de + y - entre la columna de stock
                putColumnsAddAndSubstract();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }          
            
        }

        private void putColumnsAddAndSubstract()
        {
            DataGridViewButtonColumn addToStock = new DataGridViewButtonColumn();
            addToStock.HeaderText = "Add";
            addToStock.Text = "+";
            addToStock.Name = "btnAdd";
            addToStock.UseColumnTextForButtonValue = true;
            dgvProducts.Columns.Insert(3, addToStock);

            DataGridViewButtonColumn substractToStock = new DataGridViewButtonColumn();
            substractToStock.HeaderText = "Substract";
            substractToStock.Text = "-";
            substractToStock.Name = "btnSubstract";
            substractToStock.UseColumnTextForButtonValue = true;
            dgvProducts.Columns.Insert(5, substractToStock);

            dgvProducts.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);
            dgvProducts.Columns[5].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);

            DataGridViewImageColumn DeleteImg = new DataGridViewImageColumn();
            DataGridViewImageColumn EditImg = new DataGridViewImageColumn();

            string deleteFile = "delete.png";
            string editFile = "edit.png";
            string DirDebug = System.IO.Directory.GetCurrentDirectory();
            string DirProject = DirDebug;

            for (int counter_slash = 0; counter_slash < 3; counter_slash++)
            {
                DirProject = DirProject.Substring(0, DirProject.LastIndexOf(@"\"));
            }

            string pathDelete = Path.Combine(DirProject, @"Vista\Img", deleteFile);
            string patEdit = Path.Combine(DirProject, @"Vista\Img", editFile);

            Image imageDelete = Image.FromFile(pathDelete);
            Image imageEdit = Image.FromFile(patEdit);

            EditImg.Image = imageEdit;
            dgvProducts.Columns.Insert(6, EditImg);
            EditImg.HeaderText = "Edit";
            EditImg.Name = "imgDelete";

            DeleteImg.Image = imageDelete;
            dgvProducts.Columns.Insert(7, DeleteImg);
            DeleteImg.HeaderText = "Delete";
            DeleteImg.Name = "imgDelete";
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());

            MessageBox.Show(e.ColumnIndex.ToString() + " " + e.RowIndex.ToString());

            if (e.ColumnIndex == 0)
            {
                //AGREGAR UNIDAD
                var listProducts = Controladora.ControladoraProductos.GetProducts();
                Modelo.DTO.ProductosDto productModified = new Modelo.DTO.ProductosDto();

                try
                {
                    var currentProduct = listProducts.Find(x => x.ProductName.ToString() == this.dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString());
                    

                    productModified.ProductName = currentProduct.ProductName;
                    productModified.BrandName = currentProduct.BrandName;
                    productModified.DescriptionProduct = currentProduct.DescriptionProduct;
                    productModified.StockProduct = currentProduct.StockProduct + 1;

                    this.dgvProducts.Rows[e.RowIndex].Cells[7].Value = productModified.StockProduct;
                    Controladora.ControladoraProductos.EditarProducto(productModified);

                }
                catch
                {
                    MessageBox.Show("NO SE PUDO MODIFICAR EL PRODUCTO");
                }

            }
            else if(e.ColumnIndex == 1)
            {
                //RESTAR UNIDAD
                var listProducts = Controladora.ControladoraProductos.GetProducts();
                Modelo.DTO.ProductosDto productModified = new Modelo.DTO.ProductosDto();

                try
                {
                    var currentProduct = listProducts.Find(x => x.ProductName.ToString() == this.dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString());


                    productModified.ProductName = currentProduct.ProductName;
                    productModified.BrandName = currentProduct.BrandName;
                    productModified.DescriptionProduct = currentProduct.DescriptionProduct;
                    productModified.StockProduct = currentProduct.StockProduct - 1;

                    this.dgvProducts.Rows[e.RowIndex].Cells[7].Value = productModified.StockProduct;
                    Controladora.ControladoraProductos.EditarProducto(productModified);

                }
                catch
                {
                    MessageBox.Show("NO SE PUDO MODIFICAR EL PRODUCTO");
                }
            }
            else if(e.ColumnIndex == 2)
            {
                //MODIFICAR PRODUCTO

                var listProducts = Controladora.ControladoraProductos.GetProducts();
                Modelo.DTO.ProductosDto productModified = new Modelo.DTO.ProductosDto();

                try
                {
                    var currentProduct = listProducts.Find(x => x.ProductName.ToString() == this.dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString());


                    productModified.ProductName = currentProduct.ProductName;
                    productModified.BrandName = currentProduct.BrandName;
                    productModified.DescriptionProduct = currentProduct.DescriptionProduct;
                    productModified.StockProduct = currentProduct.StockProduct;

                    this.dgvProducts.Rows[e.RowIndex].Cells[7].Value = productModified.StockProduct;
                    Controladora.ControladoraProductos.EditarProducto(productModified);

                }
                catch
                {
                    MessageBox.Show("NO SE PUDO MODIFICAR EL PRODUCTO");
                }
            }
            else if(e.ColumnIndex == 7)
            {
                firstTime = true;
            }
        }

        private void dgvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //EDITAR MANUALMENTE EL STOCK

            if (firstTime) 
            {
            var listProducts = Controladora.ControladoraProductos.GetProducts();
            Modelo.DTO.ProductosDto productModified = new Modelo.DTO.ProductosDto();

            try
            {
                var currentProduct = listProducts.Find(x => x.ProductName.ToString() == this.dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString());

                productModified.ProductName = this.dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModified.BrandName = this.dgvProducts.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModified.DescriptionProduct = this.dgvProducts.Rows[e.RowIndex].Cells[6].Value.ToString();
                productModified.StockProduct = int.Parse(this.dgvProducts.Rows[e.RowIndex].Cells[7].Value.ToString());

                this.dgvProducts.Rows[e.RowIndex].Cells[7].Value = productModified.StockProduct;
                Controladora.ControladoraProductos.EditarProducto(productModified);

            }
            catch
            {
                MessageBox.Show("NO SE PUDO MODIFICAR EL PRODUCTO");
            }
            }
        }

    }
}
