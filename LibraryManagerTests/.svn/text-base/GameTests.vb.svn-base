Imports LibraryManager.Actors
Imports NUnit.Framework

<TestFixture()> _
Public Class GameTests



    <SetUp()> _
    Public Sub SetUp()

    End Sub

    <Test()> _
   Public Sub CopyConstructor_HasBorrower_ReturnsAreEqual()
        Dim borrowerCode As New SimpleBarcode("FCBC-1234")
        Dim borrowerFirstName As String = "Jordan"
        Dim borrwerLastName As String = "Pratt"
        Dim borrower As New Borrower(borrowerCode, borrowerFirstName, borrwerLastName)

        Dim gameCode As New SimpleBarcode("FCGC-1234")
        Dim gameName As String = "Goa"
        Dim gameOwner As String = "John Burt"
        Dim expectedGame As New Game(gameCode, gameName, gameOwner)
        expectedGame.Borrower = borrower

        Dim actualGame As New Game(expectedGame)
        Assert.AreEqual(expectedGame, actualGame)
    End Sub


    <Test()> _
  Public Sub CopyConstructor_HasNoBorrower_ReturnsAreEqual()
        Dim gameCode As New SimpleBarcode("FCGC-1234")
        Dim gameName As String = "Goa"
        Dim gameOwner As String = "John Burt"
        Dim expectedGame As New Game(gameCode, gameName, gameOwner)
        expectedGame.Borrower = Nothing

        Dim actualGame As New Game(expectedGame)
        Assert.AreEqual(expectedGame, actualGame)
    End Sub

End Class
