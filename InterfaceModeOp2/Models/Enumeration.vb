Public Module Enumeration
    'pas de fonction CRUD pour ces tables

    Public Enum Operations
        Undefined = -1
        Impression = 0
        Importation = 1
        Exportation = 2
        Archivage = 3
    End Enum

    Public Enum Droits
        NoUse = -1
        Guest = 0
        User = 1
        KeyUser = 2
        UserAQ = 3
        AdminAQ = 4
        AdminDvlp = 5
    End Enum

    Public Enum Verification
        Undefined = -1
        NoVerif = 0
        EnCours = 1
        VerifOK = 2
    End Enum
End Module
