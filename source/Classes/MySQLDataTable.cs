﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySQL.Utility;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace MySQL.ForExcel
{
  public class MySQLDataTable : DataTable
  {
    private bool firstRowIsHeaders;
    private bool addPK;

    public bool AddPK {
      get { return addPK; }
      set
      {
        addPK = value;
        for (int i = 1; i < Columns.Count && value; i++)
          (Columns[i] as MySQLDataColumn).PrimaryKey = false;
      }
    }
    public bool IsFormatted { get; private set; }
    public bool FirstRowIsHeaders
    {
      get { return firstRowIsHeaders; }
      set { firstRowIsHeaders = value; UseFirstRowAsHeaders(value); }
    }
    public bool FirstColumnContainsIntegers
    {
      get
      {
        bool containsIntegers = false;
        int res = 0;
        if (Columns.Count > 1)
          containsIntegers = (Columns[1] as MySQLDataColumn).MySQLDataType.ToLowerInvariant() == "integer";
        if (!containsIntegers)
        {
          containsIntegers = true;
          for (int rowIdx = (firstRowIsHeaders ? 1 : 0); rowIdx < Math.Min(Rows.Count, 50); rowIdx++)
          {
            containsIntegers = containsIntegers && Int32.TryParse(Rows[rowIdx][1].ToString(), out res);
          }
        }
        return containsIntegers;
      }
    }
    public int NumberOfPK
    {
      get { return Columns.OfType<MySQLDataColumn>().Skip(1).Count(col => col.PrimaryKey); }
    }

    private void UseFirstRowAsHeaders(bool useFirstRow)
    {
      DataRow row = Rows[0];
      for (int i = 1; i < Columns.Count; i++)
      {
        MySQLDataColumn col = Columns[i] as MySQLDataColumn;
        col.DisplayName = (useFirstRow ? DataToColName(row[i].ToString()) : col.ColumnName);
        col.MySQLDataType = (useFirstRow ? col.OtherRowsDataType : col.FirstRowDataType);
      }
      (Columns[0] as MySQLDataColumn).DisplayName = TableName + "_id";
      int adjustIdx = (useFirstRow ? 0 : 1);
      for (int i = 0; i < Rows.Count; i++)
      {
        Rows[i][0] = i + adjustIdx;
      }
    }

    public void SetData(Excel.Range dataRange, bool useFormattedData, bool detectTypes, bool createIndexForIntColumns, bool allowEmptyNonIdxCols)
    {
      object[,] data;

      // we have to treat a single cell specially.  It doesn't come in as an array but as a single value
      if (dataRange.Count == 1)
      {
        data = new object[2, 2];
        data[1, 1] = useFormattedData ? dataRange.Value : dataRange.Value2;
      }
      else
        data = useFormattedData ? dataRange.Value : dataRange.Value2;

      IsFormatted = useFormattedData;

      int numRows = data.GetUpperBound(0);
      int numCols = data.GetUpperBound(1);

      if (Columns.Count == 0)
        CreateColumns(numCols);

      for (int row = 1; row <= numRows; row++)
      {
        DataRow dataRow = NewRow();
        dataRow[0] = row;
        for (int col = 1; col <= numCols; col++)
        {
          dataRow[col] = data[row, col];
        }
        Rows.Add(dataRow);
      }
      if (detectTypes)
        DetectTypes(data, createIndexForIntColumns);
      if (allowEmptyNonIdxCols)
        foreach (MySQLColumn mysqlCol in Columns.OfType<MySQLColumn>().Skip(1))
        {
          mysqlCol.AllowNull = !mysqlCol.CreateIndex;
        }
    }

    private void DetectTypes()
    {
      foreach (MySQLDataColumn col in Columns)
        col.DetectType(firstRowIsHeaders);
    }

    private void DetectTypes(object[,] data, bool createIndexForIntColumns)
    {
      int rowsCount = data.GetUpperBound(0);
      int colsCount = data.GetUpperBound(1);
      string dateFormat = "yyyy-MM-dd HH:mm:ss";

      for (int colPos = 1; colPos <= colsCount; colPos++)
      {
        object valueFromArray = null;
        string proposedType = String.Empty;
        string strippedType = String.Empty;
        string headerType = String.Empty;
        bool typesConsistent = true;
        bool valueOverflow = false;
        List<string> typesList = new List<string>(rowsCount);
        int[] varCharMaxLen = new int[2] { 0, 0 };
        int[] decimalMaxLen = new int[2] { 0, 0 };
        int lParensIndex = -1;

        for (int rowPos = 1; rowPos <= rowsCount; rowPos++)
        {
          valueFromArray = data[rowPos, colPos];
          if (valueFromArray == null)
            continue;

          // Treat always as a Varchar value first in case all rows do not have a consistent datatype
          proposedType = Utilities.GetMySQLExportDataType(valueFromArray.ToString(), out valueOverflow);
          lParensIndex = proposedType.IndexOf("(");
          varCharMaxLen[1] = Math.Max(Int32.Parse(proposedType.Substring(lParensIndex + 1, proposedType.Length - lParensIndex - 2)), varCharMaxLen[1]);

          // Normal datatype detection
          proposedType = Utilities.GetMySQLExportDataType(valueFromArray, out valueOverflow);
          lParensIndex = proposedType.IndexOf("(");
          strippedType = (lParensIndex < 0 ? proposedType : proposedType.Substring(0, lParensIndex));
          switch (strippedType)
          {
            case "Date":
            case "Datetime":
              DateTime dtValue = (DateTime)valueFromArray;
              Rows[rowPos - 1][colPos] = dtValue.ToString(dateFormat);
              break;
            case "Varchar":
              if (rowPos > 1)
                varCharMaxLen[0] = Math.Max(Int32.Parse(proposedType.Substring(lParensIndex + 1, proposedType.Length - lParensIndex - 2)), varCharMaxLen[0]);
              break;
            case "Decimal":
              int commaPos = proposedType.IndexOf(",");
              decimalMaxLen[0] = Math.Max(Int32.Parse(proposedType.Substring(lParensIndex + 1, commaPos - lParensIndex -1)), decimalMaxLen[0]);
              decimalMaxLen[1] = Math.Max(Int32.Parse(proposedType.Substring(commaPos + 1, proposedType.Length - commaPos - 2)), decimalMaxLen[1]);
              break;
          }
          if (rowPos == 1)
            headerType = proposedType;
          else
            typesList.Add(strippedType);
        }

        typesConsistent = typesList.All(str => str == strippedType);
        if (!typesConsistent)
        {
          if (typesList.Count(str => str == "Integer") + typesList.Count(str => str == "Bool") == typesList.Count)
          {
            typesConsistent = true;
            proposedType = "Integer";
          }
          else if (typesList.Count(str => str == "Integer") + typesList.Count(str => str == "BigInt") == typesList.Count)
          {
            typesConsistent = true;
            proposedType = "BigInt";
          }
          else if (typesList.Count(str => str == "Integer") + typesList.Count(str => str == "Decimal") == typesList.Count)
          {
            typesConsistent = true;
            strippedType = "Decimal";
          }
          else if (typesList.Count(str => str == "Integer") + typesList.Count(str => str == "Decimal") + typesList.Count(str => str == "Double") == typesList.Count)
          {
            typesConsistent = true;
            proposedType = "Double";
          }
        }

        if (typesConsistent)
          switch (strippedType)
          {
            case "Varchar":
              proposedType = String.Format("Varchar({0})", varCharMaxLen[0]);
              break;
            case "Decimal":
              if (decimalMaxLen[0] > 12 || decimalMaxLen[1] > 2)
              {
                decimalMaxLen[0] = 65;
                decimalMaxLen[1] = 30;
              }
              else
              {
                decimalMaxLen[0] = 12;
                decimalMaxLen[1] = 2;
              }
              proposedType = String.Format("Decimal({0}, {1})", decimalMaxLen[0], decimalMaxLen[1]);
              break;
          }
        else
            proposedType = String.Format("Varchar({0})", varCharMaxLen[1]);

        MySQLDataColumn col = Columns[colPos] as MySQLDataColumn;
        col.FirstRowDataType = headerType;
        col.OtherRowsDataType = proposedType;
        col.MySQLDataType = (firstRowIsHeaders ? headerType : proposedType);
        col.CreateIndex = (createIndexForIntColumns && col.MySQLDataType == "Integer");
      }
    }

    private void CreateColumns(int numCols)
    {
      MySQLDataColumn column = null;
      for (int colIdx = 0; colIdx <= numCols; colIdx++)
      {
        string name = "Column" + colIdx;
        column = new MySQLDataColumn();
        column.ColumnName = column.DisplayName = name;
        Columns.Add(column);
      }
      column = (Columns[0] as MySQLDataColumn);
      column.PrimaryKey = true;
      column.AutoPK = true;
      column.ColumnName = column.DisplayName = TableName + "_id";
      column.MySQLDataType = "Integer";
      column.AutoIncrement = true;
    }

    private string DataToColName(string dataValue)
    {
      return (dataValue != null ? dataValue.Replace(" ", "_").Replace("(", String.Empty).Replace(")", String.Empty) : String.Empty);
    }

    public bool CreateTable(MySqlWorkbenchConnection wbConnection)
    {
      bool success = false;
      string connectionString = Utilities.GetConnectionString(wbConnection);
      string queryString = GetCreateSQL(false);

      try
      {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
          conn.Open();

          MySqlCommand cmd = new MySqlCommand(queryString, conn);
          cmd.ExecuteNonQuery();
          success = true;
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
      }

      return success;
    }

    public string GetCreateSQL(bool formatNewLinesAndTabs)
    {
      StringBuilder sql = new StringBuilder();
      string nl = (formatNewLinesAndTabs ? "\n" : " ");
      string nlt = (formatNewLinesAndTabs ? "\n\t" : " ");

      sql.AppendFormat("CREATE TABLE `{0}`{1}(", TableName, nl);

      string delimiter = nlt;
      foreach (MySQLDataColumn column in Columns)
      {
        if (column.ExcludeColumn)
          continue;
        sql.AppendFormat("{0}{1}", delimiter, column.GetSQL());
        delimiter = "," + nlt;
      }
      foreach (MySQLDataColumn col in Columns)
      {
        if (col.AutoPK || col.PrimaryKey || col.UniqueKey || !col.CreateIndex)
          continue;
        sql.AppendFormat("{0}INDEX {1}_idx ({1})", delimiter, col.DisplayName);
      }
      sql.Append(nl);
      sql.Append(")");
      return sql.ToString();
    }

    public string GetInsertSQL(int limit, bool formatNewLinesAndTabs)
    {
      int exportColsCount = Columns.Count;
      if (Rows.Count - (firstRowIsHeaders ? 1 : 0) < 1)
        return null;

      StringBuilder queryString = new StringBuilder();
      string nl = (formatNewLinesAndTabs ? "\n" : " ");
      int rowIdx = 0;
      int colIdx = 0;

      string separator = String.Empty;
      queryString.AppendFormat("INSERT INTO `{0}`{1}(", TableName, nl);

      for (colIdx = 0; colIdx < exportColsCount; colIdx++)
      {
        MySQLDataColumn column = Columns[colIdx] as MySQLDataColumn;
        if (column.ExcludeColumn)
          continue;
        queryString.AppendFormat("{0}{1}",
                                 separator,
                                 column.DisplayName);
        separator = ",";
      }
      queryString.AppendFormat("){0}VALUES{0}", nl);

      foreach (DataRow dr in Rows)
      {
        if (firstRowIsHeaders && rowIdx++ == 0)
          continue;
        if (limit > 0 && rowIdx >= limit)
          break;
        queryString.Append("(");
        separator = String.Empty;
        for (colIdx = 0; colIdx < exportColsCount; colIdx++)
        {
          MySQLDataColumn column = Columns[colIdx] as MySQLDataColumn;
          if (column.ExcludeColumn)
            continue;
          string toLowerDataType = column.MySQLDataType.ToLowerInvariant();
          queryString.AppendFormat("{0}{1}{2}{1}",
                                   separator,
                                   (column.ColumnsRequireQuotes ? "'" : String.Empty),
                                   dr[column.ColumnName].ToString());
          separator = ",";
        }        
        queryString.AppendFormat("),{0}", nl);
      }
      if (Rows.Count > 0)
        queryString.Remove(queryString.Length - 2, 2);
      return queryString.ToString();
    }

    public bool InsertDataWithAdapter(MySqlWorkbenchConnection wbConnection, bool firstRowHeader, bool useFormattedData)
    {
      bool success = false;

      DataTable copyOriginal = this.Clone();
      copyOriginal.Merge(this);
      foreach (MySQLDataColumn col in Columns)
      {
        MySQLDataColumn copyCol = copyOriginal.Columns[col.ColumnName] as MySQLDataColumn;
        if (col.ExcludeColumn)
          copyOriginal.Columns.Remove(copyCol);
        else
          copyCol.ColumnName = col.DisplayName;
      }
      if (firstRowHeader)
        copyOriginal.Rows.RemoveAt(0);
      copyOriginal.AcceptChanges();

      string connectionString = Utilities.GetConnectionString(wbConnection);
      // Create & open a SqlConnection, and dispose of it after we are done.
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(String.Format("SELECT * FROM {0} LIMIT 0", TableName), connection);
        DataTable exportingDataTable = new DataTable();
        dataAdapter.Fill(exportingDataTable);
        foreach (DataRow row in copyOriginal.Rows)
        {
          exportingDataTable.LoadDataRow(row.ItemArray, LoadOption.OverwriteChanges);
        }
        MySqlCommandBuilder commBuilder = new MySqlCommandBuilder(dataAdapter);
        dataAdapter.InsertCommand = commBuilder.GetInsertCommand();

        int updatedCount = 0;
        try
        {
          updatedCount = dataAdapter.Update(exportingDataTable);
          success = updatedCount > 0;
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine(ex.Message);
        }
      }

      return success;
    }
  }

  public class MySQLDataColumn : DataColumn
  {
    private bool uniqueKey;
    private string displayName;

    public bool AutoPK { get; set; }
    public bool CreateIndex { get; set; }
    public bool UniqueKey 
    {
      get { return uniqueKey; }
      set { uniqueKey = value; if (uniqueKey) CreateIndex = true; }
    }
    public string DisplayName
    {
      get { return displayName; }
      set 
      { 
        string trimmedName = value.Trim();
        displayName = trimmedName;
        if (Table == null || Table.Columns.Count < 2)
          return;
        int colIdx = 1;
        while (Table.Columns.OfType<MySQLDataColumn>().Count(col => col.DisplayName == displayName) > 1)
        {
          displayName = trimmedName + colIdx;
        }
      }
    }

    public bool PrimaryKey { get; set; }
    public bool AllowNull { get; set; }
    public bool ExcludeColumn { get; set; }
    public string MySQLDataType { get; set; }
    public string WarningText { get; set; }
    public string FirstRowDataType { get; set; }
    public string OtherRowsDataType { get; set; }

    #region Properties

    public bool IsDecimal
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return toLowerDataType == "real" || toLowerDataType == "double" || toLowerDataType == "float" || toLowerDataType == "decimal" || toLowerDataType == "numeric";
      }
    }

    public bool IsNumeric
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return IsDecimal || toLowerDataType.Contains("int");
      }
    }

    public bool IsChar
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return toLowerDataType.Contains("char");
      }
    }

    public bool IsCharOrText
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return toLowerDataType.Contains("char") || toLowerDataType.Contains("text");
      }
    }

    public bool IsBinary
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return toLowerDataType.Contains("binary");
      }
    }

    public bool IsDate
    {
      get
      {
        string toLowerDataType = MySQLDataType.ToLowerInvariant();
        return toLowerDataType.Contains("date") || toLowerDataType == "timestamp";
      }
    }

    public bool ColumnsRequireQuotes
    {
      get { return IsCharOrText || IsDate; }
    }

    #endregion Properties

    public void DetectType(bool firstRowIsHeaders)
    {
      object valueFromArray = null;
      string proposedType = String.Empty;
      string previousType = String.Empty;
      string headerType = String.Empty;
      bool typesConsistent = true;
      bool valueOverflow = false;
      string dateFormat = "yyyy-MM-dd HH:mm:ss";
      int rowPos = 0;

      foreach (DataRow dr in Table.Rows)
      {
        valueFromArray = dr[Ordinal];
        if (valueFromArray == null)
          continue;
        proposedType = Utilities.GetMySQLExportDataType(valueFromArray, out valueOverflow);
        if (proposedType.StartsWith("Date") && valueFromArray is DateTime)
        {
          DateTime dtValue = (DateTime)valueFromArray;
          dr[Ordinal] = dtValue.ToString(dateFormat);
        }
        if (rowPos == 1)
          headerType = proposedType;
        else
        {
          typesConsistent = typesConsistent && (rowPos > 2 ? previousType == proposedType : true);
          previousType = proposedType;
        }
      }
      if (previousType.Length == 0)
        previousType = "Varchar(255)";
      if (headerType.Length == 0)
        headerType = previousType;
      FirstRowDataType = headerType;
      OtherRowsDataType = previousType;
      MySQLDataType = (firstRowIsHeaders ? headerType : previousType);
      rowPos++;
    }

    public string GetSQL()
    {
      if (String.IsNullOrEmpty(displayName))
        return null;

      StringBuilder colDefinition = new StringBuilder(displayName);
      colDefinition.AppendFormat(" {0}", MySQLDataType);
      if (AutoPK)
        colDefinition.Append(" primary key");
      else if (UniqueKey)
        colDefinition.Append(" unique key");
      if (AllowNull)
        colDefinition.Append(" null");
      if (AutoIncrement)
        colDefinition.Append(" auto_increment");

      return colDefinition.ToString();
    }

  }
}
