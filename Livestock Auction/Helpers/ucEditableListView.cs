using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction.Helpers
{
    public partial class EditableListView : ListView
    {
        public class EditableListViewItemCollection : ListView.ListViewItemCollection
        {
            EditableListView parentListView = null;
            public event EventHandler<FocusEventArgs> LostFocus = null;
            public event EventHandler<EditableItemCollectionEventArgs> ItemAdded = null;
            public event EventHandler<EditableItemCollectionEventArgs> ItemRemoved = null;

            public EditableListViewItemCollection(EditableListView owner)
                : base(owner)
            {
                parentListView = owner;
            }

            public EditableListViewItem Add(EditableListViewItem value)
            {
                // Connect the focus up and focus down events before adding
                value.SubItems.FocusDown += SubItems_FocusDown;
                value.SubItems.FocusUp += SubItems_FocusUp;
                value.SubItems.FocusMove += SubItems_FocusMove;
                base.Add(value);

                //Keep the template at the end of the list if it's visible
                if (parentListView.lviTemplate != null && value != parentListView.lviTemplate && this.Contains(parentListView.lviTemplate) && parentListView.lviTemplate.Index != (this.Count - 1))
                {
                    this.Remove(parentListView.lviTemplate);
                    parentListView.lviTemplate.Remove();
                    try
                    {
                        base.Add(parentListView.lviTemplate);
                    }
                    catch (Exception)
                    {
                        return value;
                    }
                }

                onItemAdded(new EditableItemCollectionEventArgs(value));

                return value;
            }

            public override ListViewItem Add(ListViewItem value)
            {
                onItemAdded(new EditableItemCollectionEventArgs(value));

                return base.Add((EditableListViewItem)value);
            }

            public new EditableListViewItem Add(string text)
            {
                EditableListViewItem value = new EditableListViewItem(text);
                return this.Add(value);
            }

            public override void Remove(ListViewItem item)
            {
                this.Remove((EditableListViewItem)item);
            }

            public void Remove(EditableListViewItem item)
            {
                item.SubItems.FocusDown -= SubItems_FocusDown;
                item.SubItems.FocusUp -= SubItems_FocusUp;

                onItemRemoved(new EditableItemCollectionEventArgs(item));
                base.Remove(item);
            }

            public override void Clear()
            {
                this.parentListView.Controls.Clear();
                base.Clear();
            }

            void SubItems_FocusMove(object sender, EditableListViewItem.EditableListViewSubItemCollection.FocusMoveEventArgs e)
            {
                MoveFocus(this.IndexOf(e.Row), e.Column);
            }

            public void MoveFocus(int Row, int Column)
            {
                Control newctrl = null;

                if (Row >= 0 && Row < this.Count)
                {
                    if (Column < 0)
                    {
                        Column = 0;
                    }
                    else if (Column > this[Row].SubItems.Count)
                    {
                        Column = this[Row].SubItems.Count - 1;
                    }

                    //Move focus to the listview control
                    if (!this.parentListView.Focused)
                    {
                        this.parentListView.Focus();
                    }

                    this[Row].EnsureVisible();

                    //Focus on the row
                    if (this.parentListView.SelectedItems.Count == 0 || this.parentListView.SelectedItems[0] != this[Row])
                    {
                        //If there is only one item selected, and it is not the item to be selected, deselect it
                        if (this.parentListView.SelectedItems.Count == 1)
                        {
                            this.parentListView.SelectedItems[0].Selected = false;
                        }
                        this[Row].Selected = true;
                        this[Row].Focused = true;
                    }

                    //Focus on the column
                    parentListView.Controls.AddRange(((EditableListViewItem)this[Row]).GetControls());
                    newctrl = ((EditableListViewItem.EditableListViewSubItem)this[Row].SubItems[Column]).GetControl();
                    if (newctrl != null)
                    {
                        newctrl.Visible = true;
                        newctrl.Focus();
                    }
                }
            }

            void SubItems_FocusUp(object sender, EditableListViewItem.EditableListViewSubItemCollection.FocusMoveEventArgs e)
            {
                Control newctrl = null;
                int iRow = this.IndexOf(e.Row);
                if (iRow > 0)
                {
                    this[iRow].Selected = false;
                    this[iRow - 1].Selected = true;
                    this[iRow - 1].Focused = true;
                    
                    parentListView.Controls.AddRange(((EditableListViewItem)this[iRow - 1]).GetControls());
                    newctrl = ((EditableListViewItem.EditableListViewSubItem)this[iRow - 1].SubItems[e.Column]).GetControl();
                }
                if (newctrl != null)
                {
                    newctrl.Visible = true;
                    newctrl.Focus();
                }
                else if (e.TabAdvance)
                {
                    parentListView.Parent.SelectNextControl(parentListView, false, true, false, true);
                }
            }

            void SubItems_FocusDown(object sender, EditableListViewItem.EditableListViewSubItemCollection.FocusMoveEventArgs e)
            {
                Control newctrl = null;
                int iRow = this.IndexOf(e.Row);
                if (iRow >= 0 && iRow < this.Count - 1)
                {
                    //Move the focus to the next item on the list
                    this[iRow].Selected = false;
                    this[iRow + 1].Selected = true;
                    this[iRow + 1].Focused = true;
                    this[iRow + 1].EnsureVisible();

                    parentListView.Controls.AddRange(((EditableListViewItem)this[iRow + 1]).GetControls());
                    newctrl = ((EditableListViewItem.EditableListViewSubItem)this[iRow + 1].SubItems[e.Column]).GetControl();
                }
                else if (parentListView.lviTemplate != null)
                {
                    //There are no more items, show the template if it is not already there
                    if (parentListView.lviTemplate.Selected && parentListView.lviTemplate.Edited)
                    {
                        //The template was edited, hide it (which will add the edited record and remove the template so it will get readded below)
                        parentListView.lviTemplate.Selected = false;
                        parentListView.lviTemplate.Focused = false;
                        parentListView.HideTemplate();
                    }

                    if (!this.Contains(parentListView.lviTemplate))
                    {
                        //The template is not on the list (either it was removed above, or wasn't there to begin with)
                        parentListView.ShowTemplate();
                        this[iRow].Selected = false;
                        parentListView.lviTemplate.Selected = true;
                        parentListView.lviTemplate.Focused = true;
                        parentListView.lviTemplate.EnsureVisible();
                        parentListView.Controls.AddRange(parentListView.lviTemplate.GetControls());
                        newctrl = ((EditableListViewItem.EditableListViewSubItem)parentListView.lviTemplate.SubItems[0]).GetControl();
                    }
                }

                if (newctrl != null)
                {
                    newctrl.Visible = true;
                    newctrl.Focus();
                }
                else if (e.TabAdvance)
                {
                    parentListView.Parent.SelectNextControl(parentListView, true, true, false, true);
                    parentListView.OnLostFocus((EventArgs)e);
                }
            }

            protected void onItemAdded(EditableItemCollectionEventArgs e)
            {
                if (ItemAdded != null)
                {
                    ItemAdded(this, e);
                }
            }

            protected void onItemRemoved(EditableItemCollectionEventArgs e)
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, e);
                }
            }
        }

        EditableListViewItemCollection lstItemsCollection = null;
        EditableListViewItem lviTemplate = null;

        public EditableListView()
        {
            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.OwnerDraw = false;

            lstItemsCollection = new EditableListViewItemCollection(this);
        }

        public void HideTemplate()
        {
            if (lviTemplate != null)
            {
                if (this.Items.Contains(lviTemplate))
                {
                    this.Items.Remove(lviTemplate);

                    if (lviTemplate.Edited)
                    {
                        this.Items.Add(lviTemplate);
                        this.lviTemplate = lviTemplate.Clone();
                    }
                }
            }
        }

        public void ShowTemplate()
        {
            if (lviTemplate != null)
            {
                if (!this.Items.Contains(lviTemplate))
                {
                    this.Items.Add(lviTemplate);
                }
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
            base.OnDrawItem(e);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (this.ContainsFocus && e.Item.Focused && e.Item.Selected)
            {
                Control ctrl = ((EditableListViewItem.EditableListViewSubItem)e.SubItem).GetControl();
                if (ctrl != null)
                {
                    if (!this.Controls.Contains(ctrl))
                    {
                        this.Controls.Add(ctrl);
                    }
                    ctrl.SetBounds(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    ctrl.Visible = true;
                }
                else
                {
                    e.DrawDefault = true;
                }
            }
            else
            {
                //The item does not have focus, clear the controls
                e.DrawDefault = true;
                Control ctrl = ((EditableListViewItem.EditableListViewSubItem)e.SubItem).GetControl();
                if (ctrl != null)
                {
                    ctrl.Visible = false;
                    this.Controls.Remove(ctrl);
                }
            }

            base.OnDrawSubItem(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            //Make sure the controls associated with the selected listview item are added to the listview's control array
            if (this.SelectedItems.Count > 0)
            {
                foreach (EditableListViewItem Item in this.Items)
                {
                    Control[] controls = Item.GetControls();
                    if (Item.Selected && Item.Focused)
                    {
                        Item.EnsureVisible();
                        this.Controls.AddRange(controls);
                        controls[0].Visible = true;
                        controls[controls.Length - 1].Visible = true;
                        foreach (EditableListViewItem.EditableListViewSubItem SubItem in Item.SubItems)
                        {
                            Control ctrl = SubItem.GetControl();
                            if (ctrl != null)
                            {
                                ctrl.SetBounds(SubItem.Bounds.X, SubItem.Bounds.Y, SubItem.Bounds.Width, SubItem.Bounds.Height);
                                ctrl.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        this.Controls.Remove(controls[0]);
                        this.Controls.Remove(controls[controls.Length - 1]);
                    }
                }

                if (lviTemplate != null && !lviTemplate.Selected)
                {
                    HideTemplate();
                }
            }
            else
            {
                if (this.ContainsFocus)
                {
                    ShowTemplate();
                }
            
            }
            this.Refresh();

            base.OnSelectedIndexChanged(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.SelectedItems.Count == 0)
            {
                ShowTemplate();
            }    
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Refresh();
            if (!this.ContainsFocus)
            {
                HideTemplate();
            }
            base.OnLostFocus(e);
        }

        public EditableListViewItem Template
        {
            get
            {
                return lviTemplate;
            }
            set
            {
                lviTemplate = value;
            }
        }
        
        public new EditableListViewItemCollection Items
        {
            get
            {
                return lstItemsCollection;
            }
        }
    }

    public class FocusEventArgs : EventArgs
    {
        bool bTabPressed;

        public FocusEventArgs(bool TabPressed)
        {
            bTabPressed = TabPressed;
        }

        public bool TabPressed
        {
            get
            {
                return bTabPressed;
            }
        }
    }

    public class EditableItemCollectionEventArgs : EventArgs
    {
        ListViewItem m_Item;

        public EditableItemCollectionEventArgs(ListViewItem Item)
        {
            m_Item = Item;
        }

        public ListViewItem Item
        {
            get
            {
                return m_Item;
            }
        }
    }

    public class EditableListViewColumn : ColumnHeader
    {
    }

    public class EditableListViewItem : ListViewItem
    {
        #region Editable List View Item Support Classes
        public class EditableListViewSubItemCollection : ListViewItem.ListViewSubItemCollection
        {
            public class FocusMoveEventArgs : EventArgs
            {
                int iColumn;
                EditableListViewItem lviRow;
                bool bTabAdvance;

                public FocusMoveEventArgs(EditableListViewItem row, int column, bool tabadvance)
                {
                    lviRow = row;
                    iColumn = column;
                    bTabAdvance = tabadvance;
                }

                public EditableListViewItem Row
                {
                    get
                    {
                        return lviRow;
                    }
                }

                public int Column
                {
                    get
                    {
                        return iColumn;
                    }
                }

                public bool TabAdvance
                {
                    get
                    {
                        return bTabAdvance;
                    }
                }
            }

            EditableListViewItem parentItem = null;
            Button cmdFocusFodderStart = null;
            Button cmdFocusFodderEnd = null;
            public event EventHandler<FocusMoveEventArgs> LostFocus = null;
            public event EventHandler<FocusMoveEventArgs> FocusDown = null;
            public event EventHandler<FocusMoveEventArgs> FocusUp = null;
            public event EventHandler<FocusMoveEventArgs> FocusMove = null;

            public EditableListViewSubItemCollection(EditableListViewItem owner)
                : base(owner)
            {
                parentItem = owner;
                cmdFocusFodderStart = new Button();
                cmdFocusFodderStart.Width = 0;
                cmdFocusFodderStart.Height = 0;
                cmdFocusFodderStart.Top = -100;
                cmdFocusFodderStart.Left = -100;
                cmdFocusFodderStart.GotFocus += FocusFodder_GotFocus;
                
                cmdFocusFodderEnd = new Button();
                cmdFocusFodderEnd.Width = 0;
                cmdFocusFodderEnd.Height = 0;
                cmdFocusFodderEnd.Top = -100;
                cmdFocusFodderEnd.Left = -100;
                cmdFocusFodderEnd.GotFocus += FocusFodder_GotFocus;
            }

            public EditableListViewSubItem Add(EditableListViewSubItem subitem)
            {
                subitem.FocusDown += subitem_FocusDown;
                subitem.FocusUp += subitem_FocusUp;
                subitem.LostFocus += subitem_LostFocus;
                subitem.FocusMove += subitem_FocusMove;
                subitem.ValueChanged += subitem_ValueChanged;
                return (EditableListViewSubItem)(base.Add(subitem));
            }

            public new EditableListViewSubItem Add(string text)
            {
                EditableListViewSubItem subitem = new EditableListViewSubItem(parentItem, text);
                subitem.FocusDown += subitem_FocusDown;
                subitem.FocusUp += subitem_FocusUp;
                subitem.LostFocus += subitem_LostFocus;
                subitem.FocusMove += subitem_FocusMove;
                subitem.ValueChanged += subitem_ValueChanged;
                return (EditableListViewSubItem)base.Add(subitem);
            }

            public new EditableListViewSubItem Add(string text, object value)
            {
                EditableListViewSubItem subitem = new EditableListViewSubItem(parentItem, text, value);
                subitem.FocusDown += subitem_FocusDown;
                subitem.FocusUp += subitem_FocusUp;
                subitem.LostFocus += subitem_LostFocus;
                subitem.FocusMove += subitem_FocusMove;
                subitem.ValueChanged += subitem_ValueChanged;
                return (EditableListViewSubItem)base.Add(subitem);
            }

            public void AddRange(EditableListViewSubItem[] subitems)
            {
                foreach (EditableListViewSubItem subitem in subitems)
                {
                    subitem.FocusDown += subitem_FocusDown;
                    subitem.FocusUp += subitem_FocusUp;
                    subitem.LostFocus += subitem_LostFocus;
                    this.Add(subitem);
                }
            }

            protected void OnLostFocus(FocusMoveEventArgs e)
            {
                if (LostFocus != null)
                {
                    LostFocus(this, e);
                }
            }

            protected void OnFocusDown(FocusMoveEventArgs e)
            {
                if (FocusDown != null)
                {
                    FocusDown(this, e);
                }
            }

            protected void OnFocusUp(FocusMoveEventArgs e)
            {
                if (FocusUp != null)
                {
                    FocusUp(this, e);
                }
            }

            protected void OnFocusMove(FocusMoveEventArgs e)
            {
                if (FocusMove != null)
                {
                    FocusMove(this, e);
                }
            }

            void FocusFodder_GotFocus(object sender, EventArgs e)
            {
                if (sender == cmdFocusFodderStart)
                {
                    OnFocusUp(new FocusMoveEventArgs(parentItem, this.Count - 1, true));

                }
                else if (sender == cmdFocusFodderEnd)
                {
                    OnFocusDown(new FocusMoveEventArgs(parentItem, 0, true));
                }
            }

            void subitem_FocusDown(object sender, FocusEventArgs e)
            {
                OnFocusDown(new FocusMoveEventArgs(parentItem, this.IndexOf((ListViewSubItem)sender), false));
            }

            void subitem_FocusUp(object sender, FocusEventArgs e)
            {
                OnFocusUp(new FocusMoveEventArgs(parentItem, this.IndexOf((ListViewSubItem)sender), false));
            }

            void subitem_LostFocus(object sender, FocusEventArgs e)
            {
                OnLostFocus(new FocusMoveEventArgs(parentItem, this.IndexOf((ListViewSubItem)sender), false));
            }

            void subitem_ValueChanged(object sender, EventArgs e)
            {
                parentItem.OnItemChanged(new ItemEventArgs((EditableListViewSubItem)sender));
            }

            void subitem_FocusMove(object sender, FocusEventArgs e)
            {
                OnFocusMove(new FocusMoveEventArgs(parentItem, this.IndexOf((ListViewSubItem)sender), false));
            }

            public EditableListViewSubItem this[int index]
            {
                get
                {
                    return (EditableListViewSubItem)base[index];
                }
                set
                {
                    base[index] = value;
                }
            }

            public Control FocusFodderStart
            {
                get
                {
                    return cmdFocusFodderStart;
                }
            }

            public Control FocusFodderEnd
            {
                get
                {
                    return cmdFocusFodderEnd;
                }
            }
        }

        public class ItemEventArgs : EventArgs
        {
            EditableListViewSubItem itemchanged;

            public ItemEventArgs(EditableListViewSubItem itemChanged)
            {
                itemchanged = itemChanged;
            }

            public EditableListViewSubItem SubitemChanged
            {
                get
                {
                    return itemchanged;
                }
            }
        }

        #region EditableListViewSubItem sub classes
        public class EditableListViewSubItem : ListViewItem.ListViewSubItem
        {
            public event EventHandler<EventArgs> TextChanged = null;
            public event EventHandler<FocusEventArgs> GotFocus = null;
            public event EventHandler<FocusEventArgs> LostFocus = null;
            public event EventHandler<FocusEventArgs> FocusDown = null;
            public event EventHandler<FocusEventArgs> FocusUp = null;
            public event EventHandler<FocusEventArgs> FocusMove = null;
            public event EventHandler<EventArgs> ValueChanged = null;

            protected string sInitialText = null;
            protected bool bValueSet = false;
            protected object oOriginalValue = null;

            bool bTabState = false;
            bool bKeyPressed = false;

            public EditableListViewSubItem()
                : base()
            {
                this.Init(null);
            }

            public EditableListViewSubItem(string text)
                : base()
            {
                this.Init(text);
            }

            public EditableListViewSubItem(string text, object value)
                : base()
            {
                this.Init(text);
                this.Value = value;
            }

            public EditableListViewSubItem(EditableListViewItem owner, string text)
                : base(owner, text)
            {
                this.Init(text);
            }

            public EditableListViewSubItem(EditableListViewItem owner, string text, object value)
                : base(owner, text)
            {
                this.Init(text);
                this.Value = value;
            }

            protected virtual void Init(string InitalText)
            {
                sInitialText = InitalText;
                bValueSet = false;
                this.Text = InitalText;
                this.Font = new Font(this.Font, FontStyle.Italic);
                this.ForeColor = System.Drawing.Color.LightGray;   
            }

            public virtual void Reset()
            {
                this.Text = sInitialText;
                bValueSet = false;
                this.Font = new Font(this.Font, FontStyle.Italic);
                this.ForeColor = System.Drawing.Color.LightGray;
            }

            protected void control_KeyUp(object sender, KeyEventArgs e)
            {
                if (bKeyPressed)
                {
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    {
                        OnFocusDown(new EventArgs());
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        OnFocusUp(new EventArgs());
                    }
                    else if (e.KeyCode == Keys.Tab)
                    {
                        bTabState = false;
                    }
                }
                bKeyPressed = false;
            }

            protected void control_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    bTabState = true;
                }
                bKeyPressed = true;
            }

            protected void OnTextChanged(EventArgs e)
            {
                if (TextChanged != null)
                {
                    TextChanged(this, e);
                }
            }

            protected void OnGotFocus(EventArgs e)
            {
                this.oOriginalValue = this.Value;

                if (GotFocus != null)
                {
                    GotFocus(this, new FocusEventArgs(bTabState));
                }
            }

            protected void OnLostFocus(EventArgs e)
            {
                if ((this.oOriginalValue == null) != (this.Value == null))
                {
                    this.bValueSet = true;
                }
                else if (this.oOriginalValue != null && !this.oOriginalValue.Equals(this.Value))
                {
                    this.bValueSet = true;
                }

                if (this.bValueSet)
                {
                    this.Font = new Font(this.Font, FontStyle.Regular);
                    this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                }

                if (LostFocus != null)
                {
                    LostFocus(this, new FocusEventArgs(bTabState));
                }
            }

            protected void OnFocusDown(EventArgs e)
            {
                if (FocusDown != null)
                {
                    FocusDown(this, new FocusEventArgs(bTabState));
                }
            }

            protected void OnFocusUp(EventArgs e)
            {
                if (FocusUp != null)
                {
                    FocusUp(this, new FocusEventArgs(bTabState));
                }
            }

            protected virtual void OnFocusMove(FocusEventArgs e)
            {
                if (FocusMove != null)
                {
                    FocusMove(this, e);
                }
            }

            protected void OnValueChanged(EventArgs e)
            {
                if (ValueChanged != null)
                {
                    ValueChanged(this, e);
                }
            }

            public void Focus()
            {
                OnFocusMove(new FocusEventArgs(bTabState));
                GetControl().Focus();
            }

            public virtual Control GetControl()
            {
                return null;
            }

            public bool ValueSet
            {
                get
                {
                    return bValueSet;
                }
            }

            public virtual object Value
            {
                get
                {
                    return null;
                }
                set
                {
                    if (this.GetControl() == null || !this.GetControl().ContainsFocus)
                    {
                        this.oOriginalValue = value;
                        bValueSet = true;
                        if (bValueSet && value != null)
                        {
                            this.Font = new Font(this.Font, FontStyle.Regular);
                            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                        }
                        if (this.GetControl() == null)
                        {
                            this.Text = value.ToString();
                        }
                    }
                    
                    if (value == null)
                    {
                        Reset();
                    }

                    OnValueChanged(new EventArgs());
                }
            }
        }

        public class EditableTextBoxListViewSubItem : EditableListViewSubItem
        {
            TextBox txtValueTextBox;
            bool bNewFocus = false;
            string sFormatString = "";

            public EditableTextBoxListViewSubItem()
                : base()
            { }

            public EditableTextBoxListViewSubItem(string text)
                : base(text)
            { }

            public EditableTextBoxListViewSubItem(string text, object value)
                : base(text, value)
            { }

            public EditableTextBoxListViewSubItem(EditableListViewItem owner, string text)
                : base(owner, text)
            { }

            public EditableTextBoxListViewSubItem(EditableListViewItem owner, string text, object value)
                : base(owner, text, value)
            { }

            protected override void Init(string InitalText)
            {
                txtValueTextBox = new TextBox();
                txtValueTextBox.Visible = false;
                txtValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                txtValueTextBox.TextChanged += txtValueTextBox_TextChanged;
                txtValueTextBox.KeyDown += base.control_KeyDown;
                txtValueTextBox.KeyUp += base.control_KeyUp;
                txtValueTextBox.GotFocus += txtValueTextBox_GotFocus;
                txtValueTextBox.Click += txtValueTextBox_Click;
                txtValueTextBox.LostFocus += txtValueTextBox_LostFocus;

                base.Init(InitalText);
            }

            public override void Reset()
            {
                base.Reset();
                txtValueTextBox.TextChanged -= txtValueTextBox_TextChanged;
                txtValueTextBox.Text = "";
                txtValueTextBox.TextChanged += txtValueTextBox_TextChanged;
            }

            void txtValueTextBox_Click(object sender, EventArgs e)
            {
                if (bNewFocus)
                {
                    txtValueTextBox.SelectAll();
                    bNewFocus = false;
                }
            }

            void txtValueTextBox_GotFocus(object sender, EventArgs e)
            {
                //It looks like clicking on the box immediately sets the cursor, so select all never really gets focus.
                //  Set a flag to indicate that the text box just got focus, so when it is clicked on, all text can be selected
                bNewFocus = true;
                txtValueTextBox.SelectAll();
                base.OnGotFocus(e);
            }

            void txtValueTextBox_LostFocus(object sender, EventArgs e)
            {
                base.OnLostFocus(e);
            }

            void txtValueTextBox_TextChanged(object sender, EventArgs e)
            {
                this.Text = txtValueTextBox.Text;
                this.Value = txtValueTextBox.Text;
                base.OnTextChanged(new EventArgs());
            }

            public override Control GetControl()
            {
                return txtValueTextBox;
            }

            public string FormatString
            {
                get
                {
                    return sFormatString;
                }
                set
                {
                    sFormatString = value;
                }
            }

            public override object Value
            {
                get
                {
                    if (!this.bValueSet && txtValueTextBox.Text == "")
                    {
                        return null;
                    }
                    else
                    {
                        return txtValueTextBox.Text;
                    }
                }
                set
                {
                    if (value != null)
                    {
                        txtValueTextBox.Text = value.ToString();
                        if (sFormatString.Length > 0)
                        {
                            try
                            {
                                this.Text = double.Parse(value.ToString()).ToString(sFormatString);
                            }
                            catch (Exception ex)
                            {
                                this.Text = value.ToString();
                            }
                        }
                        else
                        {
                            this.Text = value.ToString();
                        }
                        base.Value = value;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
            

            public TextBox TextBox
            {
                get
                {
                    return txtValueTextBox;
                }
            }

        }

        public class EditableRadioListViewSubItem : EditableListViewSubItem
        {
            List<RadioButton> lstRadioButtons;
            Panel panRootControl;

            string sCurrentText;
            int iCurrentValue;

            public EditableRadioListViewSubItem()
                : base()
            { }

            public EditableRadioListViewSubItem(string text)
                : base(text)
            { }

            public EditableRadioListViewSubItem(string text, object value)
                : base(text, value)
            { }

            public EditableRadioListViewSubItem(EditableListViewItem owner, string text)
                : base(owner, text)
            { }

            public EditableRadioListViewSubItem(EditableListViewItem owner, string text, object value)
                : base(owner, text, value)
            { }

            protected override void Init(string InitalText)
            {
                panRootControl = new Panel();
                panRootControl.Visible = false;

                lstRadioButtons = new List<RadioButton>();

                sCurrentText = InitalText;
                panRootControl.Resize += panRootControl_Resize;
                panRootControl.KeyDown += base.control_KeyDown;
                panRootControl.KeyUp += base.control_KeyUp;
                panRootControl.LostFocus += panRootControl_LostFocus;
                panRootControl.TabStop = true;

                base.Init(InitalText);
            }

            public override void Reset()
            {
                base.Reset();
                foreach (RadioButton rad in lstRadioButtons)
                {
                    rad.Checked = false;
                }
            }

            void panRootControl_Resize(object sender, EventArgs e)
            {
                Realign();
            }

            void panRootControl_GotFocus(object sender, EventArgs e)
            {
                base.OnGotFocus(e);
                if (lstRadioButtons.Count > 0)
                {
                    lstRadioButtons[0].Focus();
                }
            }

            void panRootControl_LostFocus(object sender, EventArgs e)
            {
                if (!panRootControl.ContainsFocus)
                {
                    base.OnLostFocus(e);
                }
            }

            public override Control GetControl()
            {
                return panRootControl;
            }

            public EditableRadioListViewSubItem Add(string Caption)
            {
                RadioButton radNew = new RadioButton();
                radNew.Text = Caption;
                radNew.Padding = new Padding(0);
                radNew.Visible = true;
                radNew.AutoSize = false;
                radNew.CheckedChanged += radNew_CheckedChanged;
                radNew.KeyUp += base.control_KeyUp;
                radNew.KeyDown += base.control_KeyDown;
                radNew.LostFocus += radNew_LostFocus;
                radNew.TabIndex = lstRadioButtons.Count;
                radNew.TabStop = true;
                panRootControl.Controls.Add(radNew);
                lstRadioButtons.Add(radNew);
                if (iCurrentValue == (lstRadioButtons.Count - 1))
                {
                    radNew.Checked = true;
                }


                Realign();

                return this;
            }

            void radNew_CheckedChanged(object sender, EventArgs e)
            {
                if (((RadioButton)sender).Checked)
                {
                    int iSelIndex = lstRadioButtons.IndexOf((RadioButton)sender);
                    if (iSelIndex > 0)
                    {
                        this.Value = iSelIndex;
                    }
                }
            }

            void radNew_LostFocus(object sender, EventArgs e)
            {
                if (!panRootControl.ContainsFocus)
                {
                    base.OnLostFocus(e);
                }
            }

            private void Realign()
            {
                int iPos = 0;
                int iSpace = panRootControl.Width / lstRadioButtons.Count;
                foreach (RadioButton rad in lstRadioButtons)
                {
                    rad.Left = iPos;
                    rad.Width = iSpace;
                    rad.Height = panRootControl.Height;
                    rad.TextAlign = ContentAlignment.MiddleLeft;
                    rad.CheckAlign = ContentAlignment.MiddleLeft;
                    iPos += iSpace;
                }
            }

            public new string Text
            {
                get
                {
                    return base.Text;
                }
                set
                {
                    base.Text = value;
                }
            }

            public override object Value
            {
                get
                {
                    if (!this.bValueSet && iCurrentValue == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return iCurrentValue;
                    }
                }
                set
                {
                    if (value != null)
                    {
                        iCurrentValue = (int)value;
                        if (iCurrentValue < lstRadioButtons.Count)
                        {
                            lstRadioButtons[iCurrentValue].Checked = true;
                            this.Text = lstRadioButtons[iCurrentValue].Text;
                        }

                        base.Value = iCurrentValue;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        public class EditableCheckBoxListViewSubItem : EditableListViewSubItem
        {
            CheckBox chkCheckBox;
            bool bCurValue = false;

            public EditableCheckBoxListViewSubItem()
                : base()
            { }

            public EditableCheckBoxListViewSubItem(string text)
                : base(text)
            { }

            public EditableCheckBoxListViewSubItem(string text, object value)
                : base(text, value)
            {
            }

            public EditableCheckBoxListViewSubItem(EditableListViewItem owner, string text)
                : base(owner, text)
            { }

            public EditableCheckBoxListViewSubItem(EditableListViewItem owner, string text, object value)
                : base(owner, text, value)
            {
            }

            protected override void Init(string InitalText)
            {
                chkCheckBox = new CheckBox();
                chkCheckBox.Visible = false;
                chkCheckBox.Text = InitalText;
                chkCheckBox.AutoSize = false;
                this.Text = "";

                base.Init(InitalText);

                chkCheckBox.CheckedChanged += chkCheckBox_CheckedChanged;
                chkCheckBox.KeyDown += base.control_KeyDown;
                chkCheckBox.KeyUp += base.control_KeyUp;
                chkCheckBox.GotFocus += chkCheckBox_GotFocus;
                chkCheckBox.LostFocus += chkCheckBox_LostFocus;
            }

            public override void Reset()
            {
                base.Reset();
                chkCheckBox.Checked = false;
            }

            void chkCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (chkCheckBox.Checked)
                {
                    this.Text = chkCheckBox.Text;
                    this.Value = true;
                }
                else
                {
                    this.Text = "";
                    this.Value = false;
                }
            }

            public override Control GetControl()
            {
                return chkCheckBox;
            }

            void chkCheckBox_GotFocus(object sender, EventArgs e)
            {
                base.OnGotFocus(e);
            }

            void chkCheckBox_LostFocus(object sender, EventArgs e)
            {
                base.OnLostFocus(e);
            }

            public override object Value
            {
                get
                {
                    if (!this.bValueSet && !this.bCurValue)
                    {
                        return null;
                    }
                    else
                    {
                        return bCurValue;
                    }
                }
                set
                {
                    if (value != null)
                    {
                        bCurValue = (bool)value;
                        chkCheckBox.Checked = bCurValue;
                        base.Value = bCurValue;
                        if (bCurValue)
                        {
                            this.Text = chkCheckBox.Text;
                        }
                        else
                        {
                            this.Text = "";
                        }
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        public class EditableButtonListViewSubItem : EditableListViewSubItem
        {
            public event EventHandler<EventArgs> Click = null;

            LinkLabel cmdButton;

            public EditableButtonListViewSubItem()
                : base()
            { }

            public EditableButtonListViewSubItem(string text)
                : base(text)
            { }

            public EditableButtonListViewSubItem(string text, object value)
                : base(text, value)
            { }

            public EditableButtonListViewSubItem(EditableListViewItem owner, string text)
                : base(owner, text)
            { }

            public EditableButtonListViewSubItem(EditableListViewItem owner, string text, object value)
                : base(owner, text, value)
            { }

            protected override void Init(string InitalText)
            {
                cmdButton = new LinkLabel();

                cmdButton.Text = InitalText;
                cmdButton.Padding = new Padding(0);

                base.Init(InitalText);

                cmdButton.Click += cmdButton_Click;
                cmdButton.KeyUp += cmdButton_KeyUp;
                cmdButton.KeyDown += base.control_KeyDown;
                cmdButton.KeyUp += base.control_KeyUp;
                cmdButton.GotFocus += cmdButton_GotFocus;
                cmdButton.LostFocus += cmdButton_LostFocus;
            }

            public override Control GetControl()
            {
                return cmdButton;
            }

            protected void OnClick(EventArgs e)
            {
                if (Click != null)
                {
                    Click(this, e);
                }
            }

            void cmdButton_Click(object sender, EventArgs e)
            {
                OnClick(e);
            }

            void cmdButton_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    OnClick(e);
                }
            }

            void cmdButton_GotFocus(object sender, EventArgs e)
            {
                base.OnGotFocus(e);
            }

            void cmdButton_LostFocus(object sender, EventArgs e)
            {
                base.OnLostFocus(e);
            }

            public override object Value
            {
                get
                {
                    return null;
                }
                set
                {
                    if (value == null)
                    {
                        Reset();
                    }
                }
            }
        }
        #endregion
        #endregion

        EditableListViewSubItemCollection lstSubItems = null;
        protected object[] ConstructorArgs;
        public event EventHandler<ItemEventArgs> ItemChanged;

        public EditableListViewItem()
            : base("")
        {
            lstSubItems = new EditableListViewSubItemCollection(this);
            this.SubItems[0] = new EditableListViewSubItem(this, "");
            this.UseItemStyleForSubItems = false;

            ConstructorArgs = new object[] { };
        }

        public EditableListViewItem(string text)
            : base(text)
        {
            lstSubItems = new EditableListViewSubItemCollection(this);
            this.SubItems[0] = new EditableListViewSubItem(this, text);
            this.UseItemStyleForSubItems = false;

            ConstructorArgs = new object[] { text };
        }

        public void Reset()
        {
            foreach (EditableListViewSubItem SubItem in this.SubItems)
            {
                SubItem.Value = null;
            }
        }

        public new EditableListViewItem Clone()
        {
            Type[] ArgsTypes = new Type[ConstructorArgs.Length];
            int i = 0;
            foreach (object arg in ConstructorArgs)
            {
                ArgsTypes[i] = arg.GetType();
                i++;
            }

            System.Reflection.ConstructorInfo LVIConstructor = this.GetType().GetConstructor(ArgsTypes);
            EditableListViewItem ClonedItem = (EditableListViewItem)LVIConstructor.Invoke(ConstructorArgs);
            return ClonedItem;
        }

        public Control[] GetControls()
        {
            List<Control> lstSubControls = new List<Control>();
            int iCurTabIndex = 0;
            lstSubControls.Add(lstSubItems.FocusFodderStart);
            foreach (EditableListViewSubItem SubItem in this.SubItems)
            {
                if (SubItem.GetControl() != null)
                {
                    Control ctrl = SubItem.GetControl();
                    ctrl.TabIndex = iCurTabIndex;
                    iCurTabIndex++;
                    lstSubControls.Add(ctrl);
                }
            }
            lstSubControls.Add(lstSubItems.FocusFodderEnd);
            return lstSubControls.ToArray();
        }

        protected virtual void OnItemChanged(ItemEventArgs e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, e);
            }
        }
        
        public new EditableListViewSubItemCollection SubItems
        {
            get
            {
                return lstSubItems;
            }
        }

        public bool Edited
        {
            get
            {
                foreach (EditableListViewSubItem item in this.SubItems)
                {
                    if (item.ValueSet)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    #region EditableListViewItem sub classes...
    //These exist to give control over the 0th sub item that is created when you first initalize a ListViewItem
    public class EditableTextBoxListViewItem : EditableListViewItem
    {
        public EditableTextBoxListViewItem()
            : base()
        {
            base.SubItems[0] = new EditableTextBoxListViewSubItem();
        }

        public EditableTextBoxListViewItem(string text)
            : base(text)
        {
            base.SubItems[0] = new EditableTextBoxListViewSubItem(this, text);
        }

        public EditableTextBoxListViewItem(string text, object value)
            : base(text)
        {
            base.SubItems[0] = new EditableTextBoxListViewSubItem(this, text, value);
        }
    }

    public class EditableRadioListViewItem : EditableListViewItem
    {
        public EditableRadioListViewItem()
            : base()
        {
            base.SubItems[0] = new EditableRadioListViewSubItem();
        }

        public EditableRadioListViewItem(string text)
            : base(text)
        {
            base.SubItems[0] = new EditableRadioListViewSubItem(this, text);
        }

        public EditableRadioListViewItem(string text, object value)
            : base(text)
        {
            base.SubItems[0] = new EditableRadioListViewSubItem(this, text, value);
        }
    }

    public class EditableCheckBoxListViewItem : EditableListViewItem
    {
        public EditableCheckBoxListViewItem()
            : base()
        {
            base.SubItems[0] = new EditableCheckBoxListViewSubItem();
        }

        public EditableCheckBoxListViewItem(string text)
            : base(text)
        {
            base.SubItems[0] = new EditableCheckBoxListViewSubItem(this, text);
        }

        public EditableCheckBoxListViewItem(string text, object value)
            : base(text)
        {
            base.SubItems[0] = new EditableCheckBoxListViewSubItem(this, text, value);
        }
    }
    #endregion
}
