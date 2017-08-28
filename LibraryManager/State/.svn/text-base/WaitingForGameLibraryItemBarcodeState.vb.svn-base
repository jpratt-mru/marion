Imports LibraryManager.Actors
Imports LibraryManager.Collections
Namespace State
    Public Class WaitingForGameBarcodeState
        Inherits BorrowingMachineState

        Public Sub New(ByVal borrowingMachine As BorrowingMachine, ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            MyBase.New(borrowingMachine, borrowingForm, currentBorrowingContext)
        End Sub

        Public Overrides Sub DisplayInitialStateLayout()
            borrowingForm.ChangeFormColor(Colors.White)
            borrowingForm.DisplayMessageInTextBox(Constants.REQUEST_GAME_BARCODE_PROMPT, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Black)
            borrowingForm.DisplayGameInfo("", "")
            borrowingForm.DisplayBorrowerInfo("", "")
        End Sub


        Public Overrides Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_BARCODE_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCancelEntry()

        End Sub

        Public Overrides Sub ReactToTextEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_BARCODE_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.REQUEST_GAME_BARCODE_NOT_BORROWER_PROMPT, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.REQUEST_GAME_BARCODE_NOT_BORROWER_PROMPT, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredBorrowerBarcodeEntry()
            DisplayNotificationAndPause(Constants.REQUEST_GAME_BARCODE_NOT_BORROWER_PROMPT, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedInGameBarcodeEntry()
            Dim currentCode As String = borrowingForm.GetCodeTextBoxText
            Dim gameBarcode As New SimpleBarcode(currentCode)
            Dim itemGoingOut As Game = currentBorrowingContext.GetGame(gameBarcode)
            borrowingMachine.CurrentGame = itemGoingOut
            borrowingMachine.CurrentState = borrowingMachine.WaitingForBorrowerBarcodeState
        End Sub

        Public Overrides Sub ReactToCheckedOutGameBarcodeEntry()
            Dim currentCode As String = borrowingForm.GetCodeTextBoxText
            Dim gameBarcode As New SimpleBarcode(currentCode)
            Dim itemComingIn As Game = currentBorrowingContext.GetGame(gameBarcode)
            Dim itemName As String = itemComingIn.ShortenedName

            Dim borrower As Borrower = itemComingIn.Borrower
            Dim borrowerName As String = borrower.FirstName + " " + borrower.LastName

            borrowingMachine.CurrentGame = Nothing
            borrowingMachine.CurrentBorrower = Nothing

            borrowingForm.DisplayGameInfo("Checking In: ", itemName)
            borrowingForm.DisplayBorrowerInfo("From Borrower: ", borrowerName)

            borrowingForm.DisplayMessageInTextBox("Item Checked In", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Black)

            currentBorrowingContext.CheckIn(gameBarcode)
            'borrowingForm.RefreshItemsOutDisplay()

            borrowingForm.DisableCodeEntry()
            clearBorrowingFormTimer.Start()

        End Sub

        Public Overrides Sub ReactToUnregisteredGameBarcodeEntry()
            Dim currentCode As String = borrowingForm.GetCodeTextBoxText
            borrowingMachine.UnregisteredBarcode = New SimpleBarcode(currentCode)
            borrowingMachine.CurrentState = borrowingMachine.WaitingForUnregisteredGameNameState()
        End Sub

    End Class
End Namespace