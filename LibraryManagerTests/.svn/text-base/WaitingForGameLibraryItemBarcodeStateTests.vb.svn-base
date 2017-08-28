Imports LibraryManager.Collections
Imports LibraryManager.Actors
Imports NUnit.Framework
Imports LibraryManager.State

<TestFixture()> _
Public Class WaitingForGameBarcodeStateTests

    Private borrowingMachineTester As BorrowingMachineTester
    Private borrowingMachine As BorrowingMachine
    Private currentBorrowingContext As CurrentBorrowingContext
    Private registeredGameCheckedInInitially As Game
    Private registeredGameCheckedOutInitially As Game
    Private unregisteredGame As Game
    Private registeredBorrowerWithItemOutInitially As Borrower
    Private registeredBorrowerWithNoItemOutInitially As Borrower
    Private unregisteredBorrower As Borrower
    Private startingOutstandingLoan As OutstandingLoan
    Private expectedContext As CurrentBorrowingContext

    <SetUp()> _
    Public Sub SetUp()
        borrowingMachineTester = New BorrowingMachineTester()
        borrowingMachine = borrowingMachineTester.BorrowingMachine
        currentBorrowingContext = borrowingMachineTester.CurrentBorrowingContext

        Dim registeredGameCheckedInInitiallyBarcode As New SimpleBarcode("FCGC-0001")
        Dim registeredGameCheckedOutInitiallyBarcode As New SimpleBarcode("FCGC-0002")
        Dim unregisteredGameBarcode As New SimpleBarcode("FCGC-0003")

        registeredGameCheckedInInitially = New Game(registeredGameCheckedInInitiallyBarcode, "Goa", "JP")
        registeredGameCheckedOutInitially = New Game(registeredGameCheckedOutInitiallyBarcode, "Power Grid", "JP")
        unregisteredGame = New Game(unregisteredGameBarcode, "Dominion", "JP")

        Dim registeredBorrowerWithItemOutInitiallyBarcode As New SimpleBarcode("FCBC-00001")
        Dim registeredBorrowerWithNoItemOutInitiallyBarcode As New SimpleBarcode("FCBC-00002")
        Dim unregisteredBorrowerBarcode As New SimpleBarcode("FCBC-00003")

        registeredBorrowerWithItemOutInitially = New Borrower(registeredBorrowerWithItemOutInitiallyBarcode, "Jordan", "Pratt")
        registeredBorrowerWithNoItemOutInitially = New Borrower(registeredBorrowerWithNoItemOutInitiallyBarcode, "Jasen", "Robillard")
        unregisteredBorrower = New Borrower(unregisteredBorrowerBarcode, "Mark", "Stadel")

        registeredGameCheckedOutInitially.Borrower = registeredBorrowerWithItemOutInitially
        registeredBorrowerWithItemOutInitially.HasGameOut = True

        currentBorrowingContext.Add(registeredGameCheckedInInitially)
        currentBorrowingContext.Add(registeredGameCheckedOutInitially)
        currentBorrowingContext.Add(registeredBorrowerWithItemOutInitially)
        currentBorrowingContext.Add(registeredBorrowerWithNoItemOutInitially)
        currentBorrowingContext.CheckOut(registeredGameCheckedOutInitiallyBarcode, registeredBorrowerWithItemOutInitiallyBarcode)

        expectedContext = New CurrentBorrowingContext(currentBorrowingContext)

    End Sub

    <Test()> _
    Public Sub ReactToUnknownBarcode_StaysInCurrentState_ContextStaysSame()
        Dim unknownBarcodeString As String = LibraryManager.Constants.BORROWER_PREFIX

        borrowingMachineTester.SimulateStringInput(unknownBarcodeString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToUnregisteredBorrowerBarcodeEntry_StaysInCurrentState_ContextStaysSame()
        borrowingMachineTester.SimulateStringInput(unregisteredBorrower.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToBorrowerWithOneGameOutBarcodeEntry_StaysInCurrentState_ContextStaysSame()
        borrowingMachineTester.SimulateStringInput(registeredBorrowerWithItemOutInitially.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToBorrowerWithNoGameOutBarcodeEntry_StaysInCurrentState_ContextStaysSame()
        borrowingMachineTester.SimulateStringInput(registeredBorrowerWithNoItemOutInitially.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToUnregisteredGameBarcodeEntry_MovesToWaitingForUnregisteredGameNameState_ContextStaysSame()
        borrowingMachineTester.SimulateStringInput(unregisteredGame.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForUnregisteredGameNameState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToCheckedOutGameBarcodeEntry_StaysInCurrentState_ContextChanged_GameCheckedIn()
        borrowingMachineTester.SimulateStringInput(registeredGameCheckedOutInitially.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreNotEqual(expectedContext, actualContext)
        Assert.AreEqual(Nothing, currentBorrowingContext.CurrentBorrower(registeredGameCheckedOutInitially.Barcode))
    End Sub

    <Test()> _
    Public Sub ReactToCheckedInGameBarcodeEntry_MovesToWaitingForBorrowerBarcodeState_ContextStaysSame()
        borrowingMachineTester.SimulateStringInput(registeredGameCheckedInInitially.Barcode.ToString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForBorrowerBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToCancelEntry_StaysInCurrentState_ContextStaysSame()
        borrowingMachineTester.SimulateEscapeInput()

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

    <Test()> _
    Public Sub ReactToTextEntry_StaysInCurrentState_ContextStaysSame()
        Dim unknownBarcodeString As String = "Text"

        borrowingMachineTester.SimulateStringInput(unknownBarcodeString)

        Dim actualContext As CurrentBorrowingContext = currentBorrowingContext

        Assert.IsInstanceOf(GetType(WaitingForGameBarcodeState), borrowingMachine.CurrentState)
        Assert.AreEqual(expectedContext, actualContext)
    End Sub

End Class
