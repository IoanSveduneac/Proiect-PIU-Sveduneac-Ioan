﻿namespace CarServiceManagement
{
    partial class Acasa
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
            this.add_order_btn = new System.Windows.Forms.Button();
            this.today_grid_view = new System.Windows.Forms.DataGridView();
            this.details_btn = new System.Windows.Forms.Button();
            this.logout_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.today_grid_view)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comenzi";
            // 
            // add_order_btn
            // 
            this.add_order_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_order_btn.Location = new System.Drawing.Point(601, 12);
            this.add_order_btn.Name = "add_order_btn";
            this.add_order_btn.Size = new System.Drawing.Size(137, 37);
            this.add_order_btn.TabIndex = 1;
            this.add_order_btn.Text = "Adauga comanda";
            this.add_order_btn.UseVisualStyleBackColor = true;
            this.add_order_btn.Click += new System.EventHandler(this.add_order_btn_Click);
            // 
            // today_grid_view
            // 
            this.today_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.today_grid_view.Location = new System.Drawing.Point(28, 106);
            this.today_grid_view.Name = "today_grid_view";
            this.today_grid_view.Size = new System.Drawing.Size(710, 328);
            this.today_grid_view.TabIndex = 2;
            this.today_grid_view.SelectionChanged += new System.EventHandler(this.today_grid_view_selection_changed);
            // 
            // details_btn
            // 
            this.details_btn.Enabled = false;
            this.details_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.details_btn.Location = new System.Drawing.Point(183, 450);
            this.details_btn.Name = "details_btn";
            this.details_btn.Size = new System.Drawing.Size(168, 37);
            this.details_btn.TabIndex = 3;
            this.details_btn.Text = "Vezi Detalii";
            this.details_btn.UseVisualStyleBackColor = true;
            this.details_btn.Click += new System.EventHandler(this.details_btn_Click);
            // 
            // logout_btn
            // 
            this.logout_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout_btn.Location = new System.Drawing.Point(448, 450);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(157, 37);
            this.logout_btn.TabIndex = 4;
            this.logout_btn.Text = "Logout";
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(474, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selecteaza o linie prin apasarea casutei goale din stanga acesteia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(347, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Apoi apasa pe vezi detalii pentru meniul avansat";
            // 
            // Acasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 499);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logout_btn);
            this.Controls.Add(this.details_btn);
            this.Controls.Add(this.today_grid_view);
            this.Controls.Add(this.add_order_btn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Acasa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acasa";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.today_grid_view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button add_order_btn;
        private System.Windows.Forms.DataGridView today_grid_view;
        private System.Windows.Forms.Button details_btn;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}