Imports LibraryManager
Imports LibraryManager.Actors
Imports NUnit.Framework

<TestFixture()> _
Public Class BarcodeTests



    <SetUp()> _
    Public Sub SetUp()

    End Sub


    <Test()> _
    Public Sub Constructor_CorrectBorrowerCode_ReturnsTypeBorrower()
        Dim actualCode As New SimpleBarcode("FCBC-1234")
        Assert.AreEqual(Enums.BarcodeCategory.Borrower, actualCode.Category)
    End Sub

    <Test()> _
    Public Sub Constructor_CorrectGameCode_ReturnsTypeGame()
        Dim actualCode As New SimpleBarcode("FCGC-1234")
        Assert.AreEqual(Enums.BarcodeCategory.Game, actualCode.Category)
    End Sub

    <Test()> _
    Public Sub Constructor_NoPrefixCode_ReturnsTypeUnknown()
        Dim actualCode As New SimpleBarcode("1234")
        Assert.AreEqual(Enums.BarcodeCategory.Unknown, actualCode.Category)
    End Sub

    <Test()> _
    Public Sub Constructor_IncorrectPrefixCode_ReturnsTypeUnknown()
        Dim actualCode As New SimpleBarcode("FC1234")
        Assert.AreEqual(Enums.BarcodeCategory.Unknown, actualCode.Category)
    End Sub

    <Test()> _
    Public Sub Constructor_ShortDigitsCode_ReturnsTypeUnknown()
        Dim actualCode As New SimpleBarcode("FCLC12")
        Assert.AreEqual(Enums.BarcodeCategory.Unknown, actualCode.Category)
    End Sub

    <Test()> _
    Public Sub Constructor_NonDigitsPresentCode_ReturnsTypeUnknown()
        Dim actualCode As New SimpleBarcode("FCLC1234A")
        Assert.AreEqual(Enums.BarcodeCategory.Unknown, actualCode.Category)
    End Sub

    <Test()> _
   Public Sub CopyConstructor_ReturnsAreEqual()
        Dim actualCode As New SimpleBarcode("FCGC-1234")
        Dim expectedCode As New SimpleBarcode(actualCode)
        Assert.AreEqual(expectedCode, actualCode)
    End Sub

End Class
