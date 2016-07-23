﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlesdeUsuario;
using System.Reflection;

namespace SGLibrary
{
    public partial class FrmConciliaciones : Form
    {


        public ServiceConciliacion serviceConciliaciones { get; set; }

        public FrmConciliaciones()
        {
            InitializeComponent();
        }

        private void FrmConciliaciones_Load(object sender, EventArgs e)
        {

            
            botonesForm1.configMododeEdicion( ABMBotonesForm.FIND);
            this.panelcarga.Visible = false;
            this.panelbusqueda.Visible = true;
            this.fechadesde.Value = DateTime.Now.AddDays(-30).Date;
            this.fechahasta.Value = DateTime.Now.Date;
            this.botonesForm1.InicializarFindBoton();
        }



        public void deshabilitarycolorearCompensados()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Console.WriteLine(item.Cells["CONCILIAR"].EditedFormattedValue);
                if ( item.Cells["COMPENSADO"].EditedFormattedValue.ToString() == "SI")
                {
                    item.ReadOnly = true;
                    item.DefaultCellStyle.BackColor = Color.Red;
                }
                
                if (this.txtflEstado.Text  =="E")
                {
                    item.ReadOnly = true;
                    item.DefaultCellStyle.BackColor = Color.Gray;
                }
            }


        }

        private void botonesForm1_ClickEventDelegateHandler(object sender, EventArgs e)
        {
            ToolStripItem miboton = (ToolStripItem)sender;
            //MessageBox.Show("tocaste un boton, boton " + miboton.Name + " TAB " + miboton.Tag); 



            switch (miboton.Tag.ToString ()){
                case "EDIT" : {
                    this.panelcarga.Visible = true;
                    this.panelbusqueda.Visible  = false;
                    
              
                    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                    {
                        this.modoEdicion.Text = "SI";
                        TB_Conciliacion una_conciliacion = serviceConciliaciones.obtenerConciliacion(row.Cells["ID"].Value.ToString());
                        this.txtIdConciliacion.Text = una_conciliacion.IdConciliacion.ToString();
                        this.txtdsUsuario.Text = una_conciliacion.dsUsuario;
                        this.txtnrCajaAdm.Text = una_conciliacion.nrCajaAdm.ToString();
                        this.txtflEstado.Text = una_conciliacion.flestado;
                        if (una_conciliacion.flestado =="E")
                            botonesForm1.configMododeEdicion(ABMBotonesForm.VIEW);
                        else
                            botonesForm1.configMododeEdicion(ABMBotonesForm.EDIT);

                        cargarDataGridViewCupones(dataGridView1, serviceConciliaciones.ObtenerDetalleConciliacion(una_conciliacion.IdConciliacion), this.modoEdicion.Text);
                    }

                    deshabilitarycolorearCompensados();
                    break;
                }
                case "ADD":
                    {
                        this.modoEdicion.Text = "NO";
                        this.txtdsUsuario.Text = serviceConciliaciones.Usuario ;
                        this.txtnrCajaAdm.Text = serviceConciliaciones.CajaAdm;
                        this.panelcarga.Visible = true;
                        this.panelbusqueda.Visible = false;
                        var listadeViajesaConciliar = serviceConciliaciones.ObtenerViajesaConciliar();
                        cargarDataGridViewCupones(dataGridView1, listadeViajesaConciliar, modoEdicion.Text ); 
                        botonesForm1.configMododeEdicion(ABMBotonesForm.ADD);
                        break;
                    }
                case "FIND":
                    {
                        this.modoEdicion.Text = "NO";
                        this.panelcarga.Visible =  false ;
                        this.panelbusqueda.Visible = true;
                        botonesForm1.configMododeEdicion(ABMBotonesForm.FIND);
                        //serviceConciliaciones.obtenerUsuariosConciliaciones();
                        cargarCombo(this.cbUsuariosConciliaciones, serviceConciliaciones.obtenerUsuariosConciliaciones());
                        var listadeConciliaciones = serviceConciliaciones.obtenerConciliaciones(this.fechadesde.Value, this.fechahasta.Value , this.cbUsuariosConciliaciones.Text );
                        cargarDataGridViewConciliaciones(dataGridView2, listadeConciliaciones);
                        break;
                        
                    }
                
                case "OK":
                    {

                        if (this.modoEdicion.Text  =="NO") 
                        {
                            if (!altadeconciliacion()) break;
                        }
                        else
                        {
                            if (!ediciondeconciliacion()) break;
                        }


                        this.modoEdicion.Text = "NO";
                        var btnFind = new ToolStripButton();
                        btnFind.Tag = "FIND";
                        MessageBox.Show("La operación se ha realizado con éxito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        botonesForm1_ClickEventDelegateHandler(btnFind, null);
                        break;
                    }
                case "CANCEL":
                    {
                        this.modoEdicion.Text = "NO";
                        this.panelcarga.Visible = false;
                        this.panelbusqueda.Visible = true;
                        botonesForm1.configMododeEdicion(ABMBotonesForm.CANCEL);
                        break;
                    }

                case "DELETE":
                    {
                        this.modoEdicion.Text = "NO";
                        foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                        {
                            TB_Conciliacion una_conciliacion = serviceConciliaciones.obtenerConciliacion(row.Cells["ID"].Value.ToString());
                            DialogResult dialogResult = MessageBox.Show("Confirma la eliminación de la conciliación " + una_conciliacion.IdConciliacion.ToString(), "Atención", MessageBoxButtons.YesNo ,MessageBoxIcon.Question);
                             if(dialogResult == DialogResult.No ) break; 
                            // COMLETAR ELIMINACION
                             serviceConciliaciones.anularConciliacion(una_conciliacion);
                             MessageBox.Show("La operación se ha realizado con éxito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        }

                        var btnFind = new ToolStripButton();
                        btnFind.Tag = "FIND";
                        botonesForm1_ClickEventDelegateHandler(btnFind, null);

                        //this.panelcarga.Visible = false;
                        //this.panelbusqueda.Visible = true;
                        //botonesForm1.configMododeEdicion(ABMBotonesForm.DELETE);
                        
                        break;
                    }
                case "EXIT":
                    {
                        this.Close();
                        break;
                    }
            } 

        }


        public Boolean  altadeconciliacion()
        {

            List<Decimal> lista = new List<Decimal>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Console.WriteLine(item.Cells["CONCILIAR"].EditedFormattedValue);
                if (item.Cells["CONCILIAR"].EditedFormattedValue.ToString() == "True")
                {
                    lista.Add(Decimal.Parse(item.Cells["ID"].EditedFormattedValue.ToString()));
                }
                //DataGridViewCheckBoxColumn unControl = (DataGridViewCheckBoxColumn) item.Cells["CONCILIAR"].;
                //Console.WriteLine ( unControl.TrueValue);
            }

            if (lista.Count() == 0)
            {
                MessageBox.Show("Debe seleccionar algún comprobante.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Focus();
                return false;
            }

            var una_conciliacion = new TB_Conciliacion();
            una_conciliacion.dtConciliacion = this.cbdtConciliacion.Value;
            serviceConciliaciones.agregarConciliacion(lista, una_conciliacion);
            return true;
        }


        public Boolean ediciondeconciliacion()
        {
            List<Decimal> listaCupones = new List<Decimal>();
            List<Decimal> listaCuponesConciliados = new List<Decimal>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Console.WriteLine(item.Cells["CONCILIAR"].EditedFormattedValue);
                if (item.Cells["CONCILIAR"].EditedFormattedValue.ToString() == "True" && item.Cells["COMPENSADO"].EditedFormattedValue.ToString() == "NO")
                {
                    listaCuponesConciliados.Add(Decimal.Parse(item.Cells["ID"].EditedFormattedValue.ToString()));
                }
                //DataGridViewCheckBoxColumn unControl = (DataGridViewCheckBoxColumn) item.Cells["CONCILIAR"].;
                //Console.WriteLine ( unControl.TrueValue);
                listaCupones.Add (Decimal.Parse(item.Cells["ID"].EditedFormattedValue.ToString()));
            }

            if (listaCuponesConciliados.Count() == 0)
            {
                MessageBox.Show("Debe seleccionar algún comprobante");
                dataGridView1.Focus();
                return false;
            }

            var una_conciliacion = new TB_Conciliacion();
            una_conciliacion.dtConciliacion = this.cbdtConciliacion.Value;
            una_conciliacion.IdConciliacion = int.Parse  ( this.txtIdConciliacion.Text);
            serviceConciliaciones.modificarConciliacion(listaCupones , listaCuponesConciliados, una_conciliacion);
            return true;
        }

        public void cargarDataGridViewCupones(DataGridView dgv, IEnumerable<Object> lista , String p_modoEdicion  )
        {

            //dgv.Rows.Clear();
            dgv.Columns.Clear();

            foreach (var item in lista)
            {

                Type t = item.GetType();
                PropertyInfo[] pi = t.GetProperties();

                foreach (PropertyInfo p in pi)
                {
                    DataGridViewColumn columna = new DataGridViewColumn();
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    columna.CellTemplate = cell;
                    columna.Name = p.Name;
                    columna.HeaderText = p.Name;
                    columna.ReadOnly = true;
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgv.Columns.Add(columna);
                }
                break;
            }

            //dgv.Columns.Add("ID","ID");
            //columna = new DataGridViewColumn();
            //cell = new DataGridViewTextBoxCell();
            //columna.CellTemplate = cell;
            //columna.Name = "DOC";
            //columna.HeaderText = "DOC";
            //columna.ReadOnly = true;
            //dgv.Columns.Add(columna);
            //dgv.Columns.Add("LETRA", "LETRA");
            //dgv.Columns.Add("PDV", "PDV");
            //dgv.Columns.Add("NRO", "NRO");
            //dgv.Columns.Add("FECHA", "FECHA");
            //dgv.Columns.Add("MONTO", "MONTO");
            
            

            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.Name = "CONCILIAR";
            doWork.HeaderText = "CONCILIAR";
            doWork.FalseValue = "0";
            doWork.TrueValue = "1";

            dgv.Columns.Add(doWork);

            foreach (object item in lista)
            {
                var row = dgv.Rows.Add();
                Type t =item.GetType();
                PropertyInfo[] pi = t.GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    Console.WriteLine(p.Name + " " + p.GetValue(item, null));
                    dgv.Rows[row].Cells[p.Name].Value = p.GetValue(item, null);
                }

                if (p_modoEdicion=="SI") 
                {
                    dgv.Rows[row].Cells["CONCILIAR"].Value = true; 
                }

            }


        }

        public void cargarDataGridViewConciliaciones(DataGridView dgv, IEnumerable<Object> lista)
        {

            //dgv.Rows.Clear();
            dgv.Columns.Clear();

            foreach (var item in lista)
            {

                Type t = item.GetType();
                PropertyInfo[] pi = t.GetProperties();

                foreach (PropertyInfo p in pi)
                {
                    DataGridViewColumn columna = new DataGridViewColumn();
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    columna.CellTemplate = cell;
                    columna.Name = p.Name;
                    columna.HeaderText = p.Name;
                    columna.ReadOnly = true;
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgv.Columns.Add(columna);
                }
                break;
            }



            foreach (object item in lista)
            {
                var row = dgv.Rows.Add();
                Type t = item.GetType();
                PropertyInfo[] pi = t.GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    Console.WriteLine(p.Name + " " + p.GetValue(item, null));
                    dgv.Rows[row].Cells[p.Name].Value = p.GetValue(item, null);
                }
            }

            // Modificamos los Encabezados de las columnas
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.HeaderText = item.HeaderText.Replace("_", " ");
            }
                

        }

        private void botonesForm1_Load(object sender, EventArgs e)
        {

        }


        public void cargarCombo(ComboBox cb, IEnumerable<Object> lista)
        {


            cb.Items.Clear();

            foreach (object item in lista)
            {                
                Type t = item.GetType();
                PropertyInfo[] pi = t.GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    Console.WriteLine(p.Name + " " + p.GetValue(item, null));
                    cb.Items.Add ( p.GetValue(item, null));
                }
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }


        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var btnFind = new ToolStripButton();
            btnFind.Tag = "EDIT";
            botonesForm1_ClickEventDelegateHandler(btnFind, null);
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var btnFind = new ToolStripButton();
            btnFind.Tag = "EDIT";
            botonesForm1_ClickEventDelegateHandler(btnFind, null);
        }


    }
}
