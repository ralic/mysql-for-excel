﻿// Copyright (c) 2012, 2013, Oracle and/or its affiliates. All rights reserved.
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

using System;
using MySQL.ForExcel.Properties;
using MySQL.Utility.Classes;

namespace MySQL.ForExcel.Classes
{
  /// <summary>
  /// A settings provider customized for MySQL for Excel.
  /// </summary>
  public class MySqlForExcelSettings : CustomSettingsProvider
  {
    /// <summary>
    /// Gets the name of this application.
    /// </summary>
    public override string ApplicationName
    {
      get
      {
        return Resources.AppName;
      }

      set
      {
      }
    }

    /// <summary>
    /// Gets the custom path where the settings file is saved.
    /// </summary>
    public override string SettingsPath
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Oracle\MySQL For Excel\settings.config";
      }
    }
  }
}