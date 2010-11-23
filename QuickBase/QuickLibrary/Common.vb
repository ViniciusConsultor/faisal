Public Class Common
  Private Shared _TotalSizes As Int32 = 13
  Private Shared _IsApplicationRunning As Boolean = False

  Enum TrimLocations As Integer
    FromStart
    FromEnd
  End Enum

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 13-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will tell if application is runnin. Currently needed in frmMaster because
  ''' code is running when we load form in design mode and generates error.
  ''' </summary>
  Public Shared Property IsApplicationRunning() As Boolean
    Get
      Try

        Return _IsApplicationRunning

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsApplicationRunning of Common.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsApplicationRunning = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsApplicationRunning of Common.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#Region "Qty and Sizes"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  28-Aug-10   Adding try catch.
  '                           Moved variable declaration with property, because 
  '                           after upgrading farpoint grid its instance is set to
  '                           nothing automatically even if I create new instance
  '                           in property it does not work.
  ''' <summary>
  ''' This is used to set the cell type for quantity columns.
  ''' </summary>
  Public Shared ReadOnly Property QtyCellType() As FarPoint.Win.Spread.CellType.NumberCellType
    Get
      Try
        Dim _QtyCellType As New FarPoint.Win.Spread.CellType.NumberCellType

        _QtyCellType.DecimalPlaces = 0

        Return _QtyCellType

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Get method of QtyCellType property of Common.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  28-Aug-10   Adding try catch.
  '                           Moved variable declaration with property, because 
  '                           after upgrading farpoint grid its instance is set to
  '                           nothing automatically even if I create new instance
  '                           in property it does not work.
  ''' <summary>
  ''' This is used to set the cell type for quantity columns.
  ''' </summary>
  Public Shared ReadOnly Property QtyTotalCellType() As FarPoint.Win.Spread.CellType.NumberCellType
    Get
      Try
        Dim _QtyTotalCellType As New FarPoint.Win.Spread.CellType.NumberCellType

        _QtyTotalCellType.DecimalPlaces = 0

        Return _QtyTotalCellType

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Get method of QtyTotalCellType property of Common.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Dec-2009
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It returns maximum total number of sizes available for items
  ''' </summary>
  Public Shared Property TotalSizes() As Int32
    Get
      Try

        Return _TotalSizes
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in TotalSizes of Common.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Int32)
      Try

        _TotalSizes = value
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in TotalSizes of Common.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Private _SystemDateTime As DateTime
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Returns current system date & time, it is centralized so that when we want 
  ''' to change time zone we can change it easily. UTC datetime should be stored.
  ''' </summary>
  Public Shared ReadOnly Property SystemDateTime() As DateTime
    Get
      Try

        Return Date.Now

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in SystemDateTime of Common.", ex)
        Throw _qex
      End Try
    End Get
  End Property

#End Region

  Public Shared Sub Wait(ByVal Second As Int32)
    Dim Time As Double
    Time = Microsoft.VisualBasic.Timer
    Do While Microsoft.VisualBasic.Timer < (Time + Second)
      'Do nothing.
    Loop
  End Sub

#Region "String Functions"

  Public Shared Function SplitStringToArrayList(ByVal Text As String, ByVal Terminator As String) As ArrayList
    Try
      Return SplitStringToArrayList(Text, Terminator, False)

    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in SplitStringToArrayList(string,string) function", ex)
      Throw _quickException
    End Try
  End Function

  Public Shared Function SplitStringToArrayList(ByVal Text As String, ByVal Terminator As String, ByVal CaseSensitive As Boolean) As ArrayList
    Try
      Dim _ArrayList As New ArrayList
      Dim _ArrayListItem As String = String.Empty
      Dim I As Int32 = 0

      Do While I < Text.Length
        If Terminator.Length < (Text.Length - I) AndAlso (Text.Substring(I, Terminator.Length) = Terminator OrElse (Text.Substring(I, Terminator.Length).ToLower = Terminator.ToLower AndAlso Not CaseSensitive)) Then
          I += Terminator.Length - 1
          _ArrayList.Add(_ArrayListItem)
          _ArrayListItem = String.Empty
        Else
          _ArrayListItem &= Text.Substring(I, 1)
        End If

        I += 1
      Loop

      'If there is text after last terminator
      If _ArrayListItem.Trim <> String.Empty Then
        _ArrayList.Add(_ArrayListItem)
      End If

      Return _ArrayList
    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in SplitStringToArrayList(string,string,boolean) function", ex)
      Throw _quickException
    End Try
  End Function

  Public Shared Function TrimCharacters(ByVal _StringToTrim As String, ByVal Count As Int32, ByVal TrimLocationpara As TrimLocations) As String
    Try

      TrimCharacters = Nothing

      If _StringToTrim.Length < Count Then
        Throw New Exception("TrimCharacters: String provided is lesser than the characters to be trimmed.")
      Else
        If TrimLocationpara = TrimLocations.FromStart Then
          Return _StringToTrim.Substring(Count, _StringToTrim.Length - Count)
        ElseIf TrimLocationpara = TrimLocations.FromEnd Then
          Return _StringToTrim.Substring(0, _StringToTrim.Length - Count)
        End If
      End If

    Catch ex As Exception
      TrimCharacters = Nothing
      Dim _qex As New QuickException("Exception in TrimCharacters method of Common class.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 13-Jun-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It counts character occurence in the given string.
  ''' </summary>
  Public Shared Function CountStringOccurences(ByVal _StringToCount As String, ByVal _Text As String) As Int32
    Try
      Dim _TotalOccurences As Int32 = 0

      For I As Int32 = 0 To _Text.Length - _StringToCount.Length
        If _Text.Substring(I, _StringToCount.Length) = _StringToCount Then
          _TotalOccurences += 1
        End If
      Next

      Return _TotalOccurences

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception in CountStringOccurences method of Common.", ex)
      Throw QuickExceptionObject
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 01-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Searches given string and finds particular occurence of the string to search.
  ''' </summary>
  Public Shared Function IndexOfStringAtOccurence(ByVal _StringToFind As String, ByVal _StringToFindFrom As String, ByVal _OccurenceNumberToFind As Int32) As Int32
    Try
      Dim _OccurenceNo As Int32 = 0
      Dim _FoundIndex As Int32 = 0

      Do
        _OccurenceNo += 1
        _FoundIndex = _StringToFindFrom.IndexOf(_StringToFind, _FoundIndex + 1)
      Loop While _OccurenceNo < _OccurenceNumberToFind AndAlso _FoundIndex >= 0

      Return _FoundIndex

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in IndexOfStringAtOccurence of Common.", ex)
      Throw _qex
    End Try
  End Function


#End Region

#Region "Security"

  Public Shared Function EncryptText(ByVal _Text As String) As String
    Try
      'Dim inName As String, outName As String, tdesKey() As Byte, tdesIV() As Byte

      'Create the file streams to handle the input and output files.
      'Dim fin As New FileStream(inName, FileMode.Open, FileAccess.Read)
      'Dim fout As New FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write)
      'fout.SetLength(0)

      ''Create variables to help with read and write.
      'Dim bin(100) As Byte 'This is intermediate storage for the encryption.
      'Dim rdlen As Long = 0 'This is the total number of bytes written.
      ''Dim totlen As Long = fin.Length 'This is the total length of the input file.
      ''Dim len As Integer 'This is the number of bytes to be written at a time.
      'Dim _Stream As System.IO.StreamWriter
      'Dim tdes As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
      'Dim encStream As New System.Security.Cryptography.CryptoStream(fout, _
      '   tdes.CreateEncryptor(tdesKey, tdesIV), CryptoStreamMode.Write)

      'Console.WriteLine("Encrypting...")

      ''Read from the input file, then encrypt and write to the output file.
      'While rdlen < totlen
      '  len = fin.Read(bin, 0, 100)
      '  encStream.Write(bin, 0, len)
      '  rdlen = rdlen + len
      '  Console.WriteLine("{0} bytes processed", rdlen)
      'End While

      'encStream.Close()
      Return _Text
    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in EncryptText(String) function", ex)
      Throw _quickException
    End Try
  End Function

  Public Shared Function DecryptText(ByVal _Text As String) As String
    Try

      Return _Text
    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in DecryptText(String) function", ex)
      Throw _quickException
    End Try
  End Function

#End Region

#Region "Document Number Related"
  '<<<<<
  'Y = Year
  'M = Month
  'P = Party Category Code
  'p = party code
  '>>>>>



  'Author: Faisal
  'Date Created(DD-MMM-YY): Nov-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  'Faisal       26-Dec-09   Added support for returning LikeOperatorPattern so that
  '                         the value can be used on form to fetch the largest 
  '                         code existing in the database.
  '
  ''' <summary>
  ''' Give it the last document number (can be nothing) and the document format and
  ''' it will give you the next document number. ReturnLikeOperatorPattern should
  ''' be true if you want a string which you need to get the maximum document no
  ''' in the database, when getting next document no it should be false.
  ''' Y will give you the year, possible patterns are Y, YY, YYY, YYYY.
  ''' 9 will give you the increamenting digit, there any repitions 1-n.
  ''' P will give you the party category code.
  ''' p will give you the party code.
  ''' </summary>
  Public Shared Function GenerateNextDocumentNo(ByVal PartyCategoryCode As String, ByVal LastDocumentNo As String, ByVal DocumentNoFormat As String, ByVal ReturnLikeOperatorPattern As Boolean) As String
    Try
      'Define only 4 digit code for business parts such as ptcc for party category code, ptcd for party code etc.
      Dim _DocumentNoFormatStringCollection As New Collections.Specialized.StringCollection
      Dim _NextDocumentNo As String = String.Empty
      Dim _CharIndexForFormat As Int32 = 0
      Dim _CharIndexForLastNo As Int32 = 0
      Dim _CharacterCount As Int32 = 0
      Dim _LastNumericNo As Int32 = 0

      Do While _CharIndexForFormat < DocumentNoFormat.Length
        If DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "Y" Then

          If DocumentNoFormat.Length - _CharIndexForFormat > 3 AndAlso DocumentNoFormat.Substring(_CharIndexForFormat, 4) = "YYYY" Then
            _NextDocumentNo &= Now.Year.ToString
            _CharIndexForFormat += 4 : _CharIndexForLastNo += 4
          ElseIf DocumentNoFormat.Length - _CharIndexForFormat > 2 AndAlso DocumentNoFormat.Substring(_CharIndexForFormat, 3) = "YYY" Then
            _NextDocumentNo &= Now.Year.ToString.Substring(1, 3)
            _CharIndexForFormat += 3 : _CharIndexForLastNo += 3
          ElseIf DocumentNoFormat.Length - _CharIndexForFormat > 1 AndAlso DocumentNoFormat.Substring(_CharIndexForFormat, 2) = "YY" Then
            _NextDocumentNo &= Now.Year.ToString.Substring(2, 2)
            _CharIndexForFormat += 2 : _CharIndexForLastNo += 2
          ElseIf DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "Y" Then
            _NextDocumentNo &= Now.Year.ToString.Substring(3, 1)
            _CharIndexForFormat += 1 : _CharIndexForLastNo += 1
          End If

        ElseIf DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "9" Then

          _CharacterCount = 0
          Do While _CharIndexForFormat < DocumentNoFormat.Length AndAlso DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "9"
            _CharIndexForFormat += 1 : _CharacterCount += 1
            If ReturnLikeOperatorPattern Then _NextDocumentNo &= "_"
          Loop
          If Not ReturnLikeOperatorPattern Then
            If LastDocumentNo.Length > 0 Then
              If Int32.TryParse(LastDocumentNo.Substring(_CharIndexForLastNo, _CharacterCount), _LastNumericNo) Then
                _LastNumericNo += 1
                _NextDocumentNo &= _LastNumericNo.ToString.PadLeft(_CharacterCount, "0"c)
              Else
                Throw New Exception("GenerateNextDocumentNo: Last Document No provided does match with format provided.")
              End If
            Else
              _NextDocumentNo &= "1".PadLeft(_CharacterCount, "0"c)
            End If

            _CharIndexForLastNo += _CharacterCount
          End If

        ElseIf DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "P" Then

          _CharacterCount = 0
          Do While _CharIndexForFormat < DocumentNoFormat.Length AndAlso DocumentNoFormat.Substring(_CharIndexForFormat, 1) = "P"
            _CharIndexForFormat += 1 : _CharacterCount += 1
            'If ReturnLikeOperatorPattern Then _NextDocumentNo &= "_"
          Loop
          'If Not ReturnLikeOperatorPattern Then

          If PartyCategoryCode.Length < _CharacterCount Then
            _NextDocumentNo = PartyCategoryCode.PadRight(_CharacterCount, " "c)
          Else
            _NextDocumentNo = PartyCategoryCode.Substring(0, _CharacterCount)
          End If

          _CharIndexForLastNo += _CharacterCount
          'End If

          'If ReturnLikeOperatorPattern Then
          '  _NextDocumentNo &= "%"
          'Else
          '  _NextDocumentNo &= PartyCategoryCode
          'End If

          '_CharIndexForFormat += 4 : _CharIndexForLastNo += PartyCategoryCode.Length

        Else

          _NextDocumentNo &= DocumentNoFormat.Substring(_CharIndexForFormat, 1)
          _CharIndexForFormat += 1 : _CharIndexForLastNo += 1

        End If
      Loop

      Return _NextDocumentNo

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in GenerateNextDocumentNo method of Common class.", ex)
      Throw _qex
    End Try
  End Function

  Public Enum DocumentNoSegments
    PartyCategoryCode
    Year
  End Enum

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Get particular segment from the Document Number. Usually it is used to check
  ''' if static segment is changed such as party category code then maybe document
  ''' number will be generated new.
  ''' </summary>
  Public Shared Function GetDocumentNoSegment(ByVal DocumentNoSegmentpara As DocumentNoSegments, ByVal DocumentNopara As String, ByVal DocumentNoFormatpara As String) As String
    Try
      Dim _CharIndex As Int32
      Dim _StartIndex As Int32
      Dim _CharCount As Int32

      GetDocumentNoSegment = String.Empty

      Select Case DocumentNoSegmentpara
        Case DocumentNoSegments.PartyCategoryCode

          _StartIndex = DocumentNoFormatpara.IndexOf("P"c)
          If _CharIndex >= 0 Then
            Do While _CharIndex < DocumentNoFormatpara.Length AndAlso DocumentNoFormatpara.Substring(_CharIndex, 1) = "P"
              _CharIndex += 1 : _CharCount += 1
            Loop

            Return DocumentNopara.Substring(_StartIndex, _CharCount)
          Else
            'Empty string is assigned as return value in the start.
          End If

        Case DocumentNoSegments.Year
          Throw New Exception("Not Implemented yet")
      End Select

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in GetDocumentNoSegment of Common.", ex)
      Throw _qex
    End Try
  End Function

#End Region

End Class
