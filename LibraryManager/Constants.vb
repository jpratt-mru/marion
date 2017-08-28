Public Class Constants
    Public Const LARGE_MESSAGE_FONT_SIZE As Integer = 20
    Public Const SMALL_MESSAGE_FONT_SIZE As Integer = 14

    Public Const MAX_CHECKOUT_DISPLAY_GAME_LENGTH As Integer = 22
    Public Const MAX_CHECKOUT_DISPLAY_BORROWER_LENGTH As Integer = 22
    Public Const CHECKOUT_DISPLAY_GAME_FIELD_WIDTH As Integer = 30
    Public Const CHECKOUT_DISPLAY_BORROWER_FIELD_WIDTH As Integer = 30

    Public Const SHORTENED_NAME_LENGTH As Integer = 20
    Public Const CHECKOUT_DISPLAY_GUTTER_WIDTH As Integer = 3

    Public Const PREFIX_SUFFIX_SEPARATOR As String = "-"
    Public Const GAME_PREFIX As String = "FCGC"
    Public Const BORROWER_PREFIX As String = "FCBC"
    Public Const MIN_NUMBER_DIGITS_IN_BORROWER_SUFFIX As Integer = 3
    Public Const MAX_NUMBER_DIGITS_IN_BORROWER_SUFFIX As Integer = 9
    Public Const MIN_NUMBER_DIGITS_IN_GAME_SUFFIX As Integer = 4
    Public Const MAX_NUMBER_DIGITS_IN_GAME_SUFFIX As Integer = 4

    Public Const BORROWER_FILE_NAME As String = "borrowers.csv"
    Public Const GAME_LIBRARY_ITEMS_FILE_NAME As String = "games.csv"
    Public Const OUTSTANDING_LOANS_FILE_NAME As String = "outstandingLoans.csv"
    Public Const LOG_FILE_NAME As String = "log.txt"

    Public Const CLEAR_FORM_TIMER_INTERVAL As Double = 1.5

    Public Const REQUEST_BORROWER_BARCODE_PROMPT As String = "Please scan a borrower barcode"
    Public Const BORROWER_NOT_GAME_BARCODE_WARNING As String = "Need a borrower barcode, not a game barcode"
    Public Const NOT_VALID_BORROWER_BARCODE_WARNING As String = "That's not a valid borrower barcode"


    Public Const REQUEST_GAME_BARCODE_PROMPT As String = "Please scan a game barcode"
    Public Const REQUEST_GAME_BARCODE_NOT_BORROWER_PROMPT As String = "Please scan a game barcode first"
    Public Const NOT_VALID_GAME_BARCODE_WARNING As String = "That's not a valid game barcode"

    Public Const NOT_VALID_BORROWER_NAME_TEXT_WARNING As String = "Need the borrower's name"
    Public Const NOT_VALID_GAME_NAME_TEXT_WARNING As String = "Need the game's name"
    Public Const NOT_VALID_GAME_OWNER_TEXT_WARNING As String = "Need the game's owner's name"

    Public Const SPLIT_DELIMITER As String = ","

End Class
