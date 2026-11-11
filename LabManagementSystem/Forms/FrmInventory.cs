using System.Diagnostics;
using System.IO;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmInventory : Form
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILookupCacheService _lookupCache;
        private LoadingOverlay _loadingOverlay = null!;

        private TabControl tabs = null!;

        // Products tab controls
        private DataGridView dgvProducts = null!;
        private TextBox txtProdName = null!;
        private ComboBox cmbCats = null!;
        private ComboBox cmbUnits = null!;
        private NumericUpDown numInitial = null!;
        private NumericUpDown numMin = null!;
        private Button btnSaveProd = null!;
        private Button btnNewProd = null!;
        private Button btnDeleteProd = null!;
        private Product? _selectedProd;

        // Orders tab controls
        private DateTimePicker dtOrderDate = null!;
        private TextBox txtOrderNotes = null!;
        private ComboBox cmbOrderProd = null!;
        private NumericUpDown numOrderQty = null!;
        private TextBox txtOrderPrice = null!;
        private Button btnAddOrderItem = null!;
        private Button btnSaveOrder = null!;
        private Button btnViewOrder = null!;
        private Button btnPrintOrder = null!;
        private Button btnExportOrderPdf = null!;
        private Label lblOrderTotal = null!;
        private DataGridView dgvOrderItems = null!;
        private DataGridView dgvOrders = null!;
        private readonly List<Tuple<int, string, int, decimal>> _currentOrderItems = new();

        public FrmInventory(int initialTabIndex = 0)
        {
            _inventoryService = ServiceLocator.InventoryService;
            _lookupCache = ServiceLocator.LookupCacheService;
            InitializeUi();
            if (initialTabIndex > 0 && tabs != null && initialTabIndex < tabs.TabCount)
            {
                tabs.SelectedIndex = initialTabIndex;
            }
            Shown += (s, e) =>
            {
                LoadLookupData();
                LoadProducts();
                LoadOrders();
            };
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "إدارة المخزون والمشتريات");
            Padding = new Padding(16);

            // 1. Header Banner
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "إدارة الكيماويات والمستلزمات وطلبات الشراء (Purchases & Inventory)",
                "متابعة أرصدة الكواشف والمحاليل المخبرية، تنبيهات حد الطلب الأدنى، وفواتير التوريد",
                "📦"
            );

            // 2. Tab Control
            var pnlTabsCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(8));
            tabs = new TabControl { Name = "tabsInventory", Dock = DockStyle.Fill, Font = ThemeManager.BodyBoldFont, RightToLeft = RightToLeft.Yes, RightToLeftLayout = true };

            var tabProds = new TabPage { Text = "🧪 الأصناف والكيماويات والمستلزمات" };
            InitProductsTab(tabProds);

            var tabOrders = new TabPage { Text = "🛒 فواتير وطلبيات الشراء (Purchases)" };
            InitOrdersTab(tabOrders);

            tabs.TabPages.AddRange(new[] { tabProds, tabOrders });
            pnlTabsCard.Controls.Add(tabs);

            Controls.Add(pnlTabsCard);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlTabsCard);
        }

        private void InitProductsTab(TabPage tab)
        {
            var pnlInputs = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(14));
            pnlInputs.Height = 150;

            var tlpInputs = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));   // Name
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));   // Cat
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));   // Unit

            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            // Row 0: Name, Cat, Unit
            var lblN = new Label { Text = "اسم الصنف:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtProdName = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtProdName);

            var lblC = new Label { Text = "التصنيف:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            cmbCats = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont };

            var lblU = new Label { Text = "الوحدة:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            cmbUnits = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont };

            tlpInputs.Controls.Add(lblN, 0, 0);
            tlpInputs.Controls.Add(txtProdName, 1, 0);
            tlpInputs.Controls.Add(lblC, 2, 0);
            tlpInputs.Controls.Add(cmbCats, 3, 0);
            tlpInputs.Controls.Add(lblU, 4, 0);
            tlpInputs.Controls.Add(cmbUnits, 5, 0);

            // Row 1: Initial Amount, Min Amount
            var lblI = new Label { Text = "الرصيد المتاح:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            numInitial = new NumericUpDown { Dock = DockStyle.Fill, Maximum = 999999, Font = ThemeManager.BodyFont };

            var lblM = new Label { Text = "الحد الأدنى:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            numMin = new NumericUpDown { Dock = DockStyle.Fill, Maximum = 999999, Value = 2, Font = ThemeManager.BodyFont };

            tlpInputs.Controls.Add(lblI, 0, 1);
            tlpInputs.Controls.Add(numInitial, 1, 1);
            tlpInputs.Controls.Add(lblM, 2, 1);
            tlpInputs.Controls.Add(numMin, 3, 1);

            // Row 2: Buttons
            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnSaveProd = new Button { Text = "💾 حفظ الصنف", Width = 140, Height = 35, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnSaveProd, ButtonStyle.Success);
            btnSaveProd.Click += BtnSaveProd_Click;

            btnNewProd = new Button { Text = "➕ جديد", Width = 95, Height = 35, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnNewProd, ButtonStyle.Secondary);
            btnNewProd.Click += (s, e) => ResetProdInputs();

            btnDeleteProd = new Button { Text = "🗑️ حذف الصنف", Width = 125, Height = 35, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnDeleteProd, ButtonStyle.Danger);
            btnDeleteProd.Click += BtnDeleteProd_Click;

            flpButtons.Controls.Add(btnSaveProd);
            flpButtons.Controls.Add(btnNewProd);
            flpButtons.Controls.Add(btnDeleteProd);

            tlpInputs.SetColumnSpan(flpButtons, 6);
            tlpInputs.Controls.Add(flpButtons, 0, 2);

            pnlInputs.Controls.Add(tlpInputs);

            var pnlGrid = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(10));
            dgvProducts = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvProducts);

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "#", FillWeight = 10 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", DataPropertyName = "Name", HeaderText = "اسم الصنف / المادة", FillWeight = 38 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCat", HeaderText = "التصنيف", FillWeight = 20 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUnit", HeaderText = "الوحدة الأساسية", FillWeight = 14 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colInitialAmount", DataPropertyName = "InitialAmount", HeaderText = "الرصيد المتوفر", FillWeight = 14 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMinAmount", DataPropertyName = "MinAmount", HeaderText = "الحد الأدنى", FillWeight = 14 });

            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            pnlGrid.Controls.Add(dgvProducts);

            tab.Controls.Add(pnlGrid);
            tab.Controls.Add(pnlInputs);
        }

        private void InitOrdersTab(TabPage tab)
        {
            var pnlTopOrder = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(12));
            pnlTopOrder.Height = 160;

            var tlpOrder = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Date Label
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F)); // Date Picker
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); // Notes Label
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Notes Textbox
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));
            tlpOrder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));

            tlpOrder.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpOrder.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpOrder.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));

            // Row 0: Date and Notes
            var lblOD = new Label { Text = "تاريخ الطلب:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            dtOrderDate = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Font = ThemeManager.BodyFont };

            var lblON = new Label { Text = "ملاحظات الفاتورة:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtOrderNotes = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtOrderNotes);

            tlpOrder.Controls.Add(lblOD, 0, 0);
            tlpOrder.Controls.Add(dtOrderDate, 1, 0);
            tlpOrder.Controls.Add(lblON, 2, 0);
            tlpOrder.Controls.Add(txtOrderNotes, 3, 0);

            // Row 1: Add Item Toolbar
            var flpItemRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblP = new Label { Text = "الصنف:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            cmbOrderProd = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont, Margin = new Padding(4) };

            var lblQ = new Label { Text = "الكمية:", AutoSize = true, Margin = new Padding(6, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            numOrderQty = new NumericUpDown { Width = 90, Minimum = 1, Maximum = 99999, Value = 1, Font = ThemeManager.BodyFont, Margin = new Padding(4) };

            var lblPr = new Label { Text = "سعر الوحدة:", AutoSize = true, Margin = new Padding(6, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtOrderPrice = new TextBox { Width = 90, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            FormStyler.StyleInput(txtOrderPrice);

            btnAddOrderItem = new Button { Text = "➕ إضافة للفاتورة", Width = 140, Height = 32, Margin = new Padding(8, 2, 4, 2) };
            FormStyler.StyleButton(btnAddOrderItem, ButtonStyle.Primary);
            btnAddOrderItem.Click += BtnAddOrderItem_Click;

            flpItemRow.Controls.Add(lblP);
            flpItemRow.Controls.Add(cmbOrderProd);
            flpItemRow.Controls.Add(lblQ);
            flpItemRow.Controls.Add(numOrderQty);
            flpItemRow.Controls.Add(lblPr);
            flpItemRow.Controls.Add(txtOrderPrice);
            flpItemRow.Controls.Add(btnAddOrderItem);

            tlpOrder.SetColumnSpan(flpItemRow, 6);
            tlpOrder.Controls.Add(flpItemRow, 0, 1);

            // Row 2: Total & Save Order Button
            var flpBottomOrder = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnSaveOrder = new Button { Text = "💾 حفظ فاتورة الشراء والتوريد", Width = 230, Height = 36, Margin = new Padding(0, 4, 16, 0) };
            FormStyler.StyleButton(btnSaveOrder, ButtonStyle.Success);
            btnSaveOrder.Click += BtnSaveOrder_Click;

            lblOrderTotal = new Label
            {
                Text = "إجمالي الفاتورة: 0.00 ج.م",
                AutoSize = true,
                Font = ThemeManager.SubtitleFont,
                ForeColor = ThemeManager.PrimaryBlue,
                Margin = new Padding(8, 10, 8, 0)
            };

            flpBottomOrder.Controls.Add(btnSaveOrder);
            flpBottomOrder.Controls.Add(lblOrderTotal);

            tlpOrder.SetColumnSpan(flpBottomOrder, 6);
            tlpOrder.Controls.Add(flpBottomOrder, 0, 2);

            pnlTopOrder.Controls.Add(tlpOrder);

            // Split into: Current Order Items Grid & Historical Orders Grid
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 145,
                Padding = new Padding(4),
                RightToLeft = RightToLeft.Yes
            };

            var pnlTopSplit = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(8));
            var lblCurrentItemsTitle = new Label { Text = "أصناف الفاتورة الحالية قيد الإدخال", Dock = DockStyle.Top, Height = 24, Font = ThemeManager.BodyBoldFont, ForeColor = ThemeManager.PrimaryNavy };
            dgvOrderItems = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvOrderItems);
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colItemName", HeaderText = "الصنف", FillWeight = 45 });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colItemQty", HeaderText = "الكمية", FillWeight = 20 });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colItemPrice", HeaderText = "السعر", FillWeight = 20 });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colItemTotal", HeaderText = "الإجمالي", FillWeight = 25 });
            pnlTopSplit.Controls.Add(dgvOrderItems);
            pnlTopSplit.Controls.Add(lblCurrentItemsTitle);
            split.Panel1.Controls.Add(pnlTopSplit);

            var pnlBottomSplit = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(8));

            var pnlOrdersHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(0, 0, 0, 6),
                RightToLeft = RightToLeft.Yes
            };

            var lblHistoryTitle = new Label
            {
                Text = "سجل فواتير وطلبيات الشراء السابقة",
                Dock = DockStyle.Right,
                AutoSize = false,
                Width = 290,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.PrimaryNavy,
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleRight
            };

            var flpOrderHistoryActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0)
            };

            btnExportOrderPdf = new Button
            {
                Name = "btnExportOrderPdf",
                Text = "تصدير PDF",
                Width = 125,
                Height = 36,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnExportOrderPdf, ButtonStyle.Outline);
            btnExportOrderPdf.Click += BtnExportOrderPdf_Click;

            btnPrintOrder = new Button
            {
                Name = "btnPrintOrder",
                Text = "طباعة الفاتورة",
                Width = 135,
                Height = 36,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnPrintOrder, ButtonStyle.Success);
            btnPrintOrder.Click += (s, e) => OpenSelectedOrderDocument(ViewerLaunchMode.Print);

            btnViewOrder = new Button
            {
                Name = "btnViewOrder",
                Text = "عرض الفاتورة",
                Width = 130,
                Height = 36,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnViewOrder, ButtonStyle.Primary);
            btnViewOrder.Click += (s, e) => OpenSelectedOrderDocument(ViewerLaunchMode.Preview);

            flpOrderHistoryActions.Controls.Add(btnExportOrderPdf);
            flpOrderHistoryActions.Controls.Add(btnPrintOrder);
            flpOrderHistoryActions.Controls.Add(btnViewOrder);

            pnlOrdersHeader.Controls.Add(flpOrderHistoryActions);
            pnlOrdersHeader.Controls.Add(lblHistoryTitle);

            dgvOrders = new DataGridView { Name = "dgvOrders", Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvOrders);
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOrderId", DataPropertyName = "Id", HeaderText = "رقم الفاتورة", FillWeight = 15 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOrderDate", DataPropertyName = "OrderDate", HeaderText = "تاريخ التوريد", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalPrice", DataPropertyName = "TotalPrice", HeaderText = "إجمالي الفاتورة (ج.م)", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNotes", DataPropertyName = "Notes", HeaderText = "البيان / الملاحظات", FillWeight = 45 });
            if (dgvOrders.Columns["colOrderDate"] != null)
                dgvOrders.Columns["colOrderDate"].DefaultCellStyle.Format = "yyyy/MM/dd";
            dgvOrders.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0) OpenSelectedOrderDocument(ViewerLaunchMode.Preview);
            };
            pnlBottomSplit.Controls.Add(dgvOrders);
            pnlBottomSplit.Controls.Add(pnlOrdersHeader);
            split.Panel2.Controls.Add(pnlBottomSplit);

            tab.Controls.Add(split);
            tab.Controls.Add(pnlTopOrder);
        }

        private void LoadLookupData()
        {
            try
            {
                var units = _lookupCache.GetUnits();
                cmbUnits.DataSource = units;
                cmbUnits.DisplayMember = "Name";
                cmbUnits.ValueMember = "Id";

                var cats = _lookupCache.GetProductCategories();
                cmbCats.DataSource = cats;
                cmbCats.DisplayMember = "Name";
                cmbCats.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات الوحدات والتصنيفات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                _loadingOverlay.ShowOverlay("جاري تحميل قائمة الأصناف والمستلزمات...");
                var list = _inventoryService.GetProducts();
                dgvProducts.DataSource = null;
                dgvProducts.Rows.Clear();

                foreach (var p in list)
                {
                    int rIdx = dgvProducts.Rows.Add(p.Id, p.Name, p.ProductCategory?.Name ?? "-", p.BaseUnit?.Name ?? "-", p.InitialAmount, p.MinAmount);
                    dgvProducts.Rows[rIdx].Tag = p;

                    if (p.InitialAmount <= p.MinAmount)
                    {
                        dgvProducts.Rows[rIdx].DefaultCellStyle.BackColor = ThemeManager.DangerLight;
                        dgvProducts.Rows[rIdx].DefaultCellStyle.ForeColor = ThemeManager.Danger;
                    }
                }

                // Fill order dropdown
                cmbOrderProd.DataSource = list.ToList();
                cmbOrderProd.DisplayMember = "Name";
                cmbOrderProd.ValueMember = "Id";

                ResetProdInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء تحميل المنتجات:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private void ResetProdInputs()
        {
            _selectedProd = null;
            txtProdName.Clear();
            numInitial.Value = 0;
            numMin.Value = 2;
            if (cmbCats.Items.Count > 0) cmbCats.SelectedIndex = 0;
            if (cmbUnits.Items.Count > 0) cmbUnits.SelectedIndex = 0;
            btnSaveProd.Text = "💾 حفظ الصنف";
            FormStyler.StyleButton(btnSaveProd, ButtonStyle.Success);
        }

        private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.Tag is Product p)
            {
                _selectedProd = p;
                txtProdName.Text = p.Name;
                numInitial.Value = Math.Max(0, p.InitialAmount);
                numMin.Value = Math.Max(0, p.MinAmount);
                if (p.ProductCategoryId > 0) cmbCats.SelectedValue = p.ProductCategoryId;
                if (p.BaseUnitId > 0) cmbUnits.SelectedValue = p.BaseUnitId;
                btnSaveProd.Text = "✏️ تحديث الصنف";
                FormStyler.StyleButton(btnSaveProd, ButtonStyle.Primary);
            }
        }

        private void BtnSaveProd_Click(object? sender, EventArgs e)
        {
            string name = txtProdName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم الصنف.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProdName.Focus();
                return;
            }

            if (cmbCats.SelectedValue == null || cmbUnits.SelectedValue == null)
            {
                MessageBox.Show("يرجى تحديد التصنيف والوحدة الأساسية للصنف.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var prod = _selectedProd ?? new Product { CreateDate = DateTime.Now };
                prod.Name = name;
                prod.ProductCategoryId = (int)cmbCats.SelectedValue;
                prod.BaseUnitId = (int)cmbUnits.SelectedValue;
                prod.InitialAmount = (int)numInitial.Value;
                prod.MinAmount = (int)numMin.Value;

                _inventoryService.SaveProduct(prod);
                MessageBox.Show("تم حفظ بيانات الصنف بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الصنف:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteProd_Click(object? sender, EventArgs e)
        {
            if (_selectedProd == null)
            {
                MessageBox.Show("يرجى تحديد صنف لحذفه أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من حذف الصنف '{_selectedProd.Name}'؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _inventoryService.DeleteProduct(_selectedProd.Id);
                    MessageBox.Show("تم حذف الصنف بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnAddOrderItem_Click(object? sender, EventArgs e)
        {
            if (cmbOrderProd.SelectedItem is not Product p)
            {
                MessageBox.Show("يرجى اختيار الصنف المطلوب أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtOrderPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("يرجى إدخال سعر وحدة صحيح أكبر من أو يساوي الصفر.", "قيمة غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderPrice.Focus();
                return;
            }

            int qty = (int)numOrderQty.Value;
            decimal total = qty * price;

            _currentOrderItems.Add(Tuple.Create(p.Id, p.Name, qty, price));
            dgvOrderItems.Rows.Add(p.Name, qty, price.ToString("0.00"), total.ToString("0.00"));

            UpdateOrderTotal();
            txtOrderPrice.Clear();
            numOrderQty.Value = 1;
        }

        private void UpdateOrderTotal()
        {
            decimal sum = _currentOrderItems.Sum(i => i.Item3 * i.Item4);
            lblOrderTotal.Text = $"إجمالي الفاتورة: {sum:0.00} ج.م";
        }

        private void BtnSaveOrder_Click(object? sender, EventArgs e)
        {
            if (_currentOrderItems.Count == 0)
            {
                MessageBox.Show("يرجى إضافة أصناف إلى الفاتورة أولاً.", "فاتورة فارغة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var order = new Order
                {
                    OrderDate = dtOrderDate.Value,
                    Notes = string.IsNullOrWhiteSpace(txtOrderNotes.Text) ? null : txtOrderNotes.Text.Trim()
                };

                var details = _currentOrderItems.Select(i => new OrderDetail
                {
                    ProductId = i.Item1,
                    Quantity = i.Item3,
                    Price = i.Item4,
                    UnitId = (_inventoryService.GetProductById(i.Item1)?.BaseUnitId ?? 1),
                    CreateDate = DateTime.Now
                }).ToList();

                _inventoryService.SaveOrder(order, details);
                MessageBox.Show("تم تسجيل وحفظ فاتورة الشراء وتحديث الأرصدة بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _currentOrderItems.Clear();
                dgvOrderItems.Rows.Clear();
                txtOrderNotes.Clear();
                UpdateOrderTotal();

                LoadProducts();
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الفاتورة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _inventoryService.GetOrders();
                dgvOrders.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء تحميل فواتير الشراء: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Order? LoadSelectedOrderReadOnly()
        {
            var row = dgvOrders.CurrentRow ?? (dgvOrders.SelectedRows.Count > 0 ? dgvOrders.SelectedRows[0] : null);
            if (row == null)
            {
                MessageBox.Show("يرجى اختيار فاتورة شراء من الجدول أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            int orderId = 0;
            if (row.DataBoundItem is Order boundOrder)
                orderId = boundOrder.Id;
            else if (row.Cells["colOrderId"].Value != null)
                orderId = Convert.ToInt32(row.Cells["colOrderId"].Value);

            if (orderId <= 0)
            {
                MessageBox.Show("تعذر تحديد رقم الفاتورة المختارة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var order = _inventoryService.GetOrderById(orderId);
            if (order == null)
            {
                MessageBox.Show("لم يتم العثور على الفاتورة في قاعدة البيانات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return order;
        }

        private void OpenSelectedOrderDocument(ViewerLaunchMode mode)
        {
            var order = LoadSelectedOrderReadOnly();
            if (order == null) return;

            var doc = new PurchaseInvoiceDocument(order);
            using var viewer = new DocumentViewerForm(doc, $"فاتورة مشتريات #{order.Id}", mode);
            viewer.ShowDialog(this);
        }

        private void BtnExportOrderPdf_Click(object? sender, EventArgs e)
        {
            var order = LoadSelectedOrderReadOnly();
            if (order == null) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "ملف PDF (*.pdf)|*.pdf",
                FileName = $"فاتورة_مشتريات_{order.Id}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var doc = new PurchaseInvoiceDocument(order);
                byte[] pdfBytes = doc.GeneratePdfBytes();
                File.WriteAllBytes(sfd.FileName, pdfBytes);

                var openPrompt = MessageBox.Show(
                    "تم تصدير فاتورة المشتريات بصيغة PDF بنجاح.\n\nهل ترغب في فتح الملف الآن؟",
                    "نجاح التصدير",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (openPrompt == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo { FileName = sfd.FileName, UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تصدير ملف PDF:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
