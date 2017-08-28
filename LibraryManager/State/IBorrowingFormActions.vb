'
' Makes unit testing easier by allowing the BorrowingFormStub to be used
'
Namespace State
    Public Interface IBorrowingFormActions
        Sub RefocusOnTextBox()
        Sub DisableCodeEntry()
        Sub DisplayMessageInTextBox(ByVal message As String, ByVal size As Integer, ByVal textColor As Color)
        Sub ChangeFormColor(ByVal color As Color)
        Sub DisplayGameInfo(ByVal titleText As String, ByVal mainText As String)
        Sub DisplayBorrowerInfo(ByVal titleText As String, ByVal mainText As String)
        Function GetCodeTextBoxText() As String
    End Interface
End Namespace