'
' Used for unit tests only.
'

Namespace State
    Public Class BorrowingFormStub
        Implements IBorrowingFormActions

        Public stubbedCodeTextBoxText As String

        Public Sub RefocusOnTextBox() Implements IBorrowingFormActions.RefocusOnTextBox

        End Sub

        Public Sub DisableCodeEntry() Implements IBorrowingFormActions.DisableCodeEntry

        End Sub

        Public Sub DisplayMessageInTextBox(ByVal message As String, ByVal size As Integer, ByVal textColor As Color) Implements IBorrowingFormActions.DisplayMessageInTextBox

        End Sub

        Public Sub ChangeFormColor(ByVal color As Color) Implements IBorrowingFormActions.ChangeFormColor

        End Sub

        Public Sub DisplayGameInfo(ByVal titleText As String, ByVal mainText As String) Implements IBorrowingFormActions.DisplayGameInfo

        End Sub

        Public Sub DisplayBorrowerInfo(ByVal titleText As String, ByVal mainText As String) Implements IBorrowingFormActions.DisplayBorrowerInfo

        End Sub

        'Public Sub RefreshItemsOutDisplay() Implements IBorrowingFormActions.RefreshItemsOutDisplay

        'End Sub

        Public Function GetCodeTextBoxText() As String Implements IBorrowingFormActions.GetCodeTextBoxText
            Return stubbedCodeTextBoxText
        End Function

    End Class
End Namespace