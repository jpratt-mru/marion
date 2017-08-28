Imports LibraryManager.Actors
Imports NUnit.Framework

<TestFixture()> _
Public Class BorrowerTests



    <SetUp()> _
    Public Sub SetUp()

    End Sub

    <Test()> _
   Public Sub CopyConstructor_ReturnsAreEqual()
        Dim borrowerCode As New SimpleBarcode("FCBC-1234")
        Dim borrowerFirstName As String = "Jordan"
        Dim borrwerLastName As String = "Pratt"
        Dim expectedBorrower As New Borrower(borrowerCode, borrowerFirstName, borrwerLastName)
        expectedBorrower.HasGameOut = True

        Dim actualBorrower As New Borrower(expectedBorrower)
        Assert.AreEqual(expectedBorrower, actualBorrower)
    End Sub


End Class
