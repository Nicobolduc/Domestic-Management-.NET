
Public Module mConstants

    Public Enum BillingInterval
        WEEKLY = 1
        FORTNIGHTLY = 2
        MONTHLY = 3
        TWO_MONTHS = 4
        ANNUEL = 5
    End Enum

    Public Enum MySQL_FieldTypes
        TINYINT_TYPE = 0
        INT_TYPE = 1
        DOUBLE_TYPE = 2
        VARCHAR_TYPE = 3
        DATETIME_TYPE = 4
    End Enum

    Public Enum Form_Modes
        CONSULT_MODE = 0
        INSERT_MODE = 1
        UPDATE_MODE = 2
        DELETE_MODE = 3
    End Enum

    Public Enum Error_Messages
        ERROR_SAVE_MSG = 5
    End Enum

    Public Enum Validation_Messages
        MANDATORY_VALUE = 6
        NUMERIC_VALUE = 10
    End Enum

    Public Enum CompanyType
        GROCERY_STORE = 1
        SMALL_BUSINESS = 2
        MEGASTORE = 3
        PHARMACY = 4
    End Enum

End Module