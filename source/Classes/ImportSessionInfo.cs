﻿// Copyright (c) 2014, Oracle and/or its affiliates. All rights reserved.
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

using System.Diagnostics;
using System;
using System.Xml.Serialization;
using MySQL.ForExcel.Classes.Exceptions;
using MySQL.ForExcel.Interfaces;
using MySQL.ForExcel.Properties;
using MySQL.Utility.Classes;
using MySQL.Utility.Classes.MySQLWorkbench;
using ExcelInterop = Microsoft.Office.Interop.Excel;
using ExcelTools = Microsoft.Office.Tools.Excel;

namespace MySQL.ForExcel.Classes
{
  /// <summary>
  /// This class stores all the information required by an import Session to be stored in disk, able to be reopened if excel is closed and restarted without closing the session.
  /// </summary>
  [Serializable]
  public class ImportSessionInfo : ISessionInfo
  {
    #region Fields

    /// <summary>
    /// The workbench connection object the session works with.
    /// </summary>
    private MySqlWorkbenchConnection _connection;

    /// <summary>
    /// The connection identifier the session works with.
    /// </summary>
    private string _connectionId;

    /// <summary>
    /// Flag indicating whether the <seealso cref="Dispose"/> method has already been called.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// The <see cref="ExcelInterop.ListObject"/> object related to the import session.
    /// </summary>
    private ExcelInterop.ListObject _excelTable;

    /// <summary>
    /// The Excel table name.
    /// </summary>
    private string _excelTableName;

    /// <summary>
    /// The name of the schema the connection works with.
    /// </summary>
    private string _schemaName;

    #endregion Fields

