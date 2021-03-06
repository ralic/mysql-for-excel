﻿// Copyright (c) 2012, 2015, Oracle and/or its affiliates. All rights reserved.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License as
// published by the Free Software Foundation; version 2 of the
// License.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
// 02110-1301  USA

namespace MySQL.ForExcel.Controls
{
  sealed partial class SearchEdit
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing"><c>true</c> if managed resources should be disposed; otherwise, <c>false</c>.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.InnerTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // InnerTextBox
      // 
      this.InnerTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.InnerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.InnerTextBox.Font = new System.Drawing.Font("Arial", 9F);
      this.InnerTextBox.Location = new System.Drawing.Point(38, 0);
      this.InnerTextBox.Name = "InnerTextBox";
      this.InnerTextBox.Size = new System.Drawing.Size(311, 14);
      this.InnerTextBox.TabIndex = 1;
      this.InnerTextBox.Enter += new System.EventHandler(this.InnerTextBox_Enter);
      this.InnerTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InnerTextBox_KeyDown);
      this.InnerTextBox.Leave += new System.EventHandler(this.InnerTextBox_Leave);
      // 
      // SearchEdit
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.InnerTextBox);
      this.Name = "SearchEdit";
      this.Size = new System.Drawing.Size(349, 15);
      this.Resize += new System.EventHandler(this.SearchEdit_Resize);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox InnerTextBox;

  }
}
