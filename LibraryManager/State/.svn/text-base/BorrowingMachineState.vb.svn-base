Imports System.Windows.Threading
Imports LibraryManager.Actors
Imports LibraryManager.Collections

'
' State pattern State class
'
Namespace State
    Public MustInherit Class BorrowingMachineState

        Protected WithEvents clearBorrowingFormTimer As DispatcherTimer

        Protected borrowingForm As IBorrowingFormActions
        Protected borrowingMachine As BorrowingMachine
        Protected currentBorrowingContext As CurrentBorrowingContext

        Public MustOverride Sub DisplayInitialStateLayout()
        Public MustOverride Sub ReactToUnregisteredBorrowerBarcodeEntry()
        Public MustOverride Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
        Public MustOverride Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
        Public MustOverride Sub ReactToUnregisteredGameBarcodeEntry()
        Public MustOverride Sub ReactToCheckedOutGameBarcodeEntry()
        Public MustOverride Sub ReactToCheckedInGameBarcodeEntry()
        Public MustOverride Sub ReactToCancelEntry()
        Public MustOverride Sub ReactToTextEntry()

        Public Sub New(ByVal borrowingMachine As BorrowingMachine, ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            If (borrowingMachine Is Nothing OrElse borrowingForm Is Nothing OrElse currentBorrowingContext Is Nothing) Then
                Throw New ArgumentNullException("Null argument passed to BorrowingMachineState constructor.")
            Else
                Me.borrowingMachine = borrowingMachine
                Me.borrowingForm = borrowingForm
                Me.currentBorrowingContext = currentBorrowingContext

                clearBorrowingFormTimer = New DispatcherTimer(DispatcherPriority.Normal)
                clearBorrowingFormTimer.Interval = New TimeSpan(0, 0, Constants.CLEAR_FORM_TIMER_INTERVAL)
                AddHandler clearBorrowingFormTimer.Tick, AddressOf Timer_Tick
            End If
        End Sub

        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearBorrowingFormTimer.Tick
            clearBorrowingFormTimer.Stop()
            DisplayInitialStateLayout()
            borrowingForm.RefocusOnTextBox()
        End Sub

        Public Overridable Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause("Unknown Barcode", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Protected Sub DisplayNotificationAndPause(ByVal notification As String, ByVal fontSize As Integer, ByVal fontColor As Color)
            borrowingForm.DisplayMessageInTextBox(notification, fontSize, fontColor)
            borrowingForm.DisableCodeEntry()
            clearBorrowingFormTimer.Start()
        End Sub

        Protected Function GetTextBoxAsBarcode() As SimpleBarcode
            Dim currentCode As String = borrowingForm.GetCodeTextBoxText
            Return New SimpleBarcode(currentCode)
        End Function

    End Class
End Namespace