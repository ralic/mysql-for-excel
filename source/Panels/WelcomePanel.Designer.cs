﻿namespace MySQL.ForExcel
{
  partial class WelcomePanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomePanel));
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Local Connections");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Remote Connections");
      this.picAddInLogo = new System.Windows.Forms.PictureBox();
      this.connectionsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.openConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.largeImages = new System.Windows.Forms.ImageList(this.components);
      this.smallImages = new System.Windows.Forms.ImageList(this.components);
      this.lblInstructions = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblAllRights = new System.Windows.Forms.Label();
      this.connectionList = new TreeViewTest.MyTreeView();
      this.openConnectionLabel = new MySQL.ForExcel.Controls.HotLabel();
      this.manageConnectionsLabel = new MySQL.ForExcel.Controls.HotLabel();
      this.newConnectionLabel = new MySQL.ForExcel.Controls.HotLabel();
      ((System.ComponentModel.ISupportInitialize)(this.picAddInLogo)).BeginInit();
      this.connectionsContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // picAddInLogo
      // 
      this.picAddInLogo.Image = global::MySQL.ForExcel.Properties.Resources.mysql_for_excel_header;
      this.picAddInLogo.Location = new System.Drawing.Point(14, 13);
      this.picAddInLogo.Name = "picAddInLogo";
      this.picAddInLogo.Size = new System.Drawing.Size(235, 68);
      this.picAddInLogo.TabIndex = 13;
      this.picAddInLogo.TabStop = false;
      // 
      // connectionsContextMenu
      // 
      this.connectionsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConnectionToolStripMenuItem});
      this.connectionsContextMenu.Name = "connectionsContextMenu";
      this.connectionsContextMenu.Size = new System.Drawing.Size(169, 26);
      // 
      // openConnectionToolStripMenuItem
      // 
      this.openConnectionToolStripMenuItem.Image = global::MySQL.ForExcel.Properties.Resources.db_mgmt_Connection_16x16;
      this.openConnectionToolStripMenuItem.Name = "openConnectionToolStripMenuItem";
      this.openConnectionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
      this.openConnectionToolStripMenuItem.Text = "Open Connection";
      // 
      // largeImages
      // 
      this.largeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImages.ImageStream")));
      this.largeImages.TransparentColor = System.Drawing.Color.Transparent;
      this.largeImages.Images.SetKeyName(0, "db.mgmt.Connection.32x32.png");
      // 
      // smallImages
      // 
      this.smallImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImages.ImageStream")));
      this.smallImages.TransparentColor = System.Drawing.Color.Transparent;
      this.smallImages.Images.SetKeyName(0, "db.mgmt.Connection.16x16.png");
      // 
      // lblInstructions
      // 
      this.lblInstructions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInstructions.ForeColor = System.Drawing.SystemColors.WindowText;
      this.lblInstructions.Location = new System.Drawing.Point(11, 83);
      this.lblInstructions.Name = "lblInstructions";
      this.lblInstructions.Size = new System.Drawing.Size(259, 83);
      this.lblInstructions.TabIndex = 2;
      this.lblInstructions.Text = "MySQL for Excel allows you to work with the MySQL Database right from within the " +
    "MS Office Excel application. Excel is a powerful tool for data analysis and edit" +
    "ing.";
      // 
      // lblCopyright
      // 
      this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblCopyright.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblCopyright.Location = new System.Drawing.Point(12, 574);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(261, 14);
      this.lblCopyright.TabIndex = 7;
      this.lblCopyright.Text = "Copyright © 2012 Oracle and/or its affiliates.";
      this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblAllRights
      // 
      this.lblAllRights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblAllRights.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAllRights.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblAllRights.Location = new System.Drawing.Point(12, 588);
      this.lblAllRights.Name = "lblAllRights";
      this.lblAllRights.Size = new System.Drawing.Size(261, 14);
      this.lblAllRights.TabIndex = 8;
      this.lblAllRights.Text = "All rights reserved.";
      this.lblAllRights.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // connectionList
      // 
      this.connectionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.connectionList.CollapsedIcon = global::MySQL.ForExcel.Properties.Resources.ArrowRight;
      this.connectionList.DescriptionColor = System.Drawing.Color.Silver;
      this.connectionList.DescriptionFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connectionList.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
      this.connectionList.ExpandedIcon = global::MySQL.ForExcel.Properties.Resources.ArrowDown;
      this.connectionList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connectionList.Indent = 18;
      this.connectionList.ItemHeight = 20;
      this.connectionList.Location = new System.Drawing.Point(14, 203);
      this.connectionList.Name = "connectionList";
      this.connectionList.NodeImages = this.largeImages;
      treeNode1.BackColor = System.Drawing.SystemColors.ControlDark;
      treeNode1.ForeColor = System.Drawing.SystemColors.WindowText;
      treeNode1.Name = "LocalConnectionsNode";
      treeNode1.Text = "Local Connections";
      treeNode2.BackColor = System.Drawing.SystemColors.ControlDark;
      treeNode2.ForeColor = System.Drawing.SystemColors.WindowText;
      treeNode2.Name = "Node0";
      treeNode2.Text = "Remote Connections";
      this.connectionList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
      this.connectionList.Size = new System.Drawing.Size(265, 264);
      this.connectionList.TabIndex = 22;
      this.connectionList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.connectionList_NodeMouseDoubleClick);
      // 
      // openConnectionLabel
      // 
      this.openConnectionLabel.Description = "Double-Click a Connection to Start";
      this.openConnectionLabel.DescriptionFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.openConnectionLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.openConnectionLabel.HotTracking = false;
      this.openConnectionLabel.Image = global::MySQL.ForExcel.Properties.Resources.MySQLforExcel_WelcomePanel_Connection_32x32;
      this.openConnectionLabel.ImageSize = new System.Drawing.Size(32, 32);
      this.openConnectionLabel.Location = new System.Drawing.Point(14, 152);
      this.openConnectionLabel.Margin = new System.Windows.Forms.Padding(4);
      this.openConnectionLabel.Name = "openConnectionLabel";
      this.openConnectionLabel.Size = new System.Drawing.Size(256, 44);
      this.openConnectionLabel.TabIndex = 20;
      this.openConnectionLabel.Title = "Open a MySQL Connection";
      // 
      // manageConnectionsLabel
      // 
      this.manageConnectionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.manageConnectionsLabel.Description = "Launch MySQL Workbench";
      this.manageConnectionsLabel.DescriptionFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.manageConnectionsLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.manageConnectionsLabel.HotTracking = true;
      this.manageConnectionsLabel.Image = global::MySQL.ForExcel.Properties.Resources.MySQLforExcel_WelcomePanel_ManageConnection_32x32;
      this.manageConnectionsLabel.ImageSize = new System.Drawing.Size(32, 32);
      this.manageConnectionsLabel.Location = new System.Drawing.Point(14, 526);
      this.manageConnectionsLabel.Margin = new System.Windows.Forms.Padding(4);
      this.manageConnectionsLabel.Name = "manageConnectionsLabel";
      this.manageConnectionsLabel.Size = new System.Drawing.Size(222, 44);
      this.manageConnectionsLabel.TabIndex = 16;
      this.manageConnectionsLabel.Title = "Manage Connections";
      this.manageConnectionsLabel.Click += new System.EventHandler(this.manageConnectionsLabel_Click);
      // 
      // newConnectionLabel
      // 
      this.newConnectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.newConnectionLabel.Description = "Add a new Database Connection";
      this.newConnectionLabel.DescriptionFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.newConnectionLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.newConnectionLabel.HotTracking = true;
      this.newConnectionLabel.Image = global::MySQL.ForExcel.Properties.Resources.MySQLforExcel_WelcomePanel_NewConnection_32x32;
      this.newConnectionLabel.ImageSize = new System.Drawing.Size(32, 32);
      this.newConnectionLabel.Location = new System.Drawing.Point(14, 474);
      this.newConnectionLabel.Margin = new System.Windows.Forms.Padding(4);
      this.newConnectionLabel.Name = "newConnectionLabel";
      this.newConnectionLabel.Size = new System.Drawing.Size(222, 44);
      this.newConnectionLabel.TabIndex = 15;
      this.newConnectionLabel.Title = "New Connection";
      this.newConnectionLabel.Click += new System.EventHandler(this.newConnectionLabel_Click);
      // 
      // WelcomePanel
      // 
      this.Controls.Add(this.connectionList);
      this.Controls.Add(this.openConnectionLabel);
      this.Controls.Add(this.manageConnectionsLabel);
      this.Controls.Add(this.lblInstructions);
      this.Controls.Add(this.newConnectionLabel);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.picAddInLogo);
      this.Controls.Add(this.lblAllRights);
      this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "WelcomePanel";
      this.Size = new System.Drawing.Size(290, 610);
      ((System.ComponentModel.ISupportInitialize)(this.picAddInLogo)).EndInit();
      this.connectionsContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox picAddInLogo;
    private System.Windows.Forms.ImageList smallImages;
    private System.Windows.Forms.ImageList largeImages;
    private System.Windows.Forms.Label lblInstructions;
    private System.Windows.Forms.ContextMenuStrip connectionsContextMenu;
    private System.Windows.Forms.ToolStripMenuItem openConnectionToolStripMenuItem;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.Label lblAllRights;
    private Controls.HotLabel newConnectionLabel;
    private Controls.HotLabel manageConnectionsLabel;
    private Controls.HotLabel openConnectionLabel;
    private TreeViewTest.MyTreeView connectionList;
  }
}
