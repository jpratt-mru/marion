Imports LibraryManager.Actors
Imports NUnit.Framework

<TestFixture()> _
Public Class OutstandingLoanTests



    <SetUp()> _
    Public Sub SetUp()

    End Sub

    <Test()> _
   Public Sub CopyConstructor_NowTime_ReturnsAreEqual()
        Dim borrowerCode As New SimpleBarcode("FCBC-1234")
        Dim borrowerFirstName As String = "Jordan"
        Dim borrwerLastName As String = "Pratt"
        Dim borrower As New Borrower(borrowerCode, borrowerFirstName, borrwerLastName)

        Dim gameCode As New SimpleBarcode("FCGC-1234")
        Dim gameName As String = "Goa"
        Dim gameOwner As String = "John Burt"
        Dim game As New Game(gameCode, gameName, gameOwner)
        game.Borrower = borrower

        Dim expectedOutstandingLoan As New OutstandingLoan(game)

        Dim actualOutstandingLoan As New OutstandingLoan(expectedOutstandingLoan)
        Assert.AreEqual(expectedOutstandingLoan, actualOutstandingLoan)
    End Sub

    <Test()> _
   Public Sub CopyConstructor_DifferentTime_ReturnsAreEqual()
        Dim borrowerCode As New SimpleBarcode("FCBC-1234")
        Dim borrowerFirstName As String = "Jordan"
        Dim borrwerLastName As String = "Pratt"
        Dim borrower As New Borrower(borrowerCode, borrowerFirstName, borrwerLastName)

        Dim gameCode As New SimpleBarcode("FCGC-1234")
        Dim gameName As String = "Goa"
        Dim gameOwner As String = "John Burt"
        Dim game As New Game(gameCode, gameName, gameOwner)
        game.Borrower = borrower

        Dim expectedOutstandingLoan As New OutstandingLoan(game, New DateTime(2010, 10, 9, 11, 5, 10))

        Dim actualOutstandingLoan As New OutstandingLoan(expectedOutstandingLoan)
        Assert.AreEqual(expectedOutstandingLoan, actualOutstandingLoan)
    End Sub

End Class
