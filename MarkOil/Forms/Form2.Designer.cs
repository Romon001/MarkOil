﻿namespace MarkOil
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSUL = new System.Windows.Forms.TextBox();
            this.textBoxCST = new System.Windows.Forms.TextBox();
            this.textBoxSPG = new System.Windows.Forms.TextBox();
            this.textBoxPAR = new System.Windows.Forms.TextBox();
            this.textBox350 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.indexSelectionButton = new System.Windows.Forms.Button();
            this.closenessSelectionButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dbMarkDataSet1 = new MarkOil.dbMarkDataSet();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dbMarkDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Содержание серы,%вес.....................SUL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(20, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Отбор фракций до 350С, %вес ...........350";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxSUL
            // 
            this.textBoxSUL.Location = new System.Drawing.Point(296, 28);
            this.textBoxSUL.Name = "textBoxSUL";
            this.textBoxSUL.Size = new System.Drawing.Size(100, 20);
            this.textBoxSUL.TabIndex = 2;
            this.textBoxSUL.Text = "0";
            this.textBoxSUL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCST
            // 
            this.textBoxCST.Location = new System.Drawing.Point(296, 132);
            this.textBoxCST.Name = "textBoxCST";
            this.textBoxCST.Size = new System.Drawing.Size(100, 20);
            this.textBoxCST.TabIndex = 13;
            this.textBoxCST.Text = "0";
            this.textBoxCST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSPG
            // 
            this.textBoxSPG.Location = new System.Drawing.Point(296, 106);
            this.textBoxSPG.Name = "textBoxSPG";
            this.textBoxSPG.Size = new System.Drawing.Size(100, 20);
            this.textBoxSPG.TabIndex = 14;
            this.textBoxSPG.Text = "0";
            this.textBoxSPG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxPAR
            // 
            this.textBoxPAR.Location = new System.Drawing.Point(296, 80);
            this.textBoxPAR.Name = "textBoxPAR";
            this.textBoxPAR.Size = new System.Drawing.Size(100, 20);
            this.textBoxPAR.TabIndex = 15;
            this.textBoxPAR.Text = "0";
            this.textBoxPAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox350
            // 
            this.textBox350.Location = new System.Drawing.Point(296, 54);
            this.textBox350.Name = "textBox350";
            this.textBox350.Size = new System.Drawing.Size(100, 20);
            this.textBox350.TabIndex = 16;
            this.textBox350.Text = "0";
            this.textBox350.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(424, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 21;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(424, 80);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 20;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(424, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 19;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(424, 132);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(424, 28);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 17;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(556, 54);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 26;
            this.textBox7.Text = "0,2";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(556, 80);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 25;
            this.textBox8.Text = "0,2";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(556, 106);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 24;
            this.textBox9.Text = "0,2";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(556, 132);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 20);
            this.textBox10.TabIndex = 23;
            this.textBox10.Text = "0,2";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(556, 28);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 20);
            this.textBox11.TabIndex = 22;
            this.textBox11.Text = "0,2";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(20, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Парафины, %вес PAR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(20, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Относительная плотность нефти, SPG";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(20, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Кинематическая вязкость при 20С, сст, CST";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(556, 177);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 20);
            this.textBox16.TabIndex = 30;
            this.textBox16.Text = "1";
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(338, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Количество аналогов по близости";
            // 
            // indexSelectionButton
            // 
            this.indexSelectionButton.Location = new System.Drawing.Point(655, 213);
            this.indexSelectionButton.Name = "indexSelectionButton";
            this.indexSelectionButton.Size = new System.Drawing.Size(132, 41);
            this.indexSelectionButton.TabIndex = 33;
            this.indexSelectionButton.Text = "Подбор аналогов по индексу";
            this.indexSelectionButton.UseCompatibleTextRendering = true;
            this.indexSelectionButton.UseVisualStyleBackColor = true;
            this.indexSelectionButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // closenessSelectionButton
            // 
            this.closenessSelectionButton.Location = new System.Drawing.Point(657, 269);
            this.closenessSelectionButton.Name = "closenessSelectionButton";
            this.closenessSelectionButton.Size = new System.Drawing.Size(130, 37);
            this.closenessSelectionButton.TabIndex = 34;
            this.closenessSelectionButton.Text = "Подбор аналогов по близости";
            this.closenessSelectionButton.UseCompatibleTextRendering = true;
            this.closenessSelectionButton.UseVisualStyleBackColor = true;
            this.closenessSelectionButton.Click += new System.EventHandler(this.closenessSelectionButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(655, 321);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 41);
            this.button3.TabIndex = 35;
            this.button3.Text = "Рассчет рецептуры МЭ";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(657, 375);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(132, 35);
            this.button5.TabIndex = 37;
            this.button5.Text = "Закрыть форму";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "label7";
            // 
            // dbMarkDataSet1
            // 
            this.dbMarkDataSet1.DataSetName = "dbMarkDataSet";
            this.dbMarkDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(22, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(627, 198);
            this.dataGridView1.TabIndex = 40;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 420;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(293, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Значение";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(421, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Допуск";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(553, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Вес свойства";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 422);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.closenessSelectionButton);
            this.Controls.Add(this.indexSelectionButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox350);
            this.Controls.Add(this.textBoxPAR);
            this.Controls.Add(this.textBoxSPG);
            this.Controls.Add(this.textBoxCST);
            this.Controls.Add(this.textBoxSUL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Подбор аналогов";
            ((System.ComponentModel.ISupportInitialize)(this.dbMarkDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSUL;
        private System.Windows.Forms.TextBox textBoxCST;
        private System.Windows.Forms.TextBox textBoxSPG;
        private System.Windows.Forms.TextBox textBoxPAR;
        private System.Windows.Forms.TextBox textBox350;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button indexSelectionButton;
        private System.Windows.Forms.Button closenessSelectionButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private dbMarkDataSet dbMarkDataSet1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}