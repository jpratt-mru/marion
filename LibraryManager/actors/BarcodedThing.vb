Namespace Actors
    Public MustInherit Class BarcodedThing

        Protected m_barcode As SimpleBarcode
        Public MustOverride Property Barcode() As SimpleBarcode

        Public Sub New()

        End Sub

        Public Sub New(ByVal barcodedThing As BarcodedThing)
            Me.Barcode = New SimpleBarcode(barcodedThing.Barcode)
        End Sub

        Public Sub New(ByVal fileString As String)
            If (fileString Is Nothing Or fileString.Length = 0) Then
                Throw New ArgumentNullException("Context file string can't be empty.")
            ElseIf (Not fileString.Contains(Constants.SPLIT_DELIMITER)) Then
                Throw New ArgumentException("Context file string contains no delimiters.")
            End If
        End Sub

        Public Overrides Function GetHashCode() As Integer
            If Barcode IsNot Nothing Then Return Barcode.GetHashCode()
            Return 0
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(BarcodedThing))) Then Return False
            Dim otherBarcodedThing As BarcodedThing = CType(obj, BarcodedThing)
            Return otherBarcodedThing.Barcode.Equals(Me.Barcode)
        End Function

        Public MustOverride Function ToFileString() As String

    End Class
End Namespace