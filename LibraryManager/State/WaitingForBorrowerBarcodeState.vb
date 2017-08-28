Imports LibraryManager.Collections
Imports LibraryManager.Actors
Imports System.Windows.Threading

Namespace State
    Public Class WaitingForBorrowerBarcodeState
        Inherits BorrowingMachineState

        Protected WithEvents clearBorrowingFormAndChangeStateTimer As DispatcherTimer

        Public Sub New(ByVal borrowingMachine As BorrowingMachine, ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            MyBase.New(borrowingMachine, borrowingForm, currentBorrowingContext)
            clearBorrowingFormAndChangeStateTimer = New DispatcherTimer(DispatcherPriority.Normal)
            clearBorrowingFormAndChangeStateTimer.Interval = New TimeSpan(0, 0, Constants.CLEAR_FORM_TIMER_INTERVAL)
            AddHandler clearBorrowingFormAndChangeStateTimer.Tick, AddressOf State_Timer_Tick
        End Sub

        Private Sub State_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearBorrowingFormAndChangeStateTimer.Tick
            clearBorrowingFormAndChangeStateTimer.Stop()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
        End Sub

        Public Overrides Sub DisplayInitialStateLayout()
            borrowingForm.ChangeFormColor(Colors.White)
            borrowingForm.DisplayMessageInTextBox(Constants.REQUEST_BORROWER_BARCODE_PROMPT, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Black)
            borrowingForm.DisplayGameInfo("Checking Out: ", borrowingMachine.CurrentGame.ShortenedName)
            borrowingForm.DisplayBorrowerInfo("", "")
        End Sub

        Protected Sub DisplayNotificationPauseAndChangeState(ByVal notification As String, ByVal fontSize As Integer, ByVal fontColor As Color, ByVal nextState As BorrowingMachineState)
            borrowingForm.DisplayMessageInTextBox(notification, fontSize, fontColor)
            borrowingForm.DisableCodeEntry()
            clearBorrowingFormAndChangeStateTimer.Start()

        End Sub

        Public Overrides Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_BARCODE_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCancelEntry()
            borrowingMachine.CurrentGame = Nothing
            borrowingMachine.CurrentBorrower = Nothing
            borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
        End Sub

        Public Overrides Sub ReactToTextEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_BARCODE_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            Dim borrowerBarcode = GetTextBoxAsBarcode()
            Dim borrower As Borrower = currentBorrowingContext.GetBorrower(borrowerBarcode)
            Dim borrowedGame As Game = currentBorrowingContext.CurrentItemOut(borrowerBarcode)

            DisplayNotificationAndPause(borrower.FirstName + " Already Has " + borrowedGame.ShortenedName + " Out", Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            Dim borrowerBarcode = GetTextBoxAsBarcode()
            Dim borrower As Borrower = currentBorrowingContext.GetBorrower(borrowerBarcode)
            Dim borrowerName As String = borrower.FirstName + " " + borrower.LastName

            currentBorrowingContext.CheckOut(borrowingMachine.CurrentGame.Barcode, borrowerBarcode)
            borrowingForm.DisplayBorrowerInfo("To:", borrowerName)

            borrowingMachine.CurrentGame = Nothing
            borrowingMachine.CurrentBorrower = Nothing

            DisplayNotificationPauseAndChangeState("Game Checked Out", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Black, borrowingMachine.WaitingForGameBarcodeState)

            ' Note remove following comment if using BorrowingMachineTests
            ' borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
        End Sub

        Public Overrides Sub ReactToUnregisteredBorrowerBarcodeEntry()
            borrowingMachine.UnregisteredBarcode = GetTextBoxAsBarcode()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForUnregisteredBorrowerNameState
        End Sub

        Public Overrides Sub ReactToCheckedInGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.BORROWER_NOT_GAME_BARCODE_WARNING, Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedOutGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.BORROWER_NOT_GAME_BARCODE_WARNING, Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.BORROWER_NOT_GAME_BARCODE_WARNING, Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub
    End Class
End Namespace