    /// <summary>
    /// DO NOT REMOVE. Default constructor required for serialization-deserialization.
    /// </summary>
    public ImportSessionInfo()
    {
      _connection = null;
      _connectionId = null;
      _excelTable = null;
      _excelTableName = string.Empty;
      _schemaName = string.Empty;
      SessionError = SessionErrorType.None;
      LastAccess = DateTime.Now;
      MySqlTable = null;
      ToolsExcelTable = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImportSessionInfo" /> class.
    /// </summary>
    /// <param name="mySqlTable">MySqlDataTable object related to the import session.</param>
    /// <param name="atCell">The top left Excel cell of the new <see cref="ExcelInterop.ListObject"/>.</param>
    /// <param name="addSummaryRow">Flag indicating whether to include a row with summary fields at the end of the data rows.</param>
    public ImportSessionInfo(MySqlDataTable mySqlTable, ExcelInterop.Range atCell, bool addSummaryRow)
      : this()
    {
      if (mySqlTable == null)
      {
        throw new ArgumentNullException("mySqlTable");
      }

      _connection = mySqlTable.WbConnection;
      MySqlTable = mySqlTable;
      SchemaName = mySqlTable.SchemaName;
      TableName = mySqlTable.TableName;
      ConnectionId = mySqlTable.WbConnection.Id;
      ImportColumnNames = mySqlTable.ImportColumnNames;
      SelectQuery = mySqlTable.SelectQuery;
      WorkbookGuid = Globals.ThisAddIn.Application.ActiveWorkbook.GetOrCreateId();
      WorkbookName = Globals.ThisAddIn.Application.ActiveWorkbook.Name;
      WorkbookFilePath = Globals.ThisAddIn.Application.ActiveWorkbook.FullName;
      ExcelInterop.Worksheet worksheet = Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet;
      WorksheetName = worksheet.Name;
      InitializeConnectionObjects(atCell, addSummaryRow);
    }

    #region Properties

    /// <summary>
    /// Gets or sets the connection identifier the session works with.
    /// </summary>
    [XmlAttribute]
    public string ConnectionId
    {
      get
      {
        return _connectionId;
      }

      set
      {
        _connectionId = value;
        if (string.IsNullOrEmpty(_connectionId))
        {
          return;
        }

        _connection = MySqlWorkbench.Connections.GetConnectionForId(ConnectionId);
        if (_connection == null)
        {
          SessionError = SessionErrorType.WorkbenchConnectionDoesNotExist;
        }
        else
        {
          _connection.Schema = SchemaName;
          _connection.AllowZeroDateTimeValues = true;
          HostIdentifier = _connection.HostIdentifier;
        }
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="ExcelInterop.ListObject"/> object related to the import session.
    /// </summary>
    [XmlIgnore]
    public ExcelInterop.ListObject ExcelTable
    {
      get
      {
        return _excelTable;
      }

      set
      {
        _excelTable = value;
        if (_excelTable == null)
        {
          return;
        }

        _excelTableName = _excelTable.Name;
        ToolsExcelTable = Globals.Factory.GetVstoObject(_excelTable);
      }
    }

    /// <summary>
    /// Gets or sets the Excel table name.
    /// </summary>
    [XmlAttribute]
    public string ExcelTableName
    {
      get
      {
        return _excelTableName;
      }

      set
      {
        if (_excelTable == null)
        {
          _excelTableName = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the host identifier.
    /// </summary>
    [XmlAttribute]
    public string HostIdentifier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [import column names].
    /// </summary>
    [XmlAttribute]
    public bool ImportColumnNames { get; set; }

    /// <summary>
    /// Gets or sets the last date and time the session was saved.
    /// </summary>
    [XmlAttribute]
    public DateTime LastAccess { get; set; }

    /// <summary>
    /// Gets or sets MySQL table for the import session.
    /// </summary>
    [XmlIgnore]
    public MySqlDataTable MySqlTable { get; private set; }

    /// <summary>
    /// Gets or sets the name of the schema the connection works with.
    /// </summary>
    [XmlAttribute]
    public string SchemaName
    {
      get
      {
        return _schemaName;
      }

      set
      {
        _schemaName = value;
        if (_connection == null)
        {
          return;
        }

        _connection.Schema = _schemaName;
      }
    }

    /// <summary>
    /// Gets or sets the query to re-generate the contents of the MySqldataTable the session is based on.
    /// </summary>
    [XmlAttribute]
    public string SelectQuery { get; set; }

    /// <summary>
    /// Gets or sets a session error identifier.
    /// </summary>
    [XmlAttribute]
    public SessionErrorType SessionError { get; set; }

    /// <summary>
    /// Gets or sets the table name the connection works with.
    /// </summary>
    [XmlAttribute]
    public string TableName { get; set; }

    /// <summary>
    /// Gets or sets the table name the connection works with.
    /// </summary>
    [XmlIgnore]
    public ExcelTools.ListObject ToolsExcelTable { get; private set; }

    /// <summary>
    /// Gets or sets the workbook full path name.
    /// </summary>
    [XmlAttribute]
    public string WorkbookFilePath { get; set; }

    /// <summary>
    /// Gets or sets the workbook guid on excel the session is making the import.
    /// </summary>
    [XmlAttribute]
    public string WorkbookGuid { get; set; }

    /// <summary>
    /// Gets or sets the name of the worbook.
    /// </summary>
    [XmlAttribute]
    public string WorkbookName { get; set; }

    /// <summary>
    /// Gets or sets the name of active worksheet.
    /// </summary>
    [XmlAttribute]
    public string WorksheetName { get; set; }

    #endregion Properties

    #region Enums

    /// <summary>
    /// This Enumeration is used to mark the error type the session presented when tried to refresh.
    /// </summary>
    [FlagsAttribute]
    public enum SessionErrorType
    {
      /// <summary>
      /// The import session is working correctly.
      /// </summary>
      None = 0,

      /// <summary>
      /// The workbench connection was deleted and no longer exists.
      /// </summary>
      WorkbenchConnectionDoesNotExist = 1,

      /// <summary>
      /// The connection refused the current credentials or no password is provided.
      /// </summary>
      ConnectionRefused = 2,

      /// <summary>
      /// The schema was deleted from the database and no longer exists.
      /// </summary>
      SchemaNoLongerExists = 4,

      /// <summary>
      /// The table was deleted from the schema and longer exists.
      /// </summary>
      TableNoLongerExists = 8,

      /// <summary>
      /// The excel table no longer exists, the session is no longer valid and would be deleted.
      /// </summary>
      ExcelTableNoLongerExists = 16,
    }

    #endregion Enums

    /// <summary>
    /// Releases all resources used by the <see cref="ImportSessionInfo"/> class
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Refreshes the Import Session non serializable objects and specified cells on the excel table.
    /// </summary>
    public void Refresh()
    {
      if (MySqlTable == null || ToolsExcelTable == null)
      {
        return;
      }

      // Test the connection before attempting the data refresh.
      if (!TestConnection())
      {
        if (SessionError == SessionErrorType.WorkbenchConnectionDoesNotExist)
        {
          MySqlSourceTrace.WriteToLog(string.Format("Session for Excel table '{0}.{1}.{2}' on was removed since the connection no longer exists.", WorkbookName, WorksheetName, ExcelTableName), SourceLevels.Warning);
          Globals.ThisAddIn.StoredImportSessions.Remove(this);
        }

        return;
      }

      try
      {
        // In case the table is bound (it should not be) then disconnect it.
        if (ToolsExcelTable.IsBinding)
        {
          ToolsExcelTable.Disconnect();
        }

        // Refresh the data on the MySqlDataTable and bind it so the Excel table is refreshed.
        Exception ex;
        MySqlTable.RefreshData(out ex);

        // Resize the ExcelTools.ListObject by giving it an ExcelInterop.Range calculated with the refreshed MySqlDataTable dimensions.
        // Detection of a collision with another Excel object must be performed first and if any then shift rows and columns to fix the collision.
        ExcelInterop.Range newRange = ToolsExcelTable.Range.Cells[1, 1];
        newRange = newRange.Resize[MySqlTable.Rows.Count + 1, MySqlTable.Columns.Count];
        var intersectingRange = newRange.GetIntersectingRangeWithAnyExcelObject(true, true, true, _excelTable.Comment);
        if (intersectingRange != null && intersectingRange.CountLarge != 0)
        {
          ExcelInterop.Range bottomRightCell = newRange.Cells[newRange.Rows.Count, newRange.Columns.Count];

          // Determine if the collision is avoided by inserting either new columns or new rows.
          if (intersectingRange.Columns.Count < intersectingRange.Rows.Count)
          {
            for (int colIdx = 0; colIdx <= intersectingRange.Columns.Count; colIdx++)
            {
              bottomRightCell.EntireColumn.Insert(ExcelInterop.XlInsertShiftDirection.xlShiftToRight, Type.Missing);
            }
          }
          else
          {
            for (int rowIdx = 0; rowIdx <= intersectingRange.Rows.Count; rowIdx++)
            {
              bottomRightCell.EntireRow.Insert(ExcelInterop.XlInsertShiftDirection.xlShiftDown, Type.Missing);
            }
          }

          // Redimension the new range. This is needed since the new rows or columns inserted are not present in the previously calculated one.
          newRange = ToolsExcelTable.Range.Cells[1, 1];
          newRange = newRange.Resize[MySqlTable.Rows.Count + 1, MySqlTable.Columns.Count];
        }

        ToolsExcelTable.Resize(newRange);

        // Bind the redimensioned ExcelTools.ListObject to the MySqlDataTable.
        ToolsExcelTable.SetDataBinding(MySqlTable);
        if (MySqlTable.ImportColumnNames)
        {
          foreach (MySqlDataColumn col in MySqlTable.Columns)
          {
            ToolsExcelTable.ListColumns[col.Ordinal + 1].Name = col.DisplayName;
          }
        }

        ToolsExcelTable.Range.Columns.AutoFit();

        // Disconnect the table so users can freely modify the data imported to the Excel table's range.
        ToolsExcelTable.Disconnect();
      }
      catch (Exception ex)
      {
        MiscUtilities.ShowCustomizedErrorDialog(string.Format(Resources.RefreshDataError, _excelTableName), ex.Message, true);
        MySqlSourceTrace.WriteAppErrorToLog(ex);
      }
    }

    /// <summary>
    /// Restores the internal session objects.
    /// </summary>
    /// <param name="workbook">The <see cref="ExcelInterop.Workbook"/> tied to the session.</param>
    public void Restore(ExcelInterop.Workbook workbook)
    {
      if (workbook == null || workbook.GetOrCreateId() != WorkbookGuid)
      {
        return;
      }

      if (_excelTable == null && !string.IsNullOrEmpty(_excelTableName))
      {
        ExcelTable = workbook.GetExcelTableByName(WorksheetName, _excelTableName);
        if (ExcelTable == null)
        {
          return;
        }
      }

      if (MySqlTable == null)
      {
        if (_connection != null)
        {
          MySqlTable = _connection.CreateImportMySqlTable(false, TableName, ImportColumnNames, SelectQuery);
        }
        else
        {
          SessionError = SessionErrorType.WorkbenchConnectionDoesNotExist;
        }
      }
    }

    /// <summary>
    /// Tests the import session connection.
    /// </summary>
    /// <returns><c>true</c> if all connection parameters are valid to stablish the connection.</returns>
    public bool TestConnection()
    {
      if (_connection == null)
      {
        SessionError = SessionErrorType.WorkbenchConnectionDoesNotExist;
        return false;
      }

      Exception connectionException;
      bool connectionIsValid = _connection.TestConnection(out connectionException);
      if (connectionException != null)
      {
        SessionError = SessionErrorType.ConnectionRefused;
      }

      return connectionIsValid;
    }

    /// <summary>
    /// Releases all resources used by the <see cref="ImportSessionInfo"/> class
    /// </summary>
    /// <param name="disposing">If true this is called by Dispose(), otherwise it is called by the finalizer</param>
    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      // Free managed resources
      if (disposing)
      {
        if (ToolsExcelTable != null)
        {
          if (ToolsExcelTable.IsBinding)
          {
            ToolsExcelTable.Disconnect();
          }

          ToolsExcelTable.DeleteSafely(false);
        }

        if (MySqlTable != null)
        {
          MySqlTable.Dispose();
        }

        // Set variables to null so this object does not hold references to them and the GC disposes of them sooner.
        _connection = null;
        MySqlTable = null;
        ExcelTable = null;
        ToolsExcelTable = null;
      }

      // Add class finalizer if unmanaged resources are added to the class
      // Free unmanaged resources if there are any
      _disposed = true;
    }

    /// <summary>
    /// Creates an Excel table starting at the given cell containing the data in a <see cref="MySqlDataTable"/> instance.
    /// </summary>
    /// <param name="importDataAtCell">The top left Excel cell of the new <see cref="ExcelInterop.ListObject"/>.</param>
    /// <param name="addSummaryRow">Flag indicating whether to include a row with summary fields at the end of the data rows.</param>
    private void InitializeConnectionObjects(ExcelInterop.Range importDataAtCell, bool addSummaryRow)
    {
      if (importDataAtCell == null)
      {
        throw new ArgumentNullException("importDataAtCell");
      }

      var worksheet = Globals.Factory.GetVstoObject(importDataAtCell.Worksheet);
      var workbook = worksheet.Parent as ExcelInterop.Workbook;
      if (workbook == null)
      {
        throw new ParentWorkbookNullException(worksheet.Name);
      }

      string workbookGuid = workbook.GetOrCreateId();
      try
      {
        // Create the Excel table needed to place the imported data into the Excel worksheet.
        CreateExcelTableFromExternalSource(worksheet, importDataAtCell, addSummaryRow);

        // Fetch the MySQL data and bind the MySqlDataTable object to the Excel table.
        Refresh();

        // Add this instance of the ImportSessionInfo class if not present already in the global collection.
        if (!Globals.ThisAddIn.StoredImportSessions.Exists(session => session.WorkbookGuid == workbookGuid && session.MySqlTable == MySqlTable && string.Equals(session.ExcelTableName, ExcelTable.Name, StringComparison.InvariantCultureIgnoreCase)))
        {
          Globals.ThisAddIn.StoredImportSessions.Add(this);
        }
      }
      catch (Exception ex)
      {
        MiscUtilities.ShowCustomizedErrorDialog(string.Format(Resources.ExcelTableCreationError, ExcelTable != null ? ExcelTable.Name : MySqlTable.ExcelTableName), ex.Message, true);
        MySqlSourceTrace.WriteAppErrorToLog(ex);
      }
    }

    /// <summary>
    /// Creates both <see cref="ExcelInterop.ListObject"/> and <see cref="ExcelTools.ListObject"/> from an external data source and places the data at the given Excel cell.
    /// </summary>
    /// <remarks>This method must be used in Excel versions lesser than 15 (2013) where the Data Model is not supported.</remarks>
    /// <param name="worksheet"></param>
    /// <param name="atCell">The top left Excel cell of the new <see cref="ExcelInterop.ListObject"/>.</param>
    /// <param name="addSummaryRow">Flag indicating whether to include a row with summary fields at the end of the data rows.</param>
    private void CreateExcelTableFromExternalSource(ExcelTools.Worksheet worksheet, ExcelInterop.Range atCell, bool addSummaryRow)
    {
      // Prepare Excel table name and dummy connection string
      string proposedName = MySqlTable.ExcelTableName;
      string excelTableName = worksheet.GetExcelTableNameAvoidingDuplicates(proposedName);
      string workbookConnectionName = excelTableName.StartsWith("MySQL.") ? excelTableName : "MySQL." + excelTableName;
      workbookConnectionName = workbookConnectionName.GetWorkbookConnectionNameAvoidingDuplicates();
      string connectionStringForCmdDefault = MySqlTable.WbConnection.GetConnectionStringForCmdDefault();

      // Create empty Interop Excel table that will be connected to a data source.
      // This automatically creates a Workbook connection as well although the data refresh does not use the Workbook connection since it is a dummy one.
      var hasHeaders = ImportColumnNames ? ExcelInterop.XlYesNoGuess.xlYes : ExcelInterop.XlYesNoGuess.xlNo;
      var excelTable = worksheet.ListObjects.Add(ExcelInterop.XlListObjectSourceType.xlSrcExternal, connectionStringForCmdDefault, false, hasHeaders, atCell);
      excelTable.Name = excelTableName;
      excelTable.TableStyle = Settings.Default.ImportExcelTableStyleName;
      excelTable.QueryTable.BackgroundQuery = false;
      excelTable.QueryTable.CommandText = MySqlTable.SelectQuery.Replace("`", string.Empty);
      excelTable.QueryTable.WorkbookConnection.Name = workbookConnectionName;
      excelTable.QueryTable.WorkbookConnection.Description = Resources.WorkbookConnectionForExcelTableDescription;
      excelTable.Comment = Guid.NewGuid().ToString();
      excelTable.ShowTotals = addSummaryRow;
      ExcelTable = excelTable;
    }
  }
}