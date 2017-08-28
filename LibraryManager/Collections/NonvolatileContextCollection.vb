Imports System.IO
Imports LibraryManager.Actors

Namespace Collections
    Public Class NonvolatileContextCollection(Of T As BarcodedThing)
        Inherits Dictionary(Of LibraryManager.Actors.SimpleBarcode, T)




        Protected saveFileName As String

        Public Sub New(ByVal saveFileName As String)
            Me.saveFileName = saveFileName
        End Sub

        Public Sub New(ByVal otherNonvolatileContextCollection As NonvolatileContextCollection(Of T))
            MyBase.New(otherNonvolatileContextCollection)
            Me.saveFileName = otherNonvolatileContextCollection.saveFileName
        End Sub

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(NonvolatileContextCollection(Of T)))) Then Return False
            Dim otherCollection As NonvolatileContextCollection(Of T) = CType(obj, NonvolatileContextCollection(Of T))
            Return otherCollection.saveFileName = Me.saveFileName AndAlso CollectionsAreSame(otherCollection)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode

        End Function

        Private Function CollectionsAreSame(ByVal otherCollection As NonvolatileContextCollection(Of T)) As Boolean
            If Me.Keys.Count <> otherCollection.Keys.Count Then
                Return False
            Else
                For i = 0 To Me.Keys.Count - 1
                    If (Not Me.Keys.ElementAt(i).Equals(otherCollection.Keys.ElementAt(i)) OrElse Not Me.Values.ElementAt(i).Equals(otherCollection.Values.ElementAt(i))) Then
                        Return False
                    End If
                Next
            End If
            Return True
        End Function

        Public Sub Save()
            Dim sWriter As New StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\" + saveFileName, False)
            For Each o As Object In Me.Values
                sWriter.WriteLine(o.ToFileString)
            Next
            sWriter.Close()
        End Sub

        Public Function Load(ByVal contextType As String) As String
            Dim errorMessages As String = ""
            Dim sReader As StreamReader
            If (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\" + saveFileName)) Then
                sReader = File.OpenText(System.AppDomain.CurrentDomain.BaseDirectory + "\" + saveFileName)
                Dim fileContents As String = sReader.ReadToEnd.Trim
                If (Not fileContents.Equals("")) Then
                    Dim fileLines As String() = fileContents.Split(vbCrLf)
                    Dim lineNumber As Integer = 1
                    For Each fileLine In fileLines
                        Try
                            Dim newBarcodedThing As BarcodedThing = BarcodedThingFactory.createBarcodedThing(contextType, fileLine)
                            Me.Add(newBarcodedThing.Barcode, newBarcodedThing)
                        Catch ex As Exception
                            errorMessages += saveFileName + " line number " + lineNumber.ToString + ":" + ex.Message + vbCrLf
                        End Try

                        lineNumber += 1
                    Next
                ElseIf (contextType = "borrower" Or contextType = "game") Then
                    errorMessages += "There is nothing in " + saveFileName + "."
                End If
                sReader.Close()
            ElseIf (contextType = "borrower" Or contextType = "game") Then
                errorMessages += "Couldn't find " + saveFileName + " ." + vbCrLf
            End If

            Return errorMessages
        End Function

    End Class
End Namespace