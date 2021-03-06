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

using System;
using MySQL.ForExcel.Properties;

namespace MySQL.ForExcel.Classes.Exceptions
{
  /// <summary>
  /// Represents an <see cref="Exception"/> thrown when a given object value cannot be converted to a certain data type.
  /// </summary>
  public class ValueNotSuitableForConversionException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueNotSuitableForConversionException"/>.
    /// </summary>
    /// <param name="rawValueAsText">The value attempted to be converted.</param>
    /// <param name="dataType">The data type related to the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ValueNotSuitableForConversionException(string rawValueAsText, string dataType, Exception innerException = null)
      : base(string.Format(Resources.ValueNotSuitableForConversionError, rawValueAsText, dataType), innerException)
    {
    }
  }
}
