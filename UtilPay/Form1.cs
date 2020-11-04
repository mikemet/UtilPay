using System;
using System.Windows.Forms;
using System.Data;

namespace UtilPay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gaz = new Gaz();
            light = new Light();
        }

        // =======================  MENU  ==============================
        private void ExitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Программа для ведения учета комунальных платежей.\n Предназначена для владельцев отдельных (собственных) домов.\n Автор: mikemet\n Версия: 1.0.1.16\n ©2017-2020", "О Программе");
        }

        // ================ load tables in DataGridView ===============
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: this line of code allows you to load data into a table "uPayDBDataSet.Trash". If necessary, it can be moved or deleted.
            this.trashTableAdapter.Fill(this.uPayDBDataSet.Trash);
            // TODO: this line of code allows you to load data into a table "uPayDBDataSet.Water". If necessary, it can be moved or deleted.
            this.waterTableAdapter.Fill(this.uPayDBDataSet.Water);
            // TODO: this line of code allows you to load data into a table "uPayDBDataSet.Light". If necessary, it can be moved or deleted.
            this.lightTableAdapter.Fill(this.uPayDBDataSet.Light);
            // TODO: this line of code allows you to load data into a table "uPayDBDataSet.Gaz".
            //If necessary, it can be moved or deleted.
            gazTableAdapter.Fill(uPayDBDataSet.Gaz);

        }

        // =========================== gazTab ===========================
        Gaz gaz = new Gaz();
        string initialGaz;
        string finalGaz;

        // Insert only numbers and backspace in the textbox
        private void tbxGazPrimaryVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        private void tbxGazPrimaryVal_TextChanged(object sender, EventArgs e)
        {
            initialGaz = tbxGazPrimaryVal.Text;
        }

        // Insert only numbers and backspace in the textbox
        private void tbxGazFinalVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        private void tbxGazFinalVal_TextChanged(object sender, EventArgs e)
        {
            finalGaz = tbxGazFinalVal.Text;
        }

        // =============== Calculations for Gaz ===========================
        private void btnCalcGaz_Click(object sender, EventArgs e)
        {
            gaz.FinalGazVal = Convert.ToUInt32(finalGaz);
            gaz.InitialGazVal = Convert.ToUInt32(initialGaz);

            decimal gazCost = gaz.CalcGazCost();
            tbxGazResultPay.Text = gazCost.ToString();

            tbxSpentCub.Text = Convert.ToString(gaz.NumberOfCubes);
            tbxGazResultPay.Text = Convert.ToString(gaz.TotaGazlCost);
        }

        // ================ CLear Data Fields =============================
        private void btnGazReset_Click(object sender, EventArgs e)
        {
            tbxGazPrimaryVal.Clear();
            tbxGazFinalVal.Clear();
            tbxSpentCub.Clear();
            tbxGazResultPay.Clear();
        }

        // ============================= Save Gaz Calculations Results ===================
        private void btnGazSaveResult_Click(object sender, EventArgs e)
        {
            Validate();
            gazBindingSource.EndEdit();

            // Formation of three temporary tables
            UPayDBDataSet.GazDataTable deleteGaz = (UPayDBDataSet.GazDataTable)
                uPayDBDataSet.Gaz.GetChanges(DataRowState.Deleted);
            UPayDBDataSet.GazDataTable newGaz = (UPayDBDataSet.GazDataTable)
                uPayDBDataSet.Gaz.GetChanges(DataRowState.Added);
            UPayDBDataSet.GazDataTable modifiedGaz = (UPayDBDataSet.GazDataTable)
                uPayDBDataSet.Gaz.GetChanges(DataRowState.Modified);

            try
            {
                // Remove all deleted orders from the Gaz table.
                if (deleteGaz != null)
                {
                    gazTableAdapter.Update(deleteGaz);
                }
                // Add new orders to the Gaz table.
                if (newGaz != null)
                {
                    gazTableAdapter.Update(newGaz);
                }
                // Modified the Gaz table
                if (modifiedGaz != null)
                {
                    gazTableAdapter.Update(modifiedGaz);
                }

                uPayDBDataSet.AcceptChanges();
                MessageBox.Show("Изменения сохранены!");
            }
            catch (Exception)
            {
                MessageBox.Show("Обновление не удалось...");
            }
            // Clear resources
            finally
            {
                if (deleteGaz != null)
                {
                    deleteGaz.Dispose();
                }

                if (newGaz != null)
                {
                    newGaz.Dispose();
                }

                if (modifiedGaz != null)
                {
                    modifiedGaz.Dispose();
                }
            }
        }
        //================================= LightTab ==================================
        Light light = new Light();
        string initialLight;
        string finalLight;

        private void tbxLightPrimaryVal_TextChanged(object sender, EventArgs e)
        {
            initialLight = tbxLightPrimaryVal.Text;
        }

        private void tbxLightPrimaryVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        private void tbxLightFinalVal_TextChanged(object sender, EventArgs e)
        {
            finalLight = tbxLightFinalVal.Text;
        }

        private void tbxLightFinalVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        // ==================== Calculations for Light =======================
        private void btnCalcLight_Click(object sender, EventArgs e)
        {
            light.InitialLightVal = Convert.ToUInt32(initialLight);
            light.FinalLightVal = Convert.ToUInt32(finalLight);

            decimal lightCost = light.CalcLightCost();
            tbxLightResultPay.Text = lightCost.ToString();

            tbxSpentLight.Text = Convert.ToString(light.SpentKilowatt);
            tbxLightResultPay.Text = Convert.ToString(light.TotalLightCost);
        }
        // ================ CLear Data Fields =============================
        private void btnLightReset_Click(object sender, EventArgs e)
        {
            tbxLightFinalVal.Clear();
            tbxLightPrimaryVal.Clear();
            tbxSpentLight.Clear();
            tbxLightResultPay.Clear();
        }

        // ============================= Save Light Calculations Results ===================
        private void btnLightSaveResult_Click(object sender, EventArgs e)
        {
            Validate();
            lightBindingSource.EndEdit();

            // Formation of three temporary tables
            UPayDBDataSet.LightDataTable deleteLight = (UPayDBDataSet.LightDataTable)
                uPayDBDataSet.Light.GetChanges(DataRowState.Deleted);
            UPayDBDataSet.LightDataTable newLight = (UPayDBDataSet.LightDataTable)
                uPayDBDataSet.Light.GetChanges(DataRowState.Added);
            UPayDBDataSet.LightDataTable modifiedLight = (UPayDBDataSet.LightDataTable)
                uPayDBDataSet.Light.GetChanges(DataRowState.Modified);

            try
            {
                // Remove all deleted orders from the Light table.
                if (deleteLight != null)
                {
                    lightTableAdapter.Update(deleteLight);
                }
                // Add new orders to the Light table.
                if (newLight != null)
                {
                    lightTableAdapter.Update(newLight);
                }
                // Modified the Light table
                if (modifiedLight != null)
                {
                    lightTableAdapter.Update(modifiedLight);
                }

                uPayDBDataSet.AcceptChanges();
                MessageBox.Show("Изменения сохранены!");
            }
            catch (Exception)
            {
                MessageBox.Show("Обновление не удалось...");
            }
            // Clear resources
            finally
            {
                if (deleteLight != null)
                {
                    deleteLight.Dispose();
                }
                if (newLight != null)
                {
                    newLight.Dispose();
                }
                if (modifiedLight != null)
                {
                    modifiedLight.Dispose();
                }
            }
        }
        //=========================== WaterTab ===================================
        Water water = new Water();
        string initialWater;
        string finalWater;

        private void tbxWaterPrimaryVal_TextChanged(object sender, EventArgs e)
        {
            initialWater = tbxWaterPrimaryVal.Text;
        }

        private void tbxWaterPrimaryVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        private void tbxWaterFinalVal_TextChanged(object sender, EventArgs e)
        {
            finalWater = tbxWaterFinalVal.Text;
        }

        private void tbxWaterFinalVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }
        // ==================== Calculations for Water =======================
        private void btnCalcWater_Click(object sender, EventArgs e)
        {
            water.InitialWaterVal = Convert.ToUInt32(initialWater);
            water.FinalWaterVal = Convert.ToUInt32(finalWater);

            decimal waterCost = water.CalcWaterCost();
            tbxWaterResultPay.Text = waterCost.ToString();

            tbxSpentWater.Text = Convert.ToString(water.SpentCubes);
            tbxWaterResultPay.Text = Convert.ToString(water.TotalWaterCost);
        }
        // ================ CLear Data Fields =============================
        private void btnWaterReset_Click(object sender, EventArgs e)
        {
            tbxWaterPrimaryVal.Clear();
            tbxWaterFinalVal.Clear();
            tbxSpentWater.Clear();
            tbxWaterResultPay.Clear();
        }
        // ============================= Save Water Calculations Results ===================
        private void btnWaterSaveResult_Click(object sender, EventArgs e)
        {
            Validate();
            waterBindingSource.EndEdit();

            // Formation of three temporary tables
            UPayDBDataSet.WaterDataTable deleteWater = (UPayDBDataSet.WaterDataTable)
                uPayDBDataSet.Water.GetChanges(DataRowState.Deleted);
            UPayDBDataSet.WaterDataTable newWater = (UPayDBDataSet.WaterDataTable)
                uPayDBDataSet.Water.GetChanges(DataRowState.Added);
            UPayDBDataSet.WaterDataTable modifiedWater = (UPayDBDataSet.WaterDataTable)
                uPayDBDataSet.Water.GetChanges(DataRowState.Modified);

            try
            {
                // Remove all deleted orders from the Light table.
                if (deleteWater != null)
                {
                    waterTableAdapter.Update(deleteWater);
                }
                // Add new orders to the Light table.
                if (newWater != null)
                {
                    waterTableAdapter.Update(newWater);
                }
                // Modified the Light table
                if (modifiedWater != null)
                {
                    waterTableAdapter.Update(modifiedWater);
                }

                uPayDBDataSet.AcceptChanges();
                MessageBox.Show("Изменения сохранены!");
            }
            catch (Exception)
            {
                MessageBox.Show("Обновление не удалось...");
            }
            // Clear resources
            finally
            {
                if (deleteWater != null)
                {
                    deleteWater.Dispose();
                }
                if (newWater != null)
                {
                    newWater.Dispose();
                }
                if (modifiedWater != null)
                {
                    modifiedWater.Dispose();
                }
            }
        }

        //============================== TrashTab ===============================
        Trash trash = new Trash();
        string numberOfPersons;

        private void tbxPersonAmount_TextChanged(object sender, EventArgs e)
        {
            numberOfPersons = tbxPersonAmount.Text;
        }

        private void tbxPersonAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumbers(e);
        }

        // ====================== Calculation for Trash ========================
        private void btnTrashCalc_Click_1(object sender, EventArgs e)
        {
            trash.numberOfPersons = Convert.ToUInt32(numberOfPersons);

            decimal trashCoast = trash.CalcTrashCoast();
            tbxTrashResultPay.Text = trashCoast.ToString();

            tbxTrashResultPay.Text = Convert.ToString(trash.totalTrashCoast);
        }

        // ================ CLear Data Fields =============================
        private void btnTrashClear_Click_1(object sender, EventArgs e)
        {
            tbxPersonAmount.Clear();
            tbxTrashResultPay.Clear();
        }

        // ============================= Save Trash Calculations Results ===================
        private void BtnSaveTrashData_Click_1(object sender, EventArgs e)
        {
            Validate();
            trashBindingSource.EndEdit();

            // Formation of three temporary tables
            UPayDBDataSet.TrashDataTable deleteTrash = (UPayDBDataSet.TrashDataTable)
                uPayDBDataSet.Trash.GetChanges(DataRowState.Deleted);
            UPayDBDataSet.TrashDataTable newTrash = (UPayDBDataSet.TrashDataTable)
                uPayDBDataSet.Trash.GetChanges(DataRowState.Added);
            UPayDBDataSet.TrashDataTable modifiedTrash = (UPayDBDataSet.TrashDataTable)
                uPayDBDataSet.Trash.GetChanges(DataRowState.Modified);

            try
            {
                // Remove all deleted orders from the Trash table.
                if (deleteTrash != null)
                {
                    trashTableAdapter.Update(deleteTrash);
                }
                // Add new orders to the Trash table.
                if (newTrash !=null)
                {
                    trashTableAdapter.Update(newTrash);
                }
                // Modified the Trash table
                if (modifiedTrash != null)
                {
                    trashTableAdapter.Update(modifiedTrash);
                }

                uPayDBDataSet.AcceptChanges();
                MessageBox.Show("Изменения сохранены!");
            }
            catch (Exception)
            {
                MessageBox.Show("Обновление не удалось...");
            }
            // Clear resources
            finally
            {
                if (deleteTrash != null)
                {
                    deleteTrash.Dispose();
                }
                if (newTrash != null)
                {
                    newTrash.Dispose();
                }
                if (modifiedTrash != null)
                {
                    modifiedTrash.Dispose();
                }
            }
        }

        // Insert only numbers and backspace in the textbox
        private void OnlyNumbers(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // numbers and key BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
