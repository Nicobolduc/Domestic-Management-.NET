
Public Module mConstants

    Public Enum Language
        FRENCH_QC = 1
        ENGLISH_CA = 2
    End Enum

    Public Enum Period
        WEEKLY = 1
        FORTNIGHTLY = 2
        MONTHLY = 3
        TWO_MONTHS = 4
        ANNUEL = 5
        ONCE = 6
    End Enum

    Public Enum Form_Mode
        CONSULT_MODE = 0
        INSERT_MODE = 1
        UPDATE_MODE = 2
        DELETE_MODE = 3
    End Enum

    Public Enum Error_Message
        ERROR_SAVE_MSG = 5
        ERROR_ITEM_USED_MSG = 19
    End Enum

    Public Enum Validation_Message
        MANDATORY_VALUE = 6
        NUMERIC_VALUE = 10
        UNIQUE_ATTRIBUTE = 17
    End Enum

    Public Enum CompanyType
        GROCERY_STORE = 1
        SMALL_BUSINESS = 2
        MEGASTORE = 3
        PHARMACY = 4
    End Enum

    Public Enum TaxeType
        TPS = 1
        TVQ = 2
    End Enum

    Structure DataFormat
        Const CURRENCY = "##,0.00$"
    End Structure

End Module