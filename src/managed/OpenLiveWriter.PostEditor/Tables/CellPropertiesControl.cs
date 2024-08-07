// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OpenLiveWriter.CoreServices.Layout;
using OpenLiveWriter.Localization;
using OpenLiveWriter.CoreServices;

namespace OpenLiveWriter.PostEditor.Tables
{
    /// <summary>
    /// Summary description for TableAppearanceControl.
    /// </summary>
    public partial class CellPropertiesControl : System.Windows.Forms.UserControl
    {
        public CellPropertiesControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            this.groupBoxCells.Text = Res.Get(StringId.Cells);
        }

        public void AdjustSize()
        {
            using (new AutoGrow(this, AnchorStyles.Right, true))
            {
                using (new AutoGrow(groupBoxCells, AnchorStyles.Right, false))
                {
                    TableAlignmentControl.AdjustSizes(verticalAlignmentControl, horizontalAlignmentControl);
                    verticalAlignmentControl.NaturalizeHeight();
                    horizontalAlignmentControl.NaturalizeHeight();
                    DisplayHelper.AutoFitSystemCheckBox(checkBoxIsHeader, 0, int.MaxValue);
                }
            }

        }

        public override string Text
        {
            get
            {
                return groupBoxCells.Text;
            }
            set
            {
                groupBoxCells.Text = value;
            }
        }

        public CellProperties CellProperties
        {
            get
            {
                CellProperties cellProperties = new CellProperties();
                cellProperties.BackgroundColor = _cellColor;
                cellProperties.HorizontalAlignment = horizontalAlignmentControl.HorizontalAlignment;
                cellProperties.VerticalAlignment = verticalAlignmentControl.VerticalAlignment;
                cellProperties.IsHeader = CheckStateToNullableBool(checkBoxIsHeader.CheckState);
                return cellProperties;
            }
            set
            {
                // null == default
                CellProperties cellProperties = value;
                if (cellProperties == null)
                    cellProperties = new CellProperties();

                // set values
                _cellColor = cellProperties.BackgroundColor;
                horizontalAlignmentControl.HorizontalAlignment = cellProperties.HorizontalAlignment;
                verticalAlignmentControl.VerticalAlignment = cellProperties.VerticalAlignment;
                checkBoxIsHeader.CheckState = NullableBoolToCheckState(cellProperties.IsHeader);
            }
        }

        public bool IsHeaderCheckBoxThreeState
        {
            get
            {
                return checkBoxIsHeader.ThreeState;
            }
            set
            {
                checkBoxIsHeader.ThreeState = value;
            }
        }

        private CheckState NullableBoolToCheckState(bool? value)
        {
            if(!value.HasValue)
            {
                return CheckState.Indeterminate;
            }
            else if(value.Value)
            {
                return CheckState.Checked;
            }

            return CheckState.Unchecked;
        }

        private bool? CheckStateToNullableBool(CheckState checkState)
        {
            switch(checkState)
            {
                case CheckState.Checked:
                    return true;
                case CheckState.Unchecked:
                    return false;
            }

            return null;
        }

        // NOTE: right now we don't support editing of cell color so we just have a member
        // variable that tracks the value which was set -- we simply return this same
        // value when the caller does a get
        private CellColor _cellColor;
    }
}
