namespace CompensationCalculator
{
	partial class Form1
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
			this.SalaryBox = new System.Windows.Forms.NumericUpDown();
			this.SalaryButton = new System.Windows.Forms.Button();
			this.TaxProfileGroup = new System.Windows.Forms.GroupBox();
			this.OutputBox = new System.Windows.Forms.TextBox();
			this.WageButton = new System.Windows.Forms.Button();
			this.WageBox = new System.Windows.Forms.NumericUpDown();
			this.ContractButton = new System.Windows.Forms.Button();
			this.ContractBox = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.SalaryBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WageBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ContractBox)).BeginInit();
			this.SuspendLayout();
			// 
			// SalaryBox
			// 
			this.SalaryBox.DecimalPlaces = 2;
			this.SalaryBox.Location = new System.Drawing.Point(12, 12);
			this.SalaryBox.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.SalaryBox.Name = "SalaryBox";
			this.SalaryBox.Size = new System.Drawing.Size(83, 20);
			this.SalaryBox.TabIndex = 2;
			this.SalaryBox.ThousandsSeparator = true;
			this.SalaryBox.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			// 
			// SalaryButton
			// 
			this.SalaryButton.Location = new System.Drawing.Point(101, 12);
			this.SalaryButton.Name = "SalaryButton";
			this.SalaryButton.Size = new System.Drawing.Size(83, 23);
			this.SalaryButton.TabIndex = 3;
			this.SalaryButton.Text = "Salary";
			this.SalaryButton.UseVisualStyleBackColor = true;
			this.SalaryButton.Click += new System.EventHandler(this.SalaryButton_Click);
			// 
			// TaxProfileGroup
			// 
			this.TaxProfileGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TaxProfileGroup.Location = new System.Drawing.Point(418, 12);
			this.TaxProfileGroup.Name = "TaxProfileGroup";
			this.TaxProfileGroup.Size = new System.Drawing.Size(304, 129);
			this.TaxProfileGroup.TabIndex = 4;
			this.TaxProfileGroup.TabStop = false;
			this.TaxProfileGroup.Text = "Tax Profile";
			// 
			// OutputBox
			// 
			this.OutputBox.Location = new System.Drawing.Point(12, 99);
			this.OutputBox.Multiline = true;
			this.OutputBox.Name = "OutputBox";
			this.OutputBox.ReadOnly = true;
			this.OutputBox.Size = new System.Drawing.Size(373, 292);
			this.OutputBox.TabIndex = 5;
			// 
			// WageButton
			// 
			this.WageButton.Location = new System.Drawing.Point(101, 41);
			this.WageButton.Name = "WageButton";
			this.WageButton.Size = new System.Drawing.Size(83, 23);
			this.WageButton.TabIndex = 7;
			this.WageButton.Text = "Wage";
			this.WageButton.UseVisualStyleBackColor = true;
			this.WageButton.Click += new System.EventHandler(this.WageButton_Click);
			// 
			// WageBox
			// 
			this.WageBox.DecimalPlaces = 2;
			this.WageBox.Location = new System.Drawing.Point(12, 41);
			this.WageBox.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.WageBox.Name = "WageBox";
			this.WageBox.Size = new System.Drawing.Size(83, 20);
			this.WageBox.TabIndex = 6;
			this.WageBox.ThousandsSeparator = true;
			this.WageBox.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			// 
			// ContractButton
			// 
			this.ContractButton.Location = new System.Drawing.Point(101, 70);
			this.ContractButton.Name = "ContractButton";
			this.ContractButton.Size = new System.Drawing.Size(83, 23);
			this.ContractButton.TabIndex = 9;
			this.ContractButton.Text = "Contract";
			this.ContractButton.UseVisualStyleBackColor = true;
			this.ContractButton.Click += new System.EventHandler(this.ContractButton_Click);
			// 
			// ContractBox
			// 
			this.ContractBox.DecimalPlaces = 2;
			this.ContractBox.Location = new System.Drawing.Point(12, 70);
			this.ContractBox.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.ContractBox.Name = "ContractBox";
			this.ContractBox.Size = new System.Drawing.Size(83, 20);
			this.ContractBox.TabIndex = 8;
			this.ContractBox.ThousandsSeparator = true;
			this.ContractBox.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 403);
			this.Controls.Add(this.ContractButton);
			this.Controls.Add(this.ContractBox);
			this.Controls.Add(this.WageButton);
			this.Controls.Add(this.WageBox);
			this.Controls.Add(this.OutputBox);
			this.Controls.Add(this.TaxProfileGroup);
			this.Controls.Add(this.SalaryButton);
			this.Controls.Add(this.SalaryBox);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.SalaryBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WageBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ContractBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NumericUpDown SalaryBox;
		private System.Windows.Forms.Button SalaryButton;
		private System.Windows.Forms.GroupBox TaxProfileGroup;
		private System.Windows.Forms.TextBox OutputBox;
		private System.Windows.Forms.Button WageButton;
		private System.Windows.Forms.NumericUpDown WageBox;
		private System.Windows.Forms.Button ContractButton;
		private System.Windows.Forms.NumericUpDown ContractBox;
	}
}

