Namespace Actors
    Public Class BarcodedThingFactory
        Public Shared Function createBarcodedThing(ByVal type As String, ByVal fileLine As String) As BarcodedThing
            Dim barcodedThing As BarcodedThing = Nothing
            If (type = "borrower") Then
                barcodedThing = New Borrower(fileLine)
            ElseIf (type = "game") Then
                barcodedThing = New Game(fileLine)
            ElseIf (type = "outstandingLoan") Then
                barcodedThing = New OutstandingLoan(fileLine)
            End If
            Return barcodedThing
        End Function
    End Class
End Namespace