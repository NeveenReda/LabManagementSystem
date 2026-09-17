namespace LabManagementSystem.Forms
{
    partial class FrmLabResult
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
            dgvResults = new DataGridView();
            colParameterId = new DataGridViewTextBoxColumn();
            colParameterName = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            colNormalRange = new DataGridViewTextBoxColumn();
            colResult = new DataGridViewTextBoxColumn();
            colNotes = new DataGridViewTextBoxColumn();
            txtResultInfo = new TextBox();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // dgvResults
            // 
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] { colParameterId, colParameterName, colUnit, colNormalRange, colResult, colNotes });
            dgvResults.Location = new Point(575, 117);
            dgvResults.Name = "dgvResults";
            dgvResults.RowHeadersWidth = 51;
            dgvResults.Size = new Size(716, 283);
            dgvResults.TabIndex = 0;
            // 
            // colParameterId
            // 
            colParameterId.DataPropertyName = "Id";
            colParameterId.HeaderText = "م";
            colParameterId.MinimumWidth = 6;
            colParameterId.Name = "colParameterId";
            colParameterId.Visible = false;
            colParameterId.Width = 125;
            // 
            // colParameterName
            // 
            colParameterName.HeaderText = "اسم التحليل";
            colParameterName.MinimumWidth = 6;
            colParameterName.Name = "colParameterName";
            colParameterName.ReadOnly = true;
            colParameterName.Width = 125;
            // 
            // colUnit
            // 
            colUnit.HeaderText = "الوحدة";
            colUnit.MinimumWidth = 6;
            colUnit.Name = "colUnit";
            colUnit.ReadOnly = true;
            colUnit.Width = 125;
            // 
            // colNormalRange
            // 
            colNormalRange.HeaderText = "المدى الطبيعي";
            colNormalRange.MinimumWidth = 6;
            colNormalRange.Name = "colNormalRange";
            colNormalRange.ReadOnly = true;
            colNormalRange.Width = 125;
            // 
            // colResult
            // 
            colResult.HeaderText = "النتيجة";
            colResult.MinimumWidth = 6;
            colResult.Name = "colResult";
            colResult.Width = 125;
            // 
            // colNotes
            // 
            colNotes.HeaderText = "ملاحظات";
            colNotes.MinimumWidth = 6;
            colNotes.Name = "colNotes";
            colNotes.Width = 125;
            // 
            // txtResultInfo
            // 
            txtResultInfo.Location = new Point(647, 40);
            txtResultInfo.Name = "txtResultInfo";
            txtResultInfo.RightToLeft = RightToLeft.Yes;
            txtResultInfo.Size = new Size(623, 27);
            txtResultInfo.TabIndex = 1;
            txtResultInfo.TextAlign = HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.MediumSeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(414, 117);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(117, 40);
            btnSave.TabIndex = 27;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // FrmLabResult
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1325, 543);
            Controls.Add(btnSave);
            Controls.Add(txtResultInfo);
            Controls.Add(dgvResults);
            Name = "FrmLabResult";
            Text = "FrmLabResult";
            Load += FrmLabResult_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvResults;
        private TextBox txtResultInfo;
        private DataGridViewTextBoxColumn colParameterId;
        private DataGridViewTextBoxColumn colParameterName;
        private DataGridViewTextBoxColumn colUnit;
        private DataGridViewTextBoxColumn colNormalRange;
        private DataGridViewTextBoxColumn colResult;
        private DataGridViewTextBoxColumn colNotes;
        private Button btnSave;
    }
}