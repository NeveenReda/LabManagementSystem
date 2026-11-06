using LabManagementSystem.Common;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmTasks : Form
    {
        private readonly ITaskService _taskService;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        private TextBox txtTitle = null!;
        private TextBox txtNotes = null!;
        private ComboBox cmbPriority = null!;
        private CheckBox chkHasDueDate = null!;
        private DateTimePicker dtDueDate = null!;
        private Button btnSave = null!;
        private Button btnNew = null!;
        private Button btnDelete = null!;

        private ComboBox cmbStatusFilter = null!;
        private ComboBox cmbPriorityFilter = null!;
        private CheckBox chkOnlyOverdue = null!;
        private DataGridView dgvTasks = null!;

        private Panel pnlGridCard = null!;
        private Need? _selectedTask;

        public FrmTasks()
        {
            _taskService = ServiceLocator.TaskService;
            InitializeUi();
            Shown += async (s, e) => await LoadTasksAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "سجل المهام والنواقص والتنبيهات");
            Padding = new Padding(16);

            // 1. Header Banner
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "سجل المهام، النواقص، والمتابعات الإدارية",
                "تسجيل ومتابعة المهام المطلوبة، النواقص، الأولويات، والمواعيد المستحقة للإنجاز",
                "📝"
            );

            // 2. Inputs Card (Responsive TableLayoutPanel)
            var pnlInputs = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(14));
            pnlInputs.Height = 165;

            var tlpInputs = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));   // Title
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));   // Priority
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F)); // Checkbox Due
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));   // Due Date

            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            // Row 0: Title, Priority, Due Date
            var lblTitle = new Label { Text = "عنوان المهمة:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtTitle = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtTitle);

            var lblPriority = new Label { Text = "الأولوية:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            cmbPriority = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont };
            cmbPriority.Items.Add(new { Text = "منخفضة (Low)", Value = 1 });
            cmbPriority.Items.Add(new { Text = "متوسطة (Medium)", Value = 2 });
            cmbPriority.Items.Add(new { Text = "هامة (High)", Value = 3 });
            cmbPriority.Items.Add(new { Text = "عاجلة (Urgent)", Value = 4 });
            cmbPriority.DisplayMember = "Text";
            cmbPriority.ValueMember = "Value";
            cmbPriority.SelectedIndex = 1;

            chkHasDueDate = new CheckBox { Text = "تاريخ استحقاق:", AutoSize = true, Checked = false, Font = ThemeManager.BodyBoldFont };
            dtDueDate = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Enabled = false, Font = ThemeManager.BodyFont };
            chkHasDueDate.CheckedChanged += (s, e) => dtDueDate.Enabled = chkHasDueDate.Checked;

            tlpInputs.Controls.Add(lblTitle, 0, 0);
            tlpInputs.Controls.Add(txtTitle, 1, 0);
            tlpInputs.Controls.Add(lblPriority, 2, 0);
            tlpInputs.Controls.Add(cmbPriority, 3, 0);
            tlpInputs.Controls.Add(chkHasDueDate, 4, 0);
            tlpInputs.Controls.Add(dtDueDate, 5, 0);

            // Row 1: Notes
            var lblNotes = new Label { Text = "التفاصيل / البيان:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtNotes = new TextBox { Dock = DockStyle.Fill, Multiline = true, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtNotes);

            tlpInputs.Controls.Add(lblNotes, 0, 1);
            tlpInputs.SetColumnSpan(txtNotes, 5);
            tlpInputs.Controls.Add(txtNotes, 1, 1);

            // Row 2: Action Buttons
            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnSave = new Button { Text = "💾 حفظ المهمة", Width = 145, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            btnSave.Click += async (s, e) => await SaveTaskAsync();

            btnNew = new Button { Text = "➕ جديد", Width = 95, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnNew, ButtonStyle.Secondary);
            btnNew.Click += (s, e) => ResetInputs();

            btnDelete = new Button { Text = "🗑️ حذف المهمة", Width = 130, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnDelete, ButtonStyle.Danger);
            btnDelete.Click += async (s, e) => await DeleteTaskAsync();

            flpButtons.Controls.Add(btnSave);
            flpButtons.Controls.Add(btnNew);
            flpButtons.Controls.Add(btnDelete);

            tlpInputs.SetColumnSpan(flpButtons, 6);
            tlpInputs.Controls.Add(flpButtons, 0, 2);

            pnlInputs.Controls.Add(tlpInputs);

            // 3. Filter Bar (Responsive FlowLayoutPanel)
            var pnlFilter = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(10, 8, 10, 8));
            pnlFilter.Height = 52;

            var flpFilter = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblSF = new Label { Text = "الحالة:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            cmbStatusFilter = new ComboBox { Width = 135, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            cmbStatusFilter.Items.Add(new { Text = "الكل", Value = (bool?)null });
            cmbStatusFilter.Items.Add(new { Text = "قيد الانتظار", Value = (bool?)false });
            cmbStatusFilter.Items.Add(new { Text = "مكتملة", Value = (bool?)true });
            cmbStatusFilter.DisplayMember = "Text";
            cmbStatusFilter.ValueMember = "Value";
            cmbStatusFilter.SelectedIndex = 1;
            cmbStatusFilter.SelectedIndexChanged += async (s, e) => await LoadTasksAsync();

            var lblPF = new Label { Text = "الأولوية:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            cmbPriorityFilter = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            cmbPriorityFilter.Items.Add(new { Text = "كافة الأولويات", Value = 0 });
            cmbPriorityFilter.Items.Add(new { Text = "منخفضة", Value = 1 });
            cmbPriorityFilter.Items.Add(new { Text = "متوسطة", Value = 2 });
            cmbPriorityFilter.Items.Add(new { Text = "هامة", Value = 3 });
            cmbPriorityFilter.Items.Add(new { Text = "عاجلة", Value = 4 });
            cmbPriorityFilter.DisplayMember = "Text";
            cmbPriorityFilter.ValueMember = "Value";
            cmbPriorityFilter.SelectedIndex = 0;
            cmbPriorityFilter.SelectedIndexChanged += async (s, e) => await LoadTasksAsync();

            chkOnlyOverdue = new CheckBox { Text = "عرض المتأخرة فقط (Overdue)", AutoSize = true, Font = ThemeManager.BodyBoldFont, ForeColor = ThemeManager.Danger, Margin = new Padding(12, 6, 4, 0) };
            chkOnlyOverdue.CheckedChanged += async (s, e) => await LoadTasksAsync();

            flpFilter.Controls.Add(lblSF);
            flpFilter.Controls.Add(cmbStatusFilter);
            flpFilter.Controls.Add(lblPF);
            flpFilter.Controls.Add(cmbPriorityFilter);
            flpFilter.Controls.Add(chkOnlyOverdue);

            pnlFilter.Controls.Add(flpFilter);

            // 4. Grid Card
            pnlGridCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(12));

            dgvTasks = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvTasks);

            var colCheck = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = "مكتملة",
                DataPropertyName = "IsCompleted",
                Width = 75
            };
            dgvTasks.Columns.Add(colCheck);
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTitle", DataPropertyName = "Title", HeaderText = "عنوان المهمة", FillWeight = 25 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNotes", DataPropertyName = "Notes", HeaderText = "التفاصيل / الملاحظات", FillWeight = 45 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPriority", HeaderText = "الأولوية", FillWeight = 15 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDueDate", HeaderText = "تاريخ الاستحقاق", FillWeight = 15 });

            dgvTasks.CellContentClick += async (s, e) => await DgvTasks_CellContentClick(s, e);
            dgvTasks.SelectionChanged += DgvTasks_SelectionChanged;

            pnlGridCard.Controls.Add(dgvTasks);

            Controls.Add(pnlGridCard);
            Controls.Add(pnlFilter);
            Controls.Add(pnlInputs);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlGridCard);
        }

        private async Task LoadTasksAsync()
        {
            if (IsDisposed) return;

            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري تحميل المهام والنواقص...");
            try
            {
                bool? isCompleted = cmbStatusFilter.SelectedItem != null ? (bool?)((dynamic)cmbStatusFilter.SelectedItem).Value : null;
                int prio = cmbPriorityFilter.SelectedItem != null ? (int)((dynamic)cmbPriorityFilter.SelectedItem).Value : 0;
                int? priority = prio == 0 ? null : prio;

                var filter = new TaskFilterDto
                {
                    IsCompleted = isCompleted,
                    Priority = priority,
                    OnlyOverdue = chkOnlyOverdue.Checked
                };

                var tasks = await _taskService.GetTasksAsync(filter, token);

                if (token.IsCancellationRequested || IsDisposed) return;

                dgvTasks.DataSource = null;
                dgvTasks.Rows.Clear();

                foreach (var t in tasks)
                {
                    string prioStr = t.Priority switch
                    {
                        1 => "منخفضة",
                        2 => "متوسطة",
                        3 => "هامة",
                        4 => "عاجلة",
                        _ => "عادية"
                    };

                    string dueStr = t.DueDate.HasValue ? t.DueDate.Value.ToString("yyyy/MM/dd") : "-";

                    int rIdx = dgvTasks.Rows.Add(t.IsCompleted, t.Title, t.Notes, prioStr, dueStr);
                    dgvTasks.Rows[rIdx].Tag = t;

                    // Color overdue
                    if (!t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Today)
                    {
                        dgvTasks.Rows[rIdx].DefaultCellStyle.BackColor = ThemeManager.DangerLight;
                        dgvTasks.Rows[rIdx].DefaultCellStyle.ForeColor = ThemeManager.Danger;
                    }
                    else if (t.IsCompleted)
                    {
                        dgvTasks.Rows[rIdx].DefaultCellStyle.ForeColor = ThemeManager.SecondaryText;
                    }
                }

                ResetInputs();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show($"خطأ أثناء تحميل المهام:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    _loadingOverlay.HideOverlay();
            }
        }

        private void ResetInputs()
        {
            _selectedTask = null;
            txtTitle.Clear();
            txtNotes.Clear();
            cmbPriority.SelectedIndex = 1;
            chkHasDueDate.Checked = false;
            dtDueDate.Value = DateTime.Today.AddDays(1);
            btnSave.Text = "💾 حفظ المهمة";
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
        }

        private void DgvTasks_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow?.Tag is Need task)
            {
                _selectedTask = task;
                txtTitle.Text = task.Title;
                txtNotes.Text = task.Notes ?? "";
                cmbPriority.SelectedIndex = Math.Clamp((int)task.Priority - 1, 0, 3);
                chkHasDueDate.Checked = task.DueDate.HasValue;
                if (task.DueDate.HasValue) dtDueDate.Value = task.DueDate.Value;
                btnSave.Text = "✏️ تحديث المهمة";
                FormStyler.StyleButton(btnSave, ButtonStyle.Primary);
            }
        }

        private async Task SaveTaskAsync()
        {
            string title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("يرجى إدخال عنوان المهمة أولاً.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            int prioVal = cmbPriority.SelectedItem != null ? (int)((dynamic)cmbPriority.SelectedItem).Value : 2;

            try
            {
                var task = _selectedTask ?? new Need { CreatedAt = DateTime.Now, IsCompleted = false };
                task.Title = title;
                task.Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? "" : txtNotes.Text.Trim();
                task.Priority = prioVal;
                task.DueDate = chkHasDueDate.Checked ? dtDueDate.Value.Date : null;

                await _taskService.SaveTaskAsync(task);
                MessageBox.Show("تم حفظ المهمة بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ المهمة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DgvTasks_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvTasks.Columns[e.ColumnIndex].Name == "colCheck")
            {
                if (dgvTasks.Rows[e.RowIndex].Tag is Need task)
                {
                    await _taskService.ToggleTaskStatusAsync(task.Id);
                    await LoadTasksAsync();
                }
            }
        }

        private async Task DeleteTaskAsync()
        {
            if (_selectedTask == null)
            {
                MessageBox.Show("يرجى تحديد مهمة لحذفها أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من حذف المهمة '{_selectedTask.Title}'؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _taskService.DeleteTaskAsync(_selectedTask.Id);
                    MessageBox.Show("تم حذف المهمة بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadTasksAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _searchCts?.Dispose();
        }
    }
}
