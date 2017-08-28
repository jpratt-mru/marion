Imports System.IO
Imports System.Collections.Specialized

Module Logger
    Public Sub Log(ByVal logLine As String)
        Dim sWriter As New StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\" + Constants.LOG_FILE_NAME, True)
        sWriter.WriteLine(logLine)
        sWriter.Close()
    End Sub

    Public Function Report() As String
        Dim reportText As String = ""
        Dim numGamesLoanedFri As Integer = 0
        Dim numGamesLoanedSat As Integer = 0
        Dim numGamesLoanedSun As Integer = 0

        Dim borrowerCount As New Dictionary(Of String, Integer)
        Dim gameCount As New Dictionary(Of String, Integer)

        Dim logFileLines As String() = LogFileText()
        For Each line As String In logFileLines
            line = line.ToUpper
            If line.Length <> 0 Then
                If (line.Contains("CHECKIN:")) Then
                    If (line.Contains("(MO)")) Then
                        numGamesLoanedFri += 1
                    ElseIf (line.Contains("(SA)")) Then
                        numGamesLoanedSat += 1
                    ElseIf (line.Contains("(SU)")) Then
                        numGamesLoanedSun += 1
                    End If
                ElseIf (line.Contains("CHECKOUT:")) Then
                    Dim indexOfBorrowerBarcodeStart As Integer = line.IndexOf(Constants.BORROWER_PREFIX + Constants.PREFIX_SUFFIX_SEPARATOR)
                    Dim indexOfBorrowerBarcodeEnd As Integer = line.IndexOf(Constants.SPLIT_DELIMITER, indexOfBorrowerBarcodeStart)
                    Dim borrowerBarcode As String = line.Substring(indexOfBorrowerBarcodeStart, indexOfBorrowerBarcodeEnd - indexOfBorrowerBarcodeStart)
                    If (borrowerCount.ContainsKey(borrowerBarcode)) Then
                        borrowerCount(borrowerBarcode) += 1
                    Else
                        borrowerCount.Add(borrowerBarcode, 1)
                    End If

                    Dim firstCommaPosition As Integer = line.IndexOf(Constants.SPLIT_DELIMITER)
                    Dim secondCommaPosition As Integer = line.IndexOf(Constants.SPLIT_DELIMITER, firstCommaPosition + 1)
                    Dim gameName As String = line.Substring(firstCommaPosition + 1, secondCommaPosition - firstCommaPosition - 1)
                    If (gameCount.ContainsKey(gameName)) Then
                        gameCount(gameName) += 1
                    Else
                        gameCount.Add(gameName, 1)
                    End If
                End If

            End If
        Next

        reportText = "Number of games loaned: " + (numGamesLoanedFri + numGamesLoanedSat + numGamesLoanedSun).ToString + vbCrLf + _
                     vbCrLf + _
                     "Fri: " + numGamesLoanedFri.ToString + vbCrLf + _
                     "Sat: " + numGamesLoanedSat.ToString + vbCrLf + _
                     "Sun: " + numGamesLoanedSun.ToString + vbCrLf + _
                     vbCrLf + _
                     "------------------------------------" + _
                     vbCrLf + _
                     vbCrLf + _
                     "Number of unique borrowers: " + borrowerCount.Count.ToString + vbCrLf + _
                     vbCrLf + _
                     "------------------------------------" + _
                     vbCrLf + _
                     vbCrLf + _
                     "Top 10 Games: " + vbCrLf

        Dim topGamesText As String = ""
        Dim sortedDict = (From entry In gameCount Take 10 Order By entry.Value Descending Select entry)

        For Each thing In sortedDict
            topGamesText += StrConv(thing.Key, VbStrConv.ProperCase) + ": " + thing.Value.ToString + vbCrLf
        Next


        Return reportText + vbCrLf + topGamesText

    End Function


    Private Function LogFileText() As String()

        Dim sReader As StreamReader = Nothing
        If (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\" + Constants.LOG_FILE_NAME)) Then
            sReader = File.OpenText(System.AppDomain.CurrentDomain.BaseDirectory + "\" + Constants.LOG_FILE_NAME)
        End If

        Return sReader.ReadToEnd.Split(vbCrLf)

    End Function

End Module
