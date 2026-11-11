using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LabManagementSystem.Common
{
    public class NavSubItem
    {
        public string Key { get; }
        public string Text { get; }
        public Button Button { get; }
        public Action OnClick { get; }
        public NavGroup ParentGroup { get; }

        public NavSubItem(string key, string text, Action onClick, NavGroup parentGroup)
        {
            Key = key;
            Text = text;
            OnClick = onClick;
            ParentGroup = parentGroup;

            Button = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 38,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#94A3B8"),
                BackColor = ThemeManager.SidebarBackground,
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 36, 0),
                Cursor = Cursors.Hand
            };
            Button.FlatAppearance.BorderSize = 0;
            Button.FlatAppearance.MouseOverBackColor = ThemeManager.SidebarHover;

            Button.Click += (s, e) =>
            {
                ParentGroup.Accordion.SetActiveSubItem(this);
                OnClick();
            };
        }
    }

    public class NavGroup
    {
        public string Key { get; }
        public string Title { get; }
        public string Icon { get; }
        public NavigationAccordion Accordion { get; }

        public Button HeaderButton { get; }
        public Label ArrowLabel { get; }
        public Panel ItemsPanel { get; }
        public List<NavSubItem> SubItems { get; } = new();

        public bool IsExpanded => ItemsPanel.Visible;

        public NavGroup(string key, string title, string icon, NavigationAccordion accordion)
        {
            Key = key;
            Title = title;
            Icon = icon;
            Accordion = accordion;

            HeaderButton = new Button
            {
                Text = $"{icon}  {title}",
                Dock = DockStyle.Top,
                Height = 44,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#F8FAFC"),
                BackColor = ColorTranslator.FromHtml("#172554"), // Dark blue group header
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 16, 0),
                Cursor = Cursors.Hand
            };
            HeaderButton.FlatAppearance.BorderSize = 0;
            HeaderButton.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1E3A8A");

            ArrowLabel = new Label
            {
                Text = "◀",
                Dock = DockStyle.Left,
                Width = 28,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = ColorTranslator.FromHtml("#94A3B8"),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            HeaderButton.Controls.Add(ArrowLabel);
            ArrowLabel.Click += (s, e) => Toggle();

            HeaderButton.Click += (s, e) => Toggle();

            ItemsPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = ThemeManager.SidebarBackground,
                Visible = false,
                Padding = new Padding(0)
            };
        }

        public NavSubItem AddSubItem(string key, string text, Action onClick)
        {
            var item = new NavSubItem(key, text, onClick, this);
            SubItems.Add(item);
            return item;
        }

        public void Expand()
        {
            if (!ItemsPanel.Visible)
            {
                ItemsPanel.Visible = true;
                ArrowLabel.Text = "▼";
                ArrowLabel.ForeColor = Color.White;
            }
        }

        public void Collapse()
        {
            if (ItemsPanel.Visible)
            {
                ItemsPanel.Visible = false;
                ArrowLabel.Text = "◀";
                ArrowLabel.ForeColor = ColorTranslator.FromHtml("#94A3B8");
            }
        }

        public void Toggle()
        {
            if (IsExpanded)
                Collapse();
            else
                Expand();
        }
    }

    public class NavigationAccordion
    {
        private readonly Panel _sidebarContainer;
        private readonly List<NavGroup> _groups = new();
        private NavSubItem? _activeSubItem;

        public IReadOnlyList<NavGroup> Groups => _groups;

        public NavigationAccordion(Panel sidebarContainer)
        {
            _sidebarContainer = sidebarContainer;
            _sidebarContainer.AutoScroll = true;
            _sidebarContainer.RightToLeft = RightToLeft.Yes;
        }

        public NavGroup AddGroup(string key, string title, string icon)
        {
            var group = new NavGroup(key, title, icon, this);
            _groups.Add(group);
            return group;
        }

        public void Render()
        {
            _sidebarContainer.SuspendLayout();
            _sidebarContainer.Controls.Clear();

            // DockStyle.Top: To display groups from top to bottom (Group 1, Group 2, ...),
            // we must add them to Controls in REVERSE order.
            for (int i = _groups.Count - 1; i >= 0; i--)
            {
                var grp = _groups[i];

                // In ItemsPanel, add child items in reverse order so first item is at top
                grp.ItemsPanel.Controls.Clear();
                for (int j = grp.SubItems.Count - 1; j >= 0; j--)
                {
                    grp.ItemsPanel.Controls.Add(grp.SubItems[j].Button);
                }

                _sidebarContainer.Controls.Add(grp.ItemsPanel);
                _sidebarContainer.Controls.Add(grp.HeaderButton);
            }

            _sidebarContainer.ResumeLayout(true);
        }

        public void SetActiveSubItem(NavSubItem item)
        {
            if (_activeSubItem != null && _activeSubItem != item)
            {
                _activeSubItem.Button.BackColor = ThemeManager.SidebarBackground;
                _activeSubItem.Button.ForeColor = ColorTranslator.FromHtml("#94A3B8");
                _activeSubItem.Button.Font = new Font(ThemeManager.PreferredFontFamily, 9.5f, FontStyle.Regular);
            }

            _activeSubItem = item;
            _activeSubItem.Button.BackColor = ThemeManager.SidebarActive;
            _activeSubItem.Button.ForeColor = Color.White;
            _activeSubItem.Button.Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Bold);
            _activeSubItem.Button.RightToLeft = RightToLeft.No;
            _activeSubItem.Button.TextAlign = ContentAlignment.MiddleRight;

            // Ensure parent group is expanded
            item.ParentGroup.Expand();
        }

        public void ClearSelection()
        {
            if (_activeSubItem != null)
            {
                _activeSubItem.Button.BackColor = ThemeManager.SidebarBackground;
                _activeSubItem.Button.ForeColor = ColorTranslator.FromHtml("#94A3B8");
                _activeSubItem.Button.Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Regular);
                _activeSubItem = null;
            }

            foreach (var grp in _groups)
            {
                grp.Collapse();
            }
        }

        public void SelectItemByKey(string itemKey)
        {
            foreach (var grp in _groups)
            {
                var subItem = grp.SubItems.FirstOrDefault(s => s.Key.Equals(itemKey, StringComparison.OrdinalIgnoreCase));
                if (subItem != null)
                {
                    SetActiveSubItem(subItem);
                    break;
                }
            }
        }
    }
}
